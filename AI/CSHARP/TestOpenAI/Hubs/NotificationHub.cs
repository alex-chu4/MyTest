using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.SignalR;
using OpenAI.Chat;

namespace AIAssistantChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatClient _api;

        public ChatHub()
        {
            _api = new(model: "gpt-4o", apiKey: "sk-1234");
        }

        public async Task SendMessage(string user, string message)
        {
            // Broadcast user's message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);

            // Call the AI API for response
            ChatCompletion result = await _api.CompleteChatAsync(message);

            // Send AI response
            await Clients.All.SendAsync("ReceiveMessage", "AI Bot", result.Content[0].Text.Replace("\n","<br/>").Replace(" ", "&nbsp;"));
        }
    }
}