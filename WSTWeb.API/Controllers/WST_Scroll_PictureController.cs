using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WST.DAL;
using WST.Model;
using Microsoft.AspNetCore.Cors;
using System.IO;

namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("any")]
    [ApiController]
    public class WST_Scroll_PictureController : ControllerBase
    {
        WST_Scroll_PictureDal dal = new WST_Scroll_PictureDal();

        //显示数据
        [HttpGet]
        public ObjectResult Detial(string stitle = "", int page = 1, int limit = 5)
        {
            int totalCount;
            List<WST_Scroll_Picture> data = dal.GetScroll_Picture(stitle, page, limit, out totalCount);
            return Ok(new { data = data, code = 0, count = totalCount });
        }

        //绑定下拉框
        [HttpGet]
        [Route("[action]")]
        public ObjectResult GetSelect()
        {
            List<WST_Scroll_Picture> data = dal.GetSelect();
            return Ok(new { data = data });
        }

        //添加
        [HttpPost]
        public int AddWST_Scroll_Picture(WST_Scroll_Picture s)
        {
            return dal.AddWST_Scroll_Picture(s);
        }

       

        //删除
        [HttpDelete]
        public int DelWST_Scroll_Picture([FromBody]int Did)
        {
            return dal.DelWST_Scroll_Picture(Did);
        }

        //修改
        [HttpPut]
        public int UpdWST_Scroll_Picture(WST_Scroll_Picture s)
        {
            return dal.UpdWST_Scroll_Picture(s);
        }
    }
}
