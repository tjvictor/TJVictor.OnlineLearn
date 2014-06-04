using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TJVictor.OnlineLearn.Biz.Entity;

namespace TJVictor.OnlineLearn.Biz
{
    public class ScheduleBiz : CommonBiz
    {
        public ScheduleBiz() : base() { }

        public List<ScheduleLogExtend> GetUserScheduleLog(string userID)
        {
            return sqlHelper.GetClassItemList<ScheduleLogExtend>(@"
                with d as
                (
	                select distinct S_ID from dbo.tblUserScheduleLog where U_ID = @p0
                )
                select a.*, b.Name, c.FirstName, c.LastName from dbo.tblUserScheduleLog as a
                join dbo.tblCourse as b on a.C_ID = b.ID
                join dbo.tblTeacher as c on a.T_ID = c.ID
                join d on a.S_ID = d.S_ID
                order by UpdateTime desc",
                new string[] { "@p0" }, new object[] { userID });
        }

        public List<Schedule> GetAllSchedule()
        {
            return sqlHelper.GetClassItemList<Schedule>(@"
                select a.*, b.Name as C_Name, c.FirstName as T_FirstName,c.LastName as T_LastName 
                from tblSchedule as a
                join dbo.tblCourse as b on a.C_ID = b.ID
                join dbo.tblTeacher as c on a.T_ID = c.ID;", null, null);
        }

        public Schedule GetScheduleByS_ID(string s_ID)
        {
            return sqlHelper.GetClassItem<Schedule>(@"
                select a.*, b.Name as C_Name, c.FirstName as T_FirstName,c.LastName as T_LastName, b.Des as C_Des
                    from tblSchedule as a
                    join dbo.tblCourse as b on a.C_ID = b.ID
                    join dbo.tblTeacher as c on a.T_ID = c.ID
                    where a.ID = @p0;",
                new string[] { "@p0" }, new object[] { s_ID });
        }

        public void InsertSchedule(Schedule s, Guid u_ID, string comment)
        {
            sqlHelper.ExecuteNonQuery(@"
                insert into dbo.tblSchedule values(@p0,@p1,@p2,@p3,@p4,getdate(),1,getdate(),@p5);
                insert into dbo.tblUserScheduleLog
	                select newid(),@p6,a.*,@p7 from tblSchedule as a where a.ID = @p0;",
                new string[] { "@p0", "@p1", "@p2", "@p3", "@p4", "@p5", "@p6", "@p7" },
                new object[] { s.ID, s.C_ID, s.T_ID, s.C_StartTime, s.C_EndTime, s.TaobaoLink, u_ID, comment });
        }

        public void UserBookSchedule(string s_ID, string u_ID)
        {
            sqlHelper.ExecuteNonQuery(@"
                update tblSchedule set Status = 2, UpdateTime = getdate() where ID = @p0;
                
                insert into tblUser_Schedule values( @p1, @p0);

                insert into dbo.tblUserScheduleLog
	                select newid(),@p1, a.*, '' from tblSchedule as a where a.ID = @p0;",
                new string[] { "@p0", "@p1", },
                new object[] { s_ID, u_ID });
        }

        public void UpdateSchedule(Schedule s, string u_ID, string comment)
        {
            sqlHelper.ExecuteNonQuery(@"
                update tblSchedule set C_ID = @p1, T_ID = @p2, C_StartTime = @p3, C_EndTime = @p4, 
                Status = @p5, UpdateTime = getdate(), TaobaoLink = @p6
                where ID = @p0;
                insert into dbo.tblUserScheduleLog
	                select newid(),@p7,a.*,@p8 from tblSchedule as a where a.ID = @p0;",
                new string[] { "@p0", "@p1", "@p2", "@p3", "@p4", "@p5", "@p6", "@p7", "@p8" },
                new object[] { s.ID, s.C_ID, s.T_ID, s.C_StartTime, s.C_EndTime, s.Status, s.TaobaoLink, u_ID, comment });
        }

        public void DeleteSchedule(Schedule s, string u_ID, string comment)
        {
            sqlHelper.ExecuteNonQuery(@"
                insert into dbo.tblUserScheduleLog
	                select newid(),@p1,a.ID, a.C_ID, a.T_ID, a.C_StartTime, a.C_EndTime,
	                a.CreateTime, 0, getdate(), a.TaobaoLink, @p2 from tblSchedule as a where a.ID = @p0;
                delete from tblSchedule where ID = @p0;",
                new string[] { "@p0", "@p1", "@p2" },
                new object[] { s.ID, u_ID, comment });
        }

        public void CancelSchedule(Schedule s, string u_ID, string comment)
        {
            Guid newID = Guid.NewGuid();
            sqlHelper.ExecuteNonQuery(@"
                update tblSchedule set Status = 3, UpdateTime = getdate() where ID = @p0;
                insert into dbo.tblUserScheduleLog
	                select newid(),@p1,a.*, @p2 from tblSchedule as a where a.ID = @p0;
                delete from tblUser_Schedule where U_ID = @p1 and S_ID = @p0;
                insert into dbo.tblSchedule values(@p3,@p4,@p5,@p6,@p7,getdate(),1,getdate(),@p8);
                insert into dbo.tblUserScheduleLog
	                select newid(),@p9, a.*, @p10 from tblSchedule as a where a.ID = @p3",
                new string[] { "@p0", "@p1", "@p2", "@p3", "@p4", "@p5", "@p6", "@p7", "@p8", "@p9", "@p10" },
                new object[] { s.ID, u_ID, comment, 
                    newID ,s.C_ID,s.T_ID,s.C_StartTime,s.C_EndTime,s.TaobaoLink,
                    "", "系统自动生成"});
        }
    }
}
