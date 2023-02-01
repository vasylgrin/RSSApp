using RSSApp.Entity.Models.Implementations;

namespace RSSApp.Entity.Models
{
    public class User : ModelBase
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public List<ReadEntries> ReadingEntries { get; set; } = new List<ReadEntries>();

        public User() { }
        public User(string name, string password)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));
            }

            Name = name;
            Password = password;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User user)
        {
            return user != null
                && user.Name == Name
                && user.Password == Password
                && user.ReadingEntries == ReadingEntries;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Password, ReadingEntries);
        }
    }
}
