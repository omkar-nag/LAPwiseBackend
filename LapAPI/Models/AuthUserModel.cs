namespace LapAPI.Models
{
    public class AuthUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ToString()
        {
            return this.UserName + " " + this.Password;
        }
    }
}
