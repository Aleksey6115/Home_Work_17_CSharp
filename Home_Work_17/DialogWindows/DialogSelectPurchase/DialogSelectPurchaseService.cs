using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;


namespace Home_Work_17.DialogWindows.DialogSelectPurchase
{
    public class DialogSelectPurchaseService
    {
        public void OpenDialogSelectPurchase(OleDbDataReader reader)
        {
            DialogSelectPurchaseWindow DSPW = new DialogSelectPurchaseWindow();
            DSPW.dataPurchase.ItemsSource = reader;

            DSPW.ShowDialog();
        }
    }
}
