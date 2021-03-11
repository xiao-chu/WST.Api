using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WST.Model;
using WST.DAL;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]//跨域
    [Consumes("application/json", "multipart/form-data")]//此处为新增
    public class LinkController : ControllerBase
    {
        //实例化DAL
        LinkListDal dal = new LinkListDal();
        //分页显示
        [HttpGet]
        public ObjectResult Index(int page = 1, int limit = 2, string LName = "")
        {
            int totalCount;
            var data = dal.GetLink(page, limit, out totalCount, LName);
            return Ok(new { data = data, code = 0, count = totalCount });
        }

        //新增
        [HttpPost]
        public ObjectResult Create(WST_LinkList w)
        {
            if (ModelState.IsValid)
            {
                var data = dal.AddLink(w);
                if (data > 0)
                {
                    return Ok(new { code = 1, msg = "添加成功" });
                }

            }
            return Ok(new { code = -1, msg = "添加失败" });
        }

        //删除
        [HttpDelete]
        public ObjectResult Delete([FromBody] int id)
        {
            var data = dal.DeleteLink(id);
            if (data > 0)
            {
                return Ok(new { code = 1, msg = "删除成功" });
            }
            return Ok(new { code = -1, msg = "删除失败" });
        }

        //修改
        [HttpPut]
        public ObjectResult Update(WST_LinkList w)
        {
            if (ModelState.IsValid)
            {
                var data = dal.UpdateLink(w);
                if (data > 0)
                {
                    return Ok(new { code = 1, msg = "修改成功" });
                }
            }
            return Ok(new { code = -1, msg = "修改失败" });
        }
    }
}
