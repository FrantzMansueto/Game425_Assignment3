// ISTA 425 / INFO 525 Algorithms for Games
//
// Sample code file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapObject : MonoBehaviour 
{
	MiniMapEntity linkedMiniMapEntity;
	MiniMapController mmc;
	GameObject owner;
	Camera mapCamera;
	Image spr;
	GameObject panelGO;

	Vector2 screenPos;
	RectTransform sprRect;
	RectTransform rt;

	Transform miniMapTarget;

	void FixedUpdate () 
	{
		if (owner == null)
			Destroy (this.gameObject);
		else
			SetTransform ();
	}

	public void SetMiniMapEntityValues(MiniMapController controller, MiniMapEntity mme, 
									   GameObject attachedGO, Camera renderCamera, GameObject parentPanelGO)
	{
		linkedMiniMapEntity = mme;
		owner = attachedGO;
		mapCamera = renderCamera;
		panelGO = parentPanelGO;
		spr = gameObject.GetComponent<Image> ();
		spr.sprite = mme.icon;
		sprRect = spr.gameObject.GetComponent<RectTransform> ();
		sprRect.sizeDelta = mme.size;
		rt = panelGO.GetComponent<RectTransform> ();
		mmc = controller;
		miniMapTarget = mmc.target;
		//moved outside of function to avoid flickering icons.
		transform.SetParent (panelGO.transform, false);
		SetTransform ();
	}

	// TODO: Implement transformation of registered map icons in MiniMap space
	void SetTransform()
	{
		Vector2 localPos;
		if (owner.transform == miniMapTarget)
		{
			localPos = Vector2.zero;
		}
		else
		{
			Vector3 offset = owner.transform.position - miniMapTarget.position;
			if (mmc.rotateWithTarget)
			{
				offset = Quaternion.Inverse(miniMapTarget.rotation) * offset;
			}
			float scale = rt.sizeDelta.x / (mapCamera.orthographicSize * 2f);
			localPos = new Vector2(offset.x * scale, offset.z * scale);
			if (linkedMiniMapEntity.clampInBorder)
			{
				Vector2 halfSize = rt.sizeDelta / 2f;
				localPos.x = Mathf.Clamp(localPos.x, -halfSize.x, halfSize.x);
				localPos.y = Mathf.Clamp(localPos.y, -halfSize.y, halfSize.y);
			}
			if (linkedMiniMapEntity.clampDist > 0 &&
			    offset.magnitude > linkedMiniMapEntity.clampDist)
			{
				spr.enabled = false;
				return;
			}
			else
			{
				spr.enabled = true;
			}
		}

		sprRect.anchoredPosition = localPos;
		if (linkedMiniMapEntity.rotateWithObject)
		{
			sprRect.rotation = Quaternion.Euler(0, 0, owner.transform.eulerAngles.z);
		}
		else
		{
			sprRect.rotation = Quaternion.identity;
		}
		// Some useful variables (see definitions in project and in Unity docs):
        //
		//   sprRect.anchoredPosition
        //   sprRect.rotation
        //   linkedMiniMapEntity.rotateWithObject
        //   mapCamera.WorldToScreenPoint

	}
}
