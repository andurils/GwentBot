namespace GwentBot.ComputerVision
{
    internal enum FriendlyGameStartStates
    {
        LoadingMatchSettings,
        MatchSettings,
        WaitingReadinessOpponent,
        Unknown,
    }

    internal enum CoinTossStates
    {
        StartToss,
        СoinWon,
        CoinLost,
        Unknown
    }

    internal enum Notifications
    {
        FriendlyDuel,
        ReceivedReward,
        RewardsTab,
        NoNotifications
    }

    internal enum GameSessionStates
    {
        Mulligan,
        OpponentChangesCards,
        MyTurnPlay,
        EnemyTurnPlay,
        WinAlert,
        LosingAlert,
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

    internal enum StartGameStates
    {
        GameLoadingScreen,
        WelcomeScreen,
        Unknown,
    }

    internal interface IGwentStateChecker
    {
        IWindowScreenShotCreator ScreenShotCreator { get; }

        FriendlyGameStartStates GetCurrentFriendlyGameStartStates();

        GameSessionStates GetCurrentGameSessionStates();

        GlobalGameStates GetCurrentGlobalGameStates();

        StartGameStates GetCurrentStartGameStates();

        CoinTossStates GetCurrentCoinTossStates();

        Notifications GetCurrentNotifications();
    }
}