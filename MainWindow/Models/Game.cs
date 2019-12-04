using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MainWindow.Models
{
    public class Game
    {
        public Game(Account author, string thumbnailImagePath, string name)
        {
            Author = author;
            ThumbnailImagePath = thumbnailImagePath;
            Name = name;
        }

        public Game(Account author, string thumbnailImagePath, string name, float price, int currentDiscountPercentage, string description, string gamePath, List<string> categories)
        {
            Author = author;
            ThumbnailImagePath = thumbnailImagePath;
            Name = name;
            Price = price;
            CurrentDiscountPercentage = currentDiscountPercentage;
            Description = description;
            GamePath = gamePath;
            Categories = categories;
        }

        [JsonConstructor]
        public Game(Account author, string thumbnailImagePath, string name, float price, int currentDiscountPercentage, string description, string gamePath, List<string> categories, List<CarrouselItem> carrouselItems)
        {
            Author = author;
            ThumbnailImagePath = thumbnailImagePath;
            Name = name;
            Price = price;
            CurrentDiscountPercentage = currentDiscountPercentage;
            Description = description;
            GamePath = gamePath;
            Categories = categories;
            CarrouselItems = carrouselItems;
        }

        public Account Author { get; private set; }
        public string ThumbnailImagePath { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }
        public int CurrentDiscountPercentage { get; private set; }
        public string Description { get; private set; }
        public string GamePath { get; private set; }
        public List<CarrouselItem> CarrouselItems { get; private set; }
        public List<string> Categories { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetThumbnail(string path)
        {
            ThumbnailImagePath = path;
        }

        public void SetPrice(float price)
        {
            Price = price;
        }

        public void SetDiscountPercentage(int discountPercentage)
        {
            CurrentDiscountPercentage = discountPercentage;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetGamePath(string gamePath)
        {
            GamePath = gamePath;
        }

        public void AddCategory(string category)
        {
            Categories.Add(category);
        }

        public void AddCategory(List<string> categoryList)
        {
            foreach (string category in categoryList)
            {
                Categories.Add(category);
            }
        }

        public void RemoveCategory(string category)
        {
            Categories.Remove(category);
        }

        public void RemoveCategory(List<string> categoryList)
        {
            foreach (string category in categoryList)
            {
                Categories.Remove(category);
            }
        }

        public void AddCarrouselItem(CarrouselItem item)
        {
            CarrouselItems.Add(item);
        }

        public void AddCarrouselItem(List<CarrouselItem> items)
        {
            foreach (CarrouselItem item in items)
            {
                CarrouselItems.Add(item);
            }
        }

        public void RemoveCarrouselItem(CarrouselItem item)
        {
            CarrouselItems.Remove(item);
        }

        public void RemoveCarrouselItem(List<CarrouselItem> items)
        {
            foreach (CarrouselItem item in items)
            {
                CarrouselItems.Remove(item);
            }
        }

    }
}
