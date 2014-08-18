using UnityEngine;
using System.Collections;

public class IntroMainStateState : KState<IntroSceneManager.State>
{
	public override void KInitialize()
	{
		base.KInitialize ();
		Debug.Log ("Start");
	}
	public override void KOnEnable()
	{
		base.KOnEnable();
		Debug.Log("Enable");
	}
	public override void KOnDisable()
	{
		base.KOnDisable ();
		Debug.Log("Disable");
	}
	public override void KUpdate()
	{
		base.KUpdate ();
		Debug.Log ("Update");
		Application.LoadLevel ("MainMenuScene");
	}
}
