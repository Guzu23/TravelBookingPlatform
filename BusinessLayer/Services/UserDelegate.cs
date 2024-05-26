using BusinessLayer.Contracts;
using DataAccessLayer.Models;

namespace BusinessLayer.Services
{
    public class UserDelegate : IUserDelegate
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public event EventHandler<UserEventArgs> UserActivated;
        public event EventHandler<UserEventArgs> UserDeactivated;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
