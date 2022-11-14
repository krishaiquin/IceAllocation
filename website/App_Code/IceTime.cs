using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for IceTime
/// Ice Time class, mainly used to find data about an ice time, and provide 
/// an appropriate string output for adding to emails.
/// 
/// The IceTime class represents a single ice time. It does not include any information about 
/// whether the ice has been returned or requested.  Functions that it does have detect whether 
/// there is any ice times in the database that conflict with the information stored in an ice 
/// time object, and whether that ice time is in a game window. 
/// 
/// Author: Jonathan
/// </summary>
public class IceTime
{

    private int iceId, locationId, teamId, duration;
    private string locationStr, iceType;
    private DateTime date, start, end;
    private Boolean unassigned;

    //Constructor, Base case, does not produce a valid ice time on its own.
    public IceTime()
    {
        iceId = 0;
        locationStr = "";
        iceType = "";
        date = new DateTime();
        start = new DateTime();
        end = new DateTime();
        duration = 0;
        unassigned = true;
    }

    //Constructor that retrieves information from the database
    public IceTime(int id)
    {
        iceId = id;
        locationStr = "";
        iceType = "";
        date = new DateTime();
        start = new DateTime();
        end = new DateTime();
        duration = 0;
        getIceTime(iceId);
        unassigned = true;
    }

    //Constructor that takes every detail as input
    public IceTime(int teamId, string iceType, string locationStr, int locationId,
        DateTime date, DateTime start, DateTime end, int duration, Boolean unassigned)
    {
        this.teamId = teamId;
        this.iceType = iceType;
        this.locationStr = locationStr;
        this.locationId = locationId;
        this.date = date;
        this.start = start;
        this.end = end;
        this.duration = duration;
        this.unassigned = unassigned;
    }


    //Saves the data in this object to the database.
    public string save()
    {
        string result = "";

        if (!hasConflict())
        {
            if (iceId == 0)
            {
                result = IceManipulation.createIce(teamId, locationId, date, start, end, iceType, duration, unassigned);
            }
            else
            {
                result = IceManipulation.updateIce(iceId, teamId, date, start, end, iceType, duration, unassigned);
            }
        }
        else
        {
            result = "This ice time has conflicts.";
        }
        return result;
    }

    //Saves the data in this object to the database, used for multi-create to use less connections to the database and save time
    public string save(SqlConnection con)
    {
        string result = "";

        if (!hasConflict())
        {
            if (iceId == 0)
            {
                result = IceManipulation.createIce(teamId, locationId, date, start, end, iceType, duration, unassigned,con);
            }
            else
            {
                result = IceManipulation.updateIce(iceId, teamId, date, start, end, iceType, duration, unassigned);
            }
        }
        else
        {
            result = "This ice time has conflicts.";
        }
        return result;
    }

    //To String, displays icetime data as a string
    public string ToString()
    {
        string result = iceType + ": " + date.DayOfWeek + " " + date.Day + " of " + shortMonth(date.Month) + " at " + locationStr + " from "
            + start.ToShortTimeString() + " to " + end.ToShortTimeString();
        return result;
    }

    //Full to string, displays all available data
    public string fullToString()
    {
        string result = teamId + "/" + iceId + " (" + iceType + "): " + date.DayOfWeek + " " + date.Day + " of " + shortMonth(date.Month) + " at " + locationStr + "(" + locationId + ") from "
            + start.ToShortTimeString() + " to " + end.ToShortTimeString() + "(" + duration + " min)";
        return result;
    }

    //Common to string, displays data in a different format
    public string commonToString()
    {
        string result = date.DayOfWeek + " " + iceType + ", " + start.ToShortTimeString() + " - " + end.ToShortTimeString() + " at " + locationStr;
        return result;
    }

    //Dates to string, returns just the month and day.
    public string datesToString()
    {
        string result = shortMonth(date.Month) + " " + date.Day;
        return result;
    }


