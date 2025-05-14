namespace Nexta.Domain.Enums
{
	public enum DetailStatus : int
	{
		Unkown = -1, //Неизвестный статус
		InStock = 0, //Есть на складе
		OutOfStock = 1, //Нет на складе
	}
}