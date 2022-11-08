using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataSource;
using DAL.Model;
using System.Linq.Expressions;

namespace DAL.Persistence
{
    public class CursoDal : IDisposable
    {
        public Conexao Con;
        public CursoDal()
        {
            Con = new Conexao();
        }

        public void Incluir(Curso c)
        {
            Con.Curso.Add(c);
            Con.SaveChanges();
        }

        public void Incluir (List<Curso> c)
        {
            c.ForEach(ent => Con.Curso.Add(ent));
            Con.SaveChanges();
        }

        public void Alterar (Curso c)
        {
            Con.Curso.Attach(c);
            Con.Entry(c).State = System.Data.Entity.EntityState.Modified;
            Con.SaveChanges();
        }

        public void Excluir(Curso c)
        {
            Con.Curso.Remove(c);
            Con.SaveChanges();
        }

        public void Excluir (Expression<Func<Curso, bool>> where)
        {

            IEnumerable<Curso> objects = Con.Curso.Where<Curso>(where).AsEnumerable();
            foreach (Curso obj in objects)
                Con.Curso.Remove(obj);
            Con.SaveChanges();
        }

        public Curso Obter (int id)
        {
            return Con.Curso.FirstOrDefault(ent => ent.cursoid.Equals(id));
        }

        public IQueryable<Curso> ObterVarios(Expression<Func<Curso, bool>> where)
        {
            return Con.Curso.Where(where);
        }

        public IQueryable<Curso> ObterTodos()
        {
            return Con.Curso;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Con.Dispose();
        }

    }
}
