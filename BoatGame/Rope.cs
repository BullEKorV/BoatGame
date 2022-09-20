public class Rope
{
    public static List<Rope> allRopes = new List<Rope>();
    public static Rope current;
    public List<Vector2> joints = new List<Vector2>();
    public bool hasBoat = false;
    public Rope(Vector2 startPos)
    {
        joints.Add(startPos);
        current = this;
        allRopes.Add(this);
    }
    public static void Update()
    {
        // Create new rope
        if (current == null && Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {
            foreach (Port port in Port.ports)
            {
                if (Vector2.Distance(new Vector2(port.rect.x, port.rect.height), Raylib.GetMousePosition()) <= 8)
                    new Rope(new Vector2(port.rect.x, port.rect.height));
            }
        }
        else if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_LEFT) && current != null)
        {
            if (!current.hasBoat)
                allRopes.Remove(current);
            current = null; // Clear current rope
        }

        // Create joints in rope
        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            if (current != null && Vector2.Distance(current.joints.Last(), Raylib.GetMousePosition()) > 10) current.joints.Add(Raylib.GetMousePosition());
    }
    public static void Draw()
    {
        foreach (Rope rope in allRopes)
        {
            for (int i = 0; i < rope.joints.Count - 1; i++)
            {
                Raylib.DrawLineEx(rope.joints[i], rope.joints[i + 1], 3, Color.ORANGE);
            }
        }
    }
    public static void CaughtBoat()
    {
        current.hasBoat = true;
        current = null;
    }
}
