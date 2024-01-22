using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace GD.Finishing.WebAPI.Models
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(User user)
        {
            UserID = user.UserID;
            Name = user.Name;
            LastName = user.LastName;
            UserName = user.UserName;
            IsActive = user.IsActive;
            AreaType area = AreaType.Acabado;
            Enum.TryParse(user.AreaID.ToString(), out area);
            Area = area;
            Token = string.Empty;
        }

        public int UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public AreaType Area { get; set; }
        public string Token { get; set; }
    }
}
