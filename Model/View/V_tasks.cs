using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class V_tasks
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public int? Duration { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Finish { get; set; }
        public decimal? PercentComplete { get; set; }
        public int? Summary { get; set; }
        public int? Critical { get; set; }
        public int? Milestone { get; set; }
        public int? ParentTaskUID { get; set; }
    }
}
