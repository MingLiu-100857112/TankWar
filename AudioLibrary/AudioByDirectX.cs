using Microsoft.DirectX.AudioVideoPlayback;

namespace AudioLibrary
{
    public class AudioByDirectX
    {
        public Audio audio;
        public AudioByDirectX()
        {
            //audio = new Audio();
        }
        public AudioByDirectX(string url)
        {
            audio = new Audio(url);
        }

        public void Play(string url)
        {
            if (audio != null)
            {
                audio.Open(url);
                audio.Play();
            }
            else
            {
                audio = Audio.FromFile(url);
            }
        }

        public void Play()
        {
            if (audio != null)
            {
                audio.Play();
            }
        }

        public void SetVolume(int volume)
        {
            if (audio != null)
            {
                audio.Volume = volume;
            }
        }
    }
}
