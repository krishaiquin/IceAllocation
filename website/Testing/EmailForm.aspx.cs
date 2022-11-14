
using System;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Create the msg object to be sent
            MailMessage msg = new MailMessage();
            //Add your email address to the recipients
            msg.To.Add(txtTo.Text);
            //Configure the address we are sending the mail from
            MailAddress address = new MailAddress("zalin.drelan@gmail.com");
            msg.From = address;
            //Append their name in the beginning of the subject
            msg.Subject = txtName.Text + " :  " + ddlSubject.Text;
            msg.Body = txtMessage.Text;

            ////Configure an SmtpClient to send the mail.
            //SmtpClient client = new SmtpClient("relay-hosting.secureserver.net",25);
            //client.EnableSsl = false; //only enable this if your provider requires it
            ////Setup credentials to login to our sender email address ("UserName", "Password")
            //NetworkCredential credentials = new NetworkCredential("admin@drelan.ca", "Project3900!");
            //client.Credentials = credentials;
            // The important part -- configuring the SMTP client

            SmtpClient client = new SmtpClient("smtp.gmail.com",587);
            //client.Port = 465;   // [1] You can try with 465 also, I always used 587 and got success
            client.EnableSsl = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
            client.UseDefaultCredentials = false; // [3] Changed this
            client.Credentials = new NetworkCredential("zalin.drelan@gmail.com", "lfoqucgglnimxkhv");  // [4] Added this. Note, first parameter is NOT string.
            //client.Host = ;  

            //Send the msg
            client.Send(msg);

            //Display some feedback to the user to let them know it was sent
            lblResult.Text = "Your message was sent!";

            //Clear the form
            txtTo.Text = "";
            txtName.Text = "";
            txtMessage.Text = "";
        }
        catch
        {
            //If the message failed at some point, let the user know
            lblResult.Text = "Your message failed to send, please try again.";
        }
    }
}
