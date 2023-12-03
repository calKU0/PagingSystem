using PagingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagingSystem.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            Reports reports = new Reports();
            items = new List<Item>();
            for (int i = 0; i < reports.GetReportsCount(); i++)
            {
                items.Add(new Item { Id = reports.ReportID[i].ToString(), Text = reports.Operator[i], Description = reports.Localisation[i] });
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            Reports.CompleteReport(int.Parse(id));

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

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = true, int status = 2 )
        {
            if (forceRefresh)
            {
                Reports reports = new Reports();
                items.Clear();
                for (int i = 0; i < reports.GetReportsCount(); i++)
                {
                    items.Add(new Item { Id = reports.ReportID[i].ToString(), Text = reports.Operator[i], Description = reports.Localisation[i], Status = reports.Status[i] });
                }
            }

            return await Task.FromResult(status == 2 ? items : items.Where((Item arg) => arg.Status == status));
        }
    }
}