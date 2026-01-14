// namespace SellingPriceProject.Service
// {
//     public class CountSellingPrice()
//     {
//         public string Name {get;}
//         public int Hpp {get;}
//         public int BiayaPacking {get;}        
//         public int BiayaPlatform {get;}
//         public int Keuntungan {get;}

//         public CountSellingPrice(string name, int hpp, int biayaPacking, int biayaPlatform, int keuntungan) 
//         {
//             Name = name;
//             Hpp = hpp;
//             BiayaPacking=biayaPacking;
//             BiayaPlatform = biayaPlatform;
//             Keuntungan = hpp * keuntungan / 100;

//         }
    
        
//         public int CountProduct => Hpp + BiayaPacking + Keuntungan;
//         public int countProductWithPlatform = CountProduct + (CountProduct * BiayaPlatform / 100);

//        public string Info => $"Nama: {Name} /n Harga Jual : {countProductWithPlatform}";
//     }
// }