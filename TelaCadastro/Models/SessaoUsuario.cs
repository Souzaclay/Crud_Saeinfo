using DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelaCadastro.Models
{
    public class SessaoUsuario
    {
        public static UsuarioSessao Sessao
        {
            get
            {
                if (HttpContext.Current.Session["Usuario"] == null)
                {
                    HttpContext.Current.Session["Usuario"] = new UsuarioSessao();
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        var name = HttpContext.Current.User.Identity.Name.Split('_');
                        DefinirUsuarioTemp(name.First());
                    }
                }
                return (UsuarioSessao)HttpContext.Current.Session["Usuario"];
            }
        }

        public static void DefinirUsuarioTemp(string email)
        {
            UsuarioDal serviceusuario = new UsuarioDal();

            var usuario = serviceusuario.ObterPorLogin(email);

            Sessao.usuarioid = usuario.usuarioid;
            Sessao.email = usuario.email;
            Sessao.senha = usuario.senha;
            Sessao.nome = usuario.nome;
            Sessao.cpf = usuario.cpf;
            Sessao.cidadeid = usuario.cidadeid;
            Sessao.datacadastro = usuario.datacadastro;
            Sessao.telefone = usuario.telefone;
            Sessao.sexo = usuario.sexo;
        }

        public static void SessaoLimpar()
        {
            HttpContext.Current.Session["Usuario"] = null;
        }
    }
}