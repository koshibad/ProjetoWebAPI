using Fiap.DesenvolvimentoWeb.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fiap.DesenvolvimentoWeb.WebApi.Controllers
{
    public class ProdutosController : ApiController
    {
        static readonly IProdutosRepositorio repositorio = new ProdutosRepositorio();

        public IEnumerable<Produtos> GetAllProducts()
        {
            return repositorio.BuscarTodos();
        }

        public Produtos GetProduct(int id)
        {
            Produtos item = repositorio.Buscar(id);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return item;
        }

        public HttpResponseMessage PostProduct(Produtos item)
        {
            item = repositorio.Adicionar(item);
            var response = Request.CreateResponse<Produtos>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage PutProduct(int id, Produtos item)
        {
            Produtos prod = repositorio.Buscar(id);
            item.Id = id;
            prod.Descricao = item.Descricao;
            prod.DataCriacao = item.DataCriacao;
            prod.Preco = item.Preco;

            repositorio.Atualizar(prod);
            var response = Request.CreateResponse<Produtos>(HttpStatusCode.NoContent, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void DeleteProduct(int id)
        {
            Produtos item = repositorio.Buscar(id);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            repositorio.Remover(item);
        }

        public IEnumerable<Produtos> GetProductsByCategory(string descricao)
        {
            return repositorio.BuscarTodos().Where(p => string.Equals(
                p.Descricao, descricao, StringComparison.OrdinalIgnoreCase));
        }
    }
}
