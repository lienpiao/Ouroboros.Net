using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.DAL
{
    /// <summary>
    /// 数据访问层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDao<T> : IDisposable where T : class, new()
    {
        /// <summary>
        /// 当前EF上下文的唯一实例
        /// </summary>
        private DbContext Db
        {
            get { return DbContextFactory.GetCurrentThreadInstance(); }
        }

        #region Create

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual T Insert(T entity)
        {
            Db.Set<T>().Add(entity);
            return entity;
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual T Update(T entity)
        {
            if (entity != null)
            {
                Db.Set<T>().Attach(entity);
                Db.Entry(entity).State = EntityState.Modified;
            }

            return entity;
        }

        #endregion

        #region Retrieve

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="whereLambda">Lambda表达式</param>
        /// <returns></returns>
        public virtual T GetModel(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().SingleOrDefault(whereLambda);
        }
        /// <summary>
        /// 通过主键得到一个对象实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public virtual T GetModel(object keyValue)
        {
            return Db.Set<T>().Find(keyValue);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="whereLambda">Lambda表达式</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where(whereLambda);
        }
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
        public virtual IQueryable<T> GetList<S>(int pageSize, int pageIndex, out int rowCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            rowCount = Db.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = Db.Set<T>().Where(whereLambda).OrderBy<T, S>(orderByLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = Db.Set<T>().Where(whereLambda).OrderByDescending<T, S>(orderByLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                return temp;
            }
        }
        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        public virtual long Count(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where(whereLambda).Count();
        }
        #endregion

        #region Delete
        /// <summary>
        /// 通过实体对象删除一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual int Delete(T entity)
        {
            Db.Set<T>().Attach(entity);
            Db.Entry(entity).State = EntityState.Deleted;
            return -1;

        }
        /// <summary>
        /// 通过主键删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public virtual int Delete(object id)
        {
            T entity = Db.Set<T>().Find(id);
            Db.Entry(entity).State = EntityState.Deleted;
            return -1;
        }
        /// <summary>
        ///  通过实体对象逻辑删除一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual int DeleteByLogical(T entity)
        {
            Db.Entry(entity).Property("IsDeleted").CurrentValue = true;
            Db.Entry(entity).Property("IsDeleted").IsModified = true;
            return -1;
        }
        /// <summary>
        /// 通过实体对象删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        public virtual int DeleteByLogical(object id)
        {
            T entity = Db.Set<T>().Find(id);
            Db.Entry(entity).Property("IsDeleted").CurrentValue = true;
            Db.Entry(entity).Property("IsDeleted").IsModified = true;
            return -1;
        }
        #endregion

        /// <summary>
        /// 释放EF上下文
        /// 虽然DbContext有默认的垃圾回收机制，但通过BaseDao实现IDisposable接口，可以在不用EF上下文的时候手动回收，时效性更强。
        /// </summary>
        public void Dispose()
        {
            Db.Dispose();
        }

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
