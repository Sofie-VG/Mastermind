using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mastermind
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public Game NewGame { get; }

        public GameWindow(GameOptions gameOptions)
        {
            NewGame = new Game(gameOptions);
            InitializeComponent();
            Solution.Visibility = Visibility.Hidden;
            DataContext = NewGame;
            SetDataContextGuess();
        }

        public void SetDataContextGuess()
        {
            Guess0.DataContext = NewGame.Guesses[0];
            Guess1.DataContext = NewGame.Guesses[1];
            Guess2.DataContext = NewGame.Guesses[2];
            Guess3.DataContext = NewGame.Guesses[3];
            Guess4.DataContext = NewGame.Guesses[4];
            Guess5.DataContext = NewGame.Guesses[5];
            Guess6.DataContext = NewGame.Guesses[6];
            Guess7.DataContext = NewGame.Guesses[7];
            Guess8.DataContext = NewGame.Guesses[8];
            Guess9.DataContext = NewGame.Guesses[9];
            CurrentGuess.DataContext = NewGame.CurrentGuess;
            Solution.DataContext = NewGame.GameSolution;
        }

        public void SetDataContextResult()
        {
            if (NewGame.Guesses[0] != null) { Result0.DataContext = NewGame.Guesses[0].Result; }
            if (NewGame.Guesses[1] != null) { Result1.DataContext = NewGame.Guesses[1].Result; }
            if (NewGame.Guesses[2] != null) { Result2.DataContext = NewGame.Guesses[2].Result; }
            if (NewGame.Guesses[3] != null) { Result3.DataContext = NewGame.Guesses[3].Result; }
            if (NewGame.Guesses[4] != null) { Result4.DataContext = NewGame.Guesses[4].Result; }
            if (NewGame.Guesses[5] != null) { Result5.DataContext = NewGame.Guesses[5].Result; }
            if (NewGame.Guesses[6] != null) { Result6.DataContext = NewGame.Guesses[6].Result; }
            if (NewGame.Guesses[7] != null) { Result7.DataContext = NewGame.Guesses[7].Result; }
            if (NewGame.Guesses[8] != null) { Result8.DataContext = NewGame.Guesses[8].Result; }
            if (NewGame.Guesses[9] != null) { Result9.DataContext = NewGame.Guesses[9].Result; }
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = (Ellipse)sender;
            BindingExpression bindingExpression = ellipse.GetBindingExpression(Ellipse.FillProperty);
            GuessColor guessColor = (GuessColor)bindingExpression.ResolvedSource;
            guessColor.ToggleColor();
        }
        

        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame.Guesses[NewGame.GuessNumber]= NewGame.CurrentGuess;
            NewGame.Guesses[NewGame.GuessNumber].CalculateResult(NewGame.GameSolution);
            NewGame.CurrentGuess = new Guess();
            SetDataContextGuess();
            SetDataContextResult();
            if (NewGame.Guesses[NewGame.GuessNumber].Correct == true)
            {
                EndGame();
                MessageBox.Show("Congratulations! You have found the correct code.");
            }
            else
            {
                NewGame.GuessNumber = NewGame.GuessNumber + 1;
            }
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            EndGame();
        }

        public void EndGame()
        {
            Solution.Visibility = Visibility.Visible;
            GuessButton.Visibility = Visibility.Hidden;
            ResultButton.Visibility = Visibility.Hidden;
            CurrentGuess.Visibility = Visibility.Hidden;
        }
    }
}
 