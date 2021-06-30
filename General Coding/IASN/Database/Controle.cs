using Database.Context;
using System;
using System.Data;
using System.Text;

namespace Database
{
  public class Controle
  {
    #region LOG
    public static void LogCarga(string referencia, string mensagem, string parametros = "", int A022_CD_EV = 0, int A012_cd_cli = 0, int A001_numsequencial = 0)
    {
      StringBuilder sbMensagem = new StringBuilder();
      sbMensagem.Append("Ás: ").Append(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")).AppendLine().Append(mensagem.Replace("'", "''"));

      Console.WriteLine(referencia + " " + sbMensagem.ToString());

      string evento = A022_CD_EV > 0 ? A022_CD_EV.ToString() : "NULL";
      string cliente = A012_cd_cli > 0 ? A012_cd_cli.ToString() : "NULL";
      string A001num = A001_numsequencial > 0 ? A001_numsequencial.ToString() : "NULL";

      string query = String.Format("INSERT INTO T000_LOG_CARGA_SIA_SIAC VALUES (SYSDATE, '{0}', '{1}', '{2}',{3},{4},{5},systimestamp)",
          referencia.Replace("'", ""),
          mensagem.Replace("'", ""),
          parametros.Replace("'", ""),
          evento,
          A001num,
          cliente);
      try
      {
        DataBase.ExecuteNonQuery(CommandType.Text, query);
      }
      catch (Exception ex)
      {
        try
        {
          Console.WriteLine("Erro ao tentar gravar LOG->" + ex.Message);
          LogCarga(referencia, "Erro ao tentar gravar LOG->" + ex.Message.Replace("'", "''"), "", A022_CD_EV, A012_cd_cli, A001_numsequencial);
        }
        catch (Exception ex2)
        {
          Console.WriteLine("Erro ao tentar gravar LOG 2->" + ex2.Message);
        }
      }
    }
    #endregion
  }
}
