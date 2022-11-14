using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for AllocationSettings
/// 
/// The AllocationSettings class simply retrieves and saves settings that affect how the site looks and 
/// behaves. Some of these settings include whether the site is enabled for team interaction, announcements
/// that the teams will see, and roll over times set by the allocator.
/// 
/// Author: Jonathan
/// </summary>
public class AllocationSettings
{
    string announcement, //General announcement, all teams will see this.
        adminEmail, //Contact information for the allocator
        reviewingOn, // valid values 'true' or 'false', Determines whether returned ice is sent to the reviewing section
        appOpen, //valid values 'true' or 'false', if false then teams cannot return or request ice
        logoUrl, //Banner url
        theme, //not implemented
        assignEmailTemplate, //The basic message for that is sent when ice is assigned, Ice information is appended to it 
        rejectEmailTemplate, //The basic message for that is sent when ice is rejected, Ice information is appended to it
        siteTitle, //Site Title, not used anywhere
        receiveNotifications,
        teamSchedulingOn; //valid values 'true' or 'false'
    TimeSpan gwWeekDayStart, //Time of weekday that marks start of gamewindow(gw)
        gwWeekDayFinish, //Time of weekday that marks finish of gamewindow(gw)
        gwWeekEndStart,  //Time of weekend that marks start of gamewindow(gw)
        gwWeekEndFinish; //Time of weekday that marks finish of gamewindow(gw) 
    int rollGameOver, //Number of hours before a game that it should roll over to a practice time
        rollPracticeOver; //number of hours before a practice that it should roll over to an open ice time

    //Constructor, basic constructor for default values
    public AllocationSettings()
    {
        announcement = "";
        adminEmail = "";
        reviewingOn = "";
        appOpen = "false";
        logoUrl = "";
        theme = "";
        siteTitle = "";
        gwWeekDayStart = new TimeSpan();
        gwWeekDayFinish = new TimeSpan();
        gwWeekEndStart = new TimeSpan();
        gwWeekEndFinish = new TimeSpan();
        assignEmailTemplate = "";
        rejectEmailTemplate = "";
        rollGameOver = 0;
        rollPracticeOver = 0;
        receiveNotifications="true";
        teamSchedulingOn = "false";
    }



    
    //-----WORK METHODS------------------------

