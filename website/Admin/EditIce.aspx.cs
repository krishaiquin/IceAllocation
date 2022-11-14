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

public partial class Admin_EditIce : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void iceTimes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "seeDetails")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            int iceId = int.Parse(iceTimes.DataKeys[index].Values["IceId"].ToString());

            Session["iceId"] = iceId;

            Response.Redirect("~/IceMaintenance");

        }

    }
    protected void iceTimes_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void iceTimes_DataBound(object sender, EventArgs e)
    {
        
        foreach (GridViewRow row in iceTimes.Rows)
        {
            //--------stores the value that needed to format------
            String startTime = row.Cells[3].Text.Trim();
            String endTime = row.Cells[4].Text.Trim();
            String date = row.Cells[2].Text.Trim();
            

            //------change those values to the right format--------
            row.Cells[3].Text = DateTime.Parse(startTime).ToString("HH:mm");
            row.Cells[4].Text = DateTime.Parse(endTime).ToString("HH:mm");
            row.Cells[2].Text = DateTime.Parse(date).ToString("ddd, d MMM");
        }
    }
}