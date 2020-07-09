using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using YourPSW.Model;
using YourPSW.View.PopUpComponent;

namespace YourPSW.View
{

    public partial class AddTasksAndOptionsPage : ContentPage
    {

        int Timewatch_position = 0;
        List<Timewatch> timewatchTimes = new List<Timewatch>();
        List<StopwatchDB> stopwatchDBs = new List<StopwatchDB>();

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ObservableCollection<Timewatch> employees = new ObservableCollection<Timewatch>();
            for (int i = 0; i < timewatchTimes.Count(); i++)
            {
                employees.Add(timewatchTimes[i]);
            }
            timewatchesListView.ItemsSource = employees;

            List<StopwatchDB> stopwatchDBs = await App.Database.GetStopwatches();

        }

        public AddTasksAndOptionsPage()
        {
            InitializeComponent();
        }


        //BUG DA RISOLVERE... SE CANCELLO l'ultimo timer si sballa il prossimo insert... vedi l'id dello stopwatch auto_increment
        async void btnSaveStopwatch(object sender, EventArgs e)
        {

            if(string.IsNullOrWhiteSpace(StopWatchName.Text) || timewatchTimes.Count() == 0)
            {
                await DisplayAlert("Attenzione!","non hai inserito il nome dello stopwatch o non hai inserito nessun timewatch!", "ok");
            }
            else if (!string.IsNullOrWhiteSpace(StopWatchName.Text) && stopwatchDBs.Count() != 0)
            {
                await App.Database.SaveStopwatchAsync(new StopwatchDB
                {
                    stopwatch_name = StopWatchName.Text,
                    img_name = "Testing"
                });
            }
            else if (!string.IsNullOrWhiteSpace(StopWatchName.Text) && stopwatchDBs.Count() == 0)
            {
                await App.Database.SaveStopwatchAsync(new StopwatchDB
                {
                    //volevo impostare l'id dell'ultimo ma non funziona ... sta qui il bug da risolvere
                    id_stopwatch = 1,
                    stopwatch_name = StopWatchName.Text,
                    img_name = "Testing"
                }) ;
            }


            List<StopwatchDB> lastStopWatch = await App.Database.GetLastStopwatch();
            int lastid = lastStopWatch[0].id_stopwatch;

            if (!string.IsNullOrWhiteSpace(StopWatchName.Text) && timewatchTimes.Count() > 0)
            {
                for (int i = 0; i < Timewatch_position; i++)
                {

                    await App.Database.SaveTimewatchAsync(new TimewatchDB
                    {
                        position = timewatchTimes[i].getPosition(),
                        id_stopwatch = lastid,
                        time_name = timewatchTimes[i].getTime_name(),
                        duration_time = timewatchTimes[i].getDuration_time(),
                        sound_name = timewatchTimes[i].getSound_name()

                    }); ;
                }

                await Navigation.PopAsync();

            }


        }

        async void btnAddANewEntry(object s, EventArgs e)
        {

            List<StopwatchDB> stopwatchDBs = await App.Database.GetLastStopwatch();
            int lastIndex = 0;
            int new_id = 0;


            if (stopwatchDBs.Count() != 0)
            {
                lastIndex = stopwatchDBs.Count();
                new_id = (stopwatchDBs[lastIndex - 1].id_stopwatch) + 1;
            }
            else
            {
                new_id = 1;
            }

            Timewatch timewatch = new Timewatch();
            var page = new TasksPopUp();


            page.Action += async (sender, stringparameter) =>
            {
                timewatch = stringparameter; 

                timewatch.id_stopwatch = new_id;
                timewatch.position = Timewatch_position;

                timewatchTimes.Add(timewatch);
                this.Timewatch_position++;

                ObservableCollection<Timewatch> employees = new ObservableCollection<Timewatch>();
                for (int i = 0; i < timewatchTimes.Count(); i++)
                {
                    employees.Add(timewatchTimes[i]);
                }
                timewatchesListView.ItemsSource = employees;

            };
            await PopupNavigation.Instance.PushAsync(page);
        }

        void btnRemoveALastEntry(object sender, EventArgs e)
        {
            if (Timewatch_position > 0)
            {
                timewatchTimes.RemoveAt(Timewatch_position - 1);
                this.Timewatch_position--;
            }

            ObservableCollection<Timewatch> employees = new ObservableCollection<Timewatch>();
            for (int i = 0; i < timewatchTimes.Count(); i++)
            {
                employees.Add(timewatchTimes[i]);
            }
            timewatchesListView.ItemsSource = employees;

        }
    }
}
