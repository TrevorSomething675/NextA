namespace Nexta.Domain.Enums
{
    public enum UserDetailStatus : int
    {
		Unknown = -1, //Неизвестный статус
		Accepted = 0, //Принят
		AtWork = 1, //В работе
		Rejected = 2, //Отказ
		Waiting = 3 //Ожидает
	}
}
