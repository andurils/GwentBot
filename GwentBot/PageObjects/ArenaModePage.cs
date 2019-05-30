﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class ArenaModePage : PageObject
    {
        public ArenaModePage(IGwentStateChecker gwentStateChecker, IWaitingService waitingService)
            : base(gwentStateChecker, waitingService)
        {
        }

        protected override bool VerifyingPage()
        {
            return this.gwentStateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.ArenaModeTab;
        }
    }
}