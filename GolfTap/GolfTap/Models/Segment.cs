using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GolfTap.Models
{
    //Golf Hole
    public class Segment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<PointBasket> Points { get; set; }
    }
}
