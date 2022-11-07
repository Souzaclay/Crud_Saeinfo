using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    [Table("professor")]
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("professorid")]
        public int professorid { get; set; }

        [Column("nome")]
        public string nome { get; set; }

        [Column("materia")]
        public string materia { get; set; }

        [Column("turno")]
        public string turno { get; set; }

        [Column("idade")]
        public int idade { get; set; }

        [Column("cpf")]
        public string cpf { get; set; } 

    }
}