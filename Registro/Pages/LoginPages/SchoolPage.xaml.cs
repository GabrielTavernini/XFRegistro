﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Registro.Controls.AndroidThemes;
using static Registro.Controls.Notifications;

namespace Registro.Pages
{
    public partial class SchoolPage : ContentPage
    {
        public SchoolPage()
        {
            Initialize();
        }

        protected void Initialize()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IThemes>().setArgumentsTheme();  //Android Themes
            
            InitializeComponent();
            Title = "Scuola";

            SchoolPicker.ItemsSource = new List<string>(App.Schools.Keys);
            label1.Scale = 0;
            PickerStack.Scale = 0;
            buttonStack.Scale = 0;

            if (Device.RuntimePlatform == Device.iOS)
            {
                SchoolPicker.BackgroundColor = Color.FromHex("#3FFF");
            }
        }


        async void AuthButtonClicked(object sender, EventArgs e)
        {
            if((string)SchoolPicker.SelectedItem != null)
            {
                await label1.FadeTo(0, App.AnimationSpeed, Easing.SinIn);
                await PickerStack.FadeTo(0, App.AnimationSpeed, Easing.SinIn);
                await buttonStack.FadeTo(0, App.AnimationSpeed, Easing.SinIn);

                await Navigation.PushAsync(new LoginPage(App.Schools[(string)SchoolPicker.SelectedItem]));
            }
            else
            {
                if (Device.RuntimePlatform == Device.Android)
                    DependencyService.Get<INotifyAndroid>().DisplayToast("Seleziona una scuola"); 
                else
                    DependencyService.Get<INotifyiOS>().ShowToast("Seleziona una scuola", 750);
            }
        }

        async void UrlButtonClicked(object sender, EventArgs e)
        {
            await label1.FadeTo(0, App.AnimationSpeed, Easing.SinIn);
            await PickerStack.FadeTo(0, App.AnimationSpeed, Easing.SinIn);
            await buttonStack.FadeTo(0, App.AnimationSpeed, Easing.SinIn);

            await Navigation.PushAsync(new SchoolUrlPage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(App.DelaySpeed);
            await label1.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
            await PickerStack.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
            await buttonStack.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
        }
    }
}
