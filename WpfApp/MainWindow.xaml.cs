using DatabaseClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			FilterWindow filterWindow = new FilterWindow();
			filterWindow.ShowDialog();

			FilterOptions filterOptions = filterWindow.FilterOptions;
			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList().Where(e =>
					e.LastName.ToLower().Contains(filterOptions.LastName.ToLower()) &&
					e.FirstName.ToLower().Contains(filterOptions.FirstName.ToLower()) &&
					e.Patronymic!.ToLower().Contains(filterOptions.Patronymic.ToLower()) &&
					(filterOptions.Gender == null || e.Gender == filterOptions.Gender) &&
					(filterOptions.MaritalStatus == null || e.MaritalStatus == filterOptions.MaritalStatus) &&
					(filterOptions.HasChildren == null || e.HasChildren == filterOptions.HasChildren) &&
					e.Position.ToLower().Contains(filterOptions.Position.ToLower()) &&
					(filterOptions.AcademicDegree == null || e.AcademicDegree == filterOptions.AcademicDegree)).ToList();

				EmployeesDataGrid.ItemsSource = employees;
			}
		}
	}
}
