using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    [Table("responsavel")]
    public class Responsavel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("responsavelid")]
        public int responsavelid { get; set; }

        [Column("nome")]
        public string nome { get; set; }

        [Column("rg")]
        public string rg { get; set; }

        [Column("cpf")]
        public string cpf { get; set; }

        [Column("profissao")]
        public string profissao { get; set; }

        [Column("celular")]
        public string celular { get; set; }

        public int? alunoid { get; set; }

        [Column("datacadastro")]
        public DateTime? datacadastro { get; set; }

        [Column("dataalteracao")]
        public DateTime? dataalteracao { get; set; }

        public virtual Aluno aluno { get; set; }

    }
}
