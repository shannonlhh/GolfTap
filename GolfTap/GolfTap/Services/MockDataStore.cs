using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolfTap.Models;

namespace GolfTap.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Rio Hondo 9/2", Description="September 2nd, 2018",
                            Segments = new List<Segment>() {
                                new Segment() { Text = "Hole 1",
                                    Points = new List<PointBasket>() {
                                        new PointBasket() { Text = "D" },
                                        new PointBasket() { Text = "5W" },
                                        new PointBasket() { Text = "S" },
                                        new PointBasket() { Text = "Putt" },
                                        new PointBasket() { Text = "Putt" }
                                    }
                                },
                                new Segment() { Text = "Hole 2",
                                    Points = new List<PointBasket>()
                                }
                            }
                },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Alondra 9/9", Description="September 9th, 2018" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<string>> GetPointBasketOptionsAsync()
        {
            var clubOptions = (new string[] { "D", "3W", "5W", "7W" }).ToList();
            for (int i = 3; i < 10; i++)
                clubOptions.Add(i.ToString());

            clubOptions.AddRange(new string[] { "A", "Pi", "S", "Putt", "X" });

            return await Task.FromResult(clubOptions);
        }
    }
}