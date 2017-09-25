<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="competencia.aspx.cs" Inherits="competencia" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-11">
            <asp:Panel ID="pnEvaluar" CssClass="panel panel-info" runat="server">
                 <div class="panel-body">
                      <label class="text-info text-center">
            <asp:Literal ID="litevaluar" runat="server"></asp:Literal></label>
                     </div>
            </asp:Panel>
        </div>
    </div>
    <div class="row">
        <div class="col-md-11">
            <h3>
                <label class="text-info">
                    <asp:Literal ID="litTitulo" runat="server"></asp:Literal></label></h3>
            <asp:HiddenField ClientIDMode="Static" ID="hdSupervisa" runat="server" />
        </div>
    </div>
      <asp:Literal ID="litCompetencia" runat="server"></asp:Literal>

     <script>
        $(function () {
            siguiente('1');
        });
    </script>
</asp:Content>
