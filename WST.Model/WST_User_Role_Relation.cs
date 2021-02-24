using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    //用户角色关系表
    public class WST_User_Role_Relation
    {
        public int User_Id { get; set; }    //--用户外键
        public int Role_Id { get; set; }    //--角色外键

    }
}
