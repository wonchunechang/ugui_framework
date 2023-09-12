using UnityEngine;
using EverydayDevup.Framework;

public class UILoading : UIBase
{
	public UIReference refData;

	public override void OnInit()
	{
		base.OnInit();

		TMPro.TMP_Text text = refData.GetData<TMPro.TMP_Text>( "company_name" );
		text.SetText( "EveryDay.DevUp" );
	}

	public void OnClick()
	{
		
	}
}
