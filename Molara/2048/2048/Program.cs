﻿using System.ComponentModel.DataAnnotations;

namespace _2048
{
    internal class Program
    {
        static int[,] map = new int[4, 4];
        static Random rnd = new Random();

        static ConsoleColor[] colors = {ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.DarkMagenta, ConsoleColor.DarkGreen, ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.DarkYellow, ConsoleColor.Magenta, ConsoleColor.White};

        enum directions
        {
            up,
            down,
            right,
            left
        }

        static void move(directions direction)
        {
            if (direction == directions.up)
            {
                for (int i = 0; i < 4; i++)
                {
                    int free = 0, last = 0;

                    for (int j = 0; j < 4; j++)
                        if (map[j, i] != 0)
                        {
                            if (j != 0)
                            {
                                if (map[j, i] == map[last, i])
                                {
                                    map[last, i] *= 2;
                                    map[j, i] = 0;
                                }
                                else
                                {
                                    if(free != j)
                                    {
                                        map[free, i] = map[j, i];
                                        map[j, i] = 0;
                                    }
                                    last = free++;
                                }
                            }
                            else free++;
                                
                        }
                }
            }
            else if (direction == directions.down)
            {
                for (int i = 0; i < 4; i++)
                {
                    int free = 3, last = 3;

                    for (int j = 3; j >= 0; j--)
                        if (map[j, i] != 0 )
                        {
                            if (j != 3)
                            {
                                if (map[j, i] == map[last, i])
                                {
                                    map[last, i] *= 2;
                                    map[j, i] = 0;
                                }
                                else
                                {
                                    if(free != j)
                                    {
                                        map[free, i] = map[j, i];
                                        map[j, i] = 0;
                                    }
                                    last = free--;
                                }
                            }
                            else free--;
                                
                            
                        }
                }
            }
            else if (direction == directions.left)
            {
                for (int i = 0; i < 4; i++)
                {
                    int free = 0, last = 0;

                    for (int j = 0; j < 4; j++)
                        if (map[i, j] != 0)
                        {
                            if (j != 0)
                            {
                                if (map[i, j] == map[i, last])
                                {
                                    map[i, last] *= 2;
                                    map[i, j] = 0;
                                }
                                else
                                {
                                    if (free != j)
                                    {
                                        map[i, free] = map[i, j];
                                        map[i, j] = 0;
                                    }
                                    last = free++;
                                }
                            }
                            else free++;
                                
                        }
                }
            }
            else if (direction == directions.right)
            {
                for (int i = 0; i < 4; i++)
                {
                    int free = 3, last = 3;

                    for (int j = 3; j >= 0; j--)
                        if (map[i, j] != 0)
                        {
                            if (j != 3)
                            {
                                if (map[i, j] == map[i, last])
                                {
                                    map[i, last] *= 2;
                                    map[i, j] = 0;
                                }
                                else
                                {
                                    if(free != j)
                                    {
                                        map[i, free] = map[i, j];
                                        map[i, j] = 0;
                                    }
                                    last = free--;
                                }
                            }
                            else free--;

                        }
                }
            }
            else throw new Exception("Invalid direction.");
        }

        static void printMap()
        {
            Console.Write("╔══════╦══════╦══════╦══════╗");

            for (int i = 0; i < map.GetLength(1); i++)
            {
                Console.Write("\n║");
                for(int j = 0; j < map.GetLength(0); j++)
                {
                    if (map[i, j] != 0)
                        Console.ForegroundColor = colors[(int)Math.Log2(map[i, j])];
                    else Console.ForegroundColor = colors[0];
                    Console.Write($" {toBuffer(map[i, j])} ");
                    Console.ForegroundColor = ConsoleColor.White; 
                    Console.Write("║");
                        
                }
                if (i != map.GetLength(1) - 1)
                    Console.Write("\n╠══════╬══════╬══════╬══════╣");

            }

            Console.WriteLine("\n╚══════╩══════╩══════╩══════╝");
        }

        static void addNumber()
        {
            bool space_available = false;
            for(int i = 0; i < map.GetLength(0);i++)
                for(int j = 0; j< map.GetLength(1); j++)
                    if (map[i, j] == 0) { space_available = true; break; }
            if (space_available)
                while (true)
                {
                    int col = rnd.Next(0, 4);
                    int line = rnd.Next(0, 4);

                    if (map[line, col] == 0)
                    {
                        map[line, col] = 2;
                        break;
                    }
                }
            else throw new Exception("Not enough space in the map.");
        }

        static string toBuffer(int num)
        {
            string numero = num.ToString();
            int length = numero.Length;

            if(length != 4)
                for(int i = 0; i < 4 - length; i++) 
                    if(i % 2 == 0)
                        numero = " " + numero;
                    else numero = numero + " ";

            return numero;
        }
        static void Main(string[] args)
        {
            while(true)
            {
                printMap();
                directions direction;
                ConsoleKey input = Console.ReadKey().Key;
                if (input == ConsoleKey.RightArrow) direction = directions.right;
                else if (input == ConsoleKey.LeftArrow) direction = directions.left;
                else if (input == ConsoleKey.UpArrow) direction = directions.up;
                else if(input == ConsoleKey.DownArrow) direction = directions.down;
                else direction = directions.up;

                move(direction);
                try
                {
                    addNumber();
                } catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tGame Over!");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                
            }
        }
    }
}