using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Model.Dto
{
    public class SysActionDto
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string ActionCode { get; set; }
        public Ouroboros.Model.EnumList.ActionType ActionType { get; set; }
        public int ParentId { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Ouroboros.Model.EnumList.HttpMethodType HttpMethd { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<SysUserAction> SysUserAction { get; set; }
        public virtual ICollection<SysRole> SysRole { get; set; }

        /// <summary>
        /// Parent树下拉框
        /// </summary>
        public string ParentTree { get; set; }
    }
}
