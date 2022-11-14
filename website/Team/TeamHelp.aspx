<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeamHelp.aspx.cs" Inherits="Team_TeamHelp" MasterPageFile="~/MasterPage/MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">



    <div>
        <h3>Contents</h3>

        <ol class="contentLinks">
            <li><a href="#create">Creating Ice/Schedule</a></li>
            <li><a href="#request">Requesting Ice</a></li>
            <li><a href="#return">Returning Ice</a></li>
            <li><a href="#open">Open Ice</a></li>
            <li><a href="#balance">Game/Practice Balance</a></li>
            <li><a href="#settings">Team Settings</a></li>
        </ol>


    </div>


    <div id="create" class="helpItem">
        <h3>Creating Ice/Schedule</h3>
        <p>At the beginning of the season, you will need to enter your own schedule. You will be able to enter single and weekly ice times. The site will also check for conflicting ice time for the rink and time selected.</p>

        <h4>To create your schedule:</h4>
        <ol>
            <li>Click on <strong>Enter Schedule</strong> on the top navigation bar. You should see a monthly calendar, along with a number of drop down lists.</li>

            <li>Enter the details for the ice time:
                <ol type="a">
                    <li><strong>Date</strong> - Select a date from calendar. You can change the month by clicking on the arrows to the left and to the right of the month. If you’re adding a weekly schedule, select the first date.</li>
                    <li><strong>Location</strong> - Select the location your game will be at. The location will remain the same throughout the ice times.</li>
                    <li><strong>Start Time</strong> - Select the start time.</li>
                    <li><strong>End Time</strong> - Select the end time. The end time is automatically set so the duration will be no less than 1 hour and no more than 2 hours.</li>
                    <li><strong>Ice Type</strong> - Select either game or practice.</li>
                    <li><strong>Number of Ice Times</strong> - Select the number of ice times to be created. A value of 1 will create one ice time on the date selected, where a value of 4 create 4 ice times starting from the date selected.</li>
                </ol>
            </li>
            <li>Click on <strong>Generate List</strong>. You will see a list of ice times to be created. Here you can deselect the days that you cannot use.
            If there are conflicting ice times, they are shown below, and those days will not be created.
            If you need to change any detail, make the change and click <strong>Regenerate List</strong>.</li>
            <li>Confirm the detail and click <strong>Submit</strong> to finalize the creation of the ice times.</li>
        </ol>
    </div>


    <div id="request" class="helpItem">
        <h3>Requesting Ice</h3>

        <p>There will be ice times available for request when teams return their ice time. They will be shown in your home page under <strong>Available Game Ice</strong> and <strong>Available Practice Ice</strong>. You must have a negative balance to be able to request ice times. Game and practice balance is explained in another section.</p>

        <h4>To request an ice time:</h4>
        <ol>
            <li>Find the ice time you wish to request, and click <strong>Request Ice</strong>. There will be a confirmation message below the table of ice times. All ice times requested by you will be highlighted in yellow.</li>
        </ol>

        <h4>To request multiple ice times:</h4>
        <ol>
            <li>Select the checkboxes for the ice times you wish to request, and click <strong>Request Selected</strong>. There will be a confirmation message below the table of ice times.</li>
        </ol>
        <p>Note: If you have game balance of zero, you will not be able to request games. If you have a practice balance of zero, you will not be able to request practices.</p>
    </div>

    <div id="open" class="helpItem">
        <h3>Open Ice:</h3>

        <p>Open ice are ice times that are too late to assign as games or practices, and must be used. If there are available open ice times, they will show up on the top of the home page. You can take open ice slots that can be used by your team, and it will not count against your game or practice balance.</p>
        <p>Ice times that you have returned will show up in the available ice time list table, but you will be unable to request it.</p>
    </div>

    <div id="return" class="helpItem">
        <h3>Returning Ice</h3>

        <p>There may be ice times in your schedule that you cannot use, and will need to return. All your future ice times will be shown under <strong>Your Scheduled Game Ice</strong> and <strong>Your Scheduled Practice Ice</strong>. Your game and practice balance will be changed when ice times are returned.</p>

        <h4>To return an ice time:</h4>
        <ol>
            <li>Find the ice time you wish to request, and click Return Ice. There will be a confirmation message below the table of ice times.</li>
        </ol>
        <h4>To return multiple ice times:</h4>
        <ol>
            <li>Select the checkboxes for the ice times you wish to return, and click Return Selected. There will be a confirmation message below the table of ice times.
            </li>
        </ol>
        <p>Note: You cannot request an ice time that you have returned, so be certain that you want to return the ice. Email the allocator if you need an ice time you have returned.</p>
    </div>

    <div id="balance" class="helpItem">
        <h3>Game/Practice Balance</h3>

        <p>The game and practice balances tracks your returns and requests. If you have a negative balance, that means you are owed ice time.</p>
    </div>

    <div id="settings" class="helpItem">
        <h3>Team Settings</h3>

        <p>This is where you can change the emails for the coach and the manager. Notifications can be sent to both the coach and the manager, or either one.</p>

        <p>You can also change your password here.</p>
    </div>


</asp:Content>
