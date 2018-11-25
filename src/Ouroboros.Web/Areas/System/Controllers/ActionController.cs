using Ouroboros.Common;
using Ouroboros.IBLL;
using Ouroboros.Model;
using Ouroboros.Model.Dto;
using Ouroboros.Model.EnumList;
using Ouroboros.Mvc.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Ouroboros.Web.Areas.System.Controllers
{
    public class ActionController : BaseController
    {
        public ActionController(ISysActionService sysActionService)
        {
            base.SysActionService = sysActionService;
            this.AddDisposableObject(sysActionService);
        }

        #region 列表
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            //1.获取用户传入的查询条件以及分页条件
            string sPageIndex = Request.QueryString["page"];
            string sPageSize = Request.QueryString["limit"];
            string actionName = Request.QueryString["actionName"];
            string sactionId = Request.QueryString["actionId"];
            //2.分页参数合法性验证
            int pageIndex = sPageIndex.AsInt();
            int pageSize = sPageSize.AsInt();
            int actionId = sactionId.AsInt();
            int rowCount = 0;
            //3.实现分页数据的获取操作
            object list;

            if (!string.IsNullOrEmpty(sactionId) && string.IsNullOrEmpty(actionName))
            {
                list = SysActionService.GetList(pageSize, pageIndex, out rowCount, x => x.ParentId == actionId || x.Id == actionId, x => x.Id, false)
                .Select(x => new { x.Id, x.ActionName, x.ActionCode, ActionType = x.ActionType.ToString(), x.ParentId, x.Url, x.Icon, x.Sort, x.Area, x.Controller, x.Action, x.HttpMethd }).ToList();
            }
            else if (!string.IsNullOrEmpty(actionName) && string.IsNullOrEmpty(sactionId))
            {
                list = SysActionService.GetList(pageSize, pageIndex, out rowCount, x => x.ActionName.Contains(actionName), x => x.Id, false)
                    .Select(x => new { x.Id, x.ActionName, x.ActionCode, ActionType = x.ActionType.ToString(), x.ParentId, x.Url, x.Icon, x.Sort, x.Area, x.Controller, x.Action, x.HttpMethd }).ToList();
            }
            else if (!string.IsNullOrEmpty(actionName) && !string.IsNullOrEmpty(sactionId))
            {
                list = SysActionService.GetList(pageSize, pageIndex, out rowCount, x => x.ActionName.Contains(actionName), x => x.Id, false).Where(x => x.ParentId == actionId || x.Id == actionId)
                .Select(x => new { x.Id, x.ActionName, x.ActionCode, ActionType = x.ActionType.ToString(), x.ParentId, x.Url, x.Icon, x.Sort, x.Area, x.Controller, x.Action, x.HttpMethd }).ToList();
            }
            else
            {
                list = SysActionService.GetList(pageSize, pageIndex, out rowCount, x => x.IsDeleted == false, x => x.Id, false)
                .Select(x => new { x.Id, x.ActionName, x.ActionCode, ActionType = x.ActionType.ToString(), x.ParentId, x.Url, x.Icon, x.Sort, x.Area, x.Controller, x.Action, x.HttpMethd }).ToList();
            }

            return Json(new { code = "0", msg = "", count = rowCount, data = list }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 新增

        /// <summary>
        /// 负责返回视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(SysActionDto dto)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return base.WriteError("实体验证失败");
                }
                SysAction entity = dto.EntityMap();
                SysActionService.Insert(entity);
                SysActionService.SaveChanges();
                return base.WriteSuccess("新增成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        #endregion

        #region 编辑
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //1 根据id做查询
            var model = SysActionService.GetModel(id);
            //2 将老数据传入视图
            return View(model.EntityMap());
        }

        [HttpPost]
        public ActionResult Edit(SysActionDto dto)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return WriteError("实体验证失败");
                }
                SysAction entity = dto.EntityMap();
                SysActionService.Update(entity);
                SysActionService.SaveChanges();
                return WriteSuccess("用户编辑成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }
        #endregion

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                //1.将id打断成一个数组
                string[] ids = id.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                //2.遍历ids进行数据的逻辑删除
                foreach (var uid in ids)
                {
                    SysActionService.DeleteByLogical(int.Parse(uid));
                }

                //批量删除操作
                SysActionService.SaveChanges();

                return WriteSuccess("删除成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LoadTree()
        {
            var data = SysActionService.GetMenuTree();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}