namespace EfCoreDemo.Entities;

public class Employee
{
    public Guid Id {get; set;}
    public string Name {get;set;}
    public Guid DepartmentId {get; set;}
    public Department? Departement {get;set;}
}