using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TJVictor.OnlineLearn.Biz.Entity;

namespace TJVictor.OnlineLearn.Biz
{
    public class CourseBiz: CommonBiz
    {
        public CourseBiz() : base() { }

        public List<CourseShow> GetAllCourseShow()
        {
            return sqlHelper.GetClassItemList<CourseShow>(@"
                With c as 
                (
                select a.ID, Avg(b.C1) as C1, Avg(b.C2) as C2, Avg(b.C3) as C3, Avg(b.C4) as C4, Avg(b.C5) as C5 
                from dbo.tblCourse as a
                join dbo.tblComment as b on a.ID = b.C_ID
                group by a.ID
                )
                select a.*,c.C1, c.C2, c.C3, c.C4, c.C5 from dbo.tblCourse as a
                join c on a.ID = c.ID;", null, null);
        }
    }
}
