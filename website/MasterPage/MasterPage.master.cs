using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;

public partial class masterpage_MasterPage : System.Web.UI.MasterPage
{
    AllocationSettings settings = new AllocationSettings();

    //Event that happens every time the page is loaded
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            lblName.Text = "Welcome ";
            LoginStatus1.Visible = true;
        }
        else
        {
            lblName.Text = "";
            LoginStatus1.Visible = false;
        }
        
        //Check if we have settings saved, retrieve if we dont
        if (Session["Settings"] == null)
        {
            settings.getSettings();
            Session["Settings"] = settings;

            LiteralControl x = new LiteralControl();
            x.Text = "<link rel=\"stylesheet\" type=\"text/css\" href=\"../Css/StyleSheet.css\" />";
          
           
        }
        else
        {
            settings = (AllocationSettings)Application["Settings"];
        }
        //Set banner to saved url
        imgLogo.ImageUrl = settings.getLogoUrl();

        //If the current user is a team, then load the team id into session data
        if (Session["TeamId"] == null && HttpContext.Current.User.IsInRole("Team"))
            Session["TeamId"] = Team.findTeamId(HttpContext.Current.User.Identity.Name);

        HtmlLink colourCss = new HtmlLink();
            colourCss.Href = "~/Css/"+settings.getTheme();
            colourCss.Attributes.Add("rel", "stylesheet");
            colourCss.Attributes.Add("type", "text/css");
            colourCss.Attributes["media"] = "all";

            Page.Header.Controls.Add(colourCss);
        lblAdminEmail.Text = settings.getAdminEmail()[0];
    }

    //Event that is triggered each time an item is added to the site map menu item
    protected void SiteMapMenu_MenuItemDataBound(object sender, MenuEventArgs e)
    {
        SiteMapNode node = e.Item.DataItem as SiteMapNode;
        //if there is "visible" attribute in the node
        //check the value if true or false.
        if (!string.IsNullOrEmpty(node["visible"]))
        {
            bool isVisible;
            if (bool.TryParse(node["visible"], out isVisible))
            {
                if (!isVisible)
                {
                    if (e.Item.Parent != null)
                        e.Item.Parent.ChildItems.Remove(e.Item);
                    else
                        ((Menu)sender).Items.Remove(e.Item);
                }
            }
        }
    }
}
