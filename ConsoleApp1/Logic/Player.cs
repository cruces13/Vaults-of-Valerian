namespace Application.Logic
{
    public class Player(string Name, string[] inventory, int health, int strength, int defense, string location, int gold)
    {

        //Set Properties
        public string Name { get; set; } = Name;
        public string[] Inventory { get; set; } = inventory;
        public int Health { get; set; } = health;
        public int Strength { get; set; } = strength;
        public int Defense { get; set; } = defense;
        public string Location { get; set; } = location;
        public int Gold { get; set; } = gold;

    }
}
