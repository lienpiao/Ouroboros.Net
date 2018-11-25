using Ouroboros.Common;
using Ouroboros.IBLL;
using Ouroboros.IDAL;
using Ouroboros.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.BLL
{
    public class SysActionService : BaseService<SysAction>, ISysActionService
    {
        #region 依赖注入
        ISysActionDao dao;
        /// <summary>
        /// 定义构造函数，接收autofac将数据仓储层的具体实现类的对象注入到此类中
        /// </summary>
        /// <param name="dao">具体实现类的对象</param>
        public SysActionService(ISysActionDao dao)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            this.AddDisposableObject(this.CurrentDao);
        }
        #endregion

        public IList<zTree> GetMenuTree()
        {
            //获取数据
            var menulist = GetList(x => x.ActionType == Model.EnumList.ActionType.Menu && x.IsDeleted == false).ToList();
            //设置easyui树的节点
            zTree node = new zTree();
            //节点的名称为菜单管理
            node.name = "菜单节点";
            node.id = "0";
            node.open = true;
            //定义树节点的子节点
            node.children = new List<zTree>();
            //遍历info中的list，目的是找到list中的父对象。list储存所有的对象（包括父对象，子对象，子对象的子对象等等）
            for (var i = 0; i < menulist.Count(); i++)
            {
                SysAction o = menulist[i];
                //当对象的父类id为0的时候，说明这个是一个父对象
                if (o.ParentId == 0)
                {
                    //定义一个新的easyUI树节点
                    zTree c = new zTree();
                    //节点的id就是这个父对象的id
                    c.id = o.Id.ToString();
                    //节点的名称就是这个父对象的名称
                    c.name = o.ActionName.ToString();
                    //获取父对象下的子对象
                    GetChild(c, o, menulist);
                    //如果这个父对象下面没有子对象的话，将这个父对象的state属性设置为closed
                    if (c.children != null && c.children.Count > 0)
                    {
                        c.open = false;
                    }
                    //将这个父对象放入node之中（node相当于爷爷对象0_0）
                    node.children.Add(c);
                }

            }
            IList<zTree> list = new List<zTree>();
            list.Add(node);
            return list;
        }

        /// <summary>
        /// 获取父对象下的子对象
        /// </summary>
        /// <param name="parent">父节点树</param>
        /// <param name="uparent">父对象</param>
        /// <param name="allMenu">所有的节点</param>
        private void GetChild(zTree parent, SysAction uparent, IList<SysAction> allMenu)
        {
            //遍历所有的对象
            for (var i = 0; i < allMenu.Count; i++)
            {
                SysAction a = allMenu[i];
                //如果这个对象的父id和这个父对象的id是相同的，那么说明这个对象是父对象的子对象
                if (a.ParentId == uparent.Id)
                {
                    //如果是第一次遍历，也就是说父对象的子对象还为空，那么我们要初始化一个子对象
                    if (parent.children == null)
                    {
                        parent.children = new List<zTree>();

                    }
                    //设置一个新的子对象，这里又用一个node来命名新的子对象不是很妥当
                    zTree node = new zTree();
                    //设置子对象的id
                    node.id = a.Id.ToString();
                    //设置子对象的名称
                    node.name = a.ActionName.ToString();
                    //一个递归调用，查找子对象是否还存在子对象
                    GetChild(node, a, allMenu);
                    //如果子对象没有子对象，就设置它的state属性为closed
                    if (node.children != null && node.children.Count > 0)
                    {
                        node.open = false;
                    }
                    //将这个子对象放入父easyUI树中
                    parent.children.Add(node);
                }
            }
        }
    }
}
