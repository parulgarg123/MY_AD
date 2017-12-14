using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Net.Mail;
using System.IO;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    String v;
    protected void Page_Load(object sender, EventArgs e)
    {
        success_pass.Visible = false;
        error_webpage.Visible = false;
        v = Request.QueryString["s"];
    }

    void getdata()
    {
        String umail, uname;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);

        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE register SET password = '" + TextBox1.Text + "' WHERE guid = '" + v + "'"; /*Response.Redirect("login.aspx");*/
            if (cmd.ExecuteNonQuery() == 1)
            {
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
                cmd.CommandText = "UPDATE register SET guid = NULL WHERE guid = '" + v + "'"; /*Response.Redirect("login.aspx");*/
                cmd.ExecuteNonQuery();
                CreateMail(uname, umail);
                success_pass.Visible = true;
                success_pass.Text = "Your Account's Password Has Been Changed.";
                String page = Request.Url.Segments[Request.Url.Segments.Length - 1];
                HtmlMeta keywords = new HtmlMeta();
                keywords.HttpEquiv = "Refresh";
                keywords.Content = ("1;url=/login.aspx").ToString();
                this.Page.Header.Controls.Add(keywords);
            }
            else
            {
                error_webpage.Visible = true;
                error_webpage.Text = "ERROR in webpage! ";
            }
        }
        catch (Exception exp)

        {
            error_webpage.Visible = true;
            error_webpage.Text = "ERROR in webpage!";
            Debug.Write("JJ EXcepTIOn"+exp);
        }
        finally
        {
            con.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        getdata();
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
        body = body.Replace("{message1}", "Your account's password has been successfully changed." +
            " Click ");
        body = body.Replace("{url}", "http://pcse944-001-site1.btempurl.com/login.aspx");
        body = body.Replace("{message2}", " to login.");
        return body;
    }
    //Fuction to send email to user.
    void SendMail(String body, String umail)
    {
        MailMessage mailMessage = new MailMessage("pcse944@gmail.com", umail);
        mailMessage.Subject = "Password Changed Successfully!";
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