using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_17.Products
{
    public class Computer : IProduct
    {
        public string ProductName { get => "Компьютер"; }
        public int ProductCode { get => 55; }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
