using Nexta.Domain.Validation;
using Nexta.Domain.Base;

namespace Nexta.Domain.Models.User
{
    public class User : Entity, IAggregateRoot
	{
        public const int MIN_NAME_LENGTH = 2;
        public const int MAX_NAME_LENGTH = 32;

        private readonly List<Notification> _notifications = new List<Notification>();

        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }
        public string? Phone { get; private set; }

        public string PasswordHash { get; private set; }
        public string Role { get; private set; }

        #pragma warning disable CS8618
        private User() { }
        #pragma warning restore CS8618

        public User(
            string firstName, string middleName,
            string lastName, string email, string passwordHash
            )
        {
            if(string.IsNullOrWhiteSpace(firstName) || firstName.Length < MIN_NAME_LENGTH || firstName.Length > MAX_NAME_LENGTH)
                throw new ArgumentNullException("First name is required", nameof(firstName));
            if (string.IsNullOrWhiteSpace(middleName) || middleName.Length < MIN_NAME_LENGTH || middleName.Length > MAX_NAME_LENGTH)
                throw new ArgumentNullException("Middle name is required", nameof(middleName));
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < MIN_NAME_LENGTH || lastName.Length > MAX_NAME_LENGTH)
                throw new ArgumentNullException("Last name is required", nameof(lastName));
            if (string.IsNullOrWhiteSpace(email) || !EmailValidator.IsValid(email))
                throw new ArgumentNullException("Email is required", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentNullException("Password hash is required", nameof(passwordHash));

            Id = Guid.NewGuid();
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Role = "User";
            PasswordHash = passwordHash;
        }

        public void ChangePassword(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentNullException("Password Hash is required", nameof(passwordHash));

            PasswordHash = passwordHash;
        }

        public void AddNotification(string header, string message)
        {
            var notification = new Notification(header, message, Id);
            _notifications.Add(notification);
        }

        public void Update(string? firstName, string? middleName, string? lastName, string? email, string? passwordHash, string? phone)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && firstName.Length >= MIN_NAME_LENGTH && firstName.Length <= MAX_NAME_LENGTH)
                FirstName = firstName;
            if (!string.IsNullOrWhiteSpace(middleName) && middleName.Length >= MIN_NAME_LENGTH && middleName.Length <= MAX_NAME_LENGTH)
                MiddleName = middleName;
            if (!string.IsNullOrWhiteSpace(lastName) && lastName.Length >= MIN_NAME_LENGTH && lastName.Length <= MAX_NAME_LENGTH)
                LastName = lastName;
            if (!string.IsNullOrWhiteSpace(email) && EmailValidator.IsValid(email))
                Email = email;
            if (!string.IsNullOrWhiteSpace(passwordHash))
                PasswordHash = passwordHash;
            if (string.IsNullOrWhiteSpace(phone))
                Phone = phone;
        }

        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
    }
}