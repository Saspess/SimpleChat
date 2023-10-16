namespace SimpleChat.Business.DTOs.UserChat
{
    public class UserChatViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatorId { get; set; }
        public int ChatId { get; set; }
    }
}
