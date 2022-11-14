/*
 * team/Settings.aspx.cs manages the user's profile(email and password). 
 * Author: Krisha
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class Team_Settings : System.Web.UI.Page
{
    //Create a connection
    SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
    String logInUser;
    String pass;
    MembershipUser user;

    //Team Object to store team data
    Team team;

    protected void Page_Load(object sender, EventArgs e)
    {
        //stores the name of the current user
        logInUser = User.Identity.Name.ToString();

        //store the password of the current user.
        user = Membership.GetUser(logInUser);
        pass = user.GetPassword();


        //Load the team data based on who is logged in.
        team = new Team(User.Identity.Name);

        if (!IsPostBack) {

            ddlWhoToEmail.SelectedValue = team.getWhoToEmail().ToString();
            txtCoachEmail.Text = team.getCoachEmail();
            txtManEmail.Text = team.getManagerEmail();

            rblReceiveNotifications.SelectedValue = team.getNotifications();
        
        }
        

    }

    //button even that saves the changes that have been created/modified
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //updates the team table. it saves the emails and the preferred contact
        update();

        //update the password    
        if (txtNewPass.Enabled && txtConfirmPass.Enabled)
        {
            user = Membership.GetUser(logInUser);
            user.ChangePassword(user.GetPassword(), txtNewPass.Text);
            user.Email = txtCoachEmail.Text;
            Membership.UpdateUser(user);

        }

    }

    //method that updates the team table
    private void update()
    {
        try
        {

            team.setCoachEmail(txtCoachEmail.Text);
            team.setManagerEmail(txtManEmail.Text);
            team.setWhoToEmail(int.Parse(ddlWhoToEmail.SelectedValue));
            team.setNotifications(rblReceiveNotifications.SelectedValue);

            lblResult.Text = team.save();

            //update the team's email in membership table;
            user = Membership.GetUser(logInUser);
            user.Email = txtCoachEmail.Text;
            Membership.UpdateUser(user);

            lblResult.Text += "   Changes saved!";

        }

        catch (SqlException ex)
        {
            lblResult.Text = ex.Message;
        }
    }

    /*
     *  cannot change password if user doesnt know the current password
     * */
    protected void txtCurrent_TextChanged(object sender, EventArgs e)
    {

        if (!txtCurrent.Text.Equals(pass))
        {
            lblWarning.Text = "Password entered does not match current password.";
            txtNewPass.Enabled = false;
            txtConfirmPass.Enabled = false;
            newPassRequired.Enabled = false;
            confirmRequired.Enabled = false;
        }
        else
        {
            lblWarning.Text = "";
            txtNewPass.Enabled = true;
            txtConfirmPass.Enabled = true;
            newPassRequired.Enabled = true;
            confirmRequired.Enabled = true;
        }
    }

}