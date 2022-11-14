using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration.Provider;
using System.Web.Security;
/// <summary>
/// Ice Manipulation Class
/// This class abstracts all sql manipulation (Inserts, Updates, Deletions) of the database into a series of 
/// static methods that can be used anywhere in the website.
/// 
/// Author: Jonathan
/// 
/// General sql interaction format
/// 
/// 1) Define a connection
/// 2) Define parameters to give to the sql command
/// 3) Define the sql command, giving it an sql string, and a connection to use - interchangeable with (2)
/// 4) Add parameters to sql command
/// 5) Open the connection
/// 6) Run the sql command
/// 7) Close the connection.
/// 
/// Some methods overloaded to be given the sql connection for the purpose of running the method multiple 
/// times using a single external connection (instead of opening and closing a connection for every entry)
/// 
/// Methods:
/// createIce(int teamId, int location, DateTime date, DateTime startTime, DateTime endTime, string iceType, int duration, Boolean unassigned)
/// createIce(int teamId, int location, DateTime date, DateTime startTime, DateTime endTime, string iceType, int duration, Boolean unassigned, SqlConnection con)
/// updateIce(int iceId, int teamId, DateTime date, DateTime startTime, DateTime endTime, string iceType, int duration, Boolean unassigned)
/// switchToPractice(int iceId)
/// switchToGame(int iceId)
/// returnIce(int iceId, int returnerId, Boolean reviewing)
/// requestIce(int iceId, int requesterId)
/// assignIce(int iceId, int teamId, string teamComment, string commentAll)
/// rejectIce(int iceId, int teamId, string teamComment)
/// rollover()
/// deleteData()
/// deleteTeams()
/// 
/// </summary>
public class IceManipulation
{
    //Static method that creates a new ice time in the database 
    public static string createIce(int teamId, int location, DateTime date, DateTime startTime, DateTime endTime, string iceType, int duration, Boolean unassigned)
    {
        string result = "";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

            //---------------------------------------------
            // Parameters for adding to teams table
            SqlParameter sqlLocation = new SqlParameter("@LocationId", SqlDbType.Int, 4, "LocationId");
            sqlLocation.Value = location;

            SqlParameter sqlDate = new SqlParameter("@Date", SqlDbType.DateTime, 4, "Date");
            sqlDate.Value = date;

            SqlParameter sqlStartTime = new SqlParameter("@StartTime", SqlDbType.DateTime, 8, "StartTime");
            sqlStartTime.Value = startTime;

            SqlParameter sqlEndTime = new SqlParameter("@EndTime", SqlDbType.DateTime, 8, "EndTime");
            sqlEndTime.Value = endTime;

            SqlParameter sqlDuration = new SqlParameter("@TimeLength", SqlDbType.Int, 4, "TimeLength");
            sqlDuration.Value = duration;

            SqlParameter sqlIceType = new SqlParameter("@IceType", SqlDbType.NVarChar, 256, "IceType");
            sqlIceType.Value = iceType;


            SqlParameter assigned = new SqlParameter("@TeamId", SqlDbType.Int, 4, "TeamId");

            //SqlParameter assigned = new SqlParameter("@TeamId",SqlDbType.Int,4,ParameterDirection.Input,true,10,0,"TeamId",DataRowVersion.Current,
            if (unassigned)
            {
                assigned.Value = DBNull.Value;
            }
            else
            {
                assigned.Value = teamId;
            }


            //--------------------------------------------------
            con.Open();
            //Add to icetimes table
            SqlCommand createIce = new SqlCommand("INSERT INTO IceTimes(LocationId, TeamId, Date, StartTime,EndTime, TimeLength, IceType) VALUES (@LocationId,@TeamId, @Date, @StartTime,@EndTime, @TimeLength, @IceType);", con);
            createIce.Parameters.Add(sqlStartTime);
            createIce.Parameters.Add(sqlDate);
            createIce.Parameters.Add(sqlEndTime);
            createIce.Parameters.Add(sqlLocation);
            createIce.Parameters.Add(sqlDuration);
            createIce.Parameters.Add(assigned);
            createIce.Parameters.Add(sqlIceType);
            createIce.ExecuteNonQuery();
            con.Close();

            result = "Ice Time successfully created.";

        }

        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }

    //Create Ice, Overloaded for connection string. 
    public static string createIce(int teamId, int location, DateTime date, DateTime startTime, DateTime endTime, string iceType, int duration, Boolean unassigned, SqlConnection con)
    {
        string result = "";
        try
        {

            //---------------------------------------------
            // Parameters for adding to teams table
            SqlParameter sqlLocation = new SqlParameter("@LocationId", SqlDbType.Int, 4, "LocationId");
            sqlLocation.Value = location;

            SqlParameter sqlDate = new SqlParameter("@Date", SqlDbType.DateTime, 4, "Date");
            sqlDate.Value = date;

            SqlParameter sqlStartTime = new SqlParameter("@StartTime", SqlDbType.DateTime, 8, "StartTime");
            sqlStartTime.Value = startTime;

            SqlParameter sqlEndTime = new SqlParameter("@EndTime", SqlDbType.DateTime, 8, "EndTime");
            sqlEndTime.Value = endTime;

            SqlParameter sqlDuration = new SqlParameter("@TimeLength", SqlDbType.Int, 4, "TimeLength");
            sqlDuration.Value = duration;

            SqlParameter sqlIceType = new SqlParameter("@IceType", SqlDbType.NVarChar, 256, "IceType");
            sqlIceType.Value = iceType;


            SqlParameter assigned = new SqlParameter("@TeamId", SqlDbType.Int, 4, "TeamId");

            //SqlParameter assigned = new SqlParameter("@TeamId",SqlDbType.Int,4,ParameterDirection.Input,true,10,0,"TeamId",DataRowVersion.Current,
            if (unassigned)
            {
                assigned.Value = DBNull.Value;
            }
            else
            {
                assigned.Value = teamId;
            }

            //--------------------------------------------------

            //Add to icetimes table
            SqlCommand createIce = new SqlCommand("INSERT INTO IceTimes(LocationId, TeamId, Date, StartTime,EndTime, TimeLength, IceType) VALUES (@LocationId,@TeamId, @Date, @StartTime,@EndTime, @TimeLength, @IceType);", con);
            createIce.Parameters.Add(sqlStartTime);
            createIce.Parameters.Add(sqlDate);
            createIce.Parameters.Add(sqlEndTime);
            createIce.Parameters.Add(sqlLocation);
            createIce.Parameters.Add(sqlDuration);
            createIce.Parameters.Add(assigned);
            createIce.Parameters.Add(sqlIceType);
            createIce.ExecuteNonQuery();

            result = "Ice Time successfully created.";
        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }

    //Static method that updates an ice time in the database 
    public static string updateIce(int iceId, int teamId, DateTime date, DateTime startTime, DateTime endTime, string iceType, int duration, Boolean unassigned)
    {
        string result = "";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

            //opens the connection
            con.Open();

            //------stating  the parameters-----------
            SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
            SqlParameter sqlDate = new SqlParameter("@Date", SqlDbType.Date);
            SqlParameter sqlStart = new SqlParameter("@StartTime", SqlDbType.DateTime);
            SqlParameter sqlEnd = new SqlParameter("@EndTime", SqlDbType.DateTime);
            SqlParameter sqlDur = new SqlParameter("@Duration", SqlDbType.Int);
            SqlParameter sqlIceType = new SqlParameter("@IceType", SqlDbType.NVarChar);
            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);

            //-----setting the values of the parameters
            sqlIceId.Value = iceId;
            sqlDate.Value = date;
            sqlStart.Value = startTime;
            sqlEnd.Value = endTime;
            sqlDur.Value = duration;
            sqlIceType.Value = iceType;

            //if it is unassigned then the value is null
            if (unassigned)
                sqlTeamId.Value = DBNull.Value;
            else
                sqlTeamId.Value = teamId;

            //update query
            SqlCommand updateIce = new SqlCommand("UPDATE IceTimes SET TeamId=@TeamId, Date=@Date, StartTime=@StartTime,EndTime=@EndTime, TimeLength=@Duration, IceType=@IceType WHERE IceId=@IceId", con);

            //-----adding parameters
            updateIce.Parameters.Add(sqlIceId);
            updateIce.Parameters.Add(sqlDate);
            updateIce.Parameters.Add(sqlStart);
            updateIce.Parameters.Add(sqlEnd);
            updateIce.Parameters.Add(sqlDur);
            updateIce.Parameters.Add(sqlIceType);
            updateIce.Parameters.Add(sqlTeamId);

            //execute it
            updateIce.ExecuteNonQuery();

            //close
            con.Close();

            result = "Changes are successfully saved!";
        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }

        catch (HttpException ex)
        {
            result = ex.Message;
        }

        return result;
    }


    //Switches a certain iceTime to a game
    public static string switchToGame(int iceId)
    {
        string result = "";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            SqlCommand command = new SqlCommand("UPDATE IceTimes SET IceType = @IceType  WHERE IceId = @IceId", con);

            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
            sqlIceId.Value = iceId;
            command.Parameters.Add(sqlIceId);

            SqlParameter sqlIceType = new SqlParameter("@IceType", SqlDbType.NVarChar);
            sqlIceType.Value = "Game";
            command.Parameters.Add(sqlIceType);

            command.ExecuteNonQuery();
            con.Close();

            result = "Ice Type successfully changed.";
        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }

    //Switches a certain iceTime to a Practice
    public static string switchToPractice(int iceId)
    {
        string result = "";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            SqlCommand command = new SqlCommand("UPDATE IceTimes SET IceType = @IceType WHERE IceId = @IceId", con);

            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
            sqlIceId.Value = iceId;
            command.Parameters.Add(sqlIceId);

            SqlParameter sqlIceType = new SqlParameter("@IceType", SqlDbType.NVarChar);
            sqlIceType.Value = "Practice";
            command.Parameters.Add(sqlIceType);

            command.ExecuteNonQuery();
            con.Close();

            result = "Ice Type successfully changed.";
        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }


    //Returns an ice time, given an icetime and a team id for the returner and whether allocator reviewing is on
    //Edits the ice time row and make an entry in the Returned Ice table
    public static string returnIce(int iceId, int returnerId, Boolean reviewing)
    {
        string result = "true";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

            //Assign the currently logged in teams id to be used
            SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
            sqlTeamId.Value = returnerId;

            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
            sqlIceId.Value = iceId;

            SqlParameter sqlStatus = new SqlParameter("@Status", SqlDbType.NVarChar);
            sqlStatus.Value = "Open";

            SqlParameter sqlIsApproved = new SqlParameter("@IsApproved", SqlDbType.NVarChar);
            sqlIsApproved.Value = (reviewing ? "false" : "true");

            //The actual command to run to update the database.
            SqlCommand sqlCmdReturnIce = new SqlCommand(
                "Insert Into ReturnedIce (IceId,ReturnerId,ReturnedDate,IsApproved,Status) VALUES (@IceId,@TeamId,GETDATE(), @IsApproved,@Status); UPDATE IceTimes SET TeamId=null WHERE IceId=@IceId;", con);
            sqlCmdReturnIce.Parameters.Add(sqlIceId);
            sqlCmdReturnIce.Parameters.Add(sqlTeamId);
            sqlCmdReturnIce.Parameters.Add(sqlIsApproved);
            sqlCmdReturnIce.Parameters.Add(sqlStatus);

            con.Open();
            sqlCmdReturnIce.ExecuteNonQuery();

            con.Close();
        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }

    //Creates a request for an ice time, given the ice id of the ice time and the team id of the requester 
    public static string requestIce(int iceId, int requesterId)
    {
        string result = "true";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            //Assign the currently logged in teams id to be used
            SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
            sqlTeamId.Value = requesterId;

            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
            sqlIceId.Value = iceId;

            SqlParameter sqlStatus = new SqlParameter("@Status", SqlDbType.NVarChar);
            sqlStatus.Value = "Open";


            //The actual command to run to update the database.
            SqlCommand sqlCmdRequestIce = new SqlCommand(
                "Insert Into Requests (TeamId, IceId,Status) VALUES (@TeamId,@IceId,@Status);", con);
            sqlCmdRequestIce.Parameters.Add(sqlIceId);
            sqlCmdRequestIce.Parameters.Add(sqlTeamId);
            sqlCmdRequestIce.Parameters.Add(sqlStatus);
            sqlCmdRequestIce.ExecuteNonQuery();

            con.Close();
        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }

    //Cancels the requested ice
    public static string cancelRequest(int IceId)
    {
        string result = "true";

        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
            sqlIceId.Value = IceId;

            SqlParameter sqlStatus = new SqlParameter("@Status", SqlDbType.NVarChar);
            sqlStatus.Value = "Closed";

            SqlCommand updateReturnedIce = new SqlCommand(
                "UPDATE returnedIce SET Status=@Status WHERE IceId=@IceId; UPDATE requests SET Status=@Status WHERE IceId=@IceId", con);
            updateReturnedIce.Parameters.Add(sqlIceId);
            updateReturnedIce.Parameters.Add(sqlStatus);

            updateReturnedIce.ExecuteNonQuery();

            con.Close();
        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }

        return result;
    }

    //Handles assigning ice to a team. Takes the ice id of the ice time, the team id of the receiver, a specific team comment, and the general comment 
    public static string assignIce(int iceId, int teamId, string teamComment, string commentAll)
    {
        string result = "true";

        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            //Assign the currently logged in teams id to be used
            SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
            sqlTeamId.Value = teamId;

            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
            sqlIceId.Value = iceId;

            SqlParameter sqlComment = new SqlParameter("@Comments", SqlDbType.NVarChar);
            sqlComment.Value = teamComment;

            SqlParameter sqlCommentAll = new SqlParameter("@CommentAll", SqlDbType.NVarChar);
            sqlCommentAll.Value = commentAll;

            SqlParameter sqlDate = new SqlParameter("@AssignedDate", SqlDbType.Date);
            sqlDate.Value = DateTime.Today;

            SqlParameter sqlClosed = new SqlParameter("@Closed", SqlDbType.NVarChar);
            sqlClosed.Value = "Closed";

            SqlParameter sqlAccepted = new SqlParameter("@Accepted", SqlDbType.NVarChar);
            sqlAccepted.Value = "Accepted";

            //The actual command to run to update the database.
            SqlCommand assignIce = new SqlCommand(
                "UPDATE IceTimes SET TeamId=@TeamId WHERE IceId=@IceId;" +
                "UPDATE ReturnedIce SET Status = @Closed, Comments=@CommentAll, AssignedId=@TeamId, AssignedDate=@AssignedDate  WHERE IceId=@IceId; UPDATE Requests SET Status = @Accepted, Comments=@Comments WHERE IceId=@IceId AND TeamId=@TeamId;", con);
            assignIce.Parameters.Add(sqlIceId);
            assignIce.Parameters.Add(sqlTeamId);
            assignIce.Parameters.Add(sqlComment);
            assignIce.Parameters.Add(sqlCommentAll);
            assignIce.Parameters.Add(sqlDate);
            assignIce.Parameters.Add(sqlClosed);
            assignIce.Parameters.Add(sqlAccepted);

            assignIce.ExecuteNonQuery();

            con.Close();
            
        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }


    //Rejects a request for an ice time. Takes the ice id of the ice time, the team id of the requester, and the team specific comment.
    public static string rejectIce(int iceId, int teamId, string teamComment)
    {
        string result = "true";

        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            //Assign the currently logged in teams id to be used
            SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);
            sqlTeamId.Value = teamId;

            SqlParameter sqlIceId = new SqlParameter("@IceId", SqlDbType.Int);
            sqlIceId.Value = iceId;

            SqlParameter sqlComment = new SqlParameter("@Comments", SqlDbType.NVarChar);
            sqlComment.Value = teamComment;

            SqlParameter sqlRejected = new SqlParameter("@Rejected", SqlDbType.NVarChar);
            sqlRejected.Value = "Rejected";

            //The actual command to run to update the database.
            SqlCommand rejectIce = new SqlCommand(
                "UPDATE Requests SET Status = @Rejected, Comments=@Comments WHERE IceId=@IceId AND TeamId=@TeamId;", con);
            rejectIce.Parameters.Add(sqlIceId);
            rejectIce.Parameters.Add(sqlTeamId);
            rejectIce.Parameters.Add(sqlComment);
            rejectIce.Parameters.Add(sqlRejected);

            rejectIce.ExecuteNonQuery();

            con.Close();

        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }



    //RollOver Method, Automatically called regularly
    //Checks all Returned Game times, if less than the saved roll over time then switch it to a practice.
    //Checks all requests, and if any have timed out closes them.
    public static string rollover()
    {
        string result = "";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            SqlCommand rollOver = new SqlCommand("UPDATE IceTimes SET IceType = @Practice  WHERE TeamId IS NULL AND (DATEDIFF(minute,GETDATE(), StartTime) <= (Select RollOverTime FROM Settings where SettingId=1));"
                + "UPDATE IceTimes SET IceType = @Open  WHERE TeamId IS NULL AND (DATEDIFF(minute,GETDATE(), StartTime) <= (Select RollPracticeOver FROM Settings where SettingId=1));"
                + "UPDATE Requests SET Status = @Closed WHERE (Select TeamId FROM IceTimes where IceTimes.IceId = Requests.IceId) IS NULL AND (DATEDIFF(minute,GETDATE(), (Select StartTime FROM IceTimes where IceTimes.IceId = Requests.IceId)) <= 0)", con);


            SqlParameter sqlOpen = new SqlParameter("@Open", SqlDbType.NVarChar);
            sqlOpen.Value = "Open";
            rollOver.Parameters.Add(sqlOpen);

            SqlParameter sqlClosed = new SqlParameter("@Closed", SqlDbType.NVarChar);
            sqlClosed.Value = "Closed";
            rollOver.Parameters.Add(sqlClosed);

            SqlParameter sqlPractice = new SqlParameter("@Practice", SqlDbType.NVarChar);
            sqlPractice.Value = "Practice";
            rollOver.Parameters.Add(sqlPractice);

            rollOver.ExecuteNonQuery();
            con.Close();

            result = "Ice Time successfully rolled.";
        }

        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }

    //New Season method. Removes all ice times, returned ice, and requests. Used between seasons to give a fresh slate.
    public static string deleteData()
    {
        string result = "";

        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            con.Open();

            SqlCommand rollOver = new SqlCommand("DELETE FROM requests; DELETE FROM ReturnedIce; DELETE FROM IceTimes; UPDATE Teams SET PracticeBalance = 0, GameBalance = 0;DBCC CheckIdent('IceTimes',RESEED,0);", con);

            rollOver.ExecuteNonQuery();
            con.Close();

            result = "The last season has been removed.";
        }

        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }

        return result;
    }


    //Remove teams method. Clears all teams from the database, used between seasons to create a fresh start. Intended to only run after newSeason() has successfully completed.
    public static string deleteTeams()
    {

        //Psuedo
        /*
         * 1) Retrieve team names from Team table in database, creates a "|" seperated list of names in a string
         * 2) For each name. Remove row from table Teams, Membership provider.deleteUser
         */
        string result = "";
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
            
            SqlCommand settings = new SqlCommand("Select UserName FROM Teams", con);
            settings.CommandType = CommandType.Text;

            SqlCommand sqlCmdRemoveTeam = new SqlCommand("DELETE FROM Teams WHERE UserName=@TeamName;DBCC CheckIdent('Teams',RESEED,0);", con);
            sqlCmdRemoveTeam.CommandType = CommandType.Text;

            SqlParameter sqlTeamName = new SqlParameter("@TeamName", SqlDbType.NVarChar);

            sqlCmdRemoveTeam.Parameters.Add(sqlTeamName);

            string username = "";
                        con.Open();
            SqlDataReader reader = settings.ExecuteReader();

            while (reader.Read())
            {
                username +="|"+ reader[0].ToString();
            }
            username = username.Substring(1);//remove leading "|"

            reader.Close(); //must close the reader before we interact with the connection again.


            foreach (string user in username.Split('|') ){
                sqlTeamName.Value = user;
                sqlCmdRemoveTeam.ExecuteNonQuery();

                //Remove User = Username, remove all associated data = true
                Membership.DeleteUser(user, true);
            }

            con.Close();
            result = "All Teams have been deleted.";
        }

        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (HttpException ex)
        {
            result = ex.Message;
        }
        return result;
    }
}