﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*
 * Author: Adrian & Jonathan
 * 
 */
public partial class Team_CreateIce : System.Web.UI.Page
{

    IceTime[] icetime;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IceList"] != null)
        {
            icetime = (IceTime[])Session["IceList"];
        }

        if (!IsPostBack)
        {
            //populates dropdownlist for start and end time
            for (int hr = 0; hr < 24; hr++)
            {
                for (int min = 0; min < 60; min += 15)
                {
                    string hrTime = hr.ToString("00");
                    string minTime = min.ToString("00");
                    string time = hrTime + ":" + minTime;
                    ddlStartTime.Items.Add(new ListItem(time, time));
                    ddlEndTime.Items.Add(new ListItem(time, time));
                }
            }

            //sets default start and end times
            ddlStartTime.SelectedIndex = 28;
            ddlEndTime.SelectedIndex = 32;

            //populate number of ice times dropdownlist
            for (int i = 1; i < Constant.MAXICEENTRY + 1; i++)
            {
                ddlNumIce.Items.Add(new ListItem("" + i, "" + i));
            }
        }

        if ((((AllocationSettings)Application["Settings"]).getAppOpen()).ToString().Equals("false") ||
            (((AllocationSettings)Application["Settings"]).getTeamSchedulingOn()).ToString().Equals("false"))
        {
            Calendar1.Enabled = false;
            ddlLocation.Enabled = false;
            ddlStartTime.Enabled = false;
            ddlEndTime.Enabled = false;
            ddlIceType.Enabled = false;
            ddlNumIce.Enabled = false;
            btnGenerate.Enabled = false;
            btnReset.Enabled = false;
            lblResult.Text = "Site is currently disabled";
            pnlSchedulingOff.Visible = true;
            pnlSchedulingOn.Visible = false;
        }
        else
        {
            pnlSchedulingOff.Visible = false;
            pnlSchedulingOn.Visible = true;
        }
    }

    //generates list of ice times to be created
    //checks for conflicting ice times
    protected void btnGenerateList_Click(object sender, EventArgs e)
    {
        //form validators

        //date selected
        if (Calendar1.SelectedDate.Date == DateTime.MinValue)
        {
            lblCalendarMsg.Visible = true;
            lblCalendarMsg.Text = "A date must be selected";
        }
        //past date is selected
        else if (Calendar1.SelectedDate < DateTime.Now)
        {
            lblCalendarMsg.Visible = true;
            lblCalendarMsg.Text = "A future date must be selected";
        }
        else
        {
            lblCalendarMsg.Visible = false;
            lblCalendarMsg.Text = "";
        }
        //no location selected
        if (ddlLocation.SelectedValue == "-")
        {
            lblLocationMsg.Visible = true;
            lblLocationMsg.Text = "A location must be selected";
        }
        else
        {
            lblLocationMsg.Visible = false;
            lblLocationMsg.Text = "";
        }
        //no ice type selected
        if (ddlIceType.SelectedValue == "-")
        {
            lblTypeMsg.Visible = true;
            lblTypeMsg.Text = "An ice type must be selected";
        }
        else
        {
            lblTypeMsg.Visible = false;
            lblTypeMsg.Text = "";
        }

        //passes all validation
        if (Calendar1.SelectedDate > DateTime.Now &&
            Calendar1.SelectedDate != null &&
            ddlLocation.SelectedValue != "-" &&
            ddlIceType.SelectedValue != "-")
        {
            //clear session and checklist
            Session["IceList"] = null;
            pnlMultiIce.Visible = false;
            pnlConflicts.Visible = false;
            blConflicts.Items.Clear();
            cblMultiIce.Items.Clear();
            lblResult.Text = "";

            //grabs number of ice times selected from dropdownlist
            int numOfIce = Convert.ToInt32(ddlNumIce.SelectedItem.Value);
            icetime = new IceTime[numOfIce];

            //adds number of ice times as per ice times dropdownlist
            for (int i = 0; i < numOfIce; i++)
            {
                //Adding days based on number of times selected
                DateTime date = Calendar1.SelectedDate.AddDays(i * 7);

                DateTime tempStart = DateTime.Parse(ddlStartTime.SelectedValue.ToString());
                DateTime tempEnd = DateTime.Parse(ddlEndTime.SelectedValue.ToString());


                int dur = (int)(tempEnd.Subtract(tempStart)).TotalMinutes;
                //if the duration is negative the user entered data in 12h clock, add 12 hours worth of minutes
                if (dur <= 0)
                {
                    tempEnd = tempEnd.AddDays(1);
                    dur = (int)(tempEnd.Subtract(tempStart)).TotalMinutes;
                }

                DateTime startTime = (date.AddHours(tempStart.Hour)).AddMinutes(tempStart.Minute);
                DateTime endTime = startTime.AddMinutes(dur);

                int teamId = Team.findTeamId(User.Identity.Name);
                int location = int.Parse(ddlLocation.SelectedValue);
                string locStr = ddlLocation.SelectedItem.Text;
                string iceType = ddlIceType.Text;

                //create the icetime array
                icetime[i] = new IceTime(teamId, iceType, locStr, location, date, startTime, endTime, dur, false);

                //displays common values for first time
                if (i == 0)
                {
                    lblCommonDetails.Text = icetime[i].commonToString();
                    lblCommonDetails.Visible = true;
                }

                ListItem iceItem = new ListItem(icetime[i].datesToString(), "" + i);

                //ice time details to text
                if (icetime[i].hasConflict())
                {
                    //to enable panel to display conflicting ice times
                    pnlConflicts.Visible = true;
                    //adds to bulleted list
                    blConflicts.Items.Add(iceItem);
                }
                else
                {
                    //to enable panel to display usable ice times
                    pnlMultiIce.Visible = true;
                    //default checkbox setting is checked
                    iceItem.Selected = true;
                    //adds to checkbox list
                    cblMultiIce.Items.Add(iceItem);
                }

            }

            Session["IceList"] = icetime;

            //enable submit
            btnGenerate.Text = "Regenerate List";
            btnSubmit.Visible = true;
        }

    }


    //Adds selected values from generated list to database
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int count = 0;
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();
            foreach (ListItem item in cblMultiIce.Items)
            {
                if (item.Selected)
                {
                    icetime[int.Parse(item.Value.ToString())].save(con);
                    count++;
                }
            }
            con.Close();

            // count ice times created.
            lblResult.Text = "Number of ice times created: " + count;

        }
        catch (SqlException ex) {
            lblResult.Text = "There was an error connecting to the database.";
        }

        formReset();

    }

    //Event that happens when the start time is changed
    //Verifies valid End time
    //Author: Jonathan
    protected void ddlStartTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TODO: stuff - check that the end time is within a certain range or change it to the closest time
        string temp = ddlEndTime.SelectedIndex.ToString();
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
        DateTime start = DateTime.Parse(ddlStartTime.SelectedValue);
        DateTime end = DateTime.Parse(ddlEndTime.SelectedValue);
        int diff = (int)end.Subtract(start).TotalMinutes;

        //check for the negative duration which occurs when the end time wraps around into the next day and add one days worth of minutes
        if (diff < 0)
        {
            end = end.AddDays(1);
            diff = (int)end.Subtract(start).TotalMinutes;
        }

        if (diff > 120)
        {
            ddlEndTime.ClearSelection();
            if (ddlStartTime.SelectedIndex + maxslots < ddlEndTime.Items.Count)
                ddlEndTime.SelectedIndex = ddlStartTime.SelectedIndex + maxslots;
            else
                //count starts from one while index starts at 0
                ddlEndTime.SelectedIndex = ddlStartTime.SelectedIndex + maxslots - ddlEndTime.Items.Count;

        }
        else if (diff < 60)
        {
            ddlEndTime.ClearSelection();
            if (ddlStartTime.SelectedIndex + minslots < ddlEndTime.Items.Count)
                ddlEndTime.SelectedIndex = ddlStartTime.SelectedIndex + minslots;
            else
                //count starts from one while index starts at 0
                ddlEndTime.SelectedIndex = ddlStartTime.SelectedIndex + minslots - ddlEndTime.Items.Count;

        }


        //Refresh the diff
        end = DateTime.Parse(ddlEndTime.SelectedValue);
        diff = (int)end.Subtract(start).TotalMinutes;
        //check for the negative duration which occurs when the end time wraps around into the next day and add one days worth of minutes
        if (diff < 0)
        {
            end = end.AddDays(1);
            diff = (int)end.Subtract(start).TotalMinutes;
        }
        lblDuration.Text = diff + " minutes";

    }

    //Event that happens when the End time is changed
    //Verifies valid start time
    //Author: Jonathan
    protected void ddlEndTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        //check that the end time is within a certain range or change it to the closest time
        int maxslots = Constant.MAXICETIME / 15;
        int minslots = Constant.MINICETIME / 15;
        DateTime start = DateTime.Parse(ddlStartTime.SelectedValue);
        DateTime end = DateTime.Parse(ddlEndTime.SelectedValue);
        int diff = (int)end.Subtract(start).TotalMinutes;


        //check for the negative duration which occurs when the end time wraps around into the next day and add one days worth of minutes
        if (diff < 0)
        {
            end = end.AddDays(1);
            diff = (int)end.Subtract(start).TotalMinutes;
        }
        if (diff > 120)
        {
            ddlStartTime.ClearSelection();
            if (ddlEndTime.SelectedIndex - maxslots >= 0)
                ddlStartTime.SelectedIndex = ddlEndTime.SelectedIndex - maxslots;
            else
                ddlStartTime.SelectedIndex = ddlEndTime.SelectedIndex - maxslots + ddlStartTime.Items.Count;

            //lblDuration.Text = diff + " minutes, Ice Time is too long.";
        }
        else if (diff < 60)
        {
            ddlStartTime.ClearSelection();
            if (ddlEndTime.SelectedIndex - minslots >= 0)
                ddlStartTime.SelectedIndex = ddlEndTime.SelectedIndex - minslots;
            else
                ddlStartTime.SelectedIndex = ddlEndTime.SelectedIndex - minslots + ddlStartTime.Items.Count;

            //lblDuration.Text = diff + " minutes, Ice Time is too short.";
        }
        start = DateTime.Parse(ddlStartTime.SelectedValue);

        //check for the negative duration which occurs when the end time wraps around into the next day and add one days worth of minutes  
        //Refresh the diff
        end = DateTime.Parse(ddlEndTime.SelectedValue);
        diff = (int)end.Subtract(start).TotalMinutes;
       if (diff < 0)
        {
            end = end.AddDays(1);
            diff = (int)end.Subtract(start).TotalMinutes;
        }
        lblDuration.Text = diff + " minutes";


    }


   
    protected void btnReset_Click(object sender, EventArgs e)
    {
        formReset();
    }

    //resets the form
    protected void formReset()
    {
        //form back to default values
        Calendar1.SelectedDates.Clear();
        ddlLocation.ClearSelection();
        ddlStartTime.SelectedIndex = 28;
        ddlEndTime.SelectedIndex = 32;
        lblDuration.Text = "60 minutes";
        ddlIceType.ClearSelection();
        ddlNumIce.ClearSelection();
        cblMultiIce.Items.Clear();
        blConflicts.Items.Clear();
        btnGenerate.Text = "Generate List";

        //clear warnings
        lblCalendarMsg.Visible = false;
        lblCalendarMsg.Text = "";
        lblLocationMsg.Visible = false;
        lblLocationMsg.Text = "";
        lblTypeMsg.Visible = false;
        lblTypeMsg.Text = "";

        //hide elements
        lblCommonDetails.Text = "";
        lblCommonDetails.Visible = false;
        pnlMultiIce.Visible = false;
        pnlConflicts.Visible = false;
        btnSubmit.Visible = false;

        //Remove variable for next insertion
        Session["IceList"] = null;
    }
}