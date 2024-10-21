using DatabaseClassLibrary;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;

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

			LoadAllEmployees();
			buttonEdit.IsEnabled = false;
			buttonDelete.IsEnabled = false;
		}

		private void LoadAllEmployees()
		{
			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
				dataGridEmployeesView.ItemsSource = employees;
				dataGridEmployeesEdit.ItemsSource = employees;
			}
		}

		private void buttonFilter_Click(object sender, RoutedEventArgs e)
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

				dataGridEmployeesView.ItemsSource = employees;
			}
		}

		private void buttonReport_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
				DefaultExt = ".csv",
				FileName = "employees"
			};

			if (saveFileDialog.ShowDialog() == true)
			{
				string filePath = saveFileDialog.FileName;

				using (StreamWriter writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
				{
					string[] headers = dataGridEmployeesView.Columns
						.Select(column => column.Header.ToString())
						.ToArray()!;
					writer.WriteLine(string.Join(",", headers));

					foreach (object? employee in dataGridEmployeesView.Items)
						if (employee is Employee emp)
							writer.WriteLine($"{emp.LastName},{emp.FirstName},{emp.Patronymic},{emp.GenderDisplay}," +
								$"{emp.DateOfBirth:dd.MM.yyyy},{emp.MaritalStatusDisplay},{(emp.HasChildren ? "Есть" : "Нет")}," +
								$"{emp.Position},{emp.AcademicDegreeDisplay}");
				}

				MessageBox.Show($"CSV файл успешно создан: {filePath}");
			}
		}

		private void dataGridEmployeesEdit_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			buttonEdit.IsEnabled = dataGridEmployeesEdit.SelectedItems.Count == 1;
			buttonDelete.IsEnabled = dataGridEmployeesEdit.SelectedItems.Count > 0;
		}

		private void buttonAdd_Click(object sender, RoutedEventArgs e)
		{
			UpsertWindow insertWindow = new UpsertWindow();
			insertWindow.ShowDialog();
			LoadAllEmployees();
		}

		private void buttonEdit_Click(object sender, RoutedEventArgs e)
		{
			UpsertWindow addWindow = new UpsertWindow((dataGridEmployeesEdit.SelectedItem as Employee)!);
			addWindow.ShowDialog();
			LoadAllEmployees();
		}

		private void buttonDelete_Click(object sender, RoutedEventArgs e)
		{
			List<Employee> selectedEmployees = dataGridEmployeesEdit.SelectedItems.Cast<Employee>().ToList();

			MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить {selectedEmployees.Count} сотрудника(ов)?", "Подтверждение удаления", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				using (LaboratoryContext context = new LaboratoryContext())
				{
					foreach (Employee employee in selectedEmployees)
						context.Employees.Remove(employee); 
					context.SaveChanges(); 
				}

				LoadAllEmployees();
			}
		}
	}
}
