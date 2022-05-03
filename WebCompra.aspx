<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebCompra.aspx.cs" Inherits="WebCompra.WebCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="WebCompraTitulo" runat="server" class="ContentHead">
        <h1>Web Compra</h1>
    </div>
    <asp:GridView ID="CompraLista"
        runat="server" AutoGenerateColumns="False" ShowFooter="True" GridLines="Vertical" CellPadding="4"
        ItemType="WebCompra.Models.CompraItem" SelectMethod="GetShoppingCartItems"
        CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="ProdutoID" HeaderText="ID" SortExpression="ProdutoID" />
            <asp:BoundField DataField="Produto.ProdutoNome" HeaderText="Nome" />
            <asp:BoundField DataField="Produto.PrecoUnidade" HeaderText="Preco (each)" DataFormatString="{0:c}" />
            <asp:TemplateField HeaderText="Quantidade">
                <ItemTemplate>
                    <asp:TextBox ID="PuxarQuantidade" Width="40" runat="server" Text="<%#: Item.Quantidade %>"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Total">
                <ItemTemplate>
                    <%#: String.Format("{0:c}", ((Convert.ToDouble(Item.Quantidade)) *  Convert.ToDouble(Item.Produto.PrecoUnidade)))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remove Item">
                <ItemTemplate>
                    <asp:CheckBox ID="Remove" runat="server"></asp:CheckBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div>
        <p></p>
        <strong>
            <asp:Label ID="LabelTotalText" runat="server" Text="Total: "></asp:Label>
            <asp:Label ID="lblTotal" runat="server" EnableViewState="false"></asp:Label>
        </strong>
    </div>
    <br />
</asp:Content>
