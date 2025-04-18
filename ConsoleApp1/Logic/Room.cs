namespace ConsoleApp1.Logic
{
    public class Room(string Name, string Description, string[] Items, string[] Enemies)
    {
        // Set Properties
        public string Name { get; set; } = Name;
        public string Description { get; set; } = Description;
        public string[]? Items { get; set; } = Items;
        public string[]? Enemies { get; set; } = Enemies;
        public string[]? LinksTo { get; set; } = [];
        public string[]? LinksFrom { get; set; } = [];
    }
}
