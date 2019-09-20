using System;
namespace MarketPlace.Domain
{
    public class ClassifiedAd
    {
        public ClassifiedAdId Id { get; private set; }
        private UserId _ownerId;
        private ClassifiedAdTitle _title;
        private ClassifiedAdText _text;
        private Price _price;

        public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
        {
            Id = id;
            _ownerId = ownerId;
        }

        public void SetTitle(ClassifiedAdTitle title) => _title = title;

        public void UpdateText(ClassifiedAdText text) => _text = text;

        public void UpdatePrice(Price price) => _price = price;
        
    }
}
