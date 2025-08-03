using UnityEngine;

public static class InputDirectionManager
{
    public static string GetDirectionName()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Mathf.Approximately(h, -1) && Mathf.Approximately(v, 1)) return "WA";
        if (Mathf.Approximately(h, 1) && Mathf.Approximately(v, 1)) return "WD";
        if (Mathf.Approximately(h, -1) && Mathf.Approximately(v, -1)) return "SA";
        if (Mathf.Approximately(h, 1) && Mathf.Approximately(v, -1)) return "SD";
        if (Mathf.Approximately(h, -1)) return "A";
        if (Mathf.Approximately(h, 1)) return "D";
        if (Mathf.Approximately(v, 1)) return "W";
        if (Mathf.Approximately(v, -1)) return "S";

        return "";
    }

    private static Vector2Int GetDirectionVector()
    {
        int h = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        int v = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        return new Vector2Int(h, v);
    }

    public static bool IsDiagonal()
    {
        Vector2Int dir = GetDirectionVector();
        return dir.x != 0 && dir.y != 0;
    }
}