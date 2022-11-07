using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DAL.Model;
using DAL.Persistence;
using iText.Html2pdf;
using iText.StyledXmlParser.Css.Media;
using TelaCadastro.Models;
using TelaCadastro.Util;
using TelaCadastro.ViewModels;

namespace TelaCadastro.Controllers
{
    public class GraficoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GraficoAlunosPorCidade()
        {
            var listacidades = new CidadeDal().ObterTodos();
            var listaenderecos = new EnderecoDal().ObterTodos().ToList();

            var listaalunoscidade = listacidades.Select(ent => new
            {
                cidade = ent.nome,
                qtdalunos = listaenderecos.Count(x => x.cidadeid == ent.cidadeid)
            });

            return Json(listaalunoscidade);
        }

        public JsonResult GraficoAlunosPorDataHora(DateTime? datainicio, DateTime? datafim)
        {
            var listaalunos = new AlunoDal().ObterTodos();

            var listaalunoshora = listaalunos.Where(ent => ent.datacadastro >= datainicio && ent.datacadastro <= datafim);

            return Json(new { alunos = "Alunos", qtdalunos = listaalunoshora.Count() });
        }
    }
}