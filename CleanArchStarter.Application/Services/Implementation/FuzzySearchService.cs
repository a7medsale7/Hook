using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FuzzySharp;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hook.Application.Services.Implementation;

public class FuzzySearchService : IFuzzySearchService
{
    private readonly ApplicationDbContext _context;
    private const int Threshold = 70;

    private string NormalizeArabic(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;
        return text.Replace("ة", "ه")
                   .Replace("أ", "ا")
                   .Replace("إ", "ا")
                   .Replace("آ", "ا")
                   .Replace("ى", "ي");
    }

    public FuzzySearchService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(object? Entity, string SourceType, string SourceId)> SearchAsync(ChatCategory category, string entityName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(entityName))
            return (null, string.Empty, string.Empty);

        var normalizedEntityName = NormalizeArabic(entityName);

        switch (category)
        {
            case ChatCategory.RestrictedLocation:
                var locations = await _context.RestrictedLocations.AsNoTracking().Select(x => new { x.Id, x.Name }).ToListAsync(cancellationToken);
                if (!locations.Any()) return (null, string.Empty, string.Empty);
                var normalizedLocations = locations.Select(x => new { OriginalId = x.Id, NormalizedName = NormalizeArabic(x.Name) }).ToList();
                var locResult = Process.ExtractOne(normalizedEntityName, normalizedLocations.Select(x => x.NormalizedName), s => s);
                if (locResult.Score >= Threshold)
                {
                    var matchedLocId = normalizedLocations[locResult.Index].OriginalId;
                    var fullLoc = await _context.RestrictedLocations.FindAsync(new object[] { matchedLocId }, cancellationToken);
                    return (fullLoc, "RestrictedLocation", matchedLocId.ToString());
                }
                
                var fallbackLoc = normalizedLocations.FirstOrDefault(x => x.NormalizedName.Contains(normalizedEntityName) || normalizedEntityName.Contains(x.NormalizedName));
                if (fallbackLoc != null)
                {
                    var fullLoc = await _context.RestrictedLocations.FindAsync(new object[] { fallbackLoc.OriginalId }, cancellationToken);
                    return (fullLoc, "RestrictedLocation", fallbackLoc.OriginalId.ToString());
                }
                break;

            case ChatCategory.RestrictedTool:
                var tools = await _context.RestrictedTools.AsNoTracking().Select(x => new { x.Id, x.Name }).ToListAsync(cancellationToken);
                if (!tools.Any()) return (null, string.Empty, string.Empty);
                var normalizedTools = tools.Select(x => new { OriginalId = x.Id, NormalizedName = NormalizeArabic(x.Name) }).ToList();
                var toolResult = Process.ExtractOne(normalizedEntityName, normalizedTools.Select(x => x.NormalizedName), s => s);
                if (toolResult.Score >= Threshold)
                {
                    var matchedToolId = normalizedTools[toolResult.Index].OriginalId;
                    var fullTool = await _context.RestrictedTools.FindAsync(new object[] { matchedToolId }, cancellationToken);
                    return (fullTool, "RestrictedTool", matchedToolId.ToString());
                }
                
                var fallbackTool = normalizedTools.FirstOrDefault(x => x.NormalizedName.Contains(normalizedEntityName) || normalizedEntityName.Contains(x.NormalizedName));
                if (fallbackTool != null)
                {
                    var fullTool = await _context.RestrictedTools.FindAsync(new object[] { fallbackTool.OriginalId }, cancellationToken);
                    return (fullTool, "RestrictedTool", fallbackTool.OriginalId.ToString());
                }
                break;

            case ChatCategory.FishingSeason:
                var seasons = await _context.FishingSeasons.AsNoTracking().Select(x => new { x.Id, x.SeasonName }).ToListAsync(cancellationToken);
                if (!seasons.Any()) return (null, string.Empty, string.Empty);
                var normalizedSeasons = seasons.Select(x => new { OriginalId = x.Id, NormalizedName = NormalizeArabic(x.SeasonName) }).ToList();
                var seasonResult = Process.ExtractOne(normalizedEntityName, normalizedSeasons.Select(x => x.NormalizedName), s => s);
                if (seasonResult.Score >= Threshold)
                {
                    var matchedSeasonId = normalizedSeasons[seasonResult.Index].OriginalId;
                    var fullSeason = await _context.FishingSeasons.FindAsync(new object[] { matchedSeasonId }, cancellationToken);
                    return (fullSeason, "FishingSeason", matchedSeasonId.ToString());
                }

                var fallbackSeason = normalizedSeasons.FirstOrDefault(x => x.NormalizedName.Contains(normalizedEntityName) || normalizedEntityName.Contains(x.NormalizedName));
                if (fallbackSeason != null)
                {
                    var fullSeason = await _context.FishingSeasons.FindAsync(new object[] { fallbackSeason.OriginalId }, cancellationToken);
                    return (fullSeason, "FishingSeason", fallbackSeason.OriginalId.ToString());
                }
                break;

            case ChatCategory.FishingFaq:
                var faqs = await _context.FishingFaqs.AsNoTracking().Select(x => new { x.Id, x.Question }).ToListAsync(cancellationToken);
                if (!faqs.Any()) return (null, string.Empty, string.Empty);
                var normalizedFaqs = faqs.Select(x => new { OriginalId = x.Id, NormalizedName = NormalizeArabic(x.Question) }).ToList();
                var faqResult = Process.ExtractOne(normalizedEntityName, normalizedFaqs.Select(x => x.NormalizedName), s => s);
                if (faqResult.Score >= Threshold)
                {
                    var matchedFaqId = normalizedFaqs[faqResult.Index].OriginalId;
                    var fullFaq = await _context.FishingFaqs.FindAsync(new object[] { matchedFaqId }, cancellationToken);
                    return (fullFaq, "FishingFaq", matchedFaqId.ToString());
                }

                var fallbackFaq = normalizedFaqs.FirstOrDefault(x => x.NormalizedName.Contains(normalizedEntityName) || normalizedEntityName.Contains(x.NormalizedName));
                if (fallbackFaq != null)
                {
                    var fullFaq = await _context.FishingFaqs.FindAsync(new object[] { fallbackFaq.OriginalId }, cancellationToken);
                    return (fullFaq, "FishingFaq", fallbackFaq.OriginalId.ToString());
                }
                break;
        }

        return (null, string.Empty, string.Empty);
    }
}
