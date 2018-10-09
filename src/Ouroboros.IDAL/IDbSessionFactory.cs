using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.IDAL
{
    /// <summary>
    /// IDbSessionFactory接口，IDbSession接口的抽象工厂
    /// 在BaseDao中会用到IDbSession的实例，我们借助"抽象工厂"生产IDbSession的实例。
    /// </summary>
    public interface IDbSessionFactory
    {
        /// <summary>
        /// 获取当前DbSession
        /// </summary>
        /// <returns></returns>
        IDbSession GetCurrentDbSession();
    }
}
