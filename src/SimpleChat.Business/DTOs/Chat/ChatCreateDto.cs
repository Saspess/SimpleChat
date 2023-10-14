namespace SimpleChat.Business.DTOs.Chat
{
    public class ChatCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatorId { get; set; }
    }
}
