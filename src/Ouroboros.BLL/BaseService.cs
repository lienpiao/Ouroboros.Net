using Ouroboros.DAL;
using Ouroboros.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.BLL
{
    /// <summary>
    /// 业务逻辑层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IDisposable where T : class, new()
    {
        /// <summary>
        /// 数据层统一访问入口工厂属性
        /// </summary>
        private IDbSessionFactory _DbSessionFactory;

        public IDbSessionFactory DbSessionFactory
        {
            get
            {
                if (_DbSessionFactory == null)
                {
                    _DbSessionFactory = new DbSessionFactory();
                }
                return _DbSessionFactory;
            }
            set { _DbSessionFactory = value; }
        }

        /// <summary>
        /// 数据层统一访问入口属性
        /// </summary>
        private IDbSession _DbSessionContext;

        public IDbSession DbSessionContext
        {
            get
            {
                if (_DbSessionContext == null)
                {
                    _DbSessionContext = DbSessionFactory.GetCurrentDbSession();
                }
                return _DbSessionContext;
            }
            set { _DbSessionContext = value; }
        }

        /// <summary>
        /// 当前Dao,在子类中实现--通过一个抽象方法在构造函数中设置
        /// </summary>
        protected IBaseDao<T> CurrentDao;

        /// <summary>
        /// 借助此方法在子类中的重写，为XXXService设置当前Dao
        /// </summary>
        /// <returns></returns>
        public abstract bool SetCurrentDao();

        public BaseService()
        {
            this.DisposableObjects = new List<IDisposable>();
            this.SetCurrentDao();
        }

        #region Create

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public T Insert(T entity)
        {
            this.CurrentDao.Insert(entity);
            DbSessionContext.SaveChanges();
            return entity;
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public T Update(T entity)
        {
            this.CurrentDao.Update(entity);
            if (this.DbSessionContext.SaveChanges() <= 0)
            {
                return null;
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
        public T GetModel(Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentDao.GetModel(whereLambda);
        }
        /// <summary>
        /// 通过主键得到一个对象实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public T GetModel(object keyValue)
        {
            return this.CurrentDao.GetModel(keyValue);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="whereLambda">Lambda表达式</param>
        /// <returns></returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentDao.GetList(whereLambda);
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
        public IQueryable<T> GetList<S>(int pageSize, int pageIndex, out int rowCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            return this.CurrentDao.GetList<S>(pageSize, pageIndex, out rowCount, whereLambda, orderByLambda, isAsc);
        }
        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        public long Count(Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentDao.Count(whereLambda);
        }
        #endregion

        #region Delete
        /// <summary>
        /// 通过实体对象删除一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int Delete(T entity)
        {
            this.CurrentDao.Delete(entity);
            return DbSessionContext.SaveChanges();
        }
        /// <summary>
        /// 通过主键删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public int Delete(object id)
        {
            this.CurrentDao.Delete(id);
            return DbSessionContext.SaveChanges();
        }
        /// <summary>
        ///  通过实体对象逻辑删除一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int DeleteByLogical(T entity)
        {
            this.CurrentDao.DeleteByLogical(entity);
            return DbSessionContext.SaveChanges();
        }
        /// <summary>
        /// 通过实体对象删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        public int DeleteByLogical(object id)
        {
            this.CurrentDao.DeleteByLogical(id);
            return DbSessionContext.SaveChanges();
        }
        #endregion

        #region 将BaseService的子类中的CurrentDao销毁
        /// <summary>
        /// IList<IDisposable>的集合
        /// </summary>
        public IList<IDisposable> DisposableObjects { get; private set; }

        /// <summary>
        /// 允许子类把CurrentDao放入其中
        /// </summary>
        /// <param name="obj"></param>
        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        /// <summary>
        /// 遍历所有的CurrentDao进行销毁
        /// </summary>
        public void Dispose()
        {
            foreach (IDisposable obj in this.DisposableObjects)
            {
                if (obj != null)
                {
                    obj.Dispose();
                }
            }
        }
        #endregion
    }
}
