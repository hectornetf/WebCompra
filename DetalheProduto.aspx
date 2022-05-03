<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalheProduto.aspx.cs" Inherits="WebCompra.DetalheProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="detalheProduto" runat="server" ItemType="WebCompra.Models.Produto" SelectMethod ="GetProdutos" RenderOuterTable="false">
        <ItemTemplate>
            <div>
                <h1><%#:Item.ProdutoNome %></h1>
            </div>
            <br />
            <table>
                <tr>
                    <td>
                        <img src="/Catalog/Images/Thumbs/<%#:Item.ImagePath %>" style="border:solid; height:300px" alt="<%#:Item.ProdutoNome %>"/>
                    </td>
                    <td>&nbsp;</td>  
                    <td style="vertical-align: top; text-align:left;">
                        <b>Descrição:</b><br /><%#:Item.Descricao %>
                        <br />
                        <span><b>Preço:</b>&nbsp;<%#: String.Format("{0:c}", Item.PrecoUnidade) %></span>
                        <br />
                        <span><b>Numero do produto:</b>&nbsp;<%#:Item.ProdutoID %></span>
                        <br />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
