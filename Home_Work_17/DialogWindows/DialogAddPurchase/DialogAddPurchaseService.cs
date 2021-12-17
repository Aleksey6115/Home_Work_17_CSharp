using System;
using System.Collections.Generic;
using System.Text;
using Home_Work_17.Products;
using System.Collections;
using System.Windows;

namespace Home_Work_17.DialogWindows.DialogAddPurchase
{
    public class DialogAddPurchaseService
    {
        /// <summary>
        /// Выбранный товар
        /// </summary>
        public IProduct SelectedProduct { get; set; }

        private List<IProduct> productList = new List<IProduct>() { new Telefon(), new Tv(), new Tablet(), new Player(), new Computer() };

        public bool OpenAddPurchaseDialog()
        {
            DialogAddPurchaseWindow DAPW = new DialogAddPurchaseWindow();
            DAPW.comboProduct.ItemsSource = productList;

            if (DAPW.ShowDialog() == true)

            {
                if (DAPW.comboProduct.SelectedItem == null) return false;

                  SelectedProduct = DAPW.comboProduct.SelectedItem as IProduct;
                  return true;
            }

            return false;
        }
    }
}
