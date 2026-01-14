namespace CalculatorProject.Services
{
    public class CalculatorService
    {
        public int A { get; set; }
        public int B { get; set; }

        public int Tambah
        {
            get { return A + B; }
        }

        public int Kurang
        {
            get { return A - B; }
        }

        public int Kali
        {
            get { return A * B; }
        }

        public double Bagi
        {
            get
            {
                if (B == 0)
                    return 0;

                return (double)A / B;
            }
        }
    }
}
