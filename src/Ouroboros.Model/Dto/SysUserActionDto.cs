using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Model.Dto
{
    public class SysUserActionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActionId { get; set; }
        public bool HasPermission { get; set; }
        public bool IsDeleted { get; set; }

        public virtual SysUser SysUser { get; set; }
        public virtual SysAction SysAction { get; set; }
    }
}
