using System;
using System.Text.RegularExpressions;
using System.Threading;
using Agent.Contrib.Drawing;
using Agent.Contrib.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace SimpleMemoryGame
{
    public class GameManager
    {
        //Application ressources
        private static readonly Font _fontSmall = Resources.GetFont(Resources.FontResources.small);
        private static Bitmap _victory = new Bitmap(Resources.GetBytes(Resources.BinaryResources.Victory), Bitmap.BitmapImageType.Bmp);
        private Bitmap _screen;
        private Drawing _drawing = new Drawing();
        private Point[] _topLeft = { new Point(5, 5), new Point(5, 35), new Point(35, 35), new Point(35, 5) };
        private Point[] _bottomLeft = { new Point(5, 93), new Point(5, 123), new Point(35, 123), new Point(35, 93) };
        private Point[] _topRight = { new Point(93, 5), new Point(93, 35), new Point(123, 35), new Point(123, 5) };
        private Point[] _bottomRight = { new Point(93, 93), new Point(93, 123), new Point(123, 123), new Point(123, 93) };

        //Game state
        private bool _isRunning = false;
        public String GameStatus
        {
            get
            {
                if (_isRunning)
                {
                    return "Running";
                }
                else
                {
                    return "Stopped";
                }
            }
        }
        private int _currentPuzzle = 0;
        private readonly int _maxPuzzle = 3;
        private ButtonsSequence _sequence;
        private ButtonsSequenceDetector _seqReader;
        private Random _random;

        public GameManager(Bitmap screen, ButtonHelper helper)
        {
            _screen = screen;
            _seqReader = new ButtonsSequenceDetector(helper);

            //For the game, we only track 4 buttons
            _seqReader.ButtonsToTrack.Add(Buttons.BottomLeft);
            _seqReader.ButtonsToTrack.Add(Buttons.BottomRight);
            _seqReader.ButtonsToTrack.Add(Buttons.TopLeft);
            _seqReader.ButtonsToTrack.Add(Buttons.TopRight);

            _sequence = new ButtonsSequence();
            _sequence.AddHandler(ValidSequenceDetected);

            _random = new Random();
        }

        public void StartGame()
        {
            if (_isRunning == false)
            {
                _isRunning = true;
                LaunchRound();
            }

        }

        private void LaunchRound()
        {
            GenerateNewSequence();

            ShowSequencetoPlayer();

            //Give the hand to the player
            _screen.Clear();
            _drawing.DrawTextCentered(_screen, "Reproduce sequence", _fontSmall, Color.White);
            _screen.Flush();

            EnableSequenceTracking();
        }

        /// <summary>
        /// Generates a new random sequence
        /// </summary>
        private void GenerateNewSequence()
        {
            Buttons curButton;

            _sequence.Clear();
            for (int i = 0; i <= _currentPuzzle; i++)
            {
                curButton = (Buttons)_seqReader.ButtonsToTrack[_random.Next(_seqReader.ButtonsToTrack.Count)];

                _sequence.AddStep(curButton, ButtonDirection.Down);
                _sequence.AddStep(curButton, ButtonDirection.Up);
            }

        }

        private void ShowSequencetoPlayer()
        {
            _screen.Clear();
            //Draw empty squares
            _drawing.DrawPoly(_screen, _topLeft, Color.White, 1, Color.White, PolyFill.POLYFILL_EMPTY);
            _drawing.DrawPoly(_screen, _bottomLeft, Color.White, 1, Color.White, PolyFill.POLYFILL_EMPTY);
            _drawing.DrawPoly(_screen, _topRight, Color.White, 1, Color.White, PolyFill.POLYFILL_EMPTY);
            _drawing.DrawPoly(_screen, _bottomRight, Color.White, 1, Color.White, PolyFill.POLYFILL_EMPTY);
            _drawing.DrawTextCentered(_screen, "Level " + (_currentPuzzle + 1), _fontSmall, Color.White);
            _screen.Flush();

            //Sequence is made of a series of 2 caracters.
            //Each couple is an operation to do.
            var regex = new Regex(@"\d{2}");
            MatchCollection result = regex.Matches(_sequence.Sequence);
            foreach (Match item in result)
            {
                DisplayMouvement(item.Value);
            }
        }

        private void DisplayMouvement(string value)
        {
            Point[] buttonToDisplay = null;

            //First number represents the button
            switch (value[0])
            {
                case '0':
                    buttonToDisplay = _topLeft;
                    break;
                case '1':
                    buttonToDisplay = _bottomLeft;
                    break;
                case '2':
                    buttonToDisplay = _topRight;
                    break;
                case '4':
                    buttonToDisplay = _bottomRight;
                    break;
                default:
                    break;
            }

            //Second number indicates if the button must be drawn filled or not
            switch (value[1])
            {
                case '0':
                    _drawing.DrawPoly(_screen, buttonToDisplay, Color.White, 1, Color.White, PolyFill.POLYFILL_SOLID);
                    break;
                case '1':
                    _drawing.DrawPoly(_screen, buttonToDisplay, Color.White, 1, Color.Black, PolyFill.POLYFILL_SOLID);
                    break;
                default:
                    break;
            }            
            _screen.Flush();
            Thread.Sleep(500);
        }

        public void ValidSequenceDetected(string userSequence)
        {
            //We don'y want to be interrupted during answer remove the sequence
            //and stop listening to invalid sequence event.
            DisableSequenceTracking();

            _screen.Clear();
            _drawing.DrawTextCentered(_screen, "Correct", _fontSmall, Color.White);
            _screen.Flush();
            Thread.Sleep(1000);

            if (_currentPuzzle < _maxPuzzle)
            {
                //We launch the next puzzle
                _currentPuzzle++;
                LaunchRound();
            }
            else
            {
                DisplayVictoryScreen();
                Thread.Sleep(1000);
                ResetGame();
            }
        }

        /// <summary>
        /// Enables sequence tracking
        /// </summary>
        private void EnableSequenceTracking()
        {
            _seqReader.AddSequence(_sequence);
            _seqReader.OnUnattendedSequence += InvalidSequenceDetected;
        }

        /// <summary>
        /// Disables sequence tracking
        /// </summary>
        private void DisableSequenceTracking()
        {
            //We don'y want to be interrupted during answer remove the sequence
            //and stop listening to invalid sequence event.
            _seqReader.RemoveSequence(_sequence);
            _seqReader.OnUnattendedSequence -= InvalidSequenceDetected;
        }

        private void InvalidSequenceDetected(string userSequence)
        {
            _screen.Clear();
            _drawing.DrawTextCentered(_screen, "False, you lost", _fontSmall, Color.White);
            _screen.Flush();

            ResetGame();
        }

        /// <summary>
        /// Display the victory screen
        /// </summary>
        private void DisplayVictoryScreen()
        {
            _screen.Clear();
            _screen.DrawImage(40, 5, _victory, 0, 0, 48, 48);
            //_drawing.DrawImageAtPoint(_screen, Resources.GetBytes(Resources.BinaryResources.Victory), Bitmap.BitmapImageType.Gif, new Point() { X = 40, Y = 5 });

            _drawing.DrawTextCentered(_screen, "You won", _fontSmall, Color.White);
            _screen.Flush();
        }

        /// <summary>
        /// Reset the game state for a new game.
        /// </summary>
        private void ResetGame()
        {
            _currentPuzzle = 0;
            _isRunning = false;
        }
    }
}
