namespace Grapher.Services.Interfaces
{
	public interface ITaskService
	{
		T GetTaskResult<T>(Task<T> taskToRun) where T : new();
	}
}
