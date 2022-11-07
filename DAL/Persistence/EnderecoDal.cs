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
    public class EnderecoDal : IDisposable
    {
        public Conexao Con;

        public EnderecoDal()
        {
            Con = new Conexao();
        }

        public void Incluir(Endereco e)
        {
            Con.Endereco.Add(e);
            Con.SaveChanges();
        }

        public void Incluir(List<Endereco> e)
        {
            e.ForEach(ent => Con.Endereco.Add(ent));
            Con.SaveChanges();
        }

        public void Alterar(Endereco e)
        {
            Con.Endereco.Attach(e);
            Con.Entry(e).State = System.Data.Entity.EntityState.Modified;
            Con.SaveChanges();
        }

        public virtual void Excluir(Endereco e)
        {
            Con.Endereco.Remove(e);
            Con.SaveChanges();
        }

        public virtual void Excluir(Expression<Func<Endereco, bool>> where)
        {
            IEnumerable<Endereco> objects = Con.Endereco.Where<Endereco>(where).AsEnumerable();
            foreach (Endereco obj in objects)
                Con.Endereco.Remove(obj);
            Con.SaveChanges();
        }

        public Endereco Obter(int id)
        {
            return Con.Endereco.FirstOrDefault(ent => ent.enderecoid.Equals(id));
        }

        public virtual IQueryable<Endereco> ObterTodos()
        {
            return Con.Endereco;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Con.Dispose();
        }
    }
}
