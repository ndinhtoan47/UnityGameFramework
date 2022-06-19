using System.Linq;
using UnityEngine;
using GameFramework.Storage;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SpriteRef", menuName = "Game/SpriteRef", order = 1)]
public class SpriteRef : ObjectRef<UnityEngine.Sprite>
{
	private Dictionary<string, Sprite> _spriteDict;
	public List<string> GetSpriteNames()
	{
		InternalInit();
		if (_spriteDict != null)
		{
			return _spriteDict.Keys.ToList();
		}
		return null;
	}
	public Sprite GetSprite(string sprName)
	{
		InternalInit();
		if (_spriteDict.ContainsKey(sprName))
		{
			return _spriteDict[sprName];
		}
		return null;
	}

	public override int FindIndex(System.Func<Sprite, bool> predict)
	{
		InternalInit();
		return base.FindIndex(predict);
	}
	private void InternalInit()
	{
		if (_objs != null && _spriteDict == null)
		{
			_spriteDict = new Dictionary<string, Sprite>();
			for (int i = 0; i < _objs.Length; i++)
			{
				if (_objs[i] != null && !_spriteDict.ContainsKey(_objs[i].name))
				{
					_spriteDict.Add(_objs[i].name, _objs[i]);
				}
			}
		}
	}
}
