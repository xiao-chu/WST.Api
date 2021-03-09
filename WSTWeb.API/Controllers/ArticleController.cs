using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
//引入命名空间
using Microsoft.AspNetCore.Cors;
using WST.DAL;
using WST.Model;
using System.IO;
using WSTWeb.API.Models;

namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("any")]//跨域
    [ApiController]
    public class ArticleController : ControllerBase
    {
        //实例化DAL
        ArticleDal dal = new ArticleDal();
        //分页显示
        [HttpGet]
        public ObjectResult Index(int page = 1, int limit = 5, string Dtitle = "", string Aname = "", int Audit_status = -1)
        {
            int totalCount;
            var data = dal.GetArticle(page, limit, out totalCount, Dtitle, Aname, Audit_status);
            return Ok(new { data = data, code = 0, count = totalCount });
        }

        //查询栏目表
        [Route("[action]")]
        [HttpGet]
        public ObjectResult GetDetialIndex()
        {
            var data = dal.GetDetial();
            return Ok(new { data = data, code = 0 });
        }

        //新增
        [HttpPost]
        public ObjectResult Create(WST_Article_management w)
        {
            if (ModelState.IsValid)
            {
                w.Tops = "置顶";      //是否置顶
                w.Audit_status = 1;   //审核状态
                w.Release_time = DateTime.Now;  //审核时间
                w.Comment = 1;         //是否评论
                w.AType = "正常";
                var data = dal.AddArticle(w);
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
            if (dal.DeleteArticle(id) > 0)
            {
                return Ok(new { code = 1, msg = "删除成功" });
            }
            return Ok(new { code = -1, msg = "删除失败" });
        }

        //修改
        [HttpPut]
        public ObjectResult Update(WST_Article_management w)
        {
            var data = dal.UpdateArticle(w);
            if (data > 0)
            {
                return Ok(new { code = 1, msg = "修改成功" });
            }
            return Ok(new { code = -1, msg = "修改失败" });
        }
    }
}
