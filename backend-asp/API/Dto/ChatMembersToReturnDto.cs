namespace API.Dto;

public class ChatMembersToReturnDto
{
    public int Id { get; set; }
    public string User { get; init; }
    public string ChatRoom { get; init; }
}