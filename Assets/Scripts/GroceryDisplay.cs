using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroceryDisplay : MonoBehaviour
{

	public Grocery grocery;
	public float cost;
	public Text costText;
	private Vector3 TestPosition;



	// Start is called before the first frame update
	void Awake()
    {
	    cost = grocery.cost;
	    GetComponent<Image>().sprite = grocery.image;
	    costText.text = cost.ToString();
	    
    }

	//private void Start()
	//{
	//	TestPosition = Camera.main.WorldToViewportPoint(new Vector3(transform.position.x + 1000, transform.position.y, transform.position.z));
	//		print(TestPosition);
	//}

	//void Update()
	//{
	//	transform.position = Vector3.MoveTowards(transform.position, new Vector3(TestPosition.x + 5,TestPosition.y, TestPosition.z), 1);
	//}

   
}
