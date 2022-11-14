/*
 * Author: Krisha
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Team_IceHistory : System.Web.UI.Page
{
    String userName;
    int userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        userName = User.Identity.Name;
        userId = Team.findTeamId(userName);

        Label1.Text = userId.ToString();

    }
    protected void gridView_DataBound(object sender, EventArgs e)
    {
        string whichControl = ((Control)sender).ID;

        switch (whichControl){
            case "currentlyRequested":
                format(currentlyRequested);
                break;
            case "acceptedRequest":
                format(acceptedRequest);
                break;
            case "rejectedRequest":
                format(rejectedRequest);
                break;
        }
    }

    private void format(GridView gridview) { 
        
        foreach (GridViewRow row in gridview.Rows)
        {
            //--------stores the value that needed to format------
            String startTime = row.Cells[2].Text.Trim();
            String endTime = row.Cells[3].Text.Trim();
            String date = row.Cells[1].Text.Trim();
            

            //------change those values to the right format--------
            row.Cells[2].Text = DateTime.Parse(startTime).ToString("HH:mm");
            row.Cells[3].Text = DateTime.Parse(endTime).ToString("HH:mm");
            row.Cells[1].Text = DateTime.Parse(date).ToString("ddd, d MMM");
        }
    }
    protected void returnedIce_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in returnedIce.Rows)
        {
            //--------stores the value that needed to format------
            String date = row.Cells[1].Text.Trim();


            //------change those values to the right format--------
            row.Cells[1].Text = DateTime.Parse(date).ToString("ddd, d MMM");
        }
    }
}