using System;

namespace KG_LAB1.Models
{
    class CMYK : BaseColorSchema
    {
        private double _c;

        private double _m;

        private double _y;

        private double _k;

        public double C
        {
            get
            {
                return _c;
            }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException("Incorrect C value");
                }

                _c = value;
            }
        }
        public double M
        {
            get
            {
                return _m;
            }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException("Incorrect M value");
                }

                _m = value;
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException("Incorrect Y value");
                }

                _y = value;
            }
        }
        public double K
        {
            get
            {
                return _k;
            }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException("Incorrect K value");
                }

                _k = value;
            }
        }

        public void ChangeCColorWithNotify(double _c)
        {
            C = _c;
            _manager.Notify(this, "CmykChangedC");
        }
        public void ChangeMColorWithNotify(double _m)
        {
            M = _m;
            _manager.Notify(this, "CmykChangedM");
        }

        public void ChangeYColorWithNotify(double _y)
        {
            Y = _y;
            _manager.Notify(this, "CmykChangedY");
        }

        public void ChangeKColorWithNotify(double _k)
        {
            K = _k;
            _manager.Notify(this, "CmykChangedK");
        }
    }
}
