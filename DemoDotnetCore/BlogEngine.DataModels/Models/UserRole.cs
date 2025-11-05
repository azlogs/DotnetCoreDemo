using System;

namespace BlogEngine.DataModels.Models
{
    public partial class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsDelete { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
