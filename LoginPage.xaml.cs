using Npgsql;
using Npgsql.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabaseApp
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private string sql = "Server=localhost; Port = 5432; Database= workers_db; User Id = postgres; Password = admin";
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ButtonLog(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            CheckData(login, password);
        }

        private void CheckData(string login, string password)
        {
            NpgsqlConnection sqlConnection = new NpgsqlConnection(sql);
            sqlConnection.Open();

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = sqlConnection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT * FROM public.workers";
            NpgsqlDataReader reader = command.ExecuteReader();

            bool userFound = false;

            while (reader.Read())
            {
                Worker user = new Worker();
                user.login = login;
                user.password = password;
                if (user.login.Equals(reader.GetString(3))
                    && user.password.Equals(reader.GetString(4)))
                {
                    user.id = reader.GetInt64(0);
                    user.surname = reader.GetString(1);
                    user.name = reader.GetString(2);

                    NavigationService.Navigate(new MainPage(user));
                    userFound = true;
                    break;
                }
            }
                if(!userFound)
                {
                    MessageBox.Show("Incorrect Data");
                }
                }
           
        }
            
        }
    
