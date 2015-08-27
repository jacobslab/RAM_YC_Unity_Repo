using UnityEngine;
using System.Collections;

public class MouseLogTrack : MonoBehaviour, ILoggable {

	Logger_Threading experimentLog { get { return Experiment.Instance.subjectLog; } }

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		if (!ExperimentSettings.isOculus && ExperimentSettings.isLogging) {
			Log ();
		}
	}
	
	// LOGGING SHOULD BE INDEPENDENT OF FRAME RATE
	void FixedUpdate () {

	}

	public void Log(){
		LogMouse ();
	}

	void LogMouse(){
		//log the position
		//TODO: do a check if the mouse position is out of range.
		experimentLog.Log (Experiment.Instance.theGameClock.SystemTime_Milliseconds, experimentLog.GetFrameCount(), "Mouse,POSITION" + "," + Input.mousePosition.x + "," + Input.mousePosition.y);

		//log a clicked object
		if(Input.GetMouseButtonDown(0)){
			Ray ray;
			RaycastHit hit;
			if(Camera.main != null){
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray, out hit)){
					experimentLog.Log(Experiment.Instance.theGameClock.SystemTime_Milliseconds, experimentLog.GetFrameCount(), "Mouse,CLICKED" + "," + hit.collider.gameObject);
				}
				else{
					experimentLog.Log(Experiment.Instance.theGameClock.SystemTime_Milliseconds, experimentLog.GetFrameCount(), "Mouse,CLICKED" + "," + "EMPTY");
				}
			}
			else{
				Debug.Log("Camera.main is null! Can't raycast mouse position.");
			}
		}
	}
}
