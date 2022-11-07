using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DAL.Model;
using DAL.Persistence;
using Microsoft.SqlServer.Server;
using TelaCadastro.Models;
using TelaCadastro.Util;
using TelaCadastro.ViewModels;

namespace TelaCadastro.Controllers
{
    public class AlunoController : Controller
    {
        public ActionResult Index()
        {
            var listaAluno = new AlunoDal().ObterTodos().ToList();
            var quantidade = listaAluno.Count();

            var paginacao = StrToInt32(ConfigurationManager.AppSettings["PaginacaoPadrao"]);

            var grid = new TabelaGenerica<Aluno>
            {
                Dados = listaAluno.ToList<Aluno>().OrderBy(ent => ent.nome).
                Take(paginacao > quantidade ? quantidade : paginacao).ToList(),
                Paginacao = paginacao,
                ClassesCss = "table table-hover",
                PaginaAtual = 1,
                TotalRegistros = quantidade
            };

            PrepararViewBags();

            return View(grid);
        }

        public ActionResult TabelaAluno(Filtro[] filtros)
        {
            var paginacao = StrToInt32(ConfigurationManager.AppSettings["PaginacaoPadrao"]);
            var paginaAtual = 1;

            var listaaluno = new AlunoDal().ObterTodos().ToList();

            if (filtros != null)
            {
                var listaFiltrosValidos = filtros.ToList<Filtro>().Where(ent => !String.IsNullOrEmpty(ent.value));
                foreach (var filtro in listaFiltrosValidos)
                {
                    switch (filtro.name)
                    {
                        case "codigo":
                            var id = Convert.ToInt32(filtro.value);
                            listaaluno = listaaluno.Where(ent => ent.alunoid == id).ToList();
                            break;
                        case "nome":
                            listaaluno = listaaluno.Where(ent => ent.nome.Contains(filtro.value)).ToList();
                            break;
                        case "cpf":
                            var cpf = RemoveMascara(filtro.value);
                            listaaluno = listaaluno.Where(ent => ent.cpf == cpf).ToList();
                            break;
                        case "datanascimento":
                            var datanascimento = Convert.ToDateTime(filtro.value + " 00:00:00");
                            listaaluno = listaaluno.Where(ent => ent.datanascimento.Value.Date == datanascimento.Date).ToList();
                            break;
                        case "datacadastro":
                            var datacadastro = Convert.ToDateTime(filtro.value + " 00:00:00");
                            listaaluno = listaaluno.Where(ent => ent.datacadastro.Value.Date == datacadastro.Date).ToList();
                            break;
                        case "Paginacao":
                            paginacao = StrToInt32(filtro.value);
                            break;
                        case "PaginaAtual":
                            paginaAtual = StrToInt32(filtro.value);
                            break;
                    }
                }
            }

            int quantidade = listaaluno.Count();

            var grid = new TabelaGenerica<Aluno>
            {
                Dados = listaaluno.OrderBy(ent => ent.nome)
              .ToList()
              .Skip(paginacao * (paginaAtual - 1)).Take(paginacao > quantidade ? quantidade : paginacao).ToList(),
                Paginacao = paginacao,
                PaginaAtual = paginaAtual,
                TotalRegistros = quantidade
            };

            return View(grid);
        }

