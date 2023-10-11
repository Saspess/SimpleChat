namespace SimpleChat.Data.Entities
{
    public class ChatEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public IEnumerable<UserChatEntity> ChatUsers { get; set; }
    }
}
