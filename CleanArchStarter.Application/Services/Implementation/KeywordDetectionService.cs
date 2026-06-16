using System;
using System.Linq;
using Hook.Application.Services.Interfaces;

namespace Hook.Application.Services.Implementation;

public class KeywordDetectionService : IKeywordDetectionService
{
    private readonly string[] _keywords = new[]
    {
        "موسم", "مواسم", "ممنوع", "محظور", "شبكة", "سنار", "صيد", "بحيرة", "بحر", "نهر",
        "طعم", "قانون", "غرامة", "عقوبة", "اداة", "طريقة", "season", "ban", "prohibited",
        "restricted", "net", "hook", "lake", "sea", "river", "bait", "law", "penalty", "tool",
        "اصطاد", "يصطاد", "نصطاد", "تصطاد", "مكان", "فين", "محمية", "شاطئ", "شواطئ", "منتزه",
        "بواغيز", "ترعة", "نيل", "مسموح", "ينفع"
    };

    public bool MightNeedDatabaseSearch(string question)
    {
        if (string.IsNullOrWhiteSpace(question)) return false;
        
        // Convert to lowercase for simpler matching
        var normalized = question.ToLowerInvariant();
        
        return _keywords.Any(k => normalized.Contains(k));
    }
}
