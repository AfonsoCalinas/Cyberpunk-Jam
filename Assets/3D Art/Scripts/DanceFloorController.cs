using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DanceFloorController : MonoBehaviour
{
    public FloorTile[] tiles; // Fill with 9 tiles in the Inspector // For tiles 0-8 except 4
    public IdleFloorTile idleTile;    // Reference to tile 4
    private bool inputLocked = false;
    public float inputCooldown = 0.25f; // prevent firing every frame

    void Update()
    {
        Vector2Int inputDir = new Vector2Int(
            Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")),
            Mathf.RoundToInt(Input.GetAxisRaw("Vertical"))
        );

        if (inputDir == Vector2Int.zero)
        {
            // No input — keep idle tile on
            idleTile.TurnOn();
        }
        else if (!inputLocked)
        {
            idleTile.TurnOff(); // turn off center

            int index = GetTileIndexFromDirection(inputDir);
            if (index >= 0 && index < tiles.Length)
            {
                tiles[index].ActivateTile();
                StartCoroutine(InputCooldown());
            }
        }
    }

    int GetTileIndexFromDirection(Vector2Int dir)
    {
        int x = dir.x + 1; // -1 → 0, 0 → 1, 1 → 2
        int y = 1 - dir.y; //  1 → 0, 0 → 1, -1 → 2 (inverted Y)
        return y * 3 + x;
    }

    System.Collections.IEnumerator InputCooldown()
    {
        inputLocked = true;
        yield return new WaitForSeconds(inputCooldown);
        inputLocked = false;
    }
}
