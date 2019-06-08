// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using GwentBot.ComputerVision;

namespace GwentBot.StateAbstractions
{
    internal enum CoinTossStates
    {
        StartToss,
        CoinWon,
        CoinLost,
        Unknown
    }

    internal enum FriendlyGameStartStates
    {
        LoadingMatchSettings,
        MatchSettings,
        CancelGameMessageBox,
        WaitingReadinessOpponent,
        Unknown,
    }

    internal enum GameSessionStates
    {
        Mulligan,
        EndMulliganMessageBox,
        OpponentChangesCards,
        MyTurnPlay,
        EnemyTurnPlay,
        GiveUpMessageBox,
        WinAlert,
        LosingAlert,
        MatchResultsScreen,
        MatchRewardsScreen,
        Unknown
    }

    internal enum GlobalGameStates
    {
        ArenaModeTab,
        HeavyLoading,
        GameModesTab,
        MainMenu,
        Unknown,
    }

    internal enum Notifications
    {
        FriendlyDuel,
        ReceivedReward,
        RewardsTab,
        NoNotifications
    }

    internal enum StartGameStates
    {
        GameLoadingScreen,
        WelcomeScreen,
        Unknown,
    }

    internal interface IGwentStateChecker
    {
        IWindowScreenShotCreator ScreenShotCreator { get; }

        CoinTossStates GetCurrentCoinTossStates();

        FriendlyGameStartStates GetCurrentFriendlyGameStartStates();

        GameSessionStates GetCurrentGameSessionStates();

        GlobalGameStates GetCurrentGlobalGameStates();

        Notifications GetCurrentNotifications();

        StartGameStates GetCurrentStartGameStates();

        byte[] GetGameScreenshotBitmap();
    }
}