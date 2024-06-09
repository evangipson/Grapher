using Microsoft.Extensions.DependencyInjection;

using Grapher.Base.DependencyInjection;
using Grapher.Base.Services.Interfaces;

namespace Grapher.Base.Services
{
	[Service(typeof(ITaskService), Lifetime = ServiceLifetime.Singleton)]
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
