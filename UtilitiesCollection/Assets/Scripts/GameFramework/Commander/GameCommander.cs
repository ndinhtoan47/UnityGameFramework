namespace GameFramework.Pattern
{
	using System.Collections.Generic;

	public class GameCommander<Enum> : ICommanderManager<Enum> where Enum : System.Enum
	{
		private Dictionary<Enum, IGameCommander<Enum>> _commanders = new Dictionary<Enum, IGameCommander<Enum>>();

		public T GetCommander<T>(Enum type) where T : IGameCommander<Enum>
		{
			return (T)GetCommander(type);
		}

		public IGameCommander<Enum> GetCommander(Enum commander)
		{
			if (_commanders.ContainsKey(commander))
			{
				return _commanders[commander];
			}
			return default;
		}

		public bool RegisterCommander(IGameCommander<Enum> commander)
		{
			Enum type = commander.GetCommanderType();
			if (!_commanders.ContainsKey(type))
			{
				commander.SetController(this);
				_commanders.Add(type, commander);
				return true;
			}
			return false;
		}

		public bool RemoveCommander(Enum type)
		{
			if (_commanders.ContainsKey(type))
			{
				IGameCommander<Enum> commander = _commanders[type];
				_commanders.Remove(type);

				commander.Dispose();
				return true;
			}
			return false;
		}

		public void UpdateCommanders()
		{
			foreach (KeyValuePair<Enum, IGameCommander<Enum>> kvpCommander in _commanders)
			{
				kvpCommander.Value.UpdateCommander();
			}
		}

	}
}
