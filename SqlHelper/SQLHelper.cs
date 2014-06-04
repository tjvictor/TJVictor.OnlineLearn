using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TJVictor.DataBase.SqlHelper
{
    public class SQLHelper
    {
        public static ISQLHelper SQLHelperFactory()
        {
            return SQLHelperFactory(SQLTypeEnum.UnKnow, null);
        }

        public static ISQLHelper SQLHelperFactory(SQLTypeEnum sqlType, string conStr)
        {
            if (sqlType == SQLTypeEnum.UnKnow)
            {
                string connectionString =
                    ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

                sqlType = (SQLTypeEnum)Enum.Parse(typeof(SQLTypeEnum), 
                    connectionString.Split(':')[0]);
                conStr = connectionString.Split(':')[1];
            }

            switch (sqlType)
            {
                case SQLTypeEnum.SQLServer:
                    return SQLServer.SQLServerHelper.GetInstace(conStr);
                case SQLTypeEnum.MySQL:
                    return MySQL.MySQLHelper.GetInstace(conStr);
                case SQLTypeEnum.SQLite:
                    return SQLite.SQLiteHelper.GetInstace(conStr);
                case SQLTypeEnum.Access:
                    return Access.AccessHelper.GetInstace(conStr);
                case SQLTypeEnum.Oracle:
                    return Oracle.OracleHelper.GetInstace(conStr);
                default: return null;
            }

        }

        public static ISQLCacheHelper SQLCacheHelperFactory()
        {
            return SQLCacheHelperFactory(SQLTypeEnum.UnKnow, null);
        }

        public static ISQLCacheHelper SQLCacheHelperFactory(SQLTypeEnum sqlType, string conStr)
        {
            if (sqlType == SQLTypeEnum.UnKnow)
            {
                string connectionString =
                    ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

                sqlType = (SQLTypeEnum)Enum.Parse(typeof(SQLTypeEnum),
                    connectionString.Split(':')[0]);
                conStr = connectionString.Split(':')[1];
            }

            switch (sqlType)
            {
                case SQLTypeEnum.SQLServer:
                    return SQLServer.SQLServerHelper.GetCacheInstace(conStr);
                case SQLTypeEnum.MySQL:
                    return MySQL.MySQLHelper.GetCacheInstace(conStr);
                case SQLTypeEnum.SQLite:
                    return SQLite.SQLiteHelper.GetCacheInstace(conStr);
                case SQLTypeEnum.Access:
                    return Access.AccessHelper.GetCacheInstace(conStr);
                case SQLTypeEnum.Oracle:
                    return Oracle.OracleHelper.GetCacheInstace(conStr);
                default: return null;
            }

        }
    }
}
