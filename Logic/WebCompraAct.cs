using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCompra.Models;

namespace WebCompra.Logic
{
    public class WebCompraAct : IDisposable
    {
        public string WebCompraId { get; set; }
        private ProdutoContext _db = new ProdutoContext();
        public const string WebCompraSessionKey = "CompraId";
        public void AddToCompra(int id)
        {
            //Recupera o produto da database
            WebCompraId = GetCompraId();

            var compraItem = _db.CompraItems.SingleOrDefault(
                c => c.CompraId == WebCompraId
                && c.ProdutoId == id);
                if (compraItem == null)
            {
                //Criar novo item se não houver nenhum
                compraItem = new CompraItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProdutoId = id,
                    CompraId = WebCompraId,
                    Produto = _db.Produtos.SingleOrDefault(
                        p => p.ProdutoID == id),
                    Quantidade = 1,
                    DataCriada = DateTime.Now
                };
                
                _db.CompraItems.Add(compraItem);

            }
                else
            {
                //Se houver item no carrinho adicionar quantidade
                compraItem.Quantidade++;
            }
                _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public string GetCompraId()
        {
            if (HttpContext.Current.Session[WebCompraSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[WebCompraSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    // Gera um novo GUID aleatório usando a classe System.Guid.
                    Guid tempWebCompraId = Guid.NewGuid();
                    HttpContext.Current.Session[WebCompraSessionKey] = tempWebCompraId.ToString();
                }
            }
            return HttpContext.Current.Session[WebCompraSessionKey].ToString();
        }

        public List<CompraItem> GetCompraItems()
        {
            WebCompraId = GetCompraId();
            return _db.CompraItems.Where(
                c => c.CompraId == WebCompraId).ToList();
        }
    }
}