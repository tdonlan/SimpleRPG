using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class BattleGame
    {
        public Tile[,] board;
        public List<GameCharacter> characterList;
        public BattleLog battleLog;

        Random r;

        public BattleGame()
        {
            r = new Random();
            battleLog = new BattleLog();
            InitBoard(20);
            InitChars();

            StartBattle();
            
        }

        private void InitBoard(int size)
        {
            board = new Tile[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = new Tile(i,j);
                }
            }

            battleLog.AddEntry("Board Initialized");
        }

        private void InitChars()
        {
            characterList = new List<GameCharacter>();
            characterList.Add(CharacterFactory.getPlayerCharacter(r));
            characterList.Add(CharacterFactory.getEnemy(r));

            battleLog.AddEntry("Characters Initialized");
        }

        private void StartBattle()
        {
            battleLog.AddEntry("Starting Battle");

            SetBattleInitiative();
            placeCharactersInBoard();
            RunBattle();

        }

        private void SetBattleInitiative()
        { 
            //randomize attack order 
        }

        //currently, randomize
        private void placeCharactersInBoard()
        {
            foreach(var gc in characterList)
            {
                var freeTile = getFreeTile();
                gc.x = freeTile.x;
                gc.y = freeTile.y;
                board[freeTile.x, freeTile.y].empty = false;
                board[freeTile.x, freeTile.y].TileChar = gc.displayChar;
            }
        }

        //return a random tile that is empty
        private Tile getFreeTile()
        {
            List<Tile> freeTileList = getFreeTileList();

            return  freeTileList.ElementAt(r.Next(freeTileList.Count));

        }

        private List<Tile> getFreeTileList()
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
        

        private void RunBattle()
        {
            BattleStatusType battleStatus = getBattleStatus();
            while(battleStatus == BattleStatusType.Running)
            {
                Console.WriteLine(this.ToString());
                Console.WriteLine(battleLog.ToString());
                DisplayMainMenu();
            }
        }

        private void DisplayMainMenu()
        {
            List<string> menu = new List<string>(){"1. View","2. Move","3. Attack"};
            int input = CoreHelper.displayMenuGetInt(menu);
            switch(input)
            {
                case 1:
                    DisplayViewMenu();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default: break;
            }
            return;
        }

        private void DisplayViewMenu()
        {
            //get list of characters
            List<string> displayCharList = new List<string>();
            int count = 1;
            foreach(var c in characterList)
            {
                displayCharList.Add(string.Format("{0}.{1} ({2})", count, c.name, c.displayChar));
                count++;
            }

            int input = CoreHelper.displayMenuGetInt(displayCharList);
            Console.Write(characterList[input - 1].ToString());
            Console.Write(">");
            Console.ReadLine();
            return;
        }

         

        private BattleStatusType getBattleStatus()
        {
            bool playersDead = true;
            bool enemiesDead = true;
            foreach(var gc in characterList)
            {
                if(gc.type == CharacterType.Player)
                {
                    if(gc.hp > 0)
                    {
                        playersDead = false;
                    }
                }
                if(gc.type == CharacterType.Enemy)
                {
                    if(gc.hp > 0)
                    {
                        enemiesDead = false;
                    }
                }
            }

            if(playersDead)
            {
                return BattleStatusType.PlayersDead;
            }
            else if(enemiesDead)
            {
                return BattleStatusType.EnemiesDead;
            }
            else
            {
                return BattleStatusType.Running;
            }
          
        }

        private void PlayerMove(GameCharacter player, int x, int y)
        {

        }

        private void PlayerAttack(GameCharacter player, GameCharacter enemy)
        {

        }

        private void EnemyMove(GameCharacter enemy, int x, int y)
        {

        }
        
        private void EnemyAttack(GameCharacter enemy, GameCharacter player)
        {

        }

        private void LoseBattle()
        {

        }
        
        private void WinBattle()
        {


        }


        

        //print game board
        public override string ToString()
        {
            string retval = "Simple RPG Board: \n";

            int width = board.GetLength(0);
            List<char> letterList = CoreHelper.getLetterList();
            retval += "    ";
            for (int i = 0; i < width;i++ )
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
