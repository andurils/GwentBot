// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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