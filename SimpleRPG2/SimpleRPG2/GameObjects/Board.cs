using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class Point
    {
        public int x {get;set;}
        public int y {get;set;}

        public Point()
        { }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


    }


    public class Board
    {
        public int size;
        public Tile[,] board;
        public BattleGame game;

        public Board(BattleGame game, int size)
        {
            this.size = size;
            this.game = game;
            InitBoard(size);
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

        public Tile getAdjascentTile(Tile t, DirectionType dir)
        {
            Tile retval = null;
            switch(dir)
            {
                case DirectionType.West:
                    retval = getTileFromLocation(t.x, t.y - 1);
                    break;
                case DirectionType.East:
                    retval = getTileFromLocation(t.x, t.y + 1);
                    break; 
                case DirectionType.North:
                    retval = getTileFromLocation(t.x - 1, t.y);
                    break;
                case DirectionType.South:
                    retval = getTileFromLocation(t.x + 1, t.y);
                    break;
                default: break;
            }
            return retval;
        }

        //return the tile that is a distance away (used to get tile patterns)
        public Tile getNearTile(Tile t, Point diff)
        {
            return getTileFromLocation(t.x + diff.x, t.y + diff.y);
        }

        public Tile getTileFromLocation(int x, int y)
        {
            try
            {
                return board[x, y];
            }
            catch
            {
                return null;
            }
        }

        public Point getPointFromTile(Tile t)
        {
            return new Point() { x = t.x, y = t.y };
        }

        //Need to use A* to get this distance
        public int getTileDistance(Tile a, Tile b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y-b.y);
        }

        public bool MoveCharacter(GameCharacter gc, Tile Destination)
        {
            bool retval = false;
            if (Destination != null)
            {
                if (Destination.empty)
                {
                    //Use Actionpoints to Move
                    Tile curTile = getTileFromLocation(gc.x, gc.y);
                    if(gc.SpendAP(getTileDistance(curTile,Destination)))
                    { 
                        EmptyTile(board[gc.x, gc.y]);
                        FillTile(gc, Destination);

                        retval = true;
                    }
                }
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

        public void SetTile(char c, Tile t)
        {
            t.empty = false;
            t.TileChar = c;
                 
        }

        public void clearTile(Tile t)
        {
            t.empty = true;
            t.TileChar = '.';
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



        public List<Tile> getTileListFromPattern(Tile origin, TilePatternType pattern)
        {
            List<Tile> retvalList = new List<Tile>();

            switch(pattern)
            {
                case TilePatternType.FourAdj:
                    return getTileListFromPointList(origin, PatternFactory.getFourAdj());
                default:
                    break;
            }

            return retvalList;
        }

        private List<Tile> getTileListFromPointList(Tile origin, List<Point> pointList)
        {
            List<Tile> retvalList = new List<Tile>();
            foreach(var p in pointList)
            {
                var t = getNearTile(origin, p);
                if(t != null)
                {
                    retvalList.Add(t);
                }
            }
            return retvalList;
        }

        public List<Tile> getBoardLOS(Tile origin, Tile destination)
        {
            List<Point> pointList = PlotLine.GetPointsOnLine(origin.x, origin.y, destination.x, destination.y).ToList();
            List<Tile> tileList = new List<Tile>();
            tileList.Add(origin);
            
            foreach(var p in pointList)
            {
                Tile tempTile = this.getTileFromLocation(p.x, p.y);
                if(tempTile != origin && tempTile != destination)
                {
                    if(tempTile.empty)
                    {
                        tileList.Add(tempTile);
                    }
                    else
                    {
                        return tileList;
                    }
                }

            }

            tileList.Add(destination);

            return tileList;
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
