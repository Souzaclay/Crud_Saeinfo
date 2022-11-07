using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.DataSource;
using DAL.Model;

namespace DAL.Persistence
{
    public class AlunoDal : IDisposable
    {
        public Conexao Con;

        public AlunoDal()
        {
            Con = new Conexao();
        }

        public void Incluir(Aluno a)
        {
            Con.Aluno.Add(a);
            Con.SaveChanges();
        }

        public void Incluir(List<Aluno> a)
        {
            a.ForEach(ent => Con.Aluno.Add(ent));
            Con.SaveChanges();
        }

        public void Alterar(Aluno a)
        {
            Con.Aluno.Attach(a);
            Con.Entry(a).State = System.Data.Entity.EntityState.Modified;
            Con.SaveChanges();
        }

        public virtual void Excluir(Aluno a)
        {
            Con.Aluno.Remove(a);
            Con.SaveChanges();
        }

        public virtual void Excluir(Expression<Func<Aluno, bool>> where)
        {
            IEnumerable<Aluno> objects = Con.Aluno.Where<Aluno>(where).AsEnumerable();
            foreach (Aluno obj in objects)
                Con.Aluno.Remove(obj);
            Con.SaveChanges();
        }

        public Aluno Obter(int id)
        {
            return Con.Aluno.FirstOrDefault(ent => ent.alunoid.Equals(id));
        }

        public IQueryable<Aluno> ObterVarios(Expression<Func<Aluno, bool>> where)
        {
            return Con.Aluno.Where(where);
        }

        public virtual IQueryable<Aluno> ObterTodos()
        {
            return Con.Aluno;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Con.Dispose();
        }
    }
}
