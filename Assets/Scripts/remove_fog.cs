using System.Collections;
using UnityEngine;

public class remove_fog : MonoBehaviour {

	private bool revertFogState = false;
	void OnPostRender() {
		revertFogState = RenderSettings.fog;
		RenderSettings.fog = enabled;
	}
	void OnPreRender() {
		RenderSettings.fog = revertFogState;
	}
}
