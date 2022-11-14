using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error_ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.IsInRole("Team"))
        {
            hlHome.NavigateUrl = "~/Home";
        }
        else if (User.IsInRole("Administrator"))
        {
            hlHome.NavigateUrl = "~/Default";
        }
        else
        {
            hlHome.NavigateUrl = "~/";
        }

    }
}