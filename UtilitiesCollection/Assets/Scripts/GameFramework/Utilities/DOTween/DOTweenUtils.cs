#define GF_USE_DOTWEEN

namespace GameFramework.Utilities
{
#if GF_USE_DOTWEEN

	using DG.Tweening;

	public static class DOTweenUtils
	{
		public static Tween UpdateValue(float duration, System.Action<float> onUpdate)
		{
			float value = 0.0f;
			return DOTween.To(() => value, (f) => value = f, 1.0f, duration).OnUpdate(() =>
			{
				onUpdate?.Invoke(value);
			});
		}
		public static Tween DelayCall(float time, System.Action doAction, bool ignoreTimeScale = true)
		{
			return DOVirtual.DelayedCall(
					delay: time,
					callback: () =>
					{
						doAction?.Invoke();
					},
					ignoreTimeScale: ignoreTimeScale);
		}
		public static Tween IntervalUpdate(float interval, System.Func<bool> doAction, bool excuteRightAway = false)
		{
			if (excuteRightAway)
			{
				bool isDone = doAction.Invoke();
				if (isDone)
				{
					return null;
				}
			}
			float value = 0.0f;
			Tween updateTween = DOTween.To(() => value, (f) => value = f, 1.0f, interval).SetLoops(-1, LoopType.Restart);
			updateTween.OnStepComplete(() =>
			{
				bool isDone = doAction.Invoke();
				if (isDone)
				{
					updateTween.Kill(false);
				}
			});
			return updateTween;
		}

		public static Tween IntervalUpdate(float interval, float timeOut, System.Func<bool> doAction, System.Action<bool> timeOutCallback)
		{
			bool isTimeOuted = false;
			UpdateValue(timeOut, null).OnComplete(() =>
			{
				isTimeOuted = true;
			});

			float value = 0.0f;
			Tween updateTween = DOTween.To(() => value, (f) => value = f, 1.0f, interval).SetLoops(-1, LoopType.Restart);
			updateTween.OnStepComplete(() =>
			{
				bool isDone = doAction.Invoke() || isTimeOuted;
				if (isDone)
				{
					updateTween.Kill(false);
					if (isTimeOuted)
					{
						timeOutCallback?.Invoke(true);
					}
					else
					{
						timeOutCallback?.Invoke(false);
					}
				}
			});
			return updateTween;
		}
	}
#endif
}