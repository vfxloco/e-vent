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
    public class TipoImovelController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (TipoImovelConnection tipoImovel = new TipoImovelConnection())
            {
                ViewBag.Status = Enum.GetValues(typeof(Status));
                ViewBag.TipoImovel = tipoImovel.ReadAll();
                return View();
            }
        }

        [HttpPost]
        public ActionResult Cadastrar(TipoImovel estado)
        {
            using (TipoImovelConnection tipoImovel = new TipoImovelConnection())
            {
                tipoImovel.Create(estado);
                return RedirectToAction("Index", "TipoImovel");
            }
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (TipoImovelConnection tipoImovel = new TipoImovelConnection())
            {
                ViewBag.Status = Enum.GetValues(typeof(Status));
                TipoImovel estado = tipoImovel.Read(id);
                return View(estado);
            }
        }
        [HttpPost]
        public ActionResult Editar(int id, TipoImovel tipoImovel)
        {
            using (TipoImovelConnection tipoImovels = new TipoImovelConnection())
            {
                tipoImovels.Update(id, tipoImovel);
                return RedirectToAction("Index", "TipoImovel");
            }
        }

        [HttpGet]
        public ActionResult Apagar(int id)
        {
            using (TipoImovelConnection tipoImovel = new TipoImovelConnection())
            {
                if (tipoImovel.Check(id))
                {
                    ViewBag.Status = Enum.GetValues(typeof(Status));
                    TipoImovel tipoImovels = tipoImovel.Read(id);
                    return View(tipoImovels);
                }
                TempData["Erro"] = "O Tipo de Imovel está em uso e não pode ser Excluido!!!";
                return RedirectToAction("Index", "TipoImovel");
            }
        }
        [HttpPost]
        public ActionResult Apagar(int id, TipoImovel estado)
        {
            using (TipoImovelConnection tipoImovel = new TipoImovelConnection())
            {
                tipoImovel.Delete(id);
                return RedirectToAction("Index", "TipoImovel");
            }
        }
    }
}