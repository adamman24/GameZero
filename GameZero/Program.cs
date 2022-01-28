using System;

namespace GameZero
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameZero())
                game.Run();
        }
    }
}
