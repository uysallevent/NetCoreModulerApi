namespace Core.Utilities.Results
{
    public class SuccessDataResultWithPaging<T> : DataResultWithPaging<T>
    {
        public SuccessDataResultWithPaging(T data, int totalPage, int currentPage, int perPage) : base(data, totalPage, currentPage, perPage, true)
        {
        }
    }
}