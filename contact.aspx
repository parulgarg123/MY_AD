<%@ Page Language="C#" MasterPageFile="~/ad.master" AutoEventWireup="true"  CodeFile="contact.aspx.cs" Inherits="contactt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="padding100">
        <div class="container">
            <div id="loginbox" style="margin-top: 50px;" class="mainbox col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <div class="panel-title">
                            Contact us</div>
                    </div>
                    <div style="padding-top: 30px" class="panel-body">
                          <div  id="login-alert" style="height:30px;" class="form-group"><p style="font-size:x-small;" align="right">Fields Marked with <b class="text text-danger">*</b> are Mandatory </p>

                        <div style="display: none" id="login-alert" class="alert alert-danger col-sm-12">
                        </div>
                         <div class="panel-body">
                        <div id="signupform" class="form-horizontal" role="form">
                           
                            
                            <div class="form-group">
                                <label for="firstname" class="col-md-3 control-label">
                                    Name <b class="text text-danger">*</b></label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="name" CssClass="form-control"  ></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="val1" ID="RequiredFieldValidator1" runat="server" ControlToValidate="name" ErrorMessage="Name is Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                     
                                       </div>
                            </div>
                            <div class="form-group">
                                <label for="email" class="col-md-3 control-label">Email <b class="text text-danger">*</b></label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="email" CssClass="form-control"></asp:TextBox>

                     
                                        <asp:RequiredFieldValidator ValidationGroup="val1" ID="RequiredFieldValidator5" runat="server" ControlToValidate="email" ErrorMessage="E-mail is Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

                             
                                    <asp:RegularExpressionValidator ValidationGroup="val1" ID="RegularExpressionValidator1" runat="server" ControlToValidate="email" ErrorMessage="You Must Enter The valid E-mail id" CssClass="text-danger" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
                                   
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="password" class="col-md-3 control-label">
                                    Subject <b class="text text-danger">*</b></label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="subject" CssClass="form-control" ></asp:TextBox>
                                    
                                     <asp:RequiredFieldValidator ValidationGroup="val1" ID="RequiredFieldValidator4" runat="server" ControlToValidate="subject" ErrorMessage="Subject is Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                     </div>
                                      </div>
                            <div class="form-group">
                                <label for="password" class="col-md-3 control-label">
                                    Message <b class="text text-danger">*</b></label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="message" CssClass="form-control" Height="150px" TextMode="MultiLine" style="resize:none;" ></asp:TextBox>
                                  
                                     <asp:RequiredFieldValidator ValidationGroup="val1" ID="RequiredFieldValidator2" runat="server" ControlToValidate="message" ErrorMessage="Message is Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                     </div>
                                      </div>
                            </div>   
                            
                            </div>                        
                            <div class="form-group">
                                <!-- Button -->
                                <div class="col-md-offset-3 col-md-9">                              
                                    <asp:Button ValidationGroup="val1" ID="Button1"  CssClass="btn btn-success" runat="server" Text="Send" Height="35px" Width="150px" OnClick="Button1_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-danger" Height="35px" Width="150px" OnClick="Button2_Click" />
                                &nbsp;<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;<div class="form-group">
                                <div class="col-md-12 control">
                                    <div style="padding-top: 15px; font-size: 85%">
                                        <a href="login.aspx"> &nbsp;</a></div>
                                </div>
                            <%--</div></div>--%>
                                    <asp:Label ID="success" CssClass="alert alert-success" runat="server"></asp:Label>
                                    <asp:Label ID="not_success" CssClass="alert alert-danger" runat="server"></asp:Label>
                            </div>
                        </div>
                &nbsp;&nbsp;
                </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
</asp:Content>


