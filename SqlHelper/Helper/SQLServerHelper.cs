using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Web;
using System.Web.Caching;

namespace TJVictor.DataBase.SqlHelper.SQLServer
{
    public class SQLServerHelper : ISQLHelper, ISQLCacheHelper, IDisposable
    {
        #region Property
        private SqlConnection globalConnection;
        private readonly int commandTimeout = 120;
        private readonly string _connectionStr;

        private DateTime absoluteExpiration;
        public DateTime AbsoluteExpiration
        {
            get { return absoluteExpiration; }
            set { absoluteExpiration = value; }
        }

        private TimeSpan slidingExpiration;
        public TimeSpan SlidingExpiration
        {
            get { return slidingExpiration; }
            set { slidingExpiration = value; }
        }

        private CacheItemPriority cachePriority;
        public CacheItemPriority CachePriority
        {
            get { return cachePriority; }
            set { cachePriority = value; }
        }
        #endregion

        #region Construction
        private SQLServerHelper(string connectionStr)
        {
            _connectionStr = connectionStr;
            globalConnection = new SqlConnection(_connectionStr);
            absoluteExpiration = Cache.NoAbsoluteExpiration;
            slidingExpiration = Cache.NoSlidingExpiration;
            this.cachePriority = CacheItemPriority.Default;
        }
        private SQLServerHelper(string connectionStr,
            DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority cachePriority)
        {
            _connectionStr = connectionStr;
            globalConnection = new SqlConnection(_connectionStr);
            this.absoluteExpiration = absoluteExpiration;
            this.slidingExpiration = slidingExpiration;
            this.cachePriority = cachePriority;
        }

        public static ISQLHelper GetInstace()
        {
            return new SQLServerHelper(
                ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString)
                as ISQLHelper;
        }

        public static ISQLHelper GetInstace(string connectionStr)
        {
            return new SQLServerHelper(connectionStr) as ISQLHelper;
        }

