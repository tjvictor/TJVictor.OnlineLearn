using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJVictor.OnlineLearn.Biz.Entity
{
    public class TeacherExtend : Teacher
    {
        public List<Course> CourseList { get; set; }

        public List<Comment> CommentList { get; set; }
    }
}
