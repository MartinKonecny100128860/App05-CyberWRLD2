using System;

namespace App05MonoGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new App05Game())
                game.Run();
        }
    }
}
