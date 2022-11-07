using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataSource;
using DAL.Model;

namespace DAL.Persistence
{ 
    public class UsuarioDal : IDisposable
    {
        public Conexao Con;

        public UsuarioDal()
        {
            Con = new Conexao();        
        }

        public void Incluir(Usuario u)
        {
            Con.Usuario.Add(u);
            Con.SaveChanges();
        }

        public Usuario ObterPorLogin(string email) {
            return Con.Usuario.FirstOrDefault(ent => ent.email == email);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Con.Dispose();
        }
    }
}
