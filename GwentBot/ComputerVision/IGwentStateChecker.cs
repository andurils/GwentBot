﻿namespace GwentBot.ComputerVision
{
    internal enum FriendlyGameStartStates
    {
        LoadingMatchSettings,
        MatchSettings,
        WaitingReadinessOpponent,
        Unknown,
    }

    internal enum GameSessionStates
    {
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
    }
}