using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	
	public GroceryDisplay[] groceryList;
	public GameObject guesses;

	public GameObject menuUI;
	public GameObject ingameUI;

	public List<GameObject> spawnPoints = new List<GameObject>();
	private List<GroceryDisplay> groceries = new List<GroceryDisplay>();

	private float totalCost;
	public Text totalCostText;
	private int score;
	public Text scoreText;

	private int rippedOff;
	public Text rippedText;

	private int totalScore;
	public Text totalScoreText;



	public Slider timeSlider;
	public float roundTimer = 0.0f;
	public float maxRoundTimer = 5;

	public bool canGuess = false;
	public bool fakePrice = false;
	public bool waitingForNextRound = true;

	
	public AudioSource kaching;
	public AudioSource awkward;
	public AudioSource doubt;

	public CashierScript casherScript;

	// Start is called before the first frame update
	void Start()
    {
    }

	public void StartGame()
	{	
		casherScript.StartCashier();
		rippedOff = 0;
		totalScore = 0;
		rippedText.text = rippedOff.ToString();
		totalScoreText.text = totalScore.ToString();
		score = 0;
		scoreText.text = score.ToString();
		roundTimer = 0;
		SpawnNewGroceries();
		canGuess = true;
		
	}

	void SpawnNewGroceries()
	{
		for (int i = 0; i < groceries.Count; i++)
		{
			if (groceries[i] != null)
			{
				Destroy(groceries[i].gameObject);
			}
		}

		groceries.Clear();
		spawnPoints.Clear();

		spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoint"));
		
		int randomNumber = Random.RandomRange(3, 6);
		totalCost = 0;

		for (int i = 0; i < randomNumber; i++)
		{
			{
				int randomGrocery = Random.RandomRange(0, groceryList.Length);
				int randomSpawnpoint = Random.RandomRange(0, spawnPoints.Count);
				
				GroceryDisplay newGrocery = Instantiate(groceryList[randomGrocery], spawnPoints[randomSpawnpoint].transform.position,
					Quaternion.identity);
				groceries.Add(newGrocery);
				newGrocery.transform.SetParent(this.transform, false);
				
				spawnPoints.RemoveAt(randomSpawnpoint);
			}
		}

		for (int i = 0; i < groceries.Count; i++)
		{
			totalCost += groceries[i].cost;
		}
		totalCost = Mathf.Round(totalCost * 100f) / 100f;

		fakePrice = Random.value > 0.5;

		if (fakePrice)
		{
			totalCost *= Random.RandomRange(1.2f, 2f);
			totalCost = Mathf.Round(totalCost * 100f) / 100f;
		}
		

		totalCostText.text = totalCost.ToString() + "$";
		canGuess = true;
		guesses.SetActive(true);
	}

	IEnumerator WaitingForNewGroceries()
	{
		canGuess = false;
		yield return new WaitForSeconds(1.5f);
		SpawnNewGroceries();
		canGuess = true;
	}

    // Update is called once per frame
    void Update()
    {

	    if (!waitingForNextRound && roundTimer < maxRoundTimer)
	    {
		    roundTimer += Time.deltaTime;
			timeSlider.value = roundTimer;
	    }

	    if (roundTimer >= maxRoundTimer)
	    {
		    ingameUI.SetActive(false);
		    menuUI.SetActive(true);
		    //guesses.SetActive(false);
		    canGuess = false;
		    for (int i = 0; i < groceries.Count; i++)
		    {
			    if (groceries[i] != null)
			    {
				    Destroy(groceries[i].gameObject);
			    }
		    }

		    groceries.Clear();
		    spawnPoints.Clear();
		}

	    if (Input.GetKeyDown(KeyCode.A) && canGuess)
	    {
		    if (!fakePrice)
		    {
			    score += groceries.Count;
				scoreText.text = score.ToString();
			    StartCoroutine(WaitingForNewGroceries());
				kaching.Play();
			    StartCoroutine(casherScript.Happy());
		    }

		    else
		    {
			    StartCoroutine(WaitingForNewGroceries());
				awkward.Play();
			    StartCoroutine(casherScript.Angry());
			    rippedOff -= groceries.Count;
			    rippedText.text = Mathf.Abs(rippedOff).ToString();
		    }

		    totalScoreText.text = (score + rippedOff).ToString();
	    }

	    if (Input.GetKeyDown(KeyCode.X) && canGuess)
	    {
		    if (fakePrice)
		    {
				StartCoroutine(WaitingForNewGroceries());
			    doubt.Play();
			    StartCoroutine(casherScript.Doubt());
			}

		    else
		    {
			    StartCoroutine(WaitingForNewGroceries());
			    awkward.Play();
			    StartCoroutine(casherScript.Doubt());
			}
		    totalScoreText.text = (score + rippedOff).ToString();

		}

	}

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        #else
        Application.Quit();

        #endif
    }
}
