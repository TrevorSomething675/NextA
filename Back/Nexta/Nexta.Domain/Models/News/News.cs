using Nexta.Domain.Base;

namespace Nexta.Domain.Models.News
{
    public class News : Entity
    {
        public News(string header, string description, string base64String)
        {
            Id = Guid.NewGuid();
            Header = header;
            Description = description;
            Base64String = base64String;
        }

        public string Header { get; }
        public string Description { get; }
        public string Base64String { get; }
    }
}