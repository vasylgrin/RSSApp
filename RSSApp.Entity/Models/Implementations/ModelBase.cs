using RSSApp.Entity.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSSApp.Entity.Models.Implementations
{
    public class ModelBase : IModleBase
    {
        public int Id { get; set; }
    }
}
