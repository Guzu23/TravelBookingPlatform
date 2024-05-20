using DataAccessLayer.Models;

namespace BusinessLayer.Contracts
{
    public interface IUserDelegate
    {
        event EventHandler<UserEventArgs> UserActivated;
        event EventHandler<UserEventArgs> UserDeactivated;

        void OnUserActivated(User user);
        void OnUserDeactivated(User user);
    }

    public class UserEventArgs : EventArgs
    {
        public User User { get; }

        public UserEventArgs(User user)
        {
            User = user;
        }
    }
}
