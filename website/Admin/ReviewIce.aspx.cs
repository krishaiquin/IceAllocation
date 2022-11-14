using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*
 * Author: Adrian
 * 
 */
public partial class Admin_ReviewIce : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            reviewingToggleList.SelectedValue = ((AllocationSettings)Application["Settings"]).getReviewingOn();
        }

    }


    //set reviwing on and off
    protected void btn_setReviewing(object sender, EventArgs e)
    {
        if (reviewingToggleList.SelectedValue.ToString().Equals("true"))
        {
            //change reviewing to off
            ((AllocationSettings)Application["Settings"]).setReviewingOn("true");

        }
        else
        {
            //change all to approved
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            SqlParameter sqlIsApproved = new SqlParameter("@IsApproved", SqlDbType.NVarChar);
            sqlIsApproved.Value = "true";
            SqlParameter sqlNotApproved = new SqlParameter("@NotApproved", SqlDbType.NVarChar);
            sqlNotApproved.Value = "false";

            SqlCommand approveAllReturns = new SqlCommand("UPDATE ReturnedIce SET IsApproved=@IsApproved WHERE IsApproved=@NotApproved;", con);
            approveAllReturns.Parameters.Add(sqlIsApproved);
            approveAllReturns.Parameters.Add(sqlNotApproved);
            approveAllReturns.ExecuteNonQuery();

            con.Close();

            //change reviewing to off
            ((AllocationSettings)Application["Settings"]).setReviewingOn("false");

            //refesh gridview
            gridReturnedIce.DataBind();

        }

        ((AllocationSettings)Application["Settings"]).saveSettings();

        if ((((AllocationSettings)Application["Settings"]).getReviewingOn()).ToString().Equals("true"))
        {
            lblReviewingStatus.Text = "Reviewing ENABLED";
        }
        else
        {
            lblReviewingStatus.Text = "Reviewing DISABLED";
        }
    }




    protected void btnApprove_Click(object sender, EventArgs e)
    {
        IceEmail notifyTeams = new IceEmail();
        notifyTeams.addSubject(Constant.NOTIFY_OTHERS_SUBJECT);
        string message = Constant.NOTIFY_OTHERS_MESSAGE+ Environment.NewLine + Environment.NewLine;
        IceTime approved;

        bool isGame = false;
        bool isPractice = false;
        bool isOpen = false;

        try
        {
            //for checking if ice times are selected
            bool isSelected = false;

            //open connection
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            //loop through each row
            foreach (GridViewRow row in gridReturnedIce.Rows)
            {
                if (((CheckBox)row.FindControl("chbSelect")).Checked)
                {
                    if (!isSelected)
                        isSelected = true;

                    //grab values from control on gridview
                    int iceId = int.Parse(((Label)row.FindControl("lblIceId")).Text);

                    //creates parameters for sql
                    SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
                    sqlIceId.Value = iceId;
                    SqlParameter sqlIsApproved = new SqlParameter("@IsApproved", SqlDbType.NVarChar);
                    sqlIsApproved.Value = "true";

                    SqlCommand approveReturn = new SqlCommand("UPDATE ReturnedIce SET IsApproved=@IsApproved WHERE IceId=@IceId;", con);

                    //injects parameters into sql statement and executes
                    approveReturn.Parameters.Add(sqlIceId);
                    approveReturn.Parameters.Add(sqlIsApproved);
                    approveReturn.ExecuteNonQuery();

                    //Create an ice time to access the to String function for the email.
                    approved = new IceTime(iceId);

                    //Check the ice types and raise appropriate flags
                    if (approved.getIceType().Equals("Game"))
                    {
                        isGame = true;
                    }
                    else if (approved.getIceType().Equals("Practice"))
                    {
                        isPractice = true;
                    }
                    else
                    {
                        isOpen = true;
                    }

                    message += approved.ToString() + Environment.NewLine;

                }
            }

            //close connection
            con.Close();

            if (isSelected)
            {
                //refresh grid view
                gridReturnedIce.DataBind();

                //post message
                lblResult.Text = "Approve successful";

                //Check the gameType and choose the appropriate contact info
                //Add all email addresses to the receivers.
                if (isOpen)
                {
                    notifyTeams.addBccRecipients(Team.getAllContactInfo());
                }
                else
                {
                    //If there is a game add all teams with negative game balance
                    if (isGame)
                    {
                        notifyTeams.addBccRecipients(Team.getAllNegativeGame());
                    }

                    //If there is a game add all teams with negative practice balance
                    if (isPractice)
                    {
                        notifyTeams.addBccRecipients(Team.getAllNegativePractice());
                    }
                }

                //If no one has been added (occurs when everything is balanced), then inform everyone.
                if (notifyTeams.getBccCount() == 0)
                {
                    notifyTeams.addBccRecipients(Team.getAllContactInfo());
                }
                //Add Message to the email and send it.
                notifyTeams.setMessage(message);
                lblResult.Text += " Email status:" + notifyTeams.send();

            }
            else
            {
                lblResult.Text = "Please select ice times";
            }
        }
        catch (SqlException ex)
        {
            lblResult.Text = ex.Message;
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (ddlTeam.SelectedValue == "-")
        {
            lblSelectTeamVal.Visible = true;
        }
        else
        {
            //hide validation label
            lblSelectTeamVal.Visible = false;

            try
            {
                //for checking if ice times are selected
                bool isSelected = false;

                //open connection
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
                con.Open();

                //loop through each row
                foreach (GridViewRow row in gridReturnedIce.Rows)
                {
                    if (((CheckBox)row.FindControl("chbSelect")).Checked)
                    {
                        if (!isSelected)
                            isSelected = true;

                        //grab value from team dropdown list, control on gridview, comment box
                        int TeamId = int.Parse(ddlTeam.SelectedValue);
                        int iceId = int.Parse(((Label)row.FindControl("lblIceId")).Text);
                        string comments = txbAssignComment.Text;

                        //creates parameters for sql
                        SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
                        sqlTeamId.Value = TeamId;
                        SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
                        sqlIceId.Value = iceId;
                        SqlParameter sqlComments = new SqlParameter("@Comments", SqlDbType.NVarChar);
                        sqlComments.Value = comments;
                        SqlParameter sqlIsApproved = new SqlParameter("@IsApproved", SqlDbType.NVarChar);
                        sqlIsApproved.Value = "true";
                        SqlParameter sqlStatus = new SqlParameter("@Status", SqlDbType.NVarChar);
                        sqlStatus.Value = "Closed";


                        SqlCommand assignReturn = new SqlCommand(
                            "UPDATE IceTimes SET TeamId=@TeamId WHERE IceId=@IceId;" +
                            "UPDATE ReturnedIce SET AssignedId=@TeamId, AssignedDate=GETDATE(), Comments=@Comments, IsApproved=@IsApproved, Status=@Status WHERE IceId=@IceId;" +
                            "UPDATE Requests SET Status = 'Accepted' WHERE IceId=@IceId;", con);

                        //injects parameters into sql statement and executes
                        assignReturn.Parameters.Add(sqlTeamId);
                        assignReturn.Parameters.Add(sqlIceId);
                        assignReturn.Parameters.Add(sqlComments);
                        assignReturn.ExecuteNonQuery();
                    }
                }

                //close connection
                con.Close();

                if (isSelected)
                {
                    //refresh grid view
                    gridReturnedIce.DataBind();

                    //clear form and post message
                    ddlTeam.ClearSelection();
                    txbAssignComment.Text = "";
                    lblResult.Text = "Assign successful";
                }
                else
                {
                    lblResult.Text = "Please select ice times";
                }
            }
            catch (SqlException ex)
            {
                lblResult.Text = ex.Message;
            }
        }
    }

    protected void btnDrop_Click(object sender, EventArgs e)
    {
        try
        {
            //for checking if ice times are selected
            bool isSelected = false;

            //open connection
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            //loop through each row
            foreach (GridViewRow row in gridReturnedIce.Rows)
            {
                if (((CheckBox)row.FindControl("chbSelect")).Checked)
                {
                    if (!isSelected)
                        isSelected = true;

                    //grab values from control on gridview
                    int iceId = int.Parse(((Label)row.FindControl("lblIceId")).Text);

                    //creates parameters for sql
                    SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
                    sqlIceId.Value = iceId;
                    SqlParameter sqlStatus = new SqlParameter("@Status", SqlDbType.NVarChar);
                    sqlStatus.Value = "Closed";

                    SqlCommand approveReturn = new SqlCommand("UPDATE ReturnedIce SET Status=@Status WHERE IceId=@IceId;", con);

                    //injects parameters into sql statement and executes
                    approveReturn.Parameters.Add(sqlIceId);
                    approveReturn.Parameters.Add(sqlStatus);
                    approveReturn.ExecuteNonQuery();
                }
            }

            //close connection
            con.Close();

            if (isSelected)
            {
                //refresh grid view
                gridReturnedIce.DataBind();

                //post message
                lblResult.Text = "Drop successful";
            }
            else
            {
                lblResult.Text = "Please select ice times";
            }
        }
        catch (SqlException ex)
        {
            lblResult.Text = ex.Message;
        }
    }

    //toggle between approve, assign and drop
    protected void rblChoose_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblResult.Text = "";

        if (rblChoose.SelectedValue == "Approve Return")
        {
            pnlApprove.Visible = true;
            pnlAssign.Visible = false;
            pnlDrop.Visible = false;
        }
        else if (rblChoose.SelectedValue == "Assign To Team")
        {
            pnlApprove.Visible = false;
            pnlAssign.Visible = true;
            pnlDrop.Visible = false;
        }
        else if (rblChoose.SelectedValue == "Drop Ice Time")
        {
            pnlApprove.Visible = false;
            pnlAssign.Visible = false;
            pnlDrop.Visible = true;
        }
    }

    //checkbox on row header for select/deselect all
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gridReturnedIce.HeaderRow.FindControl("chkSelectAll");

        if (ChkBoxHeader.Checked == true)
        {
            foreach (GridViewRow row in gridReturnedIce.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbSelect");
                ChkBoxRows.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow row in gridReturnedIce.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbSelect");
                ChkBoxRows.Checked = false;
            }
        }


    }

}