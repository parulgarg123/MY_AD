using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using myad;

public partial class profile_Default : System.Web.UI.Page
{
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
            show.Text = "Error " + exp;
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
            body = body.Replace("{message1}", "Client named <b>" + uname + "</b> have <b>" + stat + "</b> the AD request with AD Code <b>" + Session["frmcod"] + "</b>. Click ");
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
            body = body.Replace("{message1}", "You have <b>" + stat + "</b> the AD request with AD Code <b>" + Session["frmcod"] + "</b>. Thanks for choosing us. " +
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
        title.Focus();
        if (Page.IsPostBack == false)
        {
            clsdetfrm obj = new clsdetfrm();
            List<clsdetfrmprp> k = obj.find_rec_detfrm(Convert.ToInt32(Session["frmcod"]));
            if (k.Count > 0)
            {
                ViewState["frmcod"] = k[0].frmcod;
                ViewState["regcod"] = k[0].frmregcod;
                title.Text = k[0].frmtit;
                rtd.Text = Convert.ToString(k[0].frmdate.ToShortDateString());
                urls.Text = k[0].frmurl;
                requirements.Text = k[0].frmdesc;
                DropDownList1.Text = k[0].frmreqcont;
                update_det.Text = "update";
            }
        }
    }

    private void button_clear()
    {
        title.Text = string.Empty;
        rtd.Text = string.Empty;
        urls.Text = string.Empty;
        requirements.Text = string.Empty;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (rtd.Text != string.Empty)
            Calendar1.SelectedDate = Convert.ToDateTime(rtd.Text);

        Calendar1.Visible = true;
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        rtd.Text = Calendar1.SelectedDate.ToShortDateString();
        Calendar1.Visible = false;
    }

    protected void rtd_TextChanged(object sender, EventArgs e)
    {
        Calendar1.SelectedDate = Convert.ToDateTime(rtd.Text);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        clsdetfrm obj = new clsdetfrm();
        clsdetfrmprp objprp = new clsdetfrmprp();
        objprp.frmtit = title.Text;
        objprp.frmcod = Convert.ToInt32(Session["frmcod"]);
        objprp.frmdate = Convert.ToDateTime(rtd.Text);
        objprp.frmurl = urls.Text;
        objprp.frmdesc = requirements.Text;
        objprp.frmreqcont = DropDownList1.SelectedValue.ToString();
        objprp.frmsubmitdate = DateTime.Now;
        obj.update_rec_detfrm(objprp);
        getvaljj("Updated");
        Session["update"] = Convert.ToInt32(Session["frmcod"]);
        Response.Redirect("det_view.aspx");
        show.Text = "changes request send Successfully";

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        button_clear();
    }



    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {

        if (e.Day.Date <= DateTime.Now)
        {
            e.Cell.BackColor = ColorTranslator.FromHtml("#a9a9a9");
            e.Day.IsSelectable = false;
        }
    }

}