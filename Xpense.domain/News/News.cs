using System;

namespace Xpense.domain.News
{
    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
        public int Priority { get; set; }
    }
}
