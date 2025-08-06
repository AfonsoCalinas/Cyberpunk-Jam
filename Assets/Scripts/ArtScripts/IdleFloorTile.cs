using UnityEngine;

public class IdleFloorTile : MonoBehaviour
{
    private static readonly int SwitchLight = Shader.PropertyToID("_SwitchLight");
    public Material tileMaterial;

    public void TurnOn()
    {
        tileMaterial.SetFloat(SwitchLight, 0f);
    }

    public void TurnOff()
    {
        tileMaterial.SetFloat(SwitchLight, 1f);
    }
}
