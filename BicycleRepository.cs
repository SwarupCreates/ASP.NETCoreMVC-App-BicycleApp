using BicycleApp_MVC.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BicycleApp_MVC.WebApp.Repositories
{

    public class BicycleRepository : IBicycleRepository
    {
        private List<Bicycle> _bicycles;
        private const string _filePath = "bicycles.txt";
        private readonly object _fileLock = new object(); 

        public BicycleRepository()
        {
            _bicycles = new List<Bicycle>();
            LoadBicyclesFromFile();
        }

        private void LoadBicyclesFromFile()
        {
            lock (_fileLock) 
            {
                if (File.Exists(_filePath))
                {
                    var lines = File.ReadAllLines(_filePath);
                    _bicycles = lines.Select(line => line.Split(','))
                                     .Where(parts => parts.Length == 4)
                                     .Select(parts => new Bicycle
                                     {
                                         Id = int.Parse(parts[0]),
                                         Name = parts[1],
                                         Category = parts[2],
                                         Price = Convert.ToDecimal(parts[3])
                                     })
                                     .ToList();
                }
            }
        }

        private void SaveBicyclesToFile()
        {
            lock (_fileLock)
            {
                var lines = _bicycles.Select(b => $"{b.Id},{b.Name},{b.Category},{b.Price}").ToList();
                File.WriteAllLines(_filePath, lines);
            }
        }

        public void AddBicycle(Bicycle bicycle)
        {
            lock (_fileLock) 
            {
                int maxId = _bicycles.Any() ? _bicycles.Max(b => b.Id) : 0;
                bicycle.Id = maxId + 1;
                _bicycles.Add(bicycle);
                SaveBicyclesToFile();
            }
        }

        public List<Bicycle> GetAllBicycles()
        {
            lock (_fileLock) 
            {
                return new List<Bicycle>(_bicycles);
            }
        }

        public Bicycle? GetBicycleById(int id)
        {
            lock (_fileLock) 
            {
                return _bicycles.FirstOrDefault(b => b.Id == id);
            }
        }

        public Bicycle? GetBicycleByName(string name)
        {
            lock (_fileLock)
            {
                return _bicycles.FirstOrDefault(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
        }

        public void EditBicycle(Bicycle bicycle)
        {
            lock (_fileLock)
            {
                var existingBicycle = _bicycles.FirstOrDefault(b => b.Id == bicycle.Id);
                if (existingBicycle != null)
                {
                    existingBicycle.Name = bicycle.Name;
                    existingBicycle.Category = bicycle.Category;
                    existingBicycle.Price = bicycle.Price;
                    SaveBicyclesToFile();
                }
                else
                {
                    throw new KeyNotFoundException("Bicycle not found.");
                }
            }
        }

        public void DeleteBicycle(int id)
        {
            lock (_fileLock)
            {
                var bicycle = _bicycles.FirstOrDefault(b => b.Id == id);
                if (bicycle != null)
                {
                    _bicycles.Remove(bicycle);
                    SaveBicyclesToFile();
                }
                else
                {
                    throw new KeyNotFoundException("Bicycle not found.");
                }
            }
        }
    }
}

