using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TelaCadastro.ViewModels
{
    public class EnderecoViewModel
    {
        public int enderecoid { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(200, ErrorMessage = "Tamanho Máximo 200 caracteres!")]
        [DisplayName("Logradouro")]
        public string logradouro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(200, ErrorMessage = "Tamanho Máximo 200 caracteres!")]
        [DisplayName("Bairro")]
        public string bairro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Complemento")]
        public string complemento { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Número")]
        public int? numero { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Cidade")]
        public int? cidadeid { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("CEP")]
        public string cep { get; set; }
    }
}