<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminHelp.aspx.cs" Inherits="Admin_AdminHelp" MasterPageFile="~/MasterPage/MasterPage.master"%>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">



    <div>
        <h3>Contents</h3>

        <ol class="contentLinks">
            <li><a href="#setup">Setting up a New Season</a></li>
            <li><a href="#createTeam">Creating Teams</a></li>
            <li><a href="#createIce">Creating Ice</a></li>
            <li><a href="#assignIce">Assigning Ice</a></li>
            <li><a href="#reviewing">Reviewing</a></li>
            <li><a href="#appSettings">Application Settings</a></li>
        </ol>
    </div>
    
    <div id="setup" class="helpItem">
        <h3>Setting up a New Season</h3>

        At the end of a season, the administrator may want to clear/reset all data (ice-times, requests, returned ice times, etc). The reason for this may be to prepare for an upcoming season.

        <h4>To Reset the Season:</h4>

            <ol>
                <li>Click on the <strong>Settings</strong> menu in the navigation bar.</li>
                <li>Click on <strong>Reset Season</strong>, the reset season page will be displayed.</li>
                <li>Click the <strong>Clear Season</strong> button to clear all data (ice times, requests, returned ice, etc) from the season.</li>
                <li>Click the <strong>Confirm</strong> button to confirm that all data should be cleared.</li>
            </ol>

        Before any new data is to be created (ie: ice times, teams, etc.), locations of specific rinks must be created first. The administrator will not be able to create ice times before creating rink locations.

        <h4>To Create Locations:</h4>

            <ol>
                <li>Click on the <strong>Data Management</strong> menu in the navigation bar.</li>
                <li>Click on <strong>Create Location</strong>s to view the create location page.</li>
                <li>Fill out the text box under the <strong>Location</strong> and <strong>Rink</strong> columns. (ie: Location = Facility, Rink = the specific rink)</li>
                <li>Click the <strong>Insert</strong> button to enter the new location.</li>
            </ol>

        <h4>To Edit Locations:</h4>

            <ol>
                <li>Click on the <strong>Data Management</strong> menu in the navigation bar.</li>
                <li>Click on <strong>Edit Location</strong> to view the create location page.</li>
                <li>Click the <strong>Edit</strong> button beside the location that needs editing, then make your changes.</li>
                <li>Click the <strong>Update</strong> button to update the location information.</li>
            </ol>
    </div>

    <div id="createTeam" class="helpItem">

        <h3>Creating/Editing teams</h3>

        When logged in as an administrator, you will have the ability to control what data will be available within the web application. One of the main functionalities of the application is the ability to create and edit teams.
        Once you have logged in as an admin, the navigation bar near the top of the page consists of several headings such as, Allocate Ice, Data Management, and Settings, these menus provide the ability to edit and input new data in the application

        <h4>To Create a Team:</h4>

            <ol>
                <li>Click on the <strong>Data Management</strong> menu in the navigation bar.</li>
                <li>Click on the <strong>Create Teams</strong> menu.</li>
                <li>The <strong>Create Teams</strong> page is displayed, here you will input information corresponding to the particular team you want to add:</li>
                    <ol type="a">
                        <li><strong>Team</strong> - The team name to be displayed.</li>
                        <li><strong>Password</strong> - The password to be created and used by the team manager or coach.</li>
                        <li><strong>Confirm Password</strong> - Confirmation of the password to be created.</li>
                        <li><strong>Coach Email</strong> - The coach’s email for receiving updates and information about ice times.</li>
                        <li><strong>Manager Email</strong> - The team manager’s email for receiving updates and information about ice times.</li>        
                    </ol>
                <li>Once all data has been entered, click the <strong>Submit</strong> button to create the team.</li>
            </ol>

        <h4>To Edit a Team:</h4>

            <ol>
                <li>Click on the <strong>Data Management</strong> menu in the navigation bar.</li>
                <li>Click on the <strong>Edit Teams</strong>. The <strong>Edit Teams</strong> page is displayed, here you will see a list of teams that have been created and whether or not the team is enabled.</li>
                <li>To view a team’s information, click the button titled <strong>See Details</strong>.</li>
                <li></li>A detailed display of the team will be shown</li>.
                    <ol type="a">
                        <li>Here you can edit the <strong>Coach</strong> and <strong>Manager</strong> email and select the <strong>Preferred Contact</strong> method:</li>
                        <li>The<strong> Disable Team</strong> button disables the login capability of the team.</li>
                    </ol>
                <li>Once changes are made (if applicable), click the <strong>Submit</strong> button to confirm.</li>
            </ol>
    </div>

    <div id="createIce" class="helpItem">

        <h3>Creating/Editing Ice</h3>

        As an administrator, you have the ability to create and manage ice times available for the association. These ice times are viewable by all teams but you may also be able to assign a specific ice time for a specific team.

        <h4>To Create Ice Time(s):</h4>

        <ol>
            <li>Click on the <strong>Data Management</strong> menu in the navigation bar.</li>
            <li>Click on <strong>Create Ice Times</strong>, the <strong>Create Ice</strong> page is displayed.</li>
            <li>On this page, you will see the following:</li>
                <ol type="a">
                    <li><strong>Date</strong> - A calendar is displayed to allow the administrator to select a specific date for the ice time. You can change the month by clicking on the arrows to the left and to the right of the month. If you’re adding a weekly schedule, select the first date.</li>
                    <li><strong>Location</strong> - The dropdown menu displays the locations of rinks. To view more available options, click the arrow.</li>
                    <li><strong>Start Time</strong> - The start time for the ice time slot. To view more available options, click the arrow.</li>
                    <li><strong>End Time</strong> - The end time for the ice time slot. The end time is automatically set so the duration will be no less than 1 hour and no more than 2 hours.</li>
                    <li><strong>Duration</strong> - The duration of the ice time slot.</li>
                    <li><strong>Ice Type</strong> - The type of ice time. Select either “Game” or “Practice”.</li>
                    <li><strong>Assigned To</strong> - Displays the available teams to be assigned to, or a checkbox for unassigned.</li>
                    <li><strong>Number of Ice Times</strong> - Select the number of ice times to be created. A value of 1 will create one ice time on the date selected, where a value of 4 create 4 ice times starting from the date selected.</li>
                </ol>
            <li>Once all fields are entered, click the <strong>Generate List</strong> button, a list of ice times will be generated to display ice time on a weekly basis. <strong>Uncheck</strong> the checkbox to not create the ice time on that date. If there are conflicts with any ice times, they will be shown.</li>
            <li>Click the <strong>Submit</strong> button to confirm the creation of ice times. A confirmation of how many ice times are created will be shown.</li>
        </ol>

        <h4>To Edit Ice Time(s):</h4>

        <ol>
            <li>Click on the<strong> Data Management</strong> menu in the navigation bar.</li>
            <li>Click on <strong>Edit Ice</strong>, the <strong>Edit Ice </strong>page will be displayed. This page displays all ice times that have been created.</li>
            <li>To edit a particular ice time, click the <strong>Edit</strong> button for the specific ice time.</li>
            <li>The <strong>Ice Maintenance</strong> page will be displayed, the options available are similar to the <strong>Create Ice</strong> page.</li>
            <li>Once all changes have been made, click the <strong>Submit</strong> button to update changes.</li>
        </ol>
    </div>

    <div id="assignIce" class="helpItem">

        <h3>Assigning Ice</h3>

        As an administrator, you have the ability to approve ice time requests and assign these requests to specific teams.

        <h4>To Assign Ice:</h4>

        <ol>
            <li>Click on the <strong>Allocate Ice</strong> menu in the navigation bar.</li>
            <li>Click on <strong>Requested Ice</strong>. The page that is displayed is a list of all the ice times that have been created and a column titled “<strong>Requests</strong>”, this column displays the number of requests available for that specific ice time.</li>
            <li>Click on the <strong>See Requests</strong> button next to the ice time that have “<strong>Requests</strong>”. The <strong>Request Details</strong> page will be displayed.</li>
            <li>The administrator will be able to view all teams who have requested a particular ice time.</li>
            <li>To assign the ice to a specific team, click on the <strong>Assign</strong> button next to the team you want to assign the ice to.</li>
            <li>There is also a <strong>Comment Box</strong> that is available to insert a comment regarding the assigned ice.</li>
        </ol>
    </div>

    <div id="reviewing" class="helpItem">

        <h3>Reviewing</h3>

        There may be times where the administrator will be required to review ice times that have been returned. The ability to to enable or disable reviewing of returned ice is important to maintain control over how many ice times a team is able to return.

        <h4>To Review Ice Times:</h4>

        <ol>
            <li>Click on the <strong>Allocate Ice</strong> menu in the navigation bar.</li>
            <li>Click on <strong>Ice Approval</strong>. The <strong>Review Ice</strong> page will be displayed.</li>
            <li>There will be a list of all returned ice that needs approval. (note: the <strong>Enable Reviewing</strong> function must be <strong>Enabled</strong>; to enable review, see how-to below).</li>
            <li>Select the returned ice that needs to be approved by clicking the <strong>Checkbox</strong> on the right hand side.</li>
                <ol type="a">
                    <li>Select the specific action to take (<strong>Approve Return</strong>, <strong>Assign to Team</strong>, or <strong>Drop Ice Time</strong>).</li>
                </ol>
            <li>Once the selection has been made, click the <strong>Approve Selected </strong>button to complete the changes.</li>
        </ol>
            
        <h4>To Enable or Disable Reviewing of Ice Times:</h4>

        <ol>
            <li>Click on the <strong>Allocate Ice</strong> menu in the navigation bar.</li>
            <li>Click on <strong>Ice Approval</strong>. The<strong> Review Ice</strong> page will be displayed.</li>
            <li>Under where it says “<strong>Enable Reviewing</strong>”, select the corresponding option to either <strong>Enable</strong> or <strong>Disable Reviewing of Ice Times</strong>.</li>
            <li>Click Confirm to save changes.</li>
        </ol>
    </div>           
            
    <div id="appSettings" class="helpItem">
     
        <h3>Application Settings</h3>

        As an administrator, you will have control of the overall functionality of the web application. There may be times where you need to shutdown the website or change certain settings. Furthermore, the ability to provide announcements on the web application is key to let teams know of any important information relating to the season.

        <h4>To Access the Application Settings Page:</h4>

        <ol>
            <li>Click on the <strong>Settings</strong> menu in the navigation bar.</li>
            <li>Click on <strong>Application Settings</strong>. The <strong>Application Settings</strong> page will be displayed.</li>
                <li>Here, the you are able to alter the settings of the web application.</li>
                <ol type="a">
                    <li><strong>Shutdown Site</strong> - enable or disable all team interaction with the site.</li>
                    <li><strong>Team Scheduling</strong> - enable or disable the team's ability to enter new ice times.<br />*This should be disabled after the start of the season.</li>
                    <li><strong>Enable Reviewing</strong> - enable or disable reviewing of ice times.</li>
                    <li><strong>Game Window</strong> - Sets the period for when games are only to be created.</li>
                    <li><strong>Roll Overs</strong> - The amount of time prior to start where an unassigned ice tpe will switch to the next level.</li>
                    <li><strong>Logo URL</strong> - The URL for the banner/logo of the site.</li>
                    <li><strong>Public Contact Info</strong> - The email that will be displayed on the bottom of the site for contact purposes.</li>
                    <li><strong>Update Announcement</strong> - The announcement that will be displayed when any team logs in.</li>
                    <li><strong>Ice Assigned Email Template</strong> - The email template used when a team is assigned an ice time.</li>
                    <li><strong>Ice Rejected Email Template</strong> - The email template used when a team is rejected an ice time.</li>
                </ol>
            <li>When a change has been made, click the <strong>Submit</strong> button on the bottom of the page.</li>
        </ol>
    </div>
</asp:Content>