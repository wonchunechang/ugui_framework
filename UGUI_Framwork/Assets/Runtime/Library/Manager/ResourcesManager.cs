using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

namespace EverydayDevup.Framework
{
	/// <summary>
	/// Addressable Asset System을 활용하여 Unity Asset을 관리하는 클래스
	/// </summary>
	public class ResourcesManager : MonoBehaviour, IManager
	{
		/// <summary>
		/// Asset을 Load하는 함수
		/// </summary>
		/// <param name="assetReference"> Asset의 주소</param>
		/// <param name="root"> Asset이 로드 후, instantiate 되었을 때의 부모 Transform</param>
		/// <param name="successCallback"> 로드 및 instantiate에 성공했을 때 불리는 함수</param>
		/// <param name="failCallback"> 로드 및 instantitate에 실패 했을 때 불리는 함수</param>
		public void Load(AssetReference assetReference, Transform root, System.Action<GameObject> successCallback, System.Action failCallback)
		{
			StartCoroutine( LoadAsset( assetReference, root, successCallback, failCallback ) );
		}

		/// <summary>
		/// Addressable Asset을 로드할 때 비동기 방식으로 로드 되기 때문에 코루틴을 사용
		/// </summary>
		/// <param name="assetReference"> Asset의 주소</param>
		/// <param name="root"> Asset이 로드 후, instantiate 되었을 때의 부모 Transform</param>
		/// <param name="successCallback"> 로드 및 instantiate에 성공했을 때 불리는 함수</param>
		/// <param name="failCallback"> 로드 및 instantitate에 실패 했을 때 불리는 함수</param>
		/// <returns></returns>
		public IEnumerator LoadAsset(AssetReference assetReference, Transform root, System.Action<GameObject> successCallback, System.Action failCallback)
		{
			GameObject go;
			AsyncOperationHandle<GameObject> handle = assetReference.InstantiateAsync( root );
			yield return handle;

			if( handle.Status == AsyncOperationStatus.Succeeded )
			{
				go = handle.Result;
				successCallback.Invoke( go );
			}
			else
			{
				failCallback.Invoke();
			}
		}

		/// <summary>
		/// Asset을 Unload하는 함수
		/// </summary>
		/// <param name="assetReference">  Asset의 주소 </param>
		/// <param name="go"> Addressable로 로드 및 instantiate된 GameObejct</param>
		/// <param name="isCaching"> isCaching이 true이면 ReleaseAsset을 하지 않음으로써 다음 로드 시 속도를 향상 시킴</param>
		public void UnLoad(AssetReference assetReference, GameObject go, bool isCaching)
		{
			assetReference.ReleaseInstance( go );

			if( isCaching == false )
			{
				assetReference.ReleaseAsset();
			}
		}

		public void ManagerUpdate() { }
		public void ManagerLoop() { }
		public void ManagerClear() { }
	}
}
