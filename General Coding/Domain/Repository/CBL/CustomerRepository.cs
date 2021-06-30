using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public int GetTOTAL_ROWS(string search)
        {
            var sql = "SELECT Count(*) TOTAL_ROWS FROM [dbo].[Customer]";

            if (!String.IsNullOrEmpty(search))
            {
                sql += " WHERE (name LIKE '%" + search + "%'";
                sql += "   OR email LIKE '%" + search + "%')";
            }

            return Db.Database.SqlQuery<int>(sql).First();
        }

        public paginacaoCustomer getPaginacao(int length, int page)
        {
            string sSql = "";
            sSql += " DECLARE @intConta INT; ";
            sSql += " DECLARE @intItera INT = 1; ";
            sSql += " DECLARE @idMenor INT = 0; ";
            sSql += " DECLARE @idMaior INT = 0; ";
            sSql += " DECLARE @tbPagIds AS TABLE( ";
	        sSql += " pagina INT, ";
	        sSql += " idMenor INT, ";
	        sSql += " idMaior INT); ";
            sSql += " SELECT @intConta = COUNT(DISTINCT C.customer_id) FROM Customer(NOLOCK) C ";

            //sSql += " WHILE @intItera <= (@intConta / " + length + ") ";
            sSql += " WHILE @intItera <= " + page + " ";
            sSql += " BEGIN ";
	        sSql += "     IF @idMenor = 0 ";
	        sSql += "     BEGIN ";
		    sSql += "         SELECT @idMenor = MIN(C.customer_id)  ";
		    sSql += "           FROM Customer(NOLOCK) C ";
	        sSql += "     END ";
	        sSql += "     ELSE ";
	        sSql += "     BEGIN ";
		    sSql += "         SELECT @idMenor = MIN(C.customer_id) ";
		    sSql += "           FROM Customer(NOLOCK) C ";
		    sSql += "          WHERE customer_id > @idMaior; ";
	        sSql += "     END ";
	        sSql += "     IF @idMaior = 0 ";
	        sSql += "     BEGIN ";
		    sSql += "         SELECT @idMaior = MAX(C.customer_id) ";
		    sSql += "           FROM ( ";
			sSql += " 	            SELECT TOP " + length + " customer_id ";
			sSql += " 	              FROM Customer(NOLOCK) C ";
			sSql += " 	              ORDER BY Customer_id) C ";
	        sSql += "     END ";
	        sSql += "     ELSE ";
	        sSql += "     BEGIN ";
		    sSql += "         SELECT @idMaior = MAX(C.customer_id) ";
		    sSql += "           FROM ( ";
            sSql += " 	            SELECT TOP " + length + " customer_id ";
			sSql += " 	              FROM Customer(NOLOCK) C ";
			sSql += " 	             WHERE customer_id > @idMaior ";
			sSql += " 	              ORDER BY Customer_id) C ";
	        sSql += "     END ";
            sSql += "     IF @intItera = " + page + " BEGIN INSERT INTO @tbPagIds SELECT @intItera, @idMenor, @idMaior END ";
	        sSql += "     SET @intItera = @intItera + 1; ";
            sSql += " END ";
            sSql += " SELECT * FROM @tbPagIds order by pagina; ";

            return Db.Database.SqlQuery<paginacaoCustomer>(sSql).First();
        }

        public Customer GetByEmailSenha(string email, string password)
        {
            var retorno = Db.Customer.Where(c => c.email == email && c.password == password).FirstOrDefault();
            if (retorno==null)
                retorno = Db.Customer.Where(c => c.cpfCnpj == email && c.password == password).FirstOrDefault();

            return retorno;
        }

        internal string GetHash(string email)
        {
            string final = email + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss");
            string sSql = "SELECT CONVERT(VARCHAR(255), HashBytes('SHA1', '" + final + "'),2)";
            return Db.Database.SqlQuery<string>(sSql).FirstOrDefault();
        }

        public IEnumerable<Customer> GetSoundex(string term)
        {
            term = term.TrimStart().TrimEnd();
            var querie = 
                from c in base.Db.Customer
                where (SqlFunctions.SoundCode(c.name.TrimStart().TrimEnd()) == SqlFunctions.SoundCode(term) ||
                       SqlFunctions.SoundCode(c.email.TrimStart().TrimEnd()) == SqlFunctions.SoundCode(term) ||
                       SqlFunctions.SoundCode(c.cpfCnpj.TrimStart().TrimEnd()) == SqlFunctions.SoundCode(term))
                select c;

            return querie.OrderBy(m => m.name.StartsWith(term) ? 0 : 1).Take(10).ToList();
        }

        public Customer GetByEmail(string email)
        {
            return (from c in Db.Customer
                    where c.email.Equals(email)
                    select c).FirstOrDefault();
        }
    }
}
