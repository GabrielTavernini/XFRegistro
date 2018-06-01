﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Registro.Classes.JsonRequest;
using Xamarin.Forms;
using static Registro.Controls.AndroidThemes;
using static Registro.Controls.Notifications;

namespace Registro.Pages
{
    public partial class LoginPage : ContentPage
    {
        School school;

        public LoginPage(School selectedSchool)
        {
            Initialize();
            school = selectedSchool;
        }

        protected void Initialize()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IThemes>().setSettingsTheme(); 
            
            InitializeComponent();
            Title = "Login";

            label1.Scale = 0;
            UserEntry.Scale = 0;
            PassEntry.Scale = 0;
            buttonStack.Scale = 0;
            if (Device.RuntimePlatform == Device.iOS)
            {
                LoginStack.Spacing = 4;
                UserEntry.BackgroundColor = Color.FromHex("#3FFF");
                PassEntry.BackgroundColor = Color.FromHex("#3FFF");
            }
        }

        async void AuthButtonClicked(object sender, EventArgs e)
        {

            await btnAuthenticate.FadeTo(0, App.AnimationSpeed, Easing.SinIn);
            btnAuthenticate.IsVisible = false;

            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;
            LodingLabel.IsVisible = true;

            User user = new User(UserEntry.Text, PassEntry.Text, school);

            //HttpRequest.User = user;
            JsonRequest.user = user;
            if (!await JsonRequest.JsonLogin())//!await HttpRequest.extractAllAsync())
            {
                System.Diagnostics.Debug.WriteLine("Connection Error!");
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
                LodingLabel.IsVisible = false;

                btnAuthenticate.IsVisible = true;
                try { await btnAuthenticate.FadeTo(1, App.AnimationSpeed, Easing.SinIn); }
                catch { }
                return;
            }

            //Get Fine Primo Periodo
            //App.periodChange = JsonRequest.getFinePrimo();

            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            LodingLabel.IsVisible = false;

            Application.Current.Properties["username"] = UserEntry.Text;
            Application.Current.Properties["password"] = PassEntry.Text;
            Application.Current.Properties["school"] = school.name;
            Application.Current.Properties["schoolurl"] = school.loginUrl;

            await label1.FadeTo(0, App.AnimationSpeed, Easing.SinIn);
            await UserEntry.FadeTo(0, App.AnimationSpeed, Easing.SinIn);
            await PassEntry.FadeTo(0, App.AnimationSpeed, Easing.SinIn);



			Dictionary<String, String> dictionary = new Dictionary<String, String>() { { "School Name", school.name } };

			Analytics.TrackEvent("Logged in");
			Analytics.TrackEvent("School Info", dictionary);

            await Application.Current.SavePropertiesAsync();
            await Navigation.PushAsync(new HomePage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Initialize();

            await Task.Delay(App.DelaySpeed);
            await label1.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
            await UserEntry.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
            await PassEntry.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
            await buttonStack.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
        }
    }
}
