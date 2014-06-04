using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJVictor.OnlineLearn.Biz.Entity
{
    public class Teacher
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
        public string Des { get; set; }
    }
}
