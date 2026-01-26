using System;


namespace BattleKapal;

class Program
{
    static void Main()
    {
        

        WriteTitle();
        LoopOptions();

        Console.WriteLine("===== Select Player =====");
        
        Console.WriteLine("1 - Player 1");
        Console.WriteLine("2 - Player 2");
        string input = Console.ReadLine();

    }

      private static void WriteTitle()
        {
            string nameOfTheGame = @"
        _           _   _   _           _     _       
       | |         | | | | | |         | |   (_)      
       | |__   __ _| |_| |_| | ___  ___| |__  _ _ __  
       | '_ \ / _` | __| __| |/ _ \/ __| '_ \| | '_ \ 
       | |_) | (_| | |_| |_| |  __/\__ \ | | | | |_) |
       |_.__/ \__,_|\__|\__|_|\___||___/_| |_|_| .__/ 
                                               | |    
                                               |_|  ";

            string ship = @"                                        ___
                                       __|
                                  ______|_|
                           _   __|_________|  _
            _        =====| | |            | | |==== _
      =====| |        .---------------------------. | |====
<-----------------'   .  .  .  .  .  .  .  .   '------------/
  \                                                        /
   \                                                      /
    \____________________________________________________/
";
            Console.WriteLine(nameOfTheGame);
            Console.WriteLine(ship);
        }

           private static void LoopOptions()
        {
            bool notContinue = true;
            while (notContinue)
            {
                Console.WriteLine("1 - New Game (or press Enter)");
                Console.WriteLine("2 - Exit");
                string input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "":
                        notContinue = false;
                        break;
                    case "1":
                        Console.Clear();
                        notContinue = false;
                        break;
                    case "2":
                        // exit();
                        break;
                    case "exit":
                        // exit();
                        break;
                    default:
                        Console.WriteLine("Please enter in 1 for a new game, or 2 to exit.");
                        Console.WriteLine("You can also type exit\n");
                        break;
                }
            }
        }

   
}