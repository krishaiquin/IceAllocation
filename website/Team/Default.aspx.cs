using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*
 * Author: Adrian, Jonathan & Krisha
 * 
 */
public partial class Team_Default : System.Web.UI.Page
{

    Team team;
    bool reviewing, notifyAllocator;

    //Author: Adrian
    protected void Page_Load(object sender, EventArgs e)
    {

        team = new Team(User.Identity.Name);

        //sets invisible label to login name so that it can be passed to SQL statment
        LabelUserName.Text = User.Identity.Name;

        reviewing = ((AllocationSettings)Application["Settings"]).getReviewingOn().Equals("true");
        notifyAllocator = ((AllocationSettings)Application["Settings"]).getReceiveNotifications().Equals("true");

        //checks if there is annoucment or site shutdown, hide if there are no announcements
        if ((((AllocationSettings)Application["Settings"]).getAnnouncement() != null) &&
                !(((AllocationSettings)Application["Settings"]).getAnnouncement().ToString().Equals("")) ||
            ((((AllocationSettings)Application["Settings"]).getAppOpen()).ToString().Equals("false")))
        {
            pnlMessages.Visible = true;

            //Grabs announcement
            if ((((AllocationSettings)Application["Settings"]).getAnnouncement() != null) &&
                !(((AllocationSettings)Application["Settings"]).getAnnouncement().ToString().Equals("")))
            {
                pnlAnnouncements.Visible = true;
                lblAnnouncements.Text = ((AllocationSettings)Application["Settings"]).getAnnouncement();
            }
            else
            {
                pnlAnnouncements.Visible = false;
            }

            //Checks for Site shutdown
            if ((((AllocationSettings)Application["Settings"]).getAppOpen()).ToString().Equals("false"))
            {
                pnlShutdown.Visible = true;
                lblShutdownMsg.Text = "Site is currently disabled";
                btnReturnGameSel.Enabled = false;
                btnReturnPracticeSel.Enabled = false;
                btnRequestGameSel.Enabled = false;
                btnRequestPracticeSel.Enabled = false;
            }
            else
            {
                pnlShutdown.Visible = false;
            }

        }

        else
        {
            pnlMessages.Visible = false;
        }

        //Grabs game and practice balances, and sets it to assigned labels
        team.loadBalances(User.Identity.Name);
        lblGameBalance.Text = team.getGameBalance().ToString();
        lblPracticeBalance.Text = team.getPracticeBalance().ToString();

        //hide elements if they are empty
        if (gridAvailOpenIce.Rows.Count == 0)
            pnlAvailOpenIce.Visible = false;
        if (gridOpenIce.Rows.Count == 0)
            pnlOpenIce.Visible = false;
        if (gridAvailableGameIce.Rows.Count == 0)
            btnRequestGameSel.Visible = false;
        if (gridAvailablePracticeIce.Rows.Count == 0)
            btnRequestPracticeSel.Visible = false;
        if (gridScheduledGameIce.Rows.Count == 0)
            btnReturnGameSel.Visible = false;
        if (gridScheduledPracticeIce.Rows.Count == 0)
            btnReturnPracticeSel.Visible = false;

    }

    //method to claim open ice
    //Author: Adrian
    protected void gridAvailOpenIce_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        clearLabels();

        if (e.CommandName == "ClaimOpen")
        {
            int iceId = int.Parse(gridAvailOpenIce.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());

            string comment = "Open ice claimed";
            string commentAll = "Open ice claimed by " + User.Identity.Name;
            int count = -1;
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
                SqlCommand chkTeamsOpen = new SqlCommand("SELECT COUNT(*) AS COUNT FROM IceTimes WHERE IceId=@IceId AND TeamId IS NOT NULL", con);
                SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
                sqlIceId.Value = iceId;
                chkTeamsOpen.Parameters.Add(sqlIceId);
                con.Open();

                SqlDataReader reader = chkTeamsOpen.ExecuteReader();
                reader.Read();
                count = int.Parse(reader["Count"].ToString());
            }
            catch (SqlException ex)
            {
                //unable to check if ice is still available
                lblOpenStatus.Text = "Unable to verify if ice is still available.";
            }

