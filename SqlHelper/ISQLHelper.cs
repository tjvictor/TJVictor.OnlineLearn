using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.Common;

namespace TJVictor.DataBase.SqlHelper
{
    public interface ISQLHelper
    {
        #region Global Connection
        DbConnection GetGlobalconnection();
        #endregion

        #region GetClassItem
        T GetClassItem<T>(string executeSql, string[] parameters, object[] values) where T : class, new();
        T GetClassItem<T>(string executeSql, string[] parameters, object[] values, string[] columns) where T : class, new();

        T SpGetClassItem<T>(string spName, string[] parameters, object[] values) where T : class, new();
        T SpGetClassItem<T>(string spName, string[] parameters, object[] values, string[] columns) where T : class, new();

        List<T> GetClassItemList<T>(string executeSql, string[] parameters, object[] values) where T : class, new();
        List<T> GetClassItemList<T>(string executeSql, string[] parameters, object[] values, string[] columns) where T : class, new();

        List<T> SpGetClassItemList<T>(string spName, string[] parameters, object[] values) where T : class, new();
        List<T> SpGetClassItemList<T>(string spName, string[] parameters, object[] values, string[] columns) where T : class, new();

        List<T> GetClassItemList<T>(string executeSql, CommandType commandType,
            string[] parameters, object[] values, string[] columns) where T : class, new();
        #endregion

        #region GetStructItem
        Result<T> GetStructItem<T>(string executeSql, string[] parameters, object[] values) where T : struct;

        Result<T> SpGetStructItem<T>(string spName, string[] parameters, object[] values) where T : struct;

        List<T?> GetStructItemList<T>(string executeSql, string[] parameters, object[] values) where T : struct;

        List<T?> SpGetStructItemList<T>(string spName, string[] parameters, object[] values) where T : struct;

        List<T?> GetStructItemList<T>(string executeSql, CommandType commandType,
            string[] parameters, object[] values) where T : struct;
        #endregion

        #region GetStringItem
        List<string> GetStringItemList(string executeSql, CommandType commandType,
            string[] parameters, object[] values);
        #endregion

        #region Common Function
        #region ExecuteNonQuery and SpExecuteNonQuery
        int ExecuteNonQuery(string executeSql);

        int ExecuteNonQuery(string executeSql, bool transaction);

        int ExecuteNonQuery(string executeSql, string[] parameters, object[] values);

        int ExecuteNonQuery(string executeSql, bool transaction, string[] parameters, object[] values);

        int SpExecuteNonQuery(string spName);

        int SpExecuteNonQuery(string spName, bool transaction);

        int SpExecuteNonQuery(string spName, string[] parameters, object[] values);

        int SpExecuteNonQuery(string spName, bool transaction, string[] parameters, object[] values);

        int ExecuteNonQuery(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values);

        int SpExecuteNonQueryWithOutput(string spName, bool transaction, string[] parameters, object[] values,
            string[] outputParameters, object[] outValues);

        int ExecuteNonQuery(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values, string[] outputParameters, object[] outValues);
        #endregion

        #region DataReader and SpExecuteReader
        IDataReader ExecuteReader(string executeSql);

        IDataReader ExecuteReader(string executeSql, string[] parameters, object[] values);

        IDataReader SpExecuteReader(string spName);

        IDataReader SpExecuteReader(string spName, string[] parameters, object[] values);

        IDataReader ExecuteReader(string executeSql, CommandType commandType,
            string[] parameters, object[] values);
        #endregion

        #region ExecuteScalar and SpExecuteScalar
        T ExecuteScalar<T>(string executeSql);

        T ExecuteScalar<T>(string executeSql, bool transaction);

        T ExecuteScalar<T>(string executeSql, string[] parameters, object[] values);

        T ExecuteScalar<T>(string executeSql, bool transaction, string[] parameters, object[] values);

        T SpExecuteScalar<T>(string spName);

        T SpExecuteScalar<T>(string spName, bool transaction);

        T SpExecuteScalar<T>(string spName, string[] parameters, object[] values);

        T SpExecuteScalar<T>(string spName, bool transaction, string[] parameters, object[] values);

        T ExecuteScalar<T>(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values);
        #endregion

        #region ExecuteDataSet and SpExecuteDataSet
        DataSet ExecuteDataSet(string executeSql);

        DataSet ExecuteDataSet(string executeSql, bool transaction);

        DataSet ExecuteDataSet(string executeSql, string[] parameters, object[] values);

        DataSet ExecuteDataSet(string executeSql, bool transaction, string[] parameters, object[] values);

        DataSet SpExecuteDataSet(string spName);

        DataSet SpExecuteDataSet(string spName, bool transaction);

        DataSet SpExecuteDataSet(string spName, string[] parameters, object[] values);

        DataSet SpExecuteDataSet(string spName, bool transaction, string[] parameters, object[] values);

        DataSet ExecuteDataSet(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values);
        #endregion

        #region ExecuteTable and SpExecuteTable
        DataTable ExecuteTable(string executeSql);

        DataTable ExecuteTable(string executeSql, bool transaction);

        DataTable ExecuteTable(string executeSql, string[] parameters, object[] values);

        DataTable ExecuteTable(string executeSql, bool transaction, string[] parameters, object[] values);

        DataTable SpExecuteTable(string spName);

        DataTable SpExecuteTable(string spName, bool transaction);

        DataTable SpExecuteTable(string spName, string[] parameters, object[] values);

        DataTable SpExecuteTable(string spName, bool transaction, string[] parameters, object[] values);

        DataTable ExecuteTable(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values);
        #endregion

        #region ExecuteXmlReader and SpExcuteXmlReader
        XmlReader ExecuteXmlReader(string executeSql);

        XmlReader ExecuteXmlReader(string executeSql, string[] parameters, object[] values);

        XmlReader SpExecuteXmlReader(string spName);

        XmlReader SpExecuteXmlReader(string spName, string[] parameters, object[] values);

        XmlReader ExecuteXmlReader(string executeSql, CommandType commandType,
            string[] parameters, object[] values);
        #endregion

        #region Global
        int GlobalExecuteNonQuery(string executeSql, bool transaction, CommandType commandType,
            string[] parameters, object[] values, string[] outputParameters, object[] outValues);
        #endregion
        #endregion
    }
}
