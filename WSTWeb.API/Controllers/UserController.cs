﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//引用
using WST.DAL;
using WST.Model;
using Microsoft.AspNetCore.Cors;
using System.Net.Http;
using WSTWeb.API.Models;
namespace WSTWeb.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("any")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserDal dal;

        public UserController(UserDal dal_)
        {
            dal = dal_;
        }

        //登录
        [HttpGet]
        public WST_User Login(string name = "", string pass = "")
        {
            var data = dal.Lgoin(name, pass);
            return data;
        }

        //显示分页数据
        [HttpGet]
        [Route("gets")]
        public ObjectResult GetUsers(string user_Name = "", string user_Realname = "", string role_Name = "", int department_Id = 0, int page = 1, int limit = 5)
        {
            int total;
            var data = dal.GetUsers(user_Name, user_Realname, role_Name, department_Id, page, limit, out total);
            return Ok(new { data = data, count = total, code = 0 });
        }

        //修改用户信息
        [HttpPut]
        //[Microsoft.AspNetCore.Mvc.ServiceFilter(typeof(MyAuthorization))]
        public int UpdUser(WST_User  u)
        {
            return dal.UpdUser(u);
        }
        //添加用户信息
        [HttpPost]
        public int AddUser(WST_User u)
        {
            if (dal.IsUser(u.User_Name)>0)
            {
                return -1;
            }
            return dal.AddUser(u);
        }
        //删除用户
        [HttpDelete]
        public int DelUser([FromBody]int id)
        {
            return dal.DelUser(id);
        }
        //添加权限（赋予角色）
        [HttpPut]
        [Route("[action]")]
        public int AddRole(WST_User_Role_Relation r)
        {
            return dal.AddRole(r);
        }
        //绑定部门下拉框
        [HttpGet]
        [Route("[action]")]
        public List<WST_Department> GetDepartments()
        {
            return dal.GetDepartments();
        }
        //绑定类型下拉框
        [HttpGet]
        [Route("[action]")]
        public List<WST_Role> GetRoles()
        {
            return dal.GetRoles();
        }
    }
}
