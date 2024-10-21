using DatabaseClassLibrary;

namespace TestProject;

public class UnitTest
{
	private EmployeeRepository _employeeRepository = new EmployeeRepository();

	[Fact]
	public void TestClearDatabase()
	{
		Helper.ClearDatabase();

		List<Employee> employees = _employeeRepository.GetAllEmployees();
		Assert.Empty(employees);
	}


	[Fact]
	public void TestAddEmployee()
	{
		Helper.ClearDatabase();

		Employee employeeToAdd = new Employee
		{
			LastName = "��������",
			FirstName = "�������",
			Patronymic = "�������������",
			Gender = Gender.Male,
			DateOfBirth = new DateTime(1972, 3, 23),
			MaritalStatus = MaritalStatus.Single,
			HasChildren = false,
			Position = "��������",
			AcademicDegree = AcademicDegree.None
		};
		_employeeRepository.AddEmployee(employeeToAdd);
		

		List<Employee> employees = _employeeRepository.GetAllEmployees();
		Assert.Single(employees);

		Employee employee = employees[0];
		Assert.True(employee.LastName == "��������");
		Assert.True(employee.FirstName == "�������");
		Assert.True(employee.Patronymic == "�������������");
		Assert.True(employee.Gender == Gender.Male);
		Assert.True(employee.DateOfBirth == new DateTime(1972, 3, 23));
		Assert.True(employee.MaritalStatus == MaritalStatus.Single);
		Assert.True(employee.HasChildren == false);
		Assert.True(employee.Position == "��������");
		Assert.True(employee.AcademicDegree == AcademicDegree.None);
	}


	[Fact]
	public void TestAddEmployees()
	{
		Helper.ClearDatabase();

		Employee emp1 = new Employee
		{
			LastName = "��������",
			FirstName = "�������",
			Patronymic = "�������������",
			Gender = Gender.Male,
			DateOfBirth = new DateTime(1972, 3, 23),
			MaritalStatus = MaritalStatus.Single,
			HasChildren = false,
			Position = "��������",
			AcademicDegree = AcademicDegree.None
		};
		Employee emp2 = new Employee
		{
			LastName = "������",
			FirstName = "����",
			Patronymic = "����������",
			Gender = Gender.Male,
			DateOfBirth = new DateTime(1999, 3, 18),
			MaritalStatus = MaritalStatus.Married,
			HasChildren = true,
			Position = "�������-�������������",
			AcademicDegree = AcademicDegree.None
		};
		_employeeRepository.AddEmployee(emp1);
		_employeeRepository.AddEmployee(emp2);
		

		List<Employee> employees = _employeeRepository.GetAllEmployees();
		Assert.Equal(2, employees.Count);

		Employee employee1 = employees[0];
		Assert.True(employee1.LastName == "��������");
		Assert.True(employee1.FirstName == "�������");
		Assert.True(employee1.Patronymic == "�������������");
		Assert.True(employee1.Gender == Gender.Male);
		Assert.True(employee1.DateOfBirth == new DateTime(1972, 3, 23));
		Assert.True(employee1.MaritalStatus == MaritalStatus.Single);
		Assert.True(employee1.HasChildren == false);
		Assert.True(employee1.Position == "��������");
		Assert.True(employee1.AcademicDegree == AcademicDegree.None);

		Employee employee2 = employees[1];
		Assert.True(employee2.LastName == "������");
		Assert.True(employee2.FirstName == "����");
		Assert.True(employee2.Patronymic == "����������");
		Assert.True(employee2.Gender == Gender.Male);
		Assert.True(employee2.DateOfBirth == new DateTime(1999, 3, 18));
		Assert.True(employee2.MaritalStatus == MaritalStatus.Married);
		Assert.True(employee2.HasChildren == true);
		Assert.True(employee2.Position == "�������-�������������");
		Assert.True(employee2.AcademicDegree == AcademicDegree.None);
	}


