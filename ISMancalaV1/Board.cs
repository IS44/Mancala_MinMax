using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMancalaV1
{
    public class Board
    {
        public int playerPoints; //player points are banked here
        public int pcPoints; // pc points are banked here
        public int[,] itemsInSpot = new int[2, 6]; //the points or items are banked in each spot in this array
        public Boolean turn; //true means this is the PC's turn, false means this is player's turn

        public Board()
        {
            for (int i = 0; i < 6; i++)
            {
                this.itemsInSpot[0, i] = 4;
                this.itemsInSpot[1, i] = 4;
            }
            this.pcPoints = 0;
            this.playerPoints = 0;
            this.turn = true;
        }

        public Board(int[,] itemsInSpot, int pcPoints, int playerPoints, Boolean turn)
        {
            this.itemsInSpot = itemsInSpot;
            this.pcPoints = pcPoints;
            this.playerPoints = playerPoints;
            this.turn = turn;
        }

        
        public Board DeepCopy()
        {
           

            Board board1 = new Board(this.getItemsInSpot(), this.pcPoints, this.playerPoints, this.turn);
            
            return board1;
        }

        public int[,] getItemsInSpot()
        {
            int[,] itemsInSpotCopy = this.itemsInSpot.Clone() as int[,];
            return itemsInSpotCopy;

        }
        

        

        public void restartBoard() //resets the board
        {
            for (int i = 0; i < 6; i++)
            {
                this.itemsInSpot[0, i] = 4;
                this.itemsInSpot[1, i] = 4;
                this.pcPoints = 0;
                this.playerPoints = 0;
            }
        }

        public Boolean IsFinal() //Checks if the game is finished - if there are no more items in every place other than the player's or computer's last store
        {   if(this.pcPoints>24||this.playerPoints>24)
            {
                return true;
            }
            return (this.pcPoints + this.playerPoints == 48);

        }

        public int GetPcPoints()
        {
            return this.pcPoints;
        }

        public int GetPlayerPoints()
        {
            return this.playerPoints;
        }

        public Boolean GetTurn()
        {
            return this.turn;
        }
        
        public String PrintInfo()
        {
            int[,] itemsInSpot1 = this.getItemsInSpot();
            String string1 = "";
            for (int i=0; i<2; i++)
            {
                for (int j = 0; j < 6; j++)
                    string1 = string1 +  + itemsInSpot1[i, j]+ ",";
            }
            return string1 ;
        }

        public void Movement(int yLoc, int xLoc) //movement of the items is made in this program
        {

            int itemSpot = this.itemsInSpot[yLoc, xLoc];
            itemsInSpot[yLoc, xLoc] = 0;
            while (itemSpot > 0)
            {
                if (itemSpot == 1 && this.turn == false && xLoc == 5 && yLoc == 0) //adds the point to the computer and gives the player his turn
                {
                    this.pcPoints++;
                    this.turn = true;
                    itemSpot--;
                }
                else
                {
                    if (itemSpot == 1 && this.turn == true && xLoc == 0 && yLoc == 1) //changes the points according to the rules
                    {
                        this.playerPoints++;
                        this.turn = false;
                        itemSpot--;
                    }
                    else
                    {
                        if (itemSpot == 1 && this.turn == false && yLoc == 0 && xLoc != 5 && this.itemsInSpot[1, xLoc + 1] > 0 && this.itemsInSpot[0, xLoc + 1] == 0) //rule in mancala if the bin opposite to the player's bin is not empty and the player's is empty and it's the last item so all the two bins go the player's points
                        {
                            xLoc++;
                            this.pcPoints = this.pcPoints + this.itemsInSpot[1, xLoc] + 1;
                            itemSpot--;
                            itemsInSpot[1, xLoc] = 0;
                        }
                        else
                        {
                            if (itemSpot == 1 && this.turn == true && yLoc == 1 && xLoc != 0 && this.itemsInSpot[1, xLoc - 1] == 0 && this.itemsInSpot[0, xLoc - 1] > 0) //rule in mancala if the bin opposite to the computer's bin is not empty and the computer's is empty and it's the last item so all the two bins go the computer's points
                            {
                                xLoc--;
                                this.playerPoints = this.playerPoints + this.itemsInSpot[0, xLoc] + 1;
                                this.itemsInSpot[0, xLoc] = 0;
                                itemSpot--;
                            }
                            else
                            {
                                if (yLoc == 1 && itemSpot > 0 && xLoc == 0)
                                {
                                    yLoc = 0;
                                    this.playerPoints++;
                                    itemSpot--;
                                    if (itemSpot > 0)
                                    {
                                        this.itemsInSpot[0, 0]++;
                                        itemSpot--;
                                    }
                                }
                                else
                                {
                                    if (yLoc == 1 && itemSpot > 0 && xLoc > 0)
                                    {
                                        xLoc--;
                                        itemSpot--;
                                        this.itemsInSpot[yLoc, xLoc]++;

                                    }
                                    else
                                    {
                                        if (yLoc == 0 && itemSpot > 0 && xLoc == 5)
                                        {
                                            yLoc = 1;
                                            itemSpot--;
                                            this.pcPoints++;
                                            if (itemSpot > 0)
                                            {
                                                this.itemsInSpot[1, 5]++;
                                                itemSpot--;
                                            }
                                        }
                                        else
                                        {
                                            if (yLoc == 0 && itemSpot > 0 && xLoc < 5)
                                            {
                                                xLoc++;
                                                itemSpot--;
                                                this.itemsInSpot[yLoc, xLoc]++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

            }
            
            

            Boolean allPlayerEmpty = false;
            Boolean allPcEmpty = false;

            //checking if all of the computer's are empty in order to choose turns
            if (this.itemsInSpot[0,0]==0)
            {
                if (this.itemsInSpot[0, 1] == 0)
                {
                    if (this.itemsInSpot[0, 2] == 0)
                    {
                        if (this.itemsInSpot[0, 3] == 0)
                        {
                            if (this.itemsInSpot[0, 4] == 0)
                            {
                                if(this.itemsInSpot[0,5]==0)
                                {
                                    allPcEmpty = true;
                                }
                            }
                        }
                    }
                }
            }

            //checking if all of the player's are empty in order to choose turns
            if (this.itemsInSpot[1, 0] == 0) 
            {
                if (this.itemsInSpot[1, 1] == 0)
                {
                    if (this.itemsInSpot[1, 2] == 0)
                    {
                        if (this.itemsInSpot[1, 3] == 0)
                        {
                            if (this.itemsInSpot[1, 4] == 0)
                            {
                                if (this.itemsInSpot[1, 5] == 0)
                                {
                                    allPlayerEmpty = true;
                                }
                            }
                        }
                    }
                }
            }

            if (this.turn == true)
            {
                this.turn = false;
            }
            else
            {
                this.turn = true;
            }

            if(allPlayerEmpty==true || allPcEmpty == true)
            {
                if (allPlayerEmpty == true)
                {
                    this.turn = false;
                }
                else
                {
                    this.turn = true;
                }
            }
        }

        
    }
}

