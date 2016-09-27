using Fiap.DesenvolvimentoWeb.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Fiap.DesenvolvimentoWeb.ClienteWebApi.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client;
        Uri produtoUri;
        String msg = "";

        public HomeController()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:57573/");
                client.DefaultRequestHeaders.Accept.Add(new
                    System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Executar()
        {
            System.Net.Http.HttpResponseMessage response = client.GetAsync("api/produtos").Result;

            if (response.IsSuccessStatusCode)
            {
                //obtendo o cabeçalho da resposta
                produtoUri = response.Headers.Location;
                //obtendo os dados do Rest –
                //instalar Microsoft.AspNet.WebApi.Client
                return View(response.Content.ReadAsAsync<IEnumerable<Produtos>>().Result);
            }
            else
            {
                msg = response.StatusCode.ToString() + " - " + response.ReasonPhrase;
                ViewBag.Erro = msg;
                return View("Error");
            }
        }

        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Produtos p)
        {
            HttpResponseMessage response = client.GetAsync("api/produtos").Result;
            response = client.PostAsJsonAsync("api/produtos", p).Result;
            if (response.IsSuccessStatusCode)
            {
                produtoUri = response.Headers.Location;
                return View();
            }
            else
            {
                msg = response.StatusCode.ToString() + " - " +
               response.ReasonPhrase;
                ViewBag.Erro = msg;
                return View("Error");
            }
        }

        public ActionResult Editar(int id)
        {
            return View(new Produtos());
        }

        [HttpPost]
        public ActionResult Editar(int id, Produtos p)
        {
            HttpResponseMessage response = client.GetAsync("api/produtos").Result;
            response = client.PutAsJsonAsync("api/produtos/" + id, p).Result;
            if (response.IsSuccessStatusCode)
            {
                produtoUri = response.Headers.Location;
                return RedirectToAction("Executar", "Home");
            }
            else
            {
                msg = response.StatusCode.ToString() + " - " +
               response.ReasonPhrase;
                ViewBag.Erro = msg;
                return View("Error");
            }
        }

        public ActionResult Excluir(int id)
        {
            return View(new Produtos());
        }

        [HttpPost]
        public ActionResult Excluir(int id, Produtos p)
        {
            HttpResponseMessage response = client.GetAsync("api/produtos").Result;
            response = client.DeleteAsync("api/produtos/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                produtoUri = response.Headers.Location;
                return RedirectToAction("Executar", "Home");
            }
            else
            {
                msg = response.StatusCode.ToString() + " - " +
               response.ReasonPhrase;
                ViewBag.Erro = msg;
                return View("Error");
            }
        }
    }
}