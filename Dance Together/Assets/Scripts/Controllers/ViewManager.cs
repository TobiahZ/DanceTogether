﻿using UnityEngine;
using App.Events;

namespace App.Controllers
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField, Header("View Currently Showing")]
        private View currentView;

        // Track list for views that should always exist.
        [Header("Tracked Main Views")]
        public View LoadScreenIntro;
        public View LoadScreenDefault;
        public View MainMenu;
        public View SearchingForGameMenu;
        public View LobbyMenu;
        public View GameActiveMenu;
        public View GamePostMenu;
        public View GameEndMenu;

        [Header("Tracked Popup Views")]
        public View CountDownView;
        public View PauseMenu;
        public View RulesMenu;

        public View CurrentView
        {
            get { return currentView; }
        }

        public void HideAllViews()
        {
            // quick way to close all views. easy to avoid menu glitches.
            LoadScreenIntro?.CloseView();
            LoadScreenDefault?.CloseView();
            MainMenu?.CloseView();
            SearchingForGameMenu?.CloseView();
            LobbyMenu?.CloseView();
            GameActiveMenu?.CloseView();
            GamePostMenu?.CloseView();
            GameEndMenu?.CloseView();

            // popup Views
            CountDownView?.CloseView();
            RulesMenu?.CloseView();
            //PauseMenu?.CloseView(); // pause menu is a special case menu.
        }

        public void ShowSpecificView(View _view)
        {
            if (currentView == _view)
                return;

            HideAllViews();
            currentView = _view;
            currentView?.OpenView();
        }

        public void CloseCurrentView()
        {
            currentView?.CloseView();
            currentView = null;

        }

        public void ResponseToGameStateEvent(GameEventPayLoad.States _state)
        {
            switch (_state)
            {
                case GameEventPayLoad.States.NoStateChange:

                    break;
                case GameEventPayLoad.States.StandBy:
                    HideAllViews();
                    break;
                case GameEventPayLoad.States.Initialize:
                    ShowSpecificView(LoadScreenIntro);
                    break;
                case GameEventPayLoad.States.MainMenu:
                    ShowSpecificView(MainMenu);
                    PauseMenu?.CloseView(); // pause menu can be open in all view except main menu.
                    break;
                case GameEventPayLoad.States.SearchingForGame:
                    ShowSpecificView(SearchingForGameMenu);
                    break;
                case GameEventPayLoad.States.Lobby:
                    ShowSpecificView(LobbyMenu);
                    break;
                case GameEventPayLoad.States.GameInitialize:
                    CountDownView?.OpenView(); // special case. I want this to popup over the current view.
                    break;
                case GameEventPayLoad.States.GameActive:
                    ShowSpecificView(GameActiveMenu);
                    break;
                case GameEventPayLoad.States.GameEnded:
                    ShowSpecificView(GameEndMenu);
                    break;
                case GameEventPayLoad.States.GamePost:
                    ShowSpecificView(GamePostMenu);
                    break;
                default:

                    break;
            }
        }

        // Note, Main controller calls for main menu when it resets. this is a method for neiche cases.
        public void Reset()
        {
            // on Reset, Show MainView.
            ShowSpecificView(MainMenu);
        }
    }
}