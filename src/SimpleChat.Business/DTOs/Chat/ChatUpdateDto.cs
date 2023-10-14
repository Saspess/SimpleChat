namespace SimpleChat.Business.DTOs.Chat
{
    public class ChatUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatorId { get; set; }
    }
}
