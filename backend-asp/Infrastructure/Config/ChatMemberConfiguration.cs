using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.HasOne(b => b.User).WithMany()
            .HasForeignKey(p => p.UserId);
        builder.HasOne(p => p.ChatRoom).WithMany()
            .HasForeignKey(p => p.ChatRoomId);
    }
}