using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Diagnostics;
using System.Net;

public partial class profile_Default3 : System.Web.UI.Page
{
    public int diffInSeconds;

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);

    public void getvaljj(String stat)
    {
        String umail, uname;
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            con2.Open();
            cmd.CommandText = "SELECT email FROM register WHERE regid ='" + Session["cod"] + "';";
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            umail = Convert.ToString(reader["email"]);
            reader.Close();
            cmd.CommandText = "SELECT f_name FROM register WHERE regid ='" + Session["cod"] + "';";
            reader = cmd.ExecuteReader();
            reader.Read();
            uname = Convert.ToString(reader["f_name"]);
            reader.Close();
            CreateMail(stat, uname, umail);
        }
        catch (Exception exp)
        {
            success.Text = "Error " + exp;
        }
        finally
        {
            con2.Close();
        }
    }

    //Function to create html email.
    void CreateMail(String stat, String uname, String umail)
    {
        try
        {
            String body = htmlbody(stat, 1, uname);
            SendMail(body, umail, stat, 1);
            body = htmlbody(stat, 2, uname);
            SendMail(body, umail, stat, 2);
        }
        catch (System.Net.Mail.SmtpException exp)
        {
            Debug.Write("JJ ExceptION 2" + exp);
        }
    }
    //Fuction to create html email body.
    String htmlbody(String stat, Int16 var, String uname)
    {
        String body = String.Empty;
        if (var == 1)
        {
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/adreqadmin.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", "Admin");
            body = body.Replace("{message1}", "Client named <b>" + uname + "</b> have <b>" + stat + "<\b> the AD request with AD Code <b>" + Session["frmcod"] + "<\b>. Click ");
            body = body.Replace("{url}", "http://pcse944-001-site1.btempurl.com/admindash/login.aspx");
            body = body.Replace("{message2}", " to open dashboard.");
            return body;
        }
        else
        {
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/adrequser.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", uname);
            body = body.Replace("{message1}", "You have <b>" + stat + "<\b> the AD request with AD Code <b>" + Session["frmcod"] + "<\b>. Thanks for choosing us. " +
                "Click ");
            body = body.Replace("{url}", "http://pcse944-001-site1.btempurl.com/profile/login.aspx");
            body = body.Replace("{message2}", " to login to your profile.");
            return body;
        }
    }
    //Mail Sender
    public void SendMail(String body, String umail, String stat, Int16 var)
    {
        MailMessage mailMessage;
        if (var == 1)
        {
            mailMessage = new MailMessage("pcse944@gmail.com", "pcse944@gmail.com");
        }
        else
        {
            mailMessage = new MailMessage("pcse944@gmail.com", umail);
        }
        mailMessage.Subject = "AD request " + stat + "!.";
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
    //End mail sender

    protected void Page_Load(object sender, EventArgs e)
    {
        string abc=null;
        
        if (!IsPostBack)
        {           
            DateTime dateTime1 = new DateTime();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand command = new SqlCommand("Select * from [detfrm] WHERE frmcod=" + Session["frmcod"], conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    dateTime1 = Convert.ToDateTime(reader[12]).AddDays(7);
                    DateTime dateTime2 = DateTime.Now;
                    diffInSeconds = (int)(dateTime1 - dateTime2).TotalSeconds;
                    abc = reader[8].ToString();
                }
            }
            conn.Close();
        }
        success.Visible = false;
        not_success.Visible = false;

        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from detfrm where frmregcod=" + Session["cod"] + "and frmcod=" + Session["frmcod"];
        cmd.ExecuteNonQuery();
		if (!IsPostBack)

        if (abc[0] == '/')
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dl.DataSource = dt;
            dl.DataBind();
            con.Close();
            y_url.Visible = false;
            btnShow.Visible = false;
            youtubelabel.Visible = false;
            approve.Visible = true;
            reject.Visible = true;
            change.Visible = true;
        }
        else
        {
            
            dl.Visible = false;
            btnShow.Visible = true;
            y_url.Visible = true;
            youtubelabel.Visible = true;
            y_url.Text = abc.ToString();
            lnkDummy.Visible = true;
        }
        con.Close();
    }
    protected void dl_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "update detfrm set frmstatus='Accepted by User' where frmcod=" + Session["frmcod"];
        cmd.ExecuteNonQuery();
        SqlCommand cmd3 = new SqlCommand("UPDATE detfrm SET rdt = @value WHERE frmcod = " + Session["frmcod"], con);
        cmd3.Parameters.AddWithValue("@value", DateTime.Now);
        cmd3.ExecuteNonQuery();
        getvaljj("Accepted");
        con.Close();
        success.Visible = true;
        success.Text = "Advertisement is Successfully Approved. Please wait for Admin Comformation. Thanks for choosing us";
        approve.Visible = false;
        reject.Visible = false;
        change.Visible = false;

        string page = Request.Url.Segments[Request.Url.Segments.Length - 1];
        HtmlMeta keywords = new HtmlMeta();
        keywords.HttpEquiv = "Refresh";
        keywords.Content = ("2;url=indexprofile.aspx").ToString();
        this.Page.Header.Controls.Add(keywords);

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "update detfrm set frmstatus='Rejected by User' where frmcod=" + Session["frmcod"];
        cmd.ExecuteNonQuery();
        SqlCommand cmd3 = new SqlCommand("UPDATE detfrm SET rdt = @value WHERE frmcod = " + Session["frmcod"], con);
        cmd3.Parameters.AddWithValue("@value", DateTime.Now);
        cmd3.ExecuteNonQuery();
        getvaljj("Rejected");
        con.Close();

        not_success.Visible = true;
        not_success.Text = ("OOPS!!! We will try our best with next Advertisement.");
        approve.Visible = false;
        reject.Visible = false;
        change.Visible = false;

        string page = Request.Url.Segments[Request.Url.Segments.Length - 1];
        HtmlMeta keywords = new HtmlMeta();
        keywords.HttpEquiv = "Refresh";
        keywords.Content = ("2;url=indexprofile.aspx").ToString();
        this.Page.Header.Controls.Add(keywords);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "update detfrm set frmstatus='Request Changed' where frmcod=" + Session["frmcod"];
        cmd.ExecuteNonQuery();
        SqlCommand cmd3 = new SqlCommand("UPDATE detfrm SET rdt = @value WHERE frmcod = " + Session["frmcod"], con);
        cmd3.Parameters.AddWithValue("@value", DateTime.Now);
        cmd3.ExecuteNonQuery();
        getvaljj("requested to make some changes to");
        con.Close();
        Response.Redirect("update.aspx");



    }




}