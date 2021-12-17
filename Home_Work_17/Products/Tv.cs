using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_17.Products
{
    public class Tv : IProduct
    {
        public string ProductName { get => "Телевизор"; }
        public int ProductCode { get => 22; }

        public override string ToString()
        {
            return ProductName;
        }

    }
}
