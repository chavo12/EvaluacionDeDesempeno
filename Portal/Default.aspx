<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 <div class="row">
     <div class="col-md-12">
         <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
     </div>
 </div>
    <div class="row">
     <div class="col-md-12 margenDown">
         <asp:Literal ID="litDetalle" runat="server"></asp:Literal>
     </div>
 </div>
    <div class="row">
     <div class="col-md-11">
        <a href="/home.aspx" class="btn btn-info pull-right" role="button">  <asp:Literal ID="litIniciar" Text="Iniciar" runat="server"></asp:Literal></a>
        </div>
 </div>
    
</asp:Content>
