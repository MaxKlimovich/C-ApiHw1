namespace Ex1.Models;

public class Group : BaseModel
{
    public virtual List<Product> Products { get; set; } = null!;

}