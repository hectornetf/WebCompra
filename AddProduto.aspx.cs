using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCompra.Logic;

namespace WebCompra
{
    public partial class AddProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string produtoAct = Request.QueryString["ProdutoAct"];
            if (produtoAct == "adicionar")
            {
                LabelAddStatus.Text = "Produto adicionado!";
            }
            if (produtoAct == "remover")
            {
                LabelRemoveProduto.Text = "Produto removido!";
            }

        }

        protected void AddProdutoButton_Click(object sender, EventArgs e)
        {
            Boolean fileOK = false;
            String path = Server.MapPath("~/Catalog/Images/");
            if (ProdutoImage.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(ProdutoImage.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])  
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    //Salvar imagens no diretorio
                    ProdutoImage.PostedFile.SaveAs(path + ProdutoImage.FileName);
                    //Salvar Images/Thumbs no diretorio
                    ProdutoImage.PostedFile.SaveAs(path + "Thumbs/" + ProdutoImage.FileName);
                }
                 catch (Exception ex)
                {
                    LabelAddStatus.Text = ex.Message;
                }

                //Adicionar produto ao DB
                AddProdutos produtos = new AddProdutos();
                bool addSucess = produtos.AddProduto(AddProdutoNome.Text, AddProdutoDescricao.Text,
                    AddProdutoPreco.Text, DropDownAddCategoria.SelectedValue, ProdutoImage.FileName);
                if (addSucess)
                {
                    //Atualizar pagina
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProdutoAct=adicionar");
                }
                else
                {
                    LabelAddStatus.Text = "Não foi possível adicionar novo produto ao banco de dados.";
                }
            }
            else
            {
                LabelAddStatus.Text = "Não é possível aceitar o tipo de arquivo.";
            }
        }

        public IQueryable GetCategorias()
        {
            var _db = new Models.ProdutoContext();
            IQueryable query = _db.Categorias;
            return query;
        }

        public IQueryable GetProdutos()
        {
            var _db = new Models.ProdutoContext();
            IQueryable query = _db.Produtos;
            return query;
        }

        protected void RemoveProdutoButton_Click(object sender, EventArgs e)
        {
            using (var _db = new Models.ProdutoContext())
            {
                int produtoId = Convert.ToInt16(DropDownRemoveProduto.SelectedValue);
                var myItem = (from c in _db.Produtos where c.ProdutoID == produtoId select c).FirstOrDefault();
                if (myItem != null)
                {
                    _db.Produtos.Remove(myItem);
                    _db.SaveChanges();

                    //Atualiza Pagina
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProdutoAct=remover");
                }
                else
                {
                    LabelRemoveStatus.Text = "Não foi possível localizar o produto.";
                }
            }
        }
    }
}