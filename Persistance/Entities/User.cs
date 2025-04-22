using System.ComponentModel.DataAnnotations.Schema;

namespace AIAssistantMacos.Persistance.Entities;

[Table("users")]
public class User
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("is-pro")]
    public bool IsPro { get; set; }
    [Column("registered-at")]
    public DateTime RegisterDate { get; set; }
    [Column("code-id")]
    public Guid? CodeId { get; set; }
    public Code? Code { get; set; }
}