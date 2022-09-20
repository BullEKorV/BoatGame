public class Game
{
    public Game()
    {
        Raylib.InitWindow(1280, 720, "Uwu");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Update();
            Draw();
        }
    }

    void Update()
    {

    }
    void Draw()
    {

    }
}