	[Fact]
	public void TestEditEmployee()
	{
		Helper.ClearDatabase();

		Employee employeeToAdd = new Employee
		{
			LastName = "��������",
			FirstName = "�������",
			Patronymic = "�������������",
			Gender = Gender.Male,
			DateOfBirth = new DateTime(1972, 3, 23),
			MaritalStatus = MaritalStatus.Single,
			HasChildren = false,
			Position = "��������",
			AcademicDegree = AcademicDegree.None
		};
		_employeeRepository.AddEmployee(employeeToAdd);
		

		Employee employee = _employeeRepository.GetAllEmployees().First();
		employee.LastName = "������";
		employee.FirstName = "����";
		employee.Patronymic = "����������";
		employee.Gender = Gender.Male;
		employee.DateOfBirth = new DateTime(1999, 3, 18);
		employee.MaritalStatus = MaritalStatus.Married;
		employee.HasChildren = true;
		employee.Position = "�������-�������������";
		employee.AcademicDegree = AcademicDegree.None;
		_employeeRepository.UpdateEmployee(employee);
		

		List<Employee> employees = _employeeRepository.GetAllEmployees();
		Assert.Single(employees);

		Employee emp = employees[0];
		Assert.True(emp.LastName == "������");
		Assert.True(emp.FirstName == "����");
		Assert.True(emp.Patronymic == "����������");
		Assert.True(emp.Gender == Gender.Male);
		Assert.True(emp.DateOfBirth == new DateTime(1999, 3, 18));
		Assert.True(emp.MaritalStatus == MaritalStatus.Married);
		Assert.True(emp.HasChildren == true);
		Assert.True(emp.Position == "�������-�������������");
		Assert.True(emp.AcademicDegree == AcademicDegree.None);
	}


	[Fact]
	public void TestDeleteEmployee()
	{
		Helper.ClearDatabase();

		Employee employeeToAdd = new Employee
		{
			LastName = "��������",
			FirstName = "�������",
			Patronymic = "�������������",
			Gender = Gender.Male,
			DateOfBirth = new DateTime(1972, 3, 23),
			MaritalStatus = MaritalStatus.Single,
			HasChildren = false,
			Position = "��������",
			AcademicDegree = AcademicDegree.None
		};
		_employeeRepository.AddEmployee(employeeToAdd);
		

		Employee employee = _employeeRepository.GetAllEmployees().First();
		_employeeRepository.DeleteEmployee(employee.Id);


		List<Employee> employees = _employeeRepository.GetAllEmployees();
		Assert.Empty(employees);
	}


	[Fact]
	public void TestDeleteEmployees()
	{
		Helper.ClearDatabase();

		Employee employee1 = new Employee
		{
			LastName = "��������",
			FirstName = "�������",
			Patronymic = "�������������",
			Gender = Gender.Male,
			DateOfBirth = new DateTime(1972, 3, 23),
			MaritalStatus = MaritalStatus.Single,
			HasChildren = false,
			Position = "��������",
			AcademicDegree = AcademicDegree.None
		};
		Employee employee2 = new Employee
		{
			LastName = "������",
			FirstName = "����",
			Patronymic = "����������",
			Gender = Gender.Male,
			DateOfBirth = new DateTime(1999, 3, 18),
			MaritalStatus = MaritalStatus.Married,
			HasChildren = true,
			Position = "�������-�������������",
			AcademicDegree = AcademicDegree.None
		};
		_employeeRepository.AddEmployee(employee1);
		_employeeRepository.AddEmployee(employee2);
		

		List<Employee> employeesToDelete = _employeeRepository.GetAllEmployees();
		foreach (Employee employee in employeesToDelete)
			_employeeRepository.DeleteEmployee(employee.Id);
		

		List<Employee> employees = _employeeRepository.GetAllEmployees();
		Assert.Empty(employees);
	}


	[Fact]
	public void TestReadAll()
	{
		Helper.SeedDatabase();

		List<Employee> employees = _employeeRepository.GetAllEmployees();
		Assert.Equal(10, employees.Count);
	}


	[Fact]
	public void TestFilter1()
	{
		Helper.SeedDatabase();

		FilterOptions options = new FilterOptions() 
		{ 
			Gender = Gender.Male 
		};

		List<Employee> employees = Helper.FilterEmployees(_employeeRepository.GetAllEmployees(), options);
		Assert.Equal(6, employees.Count);
	}


	[Fact]
	public void TestFilter2()
	{
		Helper.SeedDatabase();

		FilterOptions options = new FilterOptions()
		{
			Gender = Gender.Male,
			MaritalStatus = MaritalStatus.Married
		};

		List<Employee> employees = Helper.FilterEmployees(_employeeRepository.GetAllEmployees(), options);
		Assert.Equal(2, employees.Count);
	}


	[Fact]
	public void TestFilter3()
	{
		Helper.SeedDatabase();

		FilterOptions options = new FilterOptions()
		{
			Gender = Gender.Male,
			MaritalStatus = MaritalStatus.Married,
			FirstName = "����"
		};

		List<Employee> employees = Helper.FilterEmployees(_employeeRepository.GetAllEmployees(), options);
		Assert.Single(employees);
	}


	[Fact]
	public void TestFilter4()
	{
		Helper.SeedDatabase();

		FilterOptions options = new FilterOptions()
		{
			Gender = Gender.Male,
			MaritalStatus = MaritalStatus.Married,
			FirstName = "����",
			LastName = "������"
		};

		List<Employee> employees = Helper.FilterEmployees(_employeeRepository.GetAllEmployees(), options);
		Assert.Empty(employees);
	}
}
