public class Item
{
    private ItemBase Base { get; set; }
    public Item(ItemBase ib)
    {
        Base = ib;
    }
}