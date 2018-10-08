using Ouroboros.IDAL;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using Ouroboros.Model;

namespace Ouroboros.DAL
{
    /// <summary>
    /// 当前EF上下文的抽象工厂
    /// </summary>
    public class DbContextFactory : IDbContextFactory
    {
        /// <summary>
        /// 获取当前EF上下文的唯一实例
        /// </summary>
        /// <returns></returns>
        public DbContext GetCurrentThreadInstance()
        {
            DbContext obj = CallContext.GetData(typeof(DataModelContainer).FullName) as DbContext;
            if (obj == null)
            {
                obj = new DataModelContainer();
                CallContext.SetData(typeof(DataModelContainer).FullName, obj);
            }
            return obj;
        }
    }
}
