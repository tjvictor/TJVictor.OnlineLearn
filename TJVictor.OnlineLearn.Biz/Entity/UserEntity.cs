using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJVictor.OnlineLearn.Biz.Entity
{
    public class UserEntity
    {
        public string ID { get; set; }
        public string Pwd { get; set; }
        public string UserName { get; set; }
        public byte[] Avatar { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string QQ { get; set; }
        public string Weixin { get; set; }
        public string Skype { get; set; }
        public string FaceTime { get; set; }
    }
}
