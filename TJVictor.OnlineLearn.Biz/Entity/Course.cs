using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJVictor.OnlineLearn.Biz.Entity
{
    public class Course
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        public string Des { get; set; }
    }
}
