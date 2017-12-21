﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            imgLogo.Source = ImageSource.FromFile("Assets/elderly3.png");
        }

        private async void SearchButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Search());
        }

        private async void AddPatientButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPatient());
        }
    }
}