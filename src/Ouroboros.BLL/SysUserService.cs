using Ouroboros.IBLL;
using Ouroboros.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.BLL
{
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        public SysUserService():base(){ }

        /// <summary>
        /// 确定当前的CurrentDao值
        /// </summary>
        /// <returns></returns>
        public override bool SetCurrentDao()
        {
            this.CurrentDao = DbSessionContext.SysUserDao;
            this.AddDisposableObject(this.CurrentDao);
            return true;
        }
    }
}
