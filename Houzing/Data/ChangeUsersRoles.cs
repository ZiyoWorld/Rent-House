using Microsoft.AspNetCore.Identity;

namespace Houzing.Data
{
    public class ChangeUsersRoles
    {
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeUsersRoles()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
