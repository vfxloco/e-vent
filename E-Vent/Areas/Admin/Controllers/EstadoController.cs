using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Vent.Areas.Admin.Models;
using E_Vent.Areas.Admin.Models.Connection;
using E_Vent.Controllers;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Areas.Admin.Controllers
{
    [AuthFilter]
    public class EstadoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (EstadoConnection estados = new EstadoConnection())
            {
                ViewBag.Status = Enum.GetValues(typeof(Status));
                List<Estado> lista = estados.ReadAll();
                return View(lista);
            }
        }

        [HttpPost]
        public ActionResult Cadastrar(Estado estado)
        {
            using (EstadoConnection estados = new EstadoConnection())
            {
                estados.Create(estado);
                return RedirectToAction("Index", "Estado");
            }
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (EstadoConnection estados = new EstadoConnection())
            {
                ViewBag.Status = Enum.GetValues(typeof(Status));
                Estado estado = estados.Read(id);
                return View(estado);
            }
        }
        [HttpPost]
        public ActionResult Editar(int id, Estado estado)
        {
            using (EstadoConnection estados = new EstadoConnection())
            {
                estados.Update(id, estado);
                return RedirectToAction("Index", "Estado");
            }
        }

        [HttpGet]
        public ActionResult Apagar(int id)
        {
            using (EstadoConnection estados = new EstadoConnection())
            {
                if (estados.Check(id))
                {
                    ViewBag.Status = Enum.GetValues(typeof(Status));
                    Estado estado = estados.Read(id);
                    return View(estado);
                }
                TempData["Erro"] = "O Estado está em uso e não pode ser Excluido!!!";
                return RedirectToAction("Index", "Estado");
            }
        }
        [HttpPost]
        public ActionResult Apagar(int id, Estado estado)
        {
            using (EstadoConnection estados = new EstadoConnection())
            {
                estados.Delete(id);
                return RedirectToAction("Index", "Estado");
            }
        }
    }
}