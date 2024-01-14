namespace Core.Entities;

public class ChatMember : BaseEntity
{
    public User User { get; init; }
    public int UserId { get; init; }
    public ChatRoom ChatRoom { get; init; }
    public int ChatRoomId { get; init; }
}