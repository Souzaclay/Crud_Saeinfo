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
    public class ResponsavelDal : IDisposable
    {
        public Conexao Con;

        public ResponsavelDal()
        {
            Con = new Conexao();
        }

        public void Incluir(Responsavel r)
        {
            Con.Responsavel.Add(r);
            Con.SaveChanges();
        }

        public void Incluir(List<Responsavel> r)
        {
            r.ForEach(ent => Con.Responsavel.Add(ent));
            Con.SaveChanges();
        }

        public void Alterar(Responsavel r)
        {
            Con.Responsavel.Attach(r);
            Con.Entry(r).State = System.Data.Entity.EntityState.Modified;
            Con.SaveChanges();
        }

        public virtual void Excluir(Responsavel r)
        {
            Con.Responsavel.Remove(r);
            Con.SaveChanges();
        }

        public virtual void Excluir(Expression<Func<Responsavel, bool>> where)
        {
            IEnumerable<Responsavel> objects = Con.Responsavel.Where<Responsavel>(where).AsEnumerable();
            foreach (Responsavel obj in objects)
                Con.Responsavel.Remove(obj);
            Con.SaveChanges();
        }

        public Responsavel Obter(int id)
        {
            return Con.Responsavel.FirstOrDefault(ent => ent.responsavelid.Equals(id));
        }

        public IQueryable<Responsavel> ObterVarios(Expression<Func<Responsavel, bool>> where)
        {
            return Con.Responsavel.Where(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Con.Dispose();
        }
    }
}
