using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Net;
public partial class Default2 : System.Web.UI.Page
{
    String guid;
    public void getguid()
    {
        Guid g = Guid.NewGuid();
        guid = g.ToString();
    }
    //Function to create html email.
    void CreateMail(String uname)
    {
        String body = htmlbody(uname);
        SendMail(body);
    }
    //function to create html email body.
    String htmlbody(String uname)
    {
        String body = string.Empty;
        //using streamreader for reading my htmltemplate   

        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/signup_forp.html")))

        {

            body = reader.ReadToEnd();

        }

        body = body.Replace("{UserName}", uname); //replacing the required things  
        body = body.Replace("{url}", "http://pcse944-001-site1.btempurl.com/forpass1.aspx/?s=" + guid);
        body = body.Replace("{message1}", "Please click ");
        body = body.Replace("{message2}", "to change the password of your account. For security reasons link can be used only once!.");
        return body;
    }
    //function to send email.
    void SendMail(String body)
    {
        MailMessage mailMessage = new MailMessage("pcse944@gmail.com", TextBox1.Text);
        mailMessage.Subject = "Change Password";
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

    //Client name reteiver
    
  public void getvaljj()
    {
        string uname;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT f_name FROM register WHERE email = '" + TextBox1.Text + "';";
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            uname = Convert.ToString(reader["f_name"]);
            reader.Close();
            //Call to mail sender
            try
            {
                CreateMail(uname);
                send_link.Visible = true;
                send_link.Text = "A Link To Change Your Password Has Been Sent To Your Registered Email";
            }
            catch (System.Net.Mail.SmtpException ob)
            {
                net_error.Visible = true;
                //   Debug.Write(ob);
                net_error.Text = "Network Error!!Check your connectivity!";
            }
        }
        catch (Exception exp)
        {
            //Debug.WriteLine("error " + exp);
        }
        finally
        {
            con.Close();
        }
    }
    //Setting new guid.
    void getdata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE register SET guid = '" + guid + "' WHERE email = '" + TextBox1.Text + "'";
            if (cmd.ExecuteNonQuery() == 1)
            {
                getvaljj();
            }
            else
            {
                net_error.Visible = true;
                net_error.Text = "Email is not registered!";
            }

        }
        catch (Exception exp)
        {
            net_error.Visible = true;
            net_error.Text = "Error in webpage!";
            Debug.Write("JJ ExceTIOn"+exp);
        }
        finally
        {
            con.Close();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        net_error.Visible = false;
        send_link.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        getguid();
        getdata();

        //try
        //{
        //    SendMail(TextBox1.Text);
        //}
        //catch (Exception exp)
        //{
        //    Label2.Visible = true;
        //    Label2.Text = "Email ID is Required!";
        //}

    }

    protected void RegularExpressionValidator2_Load(object sender, EventArgs e)
    {
        net_error.Visible = false;
    }
}