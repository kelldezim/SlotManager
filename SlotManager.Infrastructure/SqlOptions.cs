namespace SlotManager.Infrastructure
{
    internal sealed class SqlOptions
    {
        public string ConnectionString { get; set; }
        public bool ApplyMigrations { get; set; }
        public bool InitializeData { get; set; }
    }
}
