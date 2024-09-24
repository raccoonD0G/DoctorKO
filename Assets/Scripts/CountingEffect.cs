using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountingEffect : MonoBehaviour
{
	[SerializeField]
	[Range(0.01f, 10f)]
	private	float			effectTime;		// ī���� �Ǵ� �ð�

	private	Text	effectText;     // ī���� ȿ���� ���Ǵ� �ؽ�Ʈ

	private void Awake()
	{
		effectText = GetComponent<Text>();
	}

	public void Play(int start, int end, UnityAction action=null)
	{
		StartCoroutine(Process(start, end, action));
	}

	private IEnumerator Process(int start, int end, UnityAction action)
	{
		float current = 0;
		float percent = 0;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / effectTime;

			effectText.text = Mathf.Lerp(start, end, percent).ToString("F0");

			yield return null;
		}

		// action�� null�� �ƴϸ� action�� ����Ǿ� �ִ� �޼ҵ� ����
		action?.Invoke();
	}
}

