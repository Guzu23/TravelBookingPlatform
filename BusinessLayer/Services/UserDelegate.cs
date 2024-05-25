using BusinessLayer.Contracts;
using DataAccessLayer.Models;

namespace BusinessLayer.Services
{
    public class UserDelegate : IUserDelegate
    {
        public event EventHandler<UserEventArgs> UserActivated;
        public event EventHandler<UserEventArgs> UserDeactivated;

        public void OnUserActivated(User user)
        {
            UserActivated?.Invoke(this, new UserEventArgs(user));
        }

        public void OnUserDeactivated(User user)
        {
            UserDeactivated?.Invoke(this, new UserEventArgs(user));
        }
    }
}
