using System;

namespace Basic.Common
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreationTime { get; set; }

        public long? CreationId { get; set; }

        public DateTime ModificationTime  { get; set; }

        public long? ModificationId { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDelete { get; set; }

        public long DeleteId { get; set; }
    }
}
