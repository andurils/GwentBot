// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class GameSessionPage : PageObject
    {
        internal GameSessionPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        internal bool MyTurnPlay
        {
            get
            {
                return gwentStateChecker.GetCurrentGameSessionStates() ==
                    GameSessionStates.MyTurnPlay;
            }
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentGameSessionStates() !=
                GameSessionStates.Unknown;
        }
    }
}