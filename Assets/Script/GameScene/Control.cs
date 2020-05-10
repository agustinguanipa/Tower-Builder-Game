using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Control : MonoBehaviour {

    public static Control instance = null;
    public List<TowerBox> towerBoxList = null;

	[SerializeField]
	private GameObject floor;
	
	[SerializeField]
	private GameObject towerListParent;

	[SerializeField]
	private GameObject Anchor;

	[SerializeField]
	private GameObject modelPrefab;

    [SerializeField]
    private GameObject effectPrefab;

    private GameObject effectObj = null;
    private GameObject tower = null;
    private TowerBox towerBox = null;
    private int count = 0;
    private bool isSky = false;
    private int topIndex = 0;
    private int failCount = 0;

	//UI control
    private UILabel houseUILabel;
    private UISlider timeSlider;
    private UIProgressBar timeProgressBar;
	public GameObject particle;

	void Awake () {
        instance = this;

		towerBoxList = new List<TowerBox> ();
		CreateTower ();

		GameObject uiHouseNumber = GameObject.Find("UI_House_Number");
		UILabel houseUI = uiHouseNumber.GetComponent("UILabel") as UILabel;
		houseUILabel = houseUI;

		GameObject uiTimeSlider = GameObject.Find("Time_Progress_Bar");
		UISlider timeProgressBarObject = uiTimeSlider.GetComponent ("UISlider") as UISlider;
		timeSlider = timeProgressBarObject;

//		UISlider timeSliderObject = uiTimeSlider.GetComponent("UISlider") as UISlider;
//		timeSlider = timeSliderObject;
	}

    void OnDestroy() {
        iTween.Stop();
    }

	// Update is called once per frame
	void Update () {

		if (UIControl.Instance.getTime() > 0) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				if(checkTouchPosition())
				{
					isSky = true;
					towerBox.towerRigidbody.useGravity = true;
					tower.transform.parent = towerListParent.transform;
				}
			}
		}
	}

	void CreateTower(){
		tower = Instantiate (modelPrefab);
		tower.name = "tower_" + count++;
		tower.tag = "Tower";
		tower.transform.parent = Anchor.transform;
		tower.transform.localPosition = new Vector3 (0f, -1f, 0f);
		tower.transform.localEulerAngles = new Vector3 (0f, 270f, 0f);
		tower.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);

		towerBox = tower.AddComponent<TowerBox> ();
        towerBox.towerBoxID = count;

		towerBox.OnCollision += OnCollision;

        
	}

    private int nowTop = 0;

	void OnCollision(string _tag, GameObject _towerObj, int _towerIndex, int _topIndex)
    {
        Debug.Log("_tag:" + _tag + " _towerIndex:" + _towerIndex + " _topIndex:" + _topIndex + " count:" + count);

		towerBox.OnCollision -= OnCollision;
		CreateTower ();

		if (_tag == "Tower") {
			//正確最頂的房子
			if (_towerIndex == nowTop) {
				towerBoxList.Add (towerBox);
				updateUIDate ();

				nowTop = _topIndex;

				if (towerBoxList.Count >= 1) {
					if (effectObj == null)
						effectObj = Instantiate (effectPrefab);
					else {
						effectObj.SetActive (false);
						effectObj.SetActive (true);
					}
					effectObj.transform.parent = _towerObj.transform;
					effectObj.transform.localPosition = _towerObj.transform.localPosition;

					if (towerBoxList.Count >= 2)
						towerBoxList [towerBoxList.Count - 2].lockshaking = false;

					float dec = towerBoxList [towerBoxList.Count - 1].transform.localPosition.y - 1f;

					iTween.MoveBy (towerListParent, iTween.Hash ("y", dec, "time", 0.5, "easetype", iTween.EaseType.linear));
				}

			} else {
				failCount++;
				Debug.Log ("失敗次數:" + failCount);
			}

		} 
		else if (_tag == "Floor" && towerBoxList.Count == 0) 
		{
			updateUIDate();
		}
	}

	void updateUIDate(){

		UIControl.Instance.setHouseNumber (towerBoxList.Count + 1);		//change house number
		UIControl.Instance.setTimeBarNumber (5);
		timeSlider.value = timeSlider.value++;
	}

	public bool checkTouchPosition(){
		if (Input.mousePosition.x > 0 && Input.mousePosition.x < 1024) 
		{
			if(Input.mousePosition.y > 0 && Input.mousePosition.y < 400)
				return true;
		}
		return false;
	}
}