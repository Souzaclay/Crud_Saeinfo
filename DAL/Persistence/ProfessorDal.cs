using DAL.DataSource;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence
{
    public class ProfessorDal : IDisposable 
    {
        public Conexao Con;

        public ProfessorDal()
        {
            Con = new Conexao();
        }

        public void Incluir(Professor p)
        {
            Con.Professor.Add(p);
            Con.SaveChanges();
        }

        public void Incluir(List<Professor> p)
        {
            p.ForEach(ent => Con.Professor.Add(ent));
            Con.SaveChanges();
        }

        public void Alterar(Professor p)
        {
            Con.Professor.Attach(p);
            Con.Entry(p).State = System.Data.Entity.EntityState.Modified;
            Con.SaveChanges();
        }

        public virtual void Excluir(Professor p)
        {
            Con.Professor.Remove(p);
            Con.SaveChanges();
        }

        public virtual void Excluir(Expression<Func<Professor, bool>> where)
        {
            IEnumerable<Professor> objects = Con.Professor.Where<Professor>(where).AsEnumerable();
            foreach (Professor obj in objects)
                Con.Professor.Remove(obj);
            Con.SaveChanges();
        }

        public Professor Obter(int id)
        {
            return Con.Professor.FirstOrDefault(ent => ent.professorid.Equals(id));
        }

        public IQueryable<Professor> ObterVarios(Expression<Func<Professor, bool>> where)
        {
            return Con.Professor.Where(where);
        }

        public virtual IQueryable<Professor> ObterTodos()
        {
            return Con.Professor;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Con.Dispose();
        }
    }
}
