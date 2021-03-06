﻿using UnityEngine;

namespace Console.Command
{
	class Debugging
	{
		public static void DoSomething()
		{
			
		}
	}

	class Setting
	{
		public static void SetMaxFPS(int fps = 60)
		{
			Application.targetFrameRate = fps;
			Debug.Log($"Target frame rate is updated! FPS : {fps}");
		}
	}

	class GamePlay
	{
		public static void Exit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit(0);
#endif
		}

		public static void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit(0);
#endif
		}
	}

	class Memory
	{
		public static void ToggleMemory()
		{
			
		}

		public static void ToggleBlockingVolume()
		{
			
		}
	}
}