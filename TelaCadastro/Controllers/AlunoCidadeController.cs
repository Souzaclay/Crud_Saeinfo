using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Persistence;
using TelaCadastro.ViewModels;

namespace TelaCadastro.Controllers
{
    public class AlunoCidadeController : Controller
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

        public ActionResult BuscarAlunos(int id)
        {
            var listaaluno = new AlunoDal().ObterVarios(ent => ent.endereco.cidadeid == id).ToList();
            List<AlunoViewModel> alunos = new List<AlunoViewModel>();

            foreach (var aluno in listaaluno)
            {
                alunos.Add(new AlunoViewModel
                {
                    alunoid = aluno.alunoid,
                    nome = aluno.nome,
                    sexo = aluno.sexo,
                    telefone = aluno.telefone,
                    datacadastro = aluno.datacadastro,
                    cidade = aluno.endereco?.cidade?.nome
                });
            }

            return View("TabelaAluno", alunos);
        }
    }
}