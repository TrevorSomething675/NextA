﻿namespace Nexta.Domain.Options
{
    public class EmailOptions
	{
		public const string Section = nameof(EmailOptions);

		public string SmtpServer { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string FromName { get; set; }
		public string FromAddress { get; set; }
		public bool EnableSsl { get; set; }
		public int Timeout { get; set; } = 15;
	}
}