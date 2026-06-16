using Hook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class RestrictedLocationConfiguration : IEntityTypeConfiguration<RestrictedLocation>
{
    public void Configure(EntityTypeBuilder<RestrictedLocation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Reason).HasMaxLength(500);
    }
}

public class RestrictedToolConfiguration : IEntityTypeConfiguration<RestrictedTool>
{
    public void Configure(EntityTypeBuilder<RestrictedTool> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ToolName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Penalty).HasMaxLength(500);
    }
}

public class FishingSeasonConfiguration : IEntityTypeConfiguration<FishingSeason>
{
    public void Configure(EntityTypeBuilder<FishingSeason> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Species).HasMaxLength(255).IsRequired();
    }
}

public class FishingFaqConfiguration : IEntityTypeConfiguration<FishingFaq>
{
    public void Configure(EntityTypeBuilder<FishingFaq> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Question).HasMaxLength(500).IsRequired();
        builder.Property(x => x.Category).HasMaxLength(100);
    }
}

public class ChatConversationConfiguration : IEntityTypeConfiguration<ChatConversation>
{
    public void Configure(EntityTypeBuilder<ChatConversation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(255);
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Content).IsRequired();
        builder.HasOne(x => x.Conversation).WithMany(x => x.Messages).HasForeignKey(x => x.ConversationId).OnDelete(DeleteBehavior.Cascade);
    }
}
