using System.Collections.Generic;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConquerTheDungeon.Logic;

public class RoomMap
{
    private class Content
    {
        [JsonExtensionData]
        public IDictionary<string, JToken> Map;
    }

    private readonly Content _content;
    private string _lastContent;

    public RoomMap()
    {
        _content = Game1.Instance.Content.Load<Content>("fights/map.json", new JsonContentLoader());
    }

    public string GetNext(string current)
    {
        return (string)_content.Map[current];
    }

    public string GetNext()
    {
        _lastContent ??= "start";
        return _lastContent = GetNext(_lastContent);
    }
}