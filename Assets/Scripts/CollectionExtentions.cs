
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public static class QueueExtentions
{
    public static bool IsEmpty(this ICollection collection)
    {
        return collection.Count == 0;
    }
}