        public static ISQLCacheHelper GetCacheInstace()
        {
            System.Data.SqlClient.SqlDependency.Start(
                ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
            return new SQLServerHelper(
                ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString)
                as ISQLCacheHelper;
        }

        public static ISQLCacheHelper GetCacheInstace(string connectionStr)
        {
            System.Data.SqlClient.SqlDependency.Start(connectionStr);
            return new SQLServerHelper(connectionStr)
                as ISQLCacheHelper;
        }

        public static ISQLCacheHelper GetCacheInstace(string connectionStr,
            DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority cachePriority)
        {
            System.Data.SqlClient.SqlDependency.Start(connectionStr);
            return new SQLServerHelper(connectionStr, absoluteExpiration, slidingExpiration, cachePriority)
                as ISQLCacheHelper;
        }
        #endregion

        #region Public Function
        #region GetGlobalconnection
        public System.Data.Common.DbConnection GetGlobalconnection()
        {
            return globalConnection;
        }
        #endregion

        #region GetClassItem
        /// <summary>
        /// 获取指定类型的实例，注意泛型要有默认构造函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="executeSql"></param>
        /// <param name="parameters"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public T GetClassItem<T>(string executeSql, string[] parameters, object[] values) where T : class, new()
        {
            return GetClassItem<T>(executeSql, parameters, values, null);
        }
        public T GetClassItem<T>(string executeSql, string[] parameters, object[] values, string[] columns) where T : class, new()
        {
            List<T> items = GetClassItemList<T>(executeSql, CommandType.Text, parameters, values, columns);
            if (items.Count == 0)
                return null;
            return items[0];
        }

        public T SpGetClassItem<T>(string spName, string[] parameters, object[] values) where T : class, new()
        {
            return SpGetClassItem<T>(spName, parameters, values, null);
        }
        public T SpGetClassItem<T>(string spName, string[] parameters, object[] values, string[] columns) where T : class, new()
        {
            List<T> items = GetClassItemList<T>(spName, CommandType.StoredProcedure, parameters, values, columns);
            return items.Count == 0 ? null : items[0];
        }

        public List<T> GetClassItemList<T>(string executeSql, string[] parameters, object[] values) where T : class, new()
        {
            return GetClassItemList<T>(executeSql, parameters, values, null);
        }
        public List<T> GetClassItemList<T>(string executeSql, string[] parameters, object[] values, string[] columns) where T : class, new()
        {
            return GetClassItemList<T>(executeSql, CommandType.Text, parameters, values, columns);
        }

        public List<T> SpGetClassItemList<T>(string spName, string[] parameters, object[] values) where T : class, new()
        {
            return GetClassItemList<T>(spName, parameters, values, null);
        }
        public List<T> SpGetClassItemList<T>(string spName, string[] parameters, object[] values, string[] columns) where T : class, new()
        {
            return GetClassItemList<T>(spName, CommandType.StoredProcedure, parameters, values, columns);
        }

        public List<T> GetClassItemList<T>(string executeSql, CommandType commandType,
            string[] parameters, object[] values, string[] columns) where T : class, new()
        {
            List<T> items = new List<T>();
            using (IDataReader dr = this.ExecuteReader(executeSql, commandType, parameters, values))
            {
                while (dr.Read())
                {
                    T t = new T();
                    CopyFromDR(dr, t, columns);
                    items.Add(t);
                }
            }
            return items;
        }
        #endregion

        #region GetStructItem
        /// <summary>
        /// 获取一个标量的结果集。如果结果集为空，则Result里面的HasValue为false。设计Result的原因是为了区别，返回结果为null时，是无结果集，还是查询结果真的为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="executeSql"></param>
        /// <param name="parameters"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public Result<T> GetStructItem<T>(string executeSql, string[] parameters, object[] values) where T : struct
        {
            List<T?> items = GetStructItemList<T>(executeSql, CommandType.Text, parameters, values);
            return items.Count == 0 ? new Result<T>(false, null) : new Result<T>(true, items[0]);
        }

        public Result<T> SpGetStructItem<T>(string spName, string[] parameters, object[] values) where T : struct
        {
            List<T?> items = GetStructItemList<T>(spName, CommandType.StoredProcedure, parameters, values);
            return items.Count == 0 ? new Result<T>(false, null) : new Result<T>(true, items[0]);
        }

        public List<T?> GetStructItemList<T>(string executeSql, string[] parameters, object[] values) where T : struct
        {
            return GetStructItemList<T>(executeSql, CommandType.Text, parameters, values);
        }

        public List<T?> SpGetStructItemList<T>(string spName, string[] parameters, object[] values) where T : struct
        {
            return GetStructItemList<T>(spName, CommandType.StoredProcedure, parameters, values);
        }

        public List<T?> GetStructItemList<T>(string executeSql, CommandType commandType,
            string[] parameters, object[] values) where T : struct
        {
            List<T?> items = new List<T?>();
            using (IDataReader dr = this.ExecuteReader(executeSql, commandType, parameters, values))
            {
                while (dr.Read())
                {
                    if (dr.IsDBNull(0))
                        items.Add(null);
                    else
                        items.Add((T)dr.GetValue(0));
                }
            }
            return items;
        }
        #endregion

        #region GetStringItem
        public List<string> GetStringItemList(string executeSql, CommandType commandType,
            string[] parameters, object[] values)
        {
            List<string> items = new List<string>();
            using (IDataReader dr = this.ExecuteReader(executeSql, commandType, parameters, values))
            {
                while (dr.Read())
                {
                    if (dr.IsDBNull(0))
                        items.Add(null);
                    else
                        items.Add(dr.GetString(0));
                }
            }

            return items;
        }
        #endregion
        #endregion

        #region Common Function
        #region ExecuteNonQuery and SpExecuteNonQuery
        public virtual int ExecuteNonQuery(string executeSql)
        {
            return this.ExecuteNonQuery(executeSql, false, CommandType.Text, null, null);
        }

        public int ExecuteNonQuery(string executeSql, bool transaction)
        {
            return this.ExecuteNonQuery(executeSql, transaction, CommandType.Text, null, null);
        }

        public int ExecuteNonQuery(string executeSql, string[] parameters, object[] values)
        {
            return this.ExecuteNonQuery(executeSql, false, CommandType.Text, parameters, values);
        }

        public int ExecuteNonQuery(string executeSql, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteNonQuery(executeSql, transaction, CommandType.Text, parameters, values);
        }

        public int SpExecuteNonQuery(string spName)
        {
            return this.ExecuteNonQuery(spName, false, CommandType.StoredProcedure, null, null);
        }

        public int SpExecuteNonQuery(string spName, bool transaction)
        {
            return this.ExecuteNonQuery(spName, transaction, CommandType.StoredProcedure, null, null);
        }

        public int SpExecuteNonQuery(string spName, string[] parameters, object[] values)
        {
            return this.ExecuteNonQuery(spName, false, CommandType.StoredProcedure, parameters, values);
        }

        public int SpExecuteNonQuery(string spName, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteNonQuery(spName, transaction, CommandType.StoredProcedure, parameters, values);
        }

        public int ExecuteNonQuery(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values)
        {
            return ExecuteNonQuery(executeSql, transaction, commandType, parameters, values, null, null);
        }

        public int SpExecuteNonQueryWithOutput(string spName, bool transaction, string[] parameters, object[] values,
            string[] outputParameters, object[] outValues)
        {
            return ExecuteNonQuery(spName, transaction, CommandType.StoredProcedure, parameters, values,
                outputParameters, outValues);
        }

        public int ExecuteNonQuery(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values, string[] outputParameters, object[] outValues)
        {
            int result = -1;

            SqlCommand sqlCommand = CreateSqlCommand(executeSql, transaction, commandType,
                parameters, values, outputParameters, outValues);

            try
            {
                result = sqlCommand.ExecuteNonQuery();
                if (transaction)
                    sqlCommand.Transaction.Commit();
                FillOutputValues(sqlCommand, outputParameters, outValues);

                return result;
            }
            catch
            {
                if (transaction)
                    sqlCommand.Transaction.Rollback();
                throw;
            }
            finally
            {
                sqlCommand.Connection.Close();
            }
        }
        #endregion

        #region DataReader and SpExecuteReader
        public IDataReader ExecuteReader(string executeSql)
        {
            return ExecuteReader(executeSql, CommandType.Text, null, null);
        }

        public IDataReader ExecuteReader(string executeSql, string[] parameters, object[] values)
        {
            return ExecuteReader(executeSql, CommandType.Text, parameters, values);
        }

        public IDataReader SpExecuteReader(string spName)
        {
            return ExecuteReader(spName, CommandType.StoredProcedure, null, null);
        }

        public IDataReader SpExecuteReader(string spName, string[] parameters, object[] values)
        {
            return ExecuteReader(spName, CommandType.StoredProcedure, parameters, values);
        }

        public IDataReader ExecuteReader(string executeSql, CommandType commandType,
            string[] parameters, object[] values)
        {

            SqlCommand sqlCommand = CreateSqlCommand(executeSql, false, commandType,
                parameters, values);
            try
            {
                return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                sqlCommand.Connection.Close();
                throw;
            }
            finally
            {
            }
        }
        #endregion

        #region ExecuteScalar and SpExecuteScalar
        public T ExecuteScalar<T>(string executeSql)
        {
            return this.ExecuteScalar<T>(executeSql, false, CommandType.Text, null, null);
        }

        public T ExecuteScalar<T>(string executeSql, bool transaction)
        {
            return this.ExecuteScalar<T>(executeSql, transaction, CommandType.Text, null, null);
        }

        public T ExecuteScalar<T>(string executeSql, string[] parameters, object[] values)
        {
            return this.ExecuteScalar<T>(executeSql, false, CommandType.Text, parameters, values);
        }

        public T ExecuteScalar<T>(string executeSql, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteScalar<T>(executeSql, transaction, CommandType.Text, parameters, values);
        }

        public T SpExecuteScalar<T>(string spName)
        {
            return this.ExecuteScalar<T>(spName, false, CommandType.StoredProcedure, null, null);
        }

        public T SpExecuteScalar<T>(string spName, bool transaction)
        {
            return this.ExecuteScalar<T>(spName, transaction, CommandType.StoredProcedure, null, null);
        }

        public T SpExecuteScalar<T>(string spName, string[] parameters, object[] values)
        {
            return this.ExecuteScalar<T>(spName, false, CommandType.StoredProcedure, parameters, values);
        }

        public T SpExecuteScalar<T>(string spName, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteScalar<T>(spName, transaction, CommandType.StoredProcedure, parameters, values);
        }

        public T ExecuteScalar<T>(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values)
        {
            T result = default(T);
            SqlCommand sqlCommand = CreateSqlCommand(executeSql, transaction, commandType,
                parameters, values);
            try
            {
                object ob = sqlCommand.ExecuteScalar();
                if (ob == DBNull.Value)
                    ob = null;
                result = (T)ob;
                if (transaction)
                    sqlCommand.Transaction.Commit();
                return result;
            }
            catch
            {
                if (transaction)
                    sqlCommand.Transaction.Rollback();
                throw;
            }
            finally
            {
                sqlCommand.Connection.Close();
            }
        }
        #endregion

        #region ExecuteDataSet and SpExecuteDataSet
        public DataSet ExecuteDataSet(string executeSql)
        {
            return this.ExecuteDataSet(executeSql, false, CommandType.Text, null, null);
        }

        public DataSet ExecuteDataSet(string executeSql, bool transaction)
        {
            return this.ExecuteDataSet(executeSql, transaction, CommandType.Text, null, null);
        }

        public DataSet ExecuteDataSet(string executeSql, string[] parameters, object[] values)
        {
            return this.ExecuteDataSet(executeSql, false, CommandType.Text, parameters, values);
        }

        public DataSet ExecuteDataSet(string executeSql, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteDataSet(executeSql, transaction, CommandType.Text, parameters, values);
        }

        public DataSet SpExecuteDataSet(string spName)
        {
            return this.ExecuteDataSet(spName, false, CommandType.StoredProcedure, null, null);
        }

        public DataSet SpExecuteDataSet(string spName, bool transaction)
        {
            return this.ExecuteDataSet(spName, transaction, CommandType.StoredProcedure, null, null);
        }

        public DataSet SpExecuteDataSet(string spName, string[] parameters, object[] values)
        {
            return this.ExecuteDataSet(spName, false, CommandType.StoredProcedure, parameters, values);
        }

        public DataSet SpExecuteDataSet(string spName, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteDataSet(spName, transaction, CommandType.StoredProcedure, parameters, values);
        }

        public DataSet ExecuteDataSet(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values)
        {
            DataSet result = new DataSet();

            SqlCommand sqlCommand = CreateSqlCommand(executeSql, transaction, commandType,
                parameters, values);

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);
                adp.Fill(result);
                if (transaction)
                    sqlCommand.Transaction.Commit();
                return result;
            }
            catch
            {
                if (transaction)
                    sqlCommand.Transaction.Rollback();
                throw;
            }
            finally
            {
                sqlCommand.Connection.Close();
            }
        }
        #endregion

