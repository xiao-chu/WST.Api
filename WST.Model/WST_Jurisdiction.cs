using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    //权限表
    public class WST_Jurisdiction
    {
        public int Jid      { get; set; }         //--权限ID
        public string  Jurl     { get; set; }     //--可访问见面路由
        public string  Jcontent { get; set; }     //--描述
    }
}
