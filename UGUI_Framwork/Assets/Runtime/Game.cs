using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EverydayDevup.Framework
{
	/// <summary>
	/// Framework의 Main Class
	/// </summary>
	public class Game : Singleton<Game>
	{
		/// <summary>
		/// 단말기의 해상도를 관리하는 매니저
		/// </summary>
		public ScreenManager screenManager;
		/// <summary>
		/// Asset을 관리하는 매니저
		/// </summary>
		public ResourcesManager resourcesManager;
		/// <summary>
		/// 암호화를 관리하는 매니저
		/// </summary>
		public CryptoManager cryptoManager;
		/// <summary>
		/// UI를 관리하는 매니저
		/// </summary>
		public UIManager uiManager;

		List<IManager> managerList = new List<IManager>();

		protected virtual void Start()
		{
			managerList.Add( screenManager );
			managerList.Add( resourcesManager );
			managerList.Add( cryptoManager );
			managerList.Add( uiManager );

			uiManager.Push( UI_TYPE.LOADING );
		}

		protected virtual void Update()
		{
			IManager manager;
			for( int i = 0, icount = managerList.Count; i < icount; ++i )
			{
				manager = managerList[i];
				if( manager != null )
				{
					manager.ManagerUpdate();
				}
			}
		}

		protected virtual void GameLoop()
		{
			IManager manager;
			for( int i = 0, icount = managerList.Count; i < icount; ++i )
			{
				manager = managerList[i];
				if( manager != null )
				{
					manager.ManagerLoop();
				}
			}
		}

		protected virtual void Clear()
		{
			IManager manager;
			for( int i = 0, icount = managerList.Count; i < icount; ++i )
			{
				manager = managerList[i];
				if( manager != null )
				{
					manager.ManagerClear();
				}
			}
		}

		private IEnumerator Loop()
		{
			WaitForSeconds wfs = new WaitForSeconds( 1f );
			while( true )
			{
				yield return wfs;
				GameLoop();
			}
		}
	}
}
