using DatabaseClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private User _loggedUser;

		public MainWindow(User user)
		{
			InitializeComponent();
			_loggedUser = user;

			if (_loggedUser.Role == UserRole.HRStaff)
				EditTab.IsEnabled = true;
			else
				EditTab.IsEnabled = false;

			LoadEmployees();
		}

		private void LoadEmployees()
		{
			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
				EmployeesDataGrid.ItemsSource = employees;
			}
		}
	}
}
