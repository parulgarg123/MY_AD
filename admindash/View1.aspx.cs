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
using System.Diagnostics;
using System.IO;
using System.Net;

public partial class admindash_Default5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Extv.Visible = false;
        Extvid.Visible = false;
        RequiredFieldValidator3.Visible = false;
        RequiredFieldValidator3.Enabled = false;
        //TextBox1.Text = Session["links"].ToString();
        //TextBox2.Text = Session["desc"].ToString();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        conn.Open();
        SqlCommand command = new SqlCommand("Select * from [detfrm] where frmcod=@x", conn);
        command.Parameters.AddWithValue("@x", Convert.ToInt32(Request.QueryString["rid"]));
        using (SqlDataReader reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                if (!reader[9].ToString().Equals("Accepted by Admin"))
                {
                    Response.Redirect("~/admindash/Requests.aspx");
                }
                RefUrl.Text = reader[4].ToString();
                Desc.Text = reader[5].ToString();
                if (reader[6].ToString().Equals("Image"))
                {
                    Selvid.Visible = false;
                    VidUpload.Visible = false;
                    RequiredFieldValidator2.Visible = false;
                    RequiredFieldValidator2.Enabled = false;
                    Button2.Visible = false;
                    Button2.Enabled = false;
                }
                else if (reader[6].ToString().Equals("Video"))
                {
                    Selimg.Visible = false;
                    ImgUpload.Visible = false;
                    RequiredFieldValidator1.Visible = false;
                    RequiredFieldValidator1.Enabled = false;

                }
            }
            else
            {
                Response.Redirect("~/admindash/Requests.aspx");
            }
        }
        conn.Close();
    }


    //AD id umail reteiver
    public void getvaljj()
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
            CreateMail(umail, uname); //Call to mail creater
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
    void CreateMail(String umail, String uname)
    {
        try
        {
            String body = htmlbody(uname);
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
        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/adup.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", uname);
        body = body.Replace("{message1}", "We just uploaded a 'Demo AD' related to your AD request with AD code <b>" +
            Convert.ToInt32(Request.QueryString["rid"]) + "</b>. You can choose to 'Accept AD' or 'Reject AD' or request to make some modifications in AD." +
            " <b>Remember AD will be auto APPROVED in 7 days</b>. Please click ");
        body = body.Replace("{url}", "http://pcse944-001-site1.btempurl.com/login.aspx");
        body = body.Replace("{message2}", " to login. Thanks for choosing us!.");
        return body;
    }
    //Mail Sender
    public void SendMail(String body, String umail)
    {

        MailMessage mailMessage = new MailMessage("pcse944@gmail.com", umail);
        mailMessage.IsBodyHtml = true;
        mailMessage.Subject = "Demo AD uploaded!!";
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

    protected void Upload_Click(object sender, EventArgs e)
    {
        if (ImgUpload.HasFile)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
            con.Open();
            ImgUpload.PostedFile.SaveAs(Server.MapPath("~/Data/") + ImgUpload.FileName);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE detfrm SET frmimg = " + "'/Data/" + ImgUpload.FileName + "' WHERE frmcod = " + Convert.ToInt32(Request.QueryString["rid"]);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        if (VidUpload.HasFile)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
            con.Open();
            VidUpload.PostedFile.SaveAs(Server.MapPath("~/Data/") + VidUpload.FileName);
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "UPDATE detfrm SET frmvideo = " + "'/Data/" + VidUpload.FileName + "' WHERE frmcod = " + Convert.ToInt32(Request.QueryString["rid"]);
            cmd1.ExecuteNonQuery();

            con.Close();
        }
        if (!(Extvid.Text.Equals("")))
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
            con.Open();
            SqlCommand cmd3 = new SqlCommand("UPDATE detfrm SET frmvideo = @value WHERE frmcod = " + Convert.ToInt32(Request.QueryString["rid"]), con);
            cmd3.Parameters.AddWithValue("@value", Extvid.Text);
            cmd3.ExecuteNonQuery();
            con.Close();
        }
        if (ImgUpload.HasFile || VidUpload.HasFile || !(Extvid.Text.Equals("")))
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
            con.Open();
            SqlCommand cmd3 = new SqlCommand("UPDATE detfrm SET pdt = @value WHERE frmcod = " + Convert.ToInt32(Request.QueryString["rid"]), con);
            cmd3.Parameters.AddWithValue("@value", DateTime.Now);
            cmd3.ExecuteNonQuery();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "UPDATE detfrm SET frmstatus = '" + "Waiting Response" + "' WHERE frmcod = " + Convert.ToInt32(Request.QueryString["rid"]);
            cmd2.ExecuteNonQuery();
            con.Close();
            getvaljj();
            Response.Redirect("View3.aspx?rid=" + Convert.ToInt32(Request.QueryString["rid"]));
        }
    }

    protected void Extvid_Click(object sender, EventArgs e)
    {
        Selvid.Visible = false;
        VidUpload.Visible = false;
        RequiredFieldValidator2.Visible = false;
        RequiredFieldValidator2.Enabled = false;
        Extv.Visible = true;
        Extvid.Visible = true;
        RequiredFieldValidator3.Visible = true;
        RequiredFieldValidator3.Enabled = true;
    }
}