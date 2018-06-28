using E_Vent.Models.Connection;
using E_Vent.Models.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static E_Vent.Areas.Admin.Models.Common;

namespace E_Vent.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["usuario"] == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(PessoaViewLogin login)
        {
            using (UsuarioConnection usuario = new UsuarioConnection())
            {
                PessoaSession usuarioSession = usuario.Login(login);
                if (usuarioSession != null)
                {
                    if (usuarioSession.Status == Status.Ativado)
                    {
                        Session["usuario"] = usuarioSession;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Erro"] = "Sua conta foi desativada. Entre em contato com um Administrador!!!";
                    }
                }
                else
                {
                    TempData["Erro"] = "E-Mail ou Senha Incorretos!!!";
                }
            }
            return View(login);
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
            using(UsuarioConnection usuario = new UsuarioConnection())
            {
                usuario.Create(pessoa);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Sair()
        {
            //Session.Abandon();
            Session.Remove("usuario");
            return RedirectToAction("Index");
        }
    }
}