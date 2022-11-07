using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DAL.Model;
using DAL.Persistence;
using TelaCadastro.ViewModels;

namespace TelaCadastro.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            PrepararViewBags();

            return View();
        }

        [HttpPost]
        public ActionResult Incluir(UsuarioViewModel viewmodel)
        {
            if (String.IsNullOrEmpty(viewmodel.senha))
                ModelState.AddModelError("senha", "Campo Obrigatório");

            if (String.IsNullOrEmpty(viewmodel.confirmarsenha))
                ModelState.AddModelError("confirmarsenha", "Campo Obrigatório");
            else
            {
                if (viewmodel.senha != null && (viewmodel.senha != viewmodel.confirmarsenha))
                    ModelState.AddModelError("confirmarsenha", "As senhas informadas não conferem.");
            }

            if (ModelState.IsValid)
            {
                UsuarioDal serviceusuario = new UsuarioDal();

                var usuariologin = serviceusuario.ObterPorLogin(viewmodel.email);

                if (usuariologin == null)
                {
                    Usuario usuario = new Usuario
                    {
                        nome = viewmodel.nome,
                        cpf = RemoveMascara(viewmodel.cpf),
                        cidadeid = viewmodel.cidadeid,
                        sexo = viewmodel.sexo,
                        telefone = RemoveMascara(viewmodel.telefone),
                        email = viewmodel.email,
                        senha = viewmodel.senha,
                        datacadastro = DateTime.Now
                    };

                    serviceusuario.Incluir(usuario);
                }
                else
                {
                    return View("Index", "Usuario", viewmodel);
                }

                return RedirectToAction("Index", "Login");
            }

            return RedirectToAction("Index", "Usuario");
        }

        public string RemoveMascara(string texto)
        {
            return texto == null ? null : (Regex.Replace(texto, "[?\\)?\\(_./-]", "")).Replace(" ", "");
        }

        public void PrepararViewBags()
        {
            Dictionary<string, string> listasexo = new Dictionary<string, string>();
            listasexo.Add("", "Select..");
            listasexo.Add("M", "Masculino");
            listasexo.Add("F", "Feminino");

            ViewBag.ListaSexo = new SelectList(listasexo, "Key", "Value");

            var listacidade = new CidadeDal().ObterTodos().Select(ent => new
            {
                Key = ent.cidadeid,
                Value = ent.nome + " - " + ent.estado
            });

            ViewBag.ListaCidade = new SelectList(listacidade.OrderBy(ent => ent.Value), "Key", "Value");
        }
    }
}