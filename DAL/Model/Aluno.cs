using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    [Table("aluno")]
    public class Aluno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("alunoid")]
        public int alunoid { get; set; }

        [Column("nome")]
        public string nome { get; set; }

        [Column("rg")]
        public string rg { get; set; }

        [Column("cpf")]
        public string cpf { get; set; }

        [Column("datanascimento")]
        public DateTime? datanascimento { get; set; }

        [Column("enderecoid")]
        public int? enderecoid { get; set; }

        [Column("matricula")]
        public string matricula { get; set; }

        [Column("idade")]
        public int? idade { get; set; }

        [Column("sexo")]
        public string sexo { get; set; }

        [Column("telefone")]
        public string telefone { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("datacadastro")]
        public DateTime? datacadastro { get; set; }

        [Column("dataalteracao")]
        public DateTime? dataalteracao { get; set; }

        [Column("usuarioalteracao")]
        public string usuarioalteracao { get; set; }

        public virtual Endereco endereco { get; set; }

        public virtual ICollection<Responsavel> responsavel { get; set; }

    }
}
