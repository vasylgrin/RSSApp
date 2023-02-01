using RSSApp.Entity.Models.Implementations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSSApp.Entity.Models
{
    public class Entries : ModelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime TimeOfPublication { get; set; }
        public int FeedId { get; set; }
        [ForeignKey("FeedId")]
        public Feed Feed { get; set; }
        public Entries() { }


        public Entries(string title, string description, string link, DateTime timeOfPublication/*, /*params string[] categories*/)
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

            if (DateTime.MinValue == timeOfPublication)
            {
                throw new ArgumentNullException($"'{nameof(timeOfPublication)}' cannot be not to set.", nameof(link));
            }

            Title = title;
            Description = description;
            Link = link;
            TimeOfPublication = timeOfPublication;
        }

        public override string ToString()
        {
            return $"Title: {Title}{Environment.NewLine}Content: {Description}{Environment.NewLine}Link: {Link}{Environment.NewLine}Time of Publication: {TimeOfPublication}";
        }
    }
}
