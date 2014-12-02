using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class BattleGame
    {
       // public Tile[,] board;
        public Board board;
        public List<GameCharacter> characterList;
        public int currentCharacter = 0;

        public BattleLog battleLog;

        public Random r;

        public GameCharacter ActiveCharacter
        {
            get
            {
                return characterList[currentCharacter];
            }
        }

        public BattleGame()
        {
            r = new Random();
            battleLog = new BattleLog();
            board = new Board(this);
           
            InitChars();

            StartBattle();
            
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

        //Increment Initiative
        private void NextTurn()
        {
            currentCharacter++;
            if(currentCharacter >= characterList.Count)
            {
                currentCharacter = 0;
            }
        }

        //currently, randomize
        private void placeCharactersInBoard()
        {
            foreach(var gc in characterList)
            {
                var freeTile = board.getFreeTile();
                board.FillTile(gc, freeTile);
            }
        }


        private void RunBattle()
        {
            BattleStatusType battleStatus = getBattleStatus();
            while(battleStatus == BattleStatusType.Running)
            {
                Console.Clear();
                Console.WriteLine(board.ToString());
                Console.WriteLine(battleLog.ToString());
                DisplayCharList();
                if (characterList[currentCharacter].hp > 0)
                {
                    if (characterList[currentCharacter].type == CharacterType.Player)
                    {
                        DisplayMainMenu();
                    }
                    else
                    {
                        RunEnemyTurn();
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                else
                {
                    NextTurn();
                }
            }
        }

        //display the list of characters, indicating active
        private void DisplayCharList()
        {
            string txt = "";
            int counter = 0;
            foreach(var c in characterList)
            {
                if(counter == currentCharacter)
                {
                    txt += string.Format(" ->{0} ({1})<- ", c.name, c.displayChar);
                }
                else
                {
                    txt += string.Format(" {0} ({1}) ", c.name, c.displayChar);
                }
                counter++;
            }

            Console.WriteLine(txt);
        }

        private void DisplayMainMenu()
        {
            List<string> menu = new List<string>(){"1. View","2. Move","3. Attack", "4. Skip"};
            int input = CoreHelper.displayMenuGetInt(menu);
            switch(input)
            {
                case 1:
                    DisplayViewMenu();
                    break;
                case 2:
                    DisplayMoveMenu();
                    break;
                case 3:
                    DisplayAttackMenu();
                    break;
                case 4:
                    PlayerSkip();
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
            
            //Display the Character
            Console.Write(characterList[input - 1].ToString());
            Console.Write(">");
            Console.ReadLine();
            return;
        }

        private void DisplayMoveMenu()
        {
            List<string> menu = new List<string>() { "1. North","2.South","3.West","4.East","5.Back" };
            int input = CoreHelper.displayMenuGetInt(menu);
            var curTile = board.getTileFromLocation(ActiveCharacter.x,ActiveCharacter.y);
            DirectionType dir;
            switch(input)
            {
                case 1:
                    dir = DirectionType.North;
                    break;
                case 2:
                    dir = DirectionType.South;
                    break;
                case 3:
                    dir = DirectionType.West;
                     break;
                case 4:
                     dir = DirectionType.East;
                     break;
                case 5:
                     return;
                default:
                     return;
            }

            if (board.MoveCharacter(ActiveCharacter, board.getAdjascentTile(curTile, dir)))
            {
                battleLog.AddEntry(string.Format("{0} moved {1}.", ActiveCharacter.name, dir.ToString()));
            }
            else
            {
                battleLog.AddEntry(string.Format("{0} was unable to move {1}.", ActiveCharacter.name, dir.ToString()));
            }
        }

        
        private void DisplayAttackMenu()
        {

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

        private void PlayerSkip()
        {
            battleLog.AddEntry(characterList[currentCharacter].name + " skipped turn.");

            NextTurn();

        }

        private void PlayerMove(GameCharacter player, int x, int y)
        {

        }

        private void PlayerAttack(GameCharacter player, GameCharacter enemy)
        {

        }

        //enemy AI
        private void RunEnemyTurn()
        {

            EnemySkip();
               
        }

        private void EnemySkip()
        {
            battleLog.AddEntry(string.Format("{0} skipped its turn.",characterList[currentCharacter].name));

            NextTurn();
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


    }
}
