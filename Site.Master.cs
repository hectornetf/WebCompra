using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCompra.Models;

namespace WebCompra
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public IQueryable<Categoria> GetCategorias()
        {
            var _db = new Models.ProdutoContext();
            IQueryable<Categoria> query = _db.Categorias;
            return query;
        }
    }
}