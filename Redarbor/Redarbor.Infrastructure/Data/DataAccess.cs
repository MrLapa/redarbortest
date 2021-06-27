using Redarbor.Core.Interfaces;
using Redarbor.Infrastructure.Extensions;
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
            if (string.IsNullOrEmpty(storeProcedure))
            {
                throw new ArgumentNullException("stored procedure");
            }

            SqlParameter[] parameter = null;

            if (id != null)
            {
                parameter = new SqlParameter[]
                {
                    convertObjectToSqlParameter(id)
                };
            }

            DataTable result = await executeProcedureGetDataAsync(storeProcedure, parameter);
            var resultList = makeEntityFromDataTable(result);

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

            result = await cmd.ExecuteNonQueryAsync();
            result = (rowsAffected != null) ? (int)rowsAffected.Value : result;
            conn.Close();

            return result;
        }

        private async Task<DataTable> executeProcedureGetDataAsync(string cmdText, SqlParameter[] parameters = null)
        {
            SqlConnection conn = await openConnectionAsync();
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            DataTable data = null;

            conn.Open();

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


        private T convertFromDBValue(object obj)
        {
            T result = (obj == null || obj == DBNull.Value) ? default : (T)obj;

            return result;
        }

        private List<T> makeEntityFromDataTable(DataTable data)
        {
            Type objType = typeof(T);
            List<T> collection = new List<T>();

            if (data == null || data.Rows.Count < 1)
            {
                return collection;
            }

            int matched = 0;

            foreach (DataRow row in data.Rows)
            {
                T item = (T)Activator.CreateInstance(objType);
                PropertyInfo[] properties = objType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    if (data.Columns.Contains(property.Name))
                    {
                        Type pType = property.PropertyType;
                        var defaultValue = pType.GetDefaultValue();
                        var value = row[property.Name];
                        value = convertFromDBValue(value);
                        property.SetValue(item, value);
                        matched++;

                        break;
                    }
                }

                if (matched != data.Columns.Count)
                {
                    throw new Exception("Data retrieved does not match specified model.");
                }

                collection.Add(item);
            }

            return collection;
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
    }
}
