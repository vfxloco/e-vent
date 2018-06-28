using E_Vent.Areas.Admin.Models.Connection;
using E_Vent.Models;
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
    public class PublicacaoController : Controller
    {
        public ActionResult Index(int? id)
        {
            if (id.HasValue) {
                using (PublicacaoConnection publicacao = new PublicacaoConnection())
                using (TipoImovelConnection tipoImovel = new TipoImovelConnection())
                using (EstadoConnection estados = new EstadoConnection())
                using (CidadeConnection cidades = new CidadeConnection())
                {
                    ViewBag.Cidade = cidades.ReadAll();
                    ViewBag.Estado = estados.ReadAll();
                    if (Session["usuario"] != null)
                    {
                        ViewBag.User = Session["usuario"];
                    }
                    ViewBag.TipoImovel = tipoImovel.ReadAll();
                    ViewBag.Publicacao = publicacao.Read(id.Value);
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [AuthFilter]
        [HttpGet]
        public ActionResult Cadastrar()
        {
            using (TipoImovelConnection tipoImovel = new TipoImovelConnection())
            {
                using (EstadoConnection estados = new EstadoConnection())
                {
                    using (CidadeConnection cidades = new CidadeConnection())
                    {
                        ViewBag.Cidade = cidades.ReadAll();
                        ViewBag.Estado = estados.ReadAll();
                        ViewBag.TipoImovel = tipoImovel.ReadAll();
                        return View();
                    }
                }
            }
        }

        [AuthFilter]
        [HttpPost]
        public ActionResult Cadastrar(PublicacaoView publicacao)
        {
            using (PublicacaoConnection publicacaoConnection = new PublicacaoConnection())
            {
                PessoaSession usuario = Session["usuario"] as PessoaSession;
                publicacao.Pessoa_Id = usuario.Id;
                publicacaoConnection.Create(publicacao);
                return RedirectToAction("Index", "Home");
            }
        }

        [AuthFilter]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id.HasValue)
            {
                using (PublicacaoConnection publicacao = new PublicacaoConnection())
                using (TipoImovelConnection tipoImovel = new TipoImovelConnection())
                using (EstadoConnection estados = new EstadoConnection())
                using (CidadeConnection cidades = new CidadeConnection())
                {
                    ViewBag.Cidade = cidades.ReadAll();
                    ViewBag.Estado = estados.ReadAll();
                    ViewBag.User = Session["usuario"];
                    ViewBag.TipoImovel = tipoImovel.ReadAll();
                    ViewBag.Publicacao = publicacao.Read(id.Value);
                    if (ViewBag.Publicacao.Pessoa_Id == ViewBag.User.Id)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index/"+ ViewBag.Publicacao.Id, "Publicacao");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [AuthFilter]
        [HttpPost]
        public ActionResult Editar(PublicacaoView publicacao)
        {
            using (PublicacaoConnection publicacaoConnection = new PublicacaoConnection())
            {
                publicacaoConnection.Editar(publicacao);
                return RedirectToAction("Index/" + publicacao.Id, "Publicacao");
            } 
        }

        public JsonResult GetEvents(int id)
        {
            using (PublicacaoConnection publicacaoConnection = new PublicacaoConnection())
            {
                var events = publicacaoConnection.Calendar_RadAll(id);
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(int id, Event e)
        {
            var status = false;
            using (PublicacaoConnection publicacaoConnection = new PublicacaoConnection())
            {
                if(publicacaoConnection.Calendar_Create(id, e))
                {
                    status = true;
                }
                else
                {
                    status = false;
                }            
            }
            return new JsonResult { Data = new { status = status } };
        }

        /**
        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            using (PublicacaoConnection publicacaoConnection = new PublicacaoConnection())
            {
                if (e.EventId > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventId == e.EventId).FirstOrDefault();
                    if (v != null)
                    {
                        v.Titulo = e.Titulo;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Descricao = e.Descricao;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }
        **/

        public JsonResult DeleteEvent(int id)
        {
            var status = false;
            using (PublicacaoConnection publicacaoConnection = new PublicacaoConnection())
            {
                if (publicacaoConnection.Calendar_Delete(id))
                {
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}