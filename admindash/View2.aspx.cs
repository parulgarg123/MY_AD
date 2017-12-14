using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Diagnostics;
using System.Net;


public partial class admindash_Default6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        conn.Open();
        SqlCommand command = new SqlCommand("Select * from [detfrm] where frmcod=@x", conn);
        command.Parameters.AddWithValue("@x", Convert.ToInt32(Request.QueryString["rid"]));
        using (SqlDataReader reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                if (!reader[9].ToString().Equals("New") && !reader[9].ToString().Equals("Request Changed"))
                {
                    Response.Redirect("~/admindash/Requests.aspx");
                }
                RefUrl.Text = reader[4].ToString();
                Desc.Text = reader[5].ToString();
            }
            else
            {
                Response.Redirect("~/admindash/Requests.aspx");
            }
        }
        conn.Close();
    }
    //AD id umail reteiver
    public void getvaljj(int stat)
    {
        string id = Request.QueryString["rid"];
        Int32 val;
        string umail, uname;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT frmregcod FROM detfrm WHERE frmcod = '" + id + "'";
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            val = Convert.ToInt32(reader["frmregcod"]);
            reader.Close();
            cmd.CommandText = "SELECT email FROM register WHERE regid = '" + val + "';";
            reader = cmd.ExecuteReader();
            reader.Read();
            umail = Convert.ToString(reader["email"]);
            reader.Close();
            cmd.CommandText = "SELECT f_name FROM register WHERE regid = '" + val + "';";
            reader = cmd.ExecuteReader();
            reader.Read();
            uname = Convert.ToString(reader["f_name"]);
            reader.Close();
            CreateMail(umail, uname, stat); //Call to mail sender
        }
        catch (Exception exp)
        {
            Debug.WriteLine("error " + exp);
        }
        finally
        {
            con.Close();
        }
    }

    //Function to create html email.
    void CreateMail(String umail, String uname, int stat)
    {
        try
        {
            String body = htmlbody(stat, uname);
            SendMail(body, umail, stat);
        }
        catch (System.Net.Mail.SmtpException exp)
        {
            Debug.Write("JJ ExceptION 2" + exp);
        }
    }
    //Fuction to create html email body.
    String htmlbody(int stat, String uname)
    {
        String body = String.Empty;
        if (stat == 1)
        {
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/adaccept.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", uname);
            body = body.Replace("{message}", "Your AD request with AD code <b>" + Convert.ToInt32(Request.QueryString["rid"]) + "</b> has been <b>accepted</b>." +
                " We will soon upload a 'Demo AD' related to your AD request. Thanks for choosing us!!");
            return body;
        }
        else
        {
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/adrejected.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", uname);
            body = body.Replace("{message1}", "Your AD request with AD code <b>" + Convert.ToInt32(Request.QueryString["rid"]) + 
                "</b> has been <b>rejected</b>. Please click ");
            body = body.Replace("{url}", "http://pcse944-001-site1.btempurl.com/contact.aspx");
            body = body.Replace("{message2}", " for futher enquiry.");
            return body;
        }

    }
    //Mail Sender
    public void SendMail(String body, String umail, int stat)
    {

        MailMessage mailMessage = new MailMessage("pcse944@gmail.com", umail);
        mailMessage.IsBodyHtml = true;
        if (stat == 1)
        {
            mailMessage.Subject = "AD requested Accepted!!";
            mailMessage.Body = body;
        }
        else
        {
            mailMessage.Subject = "AD request Rejected!!";
            mailMessage.Body = body;
        }
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
    //End mail sender

    protected void Yes_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE detfrm SET frmstatus = '" + "Accepted by Admin" + "' WHERE frmcod = " + Convert.ToInt32(Request.QueryString["rid"]);
        cmd.ExecuteNonQuery();
        con.Close();
        getvaljj(1);
        Response.Redirect("View1.aspx?rid=" + Convert.ToInt32(Request.QueryString["rid"]));
    }

    protected void No_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE detfrm SET frmstatus = '" + "Rejected by Admin" + "' WHERE frmcod = " + Convert.ToInt32(Request.QueryString["rid"]);
        cmd.ExecuteNonQuery();
        con.Close();
        getvaljj(0);
        SqlParameter sp1 = new SqlParameter();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        con.Open();
        cmd = new SqlCommand("deldetfrm", con);
        cmd.CommandType = CommandType.StoredProcedure;
        sp1 = cmd.Parameters.Add("@frmcod", SqlDbType.Int);
        sp1.Value = Convert.ToInt32(Request.QueryString["rid"]);
        cmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("Requests.aspx");
    }
}