using UnityEngine;

public class LevelConfigs
{

        public float Speed { get; set; }
        
        public float AnimSpeed { get; set; }
        public float Ending { get; set; }
        
        public float AnimEnding { get; set; }
        public float Bpm { get; set; }
        
        public string MusicName { get; set; }
        public float Steps { get; set; }

        public LevelConfigs(
            float speed = 200f,
            float animSpeed = 1f,
            float ending = 12f,
            float animEnding = 7f,
            float bpm = 95f,
            string music = "130_120 Gumballs_(LOOPED)",
            float steps = 2.5f
        )
        {
            Speed = speed;
            AnimSpeed = animSpeed;
            Ending = ending;
            AnimEnding = ending;
            Bpm = bpm;
            MusicName = music;
            Steps = steps;
        }
        
        public AudioClip LoadMusicClip()
        {
            return Resources.Load<AudioClip>($"Music/{MusicName}");
        }
}
