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

public partial class Admin_Teams : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void teams_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //get the necessary information when button is clicked
        if (e.CommandName == "seeDetails")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            int teamId = int.Parse(teams.DataKeys[index].Values["TeamId"].ToString());

            Session["team"] = teamId;

            Response.Redirect("~/EditTeam");
        }
        else {

            
        }
    }


    protected void teams_DataBound(object sender, EventArgs e)
    {
        //chaging the display of values in the table
        foreach (GridViewRow row in teams.Rows)
        {
            String contact = row.Cells[2].Text.Trim();

            switch (contact)
            {
                case "1":
                    row.Cells[2].Text = "Coach Only";
                    break;
                case "2":
                    row.Cells[2].Text = "Manager Only";
                    break;
                case "3":
                    row.Cells[2].Text = "Contact Both";
                    break;

            }
        }
    }
}