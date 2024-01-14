namespace ConquerTheDungeon.Utils;

public class Loader<T>
{
    private readonly string _folder;
    
    public Loader(string folder)
    {
        _folder = folder;
    }

    public T Get(string assetName)
    {
        return Game1.Instance.Content.Load<T>(_folder + '/' + assetName);
    }
}