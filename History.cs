namespace BuggyInventorySystem
{
    // Keeps a record of every purchase and sale. Game writes entries here;
    // the View reads them back. Exposed read-only so nothing else can edit it.
    class History
    {
        private List<string> _entries;

        public History()
        {
            _entries = new List<string>();
        }

        public IReadOnlyList<string> Entries => _entries;

        public void Add(string entry)
        {
            _entries.Add(entry);
        }
    }
}
