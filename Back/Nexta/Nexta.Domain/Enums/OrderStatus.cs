namespace Nexta.Domain.Enums
{
    public enum OrderStatus : int
    {
		Unknown = -1, //Неизвестный статус
		Accepted = 0, //Принят
		InProgress = 1, //В работе
		Canceled = 2, //Отменён
		Ready = 3, //Готов к выдаче
		Complete = 4 //Завершён
	}
}