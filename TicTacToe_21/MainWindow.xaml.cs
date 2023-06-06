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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TicTacToe_21
{
    public partial class MainWindow : Window
    {
        private char currentPlayer;
        private char[] board;
        private Random random;

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            currentPlayer = 'X';
            board = new char[9];
            random = new Random();

            foreach (Button button in gameBoard.Children)
            {
                button.Content = string.Empty;
                button.IsEnabled = true;
            }

            resultLabel.Content = string.Empty;
            gameBoard.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = gameBoard.Children.IndexOf(button);

            button.Content = currentPlayer;
            button.IsEnabled = false;
            board[index] = currentPlayer;

            if (CheckWin(currentPlayer))
            {
                resultLabel.Content = $"{currentPlayer} wins!";
                gameBoard.IsEnabled = false;
            }
            else if (IsBoardFull())
            {
                resultLabel.Content = "It's a draw!";
                gameBoard.IsEnabled = false;
            }
            else
            {
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                if (currentPlayer == 'O')
                    RobotMove();
            }
        }

        private bool CheckWin(char player)
        {
            // Check rows
            for (int i = 0; i < 9; i += 3)
            {
                if (board[i] == player && board[i + 1] == player && board[i + 2] == player)
                    return true;
            }

            // Check columns
            for (int i = 0; i < 3; i++)
            {
                if (board[i] == player && board[i + 3] == player && board[i + 6] == player)
                    return true;
            }

            // Check diagonals
            if ((board[0] == player && board[4] == player && board[8] == player) ||
                (board[2] == player && board[4] == player && board[6] == player))
            {
                return true;
            }

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (char cell in board)
            {
                if (cell == '\0')
                    return false;
            }
            return true;
        }

        private void RobotMove()
        {
            int index;
            do
            {
                index = random.Next(9);
            }
            while (board[index] != '\0');

            Button button = (Button)gameBoard.Children[index];
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
