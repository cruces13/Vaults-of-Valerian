using System.Text.Json;

namespace Application.Logic
{
    internal class GameState
    {
        public Player Player { get; private set; }
        public Enemy[] Enemies { get; private set; }
        private Dictionary<string, Room> rooms = [];

        public GameState()
        {
            string Name = "Player Name";
            string[] startingInventory = { "Sword", "Compass" };
            int startingHealth = 20;
            int startingStrength = 5;
            int startingDefense = 3;
            string startingLocation = "STARTING ROOM";
            int startingGold = 0;

            // Prompt the player for their Name
            Console.WriteLine("Enter your character's Name:");
            string? input = Console.ReadLine();
            Name = !string.IsNullOrWhiteSpace(input) ? input : Name;

            // Initialize the player with starting values
            Player = new Player(Name, startingInventory, startingHealth, startingStrength, startingDefense, startingLocation, startingGold);

            // Initialize Enemies
            Enemies = [];

            LoadRooms();
        }

        private void LoadRooms()
        {
            // Load the rooms from rooms.json
            string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Data", "Rooms.json"));
            Console.WriteLine($"Looking for rooms.json at: {filePath}");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var roomList = JsonSerializer.Deserialize<List<Room>>(json);

                if (roomList == null)
                {
                    Console.WriteLine("Failed to deserialize rooms.json. Please check the file format.");
                    return;
                }
                else
                {
                    rooms = roomList.Where(r => !string.IsNullOrEmpty(r.Name)).ToDictionary(r => r.Name);
                    Console.WriteLine("Rooms loaded:");
                    foreach (var room in rooms)
                    {
                        Console.WriteLine($"- {room.Key}: {room.Value.Description}");
                    }
                }
                    
            }
            else
            {
                // Handle the case where the file does not exist
                Console.WriteLine("rooms.json file not found. Please ensure it exists in the Data folder.");
                rooms = [];
                return;
            }
        }

        // Method to retrieve room options
        // Look up the current room in rooms.json, display the available options
        public string GetRooms()
        {
            if (rooms.TryGetValue(Player.Location, out Room? currentRoom)) // Use nullable Room type
            {
                return currentRoom.Name;
            }
            else
            {
                return "Room not found.";
            }
        }

        // Method to handle room movement
        public string MoveRooms(string direction)
        {
            // Logic to handle room movement
            if (rooms.TryGetValue(Player.Location, out Room? currentRoom))
            {
                // Check if the direction is valid
                if (currentRoom.LinksTo != null && currentRoom.LinksTo.Contains(direction, StringComparer.OrdinalIgnoreCase))
                {
                    Player.Location = currentRoom.LinksTo.FirstOrDefault(link => link.Equals(direction, StringComparison.OrdinalIgnoreCase)) ?? Player.Location;
                    Console.WriteLine($"You moved to {Player.Location}.");
                }
                else
                {
                    Console.WriteLine("Invalid direction. You can't go that way.");
                }
                if (currentRoom.LinksTo == null)
                {
                    Console.WriteLine("You can't go that way.");
                }
            }
            else
            {
                Console.WriteLine("Current room not found.");
            }
            Console.WriteLine($"You are now in {Player.Location}.");
            return Player.Location;
        }

        public static int Damage(int strength, int defense)
        {
            // Calculate damage based on attack and defense values
            int damage = strength - defense;
            return damage > 0 ? damage : 0;
        }

        public static void Combat(Player player, Enemy[] Enemies)
        {
            // Loop through the Enemies and let the player attack them one by one
            foreach (var enemy in Enemies)
            {
                Console.WriteLine($"You encounter a {enemy.Name} with {enemy.Health} health, {enemy.Strength} strength, & {enemy.Defense} defense.");
                // Add combat logic here
                while (enemy.Health > 0 && player.Health > 0)
                {
                    // Player attacks enemy
                    enemy.Health -= Damage(player.Strength, enemy.Defense);
                    Console.WriteLine($"You attack the enemy! Enemy health: {enemy.Health}");

                    // Check if enemy is defeated
                    if (enemy.Health <= 0)
                    {
                        Console.WriteLine($"You defeated the {enemy.Name}!");
                        break;
                    }

                    // Enemy attacks player
                    player.Health -= Damage(enemy.Strength, player.Defense);
                    Console.WriteLine($"The enemy attacks you! Your health: {player.Health}");

                    // Check if player is defeated
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("You have been defeated!");
                        break;
                    }
                }
            }
        }
    }
}
