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
using System.Data;
using System.Data.SqlClient;

public partial class Admin_Requests : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        lblReturnMessage.Text = (string)(Session["comment"]);
    }
    protected void ReturnedIce_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Verify what the source of the command is.
        if (e.CommandName == "RequestDetails")
        {
            //Retrieves the ice id value from the data key table[rowindex, rowvalue] 
            //by using the row index that was given as an argument to the button
            int index = int.Parse(e.CommandArgument.ToString());
            int iceId = int.Parse(gridReturnedIce.DataKeys[index].Value.ToString());
            Session["IceIdDetails"] = iceId;
            
            Response.Redirect("~/ReqDetail");
            
        }
        
    }

    protected void gridClosedRequests_DataBound(object sender, EventArgs e)
    {
        string whichControl = ((Control)sender).ID;

        switch (whichControl){
            case "gridClosedRequests":
                format(gridClosedRequests);
                break;
            case "gridReturnedIce":
                format(gridReturnedIce);
                break;

        }
    }


    private void format(GridView gridview) { 
        
        foreach (GridViewRow row in gridview.Rows)
        {
            //--------stores the value that needed to format------
            String startTime = row.Cells[3].Text.Trim();
            String endTime = row.Cells[4].Text.Trim();
            String date = row.Cells[2].Text.Trim();
            String returnDate = row.Cells[7].Text.Trim();

            //------change those values to the right format--------
            row.Cells[3].Text = DateTime.Parse(startTime).ToString("HH:mm");
            row.Cells[4].Text = DateTime.Parse(endTime).ToString("HH:mm");
            row.Cells[2].Text = DateTime.Parse(date).ToString("ddd, d MMM");
            row.Cells[7].Text = DateTime.Parse(returnDate).ToString("ddd, d MMM");
        }
    }
    
}