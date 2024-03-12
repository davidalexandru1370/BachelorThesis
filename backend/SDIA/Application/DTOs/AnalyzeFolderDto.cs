namespace Application.DTOs;

public class AnalyzeFolderDto
{
    public Guid FolderId { get; set; }
    public bool IsCorrect { get; set; }
    public List<String> Errors { get; set; }
}