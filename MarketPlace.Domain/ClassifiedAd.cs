using System;
namespace MarketPlace.Domain
{
    public class ClassifiedAd
    {
        public Guid Id { get; private set; }
        private Guid _ownerId;
        private string _title;
        private string _text;
        private decimal _price;

        public ClassifiedAd(Guid id, Guid ownerId)
        {
            if (id == default)
                throw new ArgumentException("Id should be specified", nameof(id));

            if (ownerId == default)
                throw new ArgumentException("Owner Id should be specified", nameof(ownerId));

            Id = id;
            _ownerId = ownerId;
        }

        public void SetTitle(string title) => _title = title;

        public void UpdateText(string text) => _text = text;

        public void UpdatePrice(decimal price) => _price = price;
        
    }
}
