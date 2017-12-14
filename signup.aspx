<%@ Page Title="" Language="C#" MasterPageFile="~/ad.master" AutoEventWireup="true" CodeFile="signup.aspx.cs" Inherits="_Default" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <style type="text/css">
        .VeryPoorStrength {
            /*background: Red;*/
            color: Red;
            margin-left: 10px;
            font-weight: bold;
            padding: 6px 12px;
            font-size: 14px;
        }

        .WeakStrength {
            /*background: Gray;*/
            color: Gray;
            margin-left: 10px;
            font-weight: bold;
            padding: 6px 12px;
            font-size: 14px;
        }

        .AverageStrength {
            /*background: orange;*/
            margin-left: 10px;
            color: orange;
            padding: 6px 12px;
            font-size: 14px;
            font-weight: bold;
        }

        .GoodStrength {
            /*background: blue;*/
            color: blue;
            padding: 6px 12px;
            font-size: 14px;
            margin-left: 10px;
            font-weight: bold;
        }

        .ExcellentStrength {
            /*background: Green;*/
            color: Green;
            padding: 6px 12px;
            font-size: 14px;
            margin-left: 10px;
            font-weight: bold;
        }

        .BarBorder {
            border-style: solid;
            border-width: 1px;
            width: 180px;
            padding: 2px;
        }
    </style>




</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="padding100">
        <div class="container">
            <div id="loginbox" style="margin-top: 50px;" class="mainbox col-md-7 col-md-offset-3 col-sm-9 col-sm-offset-2">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <div class="panel-title">
                            Sign Up
                        </div>
                    </div>
                    <div style="padding-top: 30px" class="panel-body">
                        <div id="login-alert" style="height: 30px;" class="form-group">
                            <p style="font-size: x-small;" align="right">Fields Marked with <b class="text text-danger">*</b> are Mandatory </p>

                            <asp:Label Style="margin-left: 80px; margin-bottom: 20px;" CssClass="alert alert-success" ID="success" runat="server"></asp:Label>

                        </div>
                        <div class="panel-body">
                            <div id="signupform" class="form-horizontal" role="form">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                <div class="form-group">
                                    <label for="firstname" class="col-md-3 control-label">&nbsp;First Name <b class="text text-danger">*</b></label>

                                    <div class="col-md-9">
                                        <asp:TextBox runat="server" ID="txtf_name" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="val1" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtf_name" ErrorMessage="First Name is Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="lastname" class="col-md-3 control-label">
                                        Last Name <b class="text text-danger">*</b></label>
                                    <div class="col-md-9">
                                        <asp:TextBox runat="server" ID="txtl_name" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="val1" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtl_name" ErrorMessage="Last Name is Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="Country" class="col-md-3 control-label">Country  <b class="text text-danger">*</b></label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="true" DataSourceID="SqlDataSource1" DataTextField="nicename" DataValueField="nicename" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem Text="-------Select Country-------" Value=""></asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="val1" runat="server" ControlToValidate="countrycod" ErrorMessage="Country is Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:myad5ConnectionString %>" SelectCommand="SELECT [nicename] FROM [country]"></asp:SqlDataSource>

                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="contact no" class="col-md-3 control-label">
                                        Contact  Number <b class="text text-danger">*</b></label>
                                    <div class="form-inline col-md-9">
                                        <asp:TextBox runat="server" ID="countrycod" CssClass="form-control" placeholder="" Width="80px" ReadOnly="true"></asp:TextBox>

                                        <asp:TextBox runat="server" ID="contact" CssClass="form-control" Width="80%" placeholder="Contact number" MaxLength="12" MinLength="10"></asp:TextBox>


                                        <br />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="val1" runat="server" ControlToValidate="contact" ErrorMessage="Contact Number is Required" CssClass="form-group text-danger " Display="Dynamic"></asp:RequiredFieldValidator>




                                        <asp:RangeValidator ID="RangeValidator1" ValidationGroup="val1" runat="server" ControlToValidate="contact" Display="Dynamic" ErrorMessage="Enter a valid contact no." CssClass="form-group text-danger " MaximumValue="999999999999" MinimumValue="10000000" Type="Double"></asp:RangeValidator>




                                    </div>

                                </div>
                                <div class="form-group">
                                    <label for="address" class="col-md-3 control-label">
                                        Address</label>
                                    <div class="col-md-9">
                                        <asp:TextBox runat="server" ID="txtaddress" CssClass="form-control" placeholder="Address" Style="resize: none" Height="150px" OnTextChanged="txtaddress_TextChanged" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="email" class="col-md-3 control-label">
                                        Email <b class="text text-danger">*</b></label>
                                    <div class="col-md-9">
                                        <asp:TextBox runat="server" ID="txtemail" CssClass="form-control" placeholder="Email Address"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="val1" ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtemail" ErrorMessage="E-mail is Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ValidationGroup="val1" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail" ErrorMessage="You Must Enter The valid E-mail id" CssClass="text-danger" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="password" class="col-md-3 control-label">
                                        Password <b class="text text-danger">*</b></label>
                                    <div class="col-md-9">
                                        <asp:TextBox runat="server" ID="txtpassword" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="val1" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtpassword" ErrorMessage="Password is Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ValidationGroup="val1" ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtpassword" CssClass="text-danger" ValidationExpression="^[\s\S]{6,}$" ErrorMessage="Password Should be Minimum 6 Characters" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div>
                                    <asp:Label ID="lblhelp" runat="server" Style="float: right;" />
                                </div>
                                <ajax:PasswordStrength ID="pwdStrength" TargetControlID="txtpassword" StrengthIndicatorType="Text" PrefixText="Strength:" HelpStatusLabelID="lblhelp" PreferredPasswordLength="6"
                                    MinimumNumericCharacters="1" MinimumSymbolCharacters="1" TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent" TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;
AverageStrength;GoodStrength;ExcellentStrength"
                                    runat="server" />

                                <div class="form-group">
                                    <label for="confirm password" class="col-md-3 control-label">
                                        Confirm&nbsp;&nbsp;&nbsp; Password <b class="text text-danger">*</b></label>
                                    <div class="col-md-9">
                                        <asp:TextBox runat="server" ID="cpassword" CssClass="form-control" placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="val1" ID="RequiredFieldValidator7" runat="server" ControlToValidate="cpassword" ErrorMessage="Confirm your Password" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

                                        <asp:CompareValidator ValidationGroup="val1" ID="CompareValidator1" runat="server" ControlToCompare="txtpassword" ControlToValidate="cpassword" ErrorMessage="Both Password Must Be Same" CssClass="text-danger" Display="Dynamic"></asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <!-- Button -->
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="sign_up" ValidationGroup="val1" CssClass="btn btn-success" runat="server" Text="Sign Up" Height="35px" Width="150px" OnClick="Button1_Click" />
                                        <asp:Button ID="cancel" runat="server" Text="Cancel" CssClass="btn btn-danger" Height="35px" Width="150px" OnClick="Button2_Click" />
                                        &nbsp;
                                    <div style="margin-top=10px;">
                                        <br />
                                        <asp:Label CssClass="alert alert-danger  col-sm-12" ID="error" runat="server"></asp:Label>
                                    </div>
                                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                        <br />
                                        &nbsp;&nbsp;
                             <div class="form-group">
                                 <div class="col-md-12 control">
                                     <div style="padding-top: 15px; font-size: 85%">
                                         <br />
                                         Already have an accout! <a href="login.aspx">Login In Here </a>
                                     </div>
                                 </div>
                             </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

