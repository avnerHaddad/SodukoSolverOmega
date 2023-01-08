using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SodukoSolverOmega.Configuration.Consts;

namespace SodukoSolverOmega.SodukoEngine.Objects
{
    internal class Cell
    {
        //possivilities and value
        private List<char> possibilities;
        private char value;
        private Tuple<int, int> cords;
        //refrence to its perrs
        private List<Tuple<int, int>> Rowpeers;
        private List<Tuple<int, int>> Colpeers;
        private List<Tuple<int, int>> Boxpeers;
        //helper vars
        private bool isFixed;
        private bool isFilled;

        public Tuple<int, int> Cords { get { return cords; } set { cords = value; } }
        public List<Tuple<int, int>> rowpeers
        {
            get { return Rowpeers; }
            set { Rowpeers = value; }
        }
        public List<Tuple<int, int>> colpeers
        {
            get { return Colpeers; }
            set { Colpeers = value; }
        }
        public List<Tuple<int, int>> boxpeers
        {
            get { return Boxpeers; }
            set { Boxpeers = value; }
        }
        public char Value { get { return value; } }
        public bool isfilled { get { return isFilled; } }
        public bool hasPosssibilities { get { return possibilities.Count > 0; } }

        public List<char> Possibilities { get { return possibilities; } set { possibilities = value; } }

        public Cell(int i, int j)
        {
            Rowpeers = new List<Tuple<int, int>>();
            Colpeers = new List<Tuple<int, int>>();
            Boxpeers = new List<Tuple<int, int>>();
            possibilities = new List<char>();
            //initList(possibilities);
            value = '0';
            isFixed = false;
            isFilled = false;
            Cords = new Tuple<int, int>(i, j);

        }
        public Cell(char val,int i, int j)
        {
            Rowpeers = new List<Tuple<int, int>>();
            Colpeers = new List<Tuple<int, int>>();
            Boxpeers = new List<Tuple<int, int>>();
            possibilities = new List<char>();
            //initList(possibilities);
            value = val;
            isFixed = true;
            if(val != '0')
            {
                isFilled = true;
            }
            Cords = new Tuple<int, int>(i, j);
            //eliminatePeersPossibility();

        }

        //rests val to 0 and resets possibilities to what it was before
        public void resetVal()
        {
            value = '0';
            isFilled = false;
            initList(possibilities);
        }


        //creates a backuup of the old possibilities before algorithems




        //func that tests all the remaining values for the cell and places the first one that does not conflict
        //return true if found a value to place

        //checks if the testVal exsits in one of the cells peers and return false if does

        public void setVal(char val)
        {
            value = val;
            possibilities.Remove(val);
            //possibilities.Remove(val);
            isFilled = true;
            //eliminatePeersPossibility();
            //NakedPairs();
            //InterSectionRemoval();
        }
        public void initList(List<char> possibilities)
        {
            possibilities.Clear();
            for (int i = 1; i < Consts.BOARD_WIDTH+1; i++)
            {
                possibilities.Add(Consts.ValOptions[i]);
            }
        }
        public string ToString()
        {
            return value.ToString();
        }

