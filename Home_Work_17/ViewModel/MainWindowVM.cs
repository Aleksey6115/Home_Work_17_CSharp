using System;
using MVVMWpfLibraryBase;
using System.Windows;
using Home_Work_17.Service;
using Home_Work_17.DialogWindows.DialogAuthorization;
using Home_Work_17.DialogWindows.DialogAddPurchase;
using Home_Work_17.DialogWindows.DialogSelectPurchase;
using System.Data;

namespace Home_Work_17.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        #region Поля
        private DataTable dataClient = new DataTable(); // Таблица Client
        private DataRowView selectedRowClients; // Выбранный элемент таблицы Clients
        private DataTable dataProduct = new DataTable(); // Таблица Products
        private DataRowView selectedRowProducts; // Выбранный элемент таблицы Products

        #endregion

        #region Свойства

        /// <summary>
        /// Таблица Clients
        /// </summary>
        public DataTable DataClient
        {
            get => dataClient;
            set => Set(ref dataClient, value);
        }

        /// <summary>
        /// Выбранный элемент таблицы Clients
        /// </summary>
        public DataRowView SelectRowClients
        {
            get => selectedRowClients;
            set => Set(ref selectedRowClients, value);
        }

        /// <summary>
        /// Таблица Products
        /// </summary>
        public DataTable DataProduct
        {
            get => dataProduct;
            set => Set(ref dataProduct, value);
        }

        /// <summary>
        /// Выбранный элемент таблицы Products
        /// </summary>
        public DataRowView SelectedRowProducts
        {
            get => selectedRowProducts;
            set => Set(ref selectedRowProducts, value);
        }
        #endregion

        #region Сервисы
        DataBaseService DBService = new DataBaseService(); // Сервис работы с БД
        DialogAuthorizationService AuthorizationService = new DialogAuthorizationService(); // Сервис авторизации
        DialogAddPurchaseService AddPurchaseService = new DialogAddPurchaseService(); // Сервис окна "Добавить покупку"
        DialogSelectPurchaseService SelectPurchaseService = new DialogSelectPurchaseService(); // Сервис окна выборки
        #endregion

        #region Комманды
        private RelayCommand connectionOpenCommand; // Комманда открыть подключение
        private RelayCommand connectionCloseCommand; // Комманда закрыть подключение
        private RelayCommand viewStatusCommand; // Показать статус подключения
        private RelayCommand addClientCommand; // Добавить нового клиента
        private RelayCommand dataBaseUpdateCommand; // Сохранить изменения в БД
        private RelayCommand deleteClientCommand; // Удалить клиента
        private RelayCommand clientsClearCommand; // Очистить таблицу клиентов
        private RelayCommand addPurchaseCommand; // Добавить покупку
        private RelayCommand deletePurchaseCommand; // Отменить покупку
        private RelayCommand productClearCommand; // Очистить таблицу покупок
        private RelayCommand selectPurchaseCommand; // Выборка товаров

        /// <summary>
        /// Комманда открыть подключение
        /// </summary>
        public RelayCommand ConnectionOpenCommand
        {
            get
            {
                return connectionOpenCommand ?? (connectionOpenCommand = new RelayCommand(async obj =>
                {
                        if (AuthorizationService.OpenAuthorizationDialog())
                        {
                            if (await DBService.DataBaseConnectionOpenAsync())
                            {
                                DataClient = DBService.DataSetClients;
                                DataProduct = DBService.DataSetProducts;
                            }
                        }
                },
                obj =>
                {
                    if (DBService.ConnectFlag == false) return true;
                    else return false;
                }));
            }
        }

        /// <summary>
        /// Комманда закрыть подключение
        /// </summary>
        public RelayCommand ConnectionCloseCommand
        {
            get
            {
                return connectionCloseCommand ?? (connectionCloseCommand = new RelayCommand(obj =>
                {
                    DBService.DataBaseConnectionClose();
                },
                obj =>
                {
                    if (DBService.ConnectFlag == true) return true;
                    else return false;
                }));
            }
        }

        /// <summary>
        /// Открыть окно "Статус"
        /// </summary>
        public RelayCommand ViewStatusCommand
        {
            get
            {
                return viewStatusCommand ?? (viewStatusCommand = new RelayCommand(obj =>
                {
                    MessageBox.Show($"{DBService.SqlConnectStatus}\n{DBService.OleDbConnectStatus}", "Статус подключения", MessageBoxButton.OK,
                           MessageBoxImage.Information);
                }));
            }
        }

        /// <summary>
        /// Добавить нового клиента
        /// </summary>
        public RelayCommand AddClientCommand
        {
            get
            {
                return addClientCommand ?? (addClientCommand = new RelayCommand(obj =>
                {
                    DataRow row = DataClient.NewRow();
                    DataClient.Rows.Add(row);
                },
                obj =>
                {
                    if (DBService.ConnectFlag == true) return true;
                    else return false;
                }));
            }
        }

        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        public RelayCommand DataBaseUpdateCommand
        {
            get
            {
                return dataBaseUpdateCommand ?? (dataBaseUpdateCommand = new RelayCommand(obj =>
                {
                    DBService.DataBaseUpdateAsync();
                },
                obj =>
                {
                    if (DBService.ConnectFlag == true) return true;
                    else return false;
                }));
            }
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        public RelayCommand DeleteClientCommand
        {
            get
            {
                return deleteClientCommand ?? (deleteClientCommand = new RelayCommand(obj =>

                {
                    SelectRowClients.Row.Delete();
                },
                obj =>
                {
                    if (SelectRowClients != null) return true;
                    else return false;
                }));
            }
        }

        /// <summary>
        /// Очистить таблицу с клиентами
        /// </summary>
        public RelayCommand ClientsClearCommand
        {
            get
            {
                return clientsClearCommand ?? (clientsClearCommand = new RelayCommand(obj =>
                {
                    DataClient.Rows.Clear();
                },
                obj =>
                {
                    if (DataClient.Rows.Count > 0) return true;
                    else return false;
                }));
            }
        }

        /// <summary>
        /// Добавить покупку
        /// </summary>
        public RelayCommand AddPurchaseCommand
        {
            get
            {
                return addPurchaseCommand ?? (addPurchaseCommand = new RelayCommand(obj =>
                {
                    if (AddPurchaseService.OpenAddPurchaseDialog() == true)
                    {
                        DataRow row = DataProduct.NewRow();
                        row["email"] = SelectRowClients.Row["email"];
                        row["productCode"] = AddPurchaseService.SelectedProduct.ProductCode;
                        row["productName"] = AddPurchaseService.SelectedProduct.ProductName;
                        DataProduct.Rows.Add(row);
                    }
                },
                obj =>
                {
                    if (SelectRowClients != null) return true;
                    else return false;
                }));
            }
        }

        /// <summary>
        /// Отменить покупку
        /// </summary>
        public RelayCommand DeletePurchaseCommand
        {
            get
            {
                return deletePurchaseCommand ?? (deletePurchaseCommand = new RelayCommand(obj =>
                {
                    SelectedRowProducts.Row.Delete();
                },
                obj =>
                {
                    if (SelectedRowProducts != null) return true;
                    else return false;
                }));
            }
        }

        /// <summary>
        /// Очистить таблицу покупок
        /// </summary>
        public RelayCommand ProductClearCommand
        {
            get
            {
                return productClearCommand ?? (productClearCommand = new RelayCommand(obj =>
                {
                    DataProduct.Rows.Clear();
                },
                obj =>
                {
                    if (DataProduct.Rows.Count > 0) return true;
                    else return false;
                }));
            }
        }

        /// <summary>
        /// Выборка товаров
        /// </summary>
        public RelayCommand SelectPurchaseCommand
        {
            get
            {
                return selectPurchaseCommand ?? (selectPurchaseCommand = new RelayCommand(obj =>
                {
                    SelectPurchaseService.OpenDialogSelectPurchase(DBService.DBselectFromProduct(selectedRowClients.Row["email"] as string));
                },
                obj =>
                {
                    if (SelectRowClients != null) return true;
                    else return false;
                }));
            }
        }
        #endregion
    }
}