    //Public Method getSettings()
    //Retrive the settings from the database and stores them in the local variables
    public string getSettings()
    {
        string result = "";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

            SqlCommand settings = new SqlCommand("Select SettingId, Announcement, AdminEmail,ReviewingOn,AppOpen,LogoUrl,Colour,GameWindowWeekDayStart, GameWindowWeekDayFinish, GameWindowWeekEndStart, GameWindowWeekEndFinish,EmailAssignTemplate, EmailRejectTemplate, RollOverTime, RollPracticeOver, SiteTitle, ReceiveNotifications, TeamSchedulingOn  FROM Settings WHERE SettingId = 1", con);
            settings.CommandType = CommandType.Text;

            con.Open();
            SqlDataReader reader = settings.ExecuteReader();


            //Read the whole table, check which key we have and save it to the proper place
            //Allows for rows to be out of order
            //Ignores rows we dont expect
            reader.Read();

            if (!reader.IsDBNull(1))
                announcement = reader.GetString(1);
            if (!reader.IsDBNull(2))
                adminEmail = reader.GetString(2);
            if (!reader.IsDBNull(3))
                reviewingOn = reader.GetString(3);
            if (!reader.IsDBNull(4))
                appOpen = reader.GetString(4);
            if (!reader.IsDBNull(5))
                logoUrl = reader.GetString(5);

            if (!reader.IsDBNull(6))
                theme = reader.GetString(6);
            if (!reader.IsDBNull(7))
                gwWeekDayStart = reader.GetTimeSpan(7);
            if (!reader.IsDBNull(8))
                gwWeekDayFinish = reader.GetTimeSpan(8);
            if (!reader.IsDBNull(9))
                gwWeekEndStart = reader.GetTimeSpan(9);
            if (!reader.IsDBNull(10))
                gwWeekEndFinish = reader.GetTimeSpan(10);

            if (!reader.IsDBNull(11))
                assignEmailTemplate = reader.GetString(11);
            if (!reader.IsDBNull(12))
                rejectEmailTemplate = reader.GetString(12);
            if (!reader.IsDBNull(13))
                rollGameOver = reader.GetInt32(13);
            if (!reader.IsDBNull(14))
                rollPracticeOver = reader.GetInt32(14);
            if (!reader.IsDBNull(15))
                siteTitle = reader.GetString(15);

            if (!reader.IsDBNull(16))
                receiveNotifications = reader.GetString(16);
            if (!reader.IsDBNull(17))
                teamSchedulingOn = reader.GetString(17);
            con.Close();
            result = "success";
        }
        catch (SqlException ex)
        {
            result = "failed to connect to database.";
        }
        return result;
    }

    //Public method save settings.
    //Saves the current values back to he database.
    //Concern: is it possible for the team to make use of this method in some way to change the app settings? may need to make an administrator check
    public string saveSettings()
    {
        string success = "";

        //Create a connection
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

        //Create a command
        SqlCommand settings = new SqlCommand(
            "UPDATE Settings SET Announcement=@Announcement, "
            + "AdminEmail=@AdminEmail, "
            + "ReviewingOn=@ReviewingOn, "
            + "AppOpen=@AppOpen, "
            + "LogoUrl=@LogoUrl, "
            + "Colour=@BackgroundColor, "
            + "GameWindowWeekDayStart=@gwWeekDayStart, "
            + "GameWindowWeekDayFinish=@gwWeekDayFinish, "
            + "GameWindowWeekEndStart=@gwWeekEndStart, "
            + "GameWindowWeekEndFinish=@gwWeekEndFinish, "
            + "EmailAssignTemplate=@EmailAssignTemplate, "
            + "EmailRejectTemplate=@EmailRejectTemplate, "
            + "RollOverTime=@RolloverTime, "
            + "RollPracticeOver=@RollPracticeOver, "
            + "SiteTitle=@SiteTitle, "
            + "ReceiveNotifications = @Notifications, "
            + "TeamSchedulingOn = @TeamSchedulingOn "
            + "WHERE SettingId=1", con);
        settings.CommandType = CommandType.Text;


        //Create and add all the paramters
        SqlParameter sqlAdminEmail = new SqlParameter("AdminEmail", SqlDbType.NVarChar);
        sqlAdminEmail.Value = adminEmail;
        settings.Parameters.Add(sqlAdminEmail);

        SqlParameter sqlLogoUrl = new SqlParameter("LogoUrl", SqlDbType.NVarChar);
        sqlLogoUrl.Value = logoUrl;
        settings.Parameters.Add(sqlLogoUrl);

        SqlParameter sqlTheme = new SqlParameter("BackgroundColor", SqlDbType.NVarChar);
        sqlTheme.Value = theme;
        settings.Parameters.Add(sqlTheme);

        SqlParameter sqlAnnouncement = new SqlParameter("Announcement", SqlDbType.NVarChar);
        sqlAnnouncement.Value = announcement;
        settings.Parameters.Add(sqlAnnouncement);

        SqlParameter sqlAssignEmailTemplate = new SqlParameter("EmailAssignTemplate", SqlDbType.NVarChar);
        sqlAssignEmailTemplate.Value = assignEmailTemplate;
        settings.Parameters.Add(sqlAssignEmailTemplate);

        SqlParameter sqlRejectEmailTemplate = new SqlParameter("EmailRejectTemplate", SqlDbType.NVarChar);
        sqlRejectEmailTemplate.Value = rejectEmailTemplate;
        settings.Parameters.Add(sqlRejectEmailTemplate);

        //-------

        SqlParameter sqlGwWeekDayStart = new SqlParameter("gwWeekDayStart", SqlDbType.Time);
        sqlGwWeekDayStart.Value = gwWeekDayStart;
        settings.Parameters.Add(sqlGwWeekDayStart);

        SqlParameter sqlGwWeekDayFinish = new SqlParameter("gwWeekDayFinish", SqlDbType.Time);
        sqlGwWeekDayFinish.Value = gwWeekDayFinish;
        settings.Parameters.Add(sqlGwWeekDayFinish);

        SqlParameter sqlGwWeekEndStart = new SqlParameter("gwWeekEndStart", SqlDbType.Time);
        sqlGwWeekEndStart.Value = gwWeekEndStart;
        settings.Parameters.Add(sqlGwWeekEndStart);

        SqlParameter sqlGwWeekEndFinish = new SqlParameter("gwWeekEndFinish", SqlDbType.Time);
        sqlGwWeekEndFinish.Value = gwWeekEndFinish;
        settings.Parameters.Add(sqlGwWeekEndFinish);

        //-------


        SqlParameter sqlAppOpen = new SqlParameter("AppOpen", SqlDbType.NVarChar);
        sqlAppOpen.Value = appOpen;
        settings.Parameters.Add(sqlAppOpen);

        SqlParameter sqlReviewingOn = new SqlParameter("ReviewingOn", SqlDbType.NVarChar);
        sqlReviewingOn.Value = reviewingOn;
        settings.Parameters.Add(sqlReviewingOn);

        SqlParameter sqlRolloverTime = new SqlParameter("RolloverTime", SqlDbType.Int);
        sqlRolloverTime.Value = rollGameOver;
        settings.Parameters.Add(sqlRolloverTime);

        SqlParameter sqlRollPracticeOver = new SqlParameter("RollPracticeOver", SqlDbType.Int);
        sqlRollPracticeOver.Value = rollPracticeOver;
        settings.Parameters.Add(sqlRollPracticeOver);

        SqlParameter sqlSiteTitle = new SqlParameter("SiteTitle", SqlDbType.NVarChar);
        sqlSiteTitle.Value = siteTitle;
        settings.Parameters.Add(sqlSiteTitle);

        SqlParameter sqlReceiveNotifications = new SqlParameter("Notifications", SqlDbType.NVarChar);
        sqlReceiveNotifications.Value = receiveNotifications;
        settings.Parameters.Add(sqlReceiveNotifications);

        SqlParameter sqlTeamSchedulingOn = new SqlParameter("TeamSchedulingOn", SqlDbType.NVarChar);
        sqlTeamSchedulingOn.Value = teamSchedulingOn;
        settings.Parameters.Add(sqlTeamSchedulingOn);

        try
        {
            //Open the connection
            con.Open();
            //Run the command
            settings.ExecuteNonQuery();
            success = "The application settings were saved.";
            con.Close();
        }
        catch (SqlException e)
        {
            //Return error message if unsuccessful.
            success = e.Message;
            //Close connection
            
        }

        
        return success;
    }


    //-----GETTERS------------------------


    public List<string> getAdminEmail()
    {
        List<string> email = new List<string>();
        email.Add(adminEmail);
        return email;
    }

    public string getAnnouncement()
    {
        return announcement;
    }


    public string getLogoUrl()
    {
        return logoUrl;
    }
    public string getSiteTitle()
    {
        return siteTitle;
    }

    public string getTheme()
    {
        return theme;
    }

    public string getAssignEmailTemplate()
    {
        return assignEmailTemplate;
    }


    public string getRejectEmailTemplate()
    {
        return rejectEmailTemplate;
    }

    public string getGwWeekDayStart()
    {
        return gwWeekDayStart.ToString();
    }

    public string getGwWeekDayFinish()
    {
        return gwWeekDayFinish.ToString();
    }
    public string getGwWeekEndStart()
    {
        return gwWeekEndStart.ToString();
    }

    public string getGwWeekEndFinish()
    {
        return gwWeekEndFinish.ToString();
    }
    public int getRolloverTime()
    {
        return rollGameOver;
    }

    public int getRollPracticeOver()
    {
        return rollPracticeOver;
    }

    public string getAppOpen()
    {
        return appOpen;
    }

    public string getReviewingOn()
    {
        return reviewingOn;
    }

    public string getReceiveNotifications()
    {
        return receiveNotifications;
    }

    public string getTeamSchedulingOn()
    {
        return teamSchedulingOn;
    }

    //-----SETTERS-----------------------------

    public void setAdminEmail(string email)
    {
        if (email.Length <= 1000)
            adminEmail = email;
    }

    public void setAnnouncement(string newAnouncement)
    {
        if (newAnouncement.Length <= 1000)
            announcement = newAnouncement;
    }


    public void setLogoUrl(string newLogo)
    {
        if (newLogo.Length <= 1000)
            logoUrl = newLogo;
    }
    public void setSiteTitle(string newTitle)
    {
        if (newTitle.Length <= 30)
            siteTitle = newTitle;
    }

    public void setTheme(string newTheme)
    {
        if (newTheme.Length <= 1000)
            theme = newTheme;
    }

    public void setAssignEmailTemplate(string assignEmail)
    {
        if (assignEmail.Length <= 1000)
            assignEmailTemplate = assignEmail;
    }

    public void setRejectEmailTemplate(string rejectEmail)
    {
        if (rejectEmail.Length <= 1000)
            rejectEmailTemplate = rejectEmail;
    }

    public void setGwWeekDayStart(string weekdayStart)
    {

        gwWeekDayStart = TimeSpan.Parse(weekdayStart);
    }

    public void setGWWeekDayFinish(string weekdayFinish)
    {

        gwWeekDayFinish = TimeSpan.Parse(weekdayFinish);
    }

    public void setGwWeekEndStart(string weekendStart)
    {

        gwWeekEndStart = TimeSpan.Parse(weekendStart);
    }

    public void setGWWeekEndFinish(string weekendFinish)
    {
        gwWeekEndFinish = TimeSpan.Parse(weekendFinish);
    }
    public void setRolloverTime(int newGameRoll)
    {
        rollGameOver = newGameRoll * 60;
    }

    public void setRollPracticeOver(int hours)
    {

        rollPracticeOver = hours * 60;
    }

    public void setAppOpen(string open)
    {
        if (open.Equals("true") || open.Equals("false"))
            appOpen = open;
    }

    public void setReviewingOn(string reviewing)
    {
        if (reviewing.Equals("true") || reviewing.Equals("false"))
            reviewingOn = reviewing;
    }

    public void setReceiveNotifications(string notificationsOn)
    {
        if (notificationsOn.Equals("true")||notificationsOn.Equals("false"))
            receiveNotifications = notificationsOn;
    }

    public void setTeamSchedulingOn(string teamSchedule)
    {
        if (teamSchedule.Equals("true") || teamSchedule.Equals("false"))
            teamSchedulingOn = teamSchedule;
    }

}