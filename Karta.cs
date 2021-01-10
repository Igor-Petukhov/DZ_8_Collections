using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_8_Collections
{
    class Karta : ICloneable
    {
        public int type { get; set; } //6-14

        private string _mast;
        public string mast
        {
            get { return _mast; }
            set 
            {
                if (value != "Пика" && value != "Трефа" && value != "Бубна" && value != "Черва")
                    throw new ArgumentOutOfRangeException($"{nameof(value)} Должно быть пика/трефа/бубна/черва!");
                mast = value;
            }
        }

        public override string ToString()
        {
            return $"{type} {_mast}";
        }
        public Karta(int type, string mast)
        {
            this._mast = mast;
            this.type = type;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
