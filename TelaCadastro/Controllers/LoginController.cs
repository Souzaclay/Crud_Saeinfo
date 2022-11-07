using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DAL.Model;
using DAL.Persistence;
using TelaCadastro.Models;
using TelaCadastro.ViewModels;

namespace TelaCadastro.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(FormCollection f)
        {
            try
            {

                UsuarioDal serviceusuario = new UsuarioDal();

                Usuario u = serviceusuario.ObterPorLogin(f["email"]);
                if (u != null)
                {
                    if (f["senha"] == u.senha)
                    {
                        IncluiPessoaNaSessao(f["email"]);

                        return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        private void IncluiPessoaNaSessao(string email)
        {
            FormsAuthentication.SetAuthCookie(email, false);

            SessaoUsuario.DefinirUsuarioTemp(email);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            SessaoUsuario.SessaoLimpar();
            return Redirect("~/Login/Index");
        }

    }
}