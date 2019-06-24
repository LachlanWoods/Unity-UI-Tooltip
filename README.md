# Unity-UI-Tooltip
Easy tooltips for UI elements in Unity.

Tooltips will automatically move to ensure they do not go off the screen.

![Alt text](https://github.com/LachlanWoods/Unity-UI-Tooltip/blob/master/Tooltip%20Image.png)

## Usage:

1) Download and add UITooltip.cs to your Unity project.
2) Add a new Text element to your canvas. Style the text as you please. Ensure that the text element has a center middle anchor (the default)
3) Disable the new Text element gameobject
4) Add the UITooltip script to any UI element you want to display a tooltip for.
5) Enter your tooltip text.
6) drag the new Text element to the tooltip variable in the UITooltip script

## Parameters:
**Text tooltip:** The Text element that will act as the tooltip
**string tooltipText:** The message you want to display on the tooltip
**float tooltipOffset:** (default 30) Adds a gap between your mouse and the tooltip (either above or below depending on where the UI element is), so the tooltip will not be directly under the mouse.
**float displayDelay:** (default 0.5f) The delay in seconds between hovering over a UI elements and showing the tooltip.



