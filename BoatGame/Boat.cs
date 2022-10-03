public class Boat
{
    public static List<Boat> boats = new List<Boat>();
    static Texture2D texture = Raylib.LoadTexture("Textures/Boat.png");
    public static int maxBoats = 1;
    Rectangle rect;
    float rotation;
    Rope ropeHooked;
    public static int baseSpeed;
    int speed;
    public Boat()
    {
        Random rnd = new Random();
        this.rotation = rnd.Next(0, 2) == 1 ? 90 : 270;

        this.rect.width = 16;
        this.rect.height = Raylib.GetScreenHeight() / 27;
        if (rotation == 270) this.rect.x = Raylib.GetScreenWidth();
        else this.rect.x = 0;

        this.rect.y = rnd.Next((int)(Port.height * 1.3f), Raylib.GetScreenHeight() - (int)rect.height * 2);

        this.speed = baseSpeed + rnd.Next(-20, 20);

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

        if (ropeHooked == null && (rect.x < 0 - rect.width || rect.x > Raylib.GetScreenWidth())) // Boat outside of screen
            return "dead";

        // Touching rope
        if (ropeHooked == null)
            for (int i = 0; i < Rope.allRopes.Count; i++)
            {
                int index = -1;
                index = Rope.allRopes[i].joints.FindIndex(x => Raylib.CheckCollisionRecs(new Rectangle(x.X, x.Y, 2, 2), new Rectangle(rect.x - rect.width / 2, rect.y - rect.height / 2, rect.width, rect.height)));

                if (index == -1) continue;
                foreach (Boat boat in Boat.boats)
                {
                    if (boat.ropeHooked == Rope.allRopes[i]) return "";
                }

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

        // Console.WriteLine($"Rotation: {rotation} Angle: {angle} Amount: {amountToRotate}");

        return rotation + amountToRotate;
    }
    void CalculatePositionRotation()
    {
        double radians = (Math.PI / 180) * rotation;

        rect.x = (float)(rect.x + speed * Raylib.GetFrameTime() * Math.Sin(radians));
        rect.y = (float)(rect.y + speed * Raylib.GetFrameTime() * Math.Cos(radians));
    }
    Vector2 GetVector2()
    {
        return new Vector2(rect.x, rect.y);
    }
    public void Draw()
    {
        float sizeScale = 0.15f;
        Vector2 size = new Vector2(texture.width, texture.height);
        Rectangle sourceRec = new Rectangle(0, 0, size.X, size.Y);
        Rectangle destRec = new Rectangle(rect.x, rect.y, size.X * sizeScale, size.Y * sizeScale);
        Raylib.DrawTexturePro(texture, sourceRec, destRec, new Vector2(size.X * 0.5f * sizeScale, size.Y * sizeScale), -rotation, Color.WHITE);

        // Raylib.DrawRectangleRec(new Rectangle(rect.x, rect.y - rect.height / 2, rect.width, rect.height), Color.SKYBLUE);
        // Vector2 boatTip = new Vector2(rect.x, rect.y);
        // Raylib.DrawCircleV(boatTip, 3, Color.DARKPURPLE);
    }
}