using System;
using System.Text.RegularExpressions;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        public string Value {get; }

        internal ClassifiedAdText(string value) => Value = value;

        public static ClassifiedAdText FromString(string text) =>
            new ClassifiedAdText(text);

        public static implicit operator string(ClassifiedAdText text) => text.Value;

    }
}