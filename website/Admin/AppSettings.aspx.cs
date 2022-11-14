using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AppSettings : System.Web.UI.Page
{
    AllocationSettings settings;
    System.Timers.Timer turnOn;
    protected void Page_Load(object sender, EventArgs e)
    {
        settings = ((AllocationSettings)Application["Settings"]);

        settings.getSettings();
        if (!IsPostBack)
        {
            shutdownToggleList.SelectedValue = settings.getAppOpen();
            reviewingToggleList.SelectedValue = settings.getReviewingOn();
            Announcements.Text = settings.getAnnouncement();
            txtAssignEmail.Text = settings.getAssignEmailTemplate();
            txtRejectEmail.Text = settings.getRejectEmailTemplate();
            txtTitle.Text = settings.getSiteTitle();
            txtLogoUrl.Text = settings.getLogoUrl();

            ddlTheme.SelectedValue = settings.getTheme();
            txtPublicEmail.Text = settings.getAdminEmail()[0];
            rblNotifications.SelectedValue = settings.getReceiveNotifications();
            rblTeamScheduling.SelectedValue = settings.getTeamSchedulingOn();

            //populates dropdownlist for start and end time
            for (int hr = 0; hr < 24; hr++)
            {
                for (int min = 0; min < 60; min += 15)
                {
                    string hrTime = hr.ToString("00");
                    string minTime = min.ToString("00");
                    string time = hrTime + ":" + minTime;
                    ddlWeekDayStart.Items.Add(new ListItem(time, time));
                    ddlWeekDayFinish.Items.Add(new ListItem(time, time));
                    ddlWeekEndStart.Items.Add(new ListItem(time, time));
                    ddlWeekEndFinish.Items.Add(new ListItem(time, time));
                }
            }
            //Set the Game Window drop downlist based on stored values
            ddlWeekDayStart.SelectedValue = settings.getGwWeekDayStart().Substring(0, 5);
            ddlWeekDayFinish.SelectedValue = settings.getGwWeekDayFinish().Substring(0, 5);
            ddlWeekEndStart.SelectedValue = settings.getGwWeekEndStart().Substring(0, 5);
            ddlWeekEndFinish.SelectedValue = settings.getGwWeekEndFinish().Substring(0, 5);

            //set rollover drop downs to current values.
            ddlRollToOpen.SelectedValue = (settings.getRollPracticeOver() / 60).ToString();
            ddlRollToPractice.SelectedValue = (settings.getRolloverTime() / 60).ToString();

            
            


        }

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        settings.setAppOpen(shutdownToggleList.SelectedValue.ToString());
        settings.setReviewingOn(reviewingToggleList.SelectedValue.ToString());
        settings.setAnnouncement(Announcements.Text);
        settings.setAssignEmailTemplate(txtAssignEmail.Text);
        settings.setRejectEmailTemplate(txtRejectEmail.Text);
        settings.setRolloverTime(int.Parse(ddlRollToPractice.SelectedValue));
        settings.setRollPracticeOver(int.Parse(ddlRollToOpen.SelectedValue));
        settings.setGwWeekDayStart(ddlWeekDayStart.SelectedValue);
        settings.setGWWeekDayFinish(ddlWeekDayFinish.SelectedValue);
        settings.setGwWeekEndStart(ddlWeekEndStart.SelectedValue);
        settings.setGWWeekEndFinish(ddlWeekEndFinish.SelectedValue);
        settings.setLogoUrl(txtLogoUrl.Text);
        settings.setSiteTitle(txtTitle.Text);
        settings.setTheme(ddlTheme.SelectedValue);
        settings.setAdminEmail(txtPublicEmail.Text);
        settings.setReceiveNotifications(rblNotifications.SelectedValue);
        settings.setTeamSchedulingOn(rblTeamScheduling.SelectedValue);

        lblWarning.Text = settings.saveSettings();

        if (!ddlAutoOn.SelectedValue.Equals("0"))
        {
            turnOn = new System.Timers.Timer();
            turnOn.Interval = int.Parse(ddlAutoOn.SelectedValue) * 60 * 60 * 1000;
            turnOn.Elapsed += switchOn;
            turnOn.Start();
        }

        if (reviewingToggleList.SelectedValue.ToString().Equals("false"))
        {
            changeToApprove();
        }

    }

    //stay
    private void changeToApprove()
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);


        SqlParameter sqlIsApproved = new SqlParameter("@IsApproved", SqlDbType.NVarChar);
        sqlIsApproved.Value = "true";
        SqlParameter sqlNotApproved = new SqlParameter("@NotApproved", SqlDbType.NVarChar);
        sqlNotApproved.Value = "false";

        SqlCommand approveAllReturns = new SqlCommand("UPDATE ReturnedIce SET IsApproved=@IsApproved WHERE IsApproved=@NotApproved;", con);
        approveAllReturns.Parameters.Add(sqlIsApproved);
        approveAllReturns.Parameters.Add(sqlNotApproved);

        try
        {
            con.Open();
            approveAllReturns.ExecuteNonQuery();
            con.Close();
        }
        catch (SqlException e)
        {
            lblWarning.Text = e.Message;
        }

    }

    

    private void switchOn(Object source, ElapsedEventArgs e)
    {
        settings.setAppOpen("true");
        settings.saveSettings();
        turnOn.Close();
    }


    //stay
    protected void btn_clearAnnouncement(object sender, EventArgs e)
    {
        string status = "";
        settings.setAnnouncement("");
        status = settings.saveSettings();
        Announcements.Text = "";

    }




    //stay
    protected void urlValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = CheckUrlExists(args.Value);
    }

    //stay
    protected bool CheckUrlExists(string url)
    {
        // If the url does not contain Http. Add it.
        if (!url.Contains("http://"))
        {
            url = "http://" + url;
        }
        //check if its actually referring to an image
        if (!url.Contains(".jpg") && !url.Contains(".png") && !url.Contains(".bmp"))
        {
            return false;
        }

        try
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "HEAD";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                return response.StatusCode == HttpStatusCode.OK;
            }
        }
        catch
        {
            return false;
        }
    }


}