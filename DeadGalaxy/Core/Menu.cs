using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using DeadGalaxy.Core.Helpers;
using DeadGalaxy.Core.UI;
using Raylib_cs;

namespace DeadGalaxy.Core
{
    internal class Menu
    {
        /// <summary>
        /// Menu instance
        /// </summary>
        public static Menu? Instance { get; private set; }

        public bool IsMenuActive = false;

        private static readonly Color Background = new(0, 0, 0, 200);

        private Button? ResumeButton;
        private Button? SettingsButton;
        private Button? QuitButton;
        private Button? AudioButton;
        private Button? GraphicsButton;
        private Button? BackButton;

        private int CurrentMenu = 0;

        public static void InitButtons()
        {
            if (Instance == null)
            {
                Raylib.TraceLog(TraceLogLevel.Warning, "[Menu]: Couldn't create buttons because no menu was created!");

                return;
            }

            var width = Raylib.GetScreenWidth();
            var height = Raylib.GetScreenHeight();

            var lowerOffset = 0.7f;

            var offsetDiff = 0.08f;

            Instance.ResumeButton = new Button("Resume", 50, (int)(height * lowerOffset), 40, Color.White);
            Instance.AudioButton = new Button("Audio", 50, (int)(height * lowerOffset), 40, Color.White);

            lowerOffset += offsetDiff;

            Instance.SettingsButton = new Button("Settings", 50, (int)(height * lowerOffset), 40, Color.White);
            Instance.GraphicsButton = new Button("Graphics", 50, (int)(height * lowerOffset), 40, Color.White);

            lowerOffset += offsetDiff;

            Instance.QuitButton = new Button("Quit", 50, (int)(height * lowerOffset), 40, Color.White);
            Instance.BackButton = new Button("Back", 50, (int)(height * lowerOffset), 40, Color.White);
        }

        public static void DestroyButtons()
        {
            if (Instance == null)
            {
                Raylib.TraceLog(TraceLogLevel.Warning, "[Menu]: Couldn't destroy buttons because no menu was created!");

                return;
            }

            Instance.ResumeButton = null;
            Instance.SettingsButton = null;
            Instance.QuitButton = null;
        }

        public void CloseMenu()
        {
            IsMenuActive = false;
            GuiHelpers.SetGuiMode(false);

            CurrentMenu = 0;

            DestroyButtons();
        }

        /// <summary>
        /// Initializes menu
        /// </summary>
        public static void Init()
        {
            if (Instance != null)
            {
                Raylib.TraceLog(TraceLogLevel.Warning, "[Menu]: Couldn't create menu because it is already created!");

                return;
            }

            Instance = new Menu();
        }

        /// <summary>
        /// Updates menu
        /// </summary>
        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Tab))
            {
                IsMenuActive = !IsMenuActive;
                GuiHelpers.SetGuiMode(IsMenuActive);

                if (IsMenuActive)
                {
                    InitButtons();
                }
                else
                {
                    CloseMenu();
                }
            }

            if (!IsMenuActive)
            {
                return;
            }

            switch (CurrentMenu)
            {
                case 0:
                    if (ResumeButton.IsClick())
                    {
                        CloseMenu();
                    }
                    else if (SettingsButton.IsClick())
                    {
                        CurrentMenu = 1;
                    }
                    break;
                case 1:
                    if (BackButton.IsClick())
                    {
                        CurrentMenu = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Renders menu
        /// </summary>
        public void Render()
        {
            if (!IsMenuActive)
            {
                return;
            }

            var width = Raylib.GetScreenWidth();
            var height = Raylib.GetScreenHeight();

            Raylib.DrawRectangle(0, 0, width, height, Background);

            switch (CurrentMenu)
            {
                case 0:
                    ResumeButton?.Update();
                    SettingsButton?.Update();
                    QuitButton?.Update();
                    break;
                case 1:
                    AudioButton?.Update();
                    GraphicsButton?.Update();
                    BackButton?.Update();
                    break;
                default:
                    break;
            }
        }
    }
}
