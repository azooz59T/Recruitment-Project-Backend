using backend.Core.Enums;

namespace backend.Core.Dtos.Company
{
    public class CompanyReadDto
    {
        public string Name { get; set; }

        public CompanySize Size { get; set; }

        public long Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
