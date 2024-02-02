<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Product_WF._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <style>
            #Insertbtn{
                align-content:flex-start;
            }

            .auto-style1 {
                width: 441px;
                height: 24px;
            }
            .auto-style2 {
                height: 24px;
            }

            .auto-style3 {
                background-color: #CCFFFF;
            }
            .auto-style4 {
                padding:7px;
                background-color: #FFCC99;
            }
            .auto-style5 {
                background-color: #CCFFCC;
            }
            td{
                justify-items:center;
            }

            .auto-style7 {
                padding:7px;
                width: 441px;
                height: 41px;
            }
            .auto-style8 {
                height: 41px;
            }
            .auto-style9 {
                width: 241px;
                height: 46px;
            }
            .auto-style10 {
                height: 46px;
            }

        </style>
        <table class="w-100">
            <tr>
                <td style="width: 406px">&nbsp;</td>
            </tr>
    </table>
    </main>
            <div style=" font-size:x-large;text-align:center;" class="auto-style5">CRUD OPERATIONS ASP.NET WEB FORMS WITH STORED PROCEDURES</div>
            <main style="background-color:bisque;">
        <table class="w-100" style="justify-items:center;">
            <tr>
                <td style="width: 441px">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 441px; height: 48px;padding:7px;">
                    <strong>
                    <asp:Label ID="PIdLbl" runat="server" Text="Product ID" CssClass="auto-style4"></asp:Label>
                    </strong>
                </td>
                <td style="height: 48px">
                    <asp:TextBox ID="pIDtbx" runat="server" Height="33px" Width="245px" CssClass="auto-style3" ></asp:TextBox>
                    <asp:Label ID="ErrorMessageLabel" runat="server" Text="" ForeColor="Red"></asp:Label>

                </td>
            </tr>
            <tr>
                <td style="width: 441px">&nbsp;</td>
                <td>
                    <Bold>
                    <asp:Button ID="Button1" runat="server" Text="Search" Width="87px" OnClick="Search_Product" style="font-size: medium; background-color: #66FF66" />
                    </Bold>
                </td>
            </tr>
            <tr>
                <td style="width: 441px; height: 48px;padding:7px;">
                    <strong>
                    <asp:Label ID="PIdLbl0" runat="server" Text="Item Name" CssClass="auto-style4"></asp:Label>
                    </strong>
                </td>
                <td style="height: 48px">
                    <asp:TextBox ID="Pnametbx" runat="server" Height="33px" Width="245px" CssClass="auto-style3" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 441px; height: 54px;padding:7px;">
                    <strong>
                    <asp:Label ID="PIdLbl1" runat="server" Text=" Specification" CssClass="auto-style4"></asp:Label>
                    </strong>
                </td>
                <td style="height: 54px">
                    <asp:TextBox ID="SpecTbx" runat="server" Height="33px" Width="245px" CssClass="auto-style3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="display:flex;justify-content:space-between;padding:7px;" class="auto-style9">
                    <strong>
                    <asp:Label ID="Label1" runat="server" CssClass="auto-style4" Text="Unit"></asp:Label>
                    </strong></td>
                <td class="auto-style10">
                    <asp:DropDownList ID="DropDownList" runat="server" CssClass="auto-style3">
                        <asp:ListItem Value="1">PC1</asp:ListItem>
                        <asp:ListItem Value="5">PC5</asp:ListItem>
                        <asp:ListItem Value="10">PC10</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 441px; height: 65px; padding: 7px;">
                    <strong>
                        <asp:Label ID="Label2" runat="server" CssClass="auto-style4" Text="Status"></asp:Label>
                    </strong>

                </td>
                <td style="height: 65px">
                    <strong>
                        <asp:RadioButton ID="running" runat="server" CssClass="auto-style3" Text="Running" GroupName="statusGroup" />
                        <br />
                        <asp:RadioButton ID="unused" runat="server" CssClass="auto-style3" Text="Unused" GroupName="statusGroup" />

                    </strong>
                </td>

            </tr>

            <tr>
                <td class="auto-style7">
                    <strong>
                    <asp:Label ID="CreatLbl" runat="server" Text=" Creation Date" CssClass="auto-style4"></asp:Label>
                    </strong>
                </td>
                <td class="auto-style8">
                    <strong>
                    <asp:TextBox ID="DateTxt" runat="server" Height="31px" Width="245px" CssClass="auto-style3"></asp:TextBox>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td style="width: 361px; justify-content:space-between;">
                    <asp:Button ID="Insertbtn" runat="server" style="background-color: #0066FF" Text="Insert" OnClick="InsertProduct" />
                    <asp:Button ID="updatebtn" runat="server" style="background-color: #00FFCC" Text="Update" />
                    <asp:Button ID="Deletebtn" runat="server" style="background-color: #FF4444" Text="Delete" OnClick="DeletProduct" />
                    <asp:Button ID="Listbtn" runat="server" style="background-color: #FFFF00" Text="ListData" />
                </td>
            </tr>
            </table>
                <br />
        <div style="padding-left:30px;">
            <br />
        </div>
    </main>

</asp:Content>
