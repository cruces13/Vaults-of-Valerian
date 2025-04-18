namespace ConsoleApp1.Logic
{
    public class Enemy(string Name , int health, int strength, int defense)
    {

        //Set Properties
        public string Name { get; set; } = Name;
        public int Health { get; set; } = health;
        public int Strength { get; set; } = strength;
        public int Defense { get; set; } = defense;
    }
}
