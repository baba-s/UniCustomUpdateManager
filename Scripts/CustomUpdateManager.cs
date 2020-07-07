using System.Collections.Generic;
using UnityEngine;

namespace Kogane
{
	[AddComponentMenu( "" )]
	[DisallowMultipleComponent]
	public sealed class CustomUpdateManager : MonoBehaviour
	{
		//================================================================================
		// 変数(static)
		//================================================================================
		private static CustomUpdateManager m_instance;
		private static List<IUpdatable>    m_list;
		private static bool                m_isQuit;

		//================================================================================
		// プロパティ(static)
		//================================================================================
		public static IReadOnlyList<IUpdatable> List  => m_list;
		public static int                       Count => m_list.Count;

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// 更新される時に呼び出されます
		/// </summary>
		private void Update()
		{
			for ( var i = m_list.Count - 1; i >= 0; i-- )
			{
				var obj = m_list[ i ];
				obj.OnUpdate();
			}
		}

		/// <summary>
		/// アプリケーションを終了する時に呼び出されます
		/// </summary>
		private void OnApplicationQuit()
		{
			m_isQuit = true;
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 初期化します
		/// </summary>
		public static void Initialize()
		{
			Initialize( 0 );
		}

		/// <summary>
		/// 初期化します
		/// </summary>
		public static void Initialize( int capacity )
		{
			if ( m_instance != null ) return;

			var gameObject = new GameObject( nameof( CustomUpdateManager ) )
			{
				hideFlags = HideFlags.HideAndDontSave,
			};

			m_instance = gameObject.AddComponent<CustomUpdateManager>();
			m_list     = new List<IUpdatable>( capacity );

			DontDestroyOnLoad( gameObject );
		}

		/// <summary>
		/// 追加します
		/// </summary>
		public static void Add( IUpdatable obj )
		{
			if ( m_isQuit ) return;
			Initialize();
			m_list.Add( obj );
		}

		/// <summary>
		/// 削除します
		/// </summary>
		public static void Remove( IUpdatable obj )
		{
			if ( m_isQuit ) return;
			Initialize();
			m_list.Remove( obj );
		}

		/// <summary>
		/// すべて削除します
		/// </summary>
		public static void Clear()
		{
			if ( m_isQuit ) return;
			Initialize();
			m_list.Clear();
		}

		/// <summary>
		/// ゲーム実行時に呼び出されます
		/// </summary>
		[RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSceneLoad )]
		private static void RuntimeInitializeOnLoadMethod()
		{
			if ( m_instance != null )
			{
				Destroy( m_instance );
			}

			m_list?.Clear();

			m_instance = null;
			m_list     = null;
			m_isQuit   = false;
		}
	}
}