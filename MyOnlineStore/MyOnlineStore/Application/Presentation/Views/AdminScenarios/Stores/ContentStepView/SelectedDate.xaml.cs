﻿using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.ContentStepView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedDate : ContentPage
    {
        public SelectedDate()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IFeaturedItemUploadViewModel>();
        }

        private async void next(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPicture());
        }

        private async void back(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}