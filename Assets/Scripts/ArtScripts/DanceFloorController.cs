using UnityEngine;


public class DanceFloorController : MonoBehaviour
{
    public FloorTile[] tiles; // Fill with 9 tiles in the Inspector // For tiles 0-8 except 4
    public IdleFloorTile idleTile;    // Reference to tile 4
    private bool _inputLocked;
    public float inputCooldown = 0.25f; // prevent firing every frame

    private void Update()
    {
        var inputDir = InputDirectionManager.GetDirectionVector();

        if (inputDir == Vector2Int.zero)
        {
            // No input — keep idle tile on
            idleTile.TurnOn();
        }
        else if (!_inputLocked)
        {
            idleTile.TurnOff(); // turn off center

            var index = GetTileIndexFromDirection(inputDir);
            if (index < 0 || index >= tiles.Length) return;
            tiles[index].ActivateTile();
            StartCoroutine(InputCooldown());
        }
    }

    private static int GetTileIndexFromDirection(Vector2Int dir)
    {
        var x = dir.x + 1; // -1 → 0, 0 → 1, 1 → 2
        var y = 1 - dir.y; //  1 → 0, 0 → 1, -1 → 2 (inverted Y)
        return y * 3 + x;
    }

    private System.Collections.IEnumerator InputCooldown()
    {
        _inputLocked = true;
        yield return new WaitForSeconds(inputCooldown);
        _inputLocked = false;
    }
}
