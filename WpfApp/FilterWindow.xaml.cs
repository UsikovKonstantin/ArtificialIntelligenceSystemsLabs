using DatabaseClassLibrary;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
	/// <summary>
	/// Логика взаимодействия для FilterWindow.xaml
	/// </summary>
	public partial class FilterWindow : Window
	{
		public FilterOptions FilterOptions { get; set; }

        public FilterWindow()
		{
			InitializeComponent();

			FilterOptions = new FilterOptions();

			FillComboBoxes();
		}

		private void FillComboBoxes()
		{
			List<string> genders = GetEnumDisplayValues<Gender>();
			genders.Insert(0, "Не важно");
			comboBoxGender.ItemsSource = genders;
			comboBoxGender.SelectedIndex = 0;

			List<string> maritalStatuses = GetEnumDisplayValues<MaritalStatus>();
			maritalStatuses.Insert(0, "Не важно");
			comboBoxMaritalStatus.ItemsSource = maritalStatuses;
			comboBoxMaritalStatus.SelectedIndex = 0;

			List<string> hasChildren = new List<string> { "Не важно", "Есть", "Нет" };
			comboBoxHasChildren.ItemsSource = hasChildren;
			comboBoxHasChildren.SelectedIndex = 0;

			List<string> academicDegrees = GetEnumDisplayValues<AcademicDegree>();
			academicDegrees.Insert(0, "Не важно");
			comboBoxAcademicDegree.ItemsSource = academicDegrees;
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

		private void buttonFilter_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void textBoxLastName_TextChanged(object sender, TextChangedEventArgs e)
		{
			FilterOptions.LastName = textBoxLastName.Text.Trim();
		}

		private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
		{
			FilterOptions.FirstName = textBoxFirstName.Text.Trim();
		}

		private void textBoxPatronymic_TextChanged(object sender, TextChangedEventArgs e)
		{
			FilterOptions.Patronymic = textBoxPatronymic.Text.Trim();
		}

		private void comboBoxGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				FilterOptions.Gender = GetEnumValueFromDisplayName<Gender>(comboBoxGender.SelectedItem.ToString()!);
			}
			catch (Exception)
			{
				FilterOptions.Gender = null;
			}
		}

		private void comboBoxMaritalStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				FilterOptions.MaritalStatus = GetEnumValueFromDisplayName<MaritalStatus>(comboBoxMaritalStatus.SelectedItem.ToString()!);
			}
			catch (Exception)
			{
				FilterOptions.MaritalStatus = null;
			}
		}

		private void comboBoxHasChildren_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (comboBoxHasChildren.SelectedItem.ToString() == "Не важно")
			{
				FilterOptions.HasChildren = null;
			}
			else if (comboBoxHasChildren.SelectedItem.ToString() == "Есть")
			{
				FilterOptions.HasChildren = true;
			}
			else if (comboBoxHasChildren.SelectedItem.ToString() == "Нет")
			{
				FilterOptions.HasChildren = false;
			}
		}

		private void textBoxPosition_TextChanged(object sender, TextChangedEventArgs e)
		{
			FilterOptions.Position = textBoxPosition.Text.Trim();
		}

		private void comboBoxAcademicDegree_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				FilterOptions.AcademicDegree = GetEnumValueFromDisplayName<AcademicDegree>(comboBoxAcademicDegree.SelectedItem.ToString()!);
			}
			catch (Exception)
			{
				FilterOptions.AcademicDegree = null;
			}
		}
	}
}
