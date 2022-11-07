using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TelaCadastro.ViewModels
{
    public class UsuarioViewModel
    {
        public int usuarioid { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(200, ErrorMessage = "Tamanho Máximo 200 caracteres!")]
        [DisplayName("Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("CPF")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Sexo")]
        public string sexo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Telefone")]
        public string telefone { get; set; }

        public DateTime? datacadastro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Cidade")]
        public int cidadeid { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [EmailAddress(ErrorMessage = "O E-mail não é valido!")]
        [DisplayName("E-mail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Senha")]
        public string senha { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Confirmar Senha")]
        public string confirmarsenha { get; set; }
    }
}