using System.ComponentModel.DataAnnotations;

namespace Medicoz.Models
{
    public class BlogComment
    {
        public int Id { get; set; }
        public int? BlogPostId { get; set; }
        [StringLength(maximumLength:25)]
        public string? UserName { get; set; }
        [StringLength(maximumLength: 55)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [StringLength(maximumLength:100,MinimumLength =2)]
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime CommentTime { get; set; }
        public BlogPost? BlogPost { get; set; }

    }
}
