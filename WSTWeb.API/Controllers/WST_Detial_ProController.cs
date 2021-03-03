using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WST.Model;
using WST.DAL;
using Microsoft.AspNetCore.Cors;

namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("any")]
    [ApiController]
    public class WST_Detial_ProController : ControllerBase
    {
        WST_Detial_ProDal dal = new WST_Detial_ProDal();

        //显示数据
        [HttpGet]
        public ObjectResult Detial(string dtitle = "", string dptitle = "", int page = 1, int limit = 3)
        {
            int totalCount;
            List<WST_Detial_Pro> data = dal.GetDetial_Pro(dtitle, dptitle, page, limit, out totalCount);
            return Ok(new { data = data, code = 0, count = totalCount });
        }

        //添加
        [HttpPost]
        public int AddWST_Detial_Pro(WST_Detial_Pro w)
        {
            return dal.AddWST_Detial_Pro(w);
        }

        //删除
        [HttpDelete]
        public int DelWST_Detial_Pro([FromBody] int Did)
        {
            return dal.DelWST_Detial_Pro(Did);
        }

        //修改
        [HttpPut]
        public int UpdWST_Detial_Pro(WST_Detial_Pro w)
        {
            return dal.UpdWST_Detial_Pro(w);
        }
    }
}
