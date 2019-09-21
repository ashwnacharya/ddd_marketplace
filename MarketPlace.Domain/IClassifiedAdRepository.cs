using System;
using System.Threading.Tasks;

namespace MarketPlace.Domain
{
    public interface IClassifiedAdRepository
    {
        Task<bool> Exists(ClassifiedAdId Id);

        Task<ClassifiedAd> Load(ClassifiedAdId Id);

        Task Save(ClassifiedAd entity);
    }
}