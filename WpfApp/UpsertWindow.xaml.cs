using DatabaseClassLibrary;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для UpsertWindow.xaml
	/// </summary>
	public partial class UpsertWindow : Window
	{
		private EmployeeRepository _employeeRepository = new EmployeeRepository();
		private int _id;

		public UpsertWindow()
		{
			InitializeComponent();
			FillFields();
			_id = 0;
		}

		public UpsertWindow(Employee employee)
		{
			InitializeComponent();
			FillFields(employee);
			_id = employee.Id;
		}

		private void FillFields()
		{
			comboBoxGender.ItemsSource = GetEnumDisplayValues<Gender>();
			comboBoxGender.SelectedIndex = 0;

			comboBoxMaritalStatus.ItemsSource = GetEnumDisplayValues<MaritalStatus>();
			comboBoxMaritalStatus.SelectedIndex = 0;

			comboBoxAcademicDegree.ItemsSource = GetEnumDisplayValues<AcademicDegree>();
			comboBoxAcademicDegree.SelectedIndex = 0;

			datePickerDateOfBirth.SelectedDate = new DateTime(2000, 1, 1);

			buttonUpsert.Content = "Добавить";
		}

		private void FillFields(Employee employee)
		{
			textBoxLastName.Text = employee.LastName;
			textBoxFirstName.Text = employee.FirstName;
			textBoxPatronymic.Text = employee.Patronymic;

			comboBoxGender.ItemsSource = GetEnumDisplayValues<Gender>();
			comboBoxGender.SelectedItem = GetEnumDisplayValue(employee.Gender);

			datePickerDateOfBirth.SelectedDate = employee.DateOfBirth;

			comboBoxMaritalStatus.ItemsSource = GetEnumDisplayValues<MaritalStatus>();
			comboBoxMaritalStatus.SelectedItem = GetEnumDisplayValue(employee.MaritalStatus);

			checkBoxHasChildren.IsChecked = employee.HasChildren;

			textBoxPosition.Text = employee.Position;

			comboBoxAcademicDegree.ItemsSource = GetEnumDisplayValues<AcademicDegree>();
			comboBoxAcademicDegree.SelectedItem = GetEnumDisplayValue(employee.AcademicDegree);

			buttonUpsert.Content = "Изменить";
		}

		private List<string> GetEnumDisplayValues<T>() where T : Enum
		{
			return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
							.Select(f => f.GetCustomAttribute<DisplayAttribute>()?.Name ?? f.Name)
							.ToList();
		}

		private string GetEnumDisplayValue<T>(T enumValue) where T : Enum
		{
			return enumValue.GetType()
				.GetField(enumValue.ToString())
				.GetCustomAttributes(typeof(DisplayAttribute), false)
				.SingleOrDefault() is DisplayAttribute displayAttribute
				? displayAttribute.Name
				: enumValue.ToString();
		}

		public static T GetEnumValueFromDisplayName<T>(string displayName) where T : Enum
		{
			var type = typeof(T);
			foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
				if (displayAttribute != null && displayAttribute.Name == displayName)
				{
					return (T)field.GetValue(null)!;
				}
				if (field.Name == displayName)
				{
					return (T)field.GetValue(null)!;
				}
			}

			throw new ArgumentException($"Не удалось найти соответствующее значение для {displayName}");
		}

		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void buttonUpsert_Click(object sender, RoutedEventArgs e)
		{
			List<string> errors = new List<string>();
			if (textBoxLastName.Text.Trim() == "")
				errors.Add("Поле 'Фамилия' должно быть заполнено");
			if (textBoxFirstName.Text.Trim() == "")
				errors.Add("Поле 'Имя' должно быть заполнено");
			if (!datePickerDateOfBirth.SelectedDate.HasValue)
				errors.Add("Поле 'Дата рождения' должно быть заполнено");
			if (textBoxPosition.Text.Trim() == "")
				errors.Add("Поле 'Должность' должно быть заполнено");

			if (errors.Count > 0)
			{
				MessageBox.Show(string.Join("\n", errors));
				return;
			}

			Employee employee = new Employee();
			employee.LastName = textBoxLastName.Text.Trim();
			employee.FirstName = textBoxFirstName.Text.Trim();
			employee.Patronymic = textBoxPatronymic.Text.Trim();
			employee.Gender = GetEnumValueFromDisplayName<Gender>(comboBoxGender.SelectedItem.ToString()!);
			employee.DateOfBirth = datePickerDateOfBirth.SelectedDate!.Value;
			employee.MaritalStatus = GetEnumValueFromDisplayName<MaritalStatus>(comboBoxMaritalStatus.SelectedItem.ToString()!);
			employee.HasChildren = (bool)checkBoxHasChildren.IsChecked!;
			employee.Position = textBoxPosition.Text.Trim();
			employee.AcademicDegree = GetEnumValueFromDisplayName<AcademicDegree>(comboBoxAcademicDegree.SelectedItem.ToString()!);

			if (_id == 0)
			{
				_employeeRepository.AddEmployee(employee);
			}
			else
			{
				employee.Id = _id;
				_employeeRepository.UpdateEmployee(employee);
			}
			
			this.DialogResult = true;
		}
	}
}
