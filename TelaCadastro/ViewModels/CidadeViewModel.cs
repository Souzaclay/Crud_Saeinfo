using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TelaCadastro.ViewModels
{
    public class CidadeViewModel
    {
        public int cidadeid { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(200, ErrorMessage = "Tamanho Máximo 200 caracteres!")]
        [DisplayName("Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Estado")]
        public string estado { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("CEP")]
        public string cep { get; set; }
    }
}