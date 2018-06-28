using E_Vent.Areas.Admin.Models.Connection;
using E_Vent.Models.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Vent.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (PublicacaoConnection publicacao = new PublicacaoConnection())
            using (TipoImovelConnection tipoImovel = new TipoImovelConnection())
            using (EstadoConnection estados = new EstadoConnection())
            using (CidadeConnection cidades = new CidadeConnection())
            {
                ViewBag.Cidade = cidades.ReadAll();
                ViewBag.Estado = estados.ReadAll();
                ViewBag.TipoImovel = tipoImovel.ReadAll();
                ViewBag.Publicacoes = publicacao.Publicacoes();
                return View();
            }
        }


        [AuthFilter]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AuthFilter]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}