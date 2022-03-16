
using System.Collections;

public static class QueueExtentions
{
    public static bool IsEmpty(this ICollection collection)
    {
        return collection.Count == 0;
    }
}
