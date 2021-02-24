using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    //文章管理表
    public class WST_Article_management
    {
        public int Aid { get; set; }       //序号
        public string Acolumn { get; set; }    //栏目
        public string Aname { get; set; }    //名称
        public int Ranks { get; set; }       //排序
        public bool Astatus { get; set; }      //状态
        public string Tops { get; set; }    //置顶
        public bool Recommend { get; set; }      //推荐
        public bool Audit_status { get; set; }      //审核状态
        public DateTime Release_time { get; set; }  //发布时间
        public string Promulgator { get; set; }    //发布者
        public bool Comment { get; set; }      //评论
        public DateTime Begin_time { get; set; }  //开始时间
        public DateTime End_time { get; set; }  //结束时间
        public string AType { get; set; }    //类型
        public string Jump_address { get; set; }   //跳转地址
        public string Picture { get; set; }    //图片
        public string Accessory { get; set; }   //附件
    }
}
