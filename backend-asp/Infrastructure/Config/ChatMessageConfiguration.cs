using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Text).HasMaxLength(300);
        builder.HasOne(p => p.User).WithMany()
            .HasForeignKey(p => p.UserId);
        builder.HasOne(p => p.ChatRoom).WithMany()
            .HasForeignKey(p => p.ChatRoomId);
    }
}