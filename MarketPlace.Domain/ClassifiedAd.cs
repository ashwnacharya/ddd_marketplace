using System;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class ClassifiedAd: Entity
    {
        public ClassifiedAdId Id { get; private set; }
        public UserId OwnerId { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text {get; private set; }
        public Price Price {get; private set; }
        public UserId ApprovedBy {get; private set; }
        public ClassifiedAdState State {get; private set; }

        public enum ClassifiedAdState
        {
            PendingReview,
            Active,
            Inactive,
            MarkedAsSold
        }

        public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
        {
            Id = id;
            OwnerId = ownerId;
            State = ClassifiedAdState.Inactive;
            EnsureValidState();

            Raise(new Events.ClassifiedAdCreated
            {
                Id = id, 
                OwnerId = ownerId 
            });
        }

        protected override void EnsureValidState()
        {
            var valid = true;

            if (Id == null)
                valid = valid && false;

            if (OwnerId == null)
                valid = valid && false;

            if (State == ClassifiedAdState.PendingReview)
            {
                if (Title == null)
                    valid = valid && false;


                if (Text == null)
                    valid = valid && false;

                if (Price == null || Price.Amount <= 0)
                    valid = valid && false;
            }
            
            if (State == ClassifiedAdState.Active)
            {
                if (Title == null)
                    valid = valid && false;


                if (Text == null)
                    valid = valid && false;

                if (Price == null || Price.Amount <= 0)
                    valid = valid && false;

                if (ApprovedBy == null)
                    valid = valid && false;
            }

            if (!valid)
                throw new InvalidEntityStateException(
                    this, $"Post-checks failed in state {State}");
        }


        public void SetTitle(ClassifiedAdTitle title)
        {
            Title = title;
            EnsureValidState();

            Raise(new Events.ClassifiedAdTitleChanged
            {
                Id = Id,
                Title = title
            });
        }


        public void UpdateText(ClassifiedAdText text)
        {
            Text = text;
            EnsureValidState();

            Raise(new Events.ClassifiedAdTextUpdated
            {
                Id = Id,
                Text = text
            });
        }


        public void UpdatePrice(Price price)
        {
            Price = price;
            EnsureValidState();

            Raise(new Events.ClassiedAdPriceUpdated
            {
                Id = Id,
                Price = price.Amount,
                CurrencyCode = Price.Currency.CurrencyCode
            });
        }


        public void RequestToPublish()
        {
            State = ClassifiedAdState.PendingReview;
            EnsureValidState();

            Raise(new Events.ClassifiedAdSentForReview
            {
                Id = Id
            });
        }
    }
}
