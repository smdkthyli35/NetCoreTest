namespace Demo1.Api.Services
{
    public class NameService : INameService
    {
        public bool isValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
    }
}
