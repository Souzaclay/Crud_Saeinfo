using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    [Table("endereco")]
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("enderecoid")]
        public int enderecoid { get; set; }

        [Column("logradouro")]
        public string logradouro { get; set; }

        [Column("bairro")]
        public string bairro { get; set; }

        [Column("complemento")]
        public string complemento { get; set; }

        [Column("numero")]
        public int? numero { get; set; }

        [Column("cidadeid")]
        public int? cidadeid { get; set; }

        [Column("cep")]
        public string cep { get; set; }

        public virtual ICollection<Aluno> aluno { get; set; }
        public virtual Cidade cidade { get; set; }
    }
}
