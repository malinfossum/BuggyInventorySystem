namespace BuggyInventorySystem
{
    class Player
    {
        public string Name { get; private set; }

        private int _gold;

        private List<Item> _inventory;

        public Player(string name, int gold)
        {
            Name = name;
            _gold = gold;

            _inventory = new List<Item>();
        }

        public int Gold => _gold;

        // Read-only so callers can list the inventory but not change it.
        public IReadOnlyList<Item> Inventory => _inventory;

        public void AddGold(int amount)
        {
            _gold += amount;
        }

        public void RemoveGold(int amount)
        {
            _gold -= amount;
        }

        public bool CanAfford(int price)
        {
            return _gold >= price;
        }

        public void AddItem(Item item)
        {
            _inventory.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _inventory.Remove(item);
        }

        public Item? FindItem(string name)
        {
            foreach (Item item in _inventory)
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    return item;
                }
            }

            return null;
        }
    }
}
