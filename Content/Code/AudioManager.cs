using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Project_Yokai.Content.Code
{
    //The manager for the game's music and sound effects
    public class AudioManager
    {
        private Song backgroundMusic;
        private SoundEffect soundEffect;

        public void LoadContent(ContentManager content, string backgroundMusicPath)
        {
            backgroundMusic = content.Load<Song>(backgroundMusicPath);
        }

        public void LoadSoundEffect(ContentManager content, string soundEffectPath)
        {
            soundEffect = content.Load<SoundEffect>(soundEffectPath);
        }

        public void PlayBackgroundMusic()
        {
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }
        public void PlaySoundEffect()
        {
            soundEffect.Play();

        }

        public void StopMusic()
        {
            MediaPlayer.Stop();
        }
    }
}
