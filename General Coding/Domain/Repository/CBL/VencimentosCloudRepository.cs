using Framework.Database.Entity.CBL;

namespace Framework.Domain.Repository.CBL
{
   public class VencimentosCloudRepository : RepositoryBase<VencimentosCloud>
   {
      public void AtualizarVencimentoPadraoCloud(int idCloud)
      {
         string sSql = @"UPDATE VencimentosCloud SET indPadrao = 0 WHERE idCloud = " + idCloud;
         Db.Database.ExecuteSqlCommand(sSql);
      }
   }
}
