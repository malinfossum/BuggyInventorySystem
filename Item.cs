namespace BuggyInventorySystem
{
    class Item
    {
        public string Name { get; private set; }

        public int Price { get; private set; }

        public bool IsRare { get; private set; }

        public Item(string name, int price, bool isRare)
        {
            Price = price;
            Name = name;
            IsRare = isRare;
        }
    }
}