        internal void hiddenSet()
        {
            //called when found hidden single
            setVal(possibilities[0]);
        }
        /*
public bool HiddenSingles()
{
   //runtime is linear to possibilites*


   //for each num in possibilities
   //check if it exsits somewhere in its peers possibilites
   //dosnt? great place it
   //does? move on to the next possibility


   //hidden singles cols
   List<List<Cell>> Lists = new List<List<Cell>>();
   Lists.Add(Colpeers);
   Lists.Add(Rowpeers);
   Lists.Add(Boxpeers);
   foreach (List<Cell> curlist in Lists)
   {
       foreach (char possibility in possibilities)
       {
           bool hidden = true;
           foreach (Cell cell in curlist)
           {
               if (cell.possibilities.Contains(possibility))
               {
                   hidden = false;
                   break;
               }
           }
           if (hidden)
           {
               //add set to possibility func that changes to taken and shit

               setVal(possibility);
               return true;
           }


       }

   }
   return false;
}

//func that sets value to the only possibility that is left if there is only one remaining
public bool SinglePossibility()
{
   if (possibilities.Count == 1)
   {
       setVal(possibilities[0]);
       return true;
   }
   else
   {
       return false;
   }
}

public bool NakedPairs()
{
   if (possibilities.Count == 2)
   {
       List<List<Cell>> Peers = new List<List<Cell>>();
       Peers.Add(Colpeers);
       Peers.Add(Rowpeers);
       Peers.Add(Boxpeers);
       foreach (List<Cell> peerGroup in Peers)
       {
           foreach (Cell cell in peerGroup)
           {
               if (cell.possibilities.Equals(possibilities))
               {
                   eliminatePeersPossibilityByVal(possibilities[0]);
                   eliminatePeersPossibilityByVal(possibilities[1]);
                   cell.possibilities = possibilities;
                   return true;
               }
           }
       }

   }
   return false;
}
public void eliminatePeersPossibility()
{
   List<List<Cell>> Peers = new List<List<Cell>>();
   Peers.Add(Colpeers);
   Peers.Add(Rowpeers);
   Peers.Add(Boxpeers);
   foreach (List<Cell> peerGroup in Peers)
   {
       foreach (Cell peer in peerGroup)
       {
           peer.removePossibility(value);
           if (peer.possibilities.Count == 1)
           {
               peer.SinglePossibility();
           }
       }

   }
}
public void eliminatePeersPossibilityByVal(char value)
{
   List<List<Cell>> Peers = new List<List<Cell>>();
   Peers.Add(Colpeers);
   Peers.Add(Rowpeers);
   Peers.Add(Boxpeers);
   foreach (List<Cell> peerGroup in Peers)
   {
       foreach (Cell peer in peerGroup)
       {
           peer.removePossibility(value);
       }

   }
}
//func that sets up the possibilities list, fills it with nums from 1-9

public void removePossibility(char value)
{
   possibilities.Remove(value);
}

public void InterSectionRemoval()
{
   //for box peers to rows/cols
   foreach (char possibility in possibilities)
   {
       int possibilityCount = 0;
       foreach (Cell cell in boxpeers)
       {
           if (cell.possibilities.Contains(possibility))
           {
               possibilityCount++;
               if (possibilityCount == 3)
               {
                   break;
               }
           }
       }
       if (possibilityCount > 0 || possibilityCount < 3)
       {
           //remove from intersection

           //find the way of the intersection
           if (possibilityCountPerGroup(rowpeers, possibility) == 0)
           {
               RemovePossibilityFromGroupNotInGroup(colpeers, boxpeers, possibility);
           }
           //rowPeersContainPossibilityCount == 0
           //elimentate from the col peers
           //colPeerContainPossibilityCount == 0
           if (possibilityCountPerGroup(colpeers, possibility) == 0)
           {
               RemovePossibilityFromGroupNotInGroup(rowpeers, boxpeers, possibility);
           }
           //remove from row peers

           //
       }
   }
   //for cols to box peers
   foreach (char possibility in possibilities)
   {
       int possibilityCount = 0;
       foreach (Cell cell in colpeers)
       {
           if (cell.possibilities.Contains(possibility))
           {
               possibilityCount++;
               if (possibilityCount == 3)
               {
                   break;
               }
           }
       }
       if (possibilityCount > 0 || possibilityCount < 3)
       {
           //remove from intersection

           //find the way of the intersection
           if (possibilityCountPerGroup(boxpeers, possibility) == 0)
           {
               RemovePossibilityFromGroupNotInGroup(boxpeers, colpeers, possibility);
           }
           //rowPeersContainPossibilityCount == 0
           //elimentate from the col peers


           //
       }
   }
   //for rows to box peers
   foreach (char possibility in possibilities)
   {
       int possibilityCount = 0;
       foreach (Cell cell in rowpeers)
       {
           if (cell.possibilities.Contains(possibility))
           {
               possibilityCount++;
               if (possibilityCount == 3)
               {
                   break;
               }
           }
       }
       if (possibilityCount > 0 || possibilityCount < 3)
       {
           //remove from intersection

           //find the way of the intersection
           if (possibilityCountPerGroup(boxpeers, possibility) == 0)
           {
               RemovePossibilityFromGroupNotInGroup(boxpeers, rowpeers, possibility);
           }
           //rowPeersContainPossibilityCount == 0
           //elimentate from the col peers


           //
       }
   }

}
public void RemovePossibilityFromGroupNotInGroup(List<Cell> group, List<Cell> safeGroup, char possibility)
{
   foreach (Cell cell in group)
   {
       if (!safeGroup.Contains(cell))
       {
           cell.possibilities.Remove(possibility);
       }
   }
}
public int possibilityCountPerGroup(List<Cell> group, char possibility)
{
   int count = 0;
   foreach (Cell cell in group)
   {
       if (cell.possibilities.Contains(possibility))
       {
           count++;
       }
   }
   return count;
}


*/
    }

}
