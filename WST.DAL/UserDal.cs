using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//引用
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WST.Model;
namespace WST.DAL
{
    public class UserDal
    {
        //获取配置中的连接字符串
        private string strConn = "Data Source=.;Initial Catalog = WSTDb;Integrated Security = True";

        //登录
        public WST_User Lgoin(string name, string pass)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {

                //md5加密
                var newPass = Md5Helper.Get_MD5(pass, "utf-8");
                //sql语句
                string sql = $"select * from WST_User where User_Name=@name and User_Password=@pass";
                //参数化对象
                DynamicParameters para = new DynamicParameters();
                para.Add("@name", name, DbType.String);
                para.Add("@pass", newPass, DbType.String);

                WST_User user = conn.QueryFirstOrDefault<WST_User>(sql, para);
                return user;
            }
        }
        //分页显示
        public List<WST_User> GetUsers(string user_Name, string user_Realname, string role_Name, int department_Id, int pageIndex, int pageSize, out int totalCount)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@User_Name", user_Name, DbType.String);
                para.Add("@User_Realname", user_Realname, DbType.String);
                para.Add("@Role_Name", role_Name, DbType.String);
                para.Add("@Department_Id", department_Id, DbType.Int32);
                para.Add("@pageIndex", pageIndex, DbType.Int32);
                para.Add("@pageSize", pageSize, DbType.Int32);
                para.Add("@totalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var list = conn.Query<WST_User>("p_userpage", para, commandType: CommandType.StoredProcedure).ToList();
                totalCount = para.Get<int>("@totalCount");
                return list;
            }
        }

        //修改用户信息
        public int UpdUser(WST_User u)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                //md5加密
                var newPass = Md5Helper.Get_MD5(u.User_Password, "utf-8");
                string sql = $"update WST_User set User_Color=@User_Color,User_Nickname=@User_Nickname,User_Password=@User_Password,User_Email=@User_Email,User_Phone=@User_Phone,User_Remark=@User_Remark where User_Id=@User_Id";
                DynamicParameters para = new DynamicParameters();
                para.Add("@User_Color", u.User_Color, DbType.String);
                para.Add("@User_Nickname", u.User_Nickname, DbType.String);
                para.Add("@User_Password", newPass, DbType.String);
                para.Add("@User_Email", u.User_Email, DbType.String);
                para.Add("@User_Phone", u.User_Phone, DbType.String);
                para.Add("@User_Remark", u.User_Remark, DbType.String);
                para.Add("@User_Id", u.User_Id, DbType.Int32);
                return conn.Execute(sql, para);
            }
        }
        //添加权限（赋予角色）
        public int AddRole(WST_User_Role_Relation r)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                var sql = "update WST_User_Role_Relation set Role_Id=@Role_Id where User_Id=@User_Id";
                DynamicParameters para = new DynamicParameters();
                para.Add("@Role_Id", r.Role_Id, DbType.Int32);
                para.Add("@User_Id", r.User_Id, DbType.Int32);
                return conn.Execute(sql, para);
            }
        }
        //添加信息
        public int AddUser(WST_User u)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                //md5加密
                var newPass = Md5Helper.Get_MD5(u.User_Password, "utf-8");
                DynamicParameters para = new DynamicParameters();
                para.Add("@User_Color", u.User_Color, DbType.String);
                para.Add("@User_Name", u.User_Name, DbType.String);
                para.Add("@User_Nickname", u.User_Nickname, DbType.String);
                para.Add("@User_Password", newPass, DbType.String);
                para.Add("@User_Email", u.User_Email, DbType.String);
                para.Add("@User_Phone", u.User_Phone, DbType.String);
                para.Add("@User_Remark", u.User_Remark, DbType.String);
                para.Add("@User_Realname", u.User_Realname, DbType.String);
                para.Add("@Department_Id", u.Department_Id, DbType.Int32);
                para.Add("@return_Value",dbType:DbType.Int32,direction: ParameterDirection.Output);
                var result= conn.Execute("p_addUser", para,commandType: CommandType.StoredProcedure);
                int value = para.Get<int>("@return_Value");
                return value;
            }
        }

        //删除用户
        public int DelUser(int id)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                //sql语句
                string sql = "delete from WST_User where User_Id=@User_Id";
                //参数化对象
                DynamicParameters para = new DynamicParameters();
                para.Add("@User_Id", id, DbType.Int32);

                return conn.Execute(sql, para);
            }
        }
        //判断重名
        public int IsUser(string name)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                //sql语句
                string sql = "select count(*) from WST_User where User_Name=@name";
                DynamicParameters para = new DynamicParameters();
                para.Add("@name", name, DbType.String);
                return conn.ExecuteScalar<int>(sql, para);
            }
        }
        //查询权限
        public List<WST_Jurisdiction> GetJurisdictions(int id)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                //sql语句
                string sql = $"select *from WST_Jurisdiction where Jid in(" +
                    $"select Juri_id from WST_Role_Jurisdiction where Role_id = (" +
                    $"select Role_Id from WST_User_Role_Relation where User_Id = @id))";
                DynamicParameters para = new DynamicParameters();
                para.Add("@id", id, DbType.Int32);
                var jur = conn.Query<WST_Jurisdiction>(sql, para).ToList();
                return jur;
            }
        }
        //绑定部门下拉框
        public List<WST_Department> GetDepartments()
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                //sql语句
                string sql = $"select * from WST_Department ";

                var department = conn.Query<WST_Department>(sql).ToList();
                return department;
            }
        }
        //绑定类型下拉框
        public List<WST_Role> GetRoles()
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                //sql语句
                string sql = $"select * from WST_Role ";

                var role = conn.Query<WST_Role>(sql).ToList();
                return role;
            }
        }
    }
}
