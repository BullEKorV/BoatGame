public class Game
{
    public int score = 0;
    public int health = 3;
    public Game()
    {
        Raylib.InitWindow(1920, 1080, "Uwu");
        Raylib.ToggleFullscreen();

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
        // Difficulty
        Boat.baseSpeed = 50 + (int)Raylib.GetTime();
        Boat.maxBoats = (int)Math.Ceiling(Raylib.GetTime() / 20);


        // Boat logic
        if (Boat.boats.Count < Boat.maxBoats)
        {
            Random rnd = new Random();
            if (rnd.Next(Raylib.GetFPS() * 2) == 23)
                new Boat();
        }

        for (int i = 0; i < Boat.boats.Count; i++)
        {
            string results = Boat.boats[i].Update();
            if (results == "dead")
            {
                health--;
                Boat.boats.RemoveAt(i);
            }
            if (results == "score")
            {
                score++;
                Boat.boats.RemoveAt(i);
            }
        }

        // Rope logic
        Rope.Update();
    }
    void Draw()
    {
        Raylib.DrawText("Health: " + health.ToString(), 8, 5, 40, Color.RED);
        Raylib.DrawText("Score: " + score.ToString(), Raylib.GetScreenWidth() - Raylib.MeasureText("Score:  " + score.ToString().ToString(), 40), 8, 40, Color.GOLD);
        Raylib.DrawText(((int)Raylib.GetTime()).ToString(), Raylib.GetScreenWidth() / 2 - Raylib.MeasureText(((int)Raylib.GetTime()).ToString(), 50), 20, 50, Color.BLACK);

        // Draw ports
        Port.Draw();

        // Draw ropes
        Rope.Draw();

        // Draw boats
        for (int i = 0; i < Boat.boats.Count; i++)
        {
            Boat.boats[i].Draw();
        }
    }
}
