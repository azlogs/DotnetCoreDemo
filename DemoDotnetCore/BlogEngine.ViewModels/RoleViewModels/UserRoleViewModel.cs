using System;

namespace BlogEngine.ViewModels.RoleViewModels
{
    public class UserRoleViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
