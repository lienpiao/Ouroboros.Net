using Ouroboros.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace Ouroboros.DAL
{
    public class DbSession : IDbSession
    {
        #region 获取所有的Dao      
        private ISysUserDao _SysUserDao;
        public ISysUserDao SysUserDao
        {
            get
            {
                if (_SysUserDao == null)
                {
                    _SysUserDao = new SysUserDao();
                }
                return _SysUserDao;
            }

            set
            {
                _SysUserDao = value;
            }
        }

        private ISysRoleDao _SysRoleDao;
        public ISysRoleDao SysRoleDao
        {
            get
            {
                if (_SysRoleDao == null)
                {
                    _SysRoleDao = new SysRoleDao();
                }
                return _SysRoleDao;
            }

            set
            {
                _SysRoleDao = value;
            }
        }

        private ISysActionDao _SysActionDao;
        public ISysActionDao SysActionDao
        {
            get
            {
                if (_SysActionDao == null)
                {
                    _SysActionDao = new SysActionDao();
                }
                return _SysActionDao;
            }

            set
            {
                _SysActionDao = value;
            }
        }

        private ISysUserActionDao _SysUserActionDao;
        public ISysUserActionDao SysUserActionDao
        {
            get
            {
                if (_SysUserActionDao == null)
                {
                    _SysUserActionDao = new SysUserActionDao();
                }
                return _SysUserActionDao;
            }

            set
            {
                _SysUserActionDao = value;
            }
        }
        #endregion

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="paras">要应用于命令字符串的参数</param>
        /// <returns>执行命令后由数据库返回的结果</returns>
        public int ExeucteSql(string sql, params SqlParameter[] paras)
        {
            DbContext db = DbContextFactory.GetCurrentThreadInstance();
            return db.Database.ExecuteSqlCommand(sql, paras);
        }

        /// <summary>
        /// 保存所有变化
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            DbContext db = DbContextFactory.GetCurrentThreadInstance();
            return db.SaveChanges();
        }
    }
}
