using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachines
{
    public class Coin
    {
        private double _weight;  //in grams
        private double _size;  //diameter in mm

        public Coin()
        {
            //Default to penny
            _weight = 2.5;
            _size = 19.05;
        }

        public Coin(double weight, double size)
        {
            _weight = weight;
            _size = size;
        }

        public decimal coinValue
        {
            get
            {
                //Check size and weight with an error of +-.05
                if (_weight >= 2.45 && _weight <= 2.55
                    && _size >= 19 && _size <= 19.1)
                {
                    return .01m;
                }
                else if (_weight >= 4.95 && _weight <= 5.05
                        && _size >= 21.16 && _size <= 21.26)
                {
                    return .05m;
                }
                else if (_weight >= 2.218 && _weight <= 2.318
                        && _size >= 17.86 && _size <= 17.96)
                {
                    return .1m;
                }
                else if (_weight >= 5.62 && _weight <= 5.72
                        && _size >= 24.21 && _size <= 24.31)
                {
                    return .25m;
                }
                else if (_weight >= 11.29 && _weight <= 11.39
                        && _size >= 30.56 && _size <= 30.66)
                {
                    return .5m;
                }
                else if (_weight >= 8.05 && _weight <= 8.15
                        && _size >= 26.44 && _size <= 26.54)
                {
                    return 1m;
                }
                else
                {
                    return 0m;
                }
            }

        }
    }
}
