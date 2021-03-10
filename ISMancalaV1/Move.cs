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

        
        public Move DeepCopy()
        {
            Move newMove = new Move(this.y, this.x, this.score);
            return newMove;
        }


        public Move[] MoveArray2(Board board)
        {
            Console.WriteLine("Movearray2 active");
            int i,counter;
            int depth = 2;
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
                    for (i = 0; i < moveCount; i++)
                    {
                        if (board.getItemsInSpot()[1, i] > 0)
                        {
                            Board board1 = board.DeepCopy();

                            board1.Movement(1, i);
                            score = -1;
                            score = MinMax(depth, board1.DeepCopy(), true);
                            if (score < 0)
                            {
                                counter = 0;
                                while (score < 0 &&counter<5)
                                {
                                    counter++;
                                    score = MinMax(depth + counter, board1.DeepCopy(), true);
                                }
                            }
                            
                            
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
                            score = 1;
                            score = MinMax(depth, board1.DeepCopy(),false);
                            if (score > 0)
                            {
                                counter = 0;
                                while (score > 0 && counter<5)
                                {
                                    counter++;
                                    score = MinMax(depth + counter, board1.DeepCopy(), false);
                                }
                            }
                            moveList[moveArrayPlace] = new Move(0, i, score);
                            moveArrayPlace++;
                        }
                    }
                    Console.WriteLine("movearray2 closed");

                    return moveList;
                }
                Console.WriteLine("movearray2 closed");

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
            Console.WriteLine("Getbestmove openned");
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

            Console.WriteLine("getbestmove closed");
            return bestMove;

        }



    }
}
