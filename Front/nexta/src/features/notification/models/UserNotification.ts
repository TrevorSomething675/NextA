export interface UserNotification{
    id:string,
    header:string,
    message:string,
    isRead:boolean,
    createdData:string,
    userId:string
} 

/*

        public string Header { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public bool IsTemporary { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        */