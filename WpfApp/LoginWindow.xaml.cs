using DatabaseClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
			Seeder.SeedDatabase();
		}

		private void buttonLogin_Click(object sender, RoutedEventArgs e)
		{
			string login = textBoxLogin.Text;
			string password = textBoxPassword.Password;

			using (LaboratoryContext context = new LaboratoryContext())
			{
				User? user = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
				if (user != null)
				{
					MainWindow mainWindow = new MainWindow(user);
					mainWindow.Show();
					Close();
				}
				else
				{
					MessageBox.Show("Неверный логин или пароль.");
				}
			}
		}
	}
}
