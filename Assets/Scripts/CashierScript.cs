using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CashierScript : MonoBehaviour
{
	bool man;

	public Sprite[] girlSprites;

	public Sprite[] guySprites;

	public GameObject awkward;

	public GameObject rippedoff;

	public GameObject nice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void StartCashier()
	{
		man = Random.value > 0.5f;

		if (man)
		{
			GetComponent<SpriteRenderer>().sprite = guySprites[0];
		}
		else
		{
			GetComponent<SpriteRenderer>().sprite = girlSprites[0];
		}
	}

	private void OnEnable()
	{
		
	}

	public IEnumerator Happy()
	{
		if (man)
		{
			GetComponent<SpriteRenderer>().sprite = guySprites[1];
			nice.SetActive(true);
			yield return new WaitForSeconds(1);
			GetComponent<SpriteRenderer>().sprite = guySprites[0];
			nice.SetActive(false);
		}

		else
		{
			GetComponent<SpriteRenderer>().sprite = girlSprites[1];
			nice.SetActive(true);
			yield return new WaitForSeconds(1);
			GetComponent<SpriteRenderer>().sprite = girlSprites[0];
			nice.SetActive(false);
		}

	}

	public IEnumerator Doubt()
	{
		if (man)
		{
			GetComponent<SpriteRenderer>().sprite = guySprites[1];
			awkward.SetActive(true);
			yield return new WaitForSeconds(1);
			GetComponent<SpriteRenderer>().sprite = guySprites[0];
			awkward.SetActive(false);
		}

		else
		{
			GetComponent<SpriteRenderer>().sprite = girlSprites[1];
			awkward.SetActive(true);
			yield return new WaitForSeconds(1);
			GetComponent<SpriteRenderer>().sprite = girlSprites[0];
			awkward.SetActive(false);
		}
	}

	public IEnumerator Angry()
	{
		if (man)
		{
			GetComponent<SpriteRenderer>().sprite = guySprites[3];
			rippedoff.SetActive(true);
			yield return new WaitForSeconds(1);
			GetComponent<SpriteRenderer>().sprite = guySprites[0];
			rippedoff.SetActive(false);
		}

		else
		{
			GetComponent<SpriteRenderer>().sprite = girlSprites[3];
			rippedoff.SetActive(true);
			yield return new WaitForSeconds(1);
			GetComponent<SpriteRenderer>().sprite = girlSprites[0];
			rippedoff.SetActive(false);
		}
	}
}

