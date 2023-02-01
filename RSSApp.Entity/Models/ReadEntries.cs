using RSSApp.Entity.Models.Implementations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSSApp.Entity.Models
{
    public class ReadEntries : ModelBase
    {
        public Entries ReadEntry { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        

        public ReadEntries() { }
        public ReadEntries(Entries entries)
        {
            ReadEntry = entries ?? throw new ArgumentNullException(nameof(entries));
        }
    }
}
