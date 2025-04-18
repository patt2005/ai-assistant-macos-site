using System.ComponentModel.DataAnnotations.Schema;

namespace AIAssistantMacos.Persistance.Entities;

[Table("blogs")]
public class Blog
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("publish-date")]
    public DateTime PublishDate { get; set; }
    [Column("text")]
    public string Text { get; set; }
    [Column("image-url")]
    public string ImageUrl { get; set; }
    [Column("subtitle")]
    public string SubTitle { get; set; }
}