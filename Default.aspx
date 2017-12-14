<%@ Page Title="" Language="C#" MasterPageFile="~/ad.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/round-about.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Page Content -->
    <div class="container">

        <!-- Introduction Row -->
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">We Designed This Website
                    <small>It's Nice to Meet You!</small>
                </h1>
                <p>Starting with no withhand experience of Website designing and development, the team toiled hard to complete <b style="color: #9D0000">My AD</b> </p>
            </div>
        </div>

        <!-- Team Members Row -->
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">Our Team</h2>
            </div>
            <div class="col-lg-4 col-sm-6 text-center">
                <img class="img-circle img-responsive img-center" src="/img/a4.jpg" alt="">
                <h3>Bikram Sohi <small>Developer</small>
                </h3>
                <p>I was responsible for designing and development of admin panel.</p>
            </div>
            <div class="col-lg-4 col-sm-6 text-center">
                <img class="img-circle img-responsive img-center" src="/img/a4.jpg" alt="">
                <h3>Jagjot Singh <small>Developer</small>
                </h3>
                <p>Hi There! Basically I did nothing and am here for attendance.</p>
            </div>
            <div class="col-lg-4 col-sm-6 text-center">
                <img class="img-circle img-responsive img-center" src="/img/a3.jpg" alt="">
                <h3>Shobit Mahajan <small>Designer</small>
                </h3>
                <p>Hi, I was responsible for the design of whole website.</p>
            </div>
            <div class="text-center;" style="margin-left: 25%;">
                <div class="col-lg-4 col-sm-6 text-center">
                    <img class="img-circle img-responsive img-center" src="/img/a4.jpg" alt="">
                    <h3>Parul Garg <small>Developer</small>
                    </h3>
                    <p>Hello I am Parul, I handled the development of the user panel of website.</p>
                </div>
                <div class="col-lg-4 col-sm-6 text-center">
                    <img class="img-circle img-responsive img-center" src="/img/a4.jpg" alt="">
                    <h3>Kamal Kant <small>Designer</small>
                    </h3>
                    <p>My work was to create sketches for the website and </p>
                </div>
            </div>
        </div>
</asp:Content>

