using UnityEngine;

public class IdleFloorTile : MonoBehaviour
{
    public Material tileMaterial;

    public void TurnOn()
    {
        tileMaterial.SetFloat("_SwitchLight", 0f);
    }

    public void TurnOff()
    {
        tileMaterial.SetFloat("_SwitchLight", 1f);
    }
}
