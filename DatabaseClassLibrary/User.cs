namespace DatabaseClassLibrary;

public enum UserRole
{
	HRStaff,   
	UnionStaff   
}

public class User
{
	public int Id { get; set; }
	public string LastName { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string Patronymic { get; set; } = string.Empty;
	public string Login { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public UserRole Role { get; set; }

	public override string ToString()
	{
		return $"Id: {Id}, LastName: {LastName}, FirstName: {FirstName}, " +
			$"Patronymic: {Patronymic}, Login: {Login}, Password: {Password}, " +
			$"Role: {Role}";
	}
}
