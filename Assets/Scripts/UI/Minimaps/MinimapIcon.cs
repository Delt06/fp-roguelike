using UnityEngine;
using UnityEngine.UI;

namespace UI.Minimaps
{
	public class MinimapIcon : MonoBehaviour
	{
		[SerializeField] private Image _image = default;

		public Image Image => _image;

		public Vector2 Size => RectTransform.rect.size;

		public RectTransform RectTransform { get; private set; }

		public void ResetColor()
		{
			Image.color = _initialColor;
		}

		private void Awake()
		{
			RectTransform = GetComponent<RectTransform>();
			_initialColor = Image.color;
		}

		private Color _initialColor;
	}
}