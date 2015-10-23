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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private GameOptions _gameOptions;

        public GameOptions GameOptions
        {
            get
            {
                return _gameOptions;
            }
            set
            {
                _gameOptions = value;
            }
        }

        public OptionsWindow()
        {
            _gameOptions = new GameOptions();
            DataContext = _gameOptions;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(GameOptions);
            gameWindow.Show();
            this.Close();
        }
    }
}
