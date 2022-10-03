public class Port
{
    public static List<Port> ports = new List<Port>();
    public static int height;
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
            Raylib.DrawCircleV(new Vector2(port.rect.x, port.rect.height), 12, Color.DARKPURPLE);
        }
    }
    public static void CreatePorts()
    {
        height = Raylib.GetScreenHeight() / 7;
        int amount = 2;
        int spacing = Raylib.GetScreenWidth() / (amount + 1);

        for (int i = 0; i < amount; i++)
        {
            ports.Add(new Port(new Rectangle(spacing + spacing * i, 0, height * 0.7f, height)));
        }
    }
}
