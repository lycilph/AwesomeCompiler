namespace Core.LexicalAnalysis;

public class PushbackReader : TextReader
{
    private readonly TextReader reader;
    private readonly Stack<char> buffer;

    public PushbackReader(TextReader reader)
    {
        this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        buffer = [];
    }

    public override int Peek()
    {
        if (buffer.Count > 0)
            return buffer.Peek();

        return reader.Peek();
    }

    public override int Read()
    {
        if (buffer.Count > 0)
            return buffer.Pop();

        return reader.Read();
    }

    public override int Read(char[] output, int index, int count)
    {
        int readCount = 0;

        // Read from pushback buffer first
        while (buffer.Count > 0 && count > 0)
        {
            output[index++] = buffer.Pop();
            readCount++;
            count--;
        }

        // Read the rest from the underlying reader
        if (count > 0)
        {
            readCount += reader.Read(output, index, count);
        }

        return readCount;
    }

    public void Unread(char c)
    {
        buffer.Push(c);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            reader.Dispose();
        }
        base.Dispose(disposing);
    }
}