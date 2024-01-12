using CSM1.Core.Entities.Static;

namespace CSM1.Core.Entities.Reaction;

public class Reaction
{
    public string AppUserId { get; set; }

    // //
    public ReactionType ReactionType { get; set; }

    // //
    public AppUser? AppUser { get; set; }
}
