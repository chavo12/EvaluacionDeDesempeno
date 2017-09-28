<%@ Page Language="C#"  MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="logon.aspx.cs" Inherits="logon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
        <div class="panel panel-info">
            <div class="panel-heading">
                <label>Iniciar sesión</label>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:TextBox ID="txtDomain" placeholder="Dominio" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:TextBox ID="txtUsername" placeholder="Cuenta" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:TextBox ID="txtPassword" placeholder="Clave" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Button ID="btnLogin" CssClass="btn btn-info" runat="server" Text="Login" OnClick="Login_Click"></asp:Button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="errorLabel" runat="server" ForeColor="#ff3300"></asp:Label>
                        <asp:CheckBox ID="chkPersist" Visible="false" runat="server" Text="Persist Cookie" />

                    </div>
                </div>
            </div>
        </div>
                </div>
            <div class="col-md-4"></div>
    </div>
       </asp:Content>
