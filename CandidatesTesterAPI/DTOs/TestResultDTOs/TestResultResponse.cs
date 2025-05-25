namespace CandidatesTesterAPI.DTOs.TestResultDTOs;

public class TestResultResponse
{
    public int TestResultId { get; set; }
    public int UserId { get; set; }
    public int TestId { get; set; }
    public int Score { get; set; }
    public int MaxScore { get; set; }
    public DateTime CompletedAt { get; set; }
}
