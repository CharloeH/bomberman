/* Sebastian Horton
 * Friday May 17th, 2019
 * A class that creates, arms and detonates bombs.
 * */
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
    class Bomb
    {
        public bool bombPlaced; //true if a bomb is currently placed but hasnt exploded, (else false).

        static Point bombPos;

        /// <summary>
        /// Authors
        /// Sebastian Horton
        /// creates the bomb.
        /// </summary>
        public Bomb(Point p)
        {
            bombPlaced = true;
            bombPos = new Point();
            bombPos.X = p.X / 64;
            bombPos.Y = p.Y / 64;
            armBomb();
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton
        /// triggers the fuse count down and updates the matrix.
        /// </summary>
        public Point armBomb()
        {

            //MessageBox.Show(bombPos.ToString());
            Matrices.bomb[(int)bombPos.X, (int)bombPos.Y] = 1;
            Map.colourMap(-2);

            return bombPos;
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton
        /// checks if the explosion is within the map and updates the matrix.
        /// </summary>
        public int[,] explosion(Point p)
        {
            int x = (int)bombPos.X;
            int y = (int)bombPos.Y;


            if (x + 1 < 15)
            {
                Matrices.bomb[x + 1, y] = 1;
            }

            if (x - 1 > -1)
            {
                Matrices.bomb[x - 1, y] = 1;
            }

            if (y + 1 < 9)
            {
                Matrices.bomb[x, y + 1] = 1;
            }

            if (y - 1 > -1)
            {
                Matrices.bomb[x, y - 1] = 1;
            }


            Map.colourBombs();

            return Matrices.bomb;
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton
        /// resets the fuse, updates the matrix and redraws the map.
        /// </summary>
        public bool resetBomb()
        {

            Matrices.updateBlocks();
            Matrices.updateWalkable();
            Map.colourMap(-2);
            Matrices.removeBombs();
            return true;
        }

    }
}
