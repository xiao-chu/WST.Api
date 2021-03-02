using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WST.Model;

namespace WST.DAL
{
    public class WST_Scroll_PictureDal
    {
        //连接字符串
        private string strConn = "Data Source=.;Initial Catalog = WSTDb;Integrated Security = True";

        #region 分页获取数据
        public List<WST_Scroll_Picture> GetScroll_Picture(string stitle, int pageIndex, int pageSize, out int totalCount)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                var p = new DynamicParameters();
                p.Add("@PageIndex", pageIndex, DbType.Int32);
                p.Add("@PageSize", pageSize, DbType.Int32);
                p.Add("@stitle", stitle, DbType.String);
                p.Add("@totalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                List<WST_Scroll_Picture> list = conn.Query<WST_Scroll_Picture>("p_WST_Scroll_Picture", p, commandType: CommandType.StoredProcedure).ToList();

                totalCount = p.Get<int>("@totalCount");

                return list;
            }
        }
        #endregion

        #region 绑定下拉框
        public List<WST_Scroll_Picture> GetSelect()
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"select * from WST_Scroll_Picture";
                return conn.Query<WST_Scroll_Picture>(sql).ToList();
            }
        }
        #endregion

        #region 修改

        //修改
        public int UpdWST_Scroll_Picture(WST_Scroll_Picture s)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"update WST_Scroll_Picture set Sprograma='{s.Sprograma}',Stitle='{s.Stitle}',Scontent='{s.Scontent}',Sstate='{s.Sstate}',Simg='{s.Simg}',Slink='{s.Slink}',Stime='{s.Stime}' where Sid='{s.Sid}'";
                return conn.Execute(sql);
            }
        }
        #endregion

        #region 删除
        public int DelWST_Scroll_Picture(int Sid)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"delete from WST_Scroll_Picture where Sid='{Sid}'";
                return conn.Execute(sql);
            }
        }
        #endregion

        #region 新增
        public int AddWST_Scroll_Picture(WST_Scroll_Picture s)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"insert into WST_Scroll_Picture values ('{s.Sprograma}','{s.Stitle}','{s.Scontent}','{s.Sstate}','{s.Simg}','{s.Slink}','{s.Stime}')";
                return conn.Execute(sql);
            }
        }
        #endregion

    }
}
