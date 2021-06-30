using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.Domain.Utils
{
    public static class DataTableBootstrap
    {

        public static string Paging<TSource>(string query, DataTableModel model)
        {
            int indexSelect = query.ToUpper().IndexOf("SELECT", 0);
            query = query.Insert(indexSelect + 6, " row_number() OVER (ORDER BY " + Order<TSource>(model) + ") rn,");

            int indexWhere = query.ToUpper().LastIndexOf("WHERE");
            if (indexWhere > -1)
                query = query.Insert(indexWhere + 5, " 1=1 " + Where<TSource>(model) + " AND ");
            else
                query += " WHERE 1=1 " + Where<TSource>(model);

            return "SELECT * FROM (" + query + ") as tbl WHERE rn > " + model.start + " AND rn <= " + CurrentPage(model.start, model.length) * model.length;
        }

        public static string TotalRegistros<TSource>(string query, DataTableModel model)
        {
            int indexWhere = query.ToUpper().IndexOf("WHERE", 0);
            if (indexWhere > -1)
                query = query.Replace(query.Substring(indexWhere, 5), "WHERE 1=1 " + Where<TSource>(model) + " AND ");
            else
                query += " WHERE 1=1 " + Where<TSource>(model);

            return "SELECT COUNT(*) registros FROM (" + query + ") as tbl";
        }

        private static string Order<TSource>(DataTableModel model)
        {
            var order = OrderValidate(model.order);
            var column = model.columns[order.column];

            if (column.orderable)
            {
                var propertyType = typeof(TSource).GetProperty(column.data).PropertyType;

                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propertyType = propertyType.GetGenericArguments()[0];

                switch (propertyType.Name.ToUpper())
                {
                    default:
                        return column.name + " " + order.dir;
                        break;
                }
            }
            else
                return "";
        }

        private static DataTableOrderModel OrderValidate(DataTableOrderModel[] order)
        {
            if (order != null)
                return order.First();
            else
                return new DataTableOrderModel
                {
                    column = 0,
                    dir = "asc"
                };
        }

        private static string Where<TSource>(DataTableModel model)
        {
            string where = "";
            string aux = "";

            if (!String.IsNullOrEmpty(model.search.value))
            {
                foreach (var coluna in model.columns)
                {
                    aux = WhereValidate<TSource>(model.search.value, coluna);
                    where += (!String.IsNullOrEmpty(where) && !String.IsNullOrEmpty(aux) ? " OR " : " ") + (!String.IsNullOrEmpty(aux) ? aux : "");
                }
            }

            if(!String.IsNullOrEmpty(where))
                where = " AND (" + where + ")";

            return where;
        }

        private static string WhereValidate<TSource>(string search, DataTableColumnModel column)
        {
            string retorno = "";

            if (!String.IsNullOrEmpty(column.data) && column.searchable)
            {
                var propertyType = typeof (TSource).GetProperty(column.data).PropertyType;

                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propertyType = propertyType.GetGenericArguments()[0];

                switch (propertyType.Name.ToUpper())
                {
                    case "DECIMAL":
                    case "INT":
                        retorno += column.name + " like '%" + search + "%'";
                        break;

                    case "DATETIME":
                        retorno += "CONVERT(VARCHAR, " + column.name + ", 103) like '%" + search + "%'";
                        break;

                    default:
                        retorno += "UPPER(" + column.name + ") like UPPER('%" + search + "%')";
                        break;
                }
            }

            return retorno;
        }

        private static int CurrentPage(int index, int PageSize)
        {
            int rest = (index + 1) % PageSize;
            int curPage = (int)((index + 1) / PageSize);

            if (rest > 0)
                curPage++;

            return curPage;
        }

        public static DataTableResponseModel ToResponseModel<TSource>(this IEnumerable<TSource> source, DataTableModel model, int totalRegistros)
        {
            return new DataTableResponseModel
            {
                draw = model.draw,
                recordsTotal = totalRegistros,
                recordsFiltered = totalRegistros,
                data = source.ToList()
            };
        }
    }

    public class DataTableModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public DataTableSearchModel search { get; set; }
        public DataTableOrderModel[] order { get; set; }
        public DataTableColumnModel[] columns { get; set; }
    }

    public class DataTableColumnModel
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public DataTableSearchModel search { get; set; }
    }

    public class DataTableOrderModel
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class DataTableSearchModel
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class DataTableResponseModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object data { get; set; }
        public string error { get; set; }
        public object footer { get; set; } //Coluna adicional (utilizada para totais)
    }
}