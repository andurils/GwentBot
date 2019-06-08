namespace GwentBot.Model
{
    public class User
    {
        public User(string userName)
        {
            this.UserName = userName;
        }

        public string UserName { get; }
    }
}