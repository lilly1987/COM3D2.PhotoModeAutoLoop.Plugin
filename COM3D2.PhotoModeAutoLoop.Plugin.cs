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
	[PluginFilter( "COM3D2x64" ), PluginName("COM3D2.PhotoModeAutoLoop.Plugin"), PluginVersion( "1.0.0.0 edit by lilly 002" )]
	public class PhotoModeAutoLoop: PluginBase
	{
		private bool isStudio = false;
        private int isLoop = 0;
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
                    //SHIFT+CTR+L
					if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
						(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
						Input.GetKeyDown(KeyCode.L))
					{
						isLoop += 1;
                        if(isLoop>=2){
                            isLoop=0;
                        }
                        Debug.Log("PhotoModeAutoLoop Input:"+isLoop);
					}
                    //if (isLoop){
                        
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
                                            //스위치 방식으로 변경
                                            switch (isLoop)
                                            {
                                                case 0:
                                                    anm.wrapMode = WrapMode.Once;
                                                    Console.WriteLine("Case 0 Once");
                                                    break;
                                                case 1:
                                                    anm.wrapMode = WrapMode.Loop;
                                                    Console.WriteLine("Case 1 Loop");
                                                    break;
                                                //case 2:
                                                //    anm.wrapMode = WrapMode.PingPong;
                                                //    Console.WriteLine("Case 2 PingPong");
                                                //    break;
                                                //case 3:
                                                //    anm.wrapMode = WrapMode.Default;
                                                //    Console.WriteLine("Case 3 Default");
                                                //    break;
                                                //case 4:
                                                //    anm.wrapMode = WrapMode.ClampForever;
                                                //    Console.WriteLine("Case 4 ClampForever");
                                                //    break;
                                                //default:
                                                //    Console.WriteLine("Default case");
                                                //    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    //}
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