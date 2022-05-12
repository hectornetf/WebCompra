<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduto.aspx.cs" Inherits="WebCompra.AddProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Adicionar Produtos</h1>
    <hr />
    <h3>Add Produto:</h3>
    <table>
        <tr>
            <td><asp:Label ID="LabelAddCategoria" runat="server">Categoria:</asp:Label></td>
            <td>
                <asp:DropDownList ID="DropDownAddCategoria" runat="server" 
                    ItemType="WebCompra.Models.Categoria" 
                    SelectMethod="GetCategorias" DataTextField="CategoriaNome" 
                    DataValueField="CategoriaID" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelAddNome" runat="server">Nome:</asp:Label></td>
            <td>
                <asp:TextBox ID="AddProdutoNome" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="* Requer nome do produto." ControlToValidate="AddProdutoNome" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelAddDescricao" runat="server">Descricao:</asp:Label></td>
            <td>
                <asp:TextBox ID="AddProdutoDescricao" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="* Requer Descrição." ControlToValidate="AddProdutoDescricao" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelAddPreco" runat="server">Preço:</asp:Label></td>
            <td>
                <asp:TextBox ID="AddProdutoPreco" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="* Preço requerido." ControlToValidate="AddProdutoPreco" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="* Coloque valor aproximado." ControlToValidate="AddProdutoPreco" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LabelAddImageFile" runat="server">Imagem:</asp:Label></td>
            <td>
                <asp:FileUpload ID="ProdutoImage" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="* Requer imagem." ControlToValidate="ProdutoImage" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <p></p>
    <p></p>
    <asp:Button ID="AddProdutoButton" runat="server" Text="Adicione o Produto" OnClick="AddProdutoButton_Click"  CausesValidation="true"/>
    <asp:Label ID="LabelAddStatus" runat="server" Text=""></asp:Label>
    <p></p>
    <h3>Remove Produto:</h3>
    <table>
        <tr>
            <td><asp:Label ID="LabelRemoveProduto" runat="server">Produto:</asp:Label></td>
            <td><asp:DropDownList ID="DropDownRemoveProduto" runat="server" ItemType="WebCompra.Models.Produto" 
                    SelectMethod="GetProdutos" AppendDataBoundItems="true" 
                    DataTextField="ProdutoNome" DataValueField="ProdutoID" >
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <p></p>
    <asp:Button ID="RemoveProdutoButton" runat="server" Text="Remove Produto" OnClick="RemoveProdutoButton_Click" CausesValidation="false"/>
    <asp:Label ID="LabelRemoveStatus" runat="server" Text=""></asp:Label>
</asp:Content>
