/*
 *Author: Krisha 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RecoverPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
    {
        //get the username appeared on the textbox
        String username = PasswordRecovery1.UserName.ToString();
        

        if (Roles.GetRolesForUser(username).Contains("Team")) {
            //make it a Team object
            Team teamName = new Team(username);
            //add the manager's email to list of receivers. 
            e.Message.CC.Add(teamName.getManagerEmail());
        
        }
    }
}