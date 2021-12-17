using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_17.Products
{
    /// <summary>
    /// Интерфейс отображает сущность товара
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Имя товара
        /// </summary>
        string ProductName { get; }

        /// <summary>
        /// Код товара
        /// </summary>
        int ProductCode { get; }
    }
}