        #region ExecuteTable and SpExecuteTable
        public DataTable ExecuteTable(string executeSql)
        {
            return this.ExecuteTable(executeSql, false, CommandType.Text, null, null);
        }

        public DataTable ExecuteTable(string executeSql, bool transaction)
        {
            return this.ExecuteTable(executeSql, transaction, CommandType.Text, null, null);
        }

        public DataTable ExecuteTable(string executeSql, string[] parameters, object[] values)
        {
            return this.ExecuteTable(executeSql, false, CommandType.Text, parameters, values);
        }

        public DataTable ExecuteTable(string executeSql, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteTable(executeSql, transaction, CommandType.Text, parameters, values);
        }

        public DataTable SpExecuteTable(string spName)
        {
            return this.ExecuteTable(spName, false, CommandType.StoredProcedure, null, null);
        }

        public DataTable SpExecuteTable(string spName, bool transaction)
        {
            return this.ExecuteTable(spName, transaction, CommandType.StoredProcedure, null, null);
        }

        public DataTable SpExecuteTable(string spName, string[] parameters, object[] values)
        {
            return this.ExecuteTable(spName, false, CommandType.StoredProcedure, parameters, values);
        }

        public DataTable SpExecuteTable(string spName, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteTable(spName, transaction, CommandType.StoredProcedure, parameters, values);
        }

