using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//引入Dapper
using Dapper;
using WST.Model;
using System.Data;
using System.Data.SqlClient;


namespace WST.DAL
{
    public class LinkListDal
    {
        private string strConn = "Data Source=.;Initial Catalog=WSTDb;Integrated Security=True";//ConfigurationManager.ConnectionStrings["Conn"].ToString();
        //链表显示
        public List<WST_LinkList> GetLink(int pageIndex, int pageSize, out int totalCount, string LName)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageIndex", pageIndex, dbType: DbType.Int32);
                dynamic.Add("@pageSize", pageSize, dbType: DbType.Int32);
                dynamic.Add("@totalCount", direction: ParameterDirection.Output, dbType: DbType.Int32);
                dynamic.Add("@LName", LName, dbType: DbType.String);


                //获取结果集
                List<WST_LinkList> result = conn.Query<WST_LinkList>("p_Linkpage", dynamic, commandType: CommandType.StoredProcedure).ToList();
                //输出参数
                totalCount = dynamic.Get<int>("@totalCount");
                //返回结果
                return result;
            }
        }
        //新增链表
        public int AddLink(WST_LinkList w)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string strSql = $"insert into WST_LinkList values('{w.LName}','{w.URL}','{w.LType}',{w.Lsort},'{w.Show}','{w.Sites}','{w.Remark}')";
                return conn.Execute(strSql);
            }
        }

        //删除链表
        public int DeleteLink(int id)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string strSql = $"delete from WST_LinkList where LId = {id}";
                return conn.Execute(strSql);
            }
        }

        //反填
        //public WST_Article_management GetArticleById(int id)
        //{
        //    using (IDbConnection conn = new SqlConnection(strConn))
        //    {
        //        string strSql = $"select * from WST_Article_management where Aid={id}";
        //        return conn.Query<WST_Article_management>(strSql).SingleOrDefault();
        //    }
        //}
        //修改链表
        public int UpdateLink(WST_LinkList w)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string strSql = $"update WST_LinkList set LName='{w.LName}',URL='{w.URL}',LType='{w.LType}',Lsort='{w.Lsort}',Show='{w.Show}',Sites='{w.Sites}',Remark='{w.Remark}' where LId={w.LId}";
                return conn.Execute(strSql);
            }
        }
    }
}
