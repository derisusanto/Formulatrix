using System.Security.Cryptography.X509Certificates;

namespace PersonServiceProject.Service
{
    public class PersonService
{
    public string Name { get; }
    public int BirthYear { get; }

    public PersonService(string name, int birthYear)
    {
        Name = name;
        BirthYear = birthYear;
    }

    public int Age => DateTime.Now.Year - BirthYear;

    public string Info => $"Nama: {Name}, Umur: {Age} tahun";
}

  public class StudenService
    {
        public string Name {get;}
        public string Kelas {get;}
        public int Score {get;}   

        public StudenService(string name, string kelas, int score)
        {
            Name = name;
            Kelas = kelas;
            Score = score;
        }

        public string Passed => Score >= 75 ? "Lulus" : "Tidak";
        public string Info => $"Nama: {Name}, Kelas: {Kelas}, Hasil : {Passed}";
    }

}