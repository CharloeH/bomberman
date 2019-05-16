/* Sebastion Horton
 * Tuesday, May 14, 2019
 * Class that creates the game
 */
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
using System.Windows.Threading;

namespace BomberMan_2._0
{
    class Game
    {
        Matrices matrix;
        Map map;
        public static int playerDead = 0;

        Player player1;
        Point player1Point;

        Player player2;
        Point player2Point;

        public List<Bomb> bombs;

        public List<Player> players;
        public Game(Canvas c)
        {
            MainWindow.gamestate = MainWindow.GameState.gameOn;
            matrix = new Matrices();
            Matrices.removeBombs();
            map = new Map(c);
            player1Point = new Point(0, 0);
            player1 = new Player(c, 1, player1Point);  //construct player1 in top left corner

            player2Point = new Point(896, 512);
            player2 = new Player(c, 2, player2Point); //construct player2 in bottom right corner

            players = new List<Player>() { player1, player2 };
            bombs = new List<Bomb>();

        }
        public void updateGame()
        {

            player1.updatePlayer(Key.W, Key.S, Key.A, Key.D, true);
            player2.updatePlayer(Key.Up, Key.Down, Key.Left, Key.Right, false);

            player1.placeBomb(Key.LeftShift, players, bombs, playerDead);
            player2.placeBomb(Key.RightCtrl, players, bombs, playerDead);





        }
        /// <summary>
        /// Authors
        /// Sebastian Horton, Logan Ellis
        /// if the player presses the "place" key it will check if this player already has a bomb placed that hasn't exploded.
        /// If there is no bomb placed then it runs the Bomb class' function armBomb.
        /// </summary>


        /// <summary>
        /// Authors
        /// Sebastion Horton
        /// checks if the player is in the blast radius
        /// </summary>
        public static bool isPlayerDead(Point p)
        {

            if (Matrices.bomb[(int)p.X / 64, (int)p.Y / 64] == 1)
            {
                return true;
            }
            else
                return false;
        }
    }
}
