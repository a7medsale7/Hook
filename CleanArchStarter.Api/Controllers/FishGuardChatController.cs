using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Contracts.FishGuard;
using Hook.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Hook.Api.Controllers;

[Route("api/fishguard")]
[ApiController]
[Authorize]
public class FishGuardChatController : ControllerBase
{
    private readonly IFishGuardChatService _chatService;
    private readonly IConversationService _conversationService;

    public FishGuardChatController(IFishGuardChatService chatService, IConversationService conversationService)
    {
        _chatService = chatService;
        _conversationService = conversationService;
    }

    [HttpPost("chat")]
    [EnableRateLimiting("FishGuardRateLimit")]
    public async Task StartNewChat([FromBody] ChatRequestDto request, CancellationToken cancellationToken)
    {
        await ProcessChatRequest(null, request, cancellationToken);
    }

    [HttpPost("chat/{conversationId}")]
    [EnableRateLimiting("FishGuardRateLimit")]
    public async Task ContinueChat([FromRoute] Guid conversationId, [FromBody] ChatRequestDto request, CancellationToken cancellationToken)
    {
        await ProcessChatRequest(conversationId, request, cancellationToken);
    }

    private async Task ProcessChatRequest(Guid? conversationId, ChatRequestDto request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            Response.StatusCode = 401;
            return;
        }

        Response.ContentType = "text/event-stream";
        
        try
        {
            await foreach (var chunk in _chatService.ProcessAndStreamResponseAsync(userId, conversationId, request, cancellationToken))
            {
                await Response.WriteAsync($"data: {chunk}\n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
            
            await Response.WriteAsync("data: [DONE]\n\n", cancellationToken);
            await Response.Body.FlushAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // Client disconnected or request cancelled
        }
    }

    [HttpGet("conversations")]
    public async Task<IActionResult> GetUserConversations(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var result = await _conversationService.GetUserConversationsAsync(userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("conversations/starred")]
    public async Task<IActionResult> GetStarredConversations(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var result = await _conversationService.GetStarredConversationsAsync(userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("conversations/{id}")]
    public async Task<IActionResult> GetConversationMessages(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var result = await _conversationService.GetConversationMessagesAsync(id, userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPatch("conversations/{id}/star")]
    public async Task<IActionResult> ToggleStar(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var result = await _conversationService.ToggleStarAsync(id, userId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("conversations/{id}")]
    public async Task<IActionResult> DeleteConversation(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var result = await _conversationService.DeleteConversationAsync(id, userId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
