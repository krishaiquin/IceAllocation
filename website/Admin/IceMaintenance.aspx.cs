/*
 * Author: Krisha
 * 
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_IceMaintenance : System.Web.UI.Page
{
    SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            int iceId = 0;
            //if (Session["IceIdDetails"] == null)
            //{
            //    Response.Redirect("~/Admin/EditIce.aspx");
            //}
            //else
            //{
            iceId = (int)(Session["iceId"]);
            //}


            sqlConnection.Open();
            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
            sqlIceId.Value = iceId;

            SqlCommand select = new SqlCommand("SELECT IceTimes.Date AS 'Date', IceTimes.StartTime AS 'StartTime', IceTimes.EndTime AS 'EndTime', IceTimes.TimeLength AS 'Duration', IceTimes.IceType AS 'IceType', Teams.UserName AS 'Team', Locations.FullName AS 'Location' FROM IceTimes INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId LEFT OUTER JOIN Teams ON IceTimes.TeamId = Teams.TeamId WHERE IceId=@IceId ", sqlConnection);
            select.CommandType = CommandType.Text;

            select.Parameters.Add(sqlIceId);

            SqlDataReader reader = select.ExecuteReader();
            reader.Read();
            lblLoc.Text = reader["Location"].ToString();
            calSched.SelectedDate = DateTime.Parse(reader["Date"].ToString());
            calSched.VisibleDate = DateTime.Parse(reader["Date"].ToString());
            DateTime start = DateTime.Parse(reader["StartTime"].ToString());
            DateTime end = DateTime.Parse(reader["EndTime"].ToString());
            lblDur.Text = reader["Duration"].ToString() + " minutes";
            ddlStart.SelectedValue = start.ToString("HH:mm");
            ddlEnd.SelectedValue = end.ToString("HH:mm");
            ddlType.SelectedValue = reader["IceType"].ToString();
            if (reader["Team"].ToString().Equals(""))
            {
                chkUnassign.Checked = true;
                ddlTeam.Enabled = false;
            }

            else
                ddlTeam.SelectedValue = reader["Team"].ToString();

            sqlConnection.Close();
        }


    }

    //Event that happens when the start time is changed
    //Verifies valid End time
    protected void ddlStart_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TODO: stuff - check that the end time is within a certain range or change it to the closest time
        string temp = ddlEnd.SelectedIndex.ToString();
        int maxslots = Constant.MAXICETIME / 15;
        int minslots = Constant.MINICETIME / 15;
        //Pseudo
        //Get Start time
        //get end time
        // caluclate difference
        //if difference > 120 change end time to 120
        //      since items identical current index + 8
        //if difference negative or >60 change end time to 60
        //      since items identical current index + 4
        DateTime start = DateTime.Parse(ddlStart.SelectedValue);
        DateTime end = DateTime.Parse(ddlEnd.SelectedValue);
        int diff = (int)end.Subtract(start).TotalMinutes;

        //check for the negative duration which occurs when the end time wraps around into the next day and add one days worth of minutes
        if (diff < 0)
        {
            end = end.AddDays(1);
            diff = (int)end.Subtract(start).TotalMinutes;
        }

        if (diff > Constant.MAXICETIME)
        {
            ddlEnd.ClearSelection();
            if (ddlStart.SelectedIndex + maxslots < ddlEnd.Items.Count)
                ddlEnd.SelectedIndex = ddlStart.SelectedIndex + maxslots;
            else
                //count starts from one while index starts at 0
                ddlEnd.SelectedIndex = ddlStart.SelectedIndex + maxslots - ddlEnd.Items.Count;

        }
        else if (diff < Constant.MINICETIME)
        {
            ddlEnd.ClearSelection();
            if (ddlStart.SelectedIndex + minslots < ddlEnd.Items.Count)
                ddlEnd.SelectedIndex = ddlStart.SelectedIndex + minslots;
            else
                //count starts from one while index starts at 0
                ddlEnd.SelectedIndex = ddlStart.SelectedIndex + minslots - ddlEnd.Items.Count;

        }


        //Refresh the diff
        end = DateTime.Parse(ddlEnd.SelectedValue);
        diff = (int)end.Subtract(start).TotalMinutes;
        //check for the negative duration which occurs when the end time wraps around into the next day and add one days worth of minutes
        if (diff < 0)
        {
            end = end.AddDays(1);
            diff = (int)end.Subtract(start).TotalMinutes;
        }
        lblDur.Text = diff + " minutes";

    }

    //Event that happens when the End time is changed
    //Verifies valid start time for a selected end time.
    protected void ddlEndTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TODO: stuff - check that the end time is within a certain range or change it to the closest time
        int maxslots = Constant.MAXICETIME / 15;
        int minslots = Constant.MINICETIME / 15;
        DateTime start = DateTime.Parse(ddlStart.SelectedValue);
        DateTime end = DateTime.Parse(ddlEnd.SelectedValue);
        int diff = (int)end.Subtract(start).TotalMinutes;
        //check for the negative duration which occurs when the end time wraps around into the next day and add one days worth of minutes

        if (diff < 0)
        {
            end = end.AddDays(1);
            diff = (int)end.Subtract(start).TotalMinutes;
        }
        if (diff > Constant.MAXICETIME)
        {
            ddlStart.ClearSelection();
            if (ddlEnd.SelectedIndex - maxslots >= 0)
                ddlStart.SelectedIndex = ddlEnd.SelectedIndex - maxslots;
            else
                ddlStart.SelectedIndex = ddlEnd.SelectedIndex - maxslots + ddlStart.Items.Count;

            //lblDuration.Text = diff + " minutes, Ice Time is too long.";
        }
        else if (diff < Constant.MINICETIME)
        {
            ddlStart.ClearSelection();
            if (ddlEnd.SelectedIndex - minslots >= 0)
                ddlStart.SelectedIndex = ddlEnd.SelectedIndex - minslots;
            else
                ddlStart.SelectedIndex = ddlEnd.SelectedIndex - minslots + ddlStart.Items.Count;

            //lblDuration.Text = diff + " minutes, Ice Time is too short.";
        }
        start = DateTime.Parse(ddlStart.SelectedValue);

        //check for the negative duration which occurs when the end time wraps around into the next day and add one days worth of minutes  
        diff = (int)end.Subtract(start).TotalMinutes;
        if (diff < 0)
        {
            end = end.AddDays(1);
            diff = (int)end.Subtract(start).TotalMinutes;
        }
        lblDur.Text = diff + " minutes";

    }

    protected void chkUnassign_CheckedChanged(object sender, EventArgs e)
    {
        if (chkUnassign.Checked)
            ddlTeam.Enabled = false;
        else
            ddlTeam.Enabled = true;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iceId = (int)(Session["iceId"]);
        IceTime iceTime = new IceTime(iceId);

        // check if the selected date is bigger than the date now AND if has any conflict
        // if the selected time has not passed yet and no conflict then save it into DB
        if (calSched.SelectedDate > DateTime.Now && !iceTime.hasConflict())
        {
            DateTime date = calSched.SelectedDate;

            DateTime tempStart = DateTime.Parse(ddlStart.SelectedValue.ToString());
            DateTime tempEnd = DateTime.Parse(ddlEnd.SelectedValue.ToString());

            int dur = (int)(tempEnd.Subtract(tempStart)).TotalMinutes;

            if (dur <= 0)
            {
                tempEnd = tempEnd.AddDays(1);
                dur = (int)(tempEnd.Subtract(tempStart)).TotalMinutes;
            }

            DateTime startTime = (date.AddHours(tempStart.Hour)).AddMinutes(tempStart.Minute);
            DateTime endTime = startTime.AddMinutes(dur);


            DateTime start = startTime;
            DateTime end = endTime;
            int duration = dur;
            string iceType = ddlType.SelectedValue.ToString();
            Boolean isUnassign = chkUnassign.Checked;
            int teamId = Team.findTeamId(ddlTeam.SelectedValue.ToString());

            lblStatus.Text = IceManipulation.updateIce(iceId, teamId, date, start, end, iceType, duration, isUnassign) + ": " + iceId;

        }
        else
        {
            lblStatus.Text = (iceTime.hasConflict()) ? "Your requesting icetime has conflicts.Changes cannot be saved." : "Date requested has already passed. Changes cannot be saved.";
        }
    }
}