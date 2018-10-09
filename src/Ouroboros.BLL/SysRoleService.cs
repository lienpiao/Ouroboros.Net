using Ouroboros.IBLL;
using Ouroboros.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.BLL
{
    public class SysRoleService : BaseService<SysRole>, ISysRoleService
    {
        public SysRoleService():base(){ }

        /// <summary>
        /// 确定当前的CurrentDao值
        /// </summary>
        /// <returns></returns>
        public override bool SetCurrentDao()
        {
            this.CurrentDao = DbSessionContext.SysRoleDao;
            this.AddDisposableObject(this.CurrentDao);
            return true;
        }
    }
}
