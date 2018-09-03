using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using SLua;

[CustomLuaClass]
public class MyJoystickEvent : UnityEvent{}//仅通知,不传值
[CustomLuaClass]
public class MyJoystickPointerEvent : UnityEvent<PointerEventData>{}

[CustomLuaClass]
public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler{
	// 摇杆的回调频率，数值越小效果越好
	public float updateTime = 0.1f;
	// 是否锁定位置
	public bool moveLock = false;
	
	
	public float mRadius = 0f;
	protected Vector3 startPos;
	private Vector2 selfSize = Vector2.zero;
	private Vector2 areaSize = Vector2.zero;//移动范围(x,y区间)
	public RectTransform content;
	protected bool draging;
	private bool fireing;//用于控制 onDrag 事件的频率
	
	// 定义事件  用于接取c#事件，并在lua中接入相关回调
	public MyJoystickEvent onBeginDrag = new MyJoystickEvent();
	public MyJoystickEvent onDrag = new MyJoystickEvent();
	public MyJoystickEvent onEndDrag = new MyJoystickEvent();
	public MyJoystickEvent onClick = new MyJoystickEvent();
	public MyJoystickPointerEvent onDown = new MyJoystickPointerEvent();
	public MyJoystickPointerEvent onUp = new MyJoystickPointerEvent();
	
	public void Start(){
		//计算摇杆块的半径
		selfSize = (transform as RectTransform).sizeDelta;
		areaSize = selfSize;
		mRadius = selfSize.x * 0.5f;
		startPos = transform.localPosition;
		draging = false;
		fireing = false;
	}

	public void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
	{
		draging = true;
	}
	
	public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData){
		draging = true;
		// base.OnDrag(eventData);
		// var contentPosition = content.anchoredPosition;
		// if(contentPosition.magnitude - mRadius > 0.2){
		// 	if(!moveLock){
		// 		Vector3 targetpos = transform.localPosition+(Vector3)contentPosition.normalized*(contentPostion.magnitude - mRadius);
		// 		float tx = Mathf.Clamp (targetpos.x, selfsize.x/2, (parentsize.x-selfsize.x/2));
		// 		float ty = Mathf.Clamp (targetpos.y, selfsize.x/2, (parentsize.y-selfsize.y/2));
		// 		transform.localPosition = new Vector3 (tx, ty, 0);
		// 	}
		// 	contentPosition = contentPosition.normalized*mRadius;
		// 	SetContentAnchoredPosition(contentPosition);
		// }
		// if(draging && !fireing)
		// 	Invoke("FireDrag", 0);
	}

	public void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData){
		if(draging){
			// base.OnEndDrag(eventData);
			transform.localPosition = startPos;
			draging = false;
			fireing = false;
			// SetContentAnchoredPosition(Vector2.zero);
			onEndDrag.Invoke();
		}
	}
	
	public void FireDrag(){
		if(draging){
			onDrag.Invoke();
			Invoke("FireDrag", updateTime);
			fireing = true;
		}else{
			fireing = false;
		}
	}
	

	public void OnPointerClick(PointerEventData eventData)
	{
		onClick.Invoke();
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if(eventData.button != PointerEventData.InputButton.Left)
			return;
		onDown.Invoke(eventData);
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		onUp.Invoke(eventData);
	}
}