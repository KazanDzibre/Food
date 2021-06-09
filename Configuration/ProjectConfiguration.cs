namespace Food.Configuration
{
	public class ProjectConfiguration : IProjectConfiguration
	{
		public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();
		public Jwt Jwt { get; set; } = new Jwt();
	}

	public class DatabaseConfiguration
	{
		public string ConnectionString { get; set; }
	}

	public class Jwt
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string Subject { get; set; }
	}
}
