namespace BusinessLayer.Dto
{
    public class UserDto : BaseEntityDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string ImageProfile { get; set; }

        public UserDto() { }
        public UserDto(string Username, string Email, string Password) : base()
        {
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.Active = true;
        }

        public void activateUser()
        {
            this.Active = true;
        }

        public void deactivateUser()
        {
            this.Active = false;
        }


    }
}
