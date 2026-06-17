using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Domain.Enums;

namespace Hook.Application.Contracts.Ai;

public class AiMapperResult
{
    public ChatCategory Category { get; set; }
    public string SearchKeyword { get; set; } = string.Empty;
    /// <summary>
    /// All matching database entities found for the user's question.
    /// Can be multiple (e.g. multiple seasons that ban the same fish).
    /// </summary>
    public List<object> DbEntities { get; set; } = new();
    public string SourceType { get; set; } = string.Empty;
}

public interface IAiDatabaseMapper
{
    Task<AiMapperResult> MapQuestionToDatabaseAsync(string question, CancellationToken cancellationToken = default);
}
