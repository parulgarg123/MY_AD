﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;

public partial class profile_Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        success.Visible = false;
        error.Visible = false;
    }
    //To create html email.
    void CreateMail()
    {
        String body = htmlbody();
        SendMail(body);
    }
    //to create html email body.
    String htmlbody()
    {
        String body = string.Empty;
        //using streamreader for reading my htmltemplate   

        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/contactustemp.html")))

        {

            body = reader.ReadToEnd();

        }
        body = body.Replace("{message}", "Client Name :  " + name.Text + "<br />Client E-Mail :  " + email.Text + "<br />Subject :  " + subject.Text + "<br />Message :  " + message.Text);

        return body;
    }
    //to send email.
    public void SendMail(String body)
    {

        MailMessage mailMessage = new MailMessage("pcse944@gmail.com", "pcse944@gmail.com");
        mailMessage.Subject = "Contact Mail";
        mailMessage.IsBodyHtml = true;
        mailMessage.Body = body;
        SmtpClient smtpg = new SmtpClient("smtp.gmail.com", 587);
        //smtpg.Credentials = new System.Net.NetworkCredential()
        //{
        //    UserName = "my-ad@outlook.com",
        //    Password = "password1.A"
        //};
        smtpg.EnableSsl = true;
        NetworkCredential NetworkCred = new NetworkCredential();
        NetworkCred.UserName = "pcse944@gmail.com";
        NetworkCred.Password = "password1.";
        smtpg.UseDefaultCredentials = true;
        smtpg.Credentials = NetworkCred;
        smtpg.Send(mailMessage);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (email.Text == string.Empty ||
                name.Text == string.Empty ||
                subject.Text == string.Empty ||
                message.Text == string.Empty)
            {
                error.Visible = true;
                error.Text = "All above fields are required!!";
            }
            else
            {
                //SendMail();
                CreateMail();
                success.Visible = true;
                success.Text = "Your message has been successfully sent.";
            }
        }
        catch (Exception exp)
        {
            error.Visible = true;
            error.Text = "Network Error!!Check your connectivity!";
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        email.Text = string.Empty;
        name.Text = string.Empty;
        subject.Text = string.Empty;
        message.Text = string.Empty;
    }
}