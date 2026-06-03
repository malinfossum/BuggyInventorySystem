namespace BuggyInventorySystem
{
    // All console input/output lives here, so the rest of the program never
    // touches Console. Business classes return data; the View shows it.
    class View
    {
        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== SHOP SYSTEM ===");
            Console.WriteLine("1. Show shop");
            Console.WriteLine("2. Buy item");
            Console.WriteLine("3. Sell item");
            Console.WriteLine("4. Show inventory");
            Console.WriteLine("5. Show gold");
            Console.WriteLine("6. Search");
            Console.WriteLine("7. Show history");
            Console.WriteLine("8. Exit");
            Console.WriteLine();

            Console.Write("Choice: ");
        }

        public void ShowShop(IReadOnlyList<Item> items)
        {
            Console.WriteLine();
            Console.WriteLine("=== SHOP ===");

            if (items.Count == 0)
            {
                Console.WriteLine("Shop is empty");
            }

            foreach (Item item in items)
            {
                Console.WriteLine(FormatItem(item));
            }
        }

        public void ShowInventory(IReadOnlyList<Item> items)
        {
            Console.WriteLine();
            Console.WriteLine("=== INVENTORY ===");
            Console.WriteLine("Items: " + items.Count);

            foreach (Item item in items)
            {
                Console.WriteLine(FormatItem(item));
            }
        }

        public void ShowGold(int gold)
        {
            Console.WriteLine("Gold: " + gold);
        }

        public void ShowSearchResults(List<Item> results)
        {
            Console.WriteLine();
            Console.WriteLine("=== SEARCH ===");

            if (results.Count == 0)
            {
                Console.WriteLine("No items found");
            }

            foreach (Item item in results)
            {
                Console.WriteLine(FormatItem(item));
            }
        }

        public void ShowHistory(IReadOnlyList<string> entries)
        {
            Console.WriteLine();
            Console.WriteLine("=== HISTORY ===");

            if (entries.Count == 0)
            {
                Console.WriteLine("No transactions yet");
            }

            foreach (string entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string AskForChoice()
        {
            return Console.ReadLine() ?? "";
        }

        public string AskForItemName()
        {
            Console.Write("Enter item name: ");
            return Console.ReadLine() ?? "";
        }

        public string AskForSearch()
        {
            Console.Write("Search: ");
            return Console.ReadLine() ?? "";
        }

        // One place that decides how an item is shown, so the rare marker
        // appears the same way in the shop, the inventory and search.
        private string FormatItem(Item item)
        {
            if (item.IsRare)
            {
                return item.Name + " - " + item.Price + " (Rare)";
            }

            return item.Name + " - " + item.Price;
        }
    }
}
