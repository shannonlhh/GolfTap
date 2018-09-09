using System;
using System.Collections.Generic;

namespace GolfTap.Models
{
    //Golf Round
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public List<Segment> Segments { get; set; }
    }
}