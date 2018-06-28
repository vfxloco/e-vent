using E_Vent.Areas.Admin.Models;
using E_Vent.Areas.Admin.Models.Connection;
using E_Vent.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Areas.Admin.Controllers
{
    [AuthFilter]
    public class CidadeController : Controller
    {
        // GET: Admin/Cidade
        public ActionResult Index()
        {
            using (EstadoConnection estados = new EstadoConnection())
            {
                using (CidadeConnection cidades = new CidadeConnection())
                {
                    ViewBag.Status = Enum.GetValues(typeof(Status));
                    ViewBag.Estado = estados.ReadAll();
                    List<ListarCidades> lista = cidades.ReadAll();
                    return View(lista);
                }
            }
        }

        [HttpPost]
        public ActionResult Cadastrar(Cidade cidade)
        {
            using (CidadeConnection cidades = new CidadeConnection())
            {
                cidades.Create(cidade);
                return RedirectToAction("Index", "Cidade");
            }
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (EstadoConnection estados = new EstadoConnection())
            {
                using (CidadeConnection cidades = new CidadeConnection())
                {
                    ViewBag.Status = Enum.GetValues(typeof(Status));
                    ViewBag.Estado = estados.ReadAll();
                    Cidade cidade = cidades.Read(id);
                    return View(cidade);
                }
            }
        }
        [HttpPost]
        public ActionResult Editar(int id, Cidade cidade)
        {
            using (CidadeConnection cidades = new CidadeConnection())
            {
                cidades.Update(id, cidade);
                return RedirectToAction("Index", "Cidade");
            }
        }

        [HttpGet]
        public ActionResult Apagar(int id)
        {
            using (EstadoConnection estados = new EstadoConnection())
            {
                using (CidadeConnection cidades = new CidadeConnection())
                {
                    if (cidades.Check(id))
                    {
                        ViewBag.Status = Enum.GetValues(typeof(Status));
                        ViewBag.Estado = estados.ReadAll();
                        Cidade cidade = cidades.Read(id);
                        return View(cidade);
                    }
                    TempData["Erro"] = "A Cidade está em uso e não pode ser Excluido!!!";
                    return RedirectToAction("Index", "Cidade");
                }
            }
        }
        [HttpPost]
        public ActionResult Apagar(int id, Cidade cidade)
        {
            using (CidadeConnection cidades = new CidadeConnection())
            {
                cidades.Delete(id);
                return RedirectToAction("Index", "Cidade");
            }
        }
    }
}