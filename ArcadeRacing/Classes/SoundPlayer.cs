using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes
{
    public class SoundPlayer : IDisposable
    {
        private SoundEffect _sound;
        private SoundEffectInstance _instance;
        private string _name;

        public bool IsRepeating
        {
            get => _instance != default ? _instance.IsLooped : false;
            set
            {
                if (_instance != default)
                    _instance.IsLooped = value;
            }
        }

        public float Voulme
        {
            get
            {
                if (_instance != default)
                    return (int)_instance.Volume;


                return 0;
            }
            set
            {
                if (_instance != default && value >= 0 && value <= 1)
                    _instance.Volume = value;
            }
        }

        public float Pitch
        {
            get
            {
                if (_instance != default)
                    return (int)_instance.Pitch;


                return 0;
            }
            set
            {
                if (_instance != default && value >= -1 && value <= 1)
                    _instance.Pitch = value;
            }
        }

        public SoundPlayer(string name)  => _name = name;

        public void Dispose()
        {
            _instance.Dispose();
            _sound.Dispose();
            _name = default;
        }

        public void LoadContent(ContentManager content)
        {
            _sound = content.Load<SoundEffect>(_name);
            _instance = _sound.CreateInstance();
        }

        public void Play() => _instance?.Play();

        public void Stop() => _instance?.Stop();

        public void Pause() => _instance?.Pause();

        public void Resume() => _instance?.Resume();
    }
}
