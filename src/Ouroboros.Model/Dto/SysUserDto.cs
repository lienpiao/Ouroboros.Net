using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Model.Dto
{
    public class SysUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ShowName { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<SysUserAction> SysUserAction { get; set; }
        public virtual ICollection<SysRole> SysRole { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }
    }
}
