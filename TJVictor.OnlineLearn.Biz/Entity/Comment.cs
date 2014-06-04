using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJVictor.OnlineLearn.Biz.Entity
{
    public class Comment
    {
        public Guid ID { get; set; }
        public Guid C_ID { get; set; }
        public Guid T_ID { get; set; }
        public Int16 C1 { get; set; }
        public Int16 C2 { get; set; }
        public Int16 C3 { get; set; }
        public Int16 C4 { get; set; }
        public Int16 C5 { get; set; }
        public Int16 T1 { get; set; }
        public Int16 T2 { get; set; }
        public Int16 T3 { get; set; }
        public Int16 T4 { get; set; }
        public Int16 T5 { get; set; }
        public string C_Des { get; set; }
        public string T_Des { get; set; }
        public DateTime CreateTime { get; set; }
        public string TaobaoTradeNo { get; set; }
    }
}
