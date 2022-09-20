public class Game
{
    public int score = 0;
    public int health = 3;
    public Game()
    {
        Raylib.InitWindow(1280, 720, "Uwu");
        Raylib.SetTargetFPS(120);

        Port.CreatePorts();

        while (!Raylib.WindowShouldClose())
        {
            Update();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLUE);
            Draw();
            Raylib.EndDrawing();
        }
    }
    void Update()
    {
        // Boat logic
        if (Boat.boats.Count < Boat.maxBoats) new Boat();

        for (int i = 0; i < Boat.boats.Count; i++)
        {
            string results = Boat.boats[i].Update();
            if (results == "dead")
            {
                health--;
                Boat.boats.RemoveAt(i);
            }

        }

        // Rope logic
        Rope.Update();
    }
    void Draw()
    {
        Raylib.DrawText(health.ToString(), 0, 0, 20, Color.GOLD);

        // Draw boats
        for (int i = 0; i < Boat.boats.Count; i++)
        {
            Boat.boats[i].Draw();
        }

        // Draw ports
        Port.Draw();

        // Draw ropes
        Rope.Draw();
    }
}
