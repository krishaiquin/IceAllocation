/*
 * Author: Krisha
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Admin_ReqDetail : System.Web.UI.Page
{

    IceTime ice;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["IceIdDetails"] == null)
        {
            Response.Redirect("~/Requests");
        }
        else
        {
            ice = new IceTime((int)(Session["IceIdDetails"]));
            getReturnedInfo();
        }
    }

    //Gets info about this ice time and displays it appropriately
    private void getReturnedInfo()
    {
        try
        {
            //ice = new IceTime((int)(Session["IceIdDetails"]));
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            //open the connection

            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
            //select statement and add the parameter
            sqlIceId.Value = (int)(Session["IceIdDetails"]);
            SqlCommand select = new SqlCommand("SELECT ReturnedIce.IceId," +
                "(Select FullName FROM Locations WHERE IceTimes.LocationId = Locations.LocationId) AS 'Location'," +
                "Date AS 'Date'," +
                "StartTime AS 'StartTime'," +
                "EndTime AS 'EndTime'," +
                "TimeLength," +
                "(Select UserName FROM Teams WHERE Teams.TeamId = ReturnedIce.ReturnerId) AS 'Returner'," +
                "ReturnedDate as 'Date (R)'," +
                "IceType " +
                "FROM ReturnedIce " +
                "INNER JOIN IceTimes ON IceTimes.IceId = ReturnedIce.IceId " +
                "WHERE  ReturnedIce.IceId=@iceId " +
                "ORDER BY Date ASC, StartTime ASC", sqlConnection);
            select.CommandType = CommandType.Text;
            select.Parameters.Add(sqlIceId);

            //read the sql statement
            sqlConnection.Open();
            SqlDataReader row = select.ExecuteReader();
            row.Read();

            //get the values
            lblLoc.Text = row["Location"].ToString();
            string date, startTime, endTime;
            date = DateTime.Parse(row["Date"].ToString()).ToString("ddd, d MMM");
            startTime = DateTime.Parse(row["StartTime"].ToString()).ToString("HH:mm");
            endTime = DateTime.Parse(row["EndTime"].ToString()).ToString("HH:mm");
            lblDate.Text = date + " from " + startTime + " to " + endTime;
            lblDur.Text = row["TimeLength"].ToString() + " minutes";
            lblType.Text = row["IceType"].ToString();
            lblDateRet.Text = DateTime.Parse(row["Date (R)"].ToString()).ToString("ddd, d MMM");
            lblRetTeam.Text = row["Returner"].ToString();

            sqlConnection.Close();
        }
        catch (SqlException ex)
        {
            lblResult.Text = "The date for this ice time could not be loaded.";
        }
    }



    //button handler that occurs when an assign button in the gridview is clicked
    protected void AssignIce_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        if (e.CommandName == "AssignIce")
        {
            //Get index of the team to assign ice to
            int assignIndex = int.Parse(e.CommandArgument.ToString());

            //get the team id of that team
            int teamId = int.Parse(GridView1.DataKeys[assignIndex].Value.ToString());



            string commentAll = txtCommentAll.Text;

            //Cycle through gridview and make an action for each team.
            for (int req = 0; req < GridView1.DataKeys.Count; req++)
            {
                string teamComment = (GridView1.Rows[req].FindControl("txtComment") as TextBox).Text;

                //check if this team is the assigned team
                if (req == assignIndex)
                    result += updateAssigned(chkToggle.Checked, commentAll, teamComment, teamId, ice);
                else
                {
                    int rejectedTeam = int.Parse((GridView1.DataKeys[req]).Value.ToString());
                    result += updateRejected(chkToggle.Checked, commentAll, teamComment, rejectedTeam, ice);
                }
            }

        }
        lblResult.Visible = true;
        lblResult.Text += result;
        GridView1.DataBind();
        btnAssignToNonRequest.Enabled = false;
    }


    //Update assigned, handles everything for assigning a request. updates the request table, and sends a notification
    private string updateAssigned(bool includeComments,string commentAll, string teamComment, int teamId, IceTime ice)
    {
        string result = "";
        Team assignedTo = new Team(teamId);
        //if either/both team comment and commentAll is/are null then print out No Comment.
        commentAll = (commentAll.Equals("")) ? "No Comment" : commentAll;
        teamComment = (teamComment.Equals("")) ? "No Comment" : teamComment;


        string success = IceManipulation.assignIce(ice.getIceId(), teamId, teamComment, commentAll);

        if (success.Equals("true"))
        {
            //Inform the user of success.
            result = "The ice was successfully Assigned to: " + assignedTo.getTeamName() + Environment.NewLine;

            //Increase the appropriate game balance
            if (ice.getIceType().Equals("Game"))
            {
                assignedTo.increaseGame();
            }
            else if (ice.getIceType().Equals("Practice"))
            {
                assignedTo.increasePractice();
            } //do not increase if game type is open


            //check if admin wants to send notification to teams.
            //if true the sends email.
            if (includeComments)
            {
                string message = ((AllocationSettings)Application["Settings"]).getAssignEmailTemplate() + " " + ice.ToString() + Environment.NewLine.ToString() +
                Environment.NewLine.ToString() + "Team Comment:" + Environment.NewLine.ToString() + teamComment + Environment.NewLine.ToString()
                + Environment.NewLine.ToString() + "Comment for all teams:" + Environment.NewLine.ToString() + commentAll;


                IceEmail mail = new IceEmail(assignedTo);
                mail.setMessage(message);
                mail.addSubject(Constant.NOTIFY_ASSIGN_SUBJECT);
                mail.send();
            }

        }
        else
        {
            result = "Ice was not successfully assigned: " + success + "\n";
        }
        return result;
    }



    //Update rejected, handles everything for rejecting a request. updates the request table, and sends a notification
    private string updateRejected(bool includeComments,string commentAll, string teamComment, int teamId, IceTime ice)
    {
        string result = "";
        Team rejectedTeam = new Team(teamId);
        //if either/both team comment and commentAll is/are null then print out No Comment.
        commentAll = (commentAll.Equals("")) ? "No Comment" : commentAll;
        teamComment = (teamComment.Equals("")) ? "No Comment" : teamComment;

        string success = IceManipulation.rejectIce(ice.getIceId(), teamId, teamComment);

        if (success.Equals("true"))
        {
            //Inform the user of success.
            result = "Successfully Rejected Request by Team #" + teamId + " for Ice #" + ice.getIceId() + "\n";

            //check if admin wants to send notification to teams.
            //if true the sends email.

            string message = ((AllocationSettings)Application["Settings"]).getRejectEmailTemplate() + " " + ice.ToString() + Environment.NewLine.ToString();
            if (includeComments)
            {
                message += Environment.NewLine.ToString() + "Comment for all teams: " + Environment.NewLine.ToString() + commentAll;
                if (!teamComment.Equals(""))
                    message += Environment.NewLine.ToString() + "Team Comment: " + Environment.NewLine.ToString() + teamComment + Environment.NewLine.ToString();

            }
            if (rejectedTeam.getNotifications().Equals("true"))
            {
                IceEmail mail = new IceEmail(rejectedTeam);
                mail.setMessage(message);
                mail.addSubject(Constant.NOTIFY_REJECT_SUBJECT);
                mail.send();
            }

        }
        else
        {
            result = "Rejection of request by Team #" + teamId + " for Ice #" + ice.getIceId() + " was unsuccessful: " + success + "\n";
        }
        return result;
    }



    //button handler that Completely Drops/Removes the ice time from the schedule.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //value from script that determines if the user has clicked yes or no
        string confirmValue = Request.Form["confirm_value"];
        int iceId = (int)(Session["IceIdDetails"]);

        if (confirmValue == "Yes")
        {
            string success = IceManipulation.cancelRequest(iceId);

            if (success.Equals("true"))
            {
                lblResult.Text += "This ice time has been successfully cancelled.";
            }
            else
                lblResult.Text += "Cancellation failed. " + success;
        }
        lblResult.Visible = true;
        GridView1.DataBind();
        btnAssignToNonRequest.Enabled = false;
    }


    //Button handler that assigns the ice time to any team specified in the drop down list.
    protected void btnAssignToNonRequest_Click(object sender, EventArgs e)
    {
        string result = "";
        //Make the assignment
        int teamId = int.Parse(ddlNonRequestTeam.SelectedValue);
        Team assignTo = new Team(teamId);

        result += updateAssigned(chkToggle.Checked, txtCommentNonRequest.Text,"", teamId, ice);

                 


            //Cycle through gridview and reject all requests
            for (int req = 0; req < GridView1.DataKeys.Count; req++)
            {
                string teamComment = (GridView1.Rows[req].FindControl("txtComment") as TextBox).Text;

                int rejectedTeam = int.Parse((GridView1.DataKeys[req]).Value.ToString());
                //check if this team is the assigned team, if its not, then update the rejection
                if (rejectedTeam != teamId)
                {
                    updateRejected(false,"", "", rejectedTeam, ice);
                }
            }

            lblNonRequestResult.Text = result;
            GridView1.DataBind();
            btnAssignToNonRequest.Enabled = false;
        }
    
}