using Grapher.Base.DependencyInjection;
using Grapher.Services.Interfaces;

namespace Grapher.Base.Services
{
	[Service(typeof(ITaskService))]
	public class TaskService : ITaskService
	{
		public T GetTaskResult<T>(Task<T> taskToRun) where T : new()
		{
			T taskResult = new T();

			Task.Run(async () =>
			{
				taskResult = await taskToRun;
			}).Wait();

			return taskResult;
		}
	}
}
