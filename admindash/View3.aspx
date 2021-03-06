﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View3.aspx.cs" MasterPageFile="~/admindash/MasterPage.master" Inherits="admindash_Default6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2>Request Details:</h2>
        </div>
    </div>
    <!-- /. ROW  -->
    <hr />
    <br />
    <div>
        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:myad5ConnectionString %>" SelectCommand="getRek" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter Name="rid" QueryStringField="rid" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="gridView" align="center" Width="100%" CellPadding="4" DataKeyNames="frmcod" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="Both">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="frmcod" HeaderText="Request ID" SortExpression="frmcod" InsertVisible="False" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Title" SortExpression="">
                        <ItemTemplate>
                            <div style="width: 100%; overflow: hidden; white-space: nowrap; text-overflow: ellipsis" title='<%#Eval("frmtit") %>'>
                                <%# Eval("frmtit") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="frmsubmitdate" HeaderText="Request date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="frmstatus" HeaderText="Request Status" SortExpression="frmstatus" />
                    <asp:BoundField DataField="frmdate" HeaderText="Deadline" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="frmreqcont" HeaderText="Content Required" />

                    <asp:BoundField DataField="frmurl" HeaderText="Refrence URL's" Visible="false" SortExpression="frmurl" />
                    <asp:BoundField DataField="frmdesc" HeaderText="Description" Visible="false" SortExpression="frmdesc" />
                    <asp:BoundField DataField="pdt" HeaderText="Request Completion date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="pdt" />
                    <asp:BoundField DataField="rdt" HeaderText="User Response date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="rdt" />
                    <asp:TemplateField HeaderText="Image URL" SortExpression="imgurl">
                        <ItemTemplate>
                            <div style="width: 100%; overflow: hidden; white-space: nowrap; text-overflow: ellipsis" title='<%#Eval("frmimg") %>'>
                                <%# Eval("frmimg") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Video URL" SortExpression="vidurl">
                        <ItemTemplate>
                            <div style="width: 100%; overflow: hidden; white-space: nowrap; text-overflow: ellipsis" title='<%#Eval("frmvideo") %>'>
                                <%# Eval("frmvideo") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="frmregcod" HeaderText="User ID" SortExpression="frmregcod" />



                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#0000FF" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#0000FF" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>
    <br />
    <div>
        <asp:Label ID="RefU" runat="server" Text="Reference URL's:"></asp:Label>
        <asp:TextBox ID="RefUrl" runat="server" Height="100px" Width="100%" Style="resize: none" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>

    </div>
    <br />

    <div>
        <asp:Label ID="Des" runat="server" Text="Description:"></asp:Label>
        <asp:TextBox ID="Desc" runat="server" Height="200px" Width="100%" Style="resize: none" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>


    </div>
    <hr />
    <br />
    <div class="container" >
        <div class="row">
            <div class="row-md-8" style="width: 100%;">
                <div id="main-contact-form" style="height: 360px; width: 50%; float: left;" class="w3-panel w3-light-grey w3-border">
                    <asp:Label ID="Label3" runat="server"></asp:Label>

                    <asp:DataList ID="dl" runat="server">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <img src="<%#Eval("frmimg") %>" height="240px" width="426px" /></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>

                </div>
                <div id="main-contact-form1" style="height: 360px; width: 50%; float: right;" class="w3-panel w3-light-grey w3-border">

                    <asp:Label ID="Label4" runat="server"></asp:Label>

                    <asp:DataList ID="dl2" runat="server">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <video controls height="240px" width="426px">
                                            <source src="<%# Eval("frmvideo") %>" type="video/mp4"/></video>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>


                    <br />
                </div>




            </div>

        </div>
    </div>
    <div>
        <asp:Button ID="DelR" runat="server" Text="Delete Request" OnClick="DelR_Click" />
    </div>
</asp:Content>
