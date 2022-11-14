using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for IceEmail
/// The IceEmail class makes use of ASP.NETs built in smtp mail objects to send notifications 
/// to the teams and allocator. It uses the default email settings and credentials that were 
/// defined in the web.config file. 
/// 
/// Author: Jonathan
/// </summary>
public class IceEmail
{
    MailMessage email;


    //Base Constructor
    public IceEmail()
    {
        email = new MailMessage();
        email.From = new MailAddress("seafairiceallocator@gmail.com");
    }

    // Constructor that receives a list of recipients

    public IceEmail(List<string> recipients)
    {
        email = new MailMessage();
        email.From = new MailAddress("seafairiceallocator@gmail.com");
        //Verify list has contents
        if (recipients.Count != 0)
        {
            addRecipients(recipients);
        }
    }
    // Constructor for an email to a single team,
    public IceEmail(Team team)
    {
        email = new MailMessage();
        email.From = new MailAddress("seafairiceallocator@gmail.com");
        addRecipients(team.getContactInfo());
    }

    // Constructor for an email to a single team, with added subject and message
    public IceEmail(Team team, string subject, string message)
    {
        email = new MailMessage();
        email.From = new MailAddress("seafairiceallocator@gmail.com");
        addRecipients(team.getContactInfo());
        addSubject(subject);
        setMessage(message);
    }

    // Sets the from address for the current message.
    public void setSender(string sender)
    {
        email.From = new MailAddress(sender);
    }


    //Sets the message to have a single visible receiver.
    public void setReceiver(string recipient)
    {
        //clear Receiver list of any possible contents
        email.To.Clear();
        email.To.Add(recipient);
    }

    // Sets the message of the current email
    public void setMessage(string message)
    {
        email.Body = message;
    }

    //Resets the message of the current email
    public void clearMessage()
    {
        email.Body = "";
    }

    // Adds a subject to the current email
    public void addSubject(string subject)
    {
        email.Subject = subject;
    }

    /*addReceivers
     * 
     * Takes a list of email addresses to send the email to, ensures 
     * distinct email addresses, and indiviudally adds addresses to 
     * the emails receivers. 
     */
    public void addRecipients(List<string> emailList)
    {
        //Ensure that the list has contents
        if (emailList.Count != 0)
        {
            //Remove any duplicate email addresses.
            emailList = emailList.Distinct().ToList();
            foreach (string indvidualEmail in emailList)
            {
                //Verify that the email is not empty
                if (!indvidualEmail.Equals(""))
                    email.To.Add(indvidualEmail);
            }
        }
    }


    /*addBlindReceivers
 * 
 * Takes a list of email addresses to send the email to, ensures 
 * distinct email addresses, and indiviudally adds addresses to 
 * the emails blind receivers. 
 */
    public void addBccRecipients(List<string> emailList)
    {
        //Ensure that the list has contents
        if (emailList.Count != 0)
        {
            //Remove any duplicate email addresses.
            emailList = emailList.Distinct().ToList();
            foreach (string indvidualEmail in emailList)
            {
                //Verify that the email is not empty
                if (!indvidualEmail.Equals(""))
                    email.Bcc.Add(indvidualEmail);
            }
        }
    }

    /* Clears the list of BCC recipients.
     * 
     */
    public void clearBccRecipients()
    {
        email.Bcc.Clear();
    }

    /* getRecipientsCount
     * Used to verify that there are recipients entered
     */
    public int getRecipientsCount()
    {
        return email.To.Count;
    }

    /* getBccCount
     * Used to verify that there are recipients entered
     */
    public int getBccCount()
    {
        return email.Bcc.Count;
    }

    //Send the email that is stored in the email variable.
    public string send()
    {

        string result = "";
        try
        {
            ////Configure an SmtpClient to send the mail.
            SmtpClient client = new SmtpClient();

            //Send the msg
            client.Send(email);

            //Display some feedback to the user to let them know it was sent
            result = "successful.";

        }
        catch (Exception ex)
        {
            //If the message failed at some point, let the user know
            result = "failed. " + ex.Message;
        }
        return result;
    }

    /* Sends a message with out using the EmailMessage object,
     * Used for intitial testing of asp.net email functions, and only referenced by test pages.
     * Also shows how to manually set up an smtp email
     * Source: stack overflow
     */
    public static string sendGMail(string txtTo, string subject, string message)
    {
        string result = "";
        try
        {
            //Create the msg object to be sent
            MailMessage msg = new MailMessage();
            //Add your email address to the recipients
            msg.To.Add(txtTo);
            //Configure the address we are sending the mail from
            MailAddress address = new MailAddress("seafairiceallocator@gmail.com");
            msg.From = address;
            //Append their name in the beginning of the subject
            msg.Subject = subject;
            msg.Body = message;


            ////Configure an SmtpClient to send the mail.

            // The important part -- configuring the SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //client.Port = 465;   // [1] You can try with port 465 also, I always used 587 and got success
            client.EnableSsl = true; //only enable this if your provider requires it
            client.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this

            client.UseDefaultCredentials = false; // [3] Changed this
            ////Setup credentials to login to our sender email address ("UserName", "Password")
            client.Credentials = new NetworkCredential("seafairiceallocator@gmail.com", "Project3900!");  // [4] Added this. Note, first parameter is NOT string.
            //client.Host = ;  

            //Send the msg
            client.Send(msg);

            //Display some feedback to the user to let them know it was sent
            result = "Your message was sent!";

        }
        catch
        {
            //If the message failed at some point, let the user know
            result = "Your message failed to send, please try again.";
        }
        return result;
    }
}