using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.Model
{
    // 栏目滚动图片表
    public class WST_Scroll_Picture
    {
        public int Sid { get; set; }               // 滚动图片主键
        public string Sprograma { get; set; }      // 栏目
        public string Stitle { get; set; }         // 图片标题
        public string Scontent { get; set; }       // 图片内容
        public int Sstate { get; set; }           // 图片状态
        public string Simg { get; set; }           // 图片
        public string Slink { get; set; }          // 图片链接
        public DateTime Stime { get; set; }        // 图片更新的时间
    }
}
