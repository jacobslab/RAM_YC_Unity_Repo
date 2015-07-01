﻿using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour { //there can be a separate scene controller in each scene


	//SINGLETON
	private static SceneController _instance;
	
	public static SceneController Instance{
		get{
			return _instance;
		}
	}
	
	void Awake(){
		if (_instance != null) {
			Debug.Log("Instance already exists!");
			Destroy(transform.gameObject);
			return;
		}
		_instance = this;
	}


	// Use this for initialization
	void Start () {

	}

	void GetShortcutInput(){
		if(Input.GetKeyDown(KeyCode.Alpha0)){
			LoadMainMenu();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha1)){
			LoadExperiment();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2)){
			LoadEndMenu();
		}
	}

	// Update is called once per frame
	void Update () {
		GetShortcutInput();
	}

	public void LoadMainMenu(){
		if(Experiment.Instance != null){
			Experiment.Instance.OnExit();
		}

		Debug.Log("loading main menu!");
		SubjectReaderWriter.Instance.RecordSubjects();
		Application.LoadLevel(0);
	}

	public void LoadExperiment(){
		//should be no new data to record for the subject
		if(Experiment.Instance != null){
			Experiment.Instance.OnExit();
		}

		if(ExperimentSettings.currentSubject != null){
			Debug.Log("loading experiment!");
			Application.LoadLevel(1);
		}
	}

	public void LoadEndMenu(){
		if(Experiment.Instance != null){
			Experiment.Instance.OnExit();
		}

		SubjectReaderWriter.Instance.RecordSubjects();
		Debug.Log("loading end menu!");
		Application.LoadLevel(2);
	}

	public void Quit(){
		SubjectReaderWriter.Instance.RecordSubjects();
		Application.Quit();
	}

	void OnApplicationQuit(){
		Debug.Log("On Application Quit!");
		SubjectReaderWriter.Instance.RecordSubjects();
	}
}
