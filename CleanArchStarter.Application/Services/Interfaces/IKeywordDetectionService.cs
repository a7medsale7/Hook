namespace Hook.Application.Services.Interfaces;

public interface IKeywordDetectionService
{
    bool MightNeedDatabaseSearch(string question);
}
