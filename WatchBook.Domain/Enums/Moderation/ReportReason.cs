namespace WatchBook.Domain.Enums.Moderation;

/// <summary>
/// Defines the reasons users can report content for moderation review.
/// </summary>
public enum ReportReason
{
    /// <summary>
    /// The content is unsolicited or irrelevant mass posting.
    /// </summary>
    Spam = 0,

    /// <summary>
    /// The content contains spoilers about the entertainment media.
    /// </summary>
    Spoiler = 1,

    /// <summary>
    /// The content contains harassment or abusive language toward other users.
    /// </summary>
    Harassment = 2,

    /// <summary>
    /// The content contains hateful speech or discrimination.
    /// </summary>
    HateSpeech = 3,

    /// <summary>
    /// The content violates policies for other reasons.
    /// </summary>
    Other = 4
}
