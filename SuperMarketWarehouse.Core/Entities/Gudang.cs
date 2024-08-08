namespace SuperMarketWarehouse.Core.Entities
{
    public class Gudang
    {
        public int Id { get; set; }
        public string NamaGudang { get; set; }
        public string Location { get; set; }
        public ICollection<Barang> Barangs { get; set; }
    }
}
