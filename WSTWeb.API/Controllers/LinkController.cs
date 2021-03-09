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

namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]//跨域
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
        public ObjectResult Create(WST_LinkList w, IFormFile formfile)
        {
            if (ModelState.IsValid)
            {
                UploadImg(formfile);
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

            var path = Directory.GetDirectories(filePath).ToString();//文件夹路径
            if (!Directory.Exists(path))//查询目录是否存在
            {
                Directory.CreateDirectory(path);//创建目录
            }
            //保存文件
            using (var stream = new FileStream(path+newFileName, FileMode.Create))
            {
                formfile.CopyToAsync(stream);
                stream.Flush();
            }
            //完整的文件路径
            var completeFilePath = Path.Combine(path, newFileName);

            return Ok("上传成功");
        }
    }
}
