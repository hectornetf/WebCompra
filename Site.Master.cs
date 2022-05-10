using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCompra.Logic;
using WebCompra.Models;

namespace WebCompra
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
            {
            using (WebCompraAct usersWebCompraCarrinho = new WebCompraAct())
            {
                string carrinhoStr = string.Format("Carrinho ({0})", usersWebCompraCarrinho.GetCount());
                carrinhoContador.InnerText = carrinhoStr;
            }
        }
        
        public IQueryable<Categoria> GetCategorias()
        {
            var _db = new Models.ProdutoContext();
            IQueryable<Categoria> query = _db.Categorias;
            return query;
        }
    }
}