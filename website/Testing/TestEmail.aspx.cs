using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestEmail : System.Web.UI.Page
{

    IceEmail mail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailTest"] != null)
        {
            mail = (IceEmail)Session["EmailTest"];
        }
        else
        {
            mail = new IceEmail();
            Session["EmailTest"] = mail;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblResult.Text = IceEmail.sendGMail(txtTo.Text, txtSubject.Text, txtMessage.Text);
    }

    protected void btnTo_Click(object sender, EventArgs e)
    {
        mail.setReceiver(txtMailTo.Text);
        txtMailTo.Text = "";
        lblNumber.Text = mail.getRecipientsCount().ToString();
    }
    protected void btnBcc_Click(object sender, EventArgs e)
    {
        //mail.addBccReceiver(txtBcc.Text);
        //txtBcc.Text = "";

    }





    protected void btnSubject_Click(object sender, EventArgs e)
    {
        mail.addSubject(txtMailSubject.Text);
        txtMailSubject.Text = "";

    }
    protected void btnMessage_Click(object sender, EventArgs e)
    {
        mail.setMessage(txtMailMessage.Text);
        txtMailMessage.Text = "";

    }
    protected void btnSubmitMail_Click(object sender, EventArgs e)
    {
        Label1.Text = mail.send();
        lblNumber.Text = "" + mail.getRecipientsCount() + ":" + mail.getBccCount();
    }
    //btnOtherSubmitMail_Click

}