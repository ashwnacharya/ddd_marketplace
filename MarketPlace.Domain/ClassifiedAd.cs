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

    }
}
