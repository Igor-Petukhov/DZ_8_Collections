using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_8_Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player_1 = new Player("Player_1");
            Player player_2 = new Player("Player_2");
            //Player player_3 = new Player("Player_3");
            player_1.Show_my_cards();
            player_2.Show_my_cards();
            Game game = new Game(player_1, player_2/*, player_3*/);

            

            Console.ReadKey();
        }
    }
}
