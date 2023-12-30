using System.ComponentModel.DataAnnotations.Schema;

namespace CustomApi.Core.Entities;

public class GameSession : BaseEntity
{
    public int PlayerCount { get; set; }
    public string RoomName { get; set; }

    [NotMapped]
    internal bool? IsActive { get; set; } = false;
}
