namespace EfCoreDemo.Entities;
 
public class EmployeeDetail
{
    public int Id { get; set; }  // bisa sama dengan EmployeeId
    public string Address { get; set; }
    public string Phone { get; set; }

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } // One-to-One
}
