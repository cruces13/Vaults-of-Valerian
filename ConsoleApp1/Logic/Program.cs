namespace Application.Logic
{
    public class Program
    {
        // Main method to start the game
        static void Main()
        {
            //Initialize the Program
            _ = new Program();
        }
         
        

        public Program()
        {
            // Initialize the game state
            GameState game = new GameState();
            string startingMessage = "You wake up in a dark room. Outside the open door, you can go LEFT or RIGHT.\n" +
                $"You have {game.Player.Health} health.\n" +
                $"You have {game.Player.Strength} strength.\n" +
                $"You have {game.Player.Defense} defense.\n" +
                $"You are in the {game.Player.Location}.\n" +
                $"You have {game.Player.Gold} gold.\n" +
                "Your inventory contains: " + string.Join(", ", game.Player.Inventory) + ".\n";

            // Display the starting information
            Console.WriteLine(startingMessage);

            // Prompt the player for input
            Console.WriteLine("What would you like to do?");
            string? choice = Console.ReadLine();

            // Process the player's choice
            if (choice == null)
            {
                return;
            }
            game.Player.Location = game.MoveRooms(choice.ToUpper());
        }
    }
}