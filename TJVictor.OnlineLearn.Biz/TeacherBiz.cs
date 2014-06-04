using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TJVictor.OnlineLearn.Biz.Entity;

namespace TJVictor.OnlineLearn.Biz
{
    public class TeacherBiz : CommonBiz
    {
        public TeacherBiz() : base() { }

        public List<TeacherShow> GetAllTeacherShow()
        {
            return sqlHelper.GetClassItemList<TeacherShow>(@"
                With c as 
                (
                select a.ID, Avg(b.T1) as T1, Avg(b.T2) as T2, Avg(b.T3) as T3, Avg(b.T4) as T4, Avg(b.T5) as T5 
                from dbo.tblTeacher as a
                join dbo.tblComment as b on a.ID = b.T_ID
                group by a.ID
                )
                select a.*,c.T1, c.T2, c.T3, c.T4, c.T5 from dbo.tblTeacher as a
                join c on a.ID = c.ID;", null, null);
        }
    }
}
