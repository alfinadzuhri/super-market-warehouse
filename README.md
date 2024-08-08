# super-market-warehouse


## 1. Algoritma:
Persyaratan ini berkaitan dengan membuat solusi agar staf gudang dapat memeriksa barang yang kadaluarsa secara efisien dan proaktif. Berikut adalah pendekatan yang disarankan:

**Masalah**: Staf gudang (Pegawai A) perlu memeriksa barang-barang yang sudah kadaluarsa di gudang supermarket dan menemukan solusi untuk mengelola serta mengantisipasi proses pengecekan barang kadaluarsa.

**Solusi**:

### Desain Algoritma:
- **Sistem Pemindaian Otomatis**:
  - Implementasikan sistem otomatis yang memindai semua barang di gudang secara berkala untuk memeriksa tanggal kadaluarsa.
  - Sistem ini bisa menggunakan pemindaian barcode atau RFID untuk mengotomatisasi proses pelacakan barang.

- **Analisis Prediktif**:
  - Implementasikan analisis prediktif untuk menandai barang-barang yang mendekati tanggal kadaluarsa berdasarkan tren data sebelumnya. Hal ini memungkinkan staf untuk fokus pada barang yang lebih mungkin segera kadaluarsa.

- **Notifikasi Real-Time**:
  - Buat sistem notifikasi real-time yang memberi tahu staf ketika barang-barang mendekati tanggal kadaluarsa.
  - Ini bisa dilakukan melalui aplikasi mobile atau dashboard yang menyoroti barang-barang yang membutuhkan perhatian segera.

- **Basis Data Terpusat**:
  - Pertahankan basis data terpusat yang melacak semua barang inventaris, termasuk tanggal kadaluarsanya. Basis data ini akan diperbarui secara otomatis ketika barang-barang dipindai dan ditambahkan ke gudang.

- **Audit Rutin**:
  - Laksanakan audit rutin di mana sistem menghasilkan laporan barang-barang yang sudah kadaluarsa atau mendekati kadaluarsa. Laporan ini dapat ditinjau oleh staf untuk mengambil tindakan yang diperlukan.

## 2. Query:
Persyaratan ini berfokus pada pembuatan dan pengelolaan query database, prosedur, dan trigger untuk sistem gudang. Berikut cara Anda dapat mencapainya:

### a. Membuat 2 Tabel (Tabel Gudang & Tabel Barang) dengan Foreign Key dan Index:

```sql
-- Membuat Tabel Gudang
CREATE TABLE Gudang (
    KodeGudang INT PRIMARY KEY,
    NamaGudang VARCHAR(255) NOT NULL
);

-- Membuat Tabel Barang
CREATE TABLE Barang (
    KodeBarang INT PRIMARY KEY,
    NamaBarang VARCHAR(255) NOT NULL,
    HargaBarang DECIMAL(10, 2) NOT NULL,
    JumlahBarang INT NOT NULL,
    ExpiredBarang DATE NOT NULL,
    KodeGudang INT,
    FOREIGN KEY (KodeGudang) REFERENCES Gudang(KodeGudang)
);

-- Index untuk pencarian lebih cepat pada ExpiredBarang
CREATE INDEX idx_barang_expired ON Barang (ExpiredBarang);
```

### b. Membuat Stored Procedure untuk Menampilkan Data Menggunakan Query Dinamis dan Paging:

```sql
CREATE PROCEDURE GetBarangByGudang (
    IN p_KodeGudang INT,
    IN p_Page INT,
    IN p_PageSize INT
)
BEGIN
    SET @sql = CONCAT('
        SELECT KodeGudang, NamaGudang, KodeBarang, NamaBarang, HargaBarang, JumlahBarang, ExpiredBarang 
        FROM Gudang g 
        JOIN Barang b ON g.KodeGudang = b.KodeGudang 
        WHERE g.KodeGudang = ? 
        ORDER BY ExpiredBarang
        LIMIT ?, ?');

    PREPARE stmt FROM @sql;
    SET @offset = (p_Page - 1) * p_PageSize;
    EXECUTE stmt USING p_KodeGudang, @offset, p_PageSize;
    DEALLOCATE PREPARE stmt;
END;
```

### c. Membuat Trigger untuk Menandai Barang Kadaluarsa:

```sql
CREATE TRIGGER trg_check_expired
BEFORE INSERT ON Barang
FOR EACH ROW
BEGIN
    IF NEW.ExpiredBarang < CURDATE() THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Tidak bisa memasukkan barang yang sudah kadaluarsa';
    END IF;
END;
```
