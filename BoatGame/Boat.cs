public class Boat
{
    public static List<Boat> boats = new List<Boat>();
    public static int maxBoats = 1;
    Rectangle rect;
    float rotation;
    Rope ropeHooked;
    float speed = 0.5f;
    public Boat()
    {
        Random rnd = new Random();
        this.rotation = rnd.Next(0, 2) == 1 ? 90 : 270;

        this.rect.height = Raylib.GetScreenWidth() / 25;
        this.rect.width = (rect.height / 3) * 2;
        if (rotation == 270) this.rect.x = Raylib.GetScreenWidth();
        else this.rect.x = 0;

        int portHeight = Raylib.GetScreenHeight() / 5;
        this.rect.y = rnd.Next(portHeight, Raylib.GetScreenHeight() - (int)rect.height * 2);

        boats.Add(this);
    }
    public string Update()
    {
        if (ropeHooked != null)
        {
            if (Vector2.Distance(GetVector2(), ropeHooked.joints[ropeHooked.joints.Count - 2]) < 1.5)
            {
                if (ropeHooked.joints.Count >= 3)
                    ropeHooked.joints.RemoveAt(ropeHooked.joints.Count - 1);
                else
                {
                    Rope.allRopes.Remove(ropeHooked);
                    return "score";
                }
            }

            rotation = LookAt(ropeHooked.joints[ropeHooked.joints.Count - 2]);
        }

        // Normal move
        CalculatePositionRotation();

        if (rect.x < 0 - rect.width || rect.x > Raylib.GetScreenWidth()) // Boat outside of screen
            return "dead";

        // Touching rope
        if (ropeHooked == null)
            for (int i = 0; i < Rope.allRopes.Count; i++)
            {
                int index = -1;
                index = Rope.allRopes[i].joints.FindIndex(x => Raylib.CheckCollisionRecs(new Rectangle(x.X, x.Y, 2, 2), new Rectangle(rect.x, rect.y - rect.width / 2, 2, rect.height)));

                if (index == -1) continue;

                ropeHooked = Rope.allRopes[i];

                Rope.CaughtBoat();

                // Trim rope
                int amountToRemove = Rope.allRopes[i].joints.Count - index - 1;
                Rope.allRopes[i].joints.RemoveRange(index + 1, amountToRemove);
            }

        return "";
    }
    float LookAt(Vector2 joint)
    {
        float deltaY = rect.y - joint.Y; // Calculate Delta y

        float deltaX = joint.X - rect.x; // Calculate delta x

        float angle = (float)(Math.Atan2(deltaY, deltaX) * 180.0 / Math.PI) + 90; // Find angle

        float amountToRotate = angle - rotation;

        // amountToRotate *= 0.3f;

        Console.WriteLine($"Rotation: {rotation} Angle: {angle} Amount: {amountToRotate}");

        return rotation + amountToRotate;
    }
    void CalculatePositionRotation()
    {
        double radians = (Math.PI / 180) * rotation;

        rect.x = (float)(rect.x + speed * Math.Sin(radians));
        rect.y = (float)(rect.y + speed * Math.Cos(radians));
    }
    Vector2 GetVector2()
    {
        return new Vector2(rect.x, rect.y);
    }
    public void Draw()
    {
        Raylib.DrawRectanglePro(rect, new Vector2(rect.width / 2, rect.height), -rotation, Color.BLACK);

        Vector2 boatTip = new Vector2(rect.x, rect.y);
        Raylib.DrawCircleV(boatTip, 3, Color.DARKPURPLE);
    }
}