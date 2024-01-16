using Core.Entities;

namespace Core.Specifications;

public class ChatMembersWithChatRoomAndUsersSpecification : BaseSpecification<ChatMember>
{
   public ChatMembersWithChatRoomAndUsersSpecification() 
   {
      AddInclude(x => x.ChatRoom);
      AddInclude(x => x.User);
   }
}