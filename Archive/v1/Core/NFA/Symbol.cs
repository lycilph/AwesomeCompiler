namespace Core.NFA;

public class Symbol 
{
    public bool isEpsilon = false;
    public bool isAny = false;
    public HashSet<char> chars = [];
    public string label = string.Empty;

    public override string ToString()
    {
        return label == "\"" ? "\\\"" : label;
    }
}