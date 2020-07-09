using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using YourPSW.Model;

namespace YourPSW.View.PopUpComponent
{
    public partial class TasksPopUp
    {
        public TasksPopUp()
        {
            InitializeComponent();
           
        }

        //you can change "Timewatch" to any parameter you want to pass back.
        public EventHandler<Timewatch> Action;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<string> hours = new List<string>();
            List<string> minutes = new List<string>();
            List<string> seconds = new List<string>();

            for (int i = 0; i <= 23; i++)
            {
                if (i >= 0 && i <= 9)
                    hours.Add("0" + i.ToString());
                else
                    hours.Add(i.ToString());
            }

            for (int i = 0; i <= 60; i++)
            {
                if (i >=0 && i <= 9)
                {
                    minutes.Add("0" + i.ToString());
                    seconds.Add("0" + i.ToString());
                }
                else
                {
                    minutes.Add(i.ToString());
                    seconds.Add(i.ToString());
                }
            }

            pickerH.ItemsSource = hours;
            pickerM.ItemsSource = minutes;
            pickerS.ItemsSource = seconds;


            List<string> soundList = new List<string>();
            soundList.Add("Bip");
            soundList.Add("Start");
            soundList.Add("Stop");

            soundPicker.ItemsSource = soundList;

            pickerH.SelectedIndex = 0;
            pickerM.SelectedIndex = 0;
            pickerS.SelectedIndex = 0;
            soundPicker.SelectedIndex = 0;

        }


        public async void SaveTask(object sender, EventArgs e)
        {

            string name = null;
            if(taskName.Text == null || taskName.Text == "" || soundPicker.SelectedIndex == -1 || pickerH.SelectedIndex == -1 || pickerM.SelectedIndex == -1 || pickerS.SelectedIndex == -1)
            {
                await DisplayAlert("Attenzione", "I campi non possono essere vuoti!", "ok");
            }else if (pickerH.SelectedItem.ToString() == "00" && pickerM.SelectedItem.ToString() == "00" && pickerS.SelectedItem.ToString() == "00" )
            {
                await DisplayAlert("Attenzione", "il timer non può durare 00:00:00!", "ok");

            }
            else
            {
                string timeH = pickerH.SelectedItem.ToString();
                string timeS = pickerS.SelectedItem.ToString();
                string timeM = pickerM.SelectedItem.ToString();

                name = taskName.Text;
                var time = timeH + ":" + timeM + ":" + timeS;

                string sound = soundPicker.SelectedItem.ToString() + ".mp3";
                Timewatch timewatch = new Timewatch(name, time, sound);
                Action?.Invoke(this, timewatch); // don't forget to invoke the method before close the popup. (only invoke when you want to pass the value back).
                await PopupNavigation.Instance.PopAsync(true);
            }                
        }
    }
}
