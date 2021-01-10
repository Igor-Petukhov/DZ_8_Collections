using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_8_Collections
{
    class Player
    {
        public string name { get; set; }
        public Queue<Karta> myKarts = new Queue<Karta>();
        public bool Is_won()
        {
            if (myKarts.Count == 36)
            {
                return true;
            }
            else
                return false;
        }
        public Karta Hod_part1()
        {
            Console.WriteLine($"\t{name}: кладу {myKarts.Peek()} осталось на руках {myKarts.Count - 1}");
            return myKarts.Peek();
        }
        public void Hod_part2()
        {
            myKarts.Dequeue();
        }
        public void TakeKart(Karta k)
        {
            Karta newKart = (Karta)k.Clone();
            Console.WriteLine($"{name}: беру себе {k}");
            myKarts.Enqueue(newKart);
        }
        public override string ToString()
        {
            return $"{name} на руках: {myKarts.Count} карт";
        }
        public void Show_my_cards()
        {
            foreach (var item in myKarts)
            {
                Console.WriteLine($"{name} {item}");
            }
        }
        public Player(string name)
        {
            this.name = name;
        }
    }
}
