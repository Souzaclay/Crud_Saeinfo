using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    [Table("curso")]
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cursoid")]
        public int cursoid { get; set; }

        [Column("descricao")]
        public string descricao { get; set; }

        [Column("professorid")]
        public int professorid { get; set; }

        [Column("turno")]
        public string turno { get; set; }
    }
}
