using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media;

namespace Mastermind
{

    public class GameOptions : INotifyPropertyChanged
    {
        private string _playerName;
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                _playerName = value;
                OnPropertyChanged("PlayerName");
            }
        }

        public GameOptions()
        {
            _playerName = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Game
    {
        public GameOptions GameOptions { get; set; }

        public Guess[] Guesses { get; set; }
        
        public Guess CurrentGuess { get; set; }

        public int GuessNumber { get; set; }

        public Solution GameSolution { get; set; }

        public Game(GameOptions gameOptions)
        {
            GameOptions = gameOptions;
            Guesses = new Guess[10];
            CurrentGuess = new Guess();
            GuessNumber = 0;
            GameSolution = new Solution();
        }  
    }

    public class Solution
    {
        public GuessColor[] GuessColors { get; set; }

        public Solution()
        {
            GuessColors = new GuessColor[4];
            Random random = new Random();
            for (int i=0; i<4; i++)
            {
                int randomNumber = random.Next(0, 6);
                GuessColors[i] = new GuessColor(randomNumber);
            }         
        }
    }

    public class Guess
    {
        public Result Result { get; set; }

        public bool Correct { get; set; }

        public GuessColor[] GuessColors { get; set; }

        public Guess()
        {
            GuessColors = new GuessColor[4];
            Correct = false;
            for (int i = 0; i < 4; i++)
            {
                GuessColors[i] = new GuessColor();
            }
        }

        public void CalculateResult(Solution gameSolution)
        {
            int nrBlack = 0;
            int nrWhite = 0;
            List<int> usedGuessPositions = new List<int>();
            List<int> usedSolutionPositions = new List<int>();

            /* Check amount of same color same position --> black */
            for (int solutionPosition = 0; solutionPosition < 4; solutionPosition++)    
            {
                for (int guessPosition = 0; guessPosition < 4; guessPosition++)
                {
                    if (!usedGuessPositions.Contains(guessPosition) && !usedSolutionPositions.Contains(solutionPosition))
                    {
                        if (GuessColors[guessPosition] == gameSolution.GuessColors[solutionPosition])
                        {
                            if (guessPosition == solutionPosition)
                            {
                                nrBlack = nrBlack + 1;
                                usedGuessPositions.Add(guessPosition);
                                usedSolutionPositions.Add(solutionPosition);
                            }
                        }
                    }
                }
            }

            /* Check amount of same color different position --> white */
            for (int solutionPosition = 0; solutionPosition < 4; solutionPosition++)
            {
                for (int guessPosition = 0; guessPosition < 4; guessPosition++)
                {
                    if (!usedGuessPositions.Contains(guessPosition) && !usedSolutionPositions.Contains(solutionPosition))
                    {
                        if (GuessColors[guessPosition] == gameSolution.GuessColors[solutionPosition])
                        {
                            if (guessPosition != solutionPosition)
                            {
                                nrWhite = nrWhite + 1;
                                usedGuessPositions.Add(guessPosition);
                                usedSolutionPositions.Add(solutionPosition);
                            }
                        }
                    }
                }
            }
            if (nrBlack == 4) { Correct = true; }
            Result = new Result(nrBlack, nrWhite);
        }
    }

    public class GuessColor: INotifyPropertyChanged
    {
        private readonly Color[] MASTERMINDCOLORS = new Color[] { Colors.White, Colors.Black, Colors.Green, Colors.Red, Colors.Blue, Colors.Yellow };

        public int ColorNumber { get; set; }

        private SolidColorBrush _color;
        public SolidColorBrush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public GuessColor()
        {
            ColorNumber = 0;
            Color = new SolidColorBrush(MASTERMINDCOLORS[ColorNumber]);
        }

        public GuessColor(int colorNumber)
        {
            this.ColorNumber = colorNumber;
            Color = new SolidColorBrush(MASTERMINDCOLORS[colorNumber]);
        }

        public void ToggleColor()
        {
            ColorNumber = (ColorNumber + 1) % 6;
            Color.Color = MASTERMINDCOLORS[ColorNumber];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public static bool operator == (GuessColor color1, GuessColor color2)
        {
            return color1.ColorNumber == color2.ColorNumber;
        }

        public static bool operator !=(GuessColor color1, GuessColor color2)
        {
            return !(color1 == color2);
        }

        public override bool Equals(object color)
        {
            return ColorNumber == ((GuessColor)color).ColorNumber;
        }

        public override int GetHashCode()
        {
            return ColorNumber;
        }
    }

    public class Result
    {
        public ResultColor[] ResultColors { get; set; }

        public Result(int nrBlack, int nrWhite)
        {
            ResultColors = new ResultColor[4];
            for (int position = 0; position < 4; position++)
            {
                if (nrBlack > 0)
                {
                    ResultColors[position] = new ResultColor(Colors.Black);
                    nrBlack = nrBlack - 1;
                }
                else if (nrWhite > 0)
                {
                    ResultColors[position] = new ResultColor(Colors.White);
                    nrWhite = nrWhite - 1;
                }
            }         
        }
    }

    public class ResultColor : INotifyPropertyChanged
    {
        private SolidColorBrush _color;
        public SolidColorBrush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public ResultColor(Color color)
        {
            Color = new SolidColorBrush(color);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
