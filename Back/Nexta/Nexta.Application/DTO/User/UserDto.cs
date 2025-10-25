namespace Nexta.Application.DTO.User
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string? MiddleName { get; init; }
        public string LastName { get; init; }

        public string Email { get; init; }
        public string? Phone { get; init; }

        public string Role { get; init; }
    }
}