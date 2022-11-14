<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>

<script RunAt="server">

    private static System.Timers.Timer rolloverTimer;


    
    /* Application_Start
     * Event handler that is triggered everytime the application starts.
     */
    void Application_Start(object sender, EventArgs e)
    {

        rolloverTimer = new System.Timers.Timer(Constant.ROLLOVER_INTERVAL);
        rolloverTimer.Elapsed += rolloverTimer_Elapsed;
        rolloverTimer.Start();

        RegisterRoutes(System.Web.Routing.RouteTable.Routes);

        Application["Settings"] = new AllocationSettings();
        ((AllocationSettings)Application["Settings"]).getSettings();
        
    }

    /*rollOverTimer_Elapsed Method
     * This method is triggered everytime the timers interval has elapsed.
     */
    void rolloverTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Rollover occuring");
        //Call the Ice Manipulation rollover method.
        IceManipulation.rollover();
    }
    
    /* Application_End
     * Event handler that is triggered everytime the application ends.
     */
    void Application_End(object sender, EventArgs e)
    {
        //No actions needed.
    }


    /* Application_Error
     * Event handler that is triggered everytime the application receives an unhandled Error.
     */
    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        if (!System.Diagnostics.EventLog.SourceExists
            ("ASPNETApplication"))
        {
            System.Diagnostics.EventLog.CreateEventSource
               ("ASPNETApplication", "Application");
        }
        System.Diagnostics.EventLog.WriteEntry
            ("ASPNETApplication",
            Server.GetLastError().Message);

    }

    /* Session_Start
     * Event handler that is triggered everytime a new session starts.
     */
    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        AllocationSettings allocation = new AllocationSettings();
        allocation.getSettings();
        Session["Settings"] = allocation;
    }

    /* Session_End
     * Event handler that is triggered everytime a session ends.
     */
    void Session_End(object sender, EventArgs e)
    {
        //No actions needed.
    }


    /* RegisterRoutes
    * Method that binds friendly urls to the route handler
    * Author: Krisha 
    */
    public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
    {
        routes.Ignore("{resources}.axd/{*pathInfo}");
        routes.MapPageRoute("Log in", "", "~/Login.aspx");
        routes.MapPageRoute("Error", "Error", "~/Error/ErrorPage.aspx");
        routes.MapPageRoute("Not Found", "NotFound", "~/Error/NotFound.aspx");
        //------team side pages----
        routes.MapPageRoute("Home", "Home", "~/Team/Default.aspx");
        routes.MapPageRoute("Create Ice", "TeamIce", "~/Team/CreateIce.aspx");
        routes.MapPageRoute("View Ice History", "IceHistory", "~/Team/IceHistory.aspx");
        routes.MapPageRoute("Settings", "AccountSettings", "~/Team/Settings.aspx");
        routes.MapPageRoute("Team Help", "TeamHelp", "~/Team/TeamHelp.aspx");

        //----admin side pages-----
        routes.MapPageRoute("AdminHome", "Default", "~/Admin/Default.aspx");
        routes.MapPageRoute("Ice Maintenance", "IceMaintenance", "~/Admin/IceMaintenance.aspx");
        routes.MapPageRoute("Requests", "Requests", "~/Admin/Requests.aspx");
        routes.MapPageRoute("ReqDetail", "ReqDetail", "~/Admin/ReqDetail.aspx");
        routes.MapPageRoute("ReviewIce", "ReviewIce", "~/Admin/ReviewIce.aspx");
        routes.MapPageRoute("CreateTeam", "CreateTeam", "~/Admin/CreateTeam.aspx");
        routes.MapPageRoute("CreateIce", "CreateIce", "~/Admin/CreateIce.aspx");
        routes.MapPageRoute("CreateLoc", "CreateLoc", "~/Admin/CreateLoc.aspx");
        routes.MapPageRoute("EditIce", "EditIce", "~/Admin/EditIce.aspx");
        routes.MapPageRoute("Teams", "Teams", "~/Admin/Teams.aspx");
        routes.MapPageRoute("EditTeam", "EditTeam", "~/Admin/EditTeam.aspx");
        routes.MapPageRoute("AdminSettings", "Settings", "~/Admin/Settings.aspx");
        routes.MapPageRoute("AppSettings", "AppSettings", "~/Admin/AppSettings.aspx");
        routes.MapPageRoute("New Season", "NewSeason", "~/Admin/NewSeason.aspx");
        routes.MapPageRoute("Admin Help", "AdminHelp", "~/Admin/AdminHelp.aspx");

        //login pages
        routes.MapPageRoute("Login", "Login", "~/Login.aspx");
        routes.MapPageRoute("AdminLogin", "AdminLogin", "~/AdminLogin.aspx");

        routes.MapPageRoute("RecoverPassword", "RecoverPassword", "~/RecoverPassword.aspx");
            
    }

    //rewrites path.
    //Author: Krisha
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        string path = Request.Path.ToLower();
        if (path.Contains(".aspx") && !path.Contains("login"))
        {
            string segment = Request.Url.Segments.Last().Replace(".aspx", String.Empty);
            Context.RewritePath(String.Concat("~/", segment), false);

        }
    }

</script>
