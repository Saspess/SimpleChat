namespace SimpleChat.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public IEnumerable<UserChatEntity> UserChats { get; set; }
    }
}
