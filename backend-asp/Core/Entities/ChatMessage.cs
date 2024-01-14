namespace Core.Entities;

public class ChatMessage : BaseEntity
{
    public User User { get; init; }
    public int UserId { get; init; }
    public ChatRoom ChatRoom { get; init; }
    public int ChatRoomId { get; init; }
    public string? Text { get; init; }
    public int Timestamp { get; init; }
}