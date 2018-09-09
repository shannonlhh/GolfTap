using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GolfTap.Models;
using GolfTap.ViewModels;

namespace GolfTap.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SegmentDetailPage : ContentPage
    {
        SegmentDetailViewModel viewModel;

        public SegmentDetailPage(SegmentDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public SegmentDetailPage()
        {
            InitializeComponent();

            var segment = new Segment
            {
                Text = "Segment 1",
                Points = new System.Collections.Generic.List<PointBasket>()
            };

            viewModel = new SegmentDetailViewModel(segment);
            BindingContext = viewModel;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.PointBasketOptions.Count == 0)
                viewModel.LoadPointBaksetsCommand.Execute(null);
        }

        async void OnSavedPointBasketSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var pointBasket = args.SelectedItem as PointBasket;
            if (pointBasket == null)
                return;

            var confirmedDeletion = await DisplayAlert("Delete Point", $"Do you want to delete this data point? Club selection: {pointBasket.Text}", "Yes", "No");

            if (confirmedDeletion)
                RemoteSelectedPointBasket(pointBasket);
        }
        void RemoteSelectedPointBasket(PointBasket pointBasket)
        {
            viewModel.Segment.Points.Remove(pointBasket);
            viewModel.LoadPointBaksetSelectionsCommand.Execute(null);
        }
        void OnNewPointBasketSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var pointBasketText = args.SelectedItem as string;
            if (pointBasketText == null)
                return;

            viewModel.Segment.Points.Add(new PointBasket() { Text = pointBasketText });
            viewModel.LoadPointBaksetSelectionsCommand.Execute(null);
        }

    }
}