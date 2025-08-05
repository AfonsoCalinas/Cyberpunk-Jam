using UnityEngine;
using System.Collections.Generic;

public class ParticleEffectController : MonoBehaviour
{
    public ParticleSystem[] directionParticles = new ParticleSystem[9];

    private readonly Dictionary<string, int> _inputToIndex = new Dictionary<string, int>
    {
        { "WA", 0 },
        { "W", 1 },
        { "WD", 2 },
        { "A", 3 },
        { "", 4 }, // idle (center)
        { "D", 5 },
        { "SA", 6 },
        { "S", 7 },
        { "SD", 8 }
    };

    public void PlayParticlesForDirection(string dir)
    {
        if (_inputToIndex.TryGetValue(dir, out int index) && index >= 0 && index < directionParticles.Length)
        {
            directionParticles[index].Play();
        }
    }
}
