using Microsoft.EntityFrameworkCore;

namespace DatabaseClassLibrary;

public static class Seeder
{
	public static void SeedDatabase()
	{
		using (LaboratoryContext context = new LaboratoryContext())
		{
			context.Database.Migrate();

			List<Employee> employees = context.Employees.ToList();
			context.Employees.RemoveRange(employees);
			context.SaveChanges();

			List<Employee> employeesToAdd = new List<Employee>
				{
					new Employee
					{
						LastName = "Филиппов",
						FirstName = "Алексей",
						Patronymic = "Владиславович",
						Gender = Gender.Male,
						DateOfBirth = new DateTime(1972, 3, 23),
						MaritalStatus = MaritalStatus.Single,
						HasChildren = false,
						Position = "Лаборант",
						AcademicDegree = AcademicDegree.None
					},
					new Employee
					{
						LastName = "Рубцов",
						FirstName = "Егор",
						Patronymic = "Михайлович",
						Gender = Gender.Male,
						DateOfBirth = new DateTime(1999, 3, 18),
						MaritalStatus = MaritalStatus.Married,
						HasChildren = true,
						Position = "Инженер-исследователь",
						AcademicDegree = AcademicDegree.None
					},
					new Employee
					{
						LastName = "Щербакова",
						FirstName = "Варвара",
						Patronymic = "Николаевна",
						Gender = Gender.Female,
						DateOfBirth = new DateTime(2000, 4, 7),
						MaritalStatus = MaritalStatus.Single,
						HasChildren = false,
						Position = "Младший научный сотрудник",
						AcademicDegree = AcademicDegree.Candidate
					},
					new Employee
					{
						LastName = "Герасимов",
						FirstName = "Роман",
						Patronymic = "Георгиевич",
						Gender = Gender.Male,
						DateOfBirth = new DateTime(1966, 1, 3),
						MaritalStatus = MaritalStatus.Married,
						HasChildren = false,
						Position = "Ведущий научный сотрудник",
						AcademicDegree = AcademicDegree.Doctor
					},
					new Employee
					{
						LastName = "Куликов",
						FirstName = "Максим",
						Patronymic = "Артурович",
						Gender = Gender.Male,
						DateOfBirth = new DateTime(1987, 11, 24),
						MaritalStatus = MaritalStatus.Single,
						HasChildren = false,
						Position = "Главный инженер",
						AcademicDegree = AcademicDegree.Doctor
					},
					new Employee
					{
						LastName = "Максимова",
						FirstName = "Олеся",
						Patronymic = "Львовна",
						Gender = Gender.Female,
						DateOfBirth = new DateTime(1964, 10, 10),
						MaritalStatus = MaritalStatus.Married,
						HasChildren = true,
						Position = "Руководитель лаборатории",
						AcademicDegree = AcademicDegree.Candidate
					},
					new Employee
					{
						LastName = "Ильин",
						FirstName = "Фёдор",
						Patronymic = "Анатольевич",
						Gender = Gender.Male,
						DateOfBirth = new DateTime(1987, 7, 19),
						MaritalStatus = MaritalStatus.Divorced,
						HasChildren = true,
						Position = "Техник лаборатории",
						AcademicDegree = AcademicDegree.None
					},
					new Employee
					{
						LastName = "Иванов",
						FirstName = "Фёдор",
						Patronymic = "Андреевич",
						Gender = Gender.Male,
						DateOfBirth = new DateTime(1960, 1, 15),
						MaritalStatus = MaritalStatus.Widowed,
						HasChildren = true,
						Position = "Ассистент",
						AcademicDegree = AcademicDegree.None
					},
					new Employee
					{
						LastName = "Климова",
						FirstName = "Алина",
						Patronymic = "Александровна",
						Gender = Gender.Female,
						DateOfBirth = new DateTime(1975, 9, 24),
						MaritalStatus = MaritalStatus.Divorced,
						HasChildren = false,
						Position = "Аналитик",
						AcademicDegree = AcademicDegree.Doctor
					},
					new Employee
					{
						LastName = "Новикова",
						FirstName = "Алиса",
						Patronymic = "Гордеевна",
						Gender = Gender.Female,
						DateOfBirth = new DateTime(1999, 5, 13),
						MaritalStatus = MaritalStatus.Widowed,
						HasChildren = true,
						Position = "Инженер-программист",
						AcademicDegree = AcademicDegree.Candidate
					}
				};

			context.Employees.AddRange(employeesToAdd);
			context.SaveChanges();

			List<User> users = context.Users.ToList();
			context.Users.RemoveRange(users);
			context.SaveChanges();

			List<User> usersToAdd = new List<User>
				{
					new User
					{
						LastName = "Лебедев",
						FirstName = "Кирилл",
						Patronymic = "Михайлович",
						Login = "admin",
						Password = "admin",
						Role = UserRole.HRStaff
					},
					new User
					{
						LastName = "Мельникова",
						FirstName = "Мирослава",
						Patronymic = "Фёдоровна",
						Login = "user",
						Password = "user",
						Role = UserRole.UnionStaff
					}
				};

			context.Users.AddRange(usersToAdd);
			context.SaveChanges();
		}
	}
}
