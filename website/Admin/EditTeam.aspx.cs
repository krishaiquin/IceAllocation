/*
 * Author: Krisha
 * 
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class Admin_EditTeam : System.Web.UI.Page
{
    SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["IceAllocationConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        int teamId = (int)(Session["team"]);
        string team = Team.findTeamName(teamId);
        MembershipUser user = Membership.GetUser(team);

        if (!IsPostBack)
        {

            sqlConnection.Open();
            SqlParameter sqlTeamId = new SqlParameter("@TeamId", SqlDbType.Int);

            sqlTeamId.Value = teamId;

            SqlCommand select = new SqlCommand("SELECT UserName AS 'Team'," +
            "CoachEmail AS 'Coach Email'," +
            "ManagerEmail AS 'Manager Email'," +
            "WhoToEmail AS 'Primary Contact', Teams.GameBalance as 'Game Balance', Teams.PracticeBalance as 'Practice Balance' FROM Teams WHERE TeamId=@TeamId", sqlConnection);
            select.CommandType = CommandType.Text;

            select.Parameters.Add(sqlTeamId);

            SqlDataReader reader = select.ExecuteReader();
            reader.Read();

            lblName.Text = reader["Team"].ToString();
            txtCoachEmail.Text = reader["Coach Email"].ToString();
            txtManEmail.Text = reader["Manager Email"].ToString();
            ddlContact.SelectedValue = reader["Primary Contact"].ToString().Trim();
            lblGame.Text = reader["Game Balance"].ToString();
            lblPrac.Text = reader["Practice Balance"].ToString();

            sqlConnection.Close();

            string btnText = user.IsApproved ? "Disable Team" : "Enable Team";
            btnToggle.Text = btnText;

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            sqlConnection.Open();

            //---------stating the parameters--------
            SqlParameter emailCoach = new SqlParameter("@CoachEmail", SqlDbType.NVarChar);
            SqlParameter manEmail = new SqlParameter("@ManagerEmail", SqlDbType.NVarChar);
            SqlParameter emailWho = new SqlParameter("@WhoToEmail", SqlDbType.NVarChar);
            SqlParameter teamId = new SqlParameter("@TeamId", SqlDbType.Int);

            //-----stating the values of the parameters------
            emailCoach.Value = txtCoachEmail.Text;
            manEmail.Value = txtManEmail.Text;
            emailWho.Value = ddlContact.SelectedValue;
            teamId.Value = (int)(Session["team"]);

            SqlCommand updateTeam = new SqlCommand("UPDATE Teams SET CoachEmail = @CoachEmail, ManagerEmail = @ManagerEmail, WhoToEmail = @WhoToEmail WHERE TeamId = @TeamId", sqlConnection);

            //----adding parameters-----
            updateTeam.Parameters.Add(emailCoach);
            updateTeam.Parameters.Add(manEmail);
            updateTeam.Parameters.Add(emailWho);
            updateTeam.Parameters.Add(teamId);

            //execute then close the connection.
            updateTeam.ExecuteNonQuery();


            sqlConnection.Close();

            lblStatus.Text = "Changes saved!";

        }

        catch (SqlException ex)
        {
            lblStatus.Text = ex.Message;
        }


    }
    protected void ddlContact_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlContact.SelectedValue.Equals("2") || ddlContact.SelectedValue.Equals("3"))
            requiredManEmail.Enabled = true;
    }

    protected void btnToggle_Click(object sender, EventArgs e)
    {
        int teamId = (int)(Session["team"]);
        string team = Team.findTeamName(teamId);
        MembershipUser user = Membership.GetUser(team);
        //value from script that determines if the user has clicked yes or no
        string confirmValue = Request.Form["confirm_value"];
        //checks if the team has already been disable or not, then show the appropriate text for the button
        
        

        // disables the team and change btnToggle's text to enable
        if (btnToggle.Text.Equals("Disable Team"))
        {
            if (confirmValue == "Yes")
                {
                    user.IsApproved = false;
                    Membership.UpdateUser(user);
                    lblStatus.Text="This team has been successfully disabled!";
                    btnToggle.Text = "Enable Team";
                }
        }

        // enables the team and change btnToggle's text to disable
        else if (btnToggle.Text.Equals("Enable Team"))
        {
            if (confirmValue == "Yes")
                {
                    user.IsApproved = true;
                    Membership.UpdateUser(user);
                    lblStatus.Text="This team has been successfully enabled!";
                    btnToggle.Text = "Disable team";
                }
        
        }

        //check if there is ice times owned by the team
       /* sqlConnection.Open();
        SqlParameter sqlTeamId = new SqlParameter("@teamId", SqlDbType.Int);

        sqlTeamId.Value = (int)(Session["team"]);
        SqlCommand select = new SqlCommand("select Count(*) As Count from IceTimes where TeamId=@teamID AND Date > GETDATE()", sqlConnection);
        select.Parameters.Add(sqlTeamId);

        SqlDataReader reader = select.ExecuteReader();
        reader.Read();

        int count = int.Parse(reader["Count"].ToString());

        sqlConnection.Close();

        int teamId = (int)(Session["team"]);


        string confirmValue = Request.Form["confirm_value"];

        if (confirmValue == "Yes")
        {
            deleteTeam(teamId, count, Team.findTeamName(teamId));
            Response.Redirect("~/Admin/Default.aspx");
        }*/

        
    }


   /* protected void deleteTeam(int teamId, int count, string teamName)
    {

       /* try
        {
            sqlConnection.Open();

            //---------stating the parameters--------
            SqlParameter sqlteamId = new SqlParameter("@TeamId", SqlDbType.Int);
            SqlParameter nullTeamId = new SqlParameter("@nullTeam", SqlDbType.Int);
            SqlParameter anotherTeamId = new SqlParameter("@anotherTeamId", SqlDbType.Int);

            //-----stating the values of the parameters------
            sqlteamId.Value = teamId;
            nullTeamId.Value = DBNull.Value;
            anotherTeamId.Value = teamId;

            //if the team has ice, then keep the ice in the database but it will have null team
            if (count > 0)
            {
                SqlCommand nullInIce = new SqlCommand("Update IceTimes SET TeamId=@nullTeam Where TeamId=@anotherTeamId", sqlConnection);
                nullInIce.Parameters.Add(anotherTeamId);
                nullInIce.Parameters.Add(nullTeamId);

                nullInIce.ExecuteNonQuery();

            }

            //----adding parameters-----
            SqlCommand deleteTeam = new SqlCommand("Delete from Teams WHERE TeamId = @TeamId", sqlConnection);
            deleteTeam.Parameters.Add(sqlteamId);

            //execute then close the connection.
            deleteTeam.ExecuteNonQuery();


            sqlConnection.Close();

            //delete team from membership
            Membership.DeleteUser(teamName);



        }

        catch (SqlException ex)
        {
            lblStatus.Text = ex.Message;
        }*/


    //}
}