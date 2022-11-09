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
            List<AlunoViewModel> alunos = new List<AlunoViewModel>();

            foreach (var aluno in listaAluno)
            {
                alunos.Add(new AlunoViewModel
                {
                    alunoid = aluno.alunoid,
                    nome = aluno.nome,
                    sexo = aluno.sexo,
                    telefone = aluno.telefone,
                    datacadastro = aluno.datacadastro,
                    cidade = aluno.endereco.cidade.nome
                });
            }

            PrepararViewBags();

            return View(alunos);
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