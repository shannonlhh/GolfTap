using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using GolfTap.Models;
using Xamarin.Forms;

namespace GolfTap.ViewModels
{
    public class SegmentDetailViewModel : BaseViewModel
    {
        public Segment Segment { get; set; }
        public ObservableCollection<PointBasket> PointBasketSelections { get; set; }
        public Command LoadPointBaksetSelectionsCommand { get; set; }
        public ObservableCollection<string> PointBasketOptions { get; set; }
        public Command LoadPointBaksetsCommand { get; set; }

        public SegmentDetailViewModel(Segment segment = null)
        {
            Title = segment?.Text;
            Segment = segment;
            PointBasketSelections = new ObservableCollection<PointBasket>();
            foreach (var point in segment.Points)
                PointBasketSelections.Add(point);

            LoadPointBaksetSelectionsCommand = new Command(async () => await ExecuteLoadPointBaksetSelectionsCommand());
            PointBasketOptions = new ObservableCollection<string>();
            LoadPointBaksetsCommand = new Command(async () => await ExecuteLoadPointBaksetsCommand());
        }
        
        async Task ExecuteLoadPointBaksetSelectionsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                PointBasketSelections.Clear();
                foreach (var point in Segment.Points)
                    PointBasketSelections.Add(point);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        async Task ExecuteLoadPointBaksetsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                PointBasketOptions.Clear();
                var items = await DataStore.GetPointBasketOptionsAsync();
                foreach (var item in items)
                {
                    PointBasketOptions.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
