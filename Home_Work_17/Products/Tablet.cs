using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_17.Products
{
    public class Tablet : IProduct
    {
        public string ProductName { get => "Планшет"; }
        public int ProductCode { get => 44; }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
