using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("usuarioid")]
        public int usuarioid { get; set; }

        [Column("nome")]
        public string nome { get; set; }

        [Column("cpf")]
        public string cpf { get; set; }

        [Column("sexo")]
        public string sexo { get; set; }

        [Column("telefone")]
        public string telefone { get; set; }

        [Column("datacadastro")]
        public DateTime? datacadastro { get; set; }

        [Column("cidadeid")]
        public int? cidadeid { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("senha")]
        public string senha { get; set; }

        public virtual Cidade cidade { get; set; }

    }
}
