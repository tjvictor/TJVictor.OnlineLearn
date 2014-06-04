using System;
using System.Collections.Generic;
using System.Text;

namespace TJVictor.DataBase.SqlHelper
{
    public class Result<T> where T:struct
    {
        #region Property
        private bool hasValue;
        /// <summary>
        /// 是否有结果集
        /// </summary>
        public bool HasValue
        {
            get { return hasValue; }
            set { hasValue = value; }
        }

        private T? value;
        /// <summary>
        /// 如果有结果集，此为查询结果
        /// </summary>
        public T? Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        #endregion

        #region Construction
        public Result()
        {
        }

        public Result(bool hasValue, T? value)
        {
            this.hasValue = hasValue;
            this.value = value;
        }
        #endregion
    }
}
