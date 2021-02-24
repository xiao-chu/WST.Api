using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    //角色权限关系表
    public class WST_Role_Jurisdiction
    {
        public int Role_id { get; set; }   //--角色外键
        public int Juri_id { get; set; }   //--权限外键
    }
}
