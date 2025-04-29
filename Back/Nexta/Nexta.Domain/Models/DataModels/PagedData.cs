namespace Nexta.Domain.Models.DataModels
{
	public class PagedData<T>
	{
		public IEnumerable<T>? Items { get; set; }
		public int Count { get; set; }
		public int PageCount { get; set; }

		public PagedData(IEnumerable<T>? items)
		{
			Items = items;
		}

		public PagedData(IEnumerable<T>? items, int count, int pageCount)
		{
			Items = items;
			Count = count;
			PageCount = pageCount;
		}
	}
}