<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logon.aspx.cs" Inherits="logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>EVALUACION ANUAL DE DESEMPEÑO</title>
    <link href="Content/favicon.png" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="Content/Site.css" type="text/css" media="screen">
    <link rel="stylesheet" href="Content/bootstrap.css" type="text/css" media="screen">
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
  function resizeIframe(obj) {
    obj.style.height = obj.contentWindow.document.body.scrollHeight + 'px';
  }
</script>
</head>
<body  class="cuerpo" >
    <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
        <div class="panel panel-default">
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
        </div>
 </form>
</body>
</html>
