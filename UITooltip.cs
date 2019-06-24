using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private Text tooltip;
    [SerializeField]
    private string tooltipText;
    [SerializeField]
    private float tooltipOffset = 30;
    [SerializeField]
    private float displayDelay = 0.5f;

    private Canvas parentCanvas;
    private Coroutine delayRoutine;

    public void Awake() {
        parentCanvas = this.transform.GetComponentInParent<Canvas>(); //find the parent canvas object
        if (parentCanvas == null) {
            Debug.LogError("Cannot find parent canvas");
        }
    }

    public void ShowTooltip() {
        tooltip.text = tooltipText; //set our tooltip text
        delayRoutine = StartCoroutine(DelayTooltip()); //start a coroutine to show tooltip after a delay
    }

    public void HideTooltip() {
        tooltip.gameObject.SetActive(false); //hide the tooltip

        if (delayRoutine != null) { //if the delay coroutine is running, stop it
            StopCoroutine(delayRoutine);
            delayRoutine = null;
        }
    }

    private IEnumerator DelayTooltip() {
        yield return new WaitForSeconds(displayDelay);
        KeepTooltipOnScreen(tooltip.rectTransform); //move our text under our mouse, but keep it on the screen
        tooltip.gameObject.SetActive(true); //show the tooltip after a delay
    }

    //positions the tooltip under the mouse, and ensures that it does not go off the screen
    public void KeepTooltipOnScreen(RectTransform rect) {

        Vector3 mousePos = Input.mousePosition;
        Vector2 tooltipPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, mousePos, parentCanvas.worldCamera, out tooltipPos); //get the mouse position in terms of the canvas
        float tooltipWidth = rect.sizeDelta.x / 2; //half the width of the tooltip

        //move the tooltip horizontally if it will go off the screen
        if (mousePos.x + tooltipWidth > Screen.width){  //off right side of screen
            tooltipPos.x = tooltipPos.x - tooltipWidth;
        }
        else if (mousePos.x - tooltipWidth < 0) { //off left side of screen;
            tooltipPos.x = tooltipPos.x + tooltipWidth;
        }

        //move the tooltip up or down by tooltipOffset, depending on if
        //the tooltip is shown on the top or bottom half od the screen
        if (mousePos.y > (Screen.height / 2)) {  //top of screen
            tooltipPos.y = tooltipPos.y - tooltipOffset;
        }
        else { //bottom of screen;
            tooltipPos.y = tooltipPos.y + tooltipOffset;
        }

        tooltip.transform.position = parentCanvas.transform.TransformPoint(tooltipPos); //update the tooltip position
    }

    public void OnPointerExit(PointerEventData eventData) {
        HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        ShowTooltip();
    }
}
