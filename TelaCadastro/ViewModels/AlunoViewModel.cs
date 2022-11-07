using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TelaCadastro.ViewModels
{
    public class AlunoViewModel
    {
        public int alunoid { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(200, ErrorMessage = "Tamanho Máximo 200 caracteres!")]
        [DisplayName("Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("RG")]
        public string rg { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("CPF")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Data de Nascimento")]
        public DateTime? datanascimento { get; set; }

        public int? enderecoid { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Matrícula")]
        public string matricula { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Idade")]
        public int? idade { get; set; }

        public DateTime? datacadastro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Sexo")]
        public string sexo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [EmailAddress(ErrorMessage = "O E-mail não é valido!")]
        [DisplayName("E-mail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Telefone")]
        public string telefone { get; set; }

        public virtual EnderecoViewModel endereco { get; set; }

        public virtual List<ResponsavelViewModel> responsavel { get; set; }
    }
}