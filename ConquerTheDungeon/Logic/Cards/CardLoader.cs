using System;
using System.Linq;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;

namespace ConquerTheDungeon.Logic.Cards;

public static class CardLoader
{
    public static Card Get(string assetName)
    {
        var content = Game1.Instance.Content.Load<CardContent>($"cards/{assetName}.json", new JsonContentLoader());
        var typeName = content.Type;
        var type = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .FirstOrDefault(x => x.Name == typeName);
        return Activator.CreateInstance(type, content) as Card;
    }
}