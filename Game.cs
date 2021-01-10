using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DZ_8_Collections
{
    class Game
    {
        List<Player> players = new List<Player>();
        public List<Karta> Koloda = new List<Karta>();
        public void AddPlayer(Player player)
        {
            Console.WriteLine($"Добавляю в игру {player}");
            players.Add(player);
        }
        private void Sozdat_peretasovat_kolodu()
        {
            var rand = new Random();
            for (int i = 0; i < 36; i++)
            {
                Koloda.Add(new Karta(0, "0"));
            }
            int counter_mast = 0;
            int tmp;
            for (int i = 0; i < 36; i++)
            {
                for (int j = 6; j < 16; j++)
                {
                    tmp = rand.Next(36);
                    while (Koloda[tmp].type != 0)
                    {
                        tmp++;
                        if (tmp == 36)
                        {
                            tmp = 0;
                        }
                    }
                    if (counter_mast == 0)
                    {
                        Koloda[tmp] = new Karta(j, "Пика");
                        if (j > 14)
                        {
                            counter_mast++;
                            j = 6;
                        }
                    }
                    if (counter_mast == 1)
                    {
                        Koloda[tmp] = new Karta(j, "Трефа");
                        if (j > 14)
                        {
                            counter_mast++;
                            j = 6;
                        }
                    }
                    if (counter_mast == 2)
                    {
                        Koloda[tmp] = new Karta(j, "Бубна");
                        if (j > 14)
                        {
                            counter_mast++;
                            j = 6;
                        }
                    }
                    if (counter_mast == 3)
                    {
                        Koloda[tmp] = new Karta(j, "Черва");
                        if (j > 14)
                        {
                            counter_mast++;
                            j = 6;
                        }
                    }
                    Console.WriteLine($"Создали карту {Koloda[tmp]}");
                    if ((Koloda[tmp].type == 14) && (Koloda[tmp].mast == "Черва"))
                    {
                        Console.WriteLine("Generation done");
                        i = 50;
                        break;
                    }
                }
            }
        }
        public void Razdat_Kolodu(Player player_1, Player player_2, Player player_3, Player player_4)
        {
            Queue<Karta> queue_koloda = new Queue<Karta>(Koloda);
            Koloda.Clear();
            int i = 0;
            Karta one = new Karta(0, "Пика");
            Karta two = new Karta(0, "Пика");
            Karta three = new Karta(0, "Пика");
            Karta four = new Karta(0, "Пика");
            for (int j = 0; j < 36; j++)
            {
                if (i == 0)
                {
                    player_1.TakeKart(queue_koloda.Peek());
                    queue_koloda.Dequeue();
                    i++;
                    continue;
                }
                if (i == 1)
                {
                    player_2.TakeKart(queue_koloda.Peek());
                    queue_koloda.Dequeue();
                    i++;
                    continue;
                }
                if (i == 2)
                {
                    if (player_3 != null)
                    {
                        player_3.TakeKart(queue_koloda.Peek());
                        queue_koloda.Dequeue();
                        i++;
                        continue;
                    }
                    player_1.TakeKart(queue_koloda.Peek());
                    queue_koloda.Dequeue();
                    i = 1;
                    continue;
                }
                if (i == 3)
                {
                    if (player_4 != null)
                    {
                        player_4.TakeKart(queue_koloda.Peek());
                        queue_koloda.Dequeue();
                        i++;
                        continue;
                    }
                    player_1.TakeKart(queue_koloda.Peek());
                    queue_koloda.Dequeue();
                    i = 1;
                    continue;
                }
            }
        }
        public Game(Player player_1, Player player_2, Player player_3 = null, Player player_4 = null) //до 4 игроков
        {
            AddPlayer(player_1);
            AddPlayer(player_2);
            if (player_3 != null)
                AddPlayer(player_3);
            if (player_4 != null)
                AddPlayer(player_4);
            //Создаём колоду карт
            Sozdat_peretasovat_kolodu();

            

            //Подсмотрим как легли в колоду карты
            Console.WriteLine("Подсмотрим как легли в колоду карты");
            foreach (var item in Koloda)
            {
                Console.WriteLine(item);
            }

            Razdat_Kolodu(player_1, player_2, player_3, player_4);


            Play(player_1, player_2, player_3, player_4);
        }

        private void Play(Player player_1, Player player_2, Player player_3, Player player_4)
        {
            Player winner = null;

            Console.WriteLine("======GAME======");
            List<Karta> prikup = new List<Karta>();
            while (winner == null)
            {
                Karta stol_1 = (Karta)player_1.myKarts.Peek().Clone();
                Karta stol_2 = (Karta)player_2.myKarts.Peek().Clone();
                //Karta stol_3 = (Karta)player_3.myKarts.Peek().Clone();
                //Karta stol_4 = null;
                

                Console.WriteLine($"ход игрока 1 {stol_1}");
                Console.WriteLine($"ход игрока 2 {stol_2}");

                if ((player_3 == null) && (player_4 == null))
                {
                    int tmp_here = 1;
                    foreach (var item in player_1.myKarts)
                    {
                        Console.WriteLine($"{tmp_here++} {player_1} {item}");
                    }
                    int tmp_here2 = 1;
                    foreach (var item in player_2.myKarts)
                    {
                        Console.WriteLine($"{tmp_here2++} {player_2} {item}");
                    }
                    if (player_1.Is_won())
                    {
                        winner = player_1;
                        break;
                    }
                    if (player_2.Is_won())
                    {
                        winner = player_2;
                        break;
                    }
                    Console.WriteLine("Prikup bil: ");
                    foreach (Karta item in prikup)
                    {
                        Console.WriteLine(item);
                    }
                    player_1.Hod_part1();
                    player_2.Hod_part1();
                    if (stol_1.type > stol_2.type)
                    {
                        player_1.Hod_part2();
                        player_2.Hod_part2();
                        foreach (Karta item in prikup)
                        {
                            player_1.TakeKart(item);
                        }
                        prikup.Clear();
                        player_1.TakeKart(stol_1);
                        player_1.TakeKart(stol_2);
                    }
                    if (stol_2.type > stol_1.type)
                    {
                        player_1.Hod_part2();
                        player_2.Hod_part2();
                        foreach (Karta item in prikup)
                        {
                            player_2.TakeKart(item);
                        }
                        prikup.Clear();
                        player_2.TakeKart(stol_1);
                        player_2.TakeKart(stol_2);
                    }
                    if (stol_1.type == stol_2.type)
                    {
                        player_1.Hod_part2();
                        player_2.Hod_part2();
                        prikup.Add(new Karta(stol_1.type, stol_1.mast));
                        prikup.Add(new Karta(stol_2.type, stol_2.mast));
                        Console.WriteLine("Prikup stal: ");
                        foreach (Karta item in prikup)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    
                }

                //if ((player_3 != null) && (player_4 == null))
                //{
                //    if (player_3.Is_won())
                //    {
                //        winner = player_3;
                //        break;
                //    }
                //    stol_3 = player_3.Hod();
                //    if ((stol_1.type > stol_2.type) && (stol_1.type > stol_3.type))
                //    {
                //        player_1.TakeKart(stol_1);
                //        player_1.TakeKart(stol_2);
                //        player_1.TakeKart(stol_3);
                //    }
                //    if ((stol_2.type > stol_1.type) && (stol_2.type > stol_3.type))
                //    {
                //        player_2.TakeKart(stol_1);
                //        player_2.TakeKart(stol_2);
                //        player_2.TakeKart(stol_3);
                //    }
                //    if ((stol_3.type > stol_1.type) && (stol_3.type > stol_2.type))
                //    {
                //        player_3.TakeKart(stol_1);
                //        player_3.TakeKart(stol_2);
                //        player_3.TakeKart(stol_3);
                //    }
                //    else
                //        continue;
                //}

                //if ((player_4 != null) && (player_3 != null))
                //{
                //    if (player_4.Is_won())
                //    {
                //        winner = player_4;
                //        break;
                //    }
                //    stol_4 = player_4.Hod();
                //    if ((stol_1.type > stol_2.type) && (stol_1.type > stol_3.type) && (stol_1.type > stol_4.type))
                //    {
                //        player_1.TakeKart(stol_1);
                //        player_1.TakeKart(stol_2);
                //        player_1.TakeKart(stol_3);
                //        player_1.TakeKart(stol_4);
                //    }
                //    if ((stol_2.type > stol_1.type) && (stol_2.type > stol_3.type) && (stol_2.type > stol_4.type))
                //    {
                //        player_2.TakeKart(stol_1);
                //        player_2.TakeKart(stol_2);
                //        player_2.TakeKart(stol_3);
                //        player_2.TakeKart(stol_4);
                //    }
                //    if ((stol_3.type > stol_1.type) && (stol_3.type > stol_2.type) && (stol_3.type > stol_4.type))
                //    {
                //        player_3.TakeKart(stol_1);
                //        player_3.TakeKart(stol_2);
                //        player_3.TakeKart(stol_3);
                //        player_3.TakeKart(stol_4);
                //    }
                //    if ((stol_4.type > stol_1.type) && (stol_4.type > stol_2.type) && (stol_4.type > stol_3.type))
                //    {
                //        player_4.TakeKart(stol_1);
                //        player_4.TakeKart(stol_2);
                //        player_4.TakeKart(stol_3);
                //        player_4.TakeKart(stol_4);
                //    }
                //    else
                //        continue;
                //}



            }
            Console.WriteLine($"The winner is {winner}");

        }
    }
}
