using System;

namespace StoryLine.Wiremock.Services
{
    public class Times : ITimes
    {
        public string Description { get; }
        private readonly Predicate<int> _predicate;

        public Times()
        {
            _predicate = i => i == 1;
            Description = "Once";
        }

        public Times(Predicate<int> predicate, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));

            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            Description = description;
        }

        public bool Evaluate(int count)
        {
            return _predicate(count);
        }


    }
}