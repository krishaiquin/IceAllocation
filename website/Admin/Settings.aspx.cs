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
using System.Web.Security;

public partial class Admin_Settings : System.Web.UI.Page
{
    String userName;
    MembershipUser admin;
    String pass;

    protected void Page_Load(object sender, EventArgs e)
    {
        //keeps value of current password textbox after postback
        currentPass.Attributes.Add("value", currentPass.Text);
        //stores the username
        userName = User.Identity.Name.ToString();
        admin = Membership.GetUser(userName);
        //stores the password
        pass = admin.GetPassword();

        //show the current saved email
        if(!IsPostBack)
            txtEmail.Text = admin.Email.ToString();
    }
    
    //saves changes
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        admin.Email = txtEmail.Text;
        Membership.UpdateUser(admin);

        if (txtNewPass.Enabled && txtConfirmPass.Enabled) {
            admin.ChangePassword(admin.GetPassword(), txtNewPass.Text);
            Membership.UpdateUser(admin);
        }
        lblSuccess.Text = "Changes saved!";
    }

    /*
    *  cannot change password if user doesnt know the current password
    * 
     */
    protected void currentPass_TextChanged(object sender, EventArgs e)
    {
        if (!currentPass.Text.Equals(pass))
        {
            lblWarning.Text = "Password entered does not match current password.";
            txtNewPass.Enabled = false;
            txtConfirmPass.Enabled = false;
            confirmRequired.Enabled = false;
            passRequired.Enabled = false;
        }
        else {
            lblWarning.Text = "";
            txtNewPass.Enabled = true;
            txtConfirmPass.Enabled = true;
            confirmRequired.Enabled = true;
            passRequired.Enabled = true;
        }
    }
}