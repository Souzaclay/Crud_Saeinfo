using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TelaCadastro.ViewModels
{
    public class ResponsavelViewModel
    {
        public int responsavelid { get; set; }

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
        [DisplayName("Profissão")]
        public string profissao { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Celular")]
        public string celular { get; set; }

        public int? alunoid { get; set; }

        public DateTime? datacadastro { get; set; }
        public DateTime? dataalteracao { get; set; }
    }
}