using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestSettings : System.Web.UI.Page
{
    AllocationSettings settings;
    protected void Page_Load(object sender, EventArgs e)
    {
        settings = new AllocationSettings();
        settings.getSettings();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        settings.setGWWeekDayFinish(TextBox1.Text);
        Label1.Text = settings.getGwWeekDayFinish();
        Label2.Text= settings.saveSettings().ToString();
        settings.getSettings();
        Label3.Text=settings.getGwWeekDayFinish();

    }
}