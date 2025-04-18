using System.ComponentModel.DataAnnotations.Schema;

namespace AIAssistantMacos.Persistance.Entities;

[Table("users")]
public class User
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("is-pro")]
    public bool IsPro { get; set; }
}