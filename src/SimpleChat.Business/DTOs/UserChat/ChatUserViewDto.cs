namespace SimpleChat.Business.DTOs.UserChat
{
    public class ChatUserViewDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int ChatId { get; set; }
    }
}