            if (count == 0)
            {
                IceManipulation.assignIce(iceId, team.getTeamId(), comment, commentAll);
                lblOpenStatus.Text = "Open ice has been assigned to you.";
                gridAvailOpenIce.DataBind();
                gridOpenIce.DataBind();
            }
            else if (count < 0)
            {
                lblOpenStatus.Text = "Sorry, the open ice has already been claimed by another team.";
                gridAvailOpenIce.DataBind();
            }


        }
    }

    //single game and practice return
    //Author: Jonathan
    protected void ReturnCommand(object sender, GridViewCommandEventArgs e)
    {
        clearLabels();
   

        Boolean gameReturned = e.CommandName == "ReturnGame";
        int iceId;
        //Verify what the source of the command is.
        //If game ice is returned
        if (gameReturned || e.CommandName == "ReturnPractice")
        {
            //Retrieves the ice id value from the data key table[rowindex, rowvalue] 
            //by using the row index that was given as an argument to the button
            if (gameReturned)
                iceId = int.Parse(gridScheduledGameIce.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
            else
                iceId = int.Parse(gridScheduledPracticeIce.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());

            IceTime returnedIce = new IceTime(iceId);

            //Perform the actual return in the database
            string success = IceManipulation.returnIce(iceId, team.getTeamId(), reviewing);

            //If the return was successful, take appropriate actions
            if (success.Equals("true"))
            {
                //If it was a game
                if (gameReturned)
                {
                    team.decreaseGame();

                    //Inform the user of success.
                    lblReturnGameMessage.Text = "The Ice Time has been successfully returned.";
                    lblGameBalanceReturn.Text = "1 game has been subtracted from your balance.";

                    //Update game balance label
                    team.loadBalances(User.Identity.Name);
                    lblGameBalance.Text = team.getGameBalance().ToString();

                    //Refreshes grid view to show changes
                    gridScheduledGameIce.DataBind();
                    gridAvailableGameIce.DataBind();

                }
                else
                {
                    //Check if the ice time is in the game window
                    IceTime ice = new IceTime(iceId);
                    if (ice.isInGameWindow(((AllocationSettings)Application["Settings"])))
                    {
                        ice.setIceType("Game");
                        ice.save();
                        gridScheduledGameIce.DataBind();
                        gridAvailableGameIce.DataBind();
                    }


                    team.decreasePractice();
                    //Inform the user of success.
                    lblReturnPracticeMessage.Text = "The Ice Time has been successfully returned.";
                    lblPracticeBalanceReturn.Text = "1 practice has been subtracted from your balance.";

                    //Update practice balance label
                    team.loadBalances(User.Identity.Name);
                    lblPracticeBalance.Text = team.getPracticeBalance().ToString();

                    //Refreshes grid view to show changes
                    gridScheduledPracticeIce.DataBind();
                    gridAvailablePracticeIce.DataBind();
                }




                //Prep an email message to inform the allocator of the returned ice.
                if (notifyAllocator)
                {

                    //End of try catch, ice has been returned, add message and send email.
                    IceEmail notifyReturn = new IceEmail(((AllocationSettings)Application["Settings"]).getAdminEmail());
                    notifyReturn.addSubject(Constant.NOTIFY_RETURN_SUBJECT);
                    notifyReturn.setMessage(Constant.NOTIFY_RETURN_MESSAGE + User.Identity.Name + Environment.NewLine + Environment.NewLine + returnedIce.ToString());

                    //Send message and return status
                    if (gameReturned)
                        lblReturnGameMessage.Text = "Notify Allocator: " + notifyReturn.send();
                    else
                        lblReturnPracticeMessage.Text = "Notify Allocator: " + notifyReturn.send();
                }


                //Prepare an email to inform other teams of returned Ice if Allocator reviewing is turned off
                //if not reviewing ice is immediately available and teams should be informed
                if (!reviewing){
                    IceEmail notifyOthers = new IceEmail();
                    notifyOthers.addBccRecipients(gameReturned ? Team.getAllNegativeGame() : Team.getAllNegativePractice());

                    //Check that email addresses were added, prevents error if all teams are balanced
                    if (notifyOthers.getBccCount() > 0)
                    {
                        notifyOthers.addSubject(Constant.NOTIFY_OTHERS_SUBJECT);
                        notifyOthers.setMessage(Constant.NOTIFY_OTHERS_MESSAGE + Environment.NewLine + Environment.NewLine + returnedIce.ToString());

                        //Send message and return status to correct location
                        if (gameReturned)
                            lblReturnGameMessage.Text += "Notify Other Teams: " + notifyOthers.send();
                        else
                            lblReturnPracticeMessage.Text += "Notify Other Teams: " + notifyOthers.send();
                    }
                }
            }
            else
            {
                if (gameReturned)
                    lblReturnGameMessage.Text = success;
                else
                    lblReturnPracticeMessage.Text = success;
            }
        }
    }

    //multiple game ice return
    //Author: Adrian
    protected void btnReturnGameSel_Click(object sender, EventArgs e)
    {
        clearLabels();

        string gameInfo = "";
        int iceId;
        int count = 0;
        IceTime ice;
        

        foreach (GridViewRow row in gridScheduledGameIce.Rows)
        {
            if (((CheckBox)row.FindControl("chbReturn")).Checked)
            {
                iceId = int.Parse(((Label)row.FindControl("lblIceId")).Text);
                ice = new IceTime(iceId);
                string success = IceManipulation.returnIce(iceId, team.getTeamId(), reviewing);
                if (success.Equals("true"))
                {
                    team.decreaseGame();
                    count++;

                      //add this ice info to the email message.
                    gameInfo += ice.ToString() + Environment.NewLine;
                }

            }
        }
        if (count > 0)
        {
            //Inform the user of success.
            lblReturnGameMessage.Text = count + " ice times successfully returned.";
            lblGameBalanceReturn.Text = count + " games has been subtracted to your balance.";

            //Update game balance label
            team.loadBalances(User.Identity.Name);
            lblGameBalance.Text = team.getGameBalance().ToString();

            //Refreshes grid view to show changes
            gridScheduledGameIce.DataBind();
            gridAvailableGameIce.DataBind();


            if (notifyAllocator)
            {
                //send email and notify user
                IceEmail notifyReturn = new IceEmail(((AllocationSettings)Application["Settings"]).getAdminEmail());
                notifyReturn.addSubject(Constant.NOTIFY_RETURN_SUBJECT);
                string message = Constant.NOTIFY_RETURN_MESSAGE + User.Identity.Name + Environment.NewLine + gameInfo;

                notifyReturn.setMessage(message);
                lblReturnGameMessage.Text = notifyReturn.send();
            }


            //Prepare an email to inform other teams of returned Ice if Allocator reviewing is turned off
            if (!reviewing)
            {
                IceEmail notifyOthers = new IceEmail();
          
                notifyOthers.addBccRecipients(Team.getAllNegativeGame());
            

                //Check that email addresses were added, prevents error if all teams are balanced
                if (notifyOthers.getBccCount() > 0)
                {
                    notifyOthers.addSubject("New Ice is available for Request.");
                    notifyOthers.setMessage("The following ice has been returned and is available for request: " + Environment.NewLine + gameInfo);

                    //Send message and return status
                    lblReturnGameMessage.Text += "Notify Other Teams: " + notifyOthers.send();
                }
            }

        }
        else
        {
            lblReturnGameMessage.Text = "No game times have been selected.";
        }
    }

    //multiple practice ice return
    //Author: Adrian
    protected void btnReturnPracticeSel_Click(object sender, EventArgs e)
    {
        clearLabels();

      
        string gameInfo = ""; //Records info to attach to email to all teams
        int iceId;
        int count = 0;
        IceTime ice;

        foreach (GridViewRow row in gridScheduledPracticeIce.Rows)
        {
            if (((CheckBox)row.FindControl("chbReturn")).Checked)
            {
                iceId = int.Parse(((Label)row.FindControl("lblIceId")).Text);
                ice = new IceTime(iceId);
                string success = IceManipulation.returnIce(iceId, team.getTeamId(), reviewing);
                if (success.Equals("true"))
                {
                    team.decreasePractice();
                    count++;

                    //add this ice info to the email message.
      
                    gameInfo += ice.ToString() + Environment.NewLine;


                    //Check if the ice time is in the game window
                    if (ice.isInGameWindow(((AllocationSettings)Application["Settings"])))
                    {
                        ice.setIceType("Game");
                        ice.save();
                        gridScheduledGameIce.DataBind();
                        gridAvailableGameIce.DataBind();
                    }
                }
            }
        }

        if (count > 0)
        {
            //Inform the user of success.
            lblReturnPracticeMessage.Text = count + " ice times successfully returned.";
            lblPracticeBalanceReturn.Text = count + " game has been subtracted to your balance.";

            //Update game balance label
            team.loadBalances(User.Identity.Name);
            lblPracticeBalance.Text = team.getPracticeBalance().ToString();

            //Refreshes grid view to show changes
            gridScheduledPracticeIce.DataBind();
            gridAvailablePracticeIce.DataBind();

            if (notifyAllocator)
            {
                IceEmail notifyReturn = new IceEmail(((AllocationSettings)Application["Settings"]).getAdminEmail());
                notifyReturn.addSubject(Constant.NOTIFY_RETURN_SUBJECT);
                string message = Constant.NOTIFY_RETURN_MESSAGE + User.Identity.Name + Environment.NewLine + gameInfo;
                //send email and notify user
                notifyReturn.setMessage(message);
                lblReturnPracticeMessage.Text = notifyReturn.send();
            }

            //Prepare an email to inform other teams of returned Ice if Allocator reviewing is turned off
            if (!reviewing)
            {
                IceEmail notifyOthers = new IceEmail();

                notifyOthers.addBccRecipients(Team.getAllNegativePractice());


                //Check that email addresses were added, prevents error if all teams are balanced
                if (notifyOthers.getBccCount() > 0)
                {
                    notifyOthers.addSubject("New Ice is available for Request.");
                    notifyOthers.setMessage("The following ice has been returned and is available for request: " + Environment.NewLine + gameInfo);

                    //Send message and return status
                    lblReturnPracticeMessage.Text += "Notify Other Teams: " + notifyOthers.send();
                }
            }
        }
        else
        {
            lblReturnPracticeMessage.Text = "No practice times have been selected.";
        }

    }

    //multiple game requests
    //Author: Adrian
    protected void btnRequestGameSel_Click(object sender, EventArgs e)
    {
        clearLabels();

      
        string gameInfo = "";
        int iceId;
        int count = 0;
        IceTime ice;

        foreach (GridViewRow row in gridAvailableGameIce.Rows)
        {
            if (((CheckBox)row.FindControl("chbRequest")).Checked)
            {
                iceId = int.Parse(((Label)row.FindControl("lblIceId")).Text);
                ice = new IceTime(iceId);
                string success = IceManipulation.requestIce(iceId, team.getTeamId());
                if (success.Equals("true"))
                {
                    count++;

                    //add this ice info to the email message.
                    gameInfo += ice.ToString() + Environment.NewLine;
                }
            }
        }
        if (count > 0)
        {
            lblRequestGameMessage.Text = count + " game times have been requested.";
            gridAvailableGameIce.DataBind();

            if (notifyAllocator)
            {
                IceEmail notifyRequest = new IceEmail(((AllocationSettings)Application["Settings"]).getAdminEmail());
                notifyRequest.addSubject(Constant.NOTIFY_REQUEST_SUBJECT);
                string message = Constant.NOTIFY_REQUEST_MESSAGE+ User.Identity.Name + Environment.NewLine + gameInfo;
                //send email and notify user
                notifyRequest.setMessage(message);
                lblRequestGameMessage.Text = notifyRequest.send();
            }
        }
        else
        {
            lblRequestGameMessage.Text = "No game times have been selected.";
        }

    }

    //multiple practice requests
    //Author: Adrian
    protected void btnRequestPracticeSel_Click(object sender, EventArgs e)
    {
        clearLabels();

      

        string gameInfo = "";
        int iceId;
        int count = 0;
        IceTime ice;

        foreach (GridViewRow row in gridAvailablePracticeIce.Rows)
        {
            if (((CheckBox)row.FindControl("chbRequest")).Checked)
            {
                iceId = int.Parse(((Label)row.FindControl("lblIceId")).Text);
                ice = new IceTime(iceId);

                string success = IceManipulation.requestIce(iceId, team.getTeamId());
                if (success.Equals("true"))
                {
                    count++;
                    //add this ice info to the email message.
                    gameInfo += ice.ToString() + Environment.NewLine;
                }
            }
        }

        if (count > 0)
        {
            lblRequestPracticeMessage.Text = count + " practice times have been requested.";
            gridAvailablePracticeIce.DataBind();


            if (notifyAllocator)
            {
                IceEmail notifyRequest = new IceEmail(((AllocationSettings)Application["Settings"]).getAdminEmail());
                notifyRequest.addSubject("Requested ice.");
                string message = "The following ice has been requested by: " + User.Identity.Name + Environment.NewLine+gameInfo;
                //send email and notify user
                notifyRequest.setMessage(message);
                lblRequestPracticeMessage.Text = notifyRequest.send();
            }
        }
        else
        {
            lblRequestPracticeMessage.Text = "No practice times have been selected.";
        }

    }

    //Method to handle Single game or practice request
    //Author: Jonathan
    protected void RequestCommand(object sender, GridViewCommandEventArgs e)
    {
        clearLabels();

        Boolean gameRequest = (e.CommandName == "RequestGame");
        int iceId;

        //Retrieves the ice id value from the data key table[rowindex, rowvalue] 
        //by using the row index that was given as an argument to the button
        if (gameRequest || e.CommandName == "RequestPractice")
        {
            if (gameRequest)
                iceId = int.Parse(gridAvailableGameIce.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
            else
                iceId = int.Parse(gridAvailablePracticeIce.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());

            IceTime requestedIce = new IceTime(iceId);

            string success = IceManipulation.requestIce(iceId, team.getTeamId());


            if (success.Equals("true"))
            {
                //Verify what the source of the command is.
                //If game ice is requested
                if (gameRequest)
                {
                    //Inform the user of success.
                    lblRequestGameMessage.Text = "The Ice Time was successfully requested.";
                    //Refreshes grid view to show changes
                    gridAvailableGameIce.DataBind();
                }
                //If practice ice is requested
                else
                {
                    //Inform the user of success.
                    lblRequestPracticeMessage.Text = "The Ice Time was successfully requested.";
                    //Refreshes grid view to show changes
                    gridAvailablePracticeIce.DataBind();

                }

                if (notifyAllocator)
                {

                    //prep an email message to inform the allocator of the returned ice.
                    IceEmail notifyRequest = new IceEmail();
                    notifyRequest.addRecipients(((AllocationSettings)Application["Settings"]).getAdminEmail());
                    notifyRequest.addSubject(Constant.NOTIFY_REQUEST_SUBJECT);
                    //End of try catch notify allocator of the successful request.
                    notifyRequest.setMessage(Constant.NOTIFY_REQUEST_MESSAGE+ User.Identity.Name 
                        + Environment.NewLine + requestedIce.ToString());
                    notifyRequest.send();
                }
            }

            else
            {
                if (gameRequest)
                    lblRequestGameMessage.Text = success;
                else
                    lblRequestPracticeMessage.Text = success;
            }
        }
    }

    //Method to handle scheduled ice grid views
    //Author: Adrian
    protected void gridScheduled_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Check for site shutdown status
            if ((((AllocationSettings)Application["Settings"]).getAppOpen()).ToString().Equals("false"))
            {
                ((Button)e.Row.FindControl("btnReturn")).Enabled = false;
                ((Button)e.Row.FindControl("btnReturn")).Text = "Disabled";
                ((CheckBox)e.Row.FindControl("chbReturn")).Enabled = false;
            }

            if (int.Parse(((Label)e.Row.FindControl("lblReceived")).Text) > 0)
            {
                ((Button)e.Row.FindControl("btnReturn")).Enabled = false;
                ((Button)e.Row.FindControl("btnReturn")).Text = "Received";
                ((CheckBox)e.Row.FindControl("chbReturn")).Enabled = false;
            }
        }
    }


    //Method to handle returned ice grid views
    //Method to check status of returned ice, and change colour and/or disable button
    //Author: Adrian
    protected void gridReturnedIce_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        team.loadBalances(User.Identity.Name);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Check for site shutdown
            if ((((AllocationSettings)Application["Settings"]).getAppOpen()).ToString().Equals("false"))
            {
                ((Button)e.Row.FindControl("btnRequest")).Enabled = false;
                ((Button)e.Row.FindControl("btnRequest")).Text = "Disabled";
                ((CheckBox)e.Row.FindControl("chbRequest")).Enabled = false;
            }
            else
            {

                //Check game balance, disable game request if 0
                if (sender == gridAvailableGameIce)
                {
                    if (team.getGameBalance() >= 0)
                    {
                        ((Button)e.Row.FindControl("btnRequest")).Enabled = false;
                        ((Button)e.Row.FindControl("btnRequest")).Text = "Balanced";
                        ((CheckBox)e.Row.FindControl("chbRequest")).Enabled = false;
                    }
                }

                //Check practice balance, disable practice request if 0
                else if (sender == gridAvailablePracticeIce)
                {
                    if (team.getPracticeBalance() >= 0)
                    {
                        ((Button)e.Row.FindControl("btnRequest")).Enabled = false;
                        ((Button)e.Row.FindControl("btnRequest")).Text = "Balanced";
                        ((CheckBox)e.Row.FindControl("chbRequest")).Enabled = false;
                    }
                }

                //Checks if ice time has already been requested by team
                if ((int)DataBinder.Eval(e.Row.DataItem, "Requested") == 1)
                {
                    e.Row.BackColor = Color.FromName("Yellow");
                    ((Button)e.Row.FindControl("btnRequest")).Enabled = false;
                    ((Button)e.Row.FindControl("btnRequest")).Text = "Requested";
                    ((CheckBox)e.Row.FindControl("chbRequest")).Enabled = false;
                }

                //Check if ice time was returned by current team
                if (((string)DataBinder.Eval(e.Row.DataItem, "Returner")).Equals(LabelUserName.Text))
                {
                    ((Button)e.Row.FindControl("btnRequest")).Enabled = false;
                    ((Button)e.Row.FindControl("btnRequest")).Text = "Returned";
                    ((CheckBox)e.Row.FindControl("chbRequest")).Enabled = false;
                }
            }
        }
    }


    //Is triggered when a databound occurs, detects if it is a gridview and sends it for formatting
    //Author: Krisha
    protected void Scheduled_DataBound(object sender, EventArgs e)
    {
        String whichControl = ((Control)sender).ID;

        switch (whichControl)
        {
            case "gridAvailOpenIce":
                format(gridAvailOpenIce);
                break;
            case "gridOpenIce":
                format(gridOpenIce);
                break;
            case "gridScheduledGameIce":
                format(gridScheduledGameIce);
                break;
            case "gridScheduledPracticeIce":
                format(gridScheduledPracticeIce);
                break;
            case "gridAvailableGameIce":
                format(gridAvailableGameIce);
                break;
            case "gridAvailablePracticeIce":
                format(gridAvailablePracticeIce);
                break;
        }
    }


    //Method to format the gridviews date columns to proper formats
    //Author: Krisha
    private void format(GridView gridview)
    {

        foreach (GridViewRow row in gridview.Rows)
        {
            //--------stores the value that needed to format------
            String startTime = row.Cells[2].Text.Trim();
            String endTime = row.Cells[3].Text.Trim();
            String date = row.Cells[1].Text.Trim();


            //------change those values to the right format--------
            row.Cells[2].Text = DateTime.Parse(startTime).ToString("HH:mm");
            row.Cells[3].Text = DateTime.Parse(endTime).ToString("HH:mm");
            row.Cells[1].Text = DateTime.Parse(date).ToString("ddd, d MMM");
        }
    }

    //check for site shutdown on available open ice grid view
    //Author: Adrian
    protected void gridAvailOpenIce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Check for site shutdown
            if ((((AllocationSettings)Application["Settings"]).getAppOpen()).ToString().Equals("false"))
            {
                ((Button)e.Row.FindControl("btnClaimOpen")).Enabled = false;
            }
        }
    }


    //Method to clear user notification labels
    private void clearLabels()
    {

        lblOpenStatus.Text = "";

        lblRequestPracticeMessage.Text = "";

        lblRequestGameMessage.Text = "";
        
        lblReturnPracticeMessage.Text = "";
        lblPracticeBalanceReturn.Text = "";

        lblReturnGameMessage.Text = "";
        lblGameBalanceReturn.Text = "";
    }
}