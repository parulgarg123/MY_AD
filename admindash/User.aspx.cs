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

public partial class admindash_Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand command = new SqlCommand("Select * from [register] where regid=@x", conn);
            command.Parameters.AddWithValue("@x", Convert.ToInt32(Request.QueryString["uid"]));
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {

                    Fnametext.Text = reader[2].ToString();
                    Lnametext.Text = reader[3].ToString();
                    Emailtext.Text = reader[1].ToString();
                    Contacttext.Text = reader[6].ToString();
                    Addresstext.Text = reader[4].ToString();
                    Passwordtext.Text = reader[5].ToString();
                    String s = reader[9].ToString();
                    if (s.Equals("Active"))
                    {
                        DropDownList1.SelectedIndex = 0;
                    }
                    else if (s.Equals("Blocked"))
                    {
                        DropDownList1.SelectedIndex = 1;
                    }
                }
                else
                {
                    Response.Redirect("~/admindash/Requests.aspx");
                }
            }
            conn.Close();
            lockdiv();
        }
    }

    //Function to create html email.
    void CreateMail(String stat, String umail, String uname)
    {
        try
        {
            String body = htmlbody(stat,uname);
            SendMail(body, umail, stat);
        }
        catch (System.Net.Mail.SmtpException exp)
        {
            Debug.Write("JJ ExceptION 2" + exp);
        }
    }
    //Fuction to create html email body.
    String htmlbody(String stat, String uname)
    {
        String body = String.Empty;
        if (stat.CompareTo("Blocked") == 0)
        {
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/blocked.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", uname);
            body = body.Replace("{message1}", "Your account has been <b>" + stat + "</b> by MyAD Admin. Please click ");
            body = body.Replace("{url}", "http://pcse944-001-site1.btempurl.com/contact.aspx");
            body = body.Replace("{message2}", " for futher enquiry.");
            return body;
        }
        else 
        {
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/blocked.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", uname);
            body = body.Replace("{message1}", "Your account has been <b>" + stat + "</b> by MyAD Admin. Please click ");
            body = body.Replace("{url}", "http://pcse944-001-site1.btempurl.com/index.aspx");
            body = body.Replace("{message2}", " to login.");
            return body;
        }   
    }
    //Mail Sender
    public void SendMail(String body, String umail, String stat)
    {


        MailMessage mailMessage = new MailMessage("pcse944@gmail.com", umail);
        mailMessage.IsBodyHtml = true;
        if(stat.CompareTo("Blocked") == 0)
        {
            mailMessage.Subject = "Account Blocked!";
        }
        else
        {
            mailMessage.Subject = "Account Unblocked!";
        }
        
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
    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

        string req_id = GridView1.SelectedRow.Cells[0].Text;

        if (GridView1.SelectedRow.Cells[8].Text == "Accepted by Admin")
        {
            Response.Redirect("View1.aspx?rid=" + req_id);
        }
        else if (GridView1.SelectedRow.Cells[8].Text == "New" || GridView1.SelectedRow.Cells[8].Text == "Request Changed")
        {
            Response.Redirect("View2.aspx?rid=" + req_id);
        }
        else
        {
            Response.Redirect("View3.aspx?rid=" + req_id);
        }
    }



    protected void Update_Click(object sender, EventArgs e)
    {
        String s = DropDownList1.SelectedItem.Value.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE register SET f_name ='" + Fnametext.Text + "',l_name='" + Lnametext.Text + "',contact='" + Contacttext.Text + "',address='" + Addresstext.Text + "',password='" + Passwordtext.Text + "',acc_stat='" + s + "' WHERE regid = " + Convert.ToInt32(Request.QueryString["uid"]);
        cmd.ExecuteNonQuery();
        con.Close();
        //SendMail();
        if (DropDownList1.SelectedIndex == 0)
        {
            CreateMail("Unblocked", Emailtext.Text, Fnametext.Text);
        }
        else
        {
            CreateMail("Blocked", Emailtext.Text, Fnametext.Text);
        }
        Response.Redirect(Request.RawUrl);
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void DelU_Click(object sender, EventArgs e)
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlParameter sp1 = new SqlParameter();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        con.Open();
        cmd = new SqlCommand("delreg", con);
        cmd.CommandType = CommandType.StoredProcedure;
        sp1 = cmd.Parameters.Add("@regid", SqlDbType.Int);
        sp1.Value = Convert.ToInt32(Request.QueryString["uid"]);
        cmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("Users.aspx");
    }

    protected void DelUR_Click(object sender, EventArgs e)
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlParameter sp1 = new SqlParameter();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        con.Open();
        cmd = new SqlCommand("delregad", con);
        cmd.CommandType = CommandType.StoredProcedure;
        sp1 = cmd.Parameters.Add("@regid", SqlDbType.Int);
        sp1.Value = Convert.ToInt32(Request.QueryString["uid"]);
        cmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("Users.aspx");
    }

    protected void lk1_Click(object sender, EventArgs e)
    {
        unlockdiv();
    }

    void lockdiv()
    {
        Fnametext.ReadOnly = true;
        Lnametext.ReadOnly = true;
        Emailtext.ReadOnly = true;
        Contacttext.ReadOnly = true;
        Addresstext.ReadOnly = true;
        Passwordtext.ReadOnly = true;
        DropDownList1.Enabled = false;
        Update.Enabled = false;
        Cancel.Enabled = false;
    }

    void unlockdiv()
    {
        Fnametext.ReadOnly = false;
        Fnametext.Focus();
        Lnametext.ReadOnly = false;
        Emailtext.ReadOnly = false;
        Contacttext.ReadOnly = false;
        Addresstext.ReadOnly = false;
        Passwordtext.ReadOnly = false;
        DropDownList1.Enabled = true;
        Update.Enabled = true;
        Cancel.Enabled = true;
    }
}