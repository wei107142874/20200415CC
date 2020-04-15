using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace KA.Repository
{
   public class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] //是主键, 还是标识列
        public long Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime? DelTime { get; set; }
    }
}
