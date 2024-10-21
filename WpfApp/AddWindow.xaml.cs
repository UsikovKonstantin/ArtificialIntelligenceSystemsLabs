using DatabaseClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
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
	/// Логика взаимодействия для AddWindow.xaml
	/// </summary>
	public partial class AddWindow : Window
	{
		public AddWindow()
		{
			InitializeComponent();

			FillComboBoxes();

			datePickerDateOfBirth.SelectedDate = new DateTime(2000, 1, 1);
		}

		private void FillComboBoxes()
		{
			comboBoxGender.ItemsSource = GetEnumDisplayValues<Gender>();
			comboBoxGender.SelectedIndex = 0;

			comboBoxMaritalStatus.ItemsSource = GetEnumDisplayValues<MaritalStatus>();
			comboBoxMaritalStatus.SelectedIndex = 0;

			comboBoxAcademicDegree.ItemsSource = GetEnumDisplayValues<AcademicDegree>();
			comboBoxAcademicDegree.SelectedIndex = 0;
		}

		private List<string> GetEnumDisplayValues<T>() where T : Enum
		{
			return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
							.Select(f => f.GetCustomAttribute<DisplayAttribute>()?.Name ?? f.Name)
							.ToList();
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

		private void buttonAdd_Click(object sender, RoutedEventArgs e)
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

			using (LaboratoryContext context = new LaboratoryContext())
			{
				context.Add(employee);
				context.SaveChanges();
			}

			this.DialogResult = true;
		}
	}
}
