using Ouroboros.Common;
using Ouroboros.IBLL;
using Ouroboros.Model;
using Ouroboros.Model.Dto;
using Ouroboros.Mvc.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Ouroboros.Web.Areas.System.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ISysUserService sysUserService)
        {
            base.SysUserService = sysUserService;
            this.AddDisposableObject(SysUserService);
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
            string userName = Request.QueryString["userName"];
            //2.分页参数合法性验证
            int pageIndex = sPageIndex.AsInt();
            int pageSize = sPageSize.AsInt();
            int rowCount = 0;
            //3.实现分页数据的获取操作
            object list;
            if (userName.IsEmpty())
            {
                list = SysUserService.GetList(pageSize, pageIndex, out rowCount, x => x.IsDeleted == false, x => x.Id, false).Select(x => new { x.Id, x.UserName, x.ShowName }).ToList();
            }
            else
            {
                list = SysUserService.GetList(pageSize, pageIndex, out rowCount, x => x.UserName.Contains(userName) && x.IsDeleted == false, x => x.Id, false).Select(x => new { x.Id, x.UserName, x.ShowName }).ToList();
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
        public ActionResult Add(SysUserDto dto)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return base.WriteError("实体验证失败");
                }
                dto.Password = EncryptionHelper.Get32MD5One(dto.Password);
                SysUser entity = dto.EntityMap();
                SysUserService.Insert(entity);
                SysUserService.SaveChanges();
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
            var model = SysUserService.GetModel(id);
            //2 将老数据传入视图
            return View(model.EntityMap());
        }

        [HttpPost]
        public ActionResult Edit(SysUserDto dto)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return WriteError("实体验证失败");
                }
                if (dto.Password!=dto.OldPassword)
                {
                    dto.Password = EncryptionHelper.Get32MD5One(dto.Password);
                }
                SysUser entity = dto.EntityMap();
                SysUserService.Update(entity);
                SysUserService.SaveChanges();
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
                    SysUserService.DeleteByLogical(int.Parse(uid));
                }

                //批量删除操作
                SysUserService.SaveChanges();

                return WriteSuccess("删除成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

    }
}