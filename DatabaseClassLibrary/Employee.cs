using System.ComponentModel.DataAnnotations;

namespace DatabaseClassLibrary;

public enum Gender
{
	[Display(Name = "Мужской")]
	Male,
	[Display(Name = "Женский")]
	Female
}

public enum MaritalStatus
{
	[Display(Name = "Не в браке")]
	Single,
	[Display(Name = "В браке")]
	Married,
	[Display(Name = "Разведен(а)")]
	Divorced,
	[Display(Name = "Вдова(ец)")]
	Widowed
}

public enum AcademicDegree
{
	[Display(Name = "Нет")]
	None,
	[Display(Name = "Кандидат наук")]
	Candidate,
	[Display(Name = "Доктор наук")]
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
