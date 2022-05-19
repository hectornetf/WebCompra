using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebCompra.Models
{
    public class ProdutoContext :DbContext
    {
        public ProdutoContext() : base("WebCompra")
            { }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CompraItem> CompraItems { get; set; }

    }
}