using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    [Table("cidade")]
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cidadeid")]
        public int cidadeid { get; set; }

        [Column("nome")]
        public string nome { get; set; }

        [Column("estado")]
        public string estado { get; set; }

        [Column("cep")]
        public string cep { get; set; }

        public virtual ICollection<Usuario> usuario { get; set; }
        public virtual ICollection<Endereco> endereco { get; set; }
    }
}
