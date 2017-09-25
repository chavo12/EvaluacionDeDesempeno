<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="pdfResponsabilidad.aspx.cs" Inherits="pdfResponsabilidad" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-10">
            <asp:Panel ID="pnEvaluar" CssClass="panel panel-info" runat="server">
                 <div class="panel-body">
                      <label class="text-info text-center">
            <asp:Literal ID="litevaluar" runat="server"></asp:Literal></label>
                     </div>
            </asp:Panel>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <asp:Literal ID="litSubtitulo" runat="server"></asp:Literal>
        </div>
    </div>
    <asp:Literal ID="litResponsabilidades" runat="server"></asp:Literal>
    <script>
        $(function () {
          
        });
    </script>
</asp:Content>
