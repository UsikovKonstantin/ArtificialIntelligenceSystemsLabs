using DatabaseClassLibrary;
using Microsoft.EntityFrameworkCore;

using (LaboratoryContext context = new LaboratoryContext())
{
	context.Database.Migrate();

	Seeder.SeedDatabase();

	List<Employee> employees = context.Employees.ToList();
	foreach (Employee emp in employees)
	{
		Console.WriteLine($"{emp.Id}, {emp.LastName} {emp.FirstName} {emp.Patronymic}, {emp.Position}, {emp.DateOfBirth.ToShortDateString()}");
	}
}