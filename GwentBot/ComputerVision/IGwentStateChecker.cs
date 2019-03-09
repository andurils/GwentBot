namespace GwentBot.ComputerVision
{
    internal enum FriendlyGameStartStates
    {
        Unknown,
        LoadingMatchSettings,
        MatchSettings,
        WaitingReadinessOpponent,
    }

    internal enum GameSessionStates
    {
    }

    internal enum GlobalGameStates
    {
        Unknown,
        MainMenu,
        GameModesTab,
        ArenaModeTab,
        HeavyLoading,
    }

    internal enum StartGameStates
    {
        Unknown,
        GameLoadingScreen,
        WelcomeScreen,
    }

    internal interface IGwentStateChecker
    {
        IWindowScreenShotCreator ScreenShotCreator { get; }

        FriendlyGameStartStates GetCurrentFriendlyGameStartStates();

        GameSessionStates GetCurrentGameSessionStates();

        GlobalGameStates GetCurrentGlobalGameStates();

        StartGameStates GetCurrentStartGameStates();
    }
}