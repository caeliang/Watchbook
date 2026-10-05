using Microsoft.EntityFrameworkCore;
using WatchBook.Application.Features.Social.DTOs;
using WatchBook.Application.Features.Social.Interfaces;
using WatchBook.Domain.Features.Social.Entities;
using WatchBook.Infrastructure.Persistence;
using WatchBook.Infrastructure.Features.System.Interfaces;
namespace WatchBook.Infrastructure.Features.Social.Services;

/// <summary>
/// Provides operations for managing user comments.
/// </summary>
public class CommentService : ICommentService
{
    private readonly WatchBookDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public CommentService(
        WatchBookDbContext dbContext,
        ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public async Task<IReadOnlyList<CommentDto>> GetByContentAsync(
        int contentId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Comment>()
            .AsNoTracking()
            .Where(x =>
                x.ContentId == contentId &&
                x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new CommentDto
            {
                Id = x.Id,
                UserId = x.UserId,
                Text = x.Text,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CreateAsync(
        int contentId,
        string text,
        CancellationToken cancellationToken = default)
    {
        EnsureAuthenticated();

        var normalizedText = NormalizeText(text);

        var contentExists = await _dbContext.Contents
            .AnyAsync(
                x => x.Id == contentId && x.DeletedAt == null,
                cancellationToken);

        if (!contentExists)
        {
            throw new KeyNotFoundException(
                $"Content with ID {contentId} was not found.");
        }

        var now = DateTime.UtcNow;

        var comment = new Comment
        {
            UserId = _currentUserService.UserId!,
            ContentId = contentId,
            Text = normalizedText,
            CreatedAt = now,
            UpdatedAt = now
        };

        _dbContext.Set<Comment>().Add(comment);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }

    public async Task UpdateAsync(
        int commentId,
        string text,
        CancellationToken cancellationToken = default)
    {
        EnsureAuthenticated();

        var normalizedText = NormalizeText(text);

        var comment = await _dbContext.Set<Comment>()
            .FirstOrDefaultAsync(
                x =>
                    x.Id == commentId &&
                    x.UserId == _currentUserService.UserId &&
                    x.DeletedAt == null,
                cancellationToken);

        if (comment is null)
        {
            throw new KeyNotFoundException(
                $"Comment with ID {commentId} was not found.");
        }

        comment.Text = normalizedText;
        comment.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        int commentId,
        CancellationToken cancellationToken = default)
    {
        EnsureAuthenticated();

        var comment = await _dbContext.Set<Comment>()
            .FirstOrDefaultAsync(
                x =>
                    x.Id == commentId &&
                    x.UserId == _currentUserService.UserId &&
                    x.DeletedAt == null,
                cancellationToken);

        if (comment is null)
        {
            throw new KeyNotFoundException(
                $"Comment with ID {commentId} was not found.");
        }

        var now = DateTime.UtcNow;

        comment.DeletedAt = now;
        comment.UpdatedAt = now;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void EnsureAuthenticated()
    {
        if (!_currentUserService.IsAuthenticated ||
            string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException(
                "An authenticated user is required.");
        }
    }

    private static string NormalizeText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException(
                "Comment text cannot be empty.",
                nameof(text));
        }

        var normalizedText = text.Trim();

        if (normalizedText.Length > 1000)
        {
            throw new ArgumentException(
                "Comment text cannot exceed 1000 characters.",
                nameof(text));
        }

        return normalizedText;
    }
}