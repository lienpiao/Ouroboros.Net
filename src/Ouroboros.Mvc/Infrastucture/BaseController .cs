using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ouroboros.Mvc.Infrastucture
{
    /// <summary>
    /// Controller基类
    /// </summary>
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.DisposableObjects = new List<IDisposable>();
        }

        #region 手动垃圾回收的机制
        protected IList<IDisposable> DisposableObjects { get; private set; }

        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IDisposable obj in this.DisposableObjects)
                {
                    if (null != obj)
                    {
                        obj.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
