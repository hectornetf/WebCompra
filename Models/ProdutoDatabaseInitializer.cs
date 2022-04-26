using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebCompra.Models
{
    public class ProdutoDatabaseInitializer : DropCreateDatabaseIfModelChanges<ProdutoContext>
    {
        protected override void Seed(ProdutoContext context)
        {
            GetCategorias().ForEach(c => context.Categorias.Add(c));
            GetProdutos().ForEach(p => context.Produtos.Add(p));
        }

        private static List<Categoria> GetCategorias()
        {
            var categorias = new List<Categoria>
            {
                   new Categoria
                {
                    CategoriaID = 1,
                    CategoriaNome = "Computador"
                },
                   new Categoria
                {
                    CategoriaID = 2,
                    CategoriaNome = "Mouse"
                },
                   new Categoria
                {
                    CategoriaID = 3,
                    CategoriaNome = "Teclado"
                },
                   new Categoria
                {
                    CategoriaID = 4,
                    CategoriaNome = "Monitor"
                },
                   new Categoria
                {
                    CategoriaID = 5,
                    CategoriaNome = "Notebook"
                },
            };
            return categorias;
        }

        private static List<Produto> GetProdutos()
        {
            var produtos = new List<Produto>
            {
                new Produto
                {
                    ProdutoID = 1,
                    ProdutoNome = "Desktop Dell",
                    Descricao = "Dell",
                    ImagePath = "computadores.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 1,

                },
                   new Produto
                {
                    ProdutoID = 2,
                    ProdutoNome = "Desktop Hp",
                    Descricao = "Hp",
                    ImagePath = "computadores.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 1,

                },
                   new Produto
                {
                    ProdutoID = 3,
                    ProdutoNome = "Desktop Asus",
                    Descricao = "Asus",
                    ImagePath = "computadores.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 1,

                },
                   new Produto
                {
                    ProdutoID = 4,
                    ProdutoNome = " Desktop Intel",
                    Descricao = "Intel",
                    ImagePath = "computadores.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 1,

                },
                   new Produto
                {
                    ProdutoID = 5,
                    ProdutoNome = "Desktop Lenovo ",
                    Descricao = "Lenovo",
                    ImagePath = "computadores.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 1,

                },
                new Produto
                {
                    ProdutoID = 6,
                    ProdutoNome = "Mouse Dell",
                    Descricao = "Dell",
                    ImagePath = "mouse.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 2,

                },
                   new Produto
                {
                    ProdutoID = 7,
                    ProdutoNome = "Mouse Hp",
                    Descricao = "Hp",
                    ImagePath = "mouse.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 2,

                },
                   new Produto
                {
                    ProdutoID = 8,
                    ProdutoNome = "Mouse Asus",
                    Descricao = "Asus",
                    ImagePath = "mouse.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 2,

                },
                   new Produto
                {
                    ProdutoID = 9,
                    ProdutoNome = " Mouse Intel",
                    Descricao = "Intel",
                    ImagePath = "mouse.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 2,

                },
                   new Produto
                {
                    ProdutoID = 10,
                    ProdutoNome = "Teclado Lenovo ",
                    Descricao = "Lenovo",
                    ImagePath = "teclado.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 3,

                },
                   new Produto
                {
                    ProdutoID = 11,
                    ProdutoNome = "Monitor Dell",
                    Descricao = "Dell",
                    ImagePath = "monitor.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 4,

                },
                   new Produto
                {
                    ProdutoID = 12,
                    ProdutoNome = "Notebook Hp",
                    Descricao = "Hp",
                    ImagePath = "notebook.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 5,

                },
                   new Produto
                {
                    ProdutoID = 13,
                    ProdutoNome = "Notebook Asus",
                    Descricao = "Asus",
                    ImagePath = "notebook.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 5,

                },
                   new Produto
                {
                    ProdutoID = 14,
                    ProdutoNome = " Notebook Intel",
                    Descricao = "Intel",
                    ImagePath = "notebook.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 5,

                },
                   new Produto
                {
                    ProdutoID = 15,
                    ProdutoNome = "Notebook Lenovo ",
                    Descricao = "Lenovo",
                    ImagePath = "notebook.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 5,

                },
                   new Produto
                {
                    ProdutoID = 16,
                    ProdutoNome = "Notebook Lenovo ",
                    Descricao = "Lenovo",
                    ImagePath = "notebook.png",
                    PrecoUnidade = 3.500,
                    CategoriaID = 5,

                },

            };
            return produtos;
        }
    }
}