﻿using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class MinimapIcon : MonoBehaviour
	{
		[SerializeField] private Image _image = default;

		public Image Image => _image;

		public Vector2 Size => RectTransform.rect.size;

		public RectTransform RectTransform { get; private set; }

		private void Awake()
		{
			RectTransform = GetComponent<RectTransform>();
		}
	}
}