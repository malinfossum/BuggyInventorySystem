namespace BuggyInventorySystem
{
    class Game
    {
        private Shop _shop;
        private Player _player;
        private View _view;
        private History _history;

        public Game()
        {
            _shop = new Shop();
            _player = new Player("Alex", 100);
            _view = new View();
            _history = new History();
        }

        public void Start()
        {
            bool running = true;

            while (running)
            {
                _view.ShowMenu();

                string input = _view.AskForChoice();

                switch (input)
                {
                    case "1":
                        _view.ShowShop(_shop.Items);
                        break;

                    case "2":
                        BuyItem();
                        break;

                    case "3":
                        SellItem();
                        break;

                    case "4":
                        _view.ShowInventory(_player.Inventory);
                        break;

                    case "5":
                        _view.ShowGold(_player.Gold);
                        break;

                    case "6":
                        SearchItem();
                        break;

                    case "7":
                        _view.ShowHistory(_history.Entries);
                        break;

                    case "8":
                        running = false;
                        break;

                    default:
                        _view.ShowMessage("Invalid choice");
                        break;
                }
            }
        }

        private void BuyItem()
        {
            string itemName = _view.AskForItemName();

            Item? item = _shop.FindItem(itemName);

            if (item == null)
            {
                _view.ShowMessage("Item not found");
                return;
            }

            if (!_player.CanAfford(item.Price))
            {
                _view.ShowMessage("Not enough gold");
                return;
            }

            _player.AddItem(item);
            _player.RemoveGold(item.Price);
            _shop.RemoveItem(item);

            _history.Add("Bought " + item.Name + " for " + item.Price);

            _view.ShowMessage("Item purchased");
        }

        private void SellItem()
        {
            string itemName = _view.AskForItemName();

            Item? item = _player.FindItem(itemName);

            if (item == null)
            {
                _view.ShowMessage("Item not found");
                return;
            }

            _player.RemoveItem(item);
            _player.AddGold(item.Price);
            _shop.AddItem(item);

            _history.Add("Sold " + item.Name + " for " + item.Price);

            _view.ShowMessage("Item sold");
        }

        private void SearchItem()
        {
            string search = _view.AskForSearch();

            List<Item> results = _shop.Search(search);

            _view.ShowSearchResults(results);
        }
    }
}
