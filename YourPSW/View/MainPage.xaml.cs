using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using YourPSW.Model;
using YourPSW.View;

namespace YourPSW
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<StopwatchDB> stopwatchDBs = await App.Database.GetStopwatches();

            ObservableCollection<StopwatchDB> employees = new ObservableCollection<StopwatchDB>();

            for (int i = 0; i < stopwatchDBs.Count(); i++)
            {
                employees.Add(stopwatchDBs[i]);
            }
            stopwatchListView1.ItemsSource = employees;
        }

        private void onItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedItem = e.Item as StopwatchDB;
            _ = Navigation.PushAsync(new SelectedSW(tappedItem));
        }

        private void openAddPage(object sender, EventArgs e)
        {
            _ = Navigation.PushAsync(new AddTasksAndOptionsPage());
        }

        private void openStopwatch(object sender, EventArgs e)
        {
            _ = Navigation.PushAsync(new ClassicStopwatch());

        }
    }
}
