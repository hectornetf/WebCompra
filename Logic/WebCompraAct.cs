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

        public decimal GetTotal()
        {
            WebCompraId = GetCompraId();

            // Multiplique o preço do produto pela quantidade desse produto para obter
            // o preço atual de cada um desses produtos no carrinho.
            // Soma todos os totais de preços do produto para obter o total do carrinho.

            decimal? total = decimal.Zero;
            total = (decimal?)(from compraItems in _db.CompraItems
                               where compraItems.CompraId == WebCompraId
                               select (int?)compraItems.Quantidade *
                               compraItems.Produto.PrecoUnidade).Sum();
            return total ?? decimal.Zero;
        }

        public WebCompraAct GetCompra(HttpContext context)
        {
            using (var compra = new WebCompraAct())
            {
                compra.WebCompraId = compra.GetCompraId();
                return compra;
            }
        }

        public void AtualizarAtualizarWebCompraDatabase(String compraId, AtualizarWebCompra[] CompraItemUpdates)
        {
            using (var db = new Models.ProdutoContext())
            {
                try
                {
                    int CompraItemCount = CompraItemUpdates.Count();
                    List<CompraItem> myCompra = GetCompraItems();
                    foreach (var compraItem in myCompra)
                    {
                        // Iterar por todas as linhas na lista do carrinho de compras
                        for (int i = 0; i < CompraItemCount; i++)
                        {
                            if (compraItem.Produto.ProdutoID == CompraItemUpdates[i].ProdutoId)
                            {
                                if (CompraItemUpdates[i].PuxarQuantidade < 1 || CompraItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(compraId, compraItem.ProdutoId);
                                }
                                else
                                {
                                    AtualizarItem(compraId, compraItem.ProdutoId, CompraItemUpdates[i].PuxarQuantidade);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("Erro: Não foi possível atualizar o banco de dados do carrinho -" + exp.Message.ToString(), exp);
                }
            }
        }

        public void RemoveItem(string removeCompraID, int removeProdutoID)
        {
            using (var _db = new Models.ProdutoContext())
            {
                try
                {
                    var myItem = (from c in _db.CompraItems where c.CompraId == removeCompraID && c.Produto.ProdutoID == removeProdutoID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        //Remove item.
                        _db.CompraItems.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERRO: Não foi possível remover o item do carrinho - " + exp.Message.ToString(), exp);
                }

            }
        }

        public void AtualizarItem(string atualizarCompraID, int atualizarProdutoId, int quantidade)
        {
            using (var _db = new Models.ProdutoContext())
            {
                try
                {
                    var myItem = (from c in _db.CompraItems where c.CompraId == atualizarCompraID && c.Produto.ProdutoID == atualizarProdutoId select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantidade = quantidade;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("Erro: Não foi possível atualizar o item do carrinho -" + exp.Message.ToString(), exp);
                }
            }
        }
        public void CarrinhoVazio()
        {
            WebCompraId = GetCompraId();
            var compraItems = _db.CompraItems.Where(c => c.CompraId == WebCompraId);
            foreach (var compraItem in compraItems)
            {
                _db.CompraItems.Remove(compraItem);
            }
            //Salva alterações.
            _db.SaveChanges();
        }

        public int GetCount()
        {
            WebCompraId = GetCompraId();
            // Obtém a contagem de cada item no carrinho e soma-os
            int? count = (from compraItem in _db.CompraItems
                          where compraItem.CompraId == WebCompraId
                          select (int?)compraItem.Quantidade).Sum();
            //Retorna 0 se todas as entradas forem nulas
            return count ?? 0;
        }

        public struct AtualizarWebCompra
        {
            public int ProdutoId;
            public int PuxarQuantidade;
            public bool RemoveItem;
        }
    }
}