namespace Core.Specifications;

public class ChatMembersSpecParams
{
   private const int MaxPageSize = 50;
   public int PageIndex { get; set; } = 1;

   private int _pageSize = 20;

   public int PageSize
   {
      get => _pageSize;
      set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
   }

   public int? UserId { get; set; }
   public int? ChatRoomId { get; set; }
}