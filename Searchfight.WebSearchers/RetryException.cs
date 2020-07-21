using System;

namespace Searchfight.WebSearchers
{
    public class RetryException : Exception
    {
        public RetryException(string message, Exception e) : base(message, e)
        {

        }
    }
}