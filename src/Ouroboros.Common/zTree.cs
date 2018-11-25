using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Common
{
    /// <summary>
    /// zTree
    /// </summary>
    public class zTree
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public List<zTree> children { get; set; }
    }
}
