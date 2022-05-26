using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KG_LAB1.Models
{
    class BaseColorSchema
    {
        protected ColorsManager _manager;

        public BaseColorSchema(ColorsManager manager = null)
        {
            _manager = manager;
        }

        public void SetColorsManager(ColorsManager manager)
        {
            _manager = manager;
        }
    }
}
