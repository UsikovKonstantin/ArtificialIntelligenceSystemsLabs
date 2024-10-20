using DatabaseClassLibrary;
using Microsoft.EntityFrameworkCore;

using (LaboratoryContext context = new LaboratoryContext())
{
	Employee employee = new Employee
	{
		LastName = "Иванов",
		FirstName = "Иван",
		Patronymic = "Иванович",
		Gender = Gender.Male,
		DateOfBirth = new DateTime(1989, 5, 23),
		MaritalStatus = MaritalStatus.Married,
		HasChildren = true,
		Position = "Инженер",
		AcademicDegree = AcademicDegree.Doctor
	};

	context.Employees.Add(employee);
	context.SaveChanges();

	List<Employee> employees = context.Employees.ToList();
	foreach (Employee emp in employees)
	{
		Console.WriteLine($"{emp.Id} {emp.LastName} {emp.FirstName} {emp.Patronymic}, {emp.Position}, {emp.DateOfBirth.ToShortDateString()}");
	}
}