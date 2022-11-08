using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TelaCadastro.ViewModels
{
    public class CursoViewModel
    {
        public int cursoid { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string turno { get; set; }
                
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int professorid { get; set; }

        public List<SelectListItem> listaturno { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> listaprofessor { get; set; } = new List<SelectListItem>();
    }
}