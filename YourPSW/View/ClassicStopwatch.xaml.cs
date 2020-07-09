using System;
using System.Diagnostics;
using Xamarin.Forms;
using YourPSW.Model;

namespace YourPSW.View
{
    public partial class ClassicStopwatch : ContentPage
    {
        System.Diagnostics.Stopwatch stopwatch;
        bool pausa = false;

        public ClassicStopwatch()
        {
            InitializeComponent();
            stopwatch = new System.Diagnostics.Stopwatch();
            lblStopWatch.Text = "00:00:00";
        }


        private void BtnStartClicked(object sender, EventArgs e)
        {
            if (pausa == false)
            {
                stopwatch.Start();
                DependencyService.Get<IAudio>().PlayAudioFile("Start.mp3");
                Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
                {

                    lblStopWatch.Text = stopwatch.Elapsed.ToString().Substring(0, 8);
                    return true;
                });
                DependencyService.Get<IAudio>().PlayAudioFile("Stop.mp3");

                this.btnStart.Text = "Pause";
                this.pausa = true;
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("Start.mp3");
                stopwatch.Stop();
                this.btnStart.Text = "Start";
                this.pausa = false;
            }

        }

        private void BtnResetClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("Bip.mp3");
            stopwatch.Reset();
            this.btnStart.Text = "Start";
            this.pausa = false;
        }

    }
}
