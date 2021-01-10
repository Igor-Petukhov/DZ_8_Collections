using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_8_Collections
{
    class Karta
    {
        public int type { get; set; }

        private string _mast;
        public string mast
        {
            get { return mast; }
            set 
            {
                if (value != "Пика" && value != "Трефа" && value != "Бубна" && value != "Черва")
                    throw new ArgumentOutOfRangeException($"{nameof(value)} Должно быть пика/трефа/бубна/черва!");
                mast = value;
            }
        }

    }
}
