using UnityEngine;
 
public static class InputExtensions
{
	//make sure you set these somewhere on first touch
	public static bool IsUsingTouch; 
	/// <summary>
	/// Higher the value, lesser the touch delta
	/// </summary>
	public static float TouchInputDivisor;

	private static Vector3 _previousViewportPos;
	
	/// <summary>
	/// Returns Screen position pixel co-ordinates. Ignorant to where the input is coming from.  
	/// </summary>
	/// <returns></returns>
	public static Vector2 GetInputPosition ()
	{
		if (!GetFingerHeld() && !GetFingerDown()) return Vector2.zero;

		if (IsUsingTouch)
			return Input.GetTouch(0).position;
		
		return Input.mousePosition;
	}
	
	/// <summary>
	/// Returns position in viewport co-ordinates. Ignorant to where the input is coming from.
	/// </summary>
	/// <returns></returns>
	public static Vector2 GetInputViewportPosition()
	{
		if (!GetFingerHeld() && !GetFingerDown()) return Vector2.zero;

		var touchPos = IsUsingTouch ? Input.GetTouch(0).position : new Vector2( Input.mousePosition.x, Input.mousePosition.y);
		
		return new Vector2(touchPos.x / Screen.width, touchPos.y / Screen.height);
	}
	
	/// <summary>
	/// Returns change in Screen position co-ordinates. Ignorant to where the input is coming from.
	/// </summary>
	/// <returns></returns>
	public static Vector2 GetInputDelta ()
	{
		if (!GetFingerHeld()) return Vector2.zero;

		if (IsUsingTouch)
			return Input.GetTouch(0).deltaPosition / TouchInputDivisor;
		
		return new Vector2( Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
	}

	/// <summary>
	/// Returns if input is just pressed this frame.
	/// </summary>
	/// <returns></returns>
	public static bool GetFingerDown ()
	{
		if (!IsUsingTouch) return Input.GetMouseButtonDown(0);
		
		if (Input.touchCount == 0) return false;

		return Input.GetTouch(0).phase == TouchPhase.Began;

	}

	/// <summary>
	/// Returns if input is just released this frame. Ignorant to where the input is coming from.
	/// </summary>
	/// <returns></returns>
	public static bool GetFingerUp ()
	{
		if (!IsUsingTouch) return Input.GetMouseButtonUp(0);
		
		if (Input.touchCount == 0) return false;
			
		return Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled;
	}
	
	/// <summary>
	/// Returns if input has been pressed since previous frame. Ignorant to where the input is coming from.
	/// </summary>
	/// <returns></returns>
	public static bool GetFingerHeld ()
	{
		if (!IsUsingTouch) return Input.GetMouseButton(0);
		
		if (Input.touchCount == 0) return false;
		
		return Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary;
	}

	/// <summary>
	/// Returns the center point in screen space co-ordinates as a Vector3 with z = 0. (Returns the half of the screen resolution)
	/// </summary>
	/// <param name="percentageOnY"> Optionally add a normalised Y axis percentage to get the center point in X axis and custom Y axis point</param>
	/// <returns></returns>
	public static Vector3 GetCenterOfScreen(float percentageOnY = -2f)
	{
		return new Vector3(Screen.width * 0.5f, Screen.height * (percentageOnY < -1f || percentageOnY > 1f ? 0.5f : percentageOnY));
	}

	/// <summary>
	/// Return the current pointer Id, typically for use with the EventSystem as it requires a pointer Id to do many functions. 
	/// </summary>
	/// <returns></returns>
	public static int GetPointerId()
	{
		return IsUsingTouch ? Input.GetTouch(0).fingerId : -1;
	}
}
