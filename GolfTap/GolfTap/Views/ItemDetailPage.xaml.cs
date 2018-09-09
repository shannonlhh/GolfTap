using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GolfTap.Models;
using GolfTap.ViewModels;

namespace GolfTap.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };
            item.Segments = new System.Collections.Generic.List<Segment>();
            item.Segments.Add(new Segment()
            {
                Text = "Hole 1",
                Points = new System.Collections.Generic.List<PointBasket>()
            });
            item.Segments.Add(new Segment()
            {
                Text = "Hole 2",
                Points = new System.Collections.Generic.List<PointBasket>()
            });

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
        async void OnSegmentSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var segment = args.SelectedItem as Segment;
            if (segment == null)
                return;

            await Navigation.PushAsync(new SegmentDetailPage(new SegmentDetailViewModel(segment)));

            // Manually deselect item.
            SegmentListView.SelectedItem = null;
        }

    }
}