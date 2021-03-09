using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WST.Model;

namespace WST.DAL
{
    public class WST_Detial_ProDal
    {
        //连接字符串
        private string strConn = "Data Source=.;Initial Catalog = WSTDb;Integrated Security = True";

        #region 分页获取数据
        public List<WST_Detial_Pro> GetDetial_Pro(string dtitle, string dptitle, int pageIndex, int pageSize, out int totalCount)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                var p = new DynamicParameters();
                p.Add("@PageIndex", pageIndex, DbType.Int32);
                p.Add("@PageSize", pageSize, DbType.Int32);
                p.Add("@dtitle", dtitle, DbType.String);
                p.Add("@dptitle", dptitle, DbType.String);
                p.Add("@totalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                List<WST_Detial_Pro> list = conn.Query<WST_Detial_Pro>("p_WST_Detial_Pro", p, commandType: CommandType.StoredProcedure).ToList();

                totalCount = p.Get<int>("@totalCount");

                return list;
            }
        }
        #endregion

        #region 修改

        //修改
        public int UpdWST_Detial_Pro(WST_Detial_Pro w)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"update WST_Detial_Pro set Dtitle=@Dtitle,Dptitle=@Dptitle,UrlKey=@UrlKey,Dsort=@Dsort,Dstate=@Dstate,Dtype=@Dtype,Daddress=@Daddress,Dexplain=@Dexplain where Did=@Did";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Did", w.Did, DbType.Int32);
                paras.Add("@Dtitle", w.Dtitle, DbType.String);
                paras.Add("@Dptitle", w.Dptitle, DbType.String);
                paras.Add("@UrlKey", w.UrlKey, DbType.String);
                paras.Add("@Dsort", w.Dsort, DbType.Int32);
                paras.Add("@Dstate", w.Dstate, DbType.Int32);
                paras.Add("@Dtype", w.Dtype, DbType.String);
                paras.Add("@Daddress", w.Daddress, DbType.String);
                paras.Add("@Dexplain", w.Dexplain, DbType.String);
                return conn.Execute(sql, paras);
            }
        }
        #endregion

        #region 删除
        public int DelWST_Detial_Pro(int Did)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"delete from WST_Detial_Pro where Did=@Did";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Did", Did, DbType.Int32);
                return conn.Execute(sql,paras);
            }
        }
        #endregion

        #region 新增
        public int AddWST_Detial_Pro(WST_Detial_Pro w)
        {
            using (IDbConnection conn = new SqlConnection(strConn))
            {
                string sql = $"insert into WST_Detial_Pro values (@Dtitle,@Dptitle,@UrlKey,@Dsort,@Dstate,@Dtype,@Daddress,@Dexplain)";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@Dtitle", w.Dtitle, DbType.String);
                paras.Add("@Dptitle", w.Dptitle, DbType.String);
                paras.Add("@UrlKey", w.UrlKey, DbType.String);
                paras.Add("@Dsort", w.Dsort, DbType.Int32);
                paras.Add("@Dstate", w.Dstate, DbType.Int32);
                paras.Add("@Dtype", w.Dtype, DbType.String);
                paras.Add("@Daddress", w.Daddress, DbType.String);
                paras.Add("@Dexplain", w.Dexplain, DbType.String);
                return conn.Execute(sql, paras);
            }
        }
        #endregion

    }
}
