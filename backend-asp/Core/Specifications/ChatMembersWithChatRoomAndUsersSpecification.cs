using Core.Entities;

namespace Core.Specifications;

public class ChatMembersWithChatRoomAndUsersSpecification : BaseSpecification<ChatMember>
{
   public ChatMembersWithChatRoomAndUsersSpecification(int? chatRoomId, int? userId) 
      : base(x => 
      (!chatRoomId.HasValue || x.ChatRoomId == chatRoomId) && 
      (!userId.HasValue || x.UserId == userId)) 
   {
      AddInclude(x => x.ChatRoom);
      AddInclude(x => x.User);
   }

   public ChatMembersWithChatRoomAndUsersSpecification(int chatRoomId) : base(x => x.ChatRoomId == chatRoomId)
   {
      AddInclude(x => x.ChatRoom);
      AddInclude(x => x.User); 
   }
}