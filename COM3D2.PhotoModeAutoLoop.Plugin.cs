using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityInjector;
using UnityInjector.Attributes;

namespace COM3D2.PhotoModeAutLoop.Plugin
{
	#region PluginMain
	///=========================================================================
	/// <summary>スタジオモードモーション自動ループ</summary>
	/// <remarks>
	///	COM3D2.PhotoModeAutoLoop.Plugin : スタジオモードでモーションを自動でループするようにする UnityInjector/Sybaris 用クラス
	///
	/// </remarks>
	///=========================================================================
	[PluginFilter( "COM3D2x64" ), PluginName("COM3D2.PhotoModeAutoLoop.Plugin"), PluginVersion( "1.0.0.0" )]
	public class PhotoModeAutoLoop: PluginBase
	{
		private bool isStudio = false;
		private float deltaTotal = 0;

		public void Awake()
		{
			try
			{
				GameObject.DontDestroyOnLoad(this);
				SceneManager.sceneLoaded += OnSceneLoaded;
			}
			catch (Exception e)
			{
				Debug.LogError(e.ToString());
			}
		}

		public void Update()
		{
			try
			{
				if (isStudio)
				{
					deltaTotal += Time.deltaTime;
					if (deltaTotal > 0.5f)
					{
						deltaTotal = 0;
						List<Maid> maids = GameMain.Instance.CharacterMgr.GetStockMaidList();
						if (maids != null)
						{
							foreach (Maid m in maids)
							{
								if (m != null && m.Visible)
								{
									Animation anm = m.GetAnimation();
									if (anm != null)
									{
										anm.wrapMode = WrapMode.Loop;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Debug.LogError(e.ToString());
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
		{
			try
			{
                if (scene.name == "ScenePhotoMode")
				//if (scene.buildIndex == 27)
				{
					//スタジオモード
					isStudio = true;
				}
				else
				{
					//スタジオモード以外
					isStudio = false;
				}
			}
			catch (Exception e)
			{
				Debug.LogError(e.ToString());
			}
		}

		public void OnGUI()
		{
			try
			{
			}
			catch (Exception e)
			{
				Debug.LogError(e.ToString());
			}
		}

		private void Initialize()
		{
			try
			{
			}
			catch (Exception e)
			{
				Debug.LogError(e.ToString());
			}
		}
		
		
	}
	#endregion
}