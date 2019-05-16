/*Sebastian Horton, Elliot McArthur, Ethan Shipston
 * Friday May 17th, 2019
 * A class that draws a grid of rectangles based on a set of matrices.
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BomberMan_2._0
{

    class Map
    {
        /// <summary>
        /// Authors
        /// Sebastian Horton, Elliot McArthur
        ///constructs the map in the mainwindow.
        ///Also meshes the "pillars" and "blocks" matrices and sets the walkable space at the start of the game.
        /// </summary>
        public Map(Canvas c)
        {

            Matrices.updateBlocks();
            Matrices.updateWalkable();

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    Matrices.map[y, x] = new Rectangle();
                    Matrices.map[y, x].Height = 64;
                    Matrices.map[y, x].Width = 64;



                    Canvas.SetTop(Matrices.map[y, x], 64 * x);
                    Canvas.SetLeft(Matrices.map[y, x], 64 * y);
                    c.Children.Add(Matrices.map[y, x]);

                }
            }
            colourMap(-2);
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton, Elliott McArthur, Ethan Shipston
        /// updates the map colours based on the position of the blocks, pillars and walkable space.
        /// </summary>
        public static void colourMap(int i)
        {
            ImageBrush bombBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Bomb4.png", UriKind.Relative)));
            if (i != -2)
            {
                if (i == 3)
                {
                    bombBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Bomb4.png", UriKind.Relative)));
                }
                if (i == 2)
                {
                    bombBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Bomb3.png", UriKind.Relative)));
                }
                if (i == 1)
                {
                    bombBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Bomb2.png", UriKind.Relative)));
                }
                if (i == 0)
                {
                    bombBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Bomb1.png", UriKind.Relative)));
                }
            }
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    if (Matrices.pillars[y, x] == 1)
                    {
                        Matrices.map[y, x].Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Solid Wall.png", UriKind.Relative)));
                    }
                    else if (Matrices.blocks[y, x] == 1)
                    {
                        if (Matrices.bomb[y, x] == 1)
                        {
                            Matrices.map[y, x].Fill = bombBrush;
                        }
                        else
                            Matrices.map[y, x].Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Soft Wall.png", UriKind.Relative)));
                    }
                    else if (Matrices.walkable[y, x] == 1)
                    {
                        if (Matrices.bomb[y, x] == 1)
                        {
                            Matrices.map[y, x].Fill = bombBrush;
                        }
                        else
                            Matrices.map[y, x].Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Floor.png", UriKind.Relative)));
                    }

                }
            }
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton, Elliott McArthur
        /// draws the bomb and it's blast radius.
        /// </summary>
        public static void colourBombs()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    if (Matrices.bomb[y, x] == 1 && Matrices.pillars[y, x] == 0)
                    {
                        Matrices.map[y, x].Fill = Brushes.Green;
                    }
                }
            }
        }

    }
}
