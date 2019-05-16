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
        public int bombFuse;
        /// <summary>
        /// Authors
        /// Sebastian Horton, Logan Ellis, Ethan Shipston
        /// creates a player rectangle with a hieght, width, colour and position on the canvas.
        /// </summary>
        public Player(Canvas c, int i, Point p)
        {
            playerPoint.X = p.X;
            playerPoint.Y = p.Y;
            playerRectangle = new Rectangle();

            playerRectangle.Height = 64;
            playerRectangle.Width = 64;
            if (i == 1)
            {
                playerRectangle.Fill = drawPlayer(Key.S);
            }
            else
            {
                playerRectangle.Fill = drawPlayer(Key.Down);
            }

            Canvas.SetTop(playerRectangle, playerPoint.Y);
            Canvas.SetLeft(playerRectangle, playerPoint.X);
            c.Children.Add(playerRectangle);
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton, Logan Ellis
        /// Updates the player after they take an action (place a bomb or move).
        /// </summary>
        public Point updatePlayer(Key up, Key down, Key left, Key right, bool player1)
        {
            movePlayer(up, down, left, right);
            Canvas.SetTop(playerRectangle, playerPoint.Y);
            Canvas.SetLeft(playerRectangle, playerPoint.X);

            return playerPoint;
        }
        /// <summary>
        /// Authors: 
        /// Ethan Shipston 
        /// Updates the player sprite to reflect direction.
        /// </summary>
        /// <param name="dir">Directional key being pressed.</param>
        /// <returns></returns>
        private ImageBrush drawPlayer(Key dir)
        {
            ImageBrush playerBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Player1forward.png", UriKind.Relative)));

            //player1
            if (dir == Key.W)
            {
                playerBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Player1back.png", UriKind.Relative)));
            }
            if (dir == Key.S)
            {
                playerBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Player1forward.png", UriKind.Relative)));
            }
            if (dir == Key.A)
            {
                playerBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Player1left.png", UriKind.Relative)));
            }
            if (dir == Key.D)
            {
                playerBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Player2right.png", UriKind.Relative)));
            }
            //player2
            if (dir == Key.Up)
            {
                playerBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Player2back.png", UriKind.Relative)));
            }
            if (dir == Key.Down)
            {
                playerBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Player2forward.png", UriKind.Relative)));
            }
            if (dir == Key.Left)
            {
                playerBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Player2left.png", UriKind.Relative)));
            }
            if (dir == Key.Right)
            {
                playerBrush = new ImageBrush(new BitmapImage(new Uri("Sprites/Player2right.png", UriKind.Relative)));
            }

            return playerBrush;

        }
        /// <summary>
        /// Authors
        /// Sebastian Horton, Logan Ellisl, Ethan Shipston
        /// takes the players input based on their controls and makes sure that they're within the map.
        /// </summary>
        private void movePlayer(Key up, Key down, Key left, Key right)
        {
            if (Keyboard.IsKeyDown(up) && playerPoint.Y > 0)
            {
                if (checkPlayer(0, -1) == true) //the player would move up one square in the y direction (y -1).
                {
                    playerPoint.Y -= 64;
                    return;
                }
            }
            else if (Keyboard.IsKeyDown(down) && playerPoint.Y < 512)
            {
                if (checkPlayer(0, 1) == true) //the player would move down one square in the y direction (y + 1).
                {
                    playerPoint.Y += 64;
                    return;
                }
            }
            else if (Keyboard.IsKeyDown(right) && playerPoint.X < 896)
            {
                if (checkPlayer(1, 0) == true) //the player would increase their x position by one (x + 1).
                {
                    playerPoint.X += 64;
                    return;
                }
            }
            else if (Keyboard.IsKeyDown(left) && playerPoint.X > 0)
            {
                if (checkPlayer(-1, 0) == true) //the player would decrease their x position by one (x - 1).
                {
                    playerPoint.X -= 64;
                    return;
                }

            }
        }


        /// <summary>
        /// Authors
        /// Sebastian Horton, Logan Ellis, Ethan Shipston
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
        public bool placeBomb(Key place, List<Player> players, List<Bomb> bombs, int playerDead)
        {
            if (bombPlaced == false)
            {
                if (Keyboard.IsKeyDown(place))
                {
                    bombs.Add(new Bomb(getPlayerPos()));
                    bombFuse = 10;
                    return bombPlaced = true;
                }
            }
            foreach (Bomb b in bombs)
            {

                if (bombPlaced == true && bombFuse >= -2)
                {
                    bombFuse--;

                    if (bombFuse == 8)
                    {
                        Map.colourMap(3);
                    }
                    if (bombFuse == 6)
                    {
                        Map.colourMap(2);
                    }
                    if (bombFuse == 4)
                    {
                        Map.colourMap(1);
                    }
                    if (bombFuse == 2)
                    {
                        Map.colourMap(0);
                    }

                    if (bombFuse == 0)
                    {
                        b.explosion(getPlayerPos());
                        playerDead = 1;
                        foreach (Player pl in players)
                        {
                            if (Game.isPlayerDead(pl.getPlayerPos()) == true)
                            {
                                if (playerDead == 1)
                                {
                                    Menu.playerNumber = "2";
                                }
                                else
                                    Menu.playerNumber = "1";

                                MainWindow.gamestate = MainWindow.GameState.gameOver;
                            }
                            playerDead++;

                        }
                        return bombPlaced = true;
                    }
                    else if (bombFuse == -2)
                    {
                        b.resetBomb();
                        return bombPlaced = false;
                    }
                    return bombPlaced = true;
                }
                else
                    return bombPlaced = false;
            }
            return false;
        }
    }
}
