using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblUserRoles")]
    public class UserRole : BaseEntity
    {
        [Key]
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public string FormName { get; set; }
        public AuthType AuthorizeType { get; set; }
        [NotMapped]
        public string GetAuthCode
        {
            get
            {
                return AuthorizeType switch
                {
                    AuthType.Show => FormName + "Show",
                    AuthType.Add => FormName + "Add",
                    AuthType.Delete => FormName + "Del",
                    AuthType.Update => FormName + "Up",
                    AuthType.Full => FormName + "Full",
                    _ => throw new NotSupportedException()
                };
            }
        }
    }
}
public enum AuthType
{
    Show = 1,
    Add = 2,
    Delete = 3,
    Update = 4,
    Full = 5
}