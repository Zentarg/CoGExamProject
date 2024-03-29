﻿using System;
using System.Collections.Generic;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace MainWindow.Models
{
    public class Game
    {
        public Game(Account author, string thumbnailImagePath, string name, float price, int currentDiscountPercentage, string description, string gamePath, List<string> categories, List<CarrouselItem> carrouselItems, DateTimeOffset releaseDate)
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
            ReleaseDate = releaseDate;

            Identifier = Hash(name + description);

        }

        /// <summary>
        /// Generates a Md5 hash based on the string passed.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>Md5 hash of the passed string.</returns>
        private string Hash(string stringToHash)
        {
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(stringToHash, BinaryStringEncoding.Utf8);
            HashAlgorithmProvider hashAlgorithm = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer hashBuffer = hashAlgorithm.HashData(buffer);
            return CryptographicBuffer.EncodeToBase64String(hashBuffer);
        }

        public Account Author { get; }
        public string ThumbnailImagePath { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }
        public int CurrentDiscountPercentage { get; private set; }
        public string Description { get; private set; }
        public string GamePath { get; private set; }
        public List<CarrouselItem> CarrouselItems { get; }
        public List<string> Categories { get; }
        public string Identifier { get; }
        public DateTimeOffset ReleaseDate { get; private set; }


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

        public void SetReleaseDate(DateTimeOffset releaseDate)
        {
            ReleaseDate = releaseDate;
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
