using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TelaCadastro.ViewModels
{
    public class ProfessorViewModel
    {
        public int professorid { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(200, ErrorMessage = "Tamanho Máximo 200 caracteres!")]
        [DisplayName("Nome")]
        public string nome { get; set; }
                
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(200, ErrorMessage = "Tamanho Máximo 200 caracteres!")]
        [DisplayName("matéria")]
        public string materia { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("Idade")]
        public int? idade { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Tamanho Máximo 50 caracteres!")]
        [DisplayName("turno")]
        public string turno { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayName("CPF")]
        public string cpf { get; set; }
                
        public DateTime? datacadastro { get; set; }
        
    }
}