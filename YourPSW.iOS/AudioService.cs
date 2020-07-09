using System;
using Xamarin.Forms;
using SimpleAudio.iOS;
using System.IO;
using Foundation;
using AVFoundation;
using YourPSW.Model;

[assembly: Dependency(typeof(AudioService))]
namespace SimpleAudio.iOS
{
    public class AudioService : IAudio
    {
        public AudioService()
        { }
        public void PlayAudioFile(string fileName)
        {
            string sFilePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            NSUrl url = NSUrl.FromString(sFilePath);
            var _player = AVAudioPlayer.FromUrl(url);
            _player.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
            {
                _player = null;
            };
            _player.Play();
        }
    }
}
