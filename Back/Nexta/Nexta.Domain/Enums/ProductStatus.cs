namespace Nexta.Domain.Enums
{
	public enum ProductStatus : int
	{
		Unknown = -1, //Неизвестный статус
		InStock = 0, //Есть на складе
		OutOfStock = 1, //Нет на складе
	}
}