﻿using System;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class ClassifiedAd: AggregateRoot<ClassifiedAdId>
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

        public ClassifiedAd(ClassifiedAdId id, UserId ownerId) =>
            Apply(new Events.ClassifiedAdCreated
            {
                Id = id,
                OwnerId = ownerId
            });

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


        public void SetTitle(ClassifiedAdTitle title) =>
            Apply(new Events.ClassifiedAdTitleChanged
            {
                Id = Id,
                Title = title
            });



        public void UpdateText(ClassifiedAdText text) =>
            Apply(new Events.ClassifiedAdTextUpdated
            {
                Id = Id,
                Text = text
            });


        public void UpdatePrice(Price price) =>
            Apply(new Events.ClassiedAdPriceUpdated
            {
                Id = Id,
                Price = price.Amount,
                CurrencyCode = price.Currency.CurrencyCode
            });


        public void RequestToPublish() =>
            Apply(new Events.ClassifiedAdSentForReview
            {
                Id = Id
            });


        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ClassifiedAdCreated e:
                    Id = new ClassifiedAdId(e.Id);
                    OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedAdState.Inactive;
                    break;

                case Events.ClassifiedAdTitleChanged e:
                    Title = new ClassifiedAdTitle(e.Title);
                    break;

                case Events.ClassifiedAdTextUpdated e:
                    Text = new ClassifiedAdText(e.Text);
                    break;

                case Events.ClassiedAdPriceUpdated e:
                    Price = new Price(e.Price, e.CurrencyCode);
                    break;

                case Events.ClassifiedAdSentForReview e:
                    State = ClassifiedAdState.PendingReview;
                    break;
            }
        }
    }
}
