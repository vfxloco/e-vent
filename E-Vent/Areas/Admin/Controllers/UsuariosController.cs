using E_Vent.Controllers;
using E_Vent.Models.Connection;
using E_Vent.Models.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Areas.Admin.Controllers
{
    [AuthFilter]
    public class UsuariosController : Controller
    {
        // GET: Admin/Usuario
        public ActionResult Index()
        {
            using (UsuariosConnection usuarios = new UsuariosConnection())
            {
                ViewBag.Status = Enum.GetValues(typeof(Status));
                ViewBag.PessoasFisicas = usuarios.ReadAll_PessoasFisicas();
                ViewBag.PessoasJuridicas = usuarios.ReadAll_PessoasJuridica();
                return View();
            }
        }

        public ActionResult Status(int id)
        {
            using (UsuariosConnection connection = new UsuariosConnection())
            {
                connection.Status(id);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.TipoPessoa = Enum.GetValues(typeof(TipoPessoa));
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(PessoaViewModel pessoa)
        {
            using (UsuarioConnection usuario = new UsuarioConnection())
            {
                usuario.Create(pessoa);
            }
            return RedirectToAction("Index");
        }
    }
}