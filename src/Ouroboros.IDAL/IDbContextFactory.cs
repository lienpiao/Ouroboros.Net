using System.Data.Entity;

namespace Ouroboros.IDAL
{
    /// <summary>
    /// 当前EF上下文的抽象工厂接口
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// 获取当前上下文的唯一实例
        /// </summary>
        /// <returns></returns>
        DbContext GetCurrentThreadInstance();
    }
}
