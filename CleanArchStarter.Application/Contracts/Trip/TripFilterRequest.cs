using Hook.Application.Contracts;
using System;

namespace Hook.Application.Contracts.Trip;

public class TripFilterRequest : ReqeustFilters
{
    public string? Location { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public DateTime? Date { get; init; }
    public int? Participants { get; init; }
}
