﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : TabbedPage
    {
        //public static string Route = App.Current.Resources[$"{nameof(DashboardPage) + nameof(Route)}"].ToString();

        public DashboardPage()
        {
            InitializeComponent();
        }
    }
}