using System;
using Agent.Contrib.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;

namespace SimpleMemoryGame
{
    public class Program
    {
        static Bitmap _display;
        static readonly Font _fontSmall = Resources.GetFont(Resources.FontResources.small);
        static GameManager _game;

        static ButtonsSequence _middleDblClick;

        public static void Main()
        {
            //Instanciate the button helper
            ButtonHelper.ButtonSetup = new Buttons[] { Buttons.TopRight, Buttons.MiddleRight, Buttons.BottomRight, Buttons.TopLeft, Buttons.BottomLeft };

            //Instanciate the sequence detector for the main screen
            var mainDetector = new ButtonsSequenceDetector(ButtonHelper.Current);
            // For this one we only need to track the middle button
            mainDetector.ButtonsToTrack.Add(Buttons.MiddleRight);

            //We want to track the double click to start the game.
            _middleDblClick = new ButtonsSequence();
            _middleDblClick.AddStep(Buttons.MiddleRight, ButtonDirection.Down);
            _middleDblClick.AddStep(Buttons.MiddleRight, ButtonDirection.Up);
            _middleDblClick.AddStep(Buttons.MiddleRight, ButtonDirection.Down);
            _middleDblClick.AddStep(Buttons.MiddleRight, ButtonDirection.Up);
            _middleDblClick.AddHandler(Current_OnButtonDoubleClick);
            mainDetector.AddSequence(_middleDblClick);

            // initialize display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);
            //Instanciate game
            _game = new GameManager(_display, ButtonHelper.Current);

            // sample "hello world" code
            _display.Clear();
            _display.DrawText("To start or stop game", _fontSmall, Color.White, 10, 44);
            _display.DrawText("Double click here. >", _fontSmall, Color.White, 10, 54);
            _display.Flush();

            // go to sleep; all further code should be timer-driven or event-driven
            Thread.Sleep(Timeout.Infinite);
        }

        static void DetectorOnSequenceDetected(string sequence)
        {
            _display.Clear();
            _display.DrawText("Sequence detected", _fontSmall, Color.White, 10, 64);
            _display.DrawText(sequence, _fontSmall, Color.White, 10, 74);
            _display.Flush();
        }

        static void Current_OnButtonDoubleClick(string state)
        {
            if (_game.GameStatus == "Stopped")
            {
                _game.StartGame();
            }
        }      
    }
}
