namespace GwentBot.Model
{
    public class Game
    {
        private bool? iWonCoin;

        public Game(Deck deck, User user)
        {
            this.Deck = deck;
            this.User = user;
        }

        public Deck Deck { get; }

        public bool? IWonCoin
        {
            get => iWonCoin;
            set => iWonCoin = (iWonCoin == null) ? value : iWonCoin;
        }

        public byte[] MatchResultsScreenBitmap { get; set; }
        public byte[] MatchRewardsScreenBitmap { get; set; }
        public User User { get; }
    }
}