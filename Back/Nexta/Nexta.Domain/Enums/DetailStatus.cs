namespace Nexta.Domain.Enums
{
	public enum DetailStatus : int
	{
		Unkown = -1, //Неизвестный статус
		Rejected = 0, //Отказ
		Accepted = 1, //Принят
		AtWork = 2, //В работе
		Waiting = 3 //Ожидает
	}
}