        public ActionResult Ordenar(string campo, string ordem)
        {

            var listaaluno = new AlunoDal().ObterTodos().ToList();

            if (ordem == "cres")
            {
                switch (campo)
                {
                    case "alunoid":
                        listaaluno = listaaluno.OrderBy(ent => ent.alunoid).ToList();
                        break;
                    case "nome":
                        listaaluno = listaaluno.OrderBy(ent => ent.nome).ToList();
                        break;
                    case "cpf":
                        listaaluno = listaaluno.OrderBy(ent => ent.cpf).ToList();
                        break;
                    case "sexo":
                        listaaluno = listaaluno.OrderBy(ent => ent.sexo).ToList();
                        break;
                    case "telefone":
                        listaaluno = listaaluno.OrderBy(ent => ent.telefone).ToList();
                        break;
                    case "datacadastro":
                        listaaluno = listaaluno.OrderBy(ent => ent.datacadastro).ToList();
                        break;
                    case "cidade":
                        listaaluno = listaaluno.OrderBy(ent => ent.endereco.cidade.nome).ToList();
                        break;
                }
                
            }
            else
            {
                switch (campo)
                {
                    case "alunoid":
                        listaaluno = listaaluno.OrderByDescending(ent => ent.alunoid).ToList();
                        break;
                    case "nome":
                        listaaluno = listaaluno.OrderByDescending(ent => ent.nome).ToList();
                        break;
                    case "cpf":
                        listaaluno = listaaluno.OrderByDescending(ent => ent.cpf).ToList();
                        break;
                    case "sexo":
                        listaaluno = listaaluno.OrderByDescending(ent => ent.sexo).ToList();
                        break;
                    case "telefone":
                        listaaluno = listaaluno.OrderByDescending(ent => ent.telefone).ToList();
                        break;
                    case "datacadastro":
                        listaaluno = listaaluno.OrderByDescending(ent => ent.datacadastro).ToList();
                        break;
                    case "cidade":
                        listaaluno = listaaluno.OrderByDescending(ent => ent.endereco.cidade.nome).ToList();
                        break;
                }
            }

            var paginacao = StrToInt32(ConfigurationManager.AppSettings["PaginacaoPadrao"]);
            var paginaAtual = 1;

            int quantidade = listaaluno.Count();

            var grid = new TabelaGenerica<Aluno>
            {
                Dados = listaaluno.ToList()
              .Skip(paginacao * (paginaAtual - 1)).Take(paginacao > quantidade ? quantidade : paginacao).ToList(),
                Paginacao = paginacao,
                PaginaAtual = paginaAtual,
                TotalRegistros = quantidade
            };

            return View("TabelaAluno", grid);
        }

        public ActionResult Incluir()
        {
            PrepararViewBags();

            return View();
        }

        [HttpPost]
        public ActionResult Incluir(AlunoViewModel viewmodel)
        {
            ModelState.Remove("matricula");
            ModelState.Remove("endereco.cidadeid");

            if (ModelState.IsValid)
            {

                EnderecoDal serviceEndereco = new EnderecoDal();

                var endereco = new Endereco
                {
                    cidadeid = viewmodel.endereco.cidadeid,
                    bairro = viewmodel.endereco.bairro,
                    logradouro = viewmodel.endereco.logradouro,
                    numero = viewmodel.endereco.numero,
                    complemento = viewmodel.endereco.complemento,
                    cep = RemoveMascara(viewmodel.endereco.cep)
                };

                serviceEndereco.Incluir(endereco);

                AlunoDal serviceAluno = new AlunoDal();

                Aluno model = new Aluno
                {
                    nome = viewmodel.nome,
                    cpf = RemoveMascara(viewmodel.cpf),
                    rg = RemoveMascara(viewmodel.rg),
                    sexo = viewmodel.sexo,
                    datanascimento = viewmodel.datanascimento,
                    idade = viewmodel.idade,
                    matricula = GerarMatricula(),
                    telefone = RemoveMascara(viewmodel.telefone),
                    email = viewmodel.email,
                    enderecoid = endereco.enderecoid,
                    datacadastro = DateTime.Now
                };

                serviceAluno.Incluir(model);

                ResponsavelDal serviceResponsavel = new ResponsavelDal();
                var responsavel = new List<Responsavel>();

                viewmodel.responsavel.ForEach(item => responsavel.Add(new Responsavel
                {
                    nome = item.nome,
                    rg = RemoveMascara(item.rg),
                    cpf = RemoveMascara(item.cpf),
                    profissao = item.profissao,
                    celular = RemoveMascara(item.celular),
                    alunoid = model.alunoid,
                    datacadastro = DateTime.Now
                }));


                serviceResponsavel.Incluir(responsavel);

                return RedirectToAction("Index", "Aluno");
            }
            else
            {
                PrepararViewBags();

                return View(viewmodel);
            }
        }

