using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KG_LAB1.Models.ColorSchemas
{
    class HSV : BaseColorSchema
    {
        private double _h;
        
        private double _s;

        private double _v;
        public double H
        {
            get
            {
                return _h;
            }
            set
            {
                _h = (value + 360) % 360;
            }
        }

        public double S
        {
            get
            {
                return _s;
            }
            set
            {
                _s = value;
            }
        }

        public double V
        {
            get
            {
                return _v;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("Invalid V value");
                }

                _v = value;
            }
        }

        public void ChangeHColorWithNotify(double _h)
        {
            H = _h;
            _manager.Notify(this, "HsvChangedH");
        }

        public void ChangeSColorWithNotify(double _s)
        {
            S = _s;
            _manager.Notify(this, "HsvChangedS");
        }

        public void ChangeVColorWithNotify(double _v)
        {
            V = _v;
            _manager.Notify(this, "HsvChangedV");
        }
    }
}
