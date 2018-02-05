using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eindopdracht_cSharp.Game
{
    class WinChecker
    {
        Coin[,] board;
        private int columns, rows, counter, checkRow, checkCol;
        private GameField game;
        private bool flag, check;

        public WinChecker(GameField gam, int col, int row)
        {
            game = gam;
            columns = col;
            rows = row;
        }

        public bool CheckVertical(Color color)
        {
            flag = true;
            counter = 0;

            while (flag)
            {
                for (int w = 0; w < columns; w++)
                {
                    for (int h = rows - 1; h > 0; h--)
                    {
                        if (board[w, h].color == color)
                        {
                            counter++;
                        }
                        else
                        {
                            counter = 0;
                        }

                        if (counter >= 4)
                        {
                            Console.WriteLine($"Player {color} wins vertical");
                            flag = false;
                            break;
                        }
                    }
                    counter = 0;
                }
                break;
            }
            return flag;
        }

        public bool CheckHorizontal(Color color)
        {
            flag = true;
            counter = 0;

            while (flag)
            {
                for (int h = rows - 1; h > 0; h--)
                {
                    for (int w = 0; w < columns; w++)
                    {
                        if (board[w, h].color == color)
                        {
                            counter++;
                        }
                        else
                        {
                            counter = 0;
                        }

                        if (counter >= 4)
                        {
                            Console.WriteLine($"Player {color} wins horizontal");
                            flag = false;
                            break;
                        }
                    }
                    counter = 0;
                }
                break;
            }
            return flag;
        }

        public Boolean CheckDiagonalForward(Color color)
        {
            flag = true;
            counter = 0;
            checkCol = 1;
            checkRow = 1;
            check = false;

            while (flag)
            {
                for (int w = 0; w < columns; w++)
                {
                    for (int h = rows - 1; h > 0; h--)
                    {
                        if (board[w, h].color == color)
                        {
                            counter++;
                            check = true;
                            while (check)
                            {
                                if (w + checkCol < columns && h - checkRow >= 0)
                                {
                                    if (board[(w + checkCol), (h - checkRow)].color == color)
                                    {
                                        counter++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                checkCol++;
                                checkRow++;

                                if (checkCol == -1 || h - checkRow == -1)
                                {
                                    check = false;
                                    break;
                                }

                                if (counter >= 4)
                                {
                                    Console.WriteLine($"Player {color} wins Diagonal Forward");
                                    check = false;
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if (counter >= 4)
                        {
                            flag = false;
                            break;
                        }

                        counter = 0;
                        checkRow = 1;
                        checkCol = 1;
                    }
                }
                break;
            }
            return flag;
        }

        public bool CheckDiagonalBack(Color color)
        {
            flag = true;
            counter = 0;
            check = false;
            checkRow = 1;
            checkCol = 1;

            while (flag)
            {
                for (int w = 0; w < columns; w++)
                {
                    for (int h = rows - 1; h > 0; h--)
                    {
                        if (board[w, h].color == color)
                        {
                            counter++;
                            check = true;
                            while (check)
                            {
                                if (w - checkCol >= 0 && h - checkRow >= 0)
                                {
                                    if (board[(w - checkCol), (h - checkRow)].color == color)
                                    {
                                        counter++;
                                    }
                                    else
                                    {
                                        counter = 0;
                                        break;
                                    }
                                }
                                checkRow++;
                                checkCol++;

                                if (checkCol == columns || checkRow == rows - 1)
                                {
                                    check = false;
                                }
                                if (counter >= 4)
                                {
                                    Console.WriteLine($"Player {color} wins Diagonal Back");
                                    check = false;
                                    flag = false;
                                }
                            }
                        }
                        counter = 0;
                        checkRow = 0;
                        checkCol = 0;
                    }
                }
                break;
            }
            return flag;
        }

        public bool CheckDraw()
        {
            flag = true;
            foreach (Coin coin in board)
            {
                if (coin.color == Color.White)
                {
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }

        public void run()
        {
            Color red = Color.Red;
            Color Yellow = Color.Yellow;
            while (true)
            {
                board = game.Board;

                if (!CheckVertical(red) || !CheckHorizontal(red) || !CheckDiagonalForward(red) || !CheckDiagonalBack(red))
                {
                    MessageBox.Show($"The winner is {red}");
                    Application.Exit();
                }

                if (!CheckVertical(Yellow) || !CheckHorizontal(Yellow) || !CheckDiagonalForward(Yellow) || !CheckDiagonalBack(Yellow))
                {
                    MessageBox.Show($"The winner is {Yellow}");
                    Application.Exit();
                }

                if (!CheckDraw())
                {
                    MessageBox.Show("It's a draw");
                    Application.Exit();
                }

            }
        }
    }
}
