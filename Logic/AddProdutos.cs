using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCompra.Models;

namespace WebCompra.Logic
{
    public class AddProdutos
    {
        public bool AddProduto(string ProdutoNome, string ProdutoDesc, string ProdutoPreco, string ProdutoCategoria, string ProdutoImage)
        {
            var myProduto = new Produto();
            myProduto.ProdutoNome = ProdutoNome;
            myProduto.Descricao = ProdutoDesc;
            myProduto.PrecoUnidade = Convert.ToDouble(ProdutoPreco);
            myProduto.ImagePath = ProdutoImage;
            myProduto.CategoriaID = Convert.ToInt32(ProdutoCategoria);

            using (ProdutoContext _db = new ProdutoContext())
            {
                //Add produto a DB
                _db.Produtos.Add(myProduto);
                _db.SaveChanges();
            }
            //Sucesso.
            return true;

        }
    }
}