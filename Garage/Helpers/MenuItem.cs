namespace Garage.Helpers;

public record MenuItem
{
    public string Key { get; set; }
    public string Text { get; set; }

    public MenuItem(string key, string text)
    {
        Key = key;
        Text = text;
    }

    public override string ToString()
    {
        return $"{Key}. {Text}";
    }
}