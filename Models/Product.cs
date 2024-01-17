namespace Ex1.Models;

public class Product : BaseModel
{
    public string Description { get; set; }

    public int GroupID { get; set; }

    public Group Group { get; set; } = null;
    public int Price { get; set; }
    
    public List<Store> Stores { get; set; }
}