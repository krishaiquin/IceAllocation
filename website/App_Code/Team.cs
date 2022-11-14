using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Team
/// The Team class represents a single team, and allows for retrieving team information based on TeamId or 
/// team name (which is the same as a team username for logging in). It has methods for increasing and 
/// decreasing a teams balances, retrieving the teams contact information, and saving team information 
/// to the database.
/// 
/// Author: Jonathan
/// </summary>
public class Team
{
    private string userName, managerEmail, coachEmail, announcement, receiveNotifications;
    private int teamId, whoToEmail;
    private int gameBalance, practiceBalance;


    // -------------CONSTRUCTORS------------------
    //Constructor for an empty Team.
    public Team()
    {
        userName = "";
        gameBalance = 0;
        practiceBalance = 0;
        teamId = 0;
        managerEmail = "";
        coachEmail = "";
        whoToEmail = 3;
        announcement = "";
    }

    //Constructor that loads data given a team id
    public Team(int teamId)
    {
        this.teamId = teamId;
        loadInfo(teamId);

    }

    //Constructor that load data given a username
    public Team(string userName)
    {
        teamId = findTeamId(userName);
        loadInfo(teamId);

    }


    //-------------------------------------------------

    //Load just the game balances into this object
    public string loadBalances(String username)
    {
        string result = "";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

            SqlCommand settings = new SqlCommand("Select GameBalance, PracticeBalance FROM Teams WHERE UserName= @UserName", con);
            settings.CommandType = CommandType.Text;

            SqlParameter sqlUserName = new SqlParameter("@UserName", SqlDbType.NVarChar);
            sqlUserName.Value = username;
            settings.Parameters.Add(sqlUserName);

            con.Open();
            SqlDataReader reader = settings.ExecuteReader();
            reader.Read();
            gameBalance = reader.GetInt32(0);
            practiceBalance = reader.GetInt32(1);
            con.Close();
        }
        catch (SqlException ex)
        {
            result = "Failed to load balances.";
        }
        return result;
    }


    //Loads the info for the given team id into this object
    public string loadInfo(int tempId)
    {
        string result = "";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

            SqlCommand sqlGetTeamInfo = new SqlCommand("Select UserName, CoachEmail,ManagerEmail,GameBalance, PracticeBalance, WhoToEmail, Announcement, ReceiveNotifications FROM Teams WHERE TeamId= @TeamId", con);
            sqlGetTeamInfo.CommandType = CommandType.Text;

            SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
            sqlTeamId.Value = tempId;
            sqlGetTeamInfo.Parameters.Add(sqlTeamId);

            con.Open();
            SqlDataReader reader = sqlGetTeamInfo.ExecuteReader();
            reader.Read();

            //Process the results of the query
            if (!reader.IsDBNull(0))
            {
                userName = reader.GetString(0);
            }
            if (!reader.IsDBNull(1))
            {
                coachEmail = reader.GetString(1);
            }
            if (!reader.IsDBNull(2))
            {
                managerEmail = reader.GetString(2);
            }
            if (!reader.IsDBNull(3))
            {
                gameBalance = reader.GetInt32(3);
            }
            if (!reader.IsDBNull(4))
            {
                practiceBalance = reader.GetInt32(4);
            }
            if (!reader.IsDBNull(5))
            {
                whoToEmail = int.Parse(reader[5].ToString());
            }
            if (!reader.IsDBNull(6))
            {
                announcement = reader.GetString(6);
            }
            if (!reader.IsDBNull(7))
            {
                receiveNotifications = reader.GetString(7);
            }
            con.Close();
        }
        catch (SqlException ex)
        {
            result = "failed to load team info. "+ex.Message;
        }
        return result;
    }

    //Saves the current information to the database
    public string save()
    {

        string result = "";

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

        SqlCommand sqlCmdSaveTeam = new SqlCommand("UPDATE Teams SET CoachEmail=@Coach,ManagerEmail=@Manager,GameBalance=@Games, PracticeBalance=@Practices, WhoToEmail=@WhoToEmail,"
            + " Announcement=@Announcement, ReceiveNotifications=@ReceiveNotifications WHERE TeamId= @TeamId", con);
        sqlCmdSaveTeam.CommandType = CommandType.Text;

        SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
        sqlTeamId.Value = teamId;
        sqlCmdSaveTeam.Parameters.Add(sqlTeamId);

        SqlParameter sqlCoachEmail = new SqlParameter("@Coach", SqlDbType.NVarChar);
        sqlCoachEmail.Value = coachEmail;
        sqlCmdSaveTeam.Parameters.Add(sqlCoachEmail);

        SqlParameter sqlManagerEmail = new SqlParameter("@Manager", SqlDbType.NVarChar);
        sqlManagerEmail.Value = managerEmail;
        sqlCmdSaveTeam.Parameters.Add(sqlManagerEmail);

        SqlParameter sqlGameBalance = new SqlParameter("@Games", SqlDbType.Int);
        sqlGameBalance.Value = gameBalance;
        sqlCmdSaveTeam.Parameters.Add(sqlGameBalance);

        SqlParameter sqlPracticeBalance = new SqlParameter("@Practices", SqlDbType.Int);
        sqlPracticeBalance.Value = practiceBalance;
        sqlCmdSaveTeam.Parameters.Add(sqlPracticeBalance);

        SqlParameter sqlWhoToEmail = new SqlParameter("@WhoToEmail", SqlDbType.Int);
        sqlWhoToEmail.Value = whoToEmail;
        sqlCmdSaveTeam.Parameters.Add(sqlWhoToEmail);


        SqlParameter sqlAnnouncement = new SqlParameter("@Announcement", SqlDbType.NVarChar);
        sqlAnnouncement.Value = announcement;
        sqlCmdSaveTeam.Parameters.Add(sqlAnnouncement);

        SqlParameter sqlReceiveNotifications = new SqlParameter("@ReceiveNotifications", SqlDbType.NVarChar);
        sqlReceiveNotifications.Value = receiveNotifications;
        sqlCmdSaveTeam.Parameters.Add(sqlReceiveNotifications);

        try
        {
            con.Open();
            sqlCmdSaveTeam.ExecuteNonQuery().ToString();
            con.Close();

            result = "Your changes have been saved.";
        }
        catch (SqlException ex)
        {
            result = "There was an error saving the changes.";
        }

        return result;
    }


    //---------------------------------------
    //Increase Game: increases this teams game balance by one
    public string increaseGame()
    {
        string result = "";



        //confirm that an increase is allowed.
        if (gameBalance + 1 < 1)
        {
            gameBalance += 1;
            result = save();

            if (gameBalance == 0)
                result += clearGameRequests();

        }
        else
        {
            result = "Game balance is even, no increase allowed";
        }

        return result;
    }


    //Increase Practice: increases this teams Practice balance by one
    public string increasePractice()
    {
        string result = "";


        if (practiceBalance + 1 < 1)
        {
            practiceBalance += 1;
            result = save();

            if (practiceBalance == 0)
                result += " " + clearPracticeRequests();
        }
        else
        {
            result = "Practice balance is even, no increase allowed";
        }

        return result;
    }



    //Increase Game: increases this teams game balance by one
    public string decreaseGame()
    {
        string result = "";
        gameBalance -= 1;
        result = save();
        return result;
    }


    //Increase Practice: increases this teams Practice balance by one
    public string decreasePractice()
    {
        string result = "";

        practiceBalance -= 1;

        result = save();

        return result;
    }

    // Clears all practice requests for this team, used when practice balance hits 0
    private string clearPracticeRequests()
    {
        string result = "";
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

        try
        {
            con.Open();
            SqlCommand clearPractices = new SqlCommand("Update Requests SET Status  = 'Balanced' WHERE TeamId= @TeamId AND Status = 'Open' AND (Select IceType FROM IceTimes WHERE IceTimes.IceId = Requests.IceId) = 'Practice'", con);
            clearPractices.CommandType = CommandType.Text;

            SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
            sqlTeamId.Value = teamId;
            clearPractices.Parameters.Add(sqlTeamId);

            clearPractices.ExecuteNonQuery();
            con.Close();
        }
        catch (SqlException ex)
        {
            result = "Failed to clear practice requests.";
        }
        return result;
    }

    // Clears all game requests for this team, used when game balance hits 0
    private string clearGameRequests()
    {
        string result = "";
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

        try
        {
            con.Open();
            SqlCommand clearGames = new SqlCommand("Update Requests SET Status = 'Balanced' WHERE TeamId= @TeamId AND Status = 'Open' AND (Select IceType FROM IceTimes WHERE IceTimes.IceId = Requests.IceId) = 'Game'", con);
            clearGames.CommandType = CommandType.Text;

            SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
            sqlTeamId.Value = teamId;
            clearGames.Parameters.Add(sqlTeamId);

            clearGames.ExecuteNonQuery();
            con.Close();
        }
        catch (SqlException ex)
        {
            result = "Failed to clear game requests.";
        }
        return result;
    }
    //-----GETTERS ----------------------------------------

    

    public int getTeamId()
    {
        return teamId;
    }

    public string getTeamName()
    {
        return userName;
    }

    public int getGameBalance()
    {
        return gameBalance;
    }

    public int getPracticeBalance()
    {
        return practiceBalance;
    }

    public string getCoachEmail()
    {
        return coachEmail;
    }

    public string getManagerEmail()
    {
        return managerEmail;
    }

    public int getWhoToEmail()
    {
        return whoToEmail;
    }

    public string getAnnouncement()
    {
        return announcement;
    }

    public string getNotifications()
    {
        return receiveNotifications;
    }

    //-----SETTERS----------------------------
    public void setNotifications(string notifications)
    {
        receiveNotifications = notifications;
    }

    public void setCoachEmail(string email)
    {
        coachEmail = email;
    }


    public void setManagerEmail(string email)
    {
        managerEmail = email;
    }

    public void setWhoToEmail(int who)
    {
        whoToEmail = who;
    }

    public void setAnnouncement(string announcement)
    {
        this.announcement = announcement;
    }




    //--------------------------------------------------

    public string ToString()
    {
        return userName + " (Game Balance: " + gameBalance + " / Practice Balance: " + practiceBalance + ")";
    }

    public static string findTeamName(int tempId)
    {
        string UserName = "";
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

        SqlCommand settings = new SqlCommand("Select UserName FROM Teams WHERE TeamId= @TeamId", con);
        settings.CommandType = CommandType.Text;

        SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
        sqlTeamId.Value = tempId;
        settings.Parameters.Add(sqlTeamId);
        try
        {
            con.Open();
            SqlDataReader reader = settings.ExecuteReader();
            reader.Read();
            if (reader[0].ToString() != null || reader[0].ToString() != "")
                UserName = reader[0].ToString();
            con.Close();
        }
        catch (SqlException ex)
        {
            UserName = "Team does not exist.";

        }
        return UserName;
    }


    //public method that returns a specific teams team id
    public static int findTeamId(String username)
    {
        int tempId = 0;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

        SqlCommand settings = new SqlCommand("Select TeamId FROM Teams WHERE UserName= @UserName", con);
        settings.CommandType = CommandType.Text;

        SqlParameter sqlUserName = new SqlParameter("@UserName", SqlDbType.NVarChar);
        sqlUserName.Value = username;
        settings.Parameters.Add(sqlUserName);
        try
        {
            con.Open();
            SqlDataReader reader = settings.ExecuteReader();
            reader.Read();
            if (!reader.IsDBNull(0))
                tempId = reader.GetInt32(0);
            con.Close();
        }
        catch (Exception ex)
        {
            //if there is an exception, team does not exist, return invalid id 0
        }
        return tempId;
    }


    //getContactInfo() - Returns the email addresses of the person or people to contact in a comma seperated string.
    public List<string> getContactInfo()
    {
        List<string> result = new List<string>();

        if (whoToEmail == 3)
        {
            result.Add(coachEmail);
            result.Add(managerEmail);
        }
        else if (whoToEmail == 1)
        {
            result.Add(coachEmail);
        }
        else
        {
            result.Add(managerEmail);
        }

        return result;
    }






    //Static method, returns a list of all team ids for teams that are enabled
    private static List<int> getActiveTeams()
    {
        List<int> actives = new List<int>();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

        SqlCommand settings = new SqlCommand("Select TeamId FROM Teams INNER JOIN aspnet_Membership ON Teams.UserId = aspnet_Membership.UserId WHERE IsApproved = 1", con);
        settings.CommandType = CommandType.Text;

        try
        {
            con.Open();
            SqlDataReader reader = settings.ExecuteReader();
            while (reader.Read())
            {

                if (reader[0].ToString() != null || reader[0].ToString() != "")
                {
                    actives.Add(int.Parse(reader[0].ToString()));
                }
            }

            con.Close();
        }
        catch (SqlException ex)
        {
            // Error reading team ids
            actives.Clear();
        }

        return actives;
    }

    //Generates a list of email addresses for all teams that are active, used for mass mailling
    //Takes into account who the team wants contacted
    public static List<string> getAllContactInfo()
    {

        //Populate a list of all active team ids
        List<int> activeTeams = getActiveTeams();

        List<string> teamEmails = new List<string>();


        Team teamToContact;
        foreach (int teamId in activeTeams)
        {
            //get team info
            teamToContact = new Team(teamId);
            if (teamToContact.getNotifications().Equals("true"))
            {
                foreach (string email in teamToContact.getContactInfo())
                    teamEmails.Add(email);
            }
        }

        return teamEmails;
    }

    //Generates a list of email addresses for all teams that are active and have a negative practice balance, used for mass mailling
    //Takes into account who the team wants contacted
    public static List<string> getAllNegativePractice()
    {

        //Populate a list of all active team ids
        List<int> activeTeams = getActiveTeams();

        List<string> teamEmails = new List<string>();


        Team teamToContact;
        foreach (int teamId in activeTeams)
        {
            //get team info
            teamToContact = new Team(teamId);
            //check if they want notifications, and if they have a negative balance
            if (teamToContact.getNotifications().Equals("true") && (teamToContact.getPracticeBalance()<0))
            {
                //add their contact info to the list of emails
                foreach (string email in teamToContact.getContactInfo())
                    teamEmails.Add(email);
            }
        }

        return teamEmails;
    }

    //Generates a list of email addresses for all teams that are active and have a negative game balance, used for mass mailling
    //Takes into account who the team wants contacted
    public static List<string> getAllNegativeGame()
    {

        //Populate a list of all active team ids
        List<int> activeTeams = getActiveTeams();

        List<string> teamEmails = new List<string>();


        Team teamToContact;
        foreach (int teamId in activeTeams)
        {
            //get team info
            teamToContact = new Team(teamId);
            //check if they want notifications, and if they have a negative balance
            if (teamToContact.getNotifications().Equals("true") && (teamToContact.getGameBalance() < 0))
            {
                //add their contact info to the list of emails
                foreach (string email in teamToContact.getContactInfo())
                    teamEmails.Add(email);
            }
        }

        return teamEmails;
    }
}