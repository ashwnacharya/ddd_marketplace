﻿using System;
namespace MarketPlace.Domain
{
    public class ClassifiedAd
    {
        public ClassifiedAdId Id { get; private set; }
        private UserId _ownerId;
        private ClassifiedAdTitle _title;
        private string _text;
        private decimal _price;

        public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
        {
            Id = id;
            _ownerId = ownerId;
        }

        public void SetTitle(ClassifiedAdTitle title) => _title = title;

        public void UpdateText(string text) => _text = text;

        public void UpdatePrice(decimal price) => _price = price;
        
    }
}
