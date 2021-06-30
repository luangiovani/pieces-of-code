using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace Classes
{
    public class Paginacao
    {
        #region Atributos
        private int totalRegistros;
        private OracleDataReader dataReader;
        #endregion

        #region Propriedades
        public int TotalRegistros
        {
            get { return this.totalRegistros; }
            set { this.totalRegistros = value; }
        }
        public OracleDataReader DataReader
        {
            get { return this.dataReader; }
            set { this.dataReader = value; }
        }
        #endregion

        #region Construtores
        public Paginacao()
        {
            this.totalRegistros = 0;
        }
        public Paginacao(int total, OracleDataReader dr)
        {
            this.totalRegistros = total;
            this.dataReader = dr;
        }
        #endregion
    }

    /* Exemplo de como deve ser utilizado na tela
     * 
     * Paginacao retorno = ClienteFornecedor.LoadDataPaginacao(...);
     * gridView.DataSource = retorno.DataReader;
     * gridView.DataBind();
     * int totalRegistros = retorno.TotalRegistros;
     */
}
