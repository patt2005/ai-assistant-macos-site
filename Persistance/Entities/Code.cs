using System.ComponentModel.DataAnnotations.Schema;

namespace AIAssistantMacos.Persistance.Entities;

public enum CodeStatus
{
    Active,
    Used
}

[Table("activation-codes")]
public class Code
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("content")]
    public string Content { get; set; }
    [Column("activated-at")]
    public DateTime? ActivatedAt { get; set; }
    [Column("status")]
    public CodeStatus Status { get; set; }
    [Column("created-at")]
    public DateTime CreatedAt { get; set; }
}