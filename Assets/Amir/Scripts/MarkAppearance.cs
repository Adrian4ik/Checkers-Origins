using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarkAppearance : MonoBehaviour
{
	[SerializeField] Image _image;
	[SerializeField] TMP_Text _text;

	public void SetColor(Color color)
	{
		if (_image != null)
			_image.color = color;
	}

	[ContextMenu("SetText(text)")]
	public void SetText(string text)
	{
		_text.text = text;
	}
}
