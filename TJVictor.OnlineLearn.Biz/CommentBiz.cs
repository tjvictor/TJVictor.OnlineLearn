using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TJVictor.OnlineLearn.Biz.Entity;

namespace TJVictor.OnlineLearn.Biz
{
    public class CommentBiz: CommonBiz
    {
        public CommentBiz() : base() { }

        public List<CommentShow> GetAllCommentShow()
        {
            return sqlHelper.GetClassItemList<CommentShow>(@"
                select a.*, b.Avatar as U_Avatar, b.ID as U_ID, b.UserName as UserName, 
                    c.FirstName as T_FirstName, c.LastName as T_LastName,
                    d.Name as C_Name
                    from dbo.tblComment as a
                    join dbo.tblUser as b on a.U_ID = b.ID
                    join dbo.tblTeacher as c on a.T_ID=c.ID
                    join dbo.tblCourse as d on a.C_ID=d.ID order by a.CreateTime desc;", null, null);
        }
    }
}
