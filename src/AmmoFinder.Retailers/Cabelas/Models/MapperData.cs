namespace AmmoFinder.Retailers.Cabelas.Models
{
    internal class MapperData
    {
        public MapperData(string productId, string catEntryId, string productUrl, InventoryData inventory)
        {
            ProductId = productId;
            CatEntryId = catEntryId;
            ProductUrl = productUrl;
            Inventory = inventory;
        }

        public string ProductId { get; set; }
        public string CatEntryId { get; set; }
        public InventoryData Inventory { get; set; }
        public string ProductUrl { get; set; }
    }
}
