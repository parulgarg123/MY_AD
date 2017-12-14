using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Diagnostics;

public partial class _Default : System.Web.UI.Page
{
    String guid;
    protected void Page_Load(object sender, EventArgs e)
    {
        success.Visible = false;
        error.Visible = false;
    }
    public void getguid()
    {
        Guid g = Guid.NewGuid();
        guid = g.ToString();
    }
    //Function to create html email.
    void CreateMail()
    {
        String uname = txtf_name.Text;
        String msg1 = "Please click ";
        String msg2 = " to verify your account. For security reasons link can be used only once. We hope you will have a great experience with MyAD." +
            " Thanks for registering with MyAD. Didn't create an account on MyAD? Please <b>IGNORE</b> this email.";
        String url = "http://pcse944-001-site1.btempurl.com/verify.aspx/?s=" + guid;
        String body = htmlbody(uname, url, msg1, msg2);
        SendMail(body);
    }
    //Function to create html body of email.
    String htmlbody(String uname, String url, String msg1, String msg2)
    {
        String body = string.Empty;
        //using streamreader for reading my htmltemplate   

        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailtemp/signup_forp.html")))

        {

            body = reader.ReadToEnd();

        }

        body = body.Replace("{UserName}", uname); //replacing the required things  
        body = body.Replace("{url}", url);
        body = body.Replace("{message1}", msg1);
        body = body.Replace("{message2}", msg2);
        return body;
    }
    //Function to send Email to user.
    void SendMail(String body)
    {

        MailMessage mailMessage = new MailMessage("pcse944@gmail.com", txtemail.Text);
        mailMessage.Subject = "Verification Link";
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
        myad.clsregister obj = new myad.clsregister();
        myad.clsregistrationprp objprp = new myad.clsregistrationprp();
        objprp.email = txtemail.Text;
        objprp.f_name = txtf_name.Text;
        objprp.l_name = txtl_name.Text;
        objprp.address = txtaddress.Text;
        objprp.password = txtpassword.Text;
        objprp.contact = countrycod.Text + "-" + contact.Text;
        objprp.verified = "Unverified";
        objprp.join_date = DateTime.Now;
        getguid();
        objprp.guid = guid;


        try
        {
            obj.save_rec(objprp);
            try
            {
                CreateMail();
                txtemail.Text = String.Empty;
                txtf_name.Text = String.Empty;
                txtl_name.Text = String.Empty;
                txtaddress.Text = String.Empty;
                contact.Text = String.Empty;
                guid = String.Empty;
                success.Visible = true;
                success.Text = "A verification link has been sent to your email.";
            }
            catch (System.Net.Mail.SmtpException ob)
            {
                error.Visible = true;
                error.Text = "Network Error!!Check your connectivity!";
                Debug.Write("exp parul " + ob);
            }
        }
        catch (Exception exp)
        {
            error.Visible = true;
            error.Text = "Email Id already registered.";
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        txtemail.Text = String.Empty;
        txtf_name.Text = String.Empty;
        txtl_name.Text = String.Empty;
        txtaddress.Text = String.Empty;
        contact.Text = String.Empty;
        guid = String.Empty;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }

    protected void txtaddress_TextChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        int i = 0;
        String s = DropDownList1.SelectedItem.ToString();



        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myad5ConnectionString"].ConnectionString);
        conn.Open();
        SqlCommand command = new SqlCommand("Select * from [country] where nicename=@x", conn);
        command.Parameters.AddWithValue("@x", s);
        using (SqlDataReader reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                i = Convert.ToInt16(reader[6]);
            }
        }
        conn.Close();
        countrycod.Text = "+" + Convert.ToString(i);

    }
}