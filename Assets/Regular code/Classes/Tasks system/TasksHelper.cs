using More.Basic.Management;
using System.Linq;

/// <summary>
/// Класс для работы с заданиями.
/// </summary>
public static class TasksHelper
{
	/// <summary>
	/// Настройки заданий.
	/// </summary>
	public static class TasksSetups
	{
		/// <summary>
		/// Уровень 1.
		/// </summary>
		public static class Level1
		{
			/// <summary>
			/// Настройка задания 1.
			/// </summary>
			/// <param name="task"> Задание. </param>
			public static void Task_1_setup(TaskBase task)
			{
				MonstersManager.Instance.MonsterKilled += () => (task as EliminationTask).ChangeAmount(+1);
			}

			/// <summary>
			/// Настройка задания 2.
			/// </summary>
			/// <param name="task"> Задание. </param>
			public static void Task_2_setup(TaskBase task)
			{
				PlayerManager.Instance.Controller.GetComponent<AttackController>().ProjectileReleased += (args) =>
				{
					if (args.ProjectileBehaviourType == ProjectileBehaviourType.ForwardFlying && args.ProjectileEffectTypes.Contains(ProjectileEffectType.DamageDealing))
					{
						(task as CollectiveTask).ChangeAmount(+1);
					}
				};
			}
		}
	}
}
