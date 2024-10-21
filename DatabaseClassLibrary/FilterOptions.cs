namespace DatabaseClassLibrary;

public class FilterOptions
{
	public string LastName { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string Patronymic { get; set; } = string.Empty;
	public Gender? Gender { get; set; } = null;
	public MaritalStatus? MaritalStatus { get; set; } = null;
	public bool? HasChildren { get; set; } = null;
	public string Position { get; set; } = string.Empty;
	public AcademicDegree? AcademicDegree { get; set; } = null;

	public override string ToString()
	{
		return $"LastName: {LastName}, FirstName: {FirstName}, Patronymic: {Patronymic}, Gender: {Gender}, " +
			$"MaritalStatus: {MaritalStatus}, HasChildren: {HasChildren}, Position: {Position}, AcademicDegree: {AcademicDegree}";
	}
}
