using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCompra.Logic;

namespace WebCompra
{
    public partial class AddToCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawId = Request.QueryString["ProdutoId"];
            int produtoId;
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out produtoId))
            {
                using (WebCompraAct usersCompraAct = new WebCompraAct())
                {
                    usersCompraAct.AddToCompra(Convert.ToInt16(rawId));
                }
            }
            else
            {
                Debug.Fail("Erro : Nunca devemos acessar AddToCompra.aspx sem um ProdutoId.");
                throw new Exception("Erro : Carregamento so pode ser feito apos definir um ProdutoId.");
            }
            Response.Redirect("WebCompraCarrinho.aspx");
        }

    }
}