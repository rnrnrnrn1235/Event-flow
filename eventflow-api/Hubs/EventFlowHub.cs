using Microsoft.AspNetCore.SignalR;

public class EventFlowHub : Hub
{
    // Client calls this to join a group for an event
    // so only watchers of that event get ticket count updates (webSockets req. intickets)
    public async Task JoinEventGroup(string eventId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"event-{eventId}");
    }

    public async Task LeaveEventGroup(string eventId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"event-{eventId}");
    }
}