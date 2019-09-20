using System;
using System.Text.RegularExpressions;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        private readonly string _value;

        private ClassifiedAdText(string value) => _value = value;

        public static ClassifiedAdText FromString(string title) =>
            new ClassifiedAdText(title);

    }
}