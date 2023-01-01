using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SodukoSolverOmega
{
    internal class Cell
    {
        private List<char> possibilities;
        private char value;
        //refrence to its perrs
        private List<Cell> Rowpeers;
        private List<Cell> Colpeers;
        private List<Cell> Boxpeers;
        private bool fixedNum;
        private bool isFilled;
        private List<char> possibilityBackup;

        public char Value { get { return value; } }
        public bool isfilled { get { return isFilled; } }
        public bool hasPosssibilities { get { return possibilities.Count > 0; } }
        public void resetVal()
        {
            value = '0';
            isFilled = false;
            resetPossibilities();
            initList(possibilities);
        }
        public void resetPossibilities()
        {
            possibilities = possibilityBackup;
        }
        public void backupPossibilities()
        {
            possibilityBackup = possibilities;
        }
        public List<Cell> rowpeers
        {
            get { return Rowpeers; }
            set { Rowpeers = value; }
        }
        public List<Cell> colpeers
        {
            get { return Colpeers; }
            set { Colpeers = value; }
        }
        public List<Cell> boxpeers
        {
            get { return Boxpeers; }
            set { Boxpeers = value; }
        }

        public Cell()
        {
            Rowpeers = new List<Cell>();
            Colpeers = new List<Cell>();
            Boxpeers = new List<Cell>();
            possibilities = new List<char>();
            initList(possibilities);
            value = '0';
            fixedNum = false;
            isFilled = false;
        }
        public Cell(char val)
        {
            Rowpeers = new List<Cell>();
            Colpeers = new List<Cell>();
            Boxpeers = new List<Cell>();
            possibilities = new List<char>();
            value = val;
            fixedNum = true;
            isFilled = true;
            eliminatePeersPossibility();

        }

        //func that tests all the remaining values for the cell and places the first one that does not conflict
        //return true if found a value to place
        public bool Guess()
        {

            foreach (char guess in possibilities.ToList())
            {
                possibilities.Remove(guess);
                if (isValid(guess))
                {
                    value = guess;
                    isFilled = true;
                    return true;
                }
            }
            return false;

        }

        //checks if the testVal exsits in one of the cells peers and return false if does
        public bool isValid(char testVal)
        {
            foreach (Cell cell in Rowpeers)
            {
                if (cell.Value == testVal)
                {
                    return false;
                }
            }
            foreach (Cell cell in Colpeers)
            {
                if (cell.Value == testVal)
                {
                    return false;
                }
            }
            foreach (Cell cell in Boxpeers)
            {
                if (cell.Value == testVal)
                {
                    return false;
                }
            }
            return true;
        }
        
        public void setVal(char val)
        {
            value = val;
            possibilities.Clear();
            isFilled = true;
            eliminatePeersPossibility();
            NakedPairs();
        }
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
            foreach(List<Cell> curlist in Lists)
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
            if(possibilities.Count == 1)
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
            if(possibilities.Count == 2)
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
                foreach(Cell peer in peerGroup)
                {
                    peer.removePossibility(value);
                    if(peer.possibilities.Count == 1)
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
        private void initList(List<char> possibilities)
        {
            possibilities.Clear();
            for (int i = 0; i < Consts.BOARD_WIDTH; i++)
            {
                possibilities.Add(Consts.ValOptions[i]);
            }
        }
        public void removePossibility(char value)
        {
            possibilities.Remove(value);
        }

        

    }
}
