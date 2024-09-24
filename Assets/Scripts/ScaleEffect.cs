using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScaleEffect : MonoBehaviour
{
	[SerializeField]
	[Range(0.01f, 10f)]
	private	float			effectTime;		// ũ�� Ȯ��/��� �Ǵ� �ð�

	private	Text	effectText;     // ũ�� Ȯ��/��� ȿ���� ���Ǵ� �ؽ�Ʈ

	private void Awake()
	{
		effectText = GetComponent<Text>();
	}

	public void Play(float start, float end)
	{
		StartCoroutine(Process(start, end));
	}

	private IEnumerator Process(float start, float end)
	{
		float current = 0;
		float percent = 0;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / effectTime;

			effectText.fontSize = (int)Mathf.Lerp(start, end, percent);

			yield return null;
		}
	}
}

