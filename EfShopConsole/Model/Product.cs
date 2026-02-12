namespace EfShopJaya.Model;

public class Product
{
    public int Id {get; set;}
    public string Name {get; set;}="";

    //ini sebagai tanda bahwa ini FK
    public int CategoryId {get; set;}
    public Category? Category {get;set;}
}