using System.Collections;
using UnityEngine;

public class Pattern09 : MonoBehaviour
{
	[SerializeField]
	private	GameObject		ground;			// �߰� ����
	[SerializeField]
	private	GameObject[]	warningImages;	// ��� �̹��� (��, ��, �ϴ�)
	[SerializeField]
	private	GameObject[]	prefabs;		// ������ �迭
	[SerializeField]
	private	int				setCount;		// ��-��-�ϴ��� �� ��Ʈ�� ������ ���� Ƚ��

	private	AudioSource		audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		StartCoroutine(nameof(Process));
	}

	private void OnDisable()
	{
		StopCoroutine(nameof(Process));
	}

	private IEnumerator Process()
	{
		// ���� ���� �� ��� ����ϴ� �ð�
		yield return new WaitForSeconds(1);

		// ������ Ȱ��ȭ�ϰ� 1�� ���
		ground.SetActive(true);
		yield return new WaitForSeconds(1);

		// �ϴ� - �ߴ� - ��� ���������� ������ ����
		int[] numbers = new int[3] { 0, 1, 2 };
		yield return StartCoroutine(SpawnPrefabSet(numbers, 0.5f, 1));

		// ? - ? - ? ������ ������� ������ ���� (setCount Ƚ����ŭ �ݺ�)
		int count = 0;
		while ( count < setCount )
		{
			// 0~2���� ���ڸ� ������ ������� numbers �迭�� ����
			numbers = Utils.RandomNumbers(3, 3);
			yield return StartCoroutine(SpawnPrefabSet(numbers, 0.5f, 1));

			count ++;
		}

		// ���� ��Ȱ��ȭ
		ground.SetActive(false);

		// ���� ������Ʈ ��Ȱ��ȭ
		gameObject.SetActive(false);
	}

	private IEnumerator SpawnPrefabWithWarning(int index, float waitTime)
	{
		// index��° ��� �̹����� waitTime �ð����� Ȱ��-��Ȱ��
		warningImages[index].SetActive(true);
		yield return new WaitForSeconds(waitTime);
		warningImages[index].SetActive(false);

		// ���� ���
		audioSource.Play();

		// 0 : ���ʿ��� ����������, 1 : �����ʿ��� ��������
		int spawnType = Random.Range(0, 2);
		// ���� prefabs ������Ʈ �� ������ ������Ʈ ����
		int prefabIndex = Random.Range(0, prefabs.Length);
		// ������Ʈ ���� �� ��ġ ����
		Vector3 position = new Vector3(spawnType == 0 ? Constants.min.x : Constants.max.x, warningImages[index].transform.position.y, 0);
		GameObject clone = Instantiate(prefabs[prefabIndex], position, Quaternion.identity);
		// ������Ʈ �̵� ���� ����
		clone.GetComponent<MovementTransform2D>().MoveTo(spawnType == 0 ? Vector3.right : Vector3.left);
	}

	private IEnumerator SpawnPrefabSet(int[] numbers, float delayTime, float endOfWaitTime=1)
	{
		int index = 0;
		while ( index < numbers.Length )
		{
			StartCoroutine(SpawnPrefabWithWarning(numbers[index], delayTime));

			yield return new WaitForSeconds(delayTime * 0.5f);
			
			index ++;
		}

		yield return new WaitForSeconds(endOfWaitTime);
	}
}

