using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TJVictor.DataBase.SqlHelper;

namespace TJVictor.OnlineLearn.Biz
{
    public class CommonBiz
    {
        protected ISQLHelper sqlHelper;

        public CommonBiz()
        {
            sqlHelper = SQLHelper.SQLHelperFactory();
        }
    }
}
