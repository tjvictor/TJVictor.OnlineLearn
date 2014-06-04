using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TJVictor.OnlineLearn.Biz.Entity;
using System.Data;
using System.IO;

namespace TJVictor.OnlineLearn.Biz
{
    public class UserBiz : CommonBiz
    {
        public UserBiz() : base() { }

        public UserEntity LoginAuth(string userId, string password)
        {
            UserEntity user = sqlHelper.GetClassItem<UserEntity>("select * from tblUser where ID = @p0 and Pwd = @p1;",
                new string[] { "@p0", "@p1" }, new object[] { userId, password });

            if (string.IsNullOrEmpty(user.ID))
                return null;

            return user;
        }

        public bool IsUserExist(string userId)
        {
            using (IDataReader dr = sqlHelper.ExecuteReader("select 1 from tblUser where ID = @p0;",
                new string[] { "@p0" }, new object[] { userId }))
            {
                if (dr.Read())
                    return true;
            }

            return false;
        }

        public bool IsUserBooked(string u_ID, string s_ID)
        {
            using (IDataReader dr = sqlHelper.ExecuteReader("select 1 from tblUser_Schedule where U_ID = @p0 and S_ID = @p1;",
                new string[] { "@p0", "@p1" }, new object[] { u_ID, s_ID }))
            {
                if (dr.Read())
                    return true;
            }

            return false;
        }

        public void InsertUserEntity(UserEntity ue)
        {
            if (ue.Avatar == null)
            {
                using (FileStream fs = new FileStream(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Img\DefaultUserAvatar.jpg"), FileMode.Open))
                {
                    ue.Avatar = new byte[fs.Length];
                    fs.Read(ue.Avatar, 0, ue.Avatar.Length);
                }
            }
            sqlHelper.ExecuteNonQuery("INSERT INTO [dbo].[tblUser] values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9);",
                new string[] { "@p0", "@p1", "@p2", "@p3", "@p4", "@p5", "@p6", "@p7", "@p8", "@p9" },
                new object[] { ue.ID, ue.Pwd, ue.UserName, ue.Avatar, ue.Email, ue.MobileNo, ue.QQ, ue.Weixin, ue.Skype, ue.FaceTime });
        }
    }
}
