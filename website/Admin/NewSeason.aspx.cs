using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NewSeason : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];

        if (btnDel.Text.Equals("Clear Season")) {

            if (confirmValue == "Yes") {
                string result = IceManipulation.deleteData();
                lblMsg.Text = result;

                btnDel.Text = "Clear Teams";
                lblInfo.Text = "This will clear all ice teams from the database:";
            }
        }

        else if (btnDel.Text.Equals("Clear Teams"))
        {

            if (confirmValue == "Yes")
            {
                string result = IceManipulation.deleteTeams();
                lblMsg.Text = result;

                btnDel.Text = "Clear Season";
                lblInfo.Text = "This will clear all ice times, returned ice, and requests from the database:";
            }
        }

    }
}