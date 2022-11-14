using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Testing_TestTeam : System.Web.UI.Page
{

    Team team;
    int teamId = 17;
    protected void Page_Load(object sender, EventArgs e)
    {
        team = new Team(teamId);
        lblTeamInfo.Text = team.ToString();
        
    }

    protected void btnIG_Click(object sender, EventArgs e)
    {
        team.increaseGame();
        lblTeamInfo.Text = team.ToString();
        DetailsView1.DataBind();
    }
    protected void btnDG_Click(object sender, EventArgs e)
    {
        team.decreaseGame();
        lblTeamInfo.Text = team.ToString();
        DetailsView1.DataBind();
    }
    protected void btnDP_Click(object sender, EventArgs e)
    {
        team.decreasePractice();
        lblTeamInfo.Text = team.ToString();
        DetailsView1.DataBind();
    }
    protected void btnIP_Click(object sender, EventArgs e)
    {
        team.increasePractice();
        lblTeamInfo.Text = team.ToString();
        DetailsView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lblTeamId.Text = Team.findTeamId(TextBox1.Text).ToString();
    }
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        if (team.getNotifications().Equals("true"))
            team.setNotifications("false");
        
        else
            team.setNotifications("true");

        team.save();
        team = new Team(teamId);
        lblNotify.Text = team.getWhoToEmail().ToString();
        DetailsView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        List<string> emails = Team.getAllContactInfo();
        foreach (string email in emails)
        {
            ListBox1.Items.Add(email);
        }
    }
    protected void btnCoach_Click(object sender, EventArgs e)
    {
        team.setCoachEmail(txtCoachEmail.Text);
        team.save();
        team = new Team(teamId);
        lblcoach.Text = team.getCoachEmail();
    }
}