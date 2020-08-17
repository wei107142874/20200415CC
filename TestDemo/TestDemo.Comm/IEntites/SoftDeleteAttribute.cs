using System;
using System.Collections.Generic;
using System.Text;

namespace Basic.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SoftDeleteAttribute : Attribute
    {
        public SoftDeleteAttribute()
        {
        }

        public SoftDeleteAttribute(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }

        public SoftDeleteAttribute(bool isDeleted, bool deletionTime)
        {
            IsDeleted = isDeleted;
            DeletionTime = deletionTime;
        }

        public SoftDeleteAttribute(bool isDeleted, bool deletionTime, bool deleterId)
        {
            IsDeleted = isDeleted;
            DeletionTime = deletionTime;
            DeleterId = deleterId;
        }

        public bool IsDeleted { get; set; }

        public bool DeletionTime { get; set; }

        public bool DeleterId { get; set; }
    }
}
