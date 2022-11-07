using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelaCadastro.Models
{
    public class UsuarioSessao
    {
        public int usuarioid { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string sexo { get; set; }
        public string telefone { get; set; }
        public DateTime? datacadastro { get; set; }
        public int? cidadeid { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
    }
}