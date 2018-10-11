using Ouroboros.Common;
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

        #region 封装ajax请求的返回方法
        protected ActionResult WriteSuccess(string msg)
        {
            return Json(new { status = (int)EnumManage.AjaxState.sucess, msg = msg });
        }

        protected ActionResult WriteSuccess(string msg, object obj)
        {
            return Json(new { status = (int)EnumManage.AjaxState.sucess, msg = msg, datas = obj });
        }

        protected ActionResult WriteError(string errmsg)
        {
            return Json(new { status = (int)EnumManage.AjaxState.error, msg = errmsg });
        }

        protected ActionResult WriteError(Exception ex)
        {
            return Json(new { status = (int)EnumManage.AjaxState.error, msg = ex.Message });
        }
        #endregion
    }
}
