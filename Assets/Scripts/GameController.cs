using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private	UIController		uiController;
	[SerializeField]
	//private	GameObject		pattern01;
	private	PatternController	patternController;

	private	readonly float		scoreScale = 20;		// 점수 증가 계수 (읽기전용)

	// 플레이어 점수 (죽지않고 버틴 시간)
	public	float	CurrentScore	{ private set; get; } = 0;

	public	bool	IsGamePlay		{ private set; get; } = false;

	public void GameStart()
	{
		uiController.GameStart();

		//pattern01.SetActive(true);
		patternController.GameStart();

		IsGamePlay = true;
	}

	public void GameExit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
		#else
		Application.Quit();
		#endif
	}

	public void GameOver()
	{
		uiController.GameOver();

		//pattern01.SetActive(false);
		patternController.GameOver();

		IsGamePlay = false;
	}

	private void Update()
	{
		if ( IsGamePlay == false ) return;

		CurrentScore += Time.deltaTime * scoreScale;
	}
}

