using System;

namespace BlogEngine.DataModels.Models
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Guid CreatedUserId { get; set; }

        public Guid UpdatedUserId { get; set; }
    }
}