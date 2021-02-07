using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMancalaV1
{
    public class Move: Form1
    {
        public int score;
        public int x;
        public int y;

        public Move(int y = 0, int x = 0, int score = 1)
        {
            this.x = x;
            this.y = y;
            this.score = score;
        }

        public int GetScore()
        {
            return this.score;
        }
        public Move DeepCopy()
        {
            Move newMove = new Move(this.y, this.x, this.score);
            return newMove;
        }


        public Move[] MoveArray2(Board board)
        {
            int i;
            int depth = 5;
            int score, moveArrayPlace = 0;
            int moveCount = 0;
            if (board.GetTurn())
            {
                for (i = 0; i < 6; i++)
                {

                    if (board.getItemsInSpot()[1, i] > 0)
                    {
                        moveCount++;
                    }
                }
                if (moveCount > 0)
                {
                    Move[] moveList = new Move[moveCount];
                    for (i = 0; i < 6; i++)
                    {
                        if (board.getItemsInSpot()[1, i] > 0)
                        {
                            Board board1 = board.DeepCopy();

                            board1.Movement(1, i);
                            score = MinMax(depth, board1.DeepCopy());
                            moveList[moveArrayPlace] = new Move(1, i, score);
                            moveArrayPlace++;
                        }
                    }
                    return moveList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                for (i = 0; i < 6; i++)
                {

                    if (board.getItemsInSpot()[0, i] > 0)
                    {
                        moveCount++;
                    }
                }
                if (moveCount > 0)
                {
                    Move[] moveList = new Move[moveCount];
                    for (i = 0; i < 6; i++)
                    {
                        if (board.getItemsInSpot()[0, i] > 0)
                        {
                            Board board1 = board.DeepCopy();

                            board1.Movement(0, i);

                            score = MinMax(depth, board1.DeepCopy());
                            moveList[moveArrayPlace] = new Move(0, i, score);
                            moveArrayPlace++;
                        }
                    }
                    return moveList;
                }
                return null;

            }


        }

        public Move[] MoveArray(Board board)
        {
            int i;
            int score, moveArrayPlace = 0;
            int moveCount = 0;
            if (board.GetTurn())
            {
                for (i = 0; i < 6; i++)
                {

                    if (board.getItemsInSpot()[1, i] > 0)
                    {
                        moveCount++;
                    }
                }

                if (moveCount > 0)
                {
                    Move[] moveList = new Move[moveCount];
                    for (i = 0; i < 6; i++)
                    {
                        if (board.getItemsInSpot()[1, i] > 0)
                        {
                            Board board1 = board.DeepCopy();

                            board1.Movement(1, i);
                            if (board1.GetTurn())
                            {
                                score = 100;
                                moveList[moveArrayPlace] = new Move(1, i, score);
                            }
                            else
                            {
                                if (board1.GetPlayerPoints() > board.GetPlayerPoints() + 1 && board1.GetPcPoints() == board.GetPcPoints())
                                {
                                    score = 75;
                                    moveList[moveArrayPlace] = new Move(1, i, score);
                                }
                                else
                                {
                                    if (board1.GetPlayerPoints() > board.GetPlayerPoints())
                                    {
                                        score = 50;
                                        moveList[moveArrayPlace] = new Move(1, i, score);
                                    }
                                    else
                                    {
                                        score = 0;
                                        moveList[moveArrayPlace] = new Move(1, i, score);
                                    }
                                }
                            }
                            moveArrayPlace++;
                        }
                    }
                    return moveList;

                }
                else
                {
                    return null;
                }


            }
            else
            {
                for (i = 0; i < 6; i++)
                {

                    if (board.getItemsInSpot()[0, i] > 0)
                    {
                        moveCount++;
                    }
                }
                if (moveCount > 0)
                {
                    Move[] moveList = new Move[moveCount];
                    for (i = 0; i < 6; i++)
                    {
                        if (board.getItemsInSpot()[0, i] > 0)
                        {
                            Board board1 = board.DeepCopy();

                            board1.Movement(0, i);
                            if (!board1.GetTurn())
                            {
                                score = 100;
                                moveList[moveArrayPlace] = new Move(0, i, score);
                            }
                            else
                            {
                                if (board1.GetPcPoints() > board.GetPcPoints() + 1 && board1.GetPlayerPoints() == board.GetPlayerPoints())
                                {
                                    score = 75;
                                    moveList[moveArrayPlace] = new Move(0, i, score);
                                }
                                else
                                {
                                    if (board1.GetPcPoints() > board.GetPcPoints())
                                    {
                                        score = 50;
                                        moveList[moveArrayPlace] = new Move(0, i, score);
                                    }
                                    else
                                    {
                                        score = 0;
                                        moveList[moveArrayPlace] = new Move(0, i, score);
                                    }
                                }
                            }
                            moveArrayPlace++;
                        }
                    }
                    return moveList;
                }
                return null;

            }


        }
        public int getScore()
        {
            return this.score;
        }
        public int getX()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }

        public Move GetBestMove(Move[] moveArray)
        {
            Move bestMove = null;
            if (moveArray != null)
            {
                bestMove = moveArray[0].DeepCopy();
                if (moveArray.Length > 1)
                {
                    for (int i = 1; i < moveArray.Length; i++)
                    {
                        if (bestMove.getScore() < moveArray[i].getScore())
                        {
                            bestMove = moveArray[i].DeepCopy();
                        }
                    }
                }
            }

            return bestMove;
        }



    }
}
