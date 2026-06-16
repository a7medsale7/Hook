namespace Hook.Application.Contracts.FishGuard;

public class GeminiClassificationResponse
{
    public string Category { get; set; } = string.Empty;
    public string Entity { get; set; } = string.Empty;
    public double Confidence { get; set; }
}
