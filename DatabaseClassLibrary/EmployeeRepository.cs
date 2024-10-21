namespace DatabaseClassLibrary;

public class EmployeeRepository
{
	private readonly LaboratoryContext _context;

	public EmployeeRepository()
	{
		_context = new LaboratoryContext();
	}

	public List<Employee> GetAllEmployees()
	{
		return _context.Employees.ToList();
	}

	public Employee? GetEmployeeById(int id)
	{
		return _context.Employees.FirstOrDefault(e => e.Id == id);
	}

	public void AddEmployee(Employee employee)
	{
		_context.Employees.Add(employee);
		_context.SaveChanges();
	}

	public void UpdateEmployee(Employee employee)
	{
		Employee? existingEmployee = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);
		if (existingEmployee != null)
		{
			existingEmployee.LastName = employee.LastName;
			existingEmployee.FirstName = employee.FirstName;
			existingEmployee.Patronymic = employee.Patronymic;
			existingEmployee.Gender = employee.Gender;
			existingEmployee.DateOfBirth = employee.DateOfBirth;
			existingEmployee.MaritalStatus = employee.MaritalStatus;
			existingEmployee.HasChildren = employee.HasChildren;
			existingEmployee.Position = employee.Position;
			existingEmployee.AcademicDegree = employee.AcademicDegree;

			_context.SaveChanges();
		}
	}

	public void DeleteEmployee(int id)
	{
		Employee? employee = _context.Employees.FirstOrDefault(e => e.Id == id);
		if (employee != null)
		{
			_context.Employees.Remove(employee);
			_context.SaveChanges();
		}
	}
}
