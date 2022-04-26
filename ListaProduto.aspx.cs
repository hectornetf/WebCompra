using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCompra.Models;

namespace WebCompra
{
    public partial class ListaProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Produto> GetProdutos([QueryString("id")] int? categoriaId)
        {
            var _db = new WebCompra.Models.ProdutoContext();
            IQueryable<Produto> query = _db.Produtos;
            if (categoriaId.HasValue && categoriaId > 0)
            {
                query = query.Where(p => p.CategoriaID == categoriaId);
            }
            return query;
        }
    }
}