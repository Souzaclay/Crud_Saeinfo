using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;
using DAL.Model;

namespace DAL.DataSource
{
    public class Conexao : DbContext
    {
        public Conexao() : base(ConfigurationManager.ConnectionStrings["Banco"].ConnectionString)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Responsavel> Responsavel { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Curso> Curso { get; set; }

    }
}
