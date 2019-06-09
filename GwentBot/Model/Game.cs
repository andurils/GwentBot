namespace GwentBot.Model
{
    public class Game
    {
        private bool? iWonCoin;

        public Game(Deck deck, User user)
        {
            Deck = deck;
            User = user;
        }

        public Deck Deck { get; }

        public bool? IWonCoin
        {
            get => iWonCoin;
            set => iWonCoin = iWonCoin ?? value;
        }

        public byte[] MatchResultsScreenBitmap { get; set; }
        public byte[] MatchRewardsScreenBitmap { get; set; }
        public User User { get; }
    }
}