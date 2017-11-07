using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GlobalVars;


/// <summary>
/// this class plays the role of the Trigger to send vars to the Events
/// </summary>

public class UnitTrigger : MonoBehaviour {

	[SerializeField] float JumpYForse;

	[SerializeField] Slider Xslider;
	[SerializeField] Slider Zslider;


	[SerializeField] Color[] ColorArray;


	#region Camvas -> Button Panel calls

	// called on Jump Button
	public void Jump(){
		EventsClass.CallJump (JumpYForse);
	}

	// called in Change Color Butotn
	public void ChangeColors(){
		EventsClass.CallColorArray (ColorArray);		
	}

	// these two called on their slider change
	public void ChangeX(){
		EventsClass.CallXChange (Xslider.value);
	}
	public void ChangeZ(){
		EventsClass.CallZChange (Zslider.value);
	}

	#endregion

}
