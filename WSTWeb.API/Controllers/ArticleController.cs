using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
//引入命名空间
using Microsoft.AspNetCore.Cors;
using WST.DAL;
using WST.Model;
using System.IO;
using WSTWeb.API.Models;
using Microsoft.AspNetCore.Hosting;

namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("any")]//跨域
    [ApiController]
    public class ArticleController : ControllerBase
    {
        //依赖注入  IHostingEnvironment主机环境
        private readonly IHostingEnvironment hostingEnvironment;
        public ArticleController(IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
        }
        //实例化DAL
        ArticleDal dal = new ArticleDal();
        //分页显示
        [HttpGet]
        public ObjectResult Index(int page = 1, int limit = 2, string Dtitle = "", string Aname = "", int Audit_status = -1)
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
        public ObjectResult Create(WST_Article_management w, IFormFile formfile)
        {
            if (ModelState.IsValid)
            {
                w.Tops = "置顶";      //是否置顶
                w.Audit_status = 1;   //审核状态
                w.Release_time = DateTime.Now;  //审核时间
                w.Comment = 1;         //是否评论
                w.AType = "正常";
                UploadImg(formfile);
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

        //图片上传
        [Route("[action]")]
        [HttpPost]
        public ObjectResult UploadImg(IFormFile formfile)
        {
            //获取文件
            var files = Request.Form.Files;

            //文件是否上传
            if (files.Count == 0)
            {
                return Ok($"图片未上传，请上传文件");
            }
            //获取文件名
            var filePath = formfile.FileName;

            string fileExt = Path.GetExtension(filePath);//获取后缀名
            //随机生成新的文件名
            var newFileName = Guid.NewGuid().ToString() + fileExt;

            //获取主机根路径+Upload路径
            var path = Path.Combine(hostingEnvironment.ContentRootPath, "aa");

            if (!Directory.Exists(path))//查询目录是否存在
            {
                Directory.CreateDirectory(path.ToString());//创建目录
            }

            //完整的文件路径
            var completeFilePath = Path.Combine(path, newFileName);
            //保存文件
            using (var stream = new FileStream(completeFilePath, FileMode.Create))
            {
                formfile.CopyToAsync(stream);
                stream.Flush();
            }
            //
            return Ok("上传成功");
        }
    }
}
