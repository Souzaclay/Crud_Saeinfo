using DAL.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TelaCadastro.ViewModels;

namespace TelaCadastro.Controllers
{
    public class CursoController : Controller
    {
        // GET: Curso
        public CursoDal servico;
        public ProfessorDal servicoProfessor;

        public CursoController()        
        {
            servico = new CursoDal();
            servicoProfessor = new ProfessorDal();
        }

        public ActionResult Index()
        {
            var curso = servico.ObterTodos();
            var listaCurso = new List<CursoViewModel>();

            foreach (var item in curso)
            {
                var itemCurso = new CursoViewModel();

                itemCurso.cursoid = item.cursoid;
                itemCurso.descricao = item.descricao;
                itemCurso.turno = item.turno;

                listaCurso.Add(itemCurso);                
            }
            return View(listaCurso);
        }

        public ActionResult Incluir()
        {
            var listaTurnoSelect = new List<SelectListItem>();

            listaTurnoSelect.Add(new SelectListItem
            {
                Text = "selecione...",
                Value = "",
                Selected = true
            });

            listaTurnoSelect.Add(new SelectListItem
            {
                Text = "Vespertino",
                Value = "V"
            });

            listaTurnoSelect.Add(new SelectListItem
            {
                Text = "Matutino",
                Value = "M"
            });

            var professores = servicoProfessor.ObterTodos().ToList();
            var listaProfessorSelect = new List<SelectListItem>();

            listaProfessorSelect.Add(new SelectListItem
            {
                Text = "Selecione...",
                Value = "",
                Selected = true
            });

            foreach (var prof in professores)
            {
                var selectProfessor = new SelectListItem
                {
                    Text = prof.nome,
                    Value = prof.professorid.ToString()
                };

                listaProfessorSelect.Add(selectProfessor);
            }

            var cursoViewModel = new CursoViewModel();
            cursoViewModel.listaprofessor = listaProfessorSelect;
            cursoViewModel.listaturno = listaTurnoSelect;

            return View(cursoViewModel);
        }
    }   
    
    
}