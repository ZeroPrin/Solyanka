public class StringCollection
{
    private string[] _array = new string[3] {"String 1", "String 2", "String 3"};
    public string this[int index]
    {
        get => _array[index];
        set => _array[index] = value;
    }
}
