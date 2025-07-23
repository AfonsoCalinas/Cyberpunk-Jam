using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DanceFloorController : MonoBehaviour
{
    public FloorTile[] tiles; // Fill with 9 tiles in the Inspector

    /*private Dictionary<KeyCode, int> keyToTileIndex;

    void Start()
    {
        keyToTileIndex = new Dictionary<KeyCode, int>
        {
            { KeyCode.W, 1 },   // Back
            { KeyCode.S, 7 },   // Front
            { KeyCode.A, 3 },   // Left
            { KeyCode.D, 5 },   // Right
            //{ KeyCode.W + KeyCode.A, 0 }, // diagonal
            // add more if needed
        };
    }

    void Update()
    {
        foreach (var kvp in keyToTileIndex)
        {
            if (Input.GetKeyDown(kvp.Key))
            {
                FloorTile tile = tiles[kvp.Value];
                tile.ActivateTile();
            }
        }

        // for diagonals (tipe WA):
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            tiles[0].ActivateTile();
        }
        
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            tiles[2].ActivateTile();
        }
        
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            tiles[6].ActivateTile();
        }
        
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            tiles[8].ActivateTile();
        }
    }*/

    private bool inputLocked = false;
    public float inputCooldown = 0.25f; // prevent firing every frame

    void Update()
    {
        Vector2Int inputDir = new Vector2Int(
            Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")),
            Mathf.RoundToInt(Input.GetAxisRaw("Vertical"))
        );

        if (inputDir != Vector2Int.zero && !inputLocked)
        {
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
