using System.Data.SqlClient;

namespace Ouroboros.IDAL
{
    /// <summary>
    /// IDbSession接口，数据库访问层的统一入口
    /// </summary>
    public interface IDbSession
    {
        #region 获取所有的IDao
        ISysUserDao SysUserDao { get; set; }
        ISysRoleDao SysRoleDao { get; set; }
        ISysActionDao SysActionDao { get; set; }
        ISysUserActionDao SysUserActionDao { get; set; }
        #endregion

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        int ExeucteSql(string sql, params SqlParameter[] paras);

        /// <summary>
        /// 保存所有变化
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
