using EfShopJaya.Data;
using EfShopJaya.Model;
using Microsoft.EntityFrameworkCore;

bool exit = false;

// using (var context = new AppDbContext())
// {
//     // Hapus semua produk dulu karena ada foreign key ke kategori
//     context.Products.RemoveRange(context.Products);
//     context.SaveChanges();

//     // Hapus semua kategori
//     context.Categories.RemoveRange(context.Categories);
//     context.SaveChanges();

//     Console.WriteLine("Semua data kategori dan produk berhasil dihapus.");
// }

while (!exit)
{
    Console.WriteLine("\n=== EF JAYA SHOP ===");
    Console.WriteLine("1. Tambah Kategori");
    Console.WriteLine("2. Lihat Kategori");
    Console.WriteLine("3. Tambah Produk");
    Console.WriteLine("4. Lihat Produk");
    Console.WriteLine("5. Edit Produk");
    Console.WriteLine("6. Hapus Produk");
    Console.WriteLine("0. Keluar");
    Console.Write("Pilih menu: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": // Tambah Kategori
            using (var context = new AppDbContext())
            {
                Console.Write("Masukkan nama kategori baru: ");
                var catName = Console.ReadLine();
                 
                bool exists = context.Categories.Any(c => c.Name.ToLower() == catName.ToLower());
                if (exists)
                {
                    Console.WriteLine("Kategori dengan nama ini sudah ada, tidak bisa ditambahkan.");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(catName))
                {
                    var category =  new Category
                    {
                        Name = catName
                    };
                    context.Categories.Add(category);
                    context.SaveChanges();
                    Console.WriteLine("Kategori berhasil ditambahkan.");
                }
            }
            break;

        case "2": // Lihat Kategori
            using (var context = new AppDbContext())
            {
                var categories = context.Categories.ToList();
                if (!categories.Any())
                {
                    Console.WriteLine("Belum ada kategori.");
                }
                else
                {
                    Console.WriteLine("Daftar Kategori:");
                    int idx = 1;
                    foreach (var c in categories)
                    {
                        Console.WriteLine($"{idx}. {c.Name} (ID: {c.Id})");
                        idx++;
                    }
                }
            }
            break;

        case "3": // Tambah Produk
            using (var context = new AppDbContext())
            {
                var categories = context.Categories.ToList();
                if (!categories.Any())
                {
                    Console.WriteLine("Tidak ada kategori. Tambahkan kategori terlebih dahulu!");
                    break;
                }

                Console.WriteLine("Daftar Kategori:");
                foreach (var c in categories)
                    Console.WriteLine($"{c.Id}. {c.Name}");

                Console.Write("Masukkan nama produk: ");
                var prodName = Console.ReadLine();

                Console.Write("Masukkan ID kategori produk: ");
                if (!int.TryParse(Console.ReadLine(), out int catId) || !categories.Any(c => c.Id == catId))
                {
                    Console.WriteLine("ID kategori tidak valid.");
                    break;
                }

                var product = new Product { Name = prodName, CategoryId = catId };
                context.Products.Add(product);
                context.SaveChanges();

                var savedProduct = context.Products.Include(p => p.Category)
                                        .FirstOrDefault(p => p.Id == product.Id);

                Console.WriteLine($"\nProduk berhasil ditambahkan: {savedProduct.Name} - Kategori: {savedProduct.Category.Name}");
            }
            break;

        case "4": // Lihat Produk
            using (var context = new AppDbContext())
            {
                var products = context.Products.Include(p => p.Category).ToList();
                if (!products.Any())
                {
                    Console.WriteLine("Belum ada produk.");
                }
                else
                {
                    Console.WriteLine("Daftar Produk:");
                    int idx = 1;
                    foreach (var p in products)
                    {
                        Console.WriteLine($"{idx}. {p.Name} - Kategori: {p.Category.Name} (ID: {p.CategoryId})");
                        idx++;
                    }
                }
            }
            break;

        case "5": // Edit Produk
            using (var context = new AppDbContext())
            {
                var products = context.Products.Include(p => p.Category).ToList();
                if (!products.Any())
                {
                    Console.WriteLine("Belum ada produk untuk diedit.");
                    break;
                }

                Console.WriteLine("Daftar Produk:");
                foreach (var p in products)
                    Console.WriteLine($"{p.Id}. {p.Name} - Kategori: {p.Category.Name}");

                Console.Write("Masukkan ID produk yang ingin diedit: ");
                if (!int.TryParse(Console.ReadLine(), out int prodId))
                {
                    Console.WriteLine("Input tidak valid.");
                    break;
                }

                var productToUpdate = context.Products.Include(p => p.Category)
                                    .FirstOrDefault(p => p.Id == prodId);
                if (productToUpdate == null)
                {
                    Console.WriteLine("Produk tidak ditemukan.");
                    break;
                }

                Console.Write($"Nama baru produk (kosong jika tidak diubah, sekarang: {productToUpdate.Name}): ");
                var newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    productToUpdate.Name = newName;

                var categories = context.Categories.ToList();
                Console.WriteLine("Daftar Kategori:");
                foreach (var c in categories)
                    Console.WriteLine($"{c.Id}. {c.Name}");

                context.SaveChanges();
                Console.WriteLine("Produk berhasil diperbarui.");
            }
            break;

        case "6": // Hapus Produk
            using (var context = new AppDbContext())
            {
                var products = context.Products.Include(p => p.Category).ToList();
                if (!products.Any())
                {
                    Console.WriteLine("Belum ada produk untuk dihapus.");
                    break;
                }

                Console.WriteLine("Daftar Produk:");
                foreach (var p in products)
                    Console.WriteLine($"{p.Id}. {p.Name} - Kategori: {p.Category.Name}");

                Console.Write("Masukkan ID produk yang ingin dihapus: ");
                if (!int.TryParse(Console.ReadLine(), out int prodId))
                {
                    Console.WriteLine("Input tidak valid.");
                    break;
                }

                var productToRemove = context.Products.FirstOrDefault(p => p.Id == prodId);
                if (productToRemove == null)
                {
                    Console.WriteLine("Produk tidak ditemukan.");
                    break;
                }

                context.Products.Remove(productToRemove);
                context.SaveChanges();
                Console.WriteLine($"Produk '{productToRemove.Name}' berhasil dihapus.");
            }
            break;

        case "0":
            exit = true;
            Console.WriteLine("Keluar program.");
            break;

        default:
            Console.WriteLine("Pilihan tidak valid.");
            break;
    }
}
