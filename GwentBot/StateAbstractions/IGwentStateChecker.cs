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
        Unknown
    }

    internal enum GameSessionExceptionMessageBoxes
    {
        AfkGameLost,
        LocalClientProblem,
        NoMessageBoxes
    }

    internal enum GameSessionStates
    {
        SearchRival,
        Mulligan,
        EndMulliganMessageBox,
        OpponentChangesCards,
        SessionPageOpen,
        MyTurnPlay,
        EnemyTurnPlay,
        GiveUpMessageBox,
        OpponentSurrenderedMessageBox,
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
        Unknown
    }

    internal enum GlobalMessageBoxes
    {
        ErrorSearchingOpponent,
        ErrorConnectingToService,
        ConnectionLost,
        ServerOverloaded,
        ConnectionError,
        NoMessageBoxes
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
        Unknown
    }

    internal interface IGwentStateChecker
    {
        IWindowScreenShotCreator ScreenShotCreator { get; }

        CoinTossStates GetCurrentCoinTossStates();

        FriendlyGameStartStates GetCurrentFriendlyGameStartStates();

        GameSessionExceptionMessageBoxes GetCurrentGameSessionExceptionMessageBoxes();

        GameSessionStates GetCurrentGameSessionStates();

        GlobalGameStates GetCurrentGlobalGameStates();

        GlobalMessageBoxes GetCurrentGlobalMessageBoxes();

        Notifications GetCurrentNotifications();

        StartGameStates GetCurrentStartGameStates();

        byte[] GetGameScreenshotBitmap();
    }
}