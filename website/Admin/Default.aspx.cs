using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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