using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DAL.Model;
using DAL.Persistence;
using TelaCadastro.ViewModels;

namespace TelaCadastro.Controllers
{
    public class CidadeController : Controller
    {
        public ActionResult Index()
        {
            var listaCidade = new CidadeDal().ObterTodos().ToList();
            List<CidadeViewModel> cidades = new List<CidadeViewModel>();

            foreach (var cidade in listaCidade)
            {
                cidades.Add(new CidadeViewModel
                {
                    cep = cidade.cep,
                    cidadeid = cidade.cidadeid,
                    estado = cidade.estado,
                    nome = cidade.nome
                });
            }

            return View(cidades);
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Incluir(CidadeViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                CidadeDal serviceCidade = new CidadeDal();

                Cidade model = new Cidade
                {
                    nome = viewmodel.nome,
                    estado = viewmodel.estado,
                    cep = RemoveMascara(viewmodel.cep)
                };

                serviceCidade.Incluir(model);

                return RedirectToAction("Index", "Cidade");
            }
            else
            {
                return View(viewmodel);
            }
        }

        public ActionResult Alterar(int id)
        {
            var obj = new CidadeDal().Obter(id);

            var viewmodel = new CidadeViewModel
            {
                nome = obj.nome,
                estado = obj.estado,
                cep = obj.cep,
                cidadeid = obj.cidadeid
            };

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Alterar(CidadeViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                CidadeDal serviceCidade = new CidadeDal();

                var cidade = serviceCidade.Obter(viewmodel.cidadeid);

                cidade.nome = viewmodel.nome;
                cidade.estado = viewmodel.estado;
                cidade.cep = RemoveMascara(viewmodel.cep);

                serviceCidade.Alterar(cidade);

                return RedirectToAction("Index", "Cidade");
            }
            else
            {
                return View(viewmodel);
            }
        }

        public ActionResult Visualizar(int id)
        {
            var obj = new CidadeDal().Obter(id);

            var viewmodel = new CidadeViewModel
            {
                nome = obj.nome,
                estado = obj.estado,
                cep = obj.cep,
                cidadeid = obj.cidadeid
            };

            return View(viewmodel);
        }

        public ActionResult Excluir(int id)
        {
            var obj = new CidadeDal().Obter(id);

            var viewmodel = new CidadeViewModel
            {
                nome = obj.nome,
                estado = obj.estado,
                cep = obj.cep,
                cidadeid = obj.cidadeid
            };

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Excluir(CidadeViewModel viewmodel)
        {
            try
            {
                CidadeDal serviceCidade = new CidadeDal();
                serviceCidade.Excluir(ent => ent.cidadeid == viewmodel.cidadeid);

                return RedirectToAction("Index", "Cidade");

            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public string RemoveMascara(string texto)
        {
            return texto == null ? null : (Regex.Replace(texto, "[?\\)?\\(_./-]", "")).Replace(" ", "");
        }
    }
}