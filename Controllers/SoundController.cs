using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


namespace App05MonoGame.Controllers
{
    /// <summary>
    /// Add one value for each sound effect
    /// </summary>
    public enum Sounds
    {
        Coins,
        Collisions
    }
    
    /// <summary>
    /// Sound Controller will manage any sound effects 
    /// or music in the game.
    /// </summary>
    /// <author>
    /// Andrei Cruceru & Derek Peacock
    /// </author>
    public static class SoundController
    {
        #region constants

        // Dictionary Keys

        public const string SongName = "Adventure";
        public const string CoinsEffect = "CoinEffect";
        public const string CollisionEffect = "CollisionEffect";

        #endregion

        // Dictionaries

        private static readonly Dictionary<string, Song> Songs =
            new Dictionary<string, Song>();

        private static readonly Dictionary<string, SoundEffect> SoundEffects = 
            new Dictionary<string, SoundEffect>();
        
        /// <summary>
        /// Load songs and sound effects.
        /// </summary>
        public static void LoadContent(ContentManager content)
        {
            Songs.Add(SongName,content.Load<Song>("Sounds/Adventures"));            

            SoundEffects.Add(CoinsEffect, content.Load<SoundEffect>("Sounds/Coins"));
            SoundEffects.Add(CollisionEffect, content.Load<SoundEffect>("Sounds/flame"));
        }
        

        /// <summary>
        /// Play the selected sound effect if it exists in the
        /// dictionary of sound effects
        /// </summary>
        public static void PlaySoundEffect(Sounds sound)
        {
            switch (sound)
            {
                case Sounds.Coins:
                    SoundEffects[CoinsEffect].Play(); break;
                case Sounds.Collisions:
                    SoundEffects[CollisionEffect].Play(); break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Play a song
        /// </summary>
        /// <param name="song">A string type key assigned to a song.</param>
        public static void PlaySong(string song)
        {
            MediaPlayer.IsRepeating = true;

            MediaPlayer.Play(Songs[song]);
        }

        public static void PauseSong()
        {
            MediaPlayer.Pause();
        }

        public static void ResumeSong()
        {
            MediaPlayer.Resume();
        }
    }
}
