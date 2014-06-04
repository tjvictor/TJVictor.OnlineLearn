using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJVictor.OnlineLearn.Biz.Entity
{
    public class CommentShow : Comment
    {
        public string C_Name { get; set; }

        public string T_FirstName { get; set; }

        public string T_LastName { get; set; }

        public string U_ID { get; set; }

        public byte[] U_Avatar { get; set; }

        public string UserName { get; set; }

        public string T_Name { get { return string.Format("{0} {1}", T_LastName, T_FirstName); } }
    }
}
