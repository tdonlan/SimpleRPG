using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class Board
    {
        public Tile[,] board;
        public BattleGame game;

        public Board(BattleGame game)
        {
            this.game = game;
            InitBoard(20);
        }

        private void InitBoard(int size)
        {
            board = new Tile[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = new Tile(i, j);
                }
            }

            game.battleLog.AddEntry("Board Initialized");
        }

        public bool MoveCharacter(GameCharacter gc, Tile Destination)
        {
            bool retval = false;

            if(Destination.empty)
            {
                EmptyTile(board[gc.x,gc.y]);
                FillTile(gc,Destination);

                retval = true;
            }

            return retval;
        }

        

        public void FillTile(GameCharacter gc, Tile t)
        {
            t.empty=false;
            gc.x = t.x;
            gc.y = t.y;
            t.TileChar = gc.displayChar;
        }

        public void EmptyTile(Tile t)
        {
            t.empty = true;
            t.TileChar = '.';
        }
         
        
        public Tile getFreeTile()
        {
            List<Tile> freeTileList = getFreeTileList();

            return  freeTileList.ElementAt(game.r.Next(freeTileList.Count));
        }


        public List<Tile> getFreeTileList()
        {
            List<Tile> freeTileList = new List<Tile>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                   if(board[i,j].empty)
                   {
                       freeTileList.Add(board[i, j]);
                   }
                }
            }
            return freeTileList;
        }


        //print game board
        public override string ToString()
        {
            string retval = "RPG Board: \n";

            int width = board.GetLength(0);
            List<char> letterList = CoreHelper.getLetterList();
            retval += "    ";
            for (int i = 0; i < width; i++)
            {
                retval += letterList[i] + " ";
            }
            retval += "\n";

            for (int i = 0; i < board.GetLength(0); i++)
            {
                retval += CoreHelper.getPaddedNum(i) + "| ";
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    retval += board[i, j] + " ";
                }
                retval += "\n";
            }
            return retval;
        }

    }
}
