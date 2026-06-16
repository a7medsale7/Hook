using System;

namespace Hook.Application.Contracts.FishGuard;

public class ChatResponseDto
{
    public Guid ConversationId { get; set; }
    public string Answer { get; set; } = string.Empty;
    public string Source { get; set; } = "Gemini";
    public string? SourceType { get; set; }
    public string? SourceId { get; set; }
}
