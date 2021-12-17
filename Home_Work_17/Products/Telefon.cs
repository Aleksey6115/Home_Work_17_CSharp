using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_17.Products
{
    public class Telefon : IProduct
    {
        public string ProductName { get => "Телефон"; }
        public int ProductCode { get => 11; }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
