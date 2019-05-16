/*
 * Sebastian Horton, Logan Ellis, Ethan Shipston
 * Friday May 17th, 2019
 * A class that controls the players movements and interacts with the bomb class to place bombs.
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BomberMan_2._0
{

    class Player
    {

        //class-wide variables:
        Point playerPoint;
        Rectangle playerRectangle;
        public bool bombPlaced;
        Key left, right, down, up, place;
        Bomb bomb;
        /// <summary>
        /// Authors
        /// Sebastian Horton, Logan Ellis
        /// creates a player rectangle with a hieght, width, colour and position on the canvas.
        /// </summary>
        public Player(Canvas c, ImageBrush sprite, Point p, Key u, Key d, Key l, Key r, Key pl)
        {
            up = u;
            down = d;
            left = l;
            right = r;
            place = pl;

            playerPoint.X = p.X;
            playerPoint.Y = p.Y;
            playerRectangle = new Rectangle();

            playerRectangle.Height = 64;
            playerRectangle.Width = 64;
            playerRectangle.Fill = sprite;



            Canvas.SetTop(playerRectangle, playerPoint.Y);
            Canvas.SetLeft(playerRectangle, playerPoint.X);
            c.Children.Add(playerRectangle);
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton, Logan Ellis
        /// Updates the player after they take an action (place a bomb or move).
        /// </summary>
        public Point updatePlayer(int i)
        {
            movePlayer(i);
            
            placeBomb();
            if (bombPlaced == true)
            {
                bomb.updateBomb(bombPlaced);
                bombPlaced = bomb.resetBomb(bombPlaced);
            }
            if (isPlayerDead() == true)
            {
                MainWindow.playerNumber = i;
                MainWindow.gamestate = MainWindow.GameState.gameOver;
            }
            Canvas.SetTop(playerRectangle, playerPoint.Y);
            Canvas.SetLeft(playerRectangle, playerPoint.X);

            Console.WriteLine(bombPlaced.ToString());

            return playerPoint;
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton, Logan Ellis, Ethan Shipston
        /// takes the players input based on their controls and makes sure that they're within the map.
        /// </summary>
        private void movePlayer(int i)
        {
            if (Keyboard.IsKeyDown(up) && playerPoint.Y > 0)
            {
                if (checkPlayer(0, -1) == true) //the player would move up one square in the y direction (y -1).
                {
                    if (i == 1)
                        playerRectangle.Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Player1back.png", UriKind.Relative)));
                    else
                        playerRectangle.Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Player2back.png", UriKind.Relative)));
                    playerPoint.Y -= 64;
                    return;
                }
            }
            else if (Keyboard.IsKeyDown(down) && playerPoint.Y < 512)
            {
                if (checkPlayer(0, 1) == true) //the player would move down one square in the y direction (y + 1).
                {
                    if (i == 1)
                        playerRectangle.Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Player1forward.png", UriKind.Relative)));
                    else
                        playerRectangle.Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Player2forward.png", UriKind.Relative)));
                    playerPoint.Y += 64;
                    return;
                }
            }
            else if (Keyboard.IsKeyDown(right) && playerPoint.X < 896)
            {
                if (checkPlayer(1, 0) == true) //the player would increase their x position by one (x + 1).
                {
                    if (i == 1)
                        playerRectangle.Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Player1right.png", UriKind.Relative)));
                    else
                        playerRectangle.Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Player2right.png", UriKind.Relative)));
                    playerPoint.X += 64;
                    return;
                }
            }
            else if (Keyboard.IsKeyDown(left) && playerPoint.X > 0)
            {
                if (checkPlayer(-1, 0) == true) //the player would decrease their x position by one (x - 1).
                {
                    if (i == 1)
                        playerRectangle.Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Player1left.png", UriKind.Relative)));
                    else
                        playerRectangle.Fill = new ImageBrush(new BitmapImage(new Uri("Sprites/Player2left.png", UriKind.Relative)));
                    playerPoint.X -= 64;
                    return;
                }

            }
        }


        /// <summary>
        /// Authors
        /// Sebastian Horton, Logan Ellis
        /// takes the players future position and checks what type of square it is (block, pillar or walkable).
        /// </summary>
        private bool checkPlayer(int offsetX, int offsetY)
        {
            int playerPosX = ((int)playerPoint.X / 64);
            int playerPosY = ((int)playerPoint.Y / 64);
            int futureValueX = playerPosX + offsetX;
            int futureValueY = playerPosY + offsetY;

            //the player's cordinates on the grid must be within (0-14, 0-9).
            if (futureValueX <= 0)
            {
                futureValueX = 0;
            }
            else if (futureValueX > 14)
            {
                futureValueX = 14;
            }
            if (futureValueY <= 0)
            {
                futureValueY = 0;
            }
            else if (futureValueY > 9)
            {
                futureValueY = 9;
            }

            if (Matrices.walkable[futureValueX, futureValueY] == 1)
            {
                return true;
            }
            else
                return false;



        }

        //returns the player's position
        public Point getPlayerPos()
        {
            return playerPoint;
        }

        public void placeBomb()
        {

            if (Keyboard.IsKeyDown(place) && bombPlaced == false)
            {
                bombPlaced = true;
                bomb = new Bomb(getPlayerPos());
            }
        }

        public bool isPlayerDead()
        {
            if (Matrices.bomb[(int)playerPoint.X / 64, (int)playerPoint.Y / 64] == 2)
            {
                return true;
            }
            else
                return false;
        }
    }
}
