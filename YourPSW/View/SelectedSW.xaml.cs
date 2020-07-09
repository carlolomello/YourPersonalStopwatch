using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using YourPSW.Model;

namespace YourPSW.View
{
    public partial class SelectedSW : ContentPage
    {

        List<TimewatchDB> TimewatchDBs;
        System.Diagnostics.Stopwatch stopwatch;
        bool pausa = false;
        int curr_i = 0;
        int i_max;

        public SelectedSW(StopwatchDB stopwatchDB)
        {
            InitializeComponent();
            Lb_stopwatch_name.Text = stopwatchDB.stopwatch_name;
            Lb_Id.Text = stopwatchDB.id_stopwatch.ToString();
            stopwatch = new System.Diagnostics.Stopwatch();
            lblStopWatch.Text = "00:00:00";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.TimewatchDBs = await App.Database.GetTimewatches(Lb_Id.Text);
            ObservableCollection<TimewatchDB> employees = new ObservableCollection<TimewatchDB>();
            for (int i = 0; i < TimewatchDBs.Count(); i++)
            {
                employees.Add(TimewatchDBs[i]);
            }
            TasksListView.ItemsSource = employees;
            this.i_max = TimewatchDBs.Count() - 1;
        }

        private async void DeleteBtn(object sender, EventArgs e)
        {

            bool answer = await DisplayAlert("Attenzione!!!", "Sicuro di voler cancellare?", "Si", "No");
            if(answer == true)
            {
                App.Database.DeleteStopwatch(Lb_Id.Text);
                await Navigation.PopAsync();
            }
            
           
        }
        
        private void BtnStartClicked(object sender, EventArgs e)
        {
            string curr_time = this.TimewatchDBs[curr_i].duration_time;
            
            if (pausa == false)
            {
                DependencyService.Get<IAudio>().PlayAudioFile(this.TimewatchDBs[curr_i].sound_name);

                stopwatch.Start();
                Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
                {
                    lblStopWatch.Text = stopwatch.Elapsed.ToString().Substring(0, 8);


                        if ((string.Compare(lblStopWatch.Text, curr_time) > 0) & curr_i < i_max)
                    {
                        this.curr_i++;
                        DependencyService.Get<IAudio>().PlayAudioFile(this.TimewatchDBs[curr_i].sound_name);
                        stopwatch.Reset();
                        stopwatch.Start();
                        curr_time = this.TimewatchDBs[curr_i].duration_time;
                    }
                    else if ((string.Compare(lblStopWatch.Text, curr_time) > 0) & curr_i == i_max)
                    {
                        stopwatch.Stop();
                        DependencyService.Get<IAudio>().PlayAudioFile("Stop.mp3");
                        stopwatch.Reset();
                    }

                    return true;
                });

                this.btnStart.Text = "Pause";
                this.pausa = true;
            }
            else
            {
                stopwatch.Stop();
                this.btnStart.Text = "Start";
                this.pausa = false;
            }
        }

        private void BtnResetClicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
            this.btnStart.Text = "Start";
            this.pausa = false;
            this.curr_i = 0;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }
}
