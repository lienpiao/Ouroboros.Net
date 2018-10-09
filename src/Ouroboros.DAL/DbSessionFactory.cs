using Ouroboros.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.DAL
{
    /// <summary>
    /// 实现IDbSessionFactory接口，生产线程内唯一数据层访问入口实例
    /// </summary>
    public class DbSessionFactory : IDbSessionFactory
    {
        /// <summary>
        /// 获取当前DbSession
        /// </summary>
        /// <returns></returns>
        public IDbSession GetCurrentDbSession()
        {
            IDbSession dbSession = CallContext.GetData(typeof(DbSession).FullName) as IDbSession;
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData(typeof(DbSession).FullName, dbSession);
            }
            return dbSession;
        }
    }
}
