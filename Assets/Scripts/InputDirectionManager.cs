using UnityEngine;
using Terresquall; // Namespace for the joystick API

public static class InputDirectionManager
{

    private static bool _inputEnabled = true;

    public static void EnableInput() => _inputEnabled = true;
    public static void DisableInput() => _inputEnabled = false;
    
    public static string GetDirectionName()
    {
        if (!_inputEnabled) return ""; // input locked
        float h, v;
        
        if (VirtualJoystick.CountActiveInstances() > 0)
        {
            h = VirtualJoystick.GetAxisRaw("Horizontal");
            v = VirtualJoystick.GetAxisRaw("Vertical");
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        if (Mathf.Approximately(h, -1) && Mathf.Approximately(v, 1)) return "WA";
        if (Mathf.Approximately(h, 0) && Mathf.Approximately(v, 1)) return "W";
        if (Mathf.Approximately(h, 1) && Mathf.Approximately(v, 1)) return "WD";
        
        if (Mathf.Approximately(h, -1) && Mathf.Approximately(v, 0)) return "A";
        // if (Mathf.Approximately(h, 0) && Mathf.Approximately(v, 0)) return "O";/*O stands for origin = center*/
        if (Mathf.Approximately(h, 1) && Mathf.Approximately(v, 0)) return "D";

        if (Mathf.Approximately(h, -1) && Mathf.Approximately(v, -1)) return "SA";
        if (Mathf.Approximately(h, 0) && Mathf.Approximately(v, -1)) return "S";
        if (Mathf.Approximately(h, 1) && Mathf.Approximately(v, -1)) return "SD";

        return "";
    }

    public static Vector2Int GetDirectionVector()
    {

        
        float h, v;
        if (VirtualJoystick.CountActiveInstances() > 0)
        {
            h = VirtualJoystick.GetAxisRaw("Horizontal");
            v = VirtualJoystick.GetAxisRaw("Vertical");
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
        return new Vector2Int(Mathf.RoundToInt(h), Mathf.RoundToInt(v));
    }

    public static bool IsDiagonal()
    {
        var dir = GetDirectionVector();
        return dir.x != 0 && dir.y != 0;
    }
}