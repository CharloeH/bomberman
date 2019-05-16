/* Sebastian Horton, Ethan Shipston
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
        int bombFuse;
        Point bombPos;

        /// <summary>
        /// Authors
        /// Sebastian Horton
        /// creates the bomb.
        /// </summary>
        public Bomb(Point p)
        {
            bombFuse = 10;
            bombPos = new Point();
            bombPos.X = p.X / 64;
            bombPos.Y = p.Y / 64;

            Matrices.bomb[(int)bombPos.X, (int)bombPos.Y] = 1;
            Map.colourMap();
        }

        public void updateBomb(bool bombPlaced)
        {
            explosion();
            resetBomb(bombPlaced);
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton, Ethan Shipston
        /// checks if the explosion is within the map and updates the matrix, as well as updating the bomb animation.
        /// </summary>
        public int[,] explosion()
        {

            int x = (int)bombPos.X;
            int y = (int)bombPos.Y;
            if (bombFuse == 10)
                Map.colourBombs(bombFuse);

            if (bombFuse == 7)
                Map.colourBombs(bombFuse);

            if (bombFuse == 4)
                Map.colourBombs(bombFuse);

            if (bombFuse == 1)
                Map.colourBombs(bombFuse);

            if (bombFuse == 0)
            {
                Matrices.bomb[x, y] = 2;

                if (x + 1 < 15)
                {
                    Matrices.bomb[x + 1, y] = 2;
                }

                if (x - 1 > -1)
                {
                    Matrices.bomb[x - 1, y] = 2;
                }

                if (y + 1 < 9)
                {
                    Matrices.bomb[x, y + 1] = 2;
                }

                if (y - 1 > -1)
                {
                    Matrices.bomb[x, y - 1] = 2;
                }
                Map.colourBombs(bombFuse);
            }
            bombFuse--;
            return Matrices.bomb;
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton
        /// resets the fuse, updates the matrix and redraws the map.
        /// </summary>
        public bool resetBomb(bool bombPlaced)
        {
            if (bombFuse <= -2)
            {
                Matrices.updateBlocks();
                Matrices.updateWalkable();
                Matrices.removeBombs();
                Map.colourMap();

                return bombPlaced = false;
            }
            else
                return bombPlaced = true;
        }
    }
}