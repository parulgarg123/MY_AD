using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net.Mail;
using System.Diagnostics;
using System.IO;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        verify_success.Visible = false;
        verify_notsuccess.Visible = false;
        String v = Request.QueryString["s"];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);

        try
        {
            String uname, umail;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE register SET acc_stat = 'Active' WHERE guid = '" + v + "'";
            //    cmd.CommandText = "UPDATE register SET guid = NULL WHERE guid = '" + v + "'";
            if (cmd.ExecuteNonQuery() == 1)
            {
                verify_success.Visible = true;
                cmd.CommandText = "SELECT f_name FROM register WHERE guid = '" + v + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                uname = Convert.ToString(reader["f_name"]);
                reader.Close();
                cmd.CommandText = "SELECT email FROM register WHERE guid = '" + v + "'";
                reader = cmd.ExecuteReader();
                reader.Read();
                umail = Convert.ToString(reader["email"]);
                reader.Close();
                cmd.CommandText = "UPDATE register SET guid = NULL WHERE guid = '" + v + "'";
                cmd.ExecuteNonQuery();
                CreateMail(uname, umail);
                verify_success.Text = "Your Account has been successfully verified.";
                v = String.Empty;
                String page = Request.Url.Segments[Request.Url.Segments.Length - 1];
                HtmlMeta keywords = new HtmlMeta();
                keywords.HttpEquiv = "Refresh";
                keywords.Content = ("1;url=/login.aspx").ToString();
                this.Page.Header.Controls.Add(keywords);
                con.Close();
            }
            else
            {
                con.Close();
                verify_notsuccess.Visible = true;
                verify_notsuccess.Text = "This page doesn't exist!!";
            }
        }
        catch (Exception exp)
        {
            //Label2.Visible = true;
            //Label2.Text = "ERROR in webpage! ";
            Debug.Write("JJ ExceptION 1" + exp);
        }
    }
    //Function to create html email.
    void CreateMail(String uname, String umail)
    {
        String body = htmlbody(uname);
        try
        {
            SendMail(body, umail);
        }
        catch (System.Net.Mail.SmtpException exp)
        {
            Debug.Write("JJ ExceptION 2" + exp);
        }
    }
    //Fuction to create html email body.
    String htmlbody(String uname)
    {
        String body = String.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/ver_pass.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{username}", uname);
        body = body.Replace("{message1}", "Your account has been successfully verified. Thanks for choosing us." +
            " You can now click ");
        body = body.Replace("{url}", "http://pcse944-001-site1.btempurl.com/login.aspx");
        body = body.Replace("{message2}", " to login.");
        return body;
    }
    //Fuction to send email to user.
    void SendMail(String body, String umail)
    {
        MailMessage mailMessage = new MailMessage("pcse944@gmail.com", umail);
        mailMessage.Subject = "Account Verified!";
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
}