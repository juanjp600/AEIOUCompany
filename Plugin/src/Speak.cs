namespace AEIOU_Company;

public readonly struct Speak
{
    public readonly string ChatMessage;
    public readonly int PlayerId;
    public readonly ulong SteamId;
    public Speak(string chatMessage, int playerId)
    {
        ChatMessage = chatMessage;
        PlayerId = playerId;
    }
}