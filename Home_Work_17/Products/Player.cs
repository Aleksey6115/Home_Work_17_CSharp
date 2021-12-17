using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_17.Products
{
    public class Player : IProduct
    {
        public string ProductName { get => "MP3 Плеер"; }
        public int ProductCode { get => 33; }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
