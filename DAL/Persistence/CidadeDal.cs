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
    public class CidadeDal : IDisposable
    {
        public Conexao Con;

        public CidadeDal()
        {
            Con = new Conexao();
        }

        public void Incluir(Cidade c)
        {
            Con.Cidade.Add(c);
            Con.SaveChanges();
        }


        public void Incluir(List<Cidade> c)
        {
            c.ForEach(ent => Con.Cidade.Add(ent));
            Con.SaveChanges();
        }

        public void Alterar(Cidade c)
        {
            Con.Cidade.Attach(c);
            Con.Entry(c).State = System.Data.Entity.EntityState.Modified;
            Con.SaveChanges();
        }

        public virtual void Excluir(Cidade c)
        {
            Con.Cidade.Remove(c);
            Con.SaveChanges();
        }

        public virtual void Excluir(Expression<Func<Cidade, bool>> where)
        {
            IEnumerable<Cidade> objects = Con.Cidade.Where<Cidade>(where).AsEnumerable();
            foreach (Cidade obj in objects)
                Con.Cidade.Remove(obj);
            Con.SaveChanges();
        }

        public Cidade Obter(int id)
        {
            return Con.Cidade.FirstOrDefault(ent => ent.cidadeid.Equals(id));
        }

        public IQueryable<Responsavel> ObterVarios(Expression<Func<Responsavel, bool>> where)
        {
            return Con.Responsavel.Where(where);
        }

        public List<Cidade> ObterTodos()
        {
            return Con.Cidade.ToList();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Con.Dispose();
        }
    }
}
