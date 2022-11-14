using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Testing_TestIceTimes : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGetIceTime_Click(object sender, EventArgs e)
    {
        AllocationSettings y = new AllocationSettings();
        y.getSettings();
        IceTime x = new IceTime(int.Parse(txtIceId.Text));
        lblReturnMsg.Text = x.ToString();
        lblFullMsg.Text = x.fullToString();
        cbxGameWindow.Checked = x.isInGameWindow(y);

        if (x.hasConflict())
        {
            lblHasConflict.Text = "Conflicting";
        }
        else
        {
            lblHasConflict.Text = "Not Conflicting";
        }
    }

  
}