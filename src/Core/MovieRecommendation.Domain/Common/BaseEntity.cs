using System.ComponentModel.DataAnnotations;

namespace MovieRecommendation.Domain.Common
{
    abstract public class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