    //getIceTime method: loads the data for a specified ice time into the object.
    private string getIceTime(int id)
    {
        string result = "true";
        try
        {
            iceId = id;

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

            SqlCommand iceTime = new SqlCommand("SELECT IceType, (SELECT FullName FROM Locations WHERE Locations.LocationId = IceTimes.LocationId), LocationId," +
                "Date, StartTime, EndTime, TeamId, TimeLength FROM IceTimes WHERE IceId = @IceId", con);
            iceTime.CommandType = CommandType.Text;

            SqlParameter sqlIceId = new SqlParameter("IceId", iceId);
            iceTime.Parameters.Add(sqlIceId);

            con.Open();
            SqlDataReader reader = iceTime.ExecuteReader();



            //Read the whole table, check which key we have and save it to the proper place
            //Allows for rows to be out of order
            //Ignores rows we dont expect
            reader.Read();

            if (!reader.IsDBNull(0))
                iceType = reader.GetString(0);
            if (!reader.IsDBNull(1))
                locationStr = reader.GetString(1);
            if (!reader.IsDBNull(2))
                locationId = reader.GetInt32(2);
            if (!reader.IsDBNull(3))
                date = reader.GetDateTime(3);
            if (!reader.IsDBNull(4))
                start = reader.GetDateTime(4);
            if (!reader.IsDBNull(5))
                end = reader.GetDateTime(5);
            if (!reader.IsDBNull(6))
                teamId = reader.GetInt32(6);
            if (!reader.IsDBNull(7))
                duration = reader.GetInt32(7);

            con.Close();
        }
        catch (SqlException ex)
        {
            result = "There was an error getting team data from the database. "+ex.Message;
        }
        return result;
    }


    //Short Month Method - Given a month number returns abreviated month name. (1->Jan. : 12->Dec.)
    private static string shortMonth(int month)
    {
        string result = "";
        if (month == 1)
            result = "Jan.";
        else if (month == 2)
            result = "Feb.";
        else if (month == 3)
            result = "Mar.";
        else if (month == 4)
            result = "Apr.";
        else if (month == 5)
            result = "May.";
        else if (month == 6)
            result = "Jun.";
        else if (month == 7)
            result = "Jul.";
        else if (month == 8)
            result = "Aug.";
        else if (month == 9)
            result = "Sep.";
        else if (month == 10)
            result = "Oct.";
        else if (month == 11)
            result = "Nov.";
        else if (month == 12)
            result = "Dec.";
        return result;
    }

    //Method that determines if this ice time is in the saved game window, returns true if it is.
    public Boolean isInGameWindow(AllocationSettings settings)
    {
        Boolean result = false;
        //Determine which game window to use [weekday || weekend]
        if (date.DayOfWeek == System.DayOfWeek.Saturday || date.DayOfWeek == System.DayOfWeek.Sunday)
        {

            DateTime weekendStart = DateTime.Parse(settings.getGwWeekEndStart());
            DateTime weekendFinish = DateTime.Parse(settings.getGwWeekEndFinish());

            //Adds the start and end time in the settings to the current date
            //to create a comparable object for our current start/end times
            weekendStart = blendDates(date, weekendStart);
            weekendFinish = blendDates(date, weekendFinish);

            if (weekendStart.CompareTo(start) <= 0 && weekendFinish.CompareTo(end) >= 0)
                result = true;
        }
        else
        {
            DateTime weekdayStart = DateTime.Parse(settings.getGwWeekDayStart());
            DateTime weekdayFinish = DateTime.Parse(settings.getGwWeekDayFinish());

            weekdayStart = blendDates(date, weekdayStart);
            weekdayFinish = blendDates(date, weekdayFinish);

            if (weekdayStart.CompareTo(start) <= 0 && weekdayFinish.CompareTo(end) >= 0)
                result = true;
        }

        return result;
    }

