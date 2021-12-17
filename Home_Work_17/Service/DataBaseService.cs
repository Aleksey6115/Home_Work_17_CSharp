using System;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Data;

namespace Home_Work_17.Service
{
    /// <summary>
    /// Работа с Базами данных
    /// </summary>
    public class DataBaseService
    {
        #region Поля
        SqlConnection SqlConnect; // Подключение SQL
        OleDbConnection OleDbConnect; // Подключение к Access
        private bool connectFlag; // Есть ли подключение
        private DataTable dataTableClients; // DataTable for Clients
        private DataTable dataTableProducts; // DataSet for Products
        private SqlDataAdapter adapterSql; // adapter for sql
        private OleDbDataAdapter adapterOleDb; // adapter for oleDb
        #endregion

        #region Свойства

        /// <summary>
        /// Статус подключения к БД SQL
        /// </summary>
        public string SqlConnectStatus
        {
            get => $"Состояние подключения к БД SQL: {SqlConnect.State}";
        }

        /// <summary>
        /// Статус подключения к БД Access
        /// </summary>
        public string OleDbConnectStatus
        {
            get => $"Состояние подключения к БД Access: {OleDbConnect.State}";
        }

        /// <summary>
        /// Есть ли подключение
        /// </summary>
        public bool ConnectFlag
        {
            get => connectFlag;
        }

        /// <summary>
        /// DataSet for Clients
        /// </summary>
        public DataTable DataSetClients
        {
            get => dataTableClients;
        }

        /// <summary>
        /// DataSet for Products
        /// </summary>
        public DataTable DataSetProducts
        {
            get => dataTableProducts;
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор класса DataBaseService
        /// </summary>
        public DataBaseService()
        {
            SqlConnect = new SqlConnection();
            OleDbConnect = new OleDbConnection();
            connectFlag = false;
            dataTableClients = new DataTable();
            dataTableProducts = new DataTable();
        }
        #endregion

        #region Методы

        /// <summary>
        /// Метод подключается к базам данных
        /// </summary>
        /// <returns></returns>
        private bool DataBaseConnectionOpen()
        {
            SqlConnect = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionToSql"].ConnectionString);
            OleDbConnect = new OleDbConnection(ConfigurationManager.ConnectionStrings["connectionToAccess"].ConnectionString);
            string sqlCom = "select * from Clients";
            string OleDBCom = "select * from Product";

            try
            {
                SqlConnect.Open();
                adapterSql = new SqlDataAdapter(sqlCom, SqlConnect);
                adapterSql.Fill(dataTableClients);

                OleDbConnect.Open();
                adapterOleDb = new OleDbDataAdapter(OleDBCom, OleDbConnect);
                adapterOleDb.Fill(dataTableProducts);

                connectFlag = true;
                MessageBox.Show($"{SqlConnectStatus}\n{OleDbConnectStatus}", "Статус подключения", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return true;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Метод закрывает подключение
        /// </summary>
        public void DataBaseConnectionClose()
        {
            try
            {
                SqlConnect.Close();
                MessageBox.Show("Подключение закрыто", "Статус подключения", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обновление БД
        /// </summary>
        private void DataBaseUpdate()
        {
            if (connectFlag == true)
            {
                try
                {
                    SqlCommandBuilder comSql = new SqlCommandBuilder(adapterSql);
                    adapterSql.Update(dataTableClients);

                    OleDbCommandBuilder comOleDb = new OleDbCommandBuilder(adapterOleDb);
                    adapterOleDb.Update(dataTableProducts);

                    MessageBox.Show("БД обновлена", "Обновление БД", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Обновление БД", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Получить выборку по покупкам
        /// </summary>
        /// <param name="emailClient"></param>
        /// <returns></returns>
        public OleDbDataReader DBselectFromProduct(string emailClient)
        {
            string comOleDb = "select * from Product where email = @email";
            OleDbDataReader reader = null;

            if (connectFlag == true)
            {
                OleDbCommand command = new OleDbCommand(comOleDb, OleDbConnect);
                OleDbParameter emailParam = new OleDbParameter("@email", emailClient);
                command.Parameters.Add(emailParam);
                reader = command.ExecuteReader();

                return reader;
            }
            return reader;
        }
        #endregion

        #region Задачи

        /// <summary>
        /// Ассинхронный запуск метода DataBaseConnectionOpen
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DataBaseConnectionOpenAsync()
        {
            return await Task.Run(() => DataBaseConnectionOpen());
        }

        /// <summary>
        /// Ассинхронный запуск метода DataBaseUpdate
        /// </summary>
        public async void DataBaseUpdateAsync()
        {
            await Task.Run(() => DataBaseUpdate());
        }
        #endregion
    }
}
