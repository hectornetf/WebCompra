<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutReview.aspx.cs" Inherits="WebCompra.Checkout.CheckoutReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Order Review</h1>
    <p></p>
    <h3 style="padding-left: 33px">Products:</h3>
    <asp:GridView ID="OrderItemList" runat="server" AutoGenerateColumns="False" GridLines="Both" CellPadding="10" Width="500" BorderColor="#efeeef" BorderWidth="33">              
        <Columns>
            <asp:BoundField DataField="ProdutoId" HeaderText=" ID Produto" />        
            <asp:BoundField DataField="Produto.ProdutoNome" HeaderText="Nome Produto" />        
            <asp:BoundField DataField="Produto.PrecoUnidade" HeaderText="Preço" DataFormatString="{0:c}"/>     
            <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />        
        </Columns>    
    </asp:GridView>
    <asp:DetailsView ID="InfoEnvio" runat="server" AutoGenerateRows="false" GridLines="None" CellPadding="10" BorderStyle="None" CommandRowStyle-BorderStyle="None">
        <Fields>
        <asp:TemplateField>
            <ItemTemplate>
                <h3>Informações de Envio:</h3>
                <br />
                <asp:Label ID="Nome" runat="server" Text='<%#: Eval("Nome") %>'></asp:Label>  
                <asp:Label ID="Sobrenome" runat="server" Text='<%#: Eval("Sobrenome") %>'></asp:Label>
                <p></p>
                <h3>Total:</h3>
                <br />
                <asp:Label ID="Total" runat="server" Text='<%#: Eval("Total", "{0:C}") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
          </Fields>
    </asp:DetailsView>
    <p></p>
    <hr />
    <asp:Button ID="CheckoutConfirm" runat="server" Text="Complete Order" OnClick="CheckoutConfirm_Click" />
</asp:Content>
