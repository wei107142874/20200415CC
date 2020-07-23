using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Entites
{
    public class EntityBase
    {
        [EntityField(ignore:true)]
        public long Id { get; set; }

        public int Is_Enable { get; set; } = 1;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime DeleteTime { get; set; } = DateTime.MinValue;
    }
}
