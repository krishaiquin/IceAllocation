using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This constants class can be alter at will to change minor behaviors of the application.
/// 
/// Author: Jonathan
/// </summary>
public class Constant
{
   
    //---------------EDITABLES---------------------------
    public const int MINICETIME = 60; //Minimum allowed IceTime in minutes, must be multiple of 15.
    public const int MAXICETIME = 180; //Maximum allowed IceTime in minutes, must be multiple of 15.

    public const int MAXICEENTRY = 40; //Maximum number of ice times that can be created at a time.

    public const int ROLLOVER_INTERVAL = 15 * 60 * 1000;  //Time between rollover checks [minutes * seconds * milliseconds], originally 15 minutes


    //Strings to use when notifying the team of their account being created
    public const string NEW_TEAM_SUBJECT = "Your team has been added to the Ice Allocation system";
    public const string NEW_TEAM_OPENING = "Welcome to the Ice Allocation system.";
    public const string NEW_TEAM_MESSAGE = "You have been given access to an account with the following credentials: ";

    //Strings to use when notifying teams that new ice is available
    public const string NOTIFY_OTHERS_SUBJECT= "New Ice is available for Request.";
    public const string NOTIFY_OTHERS_MESSAGE="The following ice has been returned and is available for request: " ;

    //Strings to use when notifying the allocator that Ice has been returned
    public const string NOTIFY_RETURN_MESSAGE= "The following ice has been returned by: ";
    public const string NOTIFY_RETURN_SUBJECT = "Returned ice.";

    //Strings to use when notifying the allocator that Ice has been returned
    public const string NOTIFY_REQUEST_MESSAGE = "The following ice has been requested by: ";
    public const string NOTIFY_REQUEST_SUBJECT = "Ice Requested.";

    public const string NOTIFY_ASSIGN_SUBJECT = "You have received ice.";
    public const string NOTIFY_REJECT_SUBJECT = "Request rejected.";
}
