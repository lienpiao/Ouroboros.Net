using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Model.Dto
{
    public class SysRoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActlve { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<SysUser> SysUser { get; set; }
        public virtual ICollection<SysAction> SysAction { get; set; }
    }
}
