namespace SuperMarketWarehouse.Core.Entities
{
    public class Barang
    {
        public int Id { get; set; }
        public string NamaBarang { get; set; }
        public decimal Harga { get; set; }
        public int Jumlah { get; set; }
        public DateTime ExpiryDate { get; set; }

        public int GudangId { get; set; }
        public Gudang Gudang { get; set; }
    }
}
