using UnityEngine;

public class LevelConfigs
{

        public float Speed { get; set; }
        public float Ending { get; set; }
        public float Bpm { get; set; }
        
        public string MusicName { get; set; }
        public float Steps { get; set; }

        public LevelConfigs(
            float speed = 200f,
            float ending = 12f,
            float bpm = 95f,
            string music = "130_120 Gumballs_(LOOPED)",
            float steps = 2.5f
        )
        {
            Speed = speed;
            Ending = ending;
            Bpm = bpm;
            MusicName = music;
            Steps = steps;
        }
        
        public AudioClip LoadMusicClip()
        {
            return Resources.Load<AudioClip>($"Music/{MusicName}");
        }
}
