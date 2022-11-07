using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelaCadastro.Util
{
    public class TabelaGenerica<T> where T : class
    {
        public List<T> Dados { get; set; }
        public string[] Colunas { get; set; }
        public string ClassesCss { get; set; }
        public int PaginaAtual { get; set; }
        public int Paginacao { get; set; }
        public string TabelaId { get; set; }
        public Filtro[] Filtros { get; set; }
        public int TotalRegistros { get; set; }
        public Rodape Rodape
        {
            get
            {
                if (rodape == null)
                    rodape = PreencherRodape();
                return rodape;
            }
            set
            {
                rodape = value;
            }
        }

        private Rodape rodape { get; set; }

        public Rodape PreencherRodape()
        {
            return new Rodape { Paginacao = this.Paginacao, PaginaAtual = this.PaginaAtual, TotalRegistros = this.TotalRegistros, TotalPaginas = this.TotalPaginas };
        }

        public int TotalPaginas
        {
            get { return TotalRegistros / Paginacao + (TotalRegistros % Paginacao > 0 ? 1 : 0); }
        }

        public int TotalRegistrosPagina
        {
            get { return Dados.Count(); }
        }
    }

    public class Filtro
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Rodape
    {
        public int Paginacao { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
    }
}