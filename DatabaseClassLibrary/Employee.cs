namespace DatabaseClassLibrary;

public enum Gender
{
	Male,
	Female
}

public enum MaritalStatus
{
	Single,
	Married,
	Divorced,
	Widowed
}

public enum AcademicDegree
{
	None,
	Сandidate,
	Doctor
}

public class Employee
{
	public int Id { get; set; }
	public string LastName { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string? Patronymic { get; set; }
	public Gender Gender { get; set; }
	public DateTime DateOfBirth { get; set; }
	public MaritalStatus MaritalStatus { get; set; } 
	public bool HasChildren { get; set; }
	public string Position { get; set; } = string.Empty;
	public AcademicDegree AcademicDegree { get; set; }

	public override string ToString()
	{
		return $"Id: {Id}, LastName: {LastName}, FirstName: {FirstName}, " +
			$"Patronymic: {Patronymic}, Gender: {Gender}, DateOfBirth: {DateOfBirth}, " +
			$"MaritalStatus: {MaritalStatus}, HasChildren: {HasChildren}, " +
			$"Position: {Position}, AcademicDegree: {AcademicDegree}";
	}
}
