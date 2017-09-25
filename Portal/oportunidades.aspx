<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="oportunidades.aspx.cs" Inherits="oportunidades" %>

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
            <h3>
                <label class="text-info">
                    <asp:Literal ID="litTitulo" runat="server"></asp:Literal></label></h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10">
            <h5>
                <label class="text-info">
                    <asp:Literal ID="litSubtitulo" runat="server"></asp:Literal></label></h5>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <textarea rows="10" placeholder="Escriba su respuesta aquí" class="textarea form-control" <asp:Literal ID="litLectura" runat="server"></asp:Literal> id="escrito"> <asp:Literal ID="litEscrito" runat="server"></asp:Literal></textarea>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <asp:HiddenField ID="id" ClientIDMode="Static" runat="server" />
              <asp:Literal ID="litGuardar" runat="server"></asp:Literal>
             
            <a href="/desempeno.aspx" class="btn btn-info pull-right" onclick="javascript:responderOportunidad();" role="button"> <asp:Literal ID="litSiguiente" Text="Siguiente" runat="server"></asp:Literal></a>
            <a href="/competencia.aspx" class="btn btn-info pull-right" onclick="javascript:responderOportunidad();" role="button">Anterior</a>
              <asp:Literal ID="litImprimir" runat="server"></asp:Literal>
        </div>
    </div>

</asp:Content>
