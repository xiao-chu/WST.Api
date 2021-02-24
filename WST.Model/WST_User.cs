using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    //用户表
    public class WST_User
    {
        public int User_Id          { get; set; }           //--ID 
        public string User_Color       { get; set; }           //--主题颜色
        public string User_Name		 { get; set; }          //--用户名
        public string User_Nickname	 { get; set; }          //--昵称
        public string User_Password	 { get; set; }          //--用户密码
        public string User_Email		 { get; set; }          //--邮箱
        public string User_Phone		 { get; set; }          //--手机号
        public string User_Remark		 { get; set; }          //--备注
        public DateTime User_CreateTime	 { get; set; }          //--创建时间
        public string User_Realname	 { get; set; }          //--真实姓名
        public int Department_Id { get; set; }              //--部门ID

    }
}
