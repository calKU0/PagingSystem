using PagingSystem.Models;
using PagingSystem.ViewModels;
using PagingSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PagingSystem.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void statusPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsBusy == true) return;
            int selectedStatusIndex = statusPicker.SelectedIndex;
            await _viewModel.ExecuteLoadItemsCommand(selectedStatusIndex);
        }
    }
}