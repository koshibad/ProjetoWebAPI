using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fiap.DesenvolvimentoWeb.WebApi.Models
{
    public class ProdutosRepositorio : IProdutosRepositorio
    {
        DbProdutosEntities ctx = new DbProdutosEntities();

        public IEnumerable<Produtos> BuscarTodos()
        {
            return ctx.Produtos;
        }

        public Produtos Buscar(int id)
        {
            return ctx.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public Produtos Adicionar(Produtos item)
        {
            ctx.Produtos.Add(item);
            ctx.SaveChanges();
            return item;
        }

        public void Remover(Produtos item)
        {
            ctx.Produtos.Remove(item);
            ctx.SaveChanges();
        }

        public bool Atualizar(Produtos item)
        {
            try
            {
                ctx.Entry<Produtos>(item).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}