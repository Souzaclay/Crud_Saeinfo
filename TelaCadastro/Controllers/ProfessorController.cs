using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Model;
using DAL.Persistence;
using TelaCadastro.Util;
using TelaCadastro.ViewModels;

namespace TelaCadastro.Controllers
{
    public class ProfessorController : Controller
    {
        // GET: Professor
        public ActionResult Index()
        {
            var listaProfessor = new ProfessorDal().ObterTodos().ToList();
            var quantidade = listaProfessor.Count();

            var paginacao = StrToInt32(ConfigurationManager.AppSettings["PaginacaoPadrao"]);

            var grid = new TabelaGenerica<Professor>
            {
                Dados = listaProfessor.ToList<Professor>().OrderBy(ent => ent.nome).
                Take(paginacao > quantidade ? quantidade : paginacao).ToList(),
                Paginacao = paginacao,
                ClassesCss = "table table-hover",
                PaginaAtual = 1,
                TotalRegistros = quantidade
            };

            PrepararViewBags();

            return View(grid);
        }

        public ActionResult Ordenar(string campo, string ordem)
        {

            var listaprofessor = new ProfessorDal().ObterTodos().ToList();

            if (ordem == "cres")
            {
                switch (campo)
                {
                    case "professorid":
                        listaprofessor = listaprofessor.OrderBy(ent => ent.professorid).ToList();
                        break;
                    case "nome":
                        listaprofessor = listaprofessor.OrderBy(ent => ent.nome).ToList();
                        break;
                    case "materia":
                        listaprofessor = listaprofessor.OrderBy(ent => ent.materia).ToList();
                        break;
                    case "turno":
                        listaprofessor = listaprofessor.OrderBy(ent => ent.turno).ToList();
                        break;
                    case "idade":
                        listaprofessor = listaprofessor.OrderBy(ent => ent.idade).ToList();
                        break;
                    case "cpf":
                        listaprofessor = listaprofessor.OrderBy(ent => ent.cpf).ToList();
                        break;                   
                }

            }
            else
            {
                switch (campo)
                {
                    case "professorid":
                        listaprofessor = listaprofessor.OrderByDescending(ent => ent.professorid).ToList();
                        break;
                    case "nome":
                        listaprofessor = listaprofessor.OrderByDescending(ent => ent.nome).ToList();
                        break;
                    case "materia":
                        listaprofessor = listaprofessor.OrderByDescending(ent => ent.materia).ToList();
                        break;
                    case "turno":
                        listaprofessor = listaprofessor.OrderByDescending(ent => ent.turno).ToList();
                        break;
                    case "idade":
                        listaprofessor = listaprofessor.OrderByDescending(ent => ent.idade).ToList();
                        break;
                    case "cpf":
                        listaprofessor = listaprofessor.OrderByDescending(ent => ent.cpf).ToList();
                        break;
                }
            }

            var paginacao = StrToInt32(ConfigurationManager.AppSettings["PaginacaoPadrao"]);
            var paginaAtual = 1;

            int quantidade = listaprofessor.Count();

            var grid = new TabelaGenerica<Professor>
            {
                Dados = listaprofessor.ToList()
              .Skip(paginacao * (paginaAtual - 1)).Take(paginacao > quantidade ? quantidade : paginacao).ToList(),
                Paginacao = paginacao,
                PaginaAtual = paginaAtual,
                TotalRegistros = quantidade
            };

            return View("Tabelaprofessor", grid);
        }

        public ActionResult Incluir()
        {
            PrepararViewBags();

            return View();
        }

        [HttpPost]
        public ActionResult Visualizar(int id)
        {
            var obj = new ProfessorDal().Obter(id);
         
            PrepararViewBags();

            return View();
        }

        [HttpPost]
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

        public string GerarMatricula()
        {
            Random random = new Random();
            return random.Next(11111111, 99999999).ToString();
        }

        public int StrToInt32(string valor)
        {
            if (String.IsNullOrWhiteSpace(valor))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(valor);
            }
        }


    }
}