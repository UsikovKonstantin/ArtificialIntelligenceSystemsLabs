using DatabaseClassLibrary;
using System.Security.Cryptography;

namespace TestProject
{
	public class UnitTest
	{
		[Fact]
		public void TestClearDatabase()
		{
			Helper.ClearDatabase();

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
				Assert.Empty(employees);
			}
		}

		[Fact]
		public void TestAddEmployee()
		{
			Helper.ClearDatabase();

			using (LaboratoryContext context = new LaboratoryContext())
			{
				Employee employee = new Employee
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
				context.Employees.Add(employee);
				context.SaveChanges();
			}

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
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
		}

		[Fact]
		public void TestAddEmployees()
		{
			Helper.ClearDatabase();

			using (LaboratoryContext context = new LaboratoryContext())
			{
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
				context.Employees.Add(employee1);
				context.Employees.Add(employee2);
				context.SaveChanges();
			}

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
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
		}

		[Fact]
		public void TestEditEmployee()
		{
			Helper.ClearDatabase();
			using (LaboratoryContext context = new LaboratoryContext())
			{
				Employee employee = new Employee
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
				context.Employees.Add(employee);
				context.SaveChanges();
			}

			using (LaboratoryContext context = new LaboratoryContext())
			{
				Employee employee = context.Employees.ToList().First();
				Employee emp = context.Employees.First(e => e.Id == employee.Id);
				emp.LastName = "������";
				emp.FirstName = "����";
				emp.Patronymic = "����������";
				emp.Gender = Gender.Male;
				emp.DateOfBirth = new DateTime(1999, 3, 18);
				emp.MaritalStatus = MaritalStatus.Married;
				emp.HasChildren = true;
				emp.Position = "�������-�������������";
				emp.AcademicDegree = AcademicDegree.None;
				context.SaveChanges();
			}

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
				Assert.Single(employees);

				Employee employee = employees[0];
				Assert.True(employee.LastName == "������");
				Assert.True(employee.FirstName == "����");
				Assert.True(employee.Patronymic == "����������");
				Assert.True(employee.Gender == Gender.Male);
				Assert.True(employee.DateOfBirth == new DateTime(1999, 3, 18));
				Assert.True(employee.MaritalStatus == MaritalStatus.Married);
				Assert.True(employee.HasChildren == true);
				Assert.True(employee.Position == "�������-�������������");
				Assert.True(employee.AcademicDegree == AcademicDegree.None);
			}
		}

		[Fact]
		public void TestDeleteEmployee()
		{
			Helper.ClearDatabase();
			using (LaboratoryContext context = new LaboratoryContext())
			{
				Employee employee = new Employee
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
				context.Employees.Add(employee);
				context.SaveChanges();
			}

			using (LaboratoryContext context = new LaboratoryContext())
			{
				Employee employee = context.Employees.ToList().First();
				context.Employees.Remove(employee);
				context.SaveChanges();
			}

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
				Assert.Empty(employees);
			}
		}

		[Fact]
		public void TestDeleteEmployees()
		{
			Helper.ClearDatabase();
			using (LaboratoryContext context = new LaboratoryContext())
			{
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
				context.Employees.Add(employee1);
				context.Employees.Add(employee2);
				context.SaveChanges();
			}

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
				foreach (Employee employee in employees)
					context.Employees.Remove(employee);
				context.SaveChanges();
			}

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
				Assert.Empty(employees);
			}
		}

		[Fact]
		public void TestReadAll()
		{
			Helper.SeedDatabase();

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = context.Employees.ToList();
				Assert.Equal(10, employees.Count);
			}
		}

		[Fact]
		public void TestFilter1()
		{
			Helper.SeedDatabase();

			FilterOptions options = new FilterOptions() 
			{ 
				Gender = Gender.Male 
			};

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = Helper.FilterEmployees(context.Employees.ToList(), options);

				Assert.Equal(6, employees.Count);
			}
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

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = Helper.FilterEmployees(context.Employees.ToList(), options);

				Assert.Equal(2, employees.Count);
			}
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

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = Helper.FilterEmployees(context.Employees.ToList(), options);

				Assert.Single(employees);
			}
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

			using (LaboratoryContext context = new LaboratoryContext())
			{
				List<Employee> employees = Helper.FilterEmployees(context.Employees.ToList(), options);

				Assert.Empty(employees);
			}
		}
	}
}