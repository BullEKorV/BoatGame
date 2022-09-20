public class Port
{
    public static List<Port> ports = new List<Port>();
    public Rectangle rect;
    public Port(Rectangle rect)
    {
        this.rect = rect;
    }
    public static void Draw()
    {
        foreach (Port port in ports)
        {
            Raylib.DrawRectanglePro(port.rect, new Vector2(port.rect.width / 2, 0), 0, Color.BEIGE);
            Raylib.DrawCircleV(new Vector2(port.rect.x, port.rect.height), 6, Color.DARKPURPLE);
        }
    }
    public static void CreatePorts()
    {

        int amount = 2;
        int spacing = Raylib.GetScreenWidth() / (amount + 1);

        for (int i = 0; i < amount; i++)
        {
            ports.Add(new Port(new Rectangle(spacing + spacing * i, 0, 50, 70)));
        }
    }
}
