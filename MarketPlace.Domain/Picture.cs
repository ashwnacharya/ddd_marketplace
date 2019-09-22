using System;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class PictureId: Value<PictureId>
    {
        public PictureId(Guid value) => Value = value;

        public Guid Value { get; }
    }

    public class Picture: Entity<PictureId>
    {
        internal ClassifiedAdId ParentId { get; private set; }
        internal PictureSize Size {get; set; }
        internal Uri Location { get; set; }
        internal int Order {get; set; }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.PictureAddedToAClassifiedAd e:
                    Id = new PictureId(e.PictureId);
                    Location = new Uri(e.Url);
                    Size = new PictureSize
                    {
                        Height = e.Height,
                        Width = e.Width
                    };
                    Order = e.Order;
                    break;
            }
        }

        public Picture(Action<object> applier) : base(applier) {}
    }
}