        public ActionResult Alterar(int id)
        {
            var obj = new AlunoDal().Obter(id);

            var viewmodel = new AlunoViewModel
            {
                nome = obj.nome,
                cpf = obj.cpf,
                rg = obj.rg,
                sexo = obj.sexo,
                datanascimento = obj.datanascimento,
                idade = obj.idade,
                matricula = obj.matricula,
                telefone = obj.telefone,
                email = obj.email,
                enderecoid = obj.enderecoid,
                datacadastro = obj.datacadastro,
                alunoid = obj.alunoid,
                endereco = new EnderecoViewModel
                {
                    cidadeid = obj.endereco.cidadeid,
                    bairro = obj.endereco.bairro,
                    logradouro = obj.endereco.logradouro,
                    numero = obj.endereco.numero,
                    complemento = obj.endereco.complemento,
                    cep = obj.endereco.cep,
                    enderecoid = obj.enderecoid.Value
                },
                responsavel = new List<ResponsavelViewModel>()
            };

            obj.responsavel.ToList().ForEach(responsavel => viewmodel.responsavel.Add(new ResponsavelViewModel
            {
                nome = responsavel.nome,
                rg = responsavel.rg,
                cpf = responsavel.cpf,
                profissao = responsavel.profissao,
                celular = responsavel.celular,
                alunoid = responsavel.alunoid,
                datacadastro = responsavel.datacadastro,
                responsavelid = responsavel.responsavelid
            }));

            PrepararViewBags();

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Alterar(AlunoViewModel viewmodel)
        {
            ModelState.Remove("matricula");
            ModelState.Remove("endereco.cidadeid");

            if (ModelState.IsValid)
            {

                EnderecoDal serviceEndereco = new EnderecoDal();

                var endereco = serviceEndereco.Obter(viewmodel.enderecoid.Value);
                endereco.cidadeid = viewmodel.endereco.cidadeid;
                endereco.bairro = viewmodel.endereco.bairro;
                endereco.logradouro = viewmodel.endereco.logradouro;
                endereco.numero = viewmodel.endereco.numero;
                endereco.complemento = viewmodel.endereco.complemento;
                endereco.cep = RemoveMascara(viewmodel.endereco.cep);

                serviceEndereco.Alterar(endereco);

                AlunoDal serviceAluno = new AlunoDal();

                var aluno = serviceAluno.Obter(viewmodel.alunoid);

                aluno.nome = viewmodel.nome;
                aluno.cpf = RemoveMascara(viewmodel.cpf);
                aluno.rg = RemoveMascara(viewmodel.rg);
                aluno.sexo = viewmodel.sexo;
                aluno.datanascimento = viewmodel.datanascimento;
                aluno.idade = viewmodel.idade;
                aluno.telefone = RemoveMascara(viewmodel.telefone);
                aluno.email = viewmodel.email;
                aluno.enderecoid = endereco.enderecoid;
                aluno.dataalteracao = DateTime.Now;
                aluno.usuarioalteracao = SessaoUsuario.Sessao.nome;

                serviceAluno.Alterar(aluno);

                ResponsavelDal serviceResponsavel = new ResponsavelDal();

                var responsaveis = serviceResponsavel.ObterVarios(ent => ent.alunoid == aluno.alunoid).ToList();

                viewmodel.responsavel.ForEach(responsavel =>
                {
                    var obj = responsaveis.Single(ent => ent.responsavelid == responsavel.responsavelid);

                    obj.nome = responsavel.nome;
                    obj.rg = RemoveMascara(responsavel.rg);
                    obj.cpf = RemoveMascara(responsavel.cpf);
                    obj.profissao = responsavel.profissao;
                    obj.celular = RemoveMascara(responsavel.celular);
                    obj.dataalteracao = DateTime.Now;

                    serviceResponsavel.Alterar(obj);
                });

                return RedirectToAction("Index", "Aluno");
            }
            else
            {
                PrepararViewBags();

                return View(viewmodel);
            }
        }

        public ActionResult Visualizar(int id)
        {
            var obj = new AlunoDal().Obter(id);

            var viewmodel = new AlunoViewModel

            {
                nome = obj.nome,
                cpf = obj.cpf,
                rg = obj.rg,
                sexo = obj.sexo,
                datanascimento = obj.datanascimento,
                idade = obj.idade,
                matricula = obj.matricula,
                telefone = obj.telefone,
                email = obj.email,
                enderecoid = obj.enderecoid,
                datacadastro = obj.datacadastro,
                alunoid = obj.alunoid,
                endereco = new EnderecoViewModel
                {
                    cidadeid = obj.endereco.cidadeid,
                    bairro = obj.endereco.bairro,
                    logradouro = obj.endereco.logradouro,
                    numero = obj.endereco.numero,
                    complemento = obj.endereco.complemento,
                    cep = obj.endereco.cep,
                    enderecoid = obj.enderecoid.Value
                },
                responsavel = new List<ResponsavelViewModel>()
            };

            obj.responsavel.ToList().ForEach(responsavel => viewmodel.responsavel.Add(new ResponsavelViewModel
            {
                nome = responsavel.nome,
                rg = responsavel.rg,
                cpf = responsavel.cpf,
                profissao = responsavel.profissao,
                celular = responsavel.celular,
                alunoid = responsavel.alunoid,
                datacadastro = responsavel.datacadastro,
                responsavelid = responsavel.responsavelid
            }));

            PrepararViewBags();

            return View(viewmodel);
        }

        public ActionResult Excluir(int id)
        {
            var obj = new AlunoDal().Obter(id);

            var viewmodel = new AlunoViewModel

            {
                nome = obj.nome,
                cpf = obj.cpf,
                rg = obj.rg,
                sexo = obj.sexo,
                datanascimento = obj.datanascimento,
                idade = obj.idade,
                matricula = obj.matricula,
                telefone = obj.telefone,
                email = obj.email,
                enderecoid = obj.enderecoid,
                datacadastro = obj.datacadastro,
                alunoid = obj.alunoid,
                endereco = new EnderecoViewModel
                {
                    cidadeid = obj.endereco.cidadeid,
                    bairro = obj.endereco.bairro,
                    logradouro = obj.endereco.logradouro,
                    numero = obj.endereco.numero,
                    complemento = obj.endereco.complemento,
                    cep = obj.endereco.cep,
                    enderecoid = obj.enderecoid.Value
                },
                responsavel = new List<ResponsavelViewModel>()
            };

            obj.responsavel.ToList().ForEach(responsavel => viewmodel.responsavel.Add(new ResponsavelViewModel
            {
                nome = responsavel.nome,
                rg = responsavel.rg,
                cpf = responsavel.cpf,
                profissao = responsavel.profissao,
                celular = responsavel.celular,
                alunoid = responsavel.alunoid,
                datacadastro = responsavel.datacadastro,
                responsavelid = responsavel.responsavelid
            }));

            PrepararViewBags();

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Excluir(AlunoViewModel viewmodel)
        {
            try
            {
                ResponsavelDal serviceResponsavel = new ResponsavelDal();
                serviceResponsavel.Excluir(ent => ent.alunoid == viewmodel.alunoid);

                AlunoDal serviceAluno = new AlunoDal();
                serviceAluno.Excluir(ent => ent.alunoid == viewmodel.alunoid);

                EnderecoDal serviceEndereco = new EnderecoDal();
                serviceEndereco.Excluir(ent => ent.enderecoid == viewmodel.enderecoid);

                return RedirectToAction("Index", "Aluno");

            }
            catch (Exception ex)
            {
                return View();
            }

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

        public string RemoveMascara(string texto)
        {
            return texto == null ? null : (Regex.Replace(texto, "[?\\)?\\(_./-]", "")).Replace(" ", "");
        }

    }
}