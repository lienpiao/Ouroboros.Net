using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ouroboros.IDAL
{
    /// <summary>
    /// 数据访问层接口基类
    /// </summary>
    /// <typeparam name="T">实体对象</typeparam>
    public interface IBaseDao<T> where T : class, new()
    {
        #region Create

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Insert(T entity);

        #endregion

        #region Update

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Update(T entity);

        #endregion

        #region Retrieve

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="whereLambda">Lambda表达式</param>
        /// <returns></returns>
        T GetModel(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 通过主键得到一个对象实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        T GetModel(object keyValue);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="whereLambda">Lambda表达式</param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        /// <typeparam name="S">表示从T中获取的属性类型</typeparam>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="rowCount">总条数</param>
        /// <param name="whereLambda">Lambda表达式</param>
        /// <param name="orderByLambda">Lambda排序</param>
        /// <param name="isAsc">true 升序 false 降序</param>
        /// <returns></returns>
        IQueryable<T> GetList<S>(int pageSize, int pageIndex, out int rowCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc);
        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        long Count(Expression<Func<T, bool>> whereLambda = null);
        #endregion

        #region Delete
        /// <summary>
        /// 通过实体对象删除一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(T entity);
        /// <summary>
        /// 通过主键删除一条数据
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        ///  通过实体对象逻辑删除一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void DeleteByLogical(T entity);
        /// <summary>
        /// 通过实体对象删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        void DeleteByLogical(object id);
        #endregion
    }
}
