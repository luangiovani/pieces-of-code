using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    #region TipoFiltro
    public enum TipoFiltro
    {
        Data = 'D',
        Numero = 'N',
        Texto = 'T'
    }
    #region TipoAcesso
    public enum TipoAcesso
    {
        //SemAcesso = 'S',
        Consulta = 0,
        Manutencao = 1
    }

    #endregion
    #endregion  
    
    #region TipoMascara
    public enum TipoMascara
    {
        DATA = 'D',
        NUMERO = 'N',
        HORA = 'H',
        CEP = 'C',
        PLACA = 'P',
        CPF = 'F',
        CNPJ = 'J',
        VALOR = 'V',
        VALOR3 = '3',
        VALOR1 = '1'
    }
    #endregion

    #region TipoPessoa
    public enum TipoPessoa
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        Ali = 5,
        Gestor = 6,
        Senior = 7
    }
    #endregion  

    #region TipoRelato
    public enum TipoRelato
    {
        Ativo = 1,
        Inativo = 0
    }
    #endregion  

    #region StatusProjetoAli
    public enum StatusProjetoAli
    {
        Ativo = 'A',
        Cancelado = 'C',
        Finalizado = 'F'
    }
    #endregion  
}
