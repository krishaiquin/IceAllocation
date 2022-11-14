using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration.Provider;

public partial class Admin_CreateTeam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            errMsg.Text = "something good";
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //check if team exists by looking for a team id for the username, 0 is an invalid team id and is returned when the team does not exist
        if (Team.findTeamId(txtTeam.Text) == 0)
        {

            try
            {

                MembershipCreateStatus status = new MembershipCreateStatus();
                Guid id = Guid.NewGuid();

                string teamName = txtTeam.Text;
                string pass = txtConfirmPass.Text;
                string coachemail = txtCoachEmail.Text;

                //placeholder security question and answer
                string secQ = "What league are we in?";
                string secA = "Seafair";

                //Create the user in the aspnet_Users and aspnet_Membership tables, this is what allows log in.
                Membership.CreateUser(teamName, pass, coachemail, secQ, secA, true, id, out status);

                //errMsg.Text = " ID: " + id.ToString() + " status: " + status.ToString();


                //Add the new user to the Team role. 
                //The Roles function requires the usernames to add to be in an array
                //string str = txtTeam.Text;
                string[] users = new string[] { teamName };

                Roles.AddUsersToRole(users, "Team");


                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);
                con.Open();


                //---------------------------------------------
                // Parameters for adding to teams table
                SqlParameter userName = new SqlParameter("@UserName", SqlDbType.NVarChar, 256, "UserName");
                userName.Value = teamName;

                SqlParameter coachEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 256, "Email");
                coachEmail.Value = txtCoachEmail.Text;

                SqlParameter userID = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, 16, "UserID");
                userID.Value = id;

                SqlParameter managerEmail = new SqlParameter("@ManagerEmail", SqlDbType.NVarChar, 256, "Email");
                managerEmail.Value = txtManEmail.Text;

         

                //--------------------------------------------------
                //Add to teams table

                SqlCommand createTeam = new SqlCommand("INSERT INTO Teams(UserID,UserName, CoachEmail, ManagerEmail,GameBalance, PracticeBalance, WhoToEmail, ReceiveNotifications) VALUES (@UserID, @UserName, @Email, @ManagerEmail, 0,0,3,'true');", con);
                createTeam.Parameters.Add(userID);
                createTeam.Parameters.Add(coachEmail);
                createTeam.Parameters.Add(managerEmail);
                createTeam.Parameters.Add(userName);
    

                createTeam.ExecuteNonQuery();
                con.Close();

                errMsg.Text = teamName +" was successfully created.";

                List<string> emails = new List<string>();
                emails.Add(txtCoachEmail.Text);
                emails.Add(txtManEmail.Text);

                //Create an email to notify the team that they now have an account.
                IceEmail notifyTeam = new IceEmail(emails);

                notifyTeam.addSubject(Constant.NEW_TEAM_SUBJECT);
                notifyTeam.setMessage(Constant.NEW_TEAM_OPENING + Environment.NewLine + Environment.NewLine 
                    + Constant.NEW_TEAM_MESSAGE + Environment.NewLine 
                    +"Username: " + teamName + Environment.NewLine 
                    + "Password: " + pass + Environment.NewLine + Environment.NewLine 
                    + "Please change your password at the earliest convenience to secure your account.");
                
                lblEmailResult.Text= "Notification of the teams creation "+ notifyTeam.send();
            }
            catch (MembershipCreateUserException ex)
            {
                errMsg.Text = GetErrorMessage(ex.StatusCode);
            }
            catch (SqlException ex)
            {
                //TODO: insert something to rollback the create user/add user to role.
                errMsg.Text = ex.Message;
            }
            catch (HttpException ex)
            {
                errMsg.Text = ex.Message;
            }
        }
        else
        {
            errMsg.Text = "A team with that name exists.";
        }
    }


    public string GetErrorMessage(MembershipCreateStatus status)
    {
        switch (status)
        {
            case MembershipCreateStatus.DuplicateUserName:
                return "Username already exists. Please enter a different user name.";

            case MembershipCreateStatus.DuplicateEmail:
                return "A username for that e-mail address already exists. Please enter a different e-mail address.";

            case MembershipCreateStatus.InvalidPassword:
                return "The password provided is invalid. Please enter a valid password value.";

            case MembershipCreateStatus.InvalidEmail:
                return "The e-mail address provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidAnswer:
                return "The password retrieval answer provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidQuestion:
                return "The password retrieval question provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidUserName:
                return "The user name provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.ProviderError:
                return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            case MembershipCreateStatus.UserRejected:
                return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            default:
                return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
        }
    }


    protected void duplicateNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (Team.findTeamId(txtTeam.Text) == 0);
    }
}