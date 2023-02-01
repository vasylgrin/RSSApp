using RSSApp.Entity.Models.Implementations;

namespace RSSApp.Entity.Models
{
    public class Feed : ModelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public List<Entries> Entries { get; set; } = new List<Entries>();

        public Feed() { }
        public Feed(string title, string description, string link, params Entries[] entries)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException($"'{nameof(description)}' cannot be null or whitespace.", nameof(description));
            }
            if (string.IsNullOrWhiteSpace(link))
            {
                throw new ArgumentNullException($"'{nameof(link)}' cannot be null or whitespace.", nameof(link));
            }

            Title = title;
            Description = description;
            Link = link;
            Entries = entries.ToList();
        }

        public override string ToString()
        {
            return $"Title: {Title}{Environment.NewLine}Description: {Description}{Environment.NewLine}Link: {Link}";
        }
    }
}
