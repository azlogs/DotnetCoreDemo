using System;
using System.Collections.Generic;

namespace BlogEngine.DataModels.Models
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
