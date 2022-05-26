using System;

namespace KG_LAB1.Models
{
    class RGB : BaseColorSchema
    {
        private int _r;

        private int _g;

        private int _b;

        public int R
        {
            get
            {
                return _r;
            }
            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentException("Incorrect R value");
                }

                _r = value;
            }
        }
        public int G
        {
            get
            {
                return _g;
            }
            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentException("Incorrect G value");
                }

                _g = value;
            }
        }
        public int B
        {
            get
            {
                return _b;
            }
            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentException("Incorrect B value");
                }

                _b = value;
            }
        }

        public void ChangeRColorWithNotify(int _r)
        {
            R = _r;
            _manager.Notify(this, "RgbChangedR");
        }

        public void ChangeGColorWithNotify(int _g)
        {
            G = _g;
            _manager.Notify(this, "RgbChangedG");
        }

        public void ChangeBColorWithNotify(int _b)
        {
            B = _b;
            _manager.Notify(this, "RgbChangedB");
        }
    }
}
