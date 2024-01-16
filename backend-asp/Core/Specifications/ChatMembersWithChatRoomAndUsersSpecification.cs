using Core.Entities;

namespace Core.Specifications;

public class ChatMembersWithChatRoomAndUsersSpecification : BaseSpecification<ChatMember>
{
   public ChatMembersWithChatRoomAndUsersSpecification(ChatMembersSpecParams chatMembersParams) 
      : base(x => 
      (!chatMembersParams.ChatRoomId.HasValue || x.ChatRoomId == chatMembersParams.ChatRoomId) && 
      (!chatMembersParams.UserId.HasValue || x.UserId == chatMembersParams.UserId)) 
   {
      Console.WriteLine(chatMembersParams);
      AddInclude(x => x.ChatRoom);
      AddInclude(x => x.User);
      AddOrderBy(x => x.Id);
      ApplyPaging(chatMembersParams.PageSize * (chatMembersParams.PageIndex - 1), chatMembersParams.PageSize);
   }

   public ChatMembersWithChatRoomAndUsersSpecification(int id) : base(x => x.Id == id)
   {
      AddInclude(x => x.ChatRoom);
      AddInclude(x => x.User);
      AddOrderBy(x => x.Id);
   }
}