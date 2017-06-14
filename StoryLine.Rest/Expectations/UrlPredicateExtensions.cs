using System;

namespace StoryLine.Rest.Expectations
{
    public static class UrlPredicateExtensions
    {
        public static UrlPredicateBuilder FromUrl(this HttpResponse response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            return new UrlPredicateBuilder(response);
        }
    
    }
}