namespace SimpleChat.Data.Entities
{
    public class UserChatEntity : BaseEntity
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }

        public UserEntity User { get; set; }
        public ChatEntity Chat { get; set; }
    }
}
