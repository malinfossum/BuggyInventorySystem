namespace BuggyInventorySystem
{
    class Shop
    {
        private List<Item> _items;

        public Shop()
        {
            _items = new List<Item>();

            SeedItems();
        }

        private void SeedItems()
        {
            _items.Add(new Item("Sword", 30, false));
            _items.Add(new Item("Shield", 25, false));
            _items.Add(new Item("Magic Ring", 70, true));
            _items.Add(new Item("Potion", 10, false));
        }

        // Read-only so callers can list the stock but not change it.
        public IReadOnlyList<Item> Items => _items;

        public Item? FindItem(string name)
        {
            foreach (Item item in _items)
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    return item;
                }
            }

            return null;
        }

        public List<Item> Search(string search)
        {
            List<Item> results = new List<Item>();

            foreach (Item item in _items)
            {
                if (item.Name.ToLower().Contains(search.ToLower()))
                {
                    results.Add(item);
                }
            }

            return results;
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }
    }
}
