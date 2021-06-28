using Redarbor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace Redarbor.Infrastructure.Data
{
    public class DataAccess<T> : IDataAccess<T>
    {
        private const string CONNECTION_STRING = "Server=(localdb)\\MSSQLLocalDB;Database=Redarbor;Integrated Security=True;";
        private const string ROWS_AFFECTED_PARAMETER = "@affectedRows";

        public async Task<int> SetDataInDataBase(T entity, string storeProcedure)
        {
            if (string.IsNullOrEmpty(storeProcedure))
            {
                throw new ArgumentNullException("stored procedure");
            }

            PropertyInfo[] properties = entity.GetType().GetProperties();
            SqlParameter[] parameters = makeSqlParameters(properties, entity);

            int result = await executeProcedureSetDataAsync(storeProcedure, parameters);

            return result;
        }

        public async Task<IReadOnlyList<T>> GetDataFromDataBase(string storeProcedure, int? id = null)
        {
            SqlParameter[] parameter = null;

            if (id != null)
            {
                parameter = new SqlParameter[]
                {
                    convertObjectToSqlParameter(id)
                };
            }

            DataTable result = await executeProcedureGetDataAsync(storeProcedure, parameter);
            var resultList = convertDataTableToList(result);

            return resultList;
        }

        private async Task<int> executeProcedureSetDataAsync(string cmdText, SqlParameter[] parameters)
        {
            int result = 0;
            SqlConnection conn = await openConnectionAsync();
            SqlCommand cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter rowsAffected = null;

            if (parameters != null)
            {
                rowsAffected = cmd.Parameters.Add(new SqlParameter(ROWS_AFFECTED_PARAMETER, SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                });

                cmd.Parameters.AddRange(parameters);
            }

            await cmd.ExecuteNonQueryAsync();
            result = (rowsAffected != null) ? (int)rowsAffected.Value : result;
            conn.Close();

            return result;
        }

        private async Task<DataTable> executeProcedureGetDataAsync(string cmdText, SqlParameter[] parameters = null)
        {
            SqlConnection conn = await openConnectionAsync();
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            var data = new DataTable();

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            data.Load(reader);
            conn.Close();

            return data;
        }

        private SqlParameter[] makeSqlParameters(PropertyInfo[] properties, T entity)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            foreach (PropertyInfo property in properties)
            {
                parameters.Add(convertPropertyToSqlParameter(property, entity));
            }

            return ((parameters.Count > 0) ? parameters.ToArray() : null);
        }

        private SqlParameter convertPropertyToSqlParameter(PropertyInfo property, T entity)
        {
            SqlParameter parameter = new SqlParameter(string.Format("@{0}", property.Name), property.GetValue(entity));

            return parameter;
        }

        private SqlParameter convertObjectToSqlParameter(object value)
        {
            SqlParameter parameter = new SqlParameter("@id", value);

            return parameter;
        }

        private async Task<SqlConnection> openConnectionAsync()
        {
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            await conn.OpenAsync();

            if (conn.State == ConnectionState.Open)
            {
                return conn;
            }

            await conn.CloseAsync();

            throw new Exception("Connection could not be established");
        }

        private List<T> convertDataTableToList(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = getItem(row);
                data.Add(item);
            }
            return data;
        }
        private T getItem(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            PropertyInfo[] properties = temp.GetProperties();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in properties)
                {
                    if (pro.Name == column.ColumnName)
                    {
                        pro.SetValue(obj, dr[column.ColumnName], null);
                        break;
                    }
                }
            }

            return obj;
        }
    }
}
