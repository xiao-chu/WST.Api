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
        private  string strConn = "Data Source=.;Initial Catalog = WSTDb;Integrated Security = True";

        //登录
        public  WST_User Lgoin(string name,string pass)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
                //sql语句
                string sql = $"select * from WST_User where User_Name='{name}' and User_Password='{pass}'";

                WST_User user= conn.QueryFirstOrDefault<WST_User>(sql);
                return user;
            }
        }
        //分页显示
        public  List<WST_User> GetUsers(string user_Name,string user_Realname,string role_Name,int department_Id,int pageIndex,int pageSize,out int totalCount)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@User_Name", user_Name,DbType.String);
                para.Add("@User_Realname",user_Realname, DbType.String);
                para.Add("@Role_Name",role_Name, DbType.String);
                para.Add("@Department_Id",department_Id, DbType.Int32);
                para.Add("@pageIndex",pageIndex, DbType.Int32);
                para.Add("@pageSize",pageSize, DbType.Int32);
                para.Add("@totalCount",dbType: DbType.Int32,direction: ParameterDirection.Output);

                var list = conn.Query<WST_User>("p_userpage", para, commandType: CommandType.StoredProcedure).ToList();
                totalCount = para.Get<int>("@totalCount");
                return list;
            }
        }

        //修改用户信息
        public  int UpdUser(WST_User u)
        {
            using (IDbConnection conn=new SqlConnection(strConn))
            {
                string sql = $"update WST_User set User_Color='{u.User_Color}',User_Nickname='{u.User_Nickname}',User_Password='{u.User_Password}',User_Email='{u.User_Email}',User_Phone='{u.User_Phone}',User_Remark='{u.User_Remark}' where User_Id={u.User_Id}";
                return conn.Execute(sql);
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