    //Blend dates adds the stored gamewindow time to the date of this ice time to have a DateTime comparable to this ices start and finish time.
    private DateTime blendDates(DateTime dayOf, DateTime timeAt)
    {
        DateTime result = dayOf;
        result = result.AddHours(timeAt.Hour).AddMinutes(timeAt.Minute);
        return result;
    }


    //Detects if the data in this object conflicts with any other ice times in the database
    public Boolean hasConflict()
    {
        Boolean result = true; //default = true, dont allow entry unless confirmed non conflict
        int rows = 0;
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);


            // Count all ice times (rows) that are at this location where
            // Our start time is between the rows start and end  OR
            // Our end time is between the rows start and end OR
            // Our start is after the rows start and our end is before the rows end (our ice time is within the rows ice time) OR
            // Our start is berfore the rows start and our end is after the rows end (the rows ice time is within the our ice time)
            SqlCommand sqlCmdConflicts = new SqlCommand("SELECT COUNT(*) FROM IceTimes WHERE IceId != @IceId AND LocationId=@LocationId AND ((StartTime>@StartTime AND StartTime<@EndTime) OR (EndTime>@StartTime AND EndTime<@EndTime) OR (StartTime<=@StartTime AND EndTime>=@EndTime) OR (StartTime>=@StartTime AND EndTime<=@EndTime))", con);
            sqlCmdConflicts.CommandType = CommandType.Text;


            //Assign Parameters
            SqlParameter sqlLocationId = new SqlParameter("LocationId", locationId);
            sqlCmdConflicts.Parameters.Add(sqlLocationId);

            SqlParameter sqlStartTime = new SqlParameter("StartTime", start);
            sqlCmdConflicts.Parameters.Add(sqlStartTime);

            SqlParameter sqlEndTime = new SqlParameter("EndTime", end);
            sqlCmdConflicts.Parameters.Add(sqlEndTime);

            SqlParameter sqlIceId = new SqlParameter("IceId", iceId);
            sqlCmdConflicts.Parameters.Add(sqlIceId);


            //Open connection, run the command
            con.Open();
            SqlDataReader reader = sqlCmdConflicts.ExecuteReader();


            //Select first row
            reader.Read();
            //Check if item is null
            if (!reader.IsDBNull(0))
            {
                //assign the row int the result of our count command
                rows = reader.GetInt32(0);
            }

            con.Close();

            //if the result is 0, then there are no conflicts.
            if (rows == 0)
            {
                result = false;
            }
        }
        catch (SqlException ex)
        {
            // If there is a connection problem, can't confirm no conflicts leave result as true. 
            // Attempts to prevent actually trying to insert ice when there has been a connection problem.
        }
        return result;
    }


    //--------Getters-------------------------------------------

    public int getIceId()
    {
        return iceId;
    }
    public string getIceType()
    {
        return iceType;
    }
    public string getLocationString()
    {
        return locationStr;
    }
    public DateTime getStart()
    {
        return start;
    }
    public DateTime getEnd()
    {
        return end;
    }
    public DateTime getDate()
    {
        return date;
    }
    public int getTeamId()
    {
        return teamId;
    }
    public int getDuration()
    {
        return duration;
    }
    public int getLocationId()
    {
        return locationId;
    }
    public Boolean getUnassigned()
    {
        return unassigned;
    }

    //--------SETTERS-------------------------------------------
    public void setIceType(string iceType)
    {
        this.iceType = iceType;
    }
    public void setStart(DateTime startTime)
    {
        start = startTime;
    }
    public void setEnd(DateTime endTime)
    {
        end = endTime;
    }
    public void setDate(DateTime date)
    {
        this.date = date;
    }
    public void setTeamId(int teamId)
    {
        this.teamId = teamId;
    }
    public void setDuration(int dur)
    {
        duration = dur;
    }
    public void setLocationId(int locId)
    {
        locationId = locId;
    }
    public void setUnassigned(Boolean unassigned)
    {
        this.unassigned = unassigned;
    }
}