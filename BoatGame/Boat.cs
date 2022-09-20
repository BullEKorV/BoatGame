public class Boat
{
    public static List<Boat> boats = new List<Boat>();
    public static int maxBoats = 2;
    Rectangle rect;
    bool lookingLeft;
    Rope ropeHooked;
    int speed = 1;
    public Boat()
    {
        Random rnd = new Random();
        this.lookingLeft = rnd.Next(0, 2) == 1 ? true : false;

        this.rect.width = Raylib.GetScreenWidth() / 25;
        this.rect.height = (rect.width / 3) * 2;
        if (lookingLeft) this.rect.x = Raylib.GetScreenWidth();
        else this.rect.x = -rect.width;

        int portHeight = Raylib.GetScreenHeight() / 5;
        this.rect.y = rnd.Next(portHeight, Raylib.GetScreenHeight() - (int)rect.height * 2);

        boats.Add(this);
    }
    public string Update()
    {
        if (ropeHooked != null)
        {


            return "";
        }
        if (lookingLeft) rect.x -= speed;
        else if (!lookingLeft) rect.x += speed;

        if (rect.x < 0 - rect.width || rect.x > Raylib.GetScreenWidth()) // Boat outside of screen
            return "dead";

        // Touching rope
        foreach (Rope rope in Rope.allRopes)
        {
            Vector2 joint = rope.joints.Find(x => Raylib.CheckCollisionRecs(new Rectangle(x.X, x.Y, 2, 2), rect));
            if (joint == new Vector2(0, 0)) continue;
            ropeHooked = rope;


        }

        return "";
    }
    public void Draw()
    {
        Raylib.DrawRectangleRec(rect, Color.BLACK);
    }
}