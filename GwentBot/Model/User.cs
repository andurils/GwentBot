namespace GwentBot.Model
{
    public class User
    {
        public User(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}