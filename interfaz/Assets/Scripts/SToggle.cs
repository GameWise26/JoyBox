using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SToggle : MonoBehaviour
{
	[SerializeField] RectTransform circlreRecTransform;
	public Toggle toggle;
	Vector2 circlepos;

	void Awake()
	{
		toggle = GetComponent<Toggle>();
		circlepos = circlreRecTransform.anchoredPosition;
		toggle.onValueChanged.AddListener(OnSwitch);

		if (toggle.isOn)
		{
			OnSwitch(true);
		}
	}

	void OnSwitch(bool on)
	{
		circlreRecTransform.anchoredPosition = on ? circlepos * -1 : circlepos;
	}

	private void OnDestroy()
	{
		toggle.onValueChanged.AddListener(OnSwitch);
	}
}
