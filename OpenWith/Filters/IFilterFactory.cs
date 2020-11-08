namespace OpenWith.Filters
{
    public interface IFilterFactory
    {
        IFilter GetFilter(string key, object config);
    }
}
