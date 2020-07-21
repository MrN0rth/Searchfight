namespace Searchfight.Common
{
    public class Result<T>
    {
        public Result(T value)
        {
            Value = value;
        }

        public Result(string error)
        {
            Error = error;
        }

        public T Value { get; }

        public bool IsSuccess => string.IsNullOrEmpty(Error);

        public string Error { get; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Error) ? Value.ToString() : Error;
        }
    }
}