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
using System.Configuration.Provider;
using System.Web.Security;

public partial class AdminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Default");
            }
            else
            {
                Response.Redirect("~/Home");
            }
        }
    }


}