        public DataTable ExecuteTable(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values)
        {
            DataTable result = new DataTable();

            SqlCommand sqlCommand = CreateSqlCommand(executeSql, transaction, commandType,
                parameters, values);

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);
                adp.Fill(result);
                if (transaction)
                    sqlCommand.Transaction.Commit();
                return result;
            }
            catch
            {
                if (transaction)
                    sqlCommand.Transaction.Rollback();
                throw;
            }
            finally
            {
                sqlCommand.Connection.Close();
            }
        }
        #endregion

        #region ExecuteXmlReader and SpExcuteXmlReader
        public XmlReader ExecuteXmlReader(string executeSql)
        {
            return ExecuteXmlReader(executeSql, CommandType.Text, null, null);
        }

        public XmlReader ExecuteXmlReader(string executeSql, string[] parameters, object[] values)
        {
            return ExecuteXmlReader(executeSql, CommandType.Text, parameters, values);
        }

        public XmlReader SpExecuteXmlReader(string spName)
        {
            return ExecuteXmlReader(spName, CommandType.StoredProcedure, null, null);
        }

        public XmlReader SpExecuteXmlReader(string spName, string[] parameters, object[] values)
        {
            return ExecuteXmlReader(spName, CommandType.StoredProcedure, parameters, values);
        }

        public XmlReader ExecuteXmlReader(string executeSql, CommandType commandType,
            string[] parameters, object[] values)
        {

            SqlCommand sqlCommand = CreateSqlCommand(executeSql, false, commandType,
                parameters, values);
            try
            {
                return sqlCommand.ExecuteXmlReader();
            }
            catch
            {
                sqlCommand.Connection.Close();
                throw;
            }
            finally
            {
            }
        }
        #endregion

        #region Global
        public int GlobalExecuteNonQuery(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values, string[] outputParameters, object[] outputValues)
        {
            int result = -1;

            SqlCommand sqlCommand = new SqlCommand(executeSql, globalConnection);
            sqlCommand.Connection = globalConnection;
            sqlCommand.CommandText = executeSql;
            sqlCommand.CommandTimeout = commandTimeout;
            sqlCommand.CommandType = commandType;

            try
            {
                if (transaction)
                    sqlCommand.Transaction =
                        globalConnection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                FillParameters(sqlCommand, parameters, values, outputParameters, outputValues);
                result = sqlCommand.ExecuteNonQuery();
                if (transaction)
                    sqlCommand.Transaction.Commit();
                return result;
            }
            catch
            {
                if (transaction)
                    sqlCommand.Transaction.Rollback();
                throw;
            }
        }
        #endregion
        #endregion

        #region Private Function
        private void CopyFromDR(IDataReader reader, object ob, string[] columns)
        {
            Common.CopyFrom(reader, ob, columns);
        }

        private void FillParameters(SqlCommand command, string[] parameters, object[] values,
            string[] outputParameters, object[] outputValues)
        {
            if (values != null && values.Length != 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    string parameter = parameters[i];
                    object value = values[i];
                    if (value != null)
                        command.Parameters.AddWithValue(parameter, value);
                    else
                        command.Parameters.AddWithValue(parameter, DBNull.Value);
                }
            }

            if (outputParameters != null && outputParameters.Length != 0)
            {
                for (int i = 0; i < outputParameters.Length; i++)
                {
                    string parameter = outputParameters[i];
                    object value = outputValues[i];
                    if (value is string)
                    {
                        command.Parameters.Add(parameter, SqlDbType.NVarChar, 4000);
                        command.Parameters[parameter].Direction = ParameterDirection.Output;
                    }
                    else if (value != null)
                        command.Parameters.AddWithValue(parameter, value).Direction = ParameterDirection.Output;
                    else
                        command.Parameters.AddWithValue(parameter, DBNull.Value).Direction = ParameterDirection.Output;
                }
            }
        }

        private void FillOutputValues(SqlCommand cmd, string[] outputParameters, object[] outputValues)
        {
            if (outputValues != null && outputValues.Length != 0)
            {
                for (int count = 0; count < outputValues.Length; count++)
                {
                    outputValues[count] = cmd.Parameters[outputParameters[count]].Value;
                }
            }
        }

        private SqlCommand CreateSqlCommand(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values)
        {
            return CreateSqlCommand(executeSql, transaction, commandType, parameters, values, null, null);
        }

        private SqlCommand CreateSqlCommand(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values, string[] outputParameters, object[] outputValues)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionStr);
            SqlCommand sqlCommand = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(executeSql, sqlConnection);
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandTimeout = commandTimeout;
                if (transaction)
                    sqlCommand.Transaction =
                        sqlConnection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                FillParameters(sqlCommand, parameters, values, outputParameters, outputValues);

                return sqlCommand;
            }
            catch
            {
                sqlConnection.Close();
                throw;
            }
        }

        private object GetCache(string cacheKey)
        {
            return HttpRuntime.Cache.Get(cacheKey);
        }

        private void InsertCache(string cacheKey, object ob,
            SqlCommand cmd, CacheItemRemovedCallback removedCallback)
        {
            SqlCacheDependency scd = new SqlCacheDependency(cmd);
            using (cmd.Connection)
            {
                cmd.ExecuteNonQuery();
            }
            HttpRuntime.Cache.Insert(cacheKey, ob, scd, absoluteExpiration, slidingExpiration,
               cachePriority, removedCallback);

        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            System.Data.SqlClient.SqlDependency.Stop(_connectionStr);
            if (globalConnection != null && globalConnection.State != ConnectionState.Closed)
                globalConnection.Close();
        }

        #endregion

        #region ISQLCacheHelper Members
        #region GetClassItemFromCache
        public T GetClassItemFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values) where T : class, new()
        {
            return GetClassItemFromCache<T>(cacheKey, executeSql, parameters, values, null);
        }
        public T GetClassItemFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values, string[] columns) where T : class, new()
        {
            List<T> items = GetClassItemListFromCache<T>(cacheKey, executeSql, CommandType.Text, parameters, values, columns, null);
            if (items.Count == 0)
                return null;
            return items[0];
        }

        public T SpGetClassItemFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values) where T : class, new()
        {
            return SpGetClassItemFromCache<T>(cacheKey, spName, parameters, values, null);
        }
        public T SpGetClassItemFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values, string[] columns) where T : class, new()
        {
            List<T> items = GetClassItemListFromCache<T>(cacheKey, spName, CommandType.StoredProcedure, parameters, values, columns, null);
            return items.Count == 0 ? null : items[0];
        }

        public List<T> GetClassItemListFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values) where T : class, new()
        {
            return GetClassItemListFromCache<T>(cacheKey, executeSql, parameters, values, null);
        }
        public List<T> GetClassItemListFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values, string[] columns) where T : class, new()
        {
            return GetClassItemListFromCache<T>(cacheKey, executeSql, CommandType.Text, parameters, values, columns, null);
        }

        public List<T> SpGetClassItemListFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values) where T : class, new()
        {
            return SpGetClassItemListFromCache<T>(cacheKey, spName, parameters, values, null);
        }
        public List<T> SpGetClassItemListFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values, string[] columns) where T : class, new()
        {
            return GetClassItemListFromCache<T>(cacheKey, spName, CommandType.StoredProcedure, parameters, values, columns, null);
        }

        public List<T> GetClassItemListFromCache<T>(string cacheKey, string executeSql, CommandType commandType,
            string[] parameters, object[] values, string[] columns,
            CacheItemRemovedCallback removedCallback) where T : class, new()
        {
            List<T> items = GetCache(cacheKey) as List<T>;
            if (items != null)
                return items;

            items = new List<T>();
            using (IDataReader dr = this.ExecuteReader(executeSql, commandType, parameters, values))
            {
                while (dr.Read())
                {
                    T t = new T();
                    CopyFromDR(dr, t, columns);
                    items.Add(t);
                }
            }

            InsertCache(cacheKey, items,
                CreateSqlCommand(executeSql, false, commandType, parameters, values),
                removedCallback);

            return items;
        }
        #endregion

        #region GetStructItemFromCache
        public Result<T> GetStructItemFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values) where T : struct
        {
            List<T?> items = GetStructItemListFromCache<T>(cacheKey, executeSql, CommandType.Text, parameters, values, null);
            return items.Count == 0 ? new Result<T>(false, null) : new Result<T>(true, items[0]);
        }

        public Result<T> SpGetStructItemFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values) where T : struct
        {
            List<T?> items = GetStructItemListFromCache<T>(cacheKey, spName, CommandType.StoredProcedure, parameters, values, null);
            return items.Count == 0 ? new Result<T>(false, null) : new Result<T>(true, items[0]);
        }

        public List<T?> GetStructItemListFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values) where T : struct
        {
            return GetStructItemListFromCache<T>(cacheKey, executeSql, CommandType.Text, parameters, values, null);
        }

        public List<T?> SpGetStructItemListFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values) where T : struct
        {
            return GetStructItemListFromCache<T>(cacheKey, spName, CommandType.StoredProcedure, parameters, values, null);
        }

        public List<T?> GetStructItemListFromCache<T>(string cacheKey, string executeSql, CommandType commandType, string[] parameters, object[] values, CacheItemRemovedCallback removedCallback) where T : struct
        {
            List<T?> items = GetCache("cacheKey") as List<T?>;
            if (items != null)
                return items;

            items = new List<T?>();
            using (IDataReader dr = this.ExecuteReader(executeSql, commandType, parameters, values))
            {
                while (dr.Read())
                {
                    if (dr.IsDBNull(0))
                        items.Add(null);
                    else
                        items.Add((T)dr.GetValue(0));
                }
            }

            InsertCache(cacheKey, items,
                CreateSqlCommand(executeSql, false, commandType, parameters, values),
                removedCallback);

            return items;
        }
        #endregion

        #region GetStringItemFromCache
        public List<string> GetStringItemListFromCache(string cacheKey, string executeSql, CommandType commandType, string[] parameters, object[] values, CacheItemRemovedCallback removedCallback)
        {
            List<string> items = GetCache(cacheKey) as List<string>;
            if (items != null)
                return items;
            items = new List<string>();
            using (IDataReader dr = this.ExecuteReader(executeSql, commandType, parameters, values))
            {
                while (dr.Read())
                {
                    if (dr.IsDBNull(0))
                        items.Add(null);
                    else
                        items.Add(dr.GetString(0));
                }
            }

            InsertCache(cacheKey, items,
                CreateSqlCommand(executeSql, false, commandType, parameters, values),
                removedCallback);


            return items;
        }
        #endregion

        #region ExecuteScalarFromCache and SpExecuteScalarFromCache
        public T ExecuteScalarFromCache<T>(string cacheKey, string executeSql)
        {
            return this.ExecuteScalarFromCache<T>(cacheKey, executeSql, false, CommandType.Text, null, null, null);
        }
        public T ExecuteScalarFromCache<T>(string cacheKey, string executeSql, bool transaction)
        {
            return this.ExecuteScalarFromCache<T>(cacheKey, executeSql, transaction, CommandType.Text, null, null, null);
        }
        public T ExecuteScalarFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values)
        {
            return this.ExecuteScalarFromCache<T>(cacheKey, executeSql, false, CommandType.Text, parameters, values, null);
        }
        public T ExecuteScalarFromCache<T>(string cacheKey, string executeSql, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteScalarFromCache<T>(cacheKey, executeSql, transaction, CommandType.Text, parameters, values, null);
        }

        public T SpExecuteScalarFromCache<T>(string cacheKey, string spName)
        {
            return this.ExecuteScalarFromCache<T>(cacheKey, spName, false, CommandType.StoredProcedure, null, null, null);
        }
        public T SpExecuteScalarFromCache<T>(string cacheKey, string spName, bool transaction)
        {
            return this.ExecuteScalarFromCache<T>(cacheKey, spName, transaction, CommandType.StoredProcedure, null, null, null);
        }
        public T SpExecuteScalarFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values)
        {
            return this.ExecuteScalarFromCache<T>(cacheKey, spName, false, CommandType.StoredProcedure, parameters, values, null);
        }
        public T SpExecuteScalarFromCache<T>(string cacheKey, string spName, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteScalarFromCache<T>(cacheKey, spName, transaction, CommandType.StoredProcedure, parameters, values, null);
        }

        public T ExecuteScalarFromCache<T>(string cacheKey, string executeSql, bool transaction, CommandType commandType, string[] parameters, object[] values, CacheItemRemovedCallback removedCallback)
        {
            T result = (T)GetCache(cacheKey);
            if (result != null)
                return result;
            result = default(T);
            SqlCommand sqlCommand = CreateSqlCommand(executeSql, transaction, commandType,
                parameters, values);
            try
            {
                object ob = sqlCommand.ExecuteScalar();
                if (ob == DBNull.Value)
                    ob = null;
                result = (T)ob;
                if (transaction)
                    sqlCommand.Transaction.Commit();

                InsertCache(cacheKey, result,
                    CreateSqlCommand(executeSql, false, commandType, parameters, values), removedCallback);

                return result;
            }
            catch
            {
                if (transaction)
                    sqlCommand.Transaction.Rollback();
                throw;
            }
            finally
            {
                sqlCommand.Connection.Close();
            }
        }
        #endregion

        #region ExecuteDataSetFromCache and SpExecuteDataSetFromCache
        public DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql)
        {
            return this.ExecuteDataSetFromCache(cacheKey, executeSql, false, CommandType.Text, null, null, null);
        }
        public DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql, bool transaction)
        {
            return this.ExecuteDataSetFromCache(cacheKey, executeSql, transaction, CommandType.Text, null, null, null);
        }
        public DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql, string[] parameters, object[] values)
        {
            return this.ExecuteDataSetFromCache(cacheKey, executeSql, false, CommandType.Text, parameters, values, null);
        }
        public DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteDataSetFromCache(cacheKey, executeSql, transaction, CommandType.Text, parameters, values, null);
        }

        public DataSet SpExecuteDataSetFromCache(string cacheKey, string spName)
        {
            return this.ExecuteDataSetFromCache(cacheKey, spName, false, CommandType.StoredProcedure, null, null, null);
        }
        public DataSet SpExecuteDataSetFromCache(string cacheKey, string spName, bool transaction)
        {
            return this.ExecuteDataSetFromCache(cacheKey, spName, transaction, CommandType.StoredProcedure, null, null, null);
        }
        public DataSet SpExecuteDataSetFromCache(string cacheKey, string spName, string[] parameters, object[] values)
        {
            return this.ExecuteDataSetFromCache(cacheKey, spName, false, CommandType.StoredProcedure, parameters, values, null);
        }
        public DataSet SpExecuteDataSetFromCache(string cacheKey, string spName, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteDataSetFromCache(cacheKey, spName, transaction, CommandType.StoredProcedure, parameters, values, null);
        }

        public DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql, bool transaction, CommandType commandType, string[] parameters, object[] values, CacheItemRemovedCallback removedCallback)
        {
            DataSet result = GetCache(cacheKey) as DataSet;
            if (result != null)
                return result;

            result = new DataSet();

            SqlCommand sqlCommand = CreateSqlCommand(executeSql, transaction, commandType,
                parameters, values);

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);
                adp.Fill(result);
                if (transaction)
                    sqlCommand.Transaction.Commit();

                InsertCache(cacheKey, result,
                    CreateSqlCommand(executeSql, false, commandType, parameters, values), removedCallback);

                return result;
            }
            catch
            {
                if (transaction)
                    sqlCommand.Transaction.Rollback();
                throw ;
            }
            finally
            {
                sqlCommand.Connection.Close();
            }
        }
        #endregion

        #region ExecuteTableFromCache and SpExecuteTableFromCache
        public DataTable ExecuteTableFromCache(string cacheKey, string executeSql)
        {
            return this.ExecuteTableFromCache(cacheKey, executeSql, false, CommandType.Text, null, null, null);
        }
        public DataTable ExecuteTableFromCache(string cacheKey, string executeSql, bool transaction)
        {
            return this.ExecuteTableFromCache(cacheKey, executeSql, transaction, CommandType.Text, null, null, null);
        }
        public DataTable ExecuteTableFromCache(string cacheKey, string executeSql, string[] parameters, object[] values)
        {
            return this.ExecuteTableFromCache(cacheKey, executeSql, false, CommandType.Text, parameters, values, null);
        }
        public DataTable ExecuteTableFromCache(string cacheKey, string executeSql, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteTableFromCache(cacheKey, executeSql, transaction, CommandType.Text, parameters, values, null);
        }

        public DataTable SpExecuteTableFromCache(string cacheKey, string spName)
        {
            return this.ExecuteTableFromCache(cacheKey, spName, false, CommandType.StoredProcedure, null, null, null);
        }
        public DataTable SpExecuteTableFromCache(string cacheKey, string spName, bool transaction)
        {
            return this.ExecuteTableFromCache(cacheKey, spName, transaction, CommandType.StoredProcedure, null, null, null);
        }
        public DataTable SpExecuteTableFromCache(string cacheKey, string spName, string[] parameters, object[] values)
        {
            return this.ExecuteTableFromCache(cacheKey, spName, false, CommandType.StoredProcedure, parameters, values, null);
        }
        public DataTable SpExecuteTableFromCache(string cacheKey, string spName, bool transaction, string[] parameters, object[] values)
        {
            return this.ExecuteTableFromCache(cacheKey, spName, transaction, CommandType.StoredProcedure, parameters, values, null);
        }

        public DataTable ExecuteTableFromCache(string cacheKey, string executeSql, bool transaction, CommandType commandType, string[] parameters, object[] values, CacheItemRemovedCallback removedCallback)
        {
            DataTable result = GetCache(cacheKey) as DataTable;
            if (result != null)
                return result;
            result = new DataTable();

            SqlCommand sqlCommand = CreateSqlCommand(executeSql, transaction, commandType,
                parameters, values);

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);
                adp.Fill(result);
                if (transaction)
                    sqlCommand.Transaction.Commit();

                InsertCache(cacheKey, result,
                    CreateSqlCommand(executeSql, false, commandType, parameters, values), removedCallback);

                //InsertCache(cacheKey, result,
                //    sqlCommand, removedCallback);

                return result;
            }
            catch
            {
                if (transaction)
                    sqlCommand.Transaction.Rollback();
                throw;
            }
            finally
            {
                sqlCommand.Connection.Close();
            }
        }
        #endregion
        #endregion
    }
}
