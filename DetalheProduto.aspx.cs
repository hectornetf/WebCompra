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
    public partial class DetalheProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public IQueryable<Produto> GetProdutos([QueryString("produtoID")] int? produtoID)
        {
            var _db = new Models.ProdutoContext();
            IQueryable<Produto> query = _db.Produtos;
            if (produtoID.HasValue && produtoID > 0)
            {
                query = query.Where(p => p.ProdutoID == produtoID);
            }
            else
            {
                query = null;
            }
            return query;
        }
    }
}