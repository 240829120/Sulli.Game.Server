using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sulli.Game.Server;

namespace Sulli.Game.Mock
{
    /// <summary>
    /// 学生控制器
    /// </summary>
    [Controller]
    [Route("Student")]
    public class StudentController : ControllerBase
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [WebApi]
        [WebSocket]
        [Route("Query")]
        public Student Query(string name)
        {
            Student student = new Student();
            student.ID = 1;
            student.Name = "zhangsan";
            student.Age = 17;

            return student;
        }
    }
}
