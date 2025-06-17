using System;

public static class ShapeSpawnerEvents
{
    public static event Action OnMatch;

    public static void NotifyMatch()
    {
        OnMatch?.Invoke();
    }
}
