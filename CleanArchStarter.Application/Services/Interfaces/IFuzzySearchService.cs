using System.Threading;
using System.Threading.Tasks;
using Hook.Domain.Enums;

namespace Hook.Application.Services.Interfaces;

public interface IFuzzySearchService
{
    Task<(object? Entity, string SourceType, string SourceId)> SearchAsync(ChatCategory category, string entityName, CancellationToken cancellationToken = default);
}
