/* Sebastian Horton
 * Friday May 17th, 2019
 * A class that creates matrices and uses logic to mech and update them.
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace BomberMan_2._0
{
    class Matrices
    {
        //declares the matrices for the walkable tiles (walkable), bomb tiles (bomb), unbreakable tiles (pillars) and breakable tiles (blocks).

        //a grid that all of the matrices are drawn on.
        public static Rectangle[,] map;

        //a compound mesh of the matrices for the blocks and pillars.
        public static int[,] walkable;

        //adds a bomb to the players position when the use the "place" key and, after 1.5 seconds, detonates in every cardinal direction one tile. 
        //Afterwords it is removed to reset the bombs position.
        public static int[,] bomb;
        //a matrix containing all of the indestructable blocks or "pillars" (cannot be changed).
        public static int[,] pillars;
        //a matrix containing all of the destructable blocks at the start of the game (can be updated).
        public static int[,] blocks;
        public Matrices()
        {
            blocks = new int[,]{ { 0, 0, 1, 1, 1, 1, 1, 1, 1},
                                 { 0, 0, 1, 0, 1, 0, 1, 0, 1},
                                 { 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                 { 1, 0, 1, 0, 1, 0, 1, 0, 1},
                                 { 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                 { 1, 0, 1, 0, 1, 0, 1, 0, 1},
                                 { 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                 { 1, 0, 1, 0, 1, 0, 1, 0, 1},
                                 { 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                 { 1, 0, 1, 0, 1, 0, 1, 0, 1},
                                 { 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                 { 1, 0, 1, 0, 1, 0, 1, 0, 1},
                                 { 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                 { 1, 0, 1, 0, 1, 0, 1, 0, 0},
                                 { 1, 1, 1, 1, 1, 1, 1, 0, 0} };

            pillars = new int[,]{ { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                  { 0, 1, 0, 1, 0, 1, 0, 1, 0},
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                  { 0, 1, 0, 1, 0, 1, 0, 1, 0},
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                  { 0, 1, 0, 1, 0, 1, 0, 1, 0},
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                  { 0, 1, 0, 1, 0, 1, 0, 1, 0},
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                  { 0, 1, 0, 1, 0, 1, 0, 1, 0},
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                  { 0, 1, 0, 1, 0, 1, 0, 1, 0},
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                  { 0, 1, 0, 1, 0, 1, 0, 1, 0},
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0} };

            bomb = new int[15, 9];
            walkable = new int[15, 9];
            map = new Rectangle[15, 9];
        }

        ///<summary>
        /// Authors
        /// Sebastian Horton
        ///loops through all of the values in a 15,9 grid and checks for pillars or blocks. 
        ///If any are found a 0 is placed in the same cordinate in the walkable matrix. 
        ///Otherwise, a 1 is placed at that cordinate in the walkable matrix.
        ///<summary>
        public static int[,] updateWalkable()
        {
            //values are stored in length width(y,x).
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 15; y++)
                {

                    if (pillars[y, x] == 0 && blocks[y, x] == 0 && walkable[y, x] != 1)
                    {
                        walkable[y, x] = 1;
                    }
                    if (bomb[y, x] == 1 && walkable[y, x] == 1)
                    {
                        bomb[y, x] = 0;
                    }
                }
            }
            return walkable;
        }

        /// <summary>
        /// Authors
        /// Sebastian Horton
        /// removes blocks that are the same cordinates and bombs.
        /// </summary>
        public static int[,] updateBlocks()
        {

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    if (bomb[y, x] == 1 || blocks[y, x] == 0)
                    {
                        blocks[y, x] = 0;
                    }
                    else
                        blocks[y, x] = 1;

                }
            }
            return blocks;
        }
        public static int[,] removeBombs()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    bomb[y, x] = 0;
                }
            }
            return bomb;
        }

    }
}
