using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.Caching;

namespace TJVictor.DataBase.SqlHelper
{
    public interface ISQLCacheHelper
    {
        #region Access Interface
        DateTime AbsoluteExpiration { get; set; }
        TimeSpan SlidingExpiration { get; set; }
        CacheItemPriority CachePriority { get; set; }
        #endregion

        #region GetClassItem
        T GetClassItemFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values) where T : class, new();
        T GetClassItemFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values, string[] columns) where T : class, new();

        T SpGetClassItemFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values) where T : class, new();
        T SpGetClassItemFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values, string[] columns) where T : class, new();

        List<T> GetClassItemListFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values) where T : class, new();
        List<T> GetClassItemListFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values, string[] columns) where T : class, new();

        List<T> SpGetClassItemListFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values) where T : class, new();
        List<T> SpGetClassItemListFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values, string[] columns) where T : class, new();

        List<T> GetClassItemListFromCache<T>(string cacheKey, string executeSql, CommandType commandType,
            string[] parameters, object[] values, string[] columns,
            CacheItemRemovedCallback removedCallback) where T : class, new();
        #endregion

        #region GetStructItem
        Result<T> GetStructItemFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values) where T : struct;

        Result<T> SpGetStructItemFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values) where T : struct;

        List<T?> GetStructItemListFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values) where T : struct;

        List<T?> SpGetStructItemListFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values) where T : struct;

        List<T?> GetStructItemListFromCache<T>(string cacheKey, string executeSql, CommandType commandType,
            string[] parameters, object[] values,
            CacheItemRemovedCallback removedCallback) where T : struct;
        #endregion

        #region GetStringItem
        List<string> GetStringItemListFromCache(string cacheKey, string executeSql, CommandType commandType,
            string[] parameters, object[] values,
            CacheItemRemovedCallback removedCallback);
        #endregion

        #region Common Function
        #region ExecuteScalar and SpExecuteScalar
        T ExecuteScalarFromCache<T>(string cacheKey, string executeSql);

        T ExecuteScalarFromCache<T>(string cacheKey, string executeSql, bool transaction);

        T ExecuteScalarFromCache<T>(string cacheKey, string executeSql, string[] parameters, object[] values);

        T ExecuteScalarFromCache<T>(string cacheKey, string executeSql, bool transaction, string[] parameters, object[] values);

        T SpExecuteScalarFromCache<T>(string cacheKey, string spName);

        T SpExecuteScalarFromCache<T>(string cacheKey, string spName, bool transaction);

        T SpExecuteScalarFromCache<T>(string cacheKey, string spName, string[] parameters, object[] values);

        T SpExecuteScalarFromCache<T>(string cacheKey, string spName, bool transaction, string[] parameters, object[] values);

        T ExecuteScalarFromCache<T>(string cacheKey, string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values, CacheItemRemovedCallback removedCallback);
        #endregion

        #region ExecuteDataSet and SpExecuteDataSet
        DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql);

        DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql, bool transaction);

        DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql, string[] parameters, object[] values);

        DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql, bool transaction, string[] parameters, object[] values);

        DataSet SpExecuteDataSetFromCache(string cacheKey, string spName);

        DataSet SpExecuteDataSetFromCache(string cacheKey, string spName, bool transaction);

        DataSet SpExecuteDataSetFromCache(string cacheKey, string spName, string[] parameters, object[] values);

        DataSet SpExecuteDataSetFromCache(string cacheKey, string spName, bool transaction, string[] parameters, object[] values);

        DataSet ExecuteDataSetFromCache(string cacheKey, string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values, CacheItemRemovedCallback removedCallback);
        #endregion

        #region ExecuteTable and SpExecuteTable
        DataTable ExecuteTableFromCache(string cacheKey, string executeSql);

        DataTable ExecuteTableFromCache(string cacheKey, string executeSql, bool transaction);

        DataTable ExecuteTableFromCache(string cacheKey, string executeSql, string[] parameters, object[] values);

        DataTable ExecuteTableFromCache(string cacheKey, string executeSql, bool transaction, string[] parameters, object[] values);

        DataTable SpExecuteTableFromCache(string cacheKey, string spName);

        DataTable SpExecuteTableFromCache(string cacheKey, string spName, bool transaction);

        DataTable SpExecuteTableFromCache(string cacheKey, string spName, string[] parameters, object[] values);

        DataTable SpExecuteTableFromCache(string cacheKey, string spName, bool transaction, string[] parameters, object[] values);

        DataTable ExecuteTableFromCache(string cacheKey, string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values, CacheItemRemovedCallback removedCallback);
        #endregion

        #endregion
    }
}
