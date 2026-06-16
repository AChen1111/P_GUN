---@meta
---@diagnostic disable

---@param handler UnityEngine.UIElements.IEventHandler
---@param pointerId number
---@return boolean
function UnityEngine.UIElements.PointerCaptureHelper.HasPointerCapture(handler, pointerId) end
---@param handler UnityEngine.UIElements.IEventHandler
---@param pointerId number
function UnityEngine.UIElements.PointerCaptureHelper.CapturePointer(handler, pointerId) end
---@overload fun(handler: UnityEngine.UIElements.IEventHandler, pointerId: number)
---@param panel UnityEngine.UIElements.IPanel
---@param pointerId number
function UnityEngine.UIElements.PointerCaptureHelper.ReleasePointer(panel, pointerId) end
---@param panel UnityEngine.UIElements.IPanel
---@param pointerId number
---@return UnityEngine.UIElements.IEventHandler
function UnityEngine.UIElements.PointerCaptureHelper.GetCapturingElement(panel, pointerId) end

---@class UnityEngine.UIElements.PointerCaptureOutEvent : UnityEngine.UIElements.PointerCaptureEventBase
UnityEngine.UIElements.PointerCaptureOutEvent = {}
---@alias CS.UnityEngine.UIElements.PointerCaptureOutEvent UnityEngine.UIElements.PointerCaptureOutEvent
CS.UnityEngine.UIElements.PointerCaptureOutEvent = UnityEngine.UIElements.PointerCaptureOutEvent

---@return UnityEngine.UIElements.PointerCaptureOutEvent
function UnityEngine.UIElements.PointerCaptureOutEvent.New() end

---@class UnityEngine.UIElements.PointerDeviceState : System.Object
UnityEngine.UIElements.PointerDeviceState = {}
---@alias CS.UnityEngine.UIElements.PointerDeviceState UnityEngine.UIElements.PointerDeviceState
CS.UnityEngine.UIElements.PointerDeviceState = UnityEngine.UIElements.PointerDeviceState

---@param pointerId number
---@param position UnityEngine.Vector2
---@param panel UnityEngine.UIElements.IPanel
---@param contextType UnityEngine.UIElements.ContextType
function UnityEngine.UIElements.PointerDeviceState.SavePointerPosition(pointerId, position, panel, contextType) end
---@param pointerId number
---@param buttonId number
function UnityEngine.UIElements.PointerDeviceState.PressButton(pointerId, buttonId) end
---@param pointerId number
---@param buttonId number
function UnityEngine.UIElements.PointerDeviceState.ReleaseButton(pointerId, buttonId) end
---@param pointerId number
function UnityEngine.UIElements.PointerDeviceState.ReleaseAllButtons(pointerId) end
---@param pointerId number
---@param contextType UnityEngine.UIElements.ContextType
---@return UnityEngine.Vector2
function UnityEngine.UIElements.PointerDeviceState.GetPointerPosition(pointerId, contextType) end
---@param pointerId number
---@param contextType UnityEngine.UIElements.ContextType
---@return UnityEngine.UIElements.IPanel
function UnityEngine.UIElements.PointerDeviceState.GetPanel(pointerId, contextType) end
---@param pointerId number
---@param contextType UnityEngine.UIElements.ContextType
---@param flag UnityEngine.UIElements.PointerDeviceState.LocationFlag
---@return boolean
function UnityEngine.UIElements.PointerDeviceState.HasLocationFlag(pointerId, contextType, flag) end
---@param pointerId number
---@return number
function UnityEngine.UIElements.PointerDeviceState.GetPressedButtons(pointerId) end

---@class UnityEngine.UIElements.PointerDeviceState.LocationFlag
---@field None UnityEngine.UIElements.PointerDeviceState.LocationFlag
---@field OutsidePanel UnityEngine.UIElements.PointerDeviceState.LocationFlag
UnityEngine.UIElements.PointerDeviceState.LocationFlag = {}
---@alias CS.UnityEngine.UIElements.PointerDeviceState.LocationFlag UnityEngine.UIElements.PointerDeviceState.LocationFlag
CS.UnityEngine.UIElements.PointerDeviceState.LocationFlag = UnityEngine.UIElements.PointerDeviceState.LocationFlag


---@class UnityEngine.UIElements.PointerDeviceState.PointerLocation : System.ValueType
UnityEngine.UIElements.PointerDeviceState.PointerLocation = {}
---@alias CS.UnityEngine.UIElements.PointerDeviceState.PointerLocation UnityEngine.UIElements.PointerDeviceState.PointerLocation
CS.UnityEngine.UIElements.PointerDeviceState.PointerLocation = UnityEngine.UIElements.PointerDeviceState.PointerLocation


---@class UnityEngine.UIElements.PointerDispatchState : System.Object
UnityEngine.UIElements.PointerDispatchState = {}
---@alias CS.UnityEngine.UIElements.PointerDispatchState UnityEngine.UIElements.PointerDispatchState
CS.UnityEngine.UIElements.PointerDispatchState = UnityEngine.UIElements.PointerDispatchState

---@return UnityEngine.UIElements.PointerDispatchState
function UnityEngine.UIElements.PointerDispatchState.New() end
---@param pointerId number
---@return UnityEngine.UIElements.IEventHandler
function UnityEngine.UIElements.PointerDispatchState:GetCapturingElement(pointerId) end
---@param handler UnityEngine.UIElements.IEventHandler
---@param pointerId number
---@return boolean
function UnityEngine.UIElements.PointerDispatchState:HasPointerCapture(handler, pointerId) end
---@param handler UnityEngine.UIElements.IEventHandler
---@param pointerId number
function UnityEngine.UIElements.PointerDispatchState:CapturePointer(handler, pointerId) end
---@overload fun(self: UnityEngine.UIElements.PointerDispatchState, pointerId: number)
---@param handler UnityEngine.UIElements.IEventHandler
---@param pointerId number
function UnityEngine.UIElements.PointerDispatchState:ReleasePointer(handler, pointerId) end
---@param pointerId number
function UnityEngine.UIElements.PointerDispatchState:ProcessPointerCapture(pointerId) end
---@param pointerId number
function UnityEngine.UIElements.PointerDispatchState:ActivateCompatibilityMouseEvents(pointerId) end
---@param pointerId number
function UnityEngine.UIElements.PointerDispatchState:PreventCompatibilityMouseEvents(pointerId) end
---@param evt UnityEngine.UIElements.IPointerEvent
---@return boolean
function UnityEngine.UIElements.PointerDispatchState:ShouldSendCompatibilityMouseEvents(evt) end

---@class UnityEngine.UIElements.PointerDownEvent : UnityEngine.UIElements.PointerEventBase
UnityEngine.UIElements.PointerDownEvent = {}
---@alias CS.UnityEngine.UIElements.PointerDownEvent UnityEngine.UIElements.PointerDownEvent
CS.UnityEngine.UIElements.PointerDownEvent = UnityEngine.UIElements.PointerDownEvent

---@return UnityEngine.UIElements.PointerDownEvent
function UnityEngine.UIElements.PointerDownEvent.New() end

---@class UnityEngine.UIElements.PointerEnterEvent : UnityEngine.UIElements.PointerEventBase
UnityEngine.UIElements.PointerEnterEvent = {}
---@alias CS.UnityEngine.UIElements.PointerEnterEvent UnityEngine.UIElements.PointerEnterEvent
CS.UnityEngine.UIElements.PointerEnterEvent = UnityEngine.UIElements.PointerEnterEvent

---@return UnityEngine.UIElements.PointerEnterEvent
function UnityEngine.UIElements.PointerEnterEvent.New() end

---@class UnityEngine.UIElements.PointerEventBase : UnityEngine.UIElements.EventBase[T]
---@field pointerId number
---@field pointerType string
---@field isPrimary boolean
---@field button number
---@field pressedButtons number
---@field position UnityEngine.Vector3
---@field localPosition UnityEngine.Vector3
---@field deltaPosition UnityEngine.Vector3
---@field deltaTime number
---@field clickCount number
---@field pressure number
---@field tangentialPressure number
---@field altitudeAngle number
---@field azimuthAngle number
---@field twist number
---@field tilt UnityEngine.Vector2
---@field penStatus UnityEngine.PenStatus
---@field radius UnityEngine.Vector2
---@field radiusVariance UnityEngine.Vector2
---@field modifiers UnityEngine.EventModifiers
---@field shiftKey boolean
---@field ctrlKey boolean
---@field commandKey boolean
---@field altKey boolean
---@field actionKey boolean
---@field currentTarget UnityEngine.UIElements.IEventHandler
UnityEngine.UIElements.PointerEventBase = {}
---@alias CS.UnityEngine.UIElements.PointerEventBase UnityEngine.UIElements.PointerEventBase
CS.UnityEngine.UIElements.PointerEventBase = UnityEngine.UIElements.PointerEventBase

---@overload fun(systemEvent: UnityEngine.Event) : T
---@overload fun(touch: UnityEngine.Touch, modifiers: UnityEngine.EventModifiers) : T
---@overload fun(pen: UnityEngine.PenData, modifiers: UnityEngine.EventModifiers) : T
---@param triggerEvent UnityEngine.UIElements.IPointerEvent
---@return T
function UnityEngine.UIElements.PointerEventBase.GetPooled(triggerEvent) end

---@class UnityEngine.UIElements.PointerEventDispatchingStrategy : System.Object
UnityEngine.UIElements.PointerEventDispatchingStrategy = {}
---@alias CS.UnityEngine.UIElements.PointerEventDispatchingStrategy UnityEngine.UIElements.PointerEventDispatchingStrategy
CS.UnityEngine.UIElements.PointerEventDispatchingStrategy = UnityEngine.UIElements.PointerEventDispatchingStrategy

---@return UnityEngine.UIElements.PointerEventDispatchingStrategy
function UnityEngine.UIElements.PointerEventDispatchingStrategy.New() end
---@param evt UnityEngine.UIElements.EventBase
---@return boolean
function UnityEngine.UIElements.PointerEventDispatchingStrategy:CanDispatchEvent(evt) end
---@param evt UnityEngine.UIElements.EventBase
---@param panel UnityEngine.UIElements.IPanel
function UnityEngine.UIElements.PointerEventDispatchingStrategy:DispatchEvent(evt, panel) end

---@class UnityEngine.UIElements.PointerEventHelper : System.Object
UnityEngine.UIElements.PointerEventHelper = {}
---@alias CS.UnityEngine.UIElements.PointerEventHelper UnityEngine.UIElements.PointerEventHelper
CS.UnityEngine.UIElements.PointerEventHelper = UnityEngine.UIElements.PointerEventHelper

---@param eventType UnityEngine.EventType
---@param mousePosition UnityEngine.Vector3
---@param delta UnityEngine.Vector2
---@param button number
---@param clickCount number
---@param modifiers UnityEngine.EventModifiers
---@return UnityEngine.UIElements.EventBase
function UnityEngine.UIElements.PointerEventHelper.GetPooled(eventType, mousePosition, delta, button, clickCount, modifiers) end

---@class UnityEngine.UIElements.PointerEventsHelper : System.Object
UnityEngine.UIElements.PointerEventsHelper = {}
---@alias CS.UnityEngine.UIElements.PointerEventsHelper UnityEngine.UIElements.PointerEventsHelper
CS.UnityEngine.UIElements.PointerEventsHelper = UnityEngine.UIElements.PointerEventsHelper


---@class UnityEngine.UIElements.PointerId : System.Object
---@field maxPointers number
---@field invalidPointerId number
---@field mousePointerId number
---@field touchPointerIdBase number
---@field touchPointerCount number
---@field penPointerIdBase number
---@field penPointerCount number
UnityEngine.UIElements.PointerId = {}
---@alias CS.UnityEngine.UIElements.PointerId UnityEngine.UIElements.PointerId
CS.UnityEngine.UIElements.PointerId = UnityEngine.UIElements.PointerId


---@class UnityEngine.UIElements.PointerLeaveEvent : UnityEngine.UIElements.PointerEventBase
UnityEngine.UIElements.PointerLeaveEvent = {}
---@alias CS.UnityEngine.UIElements.PointerLeaveEvent UnityEngine.UIElements.PointerLeaveEvent
CS.UnityEngine.UIElements.PointerLeaveEvent = UnityEngine.UIElements.PointerLeaveEvent

---@return UnityEngine.UIElements.PointerLeaveEvent
function UnityEngine.UIElements.PointerLeaveEvent.New() end

---@class UnityEngine.UIElements.PointerManipulator : UnityEngine.UIElements.MouseManipulator
UnityEngine.UIElements.PointerManipulator = {}
---@alias CS.UnityEngine.UIElements.PointerManipulator UnityEngine.UIElements.PointerManipulator
CS.UnityEngine.UIElements.PointerManipulator = UnityEngine.UIElements.PointerManipulator


---@class UnityEngine.UIElements.PointerMoveEvent : UnityEngine.UIElements.PointerEventBase
UnityEngine.UIElements.PointerMoveEvent = {}
---@alias CS.UnityEngine.UIElements.PointerMoveEvent UnityEngine.UIElements.PointerMoveEvent
CS.UnityEngine.UIElements.PointerMoveEvent = UnityEngine.UIElements.PointerMoveEvent

---@return UnityEngine.UIElements.PointerMoveEvent
function UnityEngine.UIElements.PointerMoveEvent.New() end

---@class UnityEngine.UIElements.PointerOutEvent : UnityEngine.UIElements.PointerEventBase
UnityEngine.UIElements.PointerOutEvent = {}
---@alias CS.UnityEngine.UIElements.PointerOutEvent UnityEngine.UIElements.PointerOutEvent
CS.UnityEngine.UIElements.PointerOutEvent = UnityEngine.UIElements.PointerOutEvent

---@return UnityEngine.UIElements.PointerOutEvent
function UnityEngine.UIElements.PointerOutEvent.New() end

---@class UnityEngine.UIElements.PointerOverEvent : UnityEngine.UIElements.PointerEventBase
UnityEngine.UIElements.PointerOverEvent = {}
---@alias CS.UnityEngine.UIElements.PointerOverEvent UnityEngine.UIElements.PointerOverEvent
CS.UnityEngine.UIElements.PointerOverEvent = UnityEngine.UIElements.PointerOverEvent

---@return UnityEngine.UIElements.PointerOverEvent
function UnityEngine.UIElements.PointerOverEvent.New() end

---@class UnityEngine.UIElements.PointerStationaryEvent : UnityEngine.UIElements.PointerEventBase
UnityEngine.UIElements.PointerStationaryEvent = {}
---@alias CS.UnityEngine.UIElements.PointerStationaryEvent UnityEngine.UIElements.PointerStationaryEvent
CS.UnityEngine.UIElements.PointerStationaryEvent = UnityEngine.UIElements.PointerStationaryEvent

---@return UnityEngine.UIElements.PointerStationaryEvent
function UnityEngine.UIElements.PointerStationaryEvent.New() end

---@class UnityEngine.UIElements.PointerType : System.Object
---@field mouse string
---@field touch string
---@field pen string
---@field unknown string
UnityEngine.UIElements.PointerType = {}
---@alias CS.UnityEngine.UIElements.PointerType UnityEngine.UIElements.PointerType
CS.UnityEngine.UIElements.PointerType = UnityEngine.UIElements.PointerType


---@class UnityEngine.UIElements.PointerUpEvent : UnityEngine.UIElements.PointerEventBase
UnityEngine.UIElements.PointerUpEvent = {}
---@alias CS.UnityEngine.UIElements.PointerUpEvent UnityEngine.UIElements.PointerUpEvent
CS.UnityEngine.UIElements.PointerUpEvent = UnityEngine.UIElements.PointerUpEvent

---@return UnityEngine.UIElements.PointerUpEvent
function UnityEngine.UIElements.PointerUpEvent.New() end

---@class UnityEngine.UIElements.PopupField : UnityEngine.UIElements.BasePopupField[T,T]
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
---@field formatSelectedValueCallback System.Func[T,System.String]
---@field formatListItemCallback System.Func[T,System.String]
---@field value T
---@field index number
UnityEngine.UIElements.PopupField = {}
---@alias CS.UnityEngine.UIElements.PopupField UnityEngine.UIElements.PopupField
CS.UnityEngine.UIElements.PopupField = UnityEngine.UIElements.PopupField

---@overload fun() : UnityEngine.UIElements.PopupField
---@overload fun(label: string) : UnityEngine.UIElements.PopupField
---@overload fun(choices: System.Collections.Generic.List[T], defaultValue: T, formatSelectedValueCallback: System.Func[T,System.String], formatListItemCallback: System.Func[T,System.String]) : UnityEngine.UIElements.PopupField
---@overload fun(label: string, choices: System.Collections.Generic.List[T], defaultValue: T, formatSelectedValueCallback: System.Func[T,System.String], formatListItemCallback: System.Func[T,System.String]) : UnityEngine.UIElements.PopupField
---@overload fun(choices: System.Collections.Generic.List[T], defaultIndex: number, formatSelectedValueCallback: System.Func[T,System.String], formatListItemCallback: System.Func[T,System.String]) : UnityEngine.UIElements.PopupField
---@param label string
---@param choices System.Collections.Generic.List[T]
---@param defaultIndex number
---@param formatSelectedValueCallback System.Func[T,System.String]
---@param formatListItemCallback System.Func[T,System.String]
---@return UnityEngine.UIElements.PopupField
function UnityEngine.UIElements.PopupField.New(label, choices, defaultIndex, formatSelectedValueCallback, formatListItemCallback) end
---@param newValue T
function UnityEngine.UIElements.PopupField:SetValueWithoutNotify(newValue) end

---@class UnityEngine.UIElements.PopupWindow : UnityEngine.UIElements.TextElement
---@field ussClassName string
---@field contentUssClassName string
---@field contentContainer UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.PopupWindow = {}
---@alias CS.UnityEngine.UIElements.PopupWindow UnityEngine.UIElements.PopupWindow
CS.UnityEngine.UIElements.PopupWindow = UnityEngine.UIElements.PopupWindow

---@return UnityEngine.UIElements.PopupWindow
function UnityEngine.UIElements.PopupWindow.New() end

---@class UnityEngine.UIElements.PopupWindow.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.PopupWindow.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.PopupWindow.UxmlFactory UnityEngine.UIElements.PopupWindow.UxmlFactory
CS.UnityEngine.UIElements.PopupWindow.UxmlFactory = UnityEngine.UIElements.PopupWindow.UxmlFactory

---@return UnityEngine.UIElements.PopupWindow.UxmlFactory
function UnityEngine.UIElements.PopupWindow.UxmlFactory.New() end

---@class UnityEngine.UIElements.PopupWindow.UxmlTraits : UnityEngine.UIElements.TextElement.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.PopupWindow.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.PopupWindow.UxmlTraits UnityEngine.UIElements.PopupWindow.UxmlTraits
CS.UnityEngine.UIElements.PopupWindow.UxmlTraits = UnityEngine.UIElements.PopupWindow.UxmlTraits

---@return UnityEngine.UIElements.PopupWindow.UxmlTraits
function UnityEngine.UIElements.PopupWindow.UxmlTraits.New() end

---@class UnityEngine.UIElements.Position
---@field Relative UnityEngine.UIElements.Position
---@field Absolute UnityEngine.UIElements.Position
UnityEngine.UIElements.Position = {}
---@alias CS.UnityEngine.UIElements.Position UnityEngine.UIElements.Position
CS.UnityEngine.UIElements.Position = UnityEngine.UIElements.Position


---@class UnityEngine.UIElements.ProgressBar : UnityEngine.UIElements.AbstractProgressBar
UnityEngine.UIElements.ProgressBar = {}
---@alias CS.UnityEngine.UIElements.ProgressBar UnityEngine.UIElements.ProgressBar
CS.UnityEngine.UIElements.ProgressBar = UnityEngine.UIElements.ProgressBar

---@return UnityEngine.UIElements.ProgressBar
function UnityEngine.UIElements.ProgressBar.New() end

---@class UnityEngine.UIElements.ProgressBar.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.ProgressBar.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.ProgressBar.UxmlFactory UnityEngine.UIElements.ProgressBar.UxmlFactory
CS.UnityEngine.UIElements.ProgressBar.UxmlFactory = UnityEngine.UIElements.ProgressBar.UxmlFactory

---@return UnityEngine.UIElements.ProgressBar.UxmlFactory
function UnityEngine.UIElements.ProgressBar.UxmlFactory.New() end

---@class UnityEngine.UIElements.ProjectionUtils : System.Object
UnityEngine.UIElements.ProjectionUtils = {}
---@alias CS.UnityEngine.UIElements.ProjectionUtils UnityEngine.UIElements.ProjectionUtils
CS.UnityEngine.UIElements.ProjectionUtils = UnityEngine.UIElements.ProjectionUtils

---@param left number
---@param right number
---@param bottom number
---@param top number
---@param near number
---@param far number
---@return UnityEngine.Matrix4x4
function UnityEngine.UIElements.ProjectionUtils.Ortho(left, right, bottom, top, near, far) end

---@class UnityEngine.UIElements.PropagationPaths : System.Object
---@field trickleDownPath System.Collections.Generic.List
---@field targetElements System.Collections.Generic.List
---@field bubbleUpPath System.Collections.Generic.List
UnityEngine.UIElements.PropagationPaths = {}
---@alias CS.UnityEngine.UIElements.PropagationPaths UnityEngine.UIElements.PropagationPaths
CS.UnityEngine.UIElements.PropagationPaths = UnityEngine.UIElements.PropagationPaths

---@overload fun() : UnityEngine.UIElements.PropagationPaths
---@param paths UnityEngine.UIElements.PropagationPaths
---@return UnityEngine.UIElements.PropagationPaths
function UnityEngine.UIElements.PropagationPaths.New(paths) end
---@param elem UnityEngine.UIElements.VisualElement
---@param evt UnityEngine.UIElements.EventBase
---@return UnityEngine.UIElements.PropagationPaths
function UnityEngine.UIElements.PropagationPaths.Build(elem, evt) end
function UnityEngine.UIElements.PropagationPaths:Release() end

---@class UnityEngine.UIElements.PropagationPaths.Type
---@field None UnityEngine.UIElements.PropagationPaths.Type
---@field TrickleDown UnityEngine.UIElements.PropagationPaths.Type
---@field BubbleUp UnityEngine.UIElements.PropagationPaths.Type
UnityEngine.UIElements.PropagationPaths.Type = {}
---@alias CS.UnityEngine.UIElements.PropagationPaths.Type UnityEngine.UIElements.PropagationPaths.Type
CS.UnityEngine.UIElements.PropagationPaths.Type = UnityEngine.UIElements.PropagationPaths.Type


---@class UnityEngine.UIElements.PropagationPhase
---@field None UnityEngine.UIElements.PropagationPhase
---@field TrickleDown UnityEngine.UIElements.PropagationPhase
---@field AtTarget UnityEngine.UIElements.PropagationPhase
---@field DefaultActionAtTarget UnityEngine.UIElements.PropagationPhase
---@field BubbleUp UnityEngine.UIElements.PropagationPhase
---@field DefaultAction UnityEngine.UIElements.PropagationPhase
UnityEngine.UIElements.PropagationPhase = {}
---@alias CS.UnityEngine.UIElements.PropagationPhase UnityEngine.UIElements.PropagationPhase
CS.UnityEngine.UIElements.PropagationPhase = UnityEngine.UIElements.PropagationPhase


---@class UnityEngine.UIElements.PseudoStates
---@field Active UnityEngine.UIElements.PseudoStates
---@field Hover UnityEngine.UIElements.PseudoStates
---@field Checked UnityEngine.UIElements.PseudoStates
---@field Disabled UnityEngine.UIElements.PseudoStates
---@field Focus UnityEngine.UIElements.PseudoStates
---@field Root UnityEngine.UIElements.PseudoStates
UnityEngine.UIElements.PseudoStates = {}
---@alias CS.UnityEngine.UIElements.PseudoStates UnityEngine.UIElements.PseudoStates
CS.UnityEngine.UIElements.PseudoStates = UnityEngine.UIElements.PseudoStates


---@class UnityEngine.UIElements.RadioButton : UnityEngine.UIElements.BaseBoolField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
---@field checkmarkBackgroundUssClassName string
---@field checkmarkUssClassName string
---@field textUssClassName string
---@field value boolean
UnityEngine.UIElements.RadioButton = {}
---@alias CS.UnityEngine.UIElements.RadioButton UnityEngine.UIElements.RadioButton
CS.UnityEngine.UIElements.RadioButton = UnityEngine.UIElements.RadioButton

---@overload fun() : UnityEngine.UIElements.RadioButton
---@param label string
---@return UnityEngine.UIElements.RadioButton
function UnityEngine.UIElements.RadioButton.New(label) end
---@param newValue boolean
function UnityEngine.UIElements.RadioButton:SetValueWithoutNotify(newValue) end

---@class UnityEngine.UIElements.RadioButton.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.RadioButton.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.RadioButton.UxmlFactory UnityEngine.UIElements.RadioButton.UxmlFactory
CS.UnityEngine.UIElements.RadioButton.UxmlFactory = UnityEngine.UIElements.RadioButton.UxmlFactory

---@return UnityEngine.UIElements.RadioButton.UxmlFactory
function UnityEngine.UIElements.RadioButton.UxmlFactory.New() end

---@class UnityEngine.UIElements.RadioButton.UxmlTraits : UnityEngine.UIElements.BaseFieldTraits
UnityEngine.UIElements.RadioButton.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.RadioButton.UxmlTraits UnityEngine.UIElements.RadioButton.UxmlTraits
CS.UnityEngine.UIElements.RadioButton.UxmlTraits = UnityEngine.UIElements.RadioButton.UxmlTraits

---@return UnityEngine.UIElements.RadioButton.UxmlTraits
function UnityEngine.UIElements.RadioButton.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.RadioButton.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.RadioButtonGroup : UnityEngine.UIElements.BaseField
---@field ussClassName string
---@field containerUssClassName string
---@field choices System.Collections.Generic.IEnumerable
---@field contentContainer UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.RadioButtonGroup = {}
---@alias CS.UnityEngine.UIElements.RadioButtonGroup UnityEngine.UIElements.RadioButtonGroup
CS.UnityEngine.UIElements.RadioButtonGroup = UnityEngine.UIElements.RadioButtonGroup

---@overload fun() : UnityEngine.UIElements.RadioButtonGroup
---@param label string
---@param radioButtonChoices System.Collections.Generic.List
---@return UnityEngine.UIElements.RadioButtonGroup
function UnityEngine.UIElements.RadioButtonGroup.New(label, radioButtonChoices) end
---@param newValue number
function UnityEngine.UIElements.RadioButtonGroup:SetValueWithoutNotify(newValue) end

---@class UnityEngine.UIElements.RadioButtonGroup.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.RadioButtonGroup.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.RadioButtonGroup.UxmlFactory UnityEngine.UIElements.RadioButtonGroup.UxmlFactory
CS.UnityEngine.UIElements.RadioButtonGroup.UxmlFactory = UnityEngine.UIElements.RadioButtonGroup.UxmlFactory

---@return UnityEngine.UIElements.RadioButtonGroup.UxmlFactory
function UnityEngine.UIElements.RadioButtonGroup.UxmlFactory.New() end

---@class UnityEngine.UIElements.RadioButtonGroup.UxmlTraits : UnityEngine.UIElements.BaseFieldTraits
UnityEngine.UIElements.RadioButtonGroup.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.RadioButtonGroup.UxmlTraits UnityEngine.UIElements.RadioButtonGroup.UxmlTraits
CS.UnityEngine.UIElements.RadioButtonGroup.UxmlTraits = UnityEngine.UIElements.RadioButtonGroup.UxmlTraits

---@return UnityEngine.UIElements.RadioButtonGroup.UxmlTraits
function UnityEngine.UIElements.RadioButtonGroup.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.RadioButtonGroup.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.RareData : System.ValueType
---@field cursor UnityEngine.UIElements.Cursor
---@field textOverflow UnityEngine.UIElements.TextOverflow
---@field unityBackgroundImageTintColor UnityEngine.Color
---@field unityOverflowClipBox UnityEngine.UIElements.OverflowClipBox
---@field unitySliceBottom number
---@field unitySliceLeft number
---@field unitySliceRight number
---@field unitySliceScale number
---@field unitySliceTop number
---@field unityTextOverflowPosition UnityEngine.UIElements.TextOverflowPosition
UnityEngine.UIElements.RareData = {}
---@alias CS.UnityEngine.UIElements.RareData UnityEngine.UIElements.RareData
CS.UnityEngine.UIElements.RareData = UnityEngine.UIElements.RareData

---@return UnityEngine.UIElements.RareData
function UnityEngine.UIElements.RareData:Copy() end
---@param ref_other UnityEngine.UIElements.RareData
---@return UnityEngine.UIElements.RareData
function UnityEngine.UIElements.RareData:CopyFrom(ref_other) end
---@overload fun(self: UnityEngine.UIElements.RareData, other: UnityEngine.UIElements.RareData) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.RareData:Equals(obj) end
---@return number
function UnityEngine.UIElements.RareData:GetHashCode() end

---@class UnityEngine.UIElements.RectField : UnityEngine.UIElements.BaseCompositeField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.RectField = {}
---@alias CS.UnityEngine.UIElements.RectField UnityEngine.UIElements.RectField
CS.UnityEngine.UIElements.RectField = UnityEngine.UIElements.RectField

---@overload fun() : UnityEngine.UIElements.RectField
---@param label string
---@return UnityEngine.UIElements.RectField
function UnityEngine.UIElements.RectField.New(label) end

---@class UnityEngine.UIElements.RectField.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.RectField.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.RectField.UxmlFactory UnityEngine.UIElements.RectField.UxmlFactory
CS.UnityEngine.UIElements.RectField.UxmlFactory = UnityEngine.UIElements.RectField.UxmlFactory

---@return UnityEngine.UIElements.RectField.UxmlFactory
function UnityEngine.UIElements.RectField.UxmlFactory.New() end

---@class UnityEngine.UIElements.RectField.UxmlTraits : UnityEngine.UIElements.BaseField.UxmlTraits
UnityEngine.UIElements.RectField.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.RectField.UxmlTraits UnityEngine.UIElements.RectField.UxmlTraits
CS.UnityEngine.UIElements.RectField.UxmlTraits = UnityEngine.UIElements.RectField.UxmlTraits

---@return UnityEngine.UIElements.RectField.UxmlTraits
function UnityEngine.UIElements.RectField.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.RectField.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.RectIntField : UnityEngine.UIElements.BaseCompositeField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.RectIntField = {}
---@alias CS.UnityEngine.UIElements.RectIntField UnityEngine.UIElements.RectIntField
CS.UnityEngine.UIElements.RectIntField = UnityEngine.UIElements.RectIntField

---@overload fun() : UnityEngine.UIElements.RectIntField
---@param label string
---@return UnityEngine.UIElements.RectIntField
function UnityEngine.UIElements.RectIntField.New(label) end

---@class UnityEngine.UIElements.RectIntField.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.RectIntField.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.RectIntField.UxmlFactory UnityEngine.UIElements.RectIntField.UxmlFactory
CS.UnityEngine.UIElements.RectIntField.UxmlFactory = UnityEngine.UIElements.RectIntField.UxmlFactory

---@return UnityEngine.UIElements.RectIntField.UxmlFactory
function UnityEngine.UIElements.RectIntField.UxmlFactory.New() end

---@class UnityEngine.UIElements.RectIntField.UxmlTraits : UnityEngine.UIElements.BaseField.UxmlTraits
UnityEngine.UIElements.RectIntField.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.RectIntField.UxmlTraits UnityEngine.UIElements.RectIntField.UxmlTraits
CS.UnityEngine.UIElements.RectIntField.UxmlTraits = UnityEngine.UIElements.RectIntField.UxmlTraits

---@return UnityEngine.UIElements.RectIntField.UxmlTraits
function UnityEngine.UIElements.RectIntField.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.RectIntField.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.RegisterSerializedPropertyBindCallback : System.MulticastDelegate
UnityEngine.UIElements.RegisterSerializedPropertyBindCallback = {}
---@alias CS.UnityEngine.UIElements.RegisterSerializedPropertyBindCallback UnityEngine.UIElements.RegisterSerializedPropertyBindCallback
CS.UnityEngine.UIElements.RegisterSerializedPropertyBindCallback = UnityEngine.UIElements.RegisterSerializedPropertyBindCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.UIElements.RegisterSerializedPropertyBindCallback
function UnityEngine.UIElements.RegisterSerializedPropertyBindCallback.New(object, method) end
---@param compositeField UnityEngine.UIElements.BaseCompositeField[TValueType,TField,TFieldValue]
---@param field TField
function UnityEngine.UIElements.RegisterSerializedPropertyBindCallback:Invoke(compositeField, field) end
---@param compositeField UnityEngine.UIElements.BaseCompositeField[TValueType,TField,TFieldValue]
---@param field TField
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.UIElements.RegisterSerializedPropertyBindCallback:BeginInvoke(compositeField, field, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.UIElements.RegisterSerializedPropertyBindCallback:EndInvoke(result) end

---@class UnityEngine.UIElements.RenderHints
---@field None UnityEngine.UIElements.RenderHints
---@field GroupTransform UnityEngine.UIElements.RenderHints
---@field BoneTransform UnityEngine.UIElements.RenderHints
---@field ClipWithScissors UnityEngine.UIElements.RenderHints
---@field MaskContainer UnityEngine.UIElements.RenderHints
---@field DynamicColor UnityEngine.UIElements.RenderHints
---@field DirtyOffset UnityEngine.UIElements.RenderHints
---@field DirtyGroupTransform UnityEngine.UIElements.RenderHints
---@field DirtyBoneTransform UnityEngine.UIElements.RenderHints
---@field DirtyClipWithScissors UnityEngine.UIElements.RenderHints
---@field DirtyMaskContainer UnityEngine.UIElements.RenderHints
---@field DirtyDynamicColor UnityEngine.UIElements.RenderHints
---@field DirtyAll UnityEngine.UIElements.RenderHints
UnityEngine.UIElements.RenderHints = {}
---@alias CS.UnityEngine.UIElements.RenderHints UnityEngine.UIElements.RenderHints
CS.UnityEngine.UIElements.RenderHints = UnityEngine.UIElements.RenderHints


---@class UnityEngine.UIElements.RepaintData : System.Object
---@field currentOffset UnityEngine.Matrix4x4
---@field mousePosition UnityEngine.Vector2
---@field currentWorldClip UnityEngine.Rect
---@field repaintEvent UnityEngine.Event
UnityEngine.UIElements.RepaintData = {}
---@alias CS.UnityEngine.UIElements.RepaintData UnityEngine.UIElements.RepaintData
CS.UnityEngine.UIElements.RepaintData = UnityEngine.UIElements.RepaintData

---@return UnityEngine.UIElements.RepaintData
function UnityEngine.UIElements.RepaintData.New() end

---@class UnityEngine.UIElements.Repeat
---@field NoRepeat UnityEngine.UIElements.Repeat
---@field Space UnityEngine.UIElements.Repeat
---@field Round UnityEngine.UIElements.Repeat
---@field Repeat UnityEngine.UIElements.Repeat
UnityEngine.UIElements.Repeat = {}
---@alias CS.UnityEngine.UIElements.Repeat UnityEngine.UIElements.Repeat
CS.UnityEngine.UIElements.Repeat = UnityEngine.UIElements.Repeat


---@class UnityEngine.UIElements.RepeatButton : UnityEngine.UIElements.TextElement
---@field ussClassName string
UnityEngine.UIElements.RepeatButton = {}
---@alias CS.UnityEngine.UIElements.RepeatButton UnityEngine.UIElements.RepeatButton
CS.UnityEngine.UIElements.RepeatButton = UnityEngine.UIElements.RepeatButton

---@overload fun() : UnityEngine.UIElements.RepeatButton
---@param clickEvent System.Action | function
---@param delay number
---@param interval number
---@return UnityEngine.UIElements.RepeatButton
function UnityEngine.UIElements.RepeatButton.New(clickEvent, delay, interval) end
---@param clickEvent System.Action | function
---@param delay number
---@param interval number
function UnityEngine.UIElements.RepeatButton:SetAction(clickEvent, delay, interval) end

---@class UnityEngine.UIElements.RepeatButton.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.RepeatButton.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.RepeatButton.UxmlFactory UnityEngine.UIElements.RepeatButton.UxmlFactory
CS.UnityEngine.UIElements.RepeatButton.UxmlFactory = UnityEngine.UIElements.RepeatButton.UxmlFactory

---@return UnityEngine.UIElements.RepeatButton.UxmlFactory
function UnityEngine.UIElements.RepeatButton.UxmlFactory.New() end

---@class UnityEngine.UIElements.RepeatButton.UxmlTraits : UnityEngine.UIElements.TextElement.UxmlTraits
UnityEngine.UIElements.RepeatButton.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.RepeatButton.UxmlTraits UnityEngine.UIElements.RepeatButton.UxmlTraits
CS.UnityEngine.UIElements.RepeatButton.UxmlTraits = UnityEngine.UIElements.RepeatButton.UxmlTraits

---@return UnityEngine.UIElements.RepeatButton.UxmlTraits
function UnityEngine.UIElements.RepeatButton.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.RepeatButton.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.RepeatXY
---@field RepeatX UnityEngine.UIElements.RepeatXY
---@field RepeatY UnityEngine.UIElements.RepeatXY
UnityEngine.UIElements.RepeatXY = {}
---@alias CS.UnityEngine.UIElements.RepeatXY UnityEngine.UIElements.RepeatXY
CS.UnityEngine.UIElements.RepeatXY = UnityEngine.UIElements.RepeatXY


---@class UnityEngine.UIElements.ReusableCollectionItem : System.Object
---@field UndefinedIndex number
---@field rootElement UnityEngine.UIElements.VisualElement
---@field bindableElement UnityEngine.UIElements.VisualElement
---@field animator UnityEngine.UIElements.Experimental.ValueAnimation
---@field index number
---@field id number
UnityEngine.UIElements.ReusableCollectionItem = {}
---@alias CS.UnityEngine.UIElements.ReusableCollectionItem UnityEngine.UIElements.ReusableCollectionItem
CS.UnityEngine.UIElements.ReusableCollectionItem = UnityEngine.UIElements.ReusableCollectionItem

---@return UnityEngine.UIElements.ReusableCollectionItem
function UnityEngine.UIElements.ReusableCollectionItem.New() end
---@param item UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.ReusableCollectionItem:Init(item) end
function UnityEngine.UIElements.ReusableCollectionItem:PreAttachElement() end
function UnityEngine.UIElements.ReusableCollectionItem:DetachElement() end
function UnityEngine.UIElements.ReusableCollectionItem:DestroyElement() end
---@param selected boolean
function UnityEngine.UIElements.ReusableCollectionItem:SetSelected(selected) end
---@param dragGhost boolean
function UnityEngine.UIElements.ReusableCollectionItem:SetDragGhost(dragGhost) end

---@class UnityEngine.UIElements.ReusableListViewItem : UnityEngine.UIElements.ReusableCollectionItem
---@field rootElement UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.ReusableListViewItem = {}
---@alias CS.UnityEngine.UIElements.ReusableListViewItem UnityEngine.UIElements.ReusableListViewItem
CS.UnityEngine.UIElements.ReusableListViewItem = UnityEngine.UIElements.ReusableListViewItem

---@return UnityEngine.UIElements.ReusableListViewItem
function UnityEngine.UIElements.ReusableListViewItem.New() end
---@param item UnityEngine.UIElements.VisualElement
---@param usesAnimatedDragger boolean
function UnityEngine.UIElements.ReusableListViewItem:Init(item, usesAnimatedDragger) end
---@param needsDragHandle boolean
function UnityEngine.UIElements.ReusableListViewItem:UpdateDragHandle(needsDragHandle) end
function UnityEngine.UIElements.ReusableListViewItem:PreAttachElement() end
function UnityEngine.UIElements.ReusableListViewItem:DetachElement() end
---@param dragGhost boolean
function UnityEngine.UIElements.ReusableListViewItem:SetDragGhost(dragGhost) end

---@class UnityEngine.UIElements.ReusableMultiColumnListViewItem : UnityEngine.UIElements.ReusableListViewItem
---@field rootElement UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.ReusableMultiColumnListViewItem = {}
---@alias CS.UnityEngine.UIElements.ReusableMultiColumnListViewItem UnityEngine.UIElements.ReusableMultiColumnListViewItem
CS.UnityEngine.UIElements.ReusableMultiColumnListViewItem = UnityEngine.UIElements.ReusableMultiColumnListViewItem

---@return UnityEngine.UIElements.ReusableMultiColumnListViewItem
function UnityEngine.UIElements.ReusableMultiColumnListViewItem.New() end
---@overload fun(self: UnityEngine.UIElements.ReusableMultiColumnListViewItem, item: UnityEngine.UIElements.VisualElement)
---@param container UnityEngine.UIElements.VisualElement
---@param columns UnityEngine.UIElements.Columns
---@param usesAnimatedDrag boolean
function UnityEngine.UIElements.ReusableMultiColumnListViewItem:Init(container, columns, usesAnimatedDrag) end

---@class UnityEngine.UIElements.ReusableMultiColumnTreeViewItem : UnityEngine.UIElements.ReusableTreeViewItem
---@field rootElement UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.ReusableMultiColumnTreeViewItem = {}
---@alias CS.UnityEngine.UIElements.ReusableMultiColumnTreeViewItem UnityEngine.UIElements.ReusableMultiColumnTreeViewItem
CS.UnityEngine.UIElements.ReusableMultiColumnTreeViewItem = UnityEngine.UIElements.ReusableMultiColumnTreeViewItem

---@return UnityEngine.UIElements.ReusableMultiColumnTreeViewItem
function UnityEngine.UIElements.ReusableMultiColumnTreeViewItem.New() end
---@overload fun(self: UnityEngine.UIElements.ReusableMultiColumnTreeViewItem, item: UnityEngine.UIElements.VisualElement)
---@param container UnityEngine.UIElements.VisualElement
---@param columns UnityEngine.UIElements.Columns
function UnityEngine.UIElements.ReusableMultiColumnTreeViewItem:Init(container, columns) end

---@class UnityEngine.UIElements.ReusableTreeViewItem : UnityEngine.UIElements.ReusableCollectionItem
---@field rootElement UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.ReusableTreeViewItem = {}
---@alias CS.UnityEngine.UIElements.ReusableTreeViewItem UnityEngine.UIElements.ReusableTreeViewItem
CS.UnityEngine.UIElements.ReusableTreeViewItem = UnityEngine.UIElements.ReusableTreeViewItem

---@return UnityEngine.UIElements.ReusableTreeViewItem
function UnityEngine.UIElements.ReusableTreeViewItem.New() end
---@param item UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.ReusableTreeViewItem:Init(item) end
function UnityEngine.UIElements.ReusableTreeViewItem:PreAttachElement() end
function UnityEngine.UIElements.ReusableTreeViewItem:DetachElement() end
---@param depth number
function UnityEngine.UIElements.ReusableTreeViewItem:Indent(depth) end
---@param expanded boolean
function UnityEngine.UIElements.ReusableTreeViewItem:SetExpandedWithoutNotify(expanded) end
---@param visible boolean
function UnityEngine.UIElements.ReusableTreeViewItem:SetToggleVisibility(visible) end

---@class UnityEngine.UIElements.Rotate : System.ValueType
---@field angle UnityEngine.UIElements.Angle
UnityEngine.UIElements.Rotate = {}
---@alias CS.UnityEngine.UIElements.Rotate UnityEngine.UIElements.Rotate
CS.UnityEngine.UIElements.Rotate = UnityEngine.UIElements.Rotate

---@param angle UnityEngine.UIElements.Angle
---@return UnityEngine.UIElements.Rotate
function UnityEngine.UIElements.Rotate.New(angle) end
---@return UnityEngine.UIElements.Rotate
function UnityEngine.UIElements.Rotate.None() end
---@overload fun(self: UnityEngine.UIElements.Rotate, other: UnityEngine.UIElements.Rotate) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.Rotate:Equals(obj) end
---@return number
function UnityEngine.UIElements.Rotate:GetHashCode() end
---@return string
function UnityEngine.UIElements.Rotate:ToString() end

---@class UnityEngine.UIElements.RotateField : UnityEngine.UIElements.BaseField
UnityEngine.UIElements.RotateField = {}
---@alias CS.UnityEngine.UIElements.RotateField UnityEngine.UIElements.RotateField
CS.UnityEngine.UIElements.RotateField = UnityEngine.UIElements.RotateField

---@overload fun() : UnityEngine.UIElements.RotateField
---@overload fun(label: string) : UnityEngine.UIElements.RotateField
---@param label string
---@param rotate UnityEngine.UIElements.Rotate
---@return UnityEngine.UIElements.RotateField
function UnityEngine.UIElements.RotateField.New(label, rotate) end
---@param rotate UnityEngine.UIElements.Rotate
function UnityEngine.UIElements.RotateField:SetValueWithoutNotify(rotate) end

---@class UnityEngine.UIElements.RuleMatcher : System.ValueType
---@field sheet UnityEngine.UIElements.StyleSheet
---@field complexSelector UnityEngine.UIElements.StyleComplexSelector
UnityEngine.UIElements.RuleMatcher = {}
---@alias CS.UnityEngine.UIElements.RuleMatcher UnityEngine.UIElements.RuleMatcher
CS.UnityEngine.UIElements.RuleMatcher = UnityEngine.UIElements.RuleMatcher

---@param sheet UnityEngine.UIElements.StyleSheet
---@param complexSelector UnityEngine.UIElements.StyleComplexSelector
---@param styleSheetIndexInStack number
---@return UnityEngine.UIElements.RuleMatcher
function UnityEngine.UIElements.RuleMatcher.New(sheet, complexSelector, styleSheetIndexInStack) end
---@return string
function UnityEngine.UIElements.RuleMatcher:ToString() end

---@class UnityEngine.UIElements.RuntimeEventDispatcher : System.Object
UnityEngine.UIElements.RuntimeEventDispatcher = {}
---@alias CS.UnityEngine.UIElements.RuntimeEventDispatcher UnityEngine.UIElements.RuntimeEventDispatcher
CS.UnityEngine.UIElements.RuntimeEventDispatcher = UnityEngine.UIElements.RuntimeEventDispatcher

---@return UnityEngine.UIElements.EventDispatcher
function UnityEngine.UIElements.RuntimeEventDispatcher.Create() end

---@class UnityEngine.UIElements.RuntimePanel : UnityEngine.UIElements.BaseRuntimePanel
---@field panelSettings UnityEngine.UIElements.PanelSettings
UnityEngine.UIElements.RuntimePanel = {}
---@alias CS.UnityEngine.UIElements.RuntimePanel UnityEngine.UIElements.RuntimePanel
CS.UnityEngine.UIElements.RuntimePanel = UnityEngine.UIElements.RuntimePanel

---@param ownerObject UnityEngine.ScriptableObject
---@return UnityEngine.UIElements.RuntimePanel
function UnityEngine.UIElements.RuntimePanel.Create(ownerObject) end
function UnityEngine.UIElements.RuntimePanel:Update() end

---@class UnityEngine.UIElements.RuntimePanelUtils : System.Object
UnityEngine.UIElements.RuntimePanelUtils = {}
---@alias CS.UnityEngine.UIElements.RuntimePanelUtils UnityEngine.UIElements.RuntimePanelUtils
CS.UnityEngine.UIElements.RuntimePanelUtils = UnityEngine.UIElements.RuntimePanelUtils

---@param panel UnityEngine.UIElements.IPanel
---@param screenPosition UnityEngine.Vector2
---@return UnityEngine.Vector2
function UnityEngine.UIElements.RuntimePanelUtils.ScreenToPanel(panel, screenPosition) end
---@param panel UnityEngine.UIElements.IPanel
---@param worldPosition UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return UnityEngine.Vector2
function UnityEngine.UIElements.RuntimePanelUtils.CameraTransformWorldToPanel(panel, worldPosition, camera) end
---@param panel UnityEngine.UIElements.IPanel
---@param worldPosition UnityEngine.Vector3
---@param worldSize UnityEngine.Vector2
---@param camera UnityEngine.Camera
---@return UnityEngine.Rect
function UnityEngine.UIElements.RuntimePanelUtils.CameraTransformWorldToPanelRect(panel, worldPosition, worldSize, camera) end
---@param panel UnityEngine.UIElements.IPanel
function UnityEngine.UIElements.RuntimePanelUtils.ResetDynamicAtlas(panel) end
---@param panel UnityEngine.UIElements.IPanel
---@param texture UnityEngine.Texture2D
function UnityEngine.UIElements.RuntimePanelUtils.SetTextureDirty(panel, texture) end

---@class UnityEngine.UIElements.RuntimeUIElementsBridge : UnityEngine.UIElements.UIElementsBridge
UnityEngine.UIElements.RuntimeUIElementsBridge = {}
---@alias CS.UnityEngine.UIElements.RuntimeUIElementsBridge UnityEngine.UIElements.RuntimeUIElementsBridge
CS.UnityEngine.UIElements.RuntimeUIElementsBridge = UnityEngine.UIElements.RuntimeUIElementsBridge

---@return UnityEngine.UIElements.RuntimeUIElementsBridge
function UnityEngine.UIElements.RuntimeUIElementsBridge.New() end
---@param value number
function UnityEngine.UIElements.RuntimeUIElementsBridge:SetWantsMouseJumping(value) end

---@class UnityEngine.UIElements.SafeHandleAccess : System.ValueType
UnityEngine.UIElements.SafeHandleAccess = {}
---@alias CS.UnityEngine.UIElements.SafeHandleAccess UnityEngine.UIElements.SafeHandleAccess
CS.UnityEngine.UIElements.SafeHandleAccess = UnityEngine.UIElements.SafeHandleAccess

---@param ptr System.IntPtr
---@return UnityEngine.UIElements.SafeHandleAccess
function UnityEngine.UIElements.SafeHandleAccess.New(ptr) end
---@return boolean
function UnityEngine.UIElements.SafeHandleAccess:IsNull() end

---@class UnityEngine.UIElements.Salt
---@field TagNameSalt UnityEngine.UIElements.Salt
---@field IdSalt UnityEngine.UIElements.Salt
---@field ClassSalt UnityEngine.UIElements.Salt
UnityEngine.UIElements.Salt = {}
---@alias CS.UnityEngine.UIElements.Salt UnityEngine.UIElements.Salt
CS.UnityEngine.UIElements.Salt = UnityEngine.UIElements.Salt


---@class UnityEngine.UIElements.SavePersistentViewData : System.MulticastDelegate
UnityEngine.UIElements.SavePersistentViewData = {}
---@alias CS.UnityEngine.UIElements.SavePersistentViewData UnityEngine.UIElements.SavePersistentViewData
CS.UnityEngine.UIElements.SavePersistentViewData = UnityEngine.UIElements.SavePersistentViewData

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.UIElements.SavePersistentViewData
function UnityEngine.UIElements.SavePersistentViewData.New(object, method) end
function UnityEngine.UIElements.SavePersistentViewData:Invoke() end
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.UIElements.SavePersistentViewData:BeginInvoke(callback, object) end
---@param result System.IAsyncResult
function UnityEngine.UIElements.SavePersistentViewData:EndInvoke(result) end

---@class UnityEngine.UIElements.Scale : System.ValueType
---@field value UnityEngine.Vector3
UnityEngine.UIElements.Scale = {}
---@alias CS.UnityEngine.UIElements.Scale UnityEngine.UIElements.Scale
CS.UnityEngine.UIElements.Scale = UnityEngine.UIElements.Scale

---@overload fun(scale: UnityEngine.Vector2) : UnityEngine.UIElements.Scale
---@param scale UnityEngine.Vector3
---@return UnityEngine.UIElements.Scale
function UnityEngine.UIElements.Scale.New(scale) end
---@return UnityEngine.UIElements.Scale
function UnityEngine.UIElements.Scale.None() end
---@overload fun(self: UnityEngine.UIElements.Scale, other: UnityEngine.UIElements.Scale) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.Scale:Equals(obj) end
---@return number
function UnityEngine.UIElements.Scale:GetHashCode() end
---@return string
function UnityEngine.UIElements.Scale:ToString() end

---@class UnityEngine.UIElements.ScheduledItem : System.Object
---@field OnceCondition System.Func
---@field ForeverCondition System.Func
---@field timerUpdateStopCondition System.Func
---@field startMs number
---@field delayMs number
---@field intervalMs number
---@field endTimeMs number
UnityEngine.UIElements.ScheduledItem = {}
---@alias CS.UnityEngine.UIElements.ScheduledItem UnityEngine.UIElements.ScheduledItem
CS.UnityEngine.UIElements.ScheduledItem = UnityEngine.UIElements.ScheduledItem

---@param durationMs number
function UnityEngine.UIElements.ScheduledItem:SetDuration(durationMs) end
---@param state UnityEngine.UIElements.TimerState
function UnityEngine.UIElements.ScheduledItem:PerformTimerUpdate(state) end
---@return boolean
function UnityEngine.UIElements.ScheduledItem:ShouldUnschedule() end

---@class UnityEngine.UIElements.Scroller : UnityEngine.UIElements.VisualElement
---@field ussClassName string
---@field horizontalVariantUssClassName string
---@field verticalVariantUssClassName string
---@field sliderUssClassName string
---@field lowButtonUssClassName string
---@field highButtonUssClassName string
---@field slider UnityEngine.UIElements.Slider
---@field lowButton UnityEngine.UIElements.RepeatButton
---@field highButton UnityEngine.UIElements.RepeatButton
---@field value number
---@field lowValue number
---@field highValue number
---@field direction UnityEngine.UIElements.SliderDirection
UnityEngine.UIElements.Scroller = {}
---@alias CS.UnityEngine.UIElements.Scroller UnityEngine.UIElements.Scroller
CS.UnityEngine.UIElements.Scroller = UnityEngine.UIElements.Scroller

---@overload fun() : UnityEngine.UIElements.Scroller
---@param lowValue number
---@param highValue number
---@param valueChanged System.Action | function
---@param direction UnityEngine.UIElements.SliderDirection
---@return UnityEngine.UIElements.Scroller
function UnityEngine.UIElements.Scroller.New(lowValue, highValue, valueChanged, direction) end
---@param factor number
function UnityEngine.UIElements.Scroller:Adjust(factor) end
---@overload fun(self: UnityEngine.UIElements.Scroller)
---@param factor number
function UnityEngine.UIElements.Scroller:ScrollPageUp(factor) end
---@overload fun(self: UnityEngine.UIElements.Scroller)
---@param factor number
function UnityEngine.UIElements.Scroller:ScrollPageDown(factor) end

---@class UnityEngine.UIElements.Scroller.ScrollerSlider : UnityEngine.UIElements.Slider
UnityEngine.UIElements.Scroller.ScrollerSlider = {}
---@alias CS.UnityEngine.UIElements.Scroller.ScrollerSlider UnityEngine.UIElements.Scroller.ScrollerSlider
CS.UnityEngine.UIElements.Scroller.ScrollerSlider = UnityEngine.UIElements.Scroller.ScrollerSlider

---@param start number
---@param _end number
---@param direction UnityEngine.UIElements.SliderDirection
---@param pageSize number
---@return UnityEngine.UIElements.Scroller.ScrollerSlider
function UnityEngine.UIElements.Scroller.ScrollerSlider.New(start, _end, direction, pageSize) end

---@class UnityEngine.UIElements.Scroller.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.Scroller.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.Scroller.UxmlFactory UnityEngine.UIElements.Scroller.UxmlFactory
CS.UnityEngine.UIElements.Scroller.UxmlFactory = UnityEngine.UIElements.Scroller.UxmlFactory

---@return UnityEngine.UIElements.Scroller.UxmlFactory
function UnityEngine.UIElements.Scroller.UxmlFactory.New() end

---@class UnityEngine.UIElements.Scroller.UxmlTraits : UnityEngine.UIElements.VisualElement.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.Scroller.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.Scroller.UxmlTraits UnityEngine.UIElements.Scroller.UxmlTraits
CS.UnityEngine.UIElements.Scroller.UxmlTraits = UnityEngine.UIElements.Scroller.UxmlTraits

---@return UnityEngine.UIElements.Scroller.UxmlTraits
function UnityEngine.UIElements.Scroller.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.Scroller.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.ScrollerVisibility
---@field Auto UnityEngine.UIElements.ScrollerVisibility
---@field AlwaysVisible UnityEngine.UIElements.ScrollerVisibility
---@field Hidden UnityEngine.UIElements.ScrollerVisibility
UnityEngine.UIElements.ScrollerVisibility = {}
---@alias CS.UnityEngine.UIElements.ScrollerVisibility UnityEngine.UIElements.ScrollerVisibility
CS.UnityEngine.UIElements.ScrollerVisibility = UnityEngine.UIElements.ScrollerVisibility


---@class UnityEngine.UIElements.ScrollView : UnityEngine.UIElements.VisualElement
---@field ussClassName string
---@field viewportUssClassName string
---@field horizontalVariantViewportUssClassName string
---@field verticalVariantViewportUssClassName string
---@field verticalHorizontalVariantViewportUssClassName string
---@field contentAndVerticalScrollUssClassName string
---@field contentUssClassName string
---@field horizontalVariantContentUssClassName string
---@field verticalVariantContentUssClassName string
---@field verticalHorizontalVariantContentUssClassName string
---@field hScrollerUssClassName string
---@field vScrollerUssClassName string
---@field horizontalVariantUssClassName string
---@field verticalVariantUssClassName string
---@field verticalHorizontalVariantUssClassName string
---@field scrollVariantUssClassName string
---@field horizontalScrollerVisibility UnityEngine.UIElements.ScrollerVisibility
---@field verticalScrollerVisibility UnityEngine.UIElements.ScrollerVisibility
---@field scrollOffset UnityEngine.Vector2
---@field horizontalPageSize number
---@field verticalPageSize number
---@field mouseWheelScrollSize number
---@field scrollDecelerationRate number
---@field elasticity number
---@field touchScrollBehavior UnityEngine.UIElements.ScrollView.TouchScrollBehavior
---@field nestedInteractionKind UnityEngine.UIElements.ScrollView.NestedInteractionKind
---@field elasticAnimationIntervalMs number
---@field contentViewport UnityEngine.UIElements.VisualElement
---@field horizontalScroller UnityEngine.UIElements.Scroller
---@field verticalScroller UnityEngine.UIElements.Scroller
---@field contentContainer UnityEngine.UIElements.VisualElement
---@field mode UnityEngine.UIElements.ScrollViewMode
UnityEngine.UIElements.ScrollView = {}
---@alias CS.UnityEngine.UIElements.ScrollView UnityEngine.UIElements.ScrollView
CS.UnityEngine.UIElements.ScrollView = UnityEngine.UIElements.ScrollView

---@overload fun() : UnityEngine.UIElements.ScrollView
---@param scrollViewMode UnityEngine.UIElements.ScrollViewMode
---@return UnityEngine.UIElements.ScrollView
function UnityEngine.UIElements.ScrollView.New(scrollViewMode) end
---@param child UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.ScrollView:ScrollTo(child) end

---@class UnityEngine.UIElements.ScrollView.NestedInteractionKind
---@field Default UnityEngine.UIElements.ScrollView.NestedInteractionKind
---@field StopScrolling UnityEngine.UIElements.ScrollView.NestedInteractionKind
---@field ForwardScrolling UnityEngine.UIElements.ScrollView.NestedInteractionKind
UnityEngine.UIElements.ScrollView.NestedInteractionKind = {}
---@alias CS.UnityEngine.UIElements.ScrollView.NestedInteractionKind UnityEngine.UIElements.ScrollView.NestedInteractionKind
CS.UnityEngine.UIElements.ScrollView.NestedInteractionKind = UnityEngine.UIElements.ScrollView.NestedInteractionKind


---@class UnityEngine.UIElements.ScrollView.TouchScrollBehavior
---@field Unrestricted UnityEngine.UIElements.ScrollView.TouchScrollBehavior
---@field Elastic UnityEngine.UIElements.ScrollView.TouchScrollBehavior
---@field Clamped UnityEngine.UIElements.ScrollView.TouchScrollBehavior
UnityEngine.UIElements.ScrollView.TouchScrollBehavior = {}
---@alias CS.UnityEngine.UIElements.ScrollView.TouchScrollBehavior UnityEngine.UIElements.ScrollView.TouchScrollBehavior
CS.UnityEngine.UIElements.ScrollView.TouchScrollBehavior = UnityEngine.UIElements.ScrollView.TouchScrollBehavior


---@class UnityEngine.UIElements.ScrollView.TouchScrollingResult
---@field Apply UnityEngine.UIElements.ScrollView.TouchScrollingResult
---@field Forward UnityEngine.UIElements.ScrollView.TouchScrollingResult
---@field Block UnityEngine.UIElements.ScrollView.TouchScrollingResult
UnityEngine.UIElements.ScrollView.TouchScrollingResult = {}
---@alias CS.UnityEngine.UIElements.ScrollView.TouchScrollingResult UnityEngine.UIElements.ScrollView.TouchScrollingResult
CS.UnityEngine.UIElements.ScrollView.TouchScrollingResult = UnityEngine.UIElements.ScrollView.TouchScrollingResult


---@class UnityEngine.UIElements.ScrollView.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.ScrollView.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.ScrollView.UxmlFactory UnityEngine.UIElements.ScrollView.UxmlFactory
CS.UnityEngine.UIElements.ScrollView.UxmlFactory = UnityEngine.UIElements.ScrollView.UxmlFactory

---@return UnityEngine.UIElements.ScrollView.UxmlFactory
function UnityEngine.UIElements.ScrollView.UxmlFactory.New() end

---@class UnityEngine.UIElements.ScrollView.UxmlTraits : UnityEngine.UIElements.VisualElement.UxmlTraits
UnityEngine.UIElements.ScrollView.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.ScrollView.UxmlTraits UnityEngine.UIElements.ScrollView.UxmlTraits
CS.UnityEngine.UIElements.ScrollView.UxmlTraits = UnityEngine.UIElements.ScrollView.UxmlTraits

---@return UnityEngine.UIElements.ScrollView.UxmlTraits
function UnityEngine.UIElements.ScrollView.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.ScrollView.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.ScrollViewMode
---@field Vertical UnityEngine.UIElements.ScrollViewMode
---@field Horizontal UnityEngine.UIElements.ScrollViewMode
---@field VerticalAndHorizontal UnityEngine.UIElements.ScrollViewMode
UnityEngine.UIElements.ScrollViewMode = {}
---@alias CS.UnityEngine.UIElements.ScrollViewMode UnityEngine.UIElements.ScrollViewMode
CS.UnityEngine.UIElements.ScrollViewMode = UnityEngine.UIElements.ScrollViewMode


---@class UnityEngine.UIElements.SelectionType
---@field None UnityEngine.UIElements.SelectionType
---@field Single UnityEngine.UIElements.SelectionType
---@field Multiple UnityEngine.UIElements.SelectionType
UnityEngine.UIElements.SelectionType = {}
---@alias CS.UnityEngine.UIElements.SelectionType UnityEngine.UIElements.SelectionType
CS.UnityEngine.UIElements.SelectionType = UnityEngine.UIElements.SelectionType


---@class UnityEngine.UIElements.SerializedVirtualizationData : System.Object
---@field scrollOffset UnityEngine.Vector2
---@field firstVisibleIndex number
---@field contentPadding number
---@field contentHeight number
---@field anchoredItemIndex number
---@field anchorOffset number
UnityEngine.UIElements.SerializedVirtualizationData = {}
---@alias CS.UnityEngine.UIElements.SerializedVirtualizationData UnityEngine.UIElements.SerializedVirtualizationData
CS.UnityEngine.UIElements.SerializedVirtualizationData = UnityEngine.UIElements.SerializedVirtualizationData

---@return UnityEngine.UIElements.SerializedVirtualizationData
function UnityEngine.UIElements.SerializedVirtualizationData.New() end

---@class UnityEngine.UIElements.SetupDragAndDropArgs : System.ValueType
---@field draggedElement UnityEngine.UIElements.VisualElement
---@field selectedIds System.Collections.Generic.IEnumerable
---@field startDragArgs UnityEngine.UIElements.StartDragArgs
UnityEngine.UIElements.SetupDragAndDropArgs = {}
---@alias CS.UnityEngine.UIElements.SetupDragAndDropArgs UnityEngine.UIElements.SetupDragAndDropArgs
CS.UnityEngine.UIElements.SetupDragAndDropArgs = UnityEngine.UIElements.SetupDragAndDropArgs


---@class UnityEngine.UIElements.Slider : UnityEngine.UIElements.BaseSlider
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.Slider = {}
---@alias CS.UnityEngine.UIElements.Slider UnityEngine.UIElements.Slider
CS.UnityEngine.UIElements.Slider = UnityEngine.UIElements.Slider

---@overload fun() : UnityEngine.UIElements.Slider
---@overload fun(start: number, _end: number, direction: UnityEngine.UIElements.SliderDirection, pageSize: number) : UnityEngine.UIElements.Slider
---@param label string
---@param start number
---@param _end number
---@param direction UnityEngine.UIElements.SliderDirection
---@param pageSize number
---@return UnityEngine.UIElements.Slider
function UnityEngine.UIElements.Slider.New(label, start, _end, direction, pageSize) end
---@param delta UnityEngine.Vector3
---@param speed UnityEngine.UIElements.DeltaSpeed
---@param startValue number
function UnityEngine.UIElements.Slider:ApplyInputDeviceDelta(delta, speed, startValue) end

---@class UnityEngine.UIElements.Slider.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.Slider.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.Slider.UxmlFactory UnityEngine.UIElements.Slider.UxmlFactory
CS.UnityEngine.UIElements.Slider.UxmlFactory = UnityEngine.UIElements.Slider.UxmlFactory

---@return UnityEngine.UIElements.Slider.UxmlFactory
function UnityEngine.UIElements.Slider.UxmlFactory.New() end

---@class UnityEngine.UIElements.Slider.UxmlTraits : UnityEngine.UIElements.BaseSlider.UxmlTraits
UnityEngine.UIElements.Slider.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.Slider.UxmlTraits UnityEngine.UIElements.Slider.UxmlTraits
CS.UnityEngine.UIElements.Slider.UxmlTraits = UnityEngine.UIElements.Slider.UxmlTraits

---@return UnityEngine.UIElements.Slider.UxmlTraits
function UnityEngine.UIElements.Slider.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.Slider.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.SliderDirection
---@field Horizontal UnityEngine.UIElements.SliderDirection
---@field Vertical UnityEngine.UIElements.SliderDirection
UnityEngine.UIElements.SliderDirection = {}
---@alias CS.UnityEngine.UIElements.SliderDirection UnityEngine.UIElements.SliderDirection
CS.UnityEngine.UIElements.SliderDirection = UnityEngine.UIElements.SliderDirection


---@class UnityEngine.UIElements.SliderInt : UnityEngine.UIElements.BaseSlider
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
---@field pageSize number
UnityEngine.UIElements.SliderInt = {}
---@alias CS.UnityEngine.UIElements.SliderInt UnityEngine.UIElements.SliderInt
CS.UnityEngine.UIElements.SliderInt = UnityEngine.UIElements.SliderInt

---@overload fun() : UnityEngine.UIElements.SliderInt
---@overload fun(start: number, _end: number, direction: UnityEngine.UIElements.SliderDirection, pageSize: number) : UnityEngine.UIElements.SliderInt
---@param label string
---@param start number
---@param _end number
---@param direction UnityEngine.UIElements.SliderDirection
---@param pageSize number
---@return UnityEngine.UIElements.SliderInt
function UnityEngine.UIElements.SliderInt.New(label, start, _end, direction, pageSize) end
---@param delta UnityEngine.Vector3
---@param speed UnityEngine.UIElements.DeltaSpeed
---@param startValue number
function UnityEngine.UIElements.SliderInt:ApplyInputDeviceDelta(delta, speed, startValue) end

---@class UnityEngine.UIElements.SliderInt.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.SliderInt.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.SliderInt.UxmlFactory UnityEngine.UIElements.SliderInt.UxmlFactory
CS.UnityEngine.UIElements.SliderInt.UxmlFactory = UnityEngine.UIElements.SliderInt.UxmlFactory

---@return UnityEngine.UIElements.SliderInt.UxmlFactory
function UnityEngine.UIElements.SliderInt.UxmlFactory.New() end

---@class UnityEngine.UIElements.SliderInt.UxmlTraits : UnityEngine.UIElements.BaseSlider.UxmlTraits
UnityEngine.UIElements.SliderInt.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.SliderInt.UxmlTraits UnityEngine.UIElements.SliderInt.UxmlTraits
CS.UnityEngine.UIElements.SliderInt.UxmlTraits = UnityEngine.UIElements.SliderInt.UxmlTraits

---@return UnityEngine.UIElements.SliderInt.UxmlTraits
function UnityEngine.UIElements.SliderInt.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.SliderInt.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.SortColumnDescription : System.Object
---@field columnName string
---@field columnIndex number
---@field column UnityEngine.UIElements.Column
---@field direction UnityEngine.UIElements.SortDirection
UnityEngine.UIElements.SortColumnDescription = {}
---@alias CS.UnityEngine.UIElements.SortColumnDescription UnityEngine.UIElements.SortColumnDescription
CS.UnityEngine.UIElements.SortColumnDescription = UnityEngine.UIElements.SortColumnDescription

---@overload fun() : UnityEngine.UIElements.SortColumnDescription
---@overload fun(columnIndex: number, direction: UnityEngine.UIElements.SortDirection) : UnityEngine.UIElements.SortColumnDescription
---@param columnName string
---@param direction UnityEngine.UIElements.SortDirection
---@return UnityEngine.UIElements.SortColumnDescription
function UnityEngine.UIElements.SortColumnDescription.New(columnName, direction) end

---@class UnityEngine.UIElements.SortColumnDescription.UxmlObjectFactory : UnityEngine.UIElements.UxmlObjectFactory[T,UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits[T]]
UnityEngine.UIElements.SortColumnDescription.UxmlObjectFactory = {}
---@alias CS.UnityEngine.UIElements.SortColumnDescription.UxmlObjectFactory UnityEngine.UIElements.SortColumnDescription.UxmlObjectFactory
CS.UnityEngine.UIElements.SortColumnDescription.UxmlObjectFactory = UnityEngine.UIElements.SortColumnDescription.UxmlObjectFactory

---@return UnityEngine.UIElements.SortColumnDescription.UxmlObjectFactory
function UnityEngine.UIElements.SortColumnDescription.UxmlObjectFactory.New() end

---@class UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits : UnityEngine.UIElements.UxmlObjectTraits[T]
UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits = {}
---@alias CS.UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits
CS.UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits = UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits

---@return UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits
function UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits.New() end
---@param ref_obj T
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return T
function UnityEngine.UIElements.SortColumnDescription.UxmlObjectTraits:Init(ref_obj, bag, cc) end

---@class UnityEngine.UIElements.SortColumnDescriptions : System.Object
---@field Count number
---@field IsReadOnly boolean
---@field Item UnityEngine.UIElements.SortColumnDescription
UnityEngine.UIElements.SortColumnDescriptions = {}
---@alias CS.UnityEngine.UIElements.SortColumnDescriptions UnityEngine.UIElements.SortColumnDescriptions
CS.UnityEngine.UIElements.SortColumnDescriptions = UnityEngine.UIElements.SortColumnDescriptions

---@return UnityEngine.UIElements.SortColumnDescriptions
function UnityEngine.UIElements.SortColumnDescriptions.New() end
---@return System.Collections.Generic.IEnumerator
function UnityEngine.UIElements.SortColumnDescriptions:GetEnumerator() end
---@param item UnityEngine.UIElements.SortColumnDescription
function UnityEngine.UIElements.SortColumnDescriptions:Add(item) end
function UnityEngine.UIElements.SortColumnDescriptions:Clear() end
---@param item UnityEngine.UIElements.SortColumnDescription
---@return boolean
function UnityEngine.UIElements.SortColumnDescriptions:Contains(item) end
---@param array UnityEngine.UIElements.SortColumnDescription[]
---@param arrayIndex number
function UnityEngine.UIElements.SortColumnDescriptions:CopyTo(array, arrayIndex) end
---@param desc UnityEngine.UIElements.SortColumnDescription
---@return boolean
function UnityEngine.UIElements.SortColumnDescriptions:Remove(desc) end
---@param desc UnityEngine.UIElements.SortColumnDescription
---@return number
function UnityEngine.UIElements.SortColumnDescriptions:IndexOf(desc) end
---@param index number
---@param desc UnityEngine.UIElements.SortColumnDescription
function UnityEngine.UIElements.SortColumnDescriptions:Insert(index, desc) end
---@param index number
function UnityEngine.UIElements.SortColumnDescriptions:RemoveAt(index) end

---@class UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectFactory : UnityEngine.UIElements.UxmlObjectFactory[T,UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits[T]]
UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectFactory = {}
---@alias CS.UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectFactory UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectFactory
CS.UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectFactory = UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectFactory

---@return UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectFactory
function UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectFactory.New() end

---@class UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits : UnityEngine.UIElements.UxmlObjectTraits[T]
UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits = {}
---@alias CS.UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits
CS.UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits = UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits

---@return UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits
function UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits.New() end
---@param ref_obj T
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return T
function UnityEngine.UIElements.SortColumnDescriptions.UxmlObjectTraits:Init(ref_obj, bag, cc) end

---@class UnityEngine.UIElements.SortDirection
---@field Ascending UnityEngine.UIElements.SortDirection
---@field Descending UnityEngine.UIElements.SortDirection
UnityEngine.UIElements.SortDirection = {}
---@alias CS.UnityEngine.UIElements.SortDirection UnityEngine.UIElements.SortDirection
CS.UnityEngine.UIElements.SortDirection = UnityEngine.UIElements.SortDirection


---@class UnityEngine.UIElements.Spacing : System.ValueType
---@field left number
---@field top number
---@field right number
---@field bottom number
---@field horizontal number
---@field vertical number
UnityEngine.UIElements.Spacing = {}
---@alias CS.UnityEngine.UIElements.Spacing UnityEngine.UIElements.Spacing
CS.UnityEngine.UIElements.Spacing = UnityEngine.UIElements.Spacing

---@param left number
---@param top number
---@param right number
---@param bottom number
---@return UnityEngine.UIElements.Spacing
function UnityEngine.UIElements.Spacing.New(left, top, right, bottom) end

---@class UnityEngine.UIElements.StartDragArgs : System.ValueType
---@field title string
---@field visualMode UnityEngine.UIElements.DragVisualMode
UnityEngine.UIElements.StartDragArgs = {}
---@alias CS.UnityEngine.UIElements.StartDragArgs UnityEngine.UIElements.StartDragArgs
CS.UnityEngine.UIElements.StartDragArgs = UnityEngine.UIElements.StartDragArgs

---@param title string
---@param visualMode UnityEngine.UIElements.DragVisualMode
---@return UnityEngine.UIElements.StartDragArgs
function UnityEngine.UIElements.StartDragArgs.New(title, visualMode) end
---@param key string
---@param data System.Object
function UnityEngine.UIElements.StartDragArgs:SetGenericData(key, data) end
---@param references System.Collections.Generic.IEnumerable
function UnityEngine.UIElements.StartDragArgs:SetUnityObjectReferences(references) end

---@class UnityEngine.UIElements.StringObjectListPool : UnityEngine.UIElements.ObjectListPool
UnityEngine.UIElements.StringObjectListPool = {}
---@alias CS.UnityEngine.UIElements.StringObjectListPool UnityEngine.UIElements.StringObjectListPool
CS.UnityEngine.UIElements.StringObjectListPool = UnityEngine.UIElements.StringObjectListPool

---@return UnityEngine.UIElements.StringObjectListPool
function UnityEngine.UIElements.StringObjectListPool.New() end

---@class UnityEngine.UIElements.StringUtils : System.Object
UnityEngine.UIElements.StringUtils = {}
---@alias CS.UnityEngine.UIElements.StringUtils UnityEngine.UIElements.StringUtils
CS.UnityEngine.UIElements.StringUtils = UnityEngine.UIElements.StringUtils

---@param s string
---@param t string
---@return number
function UnityEngine.UIElements.StringUtils.LevenshteinDistance(s, t) end

---@class UnityEngine.UIElements.StringUtilsExtensions : System.Object
UnityEngine.UIElements.StringUtilsExtensions = {}
---@alias CS.UnityEngine.UIElements.StringUtilsExtensions UnityEngine.UIElements.StringUtilsExtensions
CS.UnityEngine.UIElements.StringUtilsExtensions = UnityEngine.UIElements.StringUtilsExtensions

---@param text string
---@return string
function UnityEngine.UIElements.StringUtilsExtensions.ToPascalCase(text) end
---@param text string
---@return string
function UnityEngine.UIElements.StringUtilsExtensions.ToCamelCase(text) end
---@param text string
---@return string
function UnityEngine.UIElements.StringUtilsExtensions.ToKebabCase(text) end
---@param text string
---@return string
function UnityEngine.UIElements.StringUtilsExtensions.ToTrainCase(text) end
---@param text string
---@return string
function UnityEngine.UIElements.StringUtilsExtensions.ToSnakeCase(text) end
---@param a string
---@param b string
---@return boolean
function UnityEngine.UIElements.StringUtilsExtensions.EndsWithIgnoreCaseFast(a, b) end
---@param a string
---@param b string
---@return boolean
function UnityEngine.UIElements.StringUtilsExtensions.StartsWithIgnoreCaseFast(a, b) end

---@class UnityEngine.UIElements.StyleBackground : System.ValueType
---@field value UnityEngine.UIElements.Background
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleBackground = {}
---@alias CS.UnityEngine.UIElements.StyleBackground UnityEngine.UIElements.StyleBackground
CS.UnityEngine.UIElements.StyleBackground = UnityEngine.UIElements.StyleBackground

---@overload fun(v: UnityEngine.UIElements.Background) : UnityEngine.UIElements.StyleBackground
---@overload fun(v: UnityEngine.Texture2D) : UnityEngine.UIElements.StyleBackground
---@overload fun(v: UnityEngine.Sprite) : UnityEngine.UIElements.StyleBackground
---@overload fun(v: UnityEngine.UIElements.VectorImage) : UnityEngine.UIElements.StyleBackground
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleBackground
function UnityEngine.UIElements.StyleBackground.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleBackground, other: UnityEngine.UIElements.StyleBackground) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleBackground:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleBackground:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleBackground:ToString() end

---@class UnityEngine.UIElements.StyleBackgroundPosition : System.ValueType
---@field value UnityEngine.UIElements.BackgroundPosition
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleBackgroundPosition = {}
---@alias CS.UnityEngine.UIElements.StyleBackgroundPosition UnityEngine.UIElements.StyleBackgroundPosition
CS.UnityEngine.UIElements.StyleBackgroundPosition = UnityEngine.UIElements.StyleBackgroundPosition

---@overload fun(v: UnityEngine.UIElements.BackgroundPosition) : UnityEngine.UIElements.StyleBackgroundPosition
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleBackgroundPosition
function UnityEngine.UIElements.StyleBackgroundPosition.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleBackgroundPosition, other: UnityEngine.UIElements.StyleBackgroundPosition) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleBackgroundPosition:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleBackgroundPosition:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleBackgroundPosition:ToString() end

---@class UnityEngine.UIElements.StyleBackgroundRepeat : System.ValueType
---@field value UnityEngine.UIElements.BackgroundRepeat
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleBackgroundRepeat = {}
---@alias CS.UnityEngine.UIElements.StyleBackgroundRepeat UnityEngine.UIElements.StyleBackgroundRepeat
CS.UnityEngine.UIElements.StyleBackgroundRepeat = UnityEngine.UIElements.StyleBackgroundRepeat

---@overload fun(v: UnityEngine.UIElements.BackgroundRepeat) : UnityEngine.UIElements.StyleBackgroundRepeat
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleBackgroundRepeat
function UnityEngine.UIElements.StyleBackgroundRepeat.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleBackgroundRepeat, other: UnityEngine.UIElements.StyleBackgroundRepeat) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleBackgroundRepeat:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleBackgroundRepeat:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleBackgroundRepeat:ToString() end

---@class UnityEngine.UIElements.StyleBackgroundSize : System.ValueType
---@field value UnityEngine.UIElements.BackgroundSize
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleBackgroundSize = {}
---@alias CS.UnityEngine.UIElements.StyleBackgroundSize UnityEngine.UIElements.StyleBackgroundSize
CS.UnityEngine.UIElements.StyleBackgroundSize = UnityEngine.UIElements.StyleBackgroundSize

---@overload fun(v: UnityEngine.UIElements.BackgroundSize) : UnityEngine.UIElements.StyleBackgroundSize
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleBackgroundSize
function UnityEngine.UIElements.StyleBackgroundSize.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleBackgroundSize, other: UnityEngine.UIElements.StyleBackgroundSize) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleBackgroundSize:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleBackgroundSize:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleBackgroundSize:ToString() end

---@class UnityEngine.UIElements.StyleCache : System.Object
UnityEngine.UIElements.StyleCache = {}
---@alias CS.UnityEngine.UIElements.StyleCache UnityEngine.UIElements.StyleCache
CS.UnityEngine.UIElements.StyleCache = UnityEngine.UIElements.StyleCache

---@overload fun(hash: number, out_data: UnityEngine.UIElements.ComputedStyle) : boolean, UnityEngine.UIElements.ComputedStyle
---@overload fun(hash: number, out_data: UnityEngine.UIElements.StyleVariableContext) : boolean, UnityEngine.UIElements.StyleVariableContext
---@param hash number
---@param out_data UnityEngine.UIElements.ComputedTransitionProperty[]
---@return boolean, UnityEngine.UIElements.ComputedTransitionProperty[]
function UnityEngine.UIElements.StyleCache.TryGetValue(hash, out_data) end
---@overload fun(hash: number, ref_data: UnityEngine.UIElements.ComputedStyle) : UnityEngine.UIElements.ComputedStyle
---@overload fun(hash: number, data: UnityEngine.UIElements.StyleVariableContext)
---@param hash number
---@param data UnityEngine.UIElements.ComputedTransitionProperty[]
function UnityEngine.UIElements.StyleCache.SetValue(hash, data) end
function UnityEngine.UIElements.StyleCache.ClearStyleCache() end

---@class UnityEngine.UIElements.StyleColor : System.ValueType
---@field value UnityEngine.Color
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleColor = {}
---@alias CS.UnityEngine.UIElements.StyleColor UnityEngine.UIElements.StyleColor
CS.UnityEngine.UIElements.StyleColor = UnityEngine.UIElements.StyleColor

---@overload fun(v: UnityEngine.Color) : UnityEngine.UIElements.StyleColor
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleColor
function UnityEngine.UIElements.StyleColor.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleColor, other: UnityEngine.UIElements.StyleColor) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleColor:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleColor:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleColor:ToString() end

---@class UnityEngine.UIElements.StyleComplexSelector : System.Object
---@field ancestorHashes UnityEngine.UIElements.Hashes
---@field specificity number
---@field rule UnityEngine.UIElements.StyleRule
---@field isSimple boolean
---@field selectors UnityEngine.UIElements.StyleSelector[]
UnityEngine.UIElements.StyleComplexSelector = {}
---@alias CS.UnityEngine.UIElements.StyleComplexSelector UnityEngine.UIElements.StyleComplexSelector
CS.UnityEngine.UIElements.StyleComplexSelector = UnityEngine.UIElements.StyleComplexSelector

---@return UnityEngine.UIElements.StyleComplexSelector
function UnityEngine.UIElements.StyleComplexSelector.New() end
function UnityEngine.UIElements.StyleComplexSelector:OnBeforeSerialize() end
function UnityEngine.UIElements.StyleComplexSelector:OnAfterDeserialize() end
---@return string
function UnityEngine.UIElements.StyleComplexSelector:ToString() end

---@class UnityEngine.UIElements.StyleComplexSelector.PseudoStateData : System.ValueType
---@field state UnityEngine.UIElements.PseudoStates
---@field negate boolean
UnityEngine.UIElements.StyleComplexSelector.PseudoStateData = {}
---@alias CS.UnityEngine.UIElements.StyleComplexSelector.PseudoStateData UnityEngine.UIElements.StyleComplexSelector.PseudoStateData
CS.UnityEngine.UIElements.StyleComplexSelector.PseudoStateData = UnityEngine.UIElements.StyleComplexSelector.PseudoStateData

---@param state UnityEngine.UIElements.PseudoStates
---@param negate boolean
---@return UnityEngine.UIElements.StyleComplexSelector.PseudoStateData
function UnityEngine.UIElements.StyleComplexSelector.PseudoStateData.New(state, negate) end

---@class UnityEngine.UIElements.StyleCursor : System.ValueType
---@field value UnityEngine.UIElements.Cursor
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleCursor = {}
---@alias CS.UnityEngine.UIElements.StyleCursor UnityEngine.UIElements.StyleCursor
CS.UnityEngine.UIElements.StyleCursor = UnityEngine.UIElements.StyleCursor

---@overload fun(v: UnityEngine.UIElements.Cursor) : UnityEngine.UIElements.StyleCursor
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleCursor
function UnityEngine.UIElements.StyleCursor.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleCursor, other: UnityEngine.UIElements.StyleCursor) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleCursor:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleCursor:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleCursor:ToString() end

---@class UnityEngine.UIElements.StyleDataRef : System.ValueType
---@field refCount number
---@field id number
UnityEngine.UIElements.StyleDataRef = {}
---@alias CS.UnityEngine.UIElements.StyleDataRef UnityEngine.UIElements.StyleDataRef
CS.UnityEngine.UIElements.StyleDataRef = UnityEngine.UIElements.StyleDataRef

---@return UnityEngine.UIElements.StyleDataRef
function UnityEngine.UIElements.StyleDataRef.Create() end
---@return UnityEngine.UIElements.StyleDataRef
function UnityEngine.UIElements.StyleDataRef:Acquire() end
function UnityEngine.UIElements.StyleDataRef:Release() end
---@param other UnityEngine.UIElements.StyleDataRef
function UnityEngine.UIElements.StyleDataRef:CopyFrom(other) end
---@return T&
function UnityEngine.UIElements.StyleDataRef:Read() end
---@return T&
function UnityEngine.UIElements.StyleDataRef:Write() end
---@return number
function UnityEngine.UIElements.StyleDataRef:GetHashCode() end
---@overload fun(self: UnityEngine.UIElements.StyleDataRef, other: UnityEngine.UIElements.StyleDataRef) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleDataRef:Equals(obj) end
---@param other UnityEngine.UIElements.StyleDataRef
---@return boolean
function UnityEngine.UIElements.StyleDataRef:ReferenceEquals(other) end

---@class UnityEngine.UIElements.StyleDataRef.RefCounted : System.Object
---@field value T
---@field refCount number
---@field id number
UnityEngine.UIElements.StyleDataRef.RefCounted = {}
---@alias CS.UnityEngine.UIElements.StyleDataRef.RefCounted UnityEngine.UIElements.StyleDataRef.RefCounted
CS.UnityEngine.UIElements.StyleDataRef.RefCounted = UnityEngine.UIElements.StyleDataRef.RefCounted

---@return UnityEngine.UIElements.StyleDataRef.RefCounted
function UnityEngine.UIElements.StyleDataRef.RefCounted.New() end
function UnityEngine.UIElements.StyleDataRef.RefCounted:Acquire() end
function UnityEngine.UIElements.StyleDataRef.RefCounted:Release() end
---@return UnityEngine.UIElements.StyleDataRef.RefCounted
function UnityEngine.UIElements.StyleDataRef.RefCounted:Copy() end

---@class UnityEngine.UIElements.StyleDebug : System.Object
UnityEngine.UIElements.StyleDebug = {}
---@alias CS.UnityEngine.UIElements.StyleDebug UnityEngine.UIElements.StyleDebug
CS.UnityEngine.UIElements.StyleDebug = UnityEngine.UIElements.StyleDebug

---@overload fun(ref_computedStyle: UnityEngine.UIElements.ComputedStyle, id: UnityEngine.UIElements.StyleSheets.StylePropertyId) : System.Object, UnityEngine.UIElements.ComputedStyle
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@param name string
---@return System.Object, UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleDebug.GetComputedStyleValue(ref_computedStyle, name) end
---@overload fun(id: UnityEngine.UIElements.StyleSheets.StylePropertyId) : System.Type
---@param name string
---@return System.Type
function UnityEngine.UIElements.StyleDebug.GetComputedStyleType(name) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return System.Type
function UnityEngine.UIElements.StyleDebug.GetShorthandStyleType(id) end
---@overload fun(style: UnityEngine.UIElements.IStyle, id: UnityEngine.UIElements.StyleSheets.StylePropertyId) : System.Object
---@param style UnityEngine.UIElements.IStyle
---@param name string
---@return System.Object
function UnityEngine.UIElements.StyleDebug.GetInlineStyleValue(style, name) end
---@overload fun(style: UnityEngine.UIElements.IStyle, id: UnityEngine.UIElements.StyleSheets.StylePropertyId, value: System.Object)
---@param style UnityEngine.UIElements.IStyle
---@param name string
---@param value System.Object
function UnityEngine.UIElements.StyleDebug.SetInlineStyleValue(style, name, value) end
---@param style UnityEngine.UIElements.IStyle
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@param keyword UnityEngine.UIElements.StyleKeyword
function UnityEngine.UIElements.StyleDebug.SetInlineKeyword(style, id, keyword) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return System.Collections.Generic.List
function UnityEngine.UIElements.StyleDebug.GetValidKeyword(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@param value System.Object
---@return System.Object
function UnityEngine.UIElements.StyleDebug.ConvertComputedToInlineStyleValue(id, value) end
---@overload fun(id: UnityEngine.UIElements.StyleSheets.StylePropertyId) : System.Type
---@param name string
---@return System.Type
function UnityEngine.UIElements.StyleDebug.GetInlineStyleType(name) end
---@overload fun(id: UnityEngine.UIElements.StyleSheets.StylePropertyId) : string[]
---@param shorthandName string
---@return string[]
function UnityEngine.UIElements.StyleDebug.GetLonghandPropertyNames(shorthandName) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return boolean
function UnityEngine.UIElements.StyleDebug.IsShorthandProperty(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return boolean
function UnityEngine.UIElements.StyleDebug.IsInheritedProperty(id) end
---@return UnityEngine.UIElements.StyleSheets.StylePropertyId[]
function UnityEngine.UIElements.StyleDebug.GetInheritedProperties() end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return boolean
function UnityEngine.UIElements.StyleDebug.IsDiscreteTypeProperty(id) end
---@return string[]
function UnityEngine.UIElements.StyleDebug.GetStylePropertyNames() end
---@param name string
---@return UnityEngine.UIElements.StyleSheets.StylePropertyId
function UnityEngine.UIElements.StyleDebug.GetStylePropertyIdFromName(name) end
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@param matchRecords System.Collections.Generic.IEnumerable
---@param result System.Collections.Generic.Dictionary
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleDebug.FindSpecifiedStyles(ref_computedStyle, matchRecords, result) end

---@class UnityEngine.UIElements.StyleEnum : System.ValueType
---@field value T
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleEnum = {}
---@alias CS.UnityEngine.UIElements.StyleEnum UnityEngine.UIElements.StyleEnum
CS.UnityEngine.UIElements.StyleEnum = UnityEngine.UIElements.StyleEnum

---@overload fun(v: T) : UnityEngine.UIElements.StyleEnum
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleEnum
function UnityEngine.UIElements.StyleEnum.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleEnum, other: UnityEngine.UIElements.StyleEnum) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleEnum:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleEnum:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleEnum:ToString() end

---@class UnityEngine.UIElements.StyleFloat : System.ValueType
---@field value number
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleFloat = {}
---@alias CS.UnityEngine.UIElements.StyleFloat UnityEngine.UIElements.StyleFloat
CS.UnityEngine.UIElements.StyleFloat = UnityEngine.UIElements.StyleFloat

---@overload fun(v: number) : UnityEngine.UIElements.StyleFloat
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleFloat
function UnityEngine.UIElements.StyleFloat.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleFloat, other: UnityEngine.UIElements.StyleFloat) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleFloat:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleFloat:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleFloat:ToString() end

---@class UnityEngine.UIElements.StyleFont : System.ValueType
---@field value UnityEngine.Font
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleFont = {}
---@alias CS.UnityEngine.UIElements.StyleFont UnityEngine.UIElements.StyleFont
CS.UnityEngine.UIElements.StyleFont = UnityEngine.UIElements.StyleFont

---@overload fun(v: UnityEngine.Font) : UnityEngine.UIElements.StyleFont
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleFont
function UnityEngine.UIElements.StyleFont.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleFont, other: UnityEngine.UIElements.StyleFont) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleFont:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleFont:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleFont:ToString() end

---@class UnityEngine.UIElements.StyleFontDefinition : System.ValueType
---@field value UnityEngine.UIElements.FontDefinition
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleFontDefinition = {}
---@alias CS.UnityEngine.UIElements.StyleFontDefinition UnityEngine.UIElements.StyleFontDefinition
CS.UnityEngine.UIElements.StyleFontDefinition = UnityEngine.UIElements.StyleFontDefinition

---@overload fun(f: UnityEngine.UIElements.FontDefinition) : UnityEngine.UIElements.StyleFontDefinition
---@overload fun(f: UnityEngine.TextCore.Text.FontAsset) : UnityEngine.UIElements.StyleFontDefinition
---@overload fun(f: UnityEngine.Font) : UnityEngine.UIElements.StyleFontDefinition
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleFontDefinition
function UnityEngine.UIElements.StyleFontDefinition.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleFontDefinition, other: UnityEngine.UIElements.StyleFontDefinition) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleFontDefinition:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleFontDefinition:GetHashCode() end

---@class UnityEngine.UIElements.StyleInt : System.ValueType
---@field value number
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleInt = {}
---@alias CS.UnityEngine.UIElements.StyleInt UnityEngine.UIElements.StyleInt
CS.UnityEngine.UIElements.StyleInt = UnityEngine.UIElements.StyleInt

---@overload fun(v: number) : UnityEngine.UIElements.StyleInt
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleInt
function UnityEngine.UIElements.StyleInt.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleInt, other: UnityEngine.UIElements.StyleInt) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleInt:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleInt:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleInt:ToString() end

---@class UnityEngine.UIElements.StyleKeyword
---@field Undefined UnityEngine.UIElements.StyleKeyword
---@field Null UnityEngine.UIElements.StyleKeyword
---@field Auto UnityEngine.UIElements.StyleKeyword
---@field None UnityEngine.UIElements.StyleKeyword
---@field Initial UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleKeyword = {}
---@alias CS.UnityEngine.UIElements.StyleKeyword UnityEngine.UIElements.StyleKeyword
CS.UnityEngine.UIElements.StyleKeyword = UnityEngine.UIElements.StyleKeyword


---@class UnityEngine.UIElements.StyleLength : System.ValueType
---@field value UnityEngine.UIElements.Length
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleLength = {}
---@alias CS.UnityEngine.UIElements.StyleLength UnityEngine.UIElements.StyleLength
CS.UnityEngine.UIElements.StyleLength = UnityEngine.UIElements.StyleLength

---@overload fun(v: number) : UnityEngine.UIElements.StyleLength
---@overload fun(v: UnityEngine.UIElements.Length) : UnityEngine.UIElements.StyleLength
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleLength
function UnityEngine.UIElements.StyleLength.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleLength, other: UnityEngine.UIElements.StyleLength) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleLength:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleLength:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleLength:ToString() end

---@class UnityEngine.UIElements.StyleList : System.ValueType
---@field value System.Collections.Generic.List[T]
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleList = {}
---@alias CS.UnityEngine.UIElements.StyleList UnityEngine.UIElements.StyleList
CS.UnityEngine.UIElements.StyleList = UnityEngine.UIElements.StyleList

---@overload fun(v: System.Collections.Generic.List[T]) : UnityEngine.UIElements.StyleList
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleList
function UnityEngine.UIElements.StyleList.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleList, other: UnityEngine.UIElements.StyleList) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleList:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleList:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleList:ToString() end

---@class UnityEngine.UIElements.StyleMatchingContext : System.Object
---@field variableContext UnityEngine.UIElements.StyleVariableContext
---@field currentElement UnityEngine.UIElements.VisualElement
---@field processResult System.Action | function
---@field ancestorFilter UnityEngine.UIElements.AncestorFilter
---@field styleSheetCount number
UnityEngine.UIElements.StyleMatchingContext = {}
---@alias CS.UnityEngine.UIElements.StyleMatchingContext UnityEngine.UIElements.StyleMatchingContext
CS.UnityEngine.UIElements.StyleMatchingContext = UnityEngine.UIElements.StyleMatchingContext

---@param processResult System.Action | function
---@return UnityEngine.UIElements.StyleMatchingContext
function UnityEngine.UIElements.StyleMatchingContext.New(processResult) end
---@param sheet UnityEngine.UIElements.StyleSheet
function UnityEngine.UIElements.StyleMatchingContext:AddStyleSheet(sheet) end
---@param index number
---@param count number
function UnityEngine.UIElements.StyleMatchingContext:RemoveStyleSheetRange(index, count) end
---@param index number
---@return UnityEngine.UIElements.StyleSheet
function UnityEngine.UIElements.StyleMatchingContext:GetStyleSheetAt(index) end

---@class UnityEngine.UIElements.StyleProperty : System.Object
---@field name string
---@field line number
---@field values UnityEngine.UIElements.StyleValueHandle[]
UnityEngine.UIElements.StyleProperty = {}
---@alias CS.UnityEngine.UIElements.StyleProperty UnityEngine.UIElements.StyleProperty
CS.UnityEngine.UIElements.StyleProperty = UnityEngine.UIElements.StyleProperty

---@return UnityEngine.UIElements.StyleProperty
function UnityEngine.UIElements.StyleProperty.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem : System.Object
UnityEngine.UIElements.StylePropertyAnimationSystem = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem UnityEngine.UIElements.StylePropertyAnimationSystem
CS.UnityEngine.UIElements.StylePropertyAnimationSystem = UnityEngine.UIElements.StylePropertyAnimationSystem

---@return UnityEngine.UIElements.StylePropertyAnimationSystem
function UnityEngine.UIElements.StylePropertyAnimationSystem.New() end
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: number, endValue: number, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: number, endValue: number, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.Length, endValue: UnityEngine.UIElements.Length, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.Color, endValue: UnityEngine.Color, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.Background, endValue: UnityEngine.UIElements.Background, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.FontDefinition, endValue: UnityEngine.UIElements.FontDefinition, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.Font, endValue: UnityEngine.Font, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.TextShadow, endValue: UnityEngine.UIElements.TextShadow, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.Scale, endValue: UnityEngine.UIElements.Scale, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.Rotate, endValue: UnityEngine.UIElements.Rotate, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.Translate, endValue: UnityEngine.UIElements.Translate, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.TransformOrigin, endValue: UnityEngine.UIElements.TransformOrigin, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.BackgroundPosition, endValue: UnityEngine.UIElements.BackgroundPosition, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem, owner: UnityEngine.UIElements.VisualElement, prop: UnityEngine.UIElements.StyleSheets.StylePropertyId, startValue: UnityEngine.UIElements.BackgroundRepeat, endValue: UnityEngine.UIElements.BackgroundRepeat, durationMs: number, delayMs: number, easingCurve: System.Func) : boolean
---@param owner UnityEngine.UIElements.VisualElement
---@param prop UnityEngine.UIElements.StyleSheets.StylePropertyId
---@param startValue UnityEngine.UIElements.BackgroundSize
---@param endValue UnityEngine.UIElements.BackgroundSize
---@param durationMs number
---@param delayMs number
---@param easingCurve System.Func
---@return boolean
function UnityEngine.UIElements.StylePropertyAnimationSystem:StartTransition(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve) end
---@param owner UnityEngine.UIElements.VisualElement
---@param prop UnityEngine.UIElements.StyleSheets.StylePropertyId
---@param startValue number
---@param endValue number
---@param durationMs number
---@param delayMs number
---@param easingCurve System.Func
---@return boolean
function UnityEngine.UIElements.StylePropertyAnimationSystem:StartAnimationEnum(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve) end
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem)
---@param owner UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.StylePropertyAnimationSystem:CancelAllAnimations(owner) end
---@param owner UnityEngine.UIElements.VisualElement
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
function UnityEngine.UIElements.StylePropertyAnimationSystem:CancelAnimation(owner, id) end
---@param owner UnityEngine.UIElements.VisualElement
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return boolean
function UnityEngine.UIElements.StylePropertyAnimationSystem:HasRunningAnimation(owner, id) end
---@param owner UnityEngine.UIElements.VisualElement
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
function UnityEngine.UIElements.StylePropertyAnimationSystem:UpdateAnimation(owner, id) end
---@param owner UnityEngine.UIElements.VisualElement
---@param propertyIds System.Collections.Generic.List
function UnityEngine.UIElements.StylePropertyAnimationSystem:GetAllAnimations(owner, propertyIds) end
function UnityEngine.UIElements.StylePropertyAnimationSystem:Update() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet : System.ValueType
---@field elements UnityEngine.UIElements.VisualElement[]
---@field properties UnityEngine.UIElements.StyleSheets.StylePropertyId[]
---@field timing TTimingData[]
---@field style TStyleData[]
---@field count number
UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet = UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet
function UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet.Create() end
---@param ve UnityEngine.UIElements.VisualElement
---@param prop UnityEngine.UIElements.StyleSheets.StylePropertyId
---@param out_index number
---@return boolean, number
function UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet:IndexOf(ve, prop, out_index) end
---@param owner UnityEngine.UIElements.VisualElement
---@param prop UnityEngine.UIElements.StyleSheets.StylePropertyId
---@param timingData TTimingData
---@param styleData TStyleData
function UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet:Add(owner, prop, timingData, styleData) end
---@param cancelledIndex number
function UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet:Remove(cancelledIndex) end
---@param index number
---@param timingData TTimingData
---@param styleData TStyleData
function UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet:Replace(index, timingData, styleData) end
---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet, ve: UnityEngine.UIElements.VisualElement)
function UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet:RemoveAll() end
---@param ve UnityEngine.UIElements.VisualElement
---@param outProperties System.Collections.Generic.List
function UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet:GetActivePropertiesForElement(ve, outProperties) end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair : System.ValueType
---@field Comparer System.Collections.Generic.IEqualityComparer
---@field element UnityEngine.UIElements.VisualElement
---@field property UnityEngine.UIElements.StyleSheets.StylePropertyId
UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair = UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair

---@param element UnityEngine.UIElements.VisualElement
---@param property UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair
function UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.New(element, property) end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer : System.Object
UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer = UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer
function UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer.New() end
---@param x UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair
---@param y UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair
---@return boolean
function UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer:Equals(x, y) end
---@param obj UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair
---@return number
function UnityEngine.UIElements.StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer:GetHashCode(obj) end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState
---@field None UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState
---@field Running UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState
---@field Started UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState
---@field Ended UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState
---@field Canceled UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState
UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState = UnityEngine.UIElements.StylePropertyAnimationSystem.TransitionState


---@class UnityEngine.UIElements.StylePropertyAnimationSystem.Values : System.Object
UnityEngine.UIElements.StylePropertyAnimationSystem.Values = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values UnityEngine.UIElements.StylePropertyAnimationSystem.Values
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values = UnityEngine.UIElements.StylePropertyAnimationSystem.Values

---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem.Values)
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:CancelAllAnimations(ve) end
---@param ve UnityEngine.UIElements.VisualElement
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:CancelAnimation(ve, id) end
---@param ve UnityEngine.UIElements.VisualElement
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return boolean
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:HasRunningAnimation(ve, id) end
---@param ve UnityEngine.UIElements.VisualElement
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:UpdateAnimation(ve, id) end
---@param ve UnityEngine.UIElements.VisualElement
---@param outPropertyIds System.Collections.Generic.List
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:GetAllAnimations(ve, outPropertyIds) end
---@param currentTimeMs number
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:Update(currentTimeMs) end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.Values : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field running UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet[UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TimingData[T],UnityEngine.UIElements.StylePropertyAnimationSystem.Values.StyleData[T]]
---@field completed UnityEngine.UIElements.StylePropertyAnimationSystem.AnimationDataSet[UnityEngine.UIElements.StylePropertyAnimationSystem.Values.EmptyData[T],T]
---@field isEmpty boolean
---@field SameFunc System.Func[T,T,System.Boolean]
UnityEngine.UIElements.StylePropertyAnimationSystem.Values = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values UnityEngine.UIElements.StylePropertyAnimationSystem.Values
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values = UnityEngine.UIElements.StylePropertyAnimationSystem.Values

---@overload fun(self: UnityEngine.UIElements.StylePropertyAnimationSystem.Values)
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:CancelAllAnimations(ve) end
---@param ve UnityEngine.UIElements.VisualElement
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:CancelAnimation(ve, id) end
---@param ve UnityEngine.UIElements.VisualElement
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return boolean
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:HasRunningAnimation(ve, id) end
---@param ve UnityEngine.UIElements.VisualElement
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:UpdateAnimation(ve, id) end
---@param ve UnityEngine.UIElements.VisualElement
---@param outPropertyIds System.Collections.Generic.List
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:GetAllAnimations(ve, outPropertyIds) end
---@param owner UnityEngine.UIElements.VisualElement
---@param prop UnityEngine.UIElements.StyleSheets.StylePropertyId
---@param startValue T
---@param endValue T
---@param durationMs number
---@param delayMs number
---@param easingCurve System.Func
---@param currentTimeMs number
---@return boolean
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:StartTransition(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, currentTimeMs) end
---@param currentTimeMs number
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values:Update(currentTimeMs) end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.Values.EmptyData : System.ValueType
---@field Default UnityEngine.UIElements.StylePropertyAnimationSystem.Values.EmptyData
UnityEngine.UIElements.StylePropertyAnimationSystem.Values.EmptyData = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values.EmptyData UnityEngine.UIElements.StylePropertyAnimationSystem.Values.EmptyData
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values.EmptyData = UnityEngine.UIElements.StylePropertyAnimationSystem.Values.EmptyData


---@class UnityEngine.UIElements.StylePropertyAnimationSystem.Values.StyleData : System.ValueType
---@field startValue T
---@field endValue T
---@field reversingAdjustedStartValue T
---@field currentValue T
UnityEngine.UIElements.StylePropertyAnimationSystem.Values.StyleData = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values.StyleData UnityEngine.UIElements.StylePropertyAnimationSystem.Values.StyleData
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values.StyleData = UnityEngine.UIElements.StylePropertyAnimationSystem.Values.StyleData


---@class UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TimingData : System.ValueType
---@field startTimeMs number
---@field durationMs number
---@field easingCurve System.Func
---@field easedProgress number
---@field reversingShorteningFactor number
---@field isStarted boolean
---@field delayMs number
UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TimingData = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TimingData UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TimingData
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TimingData = UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TimingData


---@class UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState : System.Object
---@field elementPropertyStateDelta System.Collections.Generic.Dictionary
---@field elementPropertyQueuedEvents System.Collections.Generic.Dictionary
---@field panel UnityEngine.UIElements.IPanel
UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState = UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState.New() end
---@return System.Collections.Generic.Queue
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState.GetPooledQueue() end
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState:RegisterChange() end
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState:UnregisterChange() end
---@return boolean
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState:StateChanged() end
function UnityEngine.UIElements.StylePropertyAnimationSystem.Values.TransitionEventsFrameState:Clear() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackground : UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackground = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackground UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackground
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackground = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackground

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackground
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackground.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundPosition : UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundPosition = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundPosition UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundPosition
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundPosition = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundPosition

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundPosition
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundPosition.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundRepeat : UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundRepeat = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundRepeat UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundRepeat
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundRepeat = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundRepeat

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundRepeat
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundRepeat.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundSize : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundSize = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundSize UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundSize
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundSize = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundSize

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundSize
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesBackgroundSize.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesColor : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesColor = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesColor UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesColor
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesColor = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesColor

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesColor
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesColor.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete : UnityEngine.UIElements.StylePropertyAnimationSystem.Values[T]
---@field SameFunc System.Func[T,T,System.Boolean]
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete


---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesEnum : UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesEnum = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesEnum UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesEnum
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesEnum = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesEnum

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesEnum
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesEnum.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFloat : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFloat = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFloat UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFloat
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFloat = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFloat

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFloat
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFloat.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFont : UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFont = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFont UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFont
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFont = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFont

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFont
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFont.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFontDefinition : UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesDiscrete
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFontDefinition = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFontDefinition UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFontDefinition
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFontDefinition = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFontDefinition

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFontDefinition
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesFontDefinition.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesInt : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesInt = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesInt UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesInt
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesInt = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesInt

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesInt
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesInt.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesLength : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesLength = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesLength UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesLength
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesLength = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesLength

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesLength
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesLength.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesRotate : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesRotate = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesRotate UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesRotate
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesRotate = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesRotate

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesRotate
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesRotate.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesScale : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesScale = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesScale UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesScale
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesScale = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesScale

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesScale
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesScale.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTextShadow : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTextShadow = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTextShadow UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTextShadow
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTextShadow = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTextShadow

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTextShadow
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTextShadow.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTransformOrigin : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTransformOrigin = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTransformOrigin UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTransformOrigin
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTransformOrigin = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTransformOrigin

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTransformOrigin
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTransformOrigin.New() end

---@class UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTranslate : UnityEngine.UIElements.StylePropertyAnimationSystem.Values
---@field SameFunc System.Func
UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTranslate = {}
---@alias CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTranslate UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTranslate
CS.UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTranslate = UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTranslate

---@return UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTranslate
function UnityEngine.UIElements.StylePropertyAnimationSystem.ValuesTranslate.New() end

---@class UnityEngine.UIElements.StylePropertyName : System.ValueType
UnityEngine.UIElements.StylePropertyName = {}
---@alias CS.UnityEngine.UIElements.StylePropertyName UnityEngine.UIElements.StylePropertyName
CS.UnityEngine.UIElements.StylePropertyName = UnityEngine.UIElements.StylePropertyName

---@param name string
---@return UnityEngine.UIElements.StylePropertyName
function UnityEngine.UIElements.StylePropertyName.New(name) end
---@param propertyName UnityEngine.UIElements.StylePropertyName
---@return boolean
function UnityEngine.UIElements.StylePropertyName.IsNullOrEmpty(propertyName) end
---@return number
function UnityEngine.UIElements.StylePropertyName:GetHashCode() end
---@overload fun(self: UnityEngine.UIElements.StylePropertyName, other: System.Object) : boolean
---@param other UnityEngine.UIElements.StylePropertyName
---@return boolean
function UnityEngine.UIElements.StylePropertyName:Equals(other) end
---@return string
function UnityEngine.UIElements.StylePropertyName:ToString() end

---@class UnityEngine.UIElements.StylePropertyNameCollection : System.ValueType
UnityEngine.UIElements.StylePropertyNameCollection = {}
---@alias CS.UnityEngine.UIElements.StylePropertyNameCollection UnityEngine.UIElements.StylePropertyNameCollection
CS.UnityEngine.UIElements.StylePropertyNameCollection = UnityEngine.UIElements.StylePropertyNameCollection

---@return UnityEngine.UIElements.StylePropertyNameCollection.Enumerator
function UnityEngine.UIElements.StylePropertyNameCollection:GetEnumerator() end
---@param stylePropertyName UnityEngine.UIElements.StylePropertyName
---@return boolean
function UnityEngine.UIElements.StylePropertyNameCollection:Contains(stylePropertyName) end

---@class UnityEngine.UIElements.StylePropertyNameCollection.Enumerator : System.ValueType
---@field Current UnityEngine.UIElements.StylePropertyName
UnityEngine.UIElements.StylePropertyNameCollection.Enumerator = {}
---@alias CS.UnityEngine.UIElements.StylePropertyNameCollection.Enumerator UnityEngine.UIElements.StylePropertyNameCollection.Enumerator
CS.UnityEngine.UIElements.StylePropertyNameCollection.Enumerator = UnityEngine.UIElements.StylePropertyNameCollection.Enumerator

---@return boolean
function UnityEngine.UIElements.StylePropertyNameCollection.Enumerator:MoveNext() end
function UnityEngine.UIElements.StylePropertyNameCollection.Enumerator:Reset() end
function UnityEngine.UIElements.StylePropertyNameCollection.Enumerator:Dispose() end

---@class UnityEngine.UIElements.StyleRotate : System.ValueType
---@field value UnityEngine.UIElements.Rotate
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleRotate = {}
---@alias CS.UnityEngine.UIElements.StyleRotate UnityEngine.UIElements.StyleRotate
CS.UnityEngine.UIElements.StyleRotate = UnityEngine.UIElements.StyleRotate

---@overload fun(v: UnityEngine.UIElements.Rotate) : UnityEngine.UIElements.StyleRotate
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleRotate
function UnityEngine.UIElements.StyleRotate.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleRotate, other: UnityEngine.UIElements.StyleRotate) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleRotate:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleRotate:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleRotate:ToString() end

---@class UnityEngine.UIElements.StyleRule : System.Object
---@field properties UnityEngine.UIElements.StyleProperty[]
UnityEngine.UIElements.StyleRule = {}
---@alias CS.UnityEngine.UIElements.StyleRule UnityEngine.UIElements.StyleRule
CS.UnityEngine.UIElements.StyleRule = UnityEngine.UIElements.StyleRule

---@return UnityEngine.UIElements.StyleRule
function UnityEngine.UIElements.StyleRule.New() end

---@class UnityEngine.UIElements.StyleScale : System.ValueType
---@field value UnityEngine.UIElements.Scale
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleScale = {}
---@alias CS.UnityEngine.UIElements.StyleScale UnityEngine.UIElements.StyleScale
CS.UnityEngine.UIElements.StyleScale = UnityEngine.UIElements.StyleScale

---@overload fun(v: UnityEngine.UIElements.Scale) : UnityEngine.UIElements.StyleScale
---@overload fun(keyword: UnityEngine.UIElements.StyleKeyword) : UnityEngine.UIElements.StyleScale
---@param scale UnityEngine.Vector2
---@return UnityEngine.UIElements.StyleScale
function UnityEngine.UIElements.StyleScale.New(scale) end
---@overload fun(self: UnityEngine.UIElements.StyleScale, other: UnityEngine.UIElements.StyleScale) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleScale:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleScale:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleScale:ToString() end

---@class UnityEngine.UIElements.StyleSelector : System.Object
---@field parts UnityEngine.UIElements.StyleSelectorPart[]
---@field previousRelationship UnityEngine.UIElements.StyleSelectorRelationship
UnityEngine.UIElements.StyleSelector = {}
---@alias CS.UnityEngine.UIElements.StyleSelector UnityEngine.UIElements.StyleSelector
CS.UnityEngine.UIElements.StyleSelector = UnityEngine.UIElements.StyleSelector

---@return UnityEngine.UIElements.StyleSelector
function UnityEngine.UIElements.StyleSelector.New() end
---@return string
function UnityEngine.UIElements.StyleSelector:ToString() end

---@class UnityEngine.UIElements.StyleSelectorPart : System.ValueType
---@field value string
---@field type UnityEngine.UIElements.StyleSelectorType
UnityEngine.UIElements.StyleSelectorPart = {}
---@alias CS.UnityEngine.UIElements.StyleSelectorPart UnityEngine.UIElements.StyleSelectorPart
CS.UnityEngine.UIElements.StyleSelectorPart = UnityEngine.UIElements.StyleSelectorPart

---@param className string
---@return UnityEngine.UIElements.StyleSelectorPart
function UnityEngine.UIElements.StyleSelectorPart.CreateClass(className) end
---@param className string
---@return UnityEngine.UIElements.StyleSelectorPart
function UnityEngine.UIElements.StyleSelectorPart.CreatePseudoClass(className) end
---@param Id string
---@return UnityEngine.UIElements.StyleSelectorPart
function UnityEngine.UIElements.StyleSelectorPart.CreateId(Id) end
---@overload fun(t: System.Type) : UnityEngine.UIElements.StyleSelectorPart
---@param typeName string
---@return UnityEngine.UIElements.StyleSelectorPart
function UnityEngine.UIElements.StyleSelectorPart.CreateType(typeName) end
---@param predicate System.Object
---@return UnityEngine.UIElements.StyleSelectorPart
function UnityEngine.UIElements.StyleSelectorPart.CreatePredicate(predicate) end
---@return UnityEngine.UIElements.StyleSelectorPart
function UnityEngine.UIElements.StyleSelectorPart.CreateWildCard() end
---@return string
function UnityEngine.UIElements.StyleSelectorPart:ToString() end

---@class UnityEngine.UIElements.StyleSelectorRelationship
---@field None UnityEngine.UIElements.StyleSelectorRelationship
---@field Child UnityEngine.UIElements.StyleSelectorRelationship
---@field Descendent UnityEngine.UIElements.StyleSelectorRelationship
UnityEngine.UIElements.StyleSelectorRelationship = {}
---@alias CS.UnityEngine.UIElements.StyleSelectorRelationship UnityEngine.UIElements.StyleSelectorRelationship
CS.UnityEngine.UIElements.StyleSelectorRelationship = UnityEngine.UIElements.StyleSelectorRelationship


---@class UnityEngine.UIElements.StyleSelectorType
---@field Unknown UnityEngine.UIElements.StyleSelectorType
---@field Wildcard UnityEngine.UIElements.StyleSelectorType
---@field Type UnityEngine.UIElements.StyleSelectorType
---@field Class UnityEngine.UIElements.StyleSelectorType
---@field PseudoClass UnityEngine.UIElements.StyleSelectorType
---@field RecursivePseudoClass UnityEngine.UIElements.StyleSelectorType
---@field ID UnityEngine.UIElements.StyleSelectorType
---@field Predicate UnityEngine.UIElements.StyleSelectorType
UnityEngine.UIElements.StyleSelectorType = {}
---@alias CS.UnityEngine.UIElements.StyleSelectorType UnityEngine.UIElements.StyleSelectorType
CS.UnityEngine.UIElements.StyleSelectorType = UnityEngine.UIElements.StyleSelectorType


---@class UnityEngine.UIElements.StyleSheet : UnityEngine.ScriptableObject
---@field importedWithErrors boolean
---@field importedWithWarnings boolean
---@field contentHash number
UnityEngine.UIElements.StyleSheet = {}
---@alias CS.UnityEngine.UIElements.StyleSheet UnityEngine.UIElements.StyleSheet
CS.UnityEngine.UIElements.StyleSheet = UnityEngine.UIElements.StyleSheet

---@return UnityEngine.UIElements.StyleSheet
function UnityEngine.UIElements.StyleSheet.New() end
---@param handle UnityEngine.UIElements.StyleValueHandle
---@return string
function UnityEngine.UIElements.StyleSheet:ReadAsString(handle) end

---@class UnityEngine.UIElements.StyleSheet.ImportStruct : System.ValueType
---@field styleSheet UnityEngine.UIElements.StyleSheet
---@field mediaQueries string[]
UnityEngine.UIElements.StyleSheet.ImportStruct = {}
---@alias CS.UnityEngine.UIElements.StyleSheet.ImportStruct UnityEngine.UIElements.StyleSheet.ImportStruct
CS.UnityEngine.UIElements.StyleSheet.ImportStruct = UnityEngine.UIElements.StyleSheet.ImportStruct


---@class UnityEngine.UIElements.StyleSheets.BaseStyleMatcher : System.Object
---@field valueCount number
---@field isCurrentVariable boolean
---@field isCurrentComma boolean
---@field hasCurrent boolean
---@field currentIndex number
---@field matchedVariableCount number
UnityEngine.UIElements.StyleSheets.BaseStyleMatcher = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.BaseStyleMatcher UnityEngine.UIElements.StyleSheets.BaseStyleMatcher
CS.UnityEngine.UIElements.StyleSheets.BaseStyleMatcher = UnityEngine.UIElements.StyleSheets.BaseStyleMatcher

function UnityEngine.UIElements.StyleSheets.BaseStyleMatcher:MoveNext() end
function UnityEngine.UIElements.StyleSheets.BaseStyleMatcher:SaveContext() end
function UnityEngine.UIElements.StyleSheets.BaseStyleMatcher:RestoreContext() end
function UnityEngine.UIElements.StyleSheets.BaseStyleMatcher:DropContext() end

---@class UnityEngine.UIElements.StyleSheets.BaseStyleMatcher.MatchContext : System.ValueType
---@field valueIndex number
---@field matchedVariableCount number
UnityEngine.UIElements.StyleSheets.BaseStyleMatcher.MatchContext = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.BaseStyleMatcher.MatchContext UnityEngine.UIElements.StyleSheets.BaseStyleMatcher.MatchContext
CS.UnityEngine.UIElements.StyleSheets.BaseStyleMatcher.MatchContext = UnityEngine.UIElements.StyleSheets.BaseStyleMatcher.MatchContext


---@class UnityEngine.UIElements.StyleSheets.CSSSpec : System.Object
UnityEngine.UIElements.StyleSheets.CSSSpec = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.CSSSpec UnityEngine.UIElements.StyleSheets.CSSSpec
CS.UnityEngine.UIElements.StyleSheets.CSSSpec = UnityEngine.UIElements.StyleSheets.CSSSpec

---@overload fun(selector: string) : number
---@param parts UnityEngine.UIElements.StyleSelectorPart[]
---@return number
function UnityEngine.UIElements.StyleSheets.CSSSpec.GetSelectorSpecificity(parts) end
---@param selector string
---@param out_parts UnityEngine.UIElements.StyleSelectorPart[]
---@return boolean, UnityEngine.UIElements.StyleSelectorPart[]
function UnityEngine.UIElements.StyleSheets.CSSSpec.ParseSelector(selector, out_parts) end

---@class UnityEngine.UIElements.StyleSheets.Dimension : System.ValueType
---@field unit UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field value number
UnityEngine.UIElements.StyleSheets.Dimension = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Dimension UnityEngine.UIElements.StyleSheets.Dimension
CS.UnityEngine.UIElements.StyleSheets.Dimension = UnityEngine.UIElements.StyleSheets.Dimension

---@param value number
---@param unit UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@return UnityEngine.UIElements.StyleSheets.Dimension
function UnityEngine.UIElements.StyleSheets.Dimension.New(value, unit) end
---@return UnityEngine.UIElements.Length
function UnityEngine.UIElements.StyleSheets.Dimension:ToLength() end
---@return UnityEngine.UIElements.TimeValue
function UnityEngine.UIElements.StyleSheets.Dimension:ToTime() end
---@return UnityEngine.UIElements.Angle
function UnityEngine.UIElements.StyleSheets.Dimension:ToAngle() end
---@overload fun(self: UnityEngine.UIElements.StyleSheets.Dimension, other: UnityEngine.UIElements.StyleSheets.Dimension) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleSheets.Dimension:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleSheets.Dimension:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleSheets.Dimension:ToString() end

---@class UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field Unitless UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field Pixel UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field Percent UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field Second UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field Millisecond UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field Degree UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field Gradian UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field Radian UnityEngine.UIElements.StyleSheets.Dimension.Unit
---@field Turn UnityEngine.UIElements.StyleSheets.Dimension.Unit
UnityEngine.UIElements.StyleSheets.Dimension.Unit = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Dimension.Unit UnityEngine.UIElements.StyleSheets.Dimension.Unit
CS.UnityEngine.UIElements.StyleSheets.Dimension.Unit = UnityEngine.UIElements.StyleSheets.Dimension.Unit


---@class UnityEngine.UIElements.StyleSheets.HierarchyTraversal : System.Object
UnityEngine.UIElements.StyleSheets.HierarchyTraversal = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.HierarchyTraversal UnityEngine.UIElements.StyleSheets.HierarchyTraversal
CS.UnityEngine.UIElements.StyleSheets.HierarchyTraversal = UnityEngine.UIElements.StyleSheets.HierarchyTraversal

---@param element UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.StyleSheets.HierarchyTraversal:Traverse(element) end
---@param element UnityEngine.UIElements.VisualElement
---@param depth number
function UnityEngine.UIElements.StyleSheets.HierarchyTraversal:TraverseRecursive(element, depth) end

---@class UnityEngine.UIElements.StyleSheets.ImageSource : System.ValueType
---@field texture UnityEngine.Texture2D
---@field sprite UnityEngine.Sprite
---@field vectorImage UnityEngine.UIElements.VectorImage
---@field renderTexture UnityEngine.RenderTexture
UnityEngine.UIElements.StyleSheets.ImageSource = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.ImageSource UnityEngine.UIElements.StyleSheets.ImageSource
CS.UnityEngine.UIElements.StyleSheets.ImageSource = UnityEngine.UIElements.StyleSheets.ImageSource

---@return boolean
function UnityEngine.UIElements.StyleSheets.ImageSource:IsNull() end

---@class UnityEngine.UIElements.StyleSheets.InitialStyle : System.Object
---@field alignContent UnityEngine.UIElements.Align
---@field alignItems UnityEngine.UIElements.Align
---@field alignSelf UnityEngine.UIElements.Align
---@field backgroundColor UnityEngine.Color
---@field backgroundImage UnityEngine.UIElements.Background
---@field backgroundPositionX UnityEngine.UIElements.BackgroundPosition
---@field backgroundPositionY UnityEngine.UIElements.BackgroundPosition
---@field backgroundRepeat UnityEngine.UIElements.BackgroundRepeat
---@field backgroundSize UnityEngine.UIElements.BackgroundSize
---@field borderBottomColor UnityEngine.Color
---@field borderBottomLeftRadius UnityEngine.UIElements.Length
---@field borderBottomRightRadius UnityEngine.UIElements.Length
---@field borderBottomWidth number
---@field borderLeftColor UnityEngine.Color
---@field borderLeftWidth number
---@field borderRightColor UnityEngine.Color
---@field borderRightWidth number
---@field borderTopColor UnityEngine.Color
---@field borderTopLeftRadius UnityEngine.UIElements.Length
---@field borderTopRightRadius UnityEngine.UIElements.Length
---@field borderTopWidth number
---@field bottom UnityEngine.UIElements.Length
---@field color UnityEngine.Color
---@field cursor UnityEngine.UIElements.Cursor
---@field display UnityEngine.UIElements.DisplayStyle
---@field flexBasis UnityEngine.UIElements.Length
---@field flexDirection UnityEngine.UIElements.FlexDirection
---@field flexGrow number
---@field flexShrink number
---@field flexWrap UnityEngine.UIElements.Wrap
---@field fontSize UnityEngine.UIElements.Length
---@field height UnityEngine.UIElements.Length
---@field justifyContent UnityEngine.UIElements.Justify
---@field left UnityEngine.UIElements.Length
---@field letterSpacing UnityEngine.UIElements.Length
---@field marginBottom UnityEngine.UIElements.Length
---@field marginLeft UnityEngine.UIElements.Length
---@field marginRight UnityEngine.UIElements.Length
---@field marginTop UnityEngine.UIElements.Length
---@field maxHeight UnityEngine.UIElements.Length
---@field maxWidth UnityEngine.UIElements.Length
---@field minHeight UnityEngine.UIElements.Length
---@field minWidth UnityEngine.UIElements.Length
---@field opacity number
---@field overflow UnityEngine.UIElements.OverflowInternal
---@field paddingBottom UnityEngine.UIElements.Length
---@field paddingLeft UnityEngine.UIElements.Length
---@field paddingRight UnityEngine.UIElements.Length
---@field paddingTop UnityEngine.UIElements.Length
---@field position UnityEngine.UIElements.Position
---@field right UnityEngine.UIElements.Length
---@field rotate UnityEngine.UIElements.Rotate
---@field scale UnityEngine.UIElements.Scale
---@field textOverflow UnityEngine.UIElements.TextOverflow
---@field textShadow UnityEngine.UIElements.TextShadow
---@field top UnityEngine.UIElements.Length
---@field transformOrigin UnityEngine.UIElements.TransformOrigin
---@field transitionDelay System.Collections.Generic.List
---@field transitionDuration System.Collections.Generic.List
---@field transitionProperty System.Collections.Generic.List
---@field transitionTimingFunction System.Collections.Generic.List
---@field translate UnityEngine.UIElements.Translate
---@field unityBackgroundImageTintColor UnityEngine.Color
---@field unityFont UnityEngine.Font
---@field unityFontDefinition UnityEngine.UIElements.FontDefinition
---@field unityFontStyleAndWeight UnityEngine.FontStyle
---@field unityOverflowClipBox UnityEngine.UIElements.OverflowClipBox
---@field unityParagraphSpacing UnityEngine.UIElements.Length
---@field unitySliceBottom number
---@field unitySliceLeft number
---@field unitySliceRight number
---@field unitySliceScale number
---@field unitySliceTop number
---@field unityTextAlign UnityEngine.TextAnchor
---@field unityTextOutlineColor UnityEngine.Color
---@field unityTextOutlineWidth number
---@field unityTextOverflowPosition UnityEngine.UIElements.TextOverflowPosition
---@field visibility UnityEngine.UIElements.Visibility
---@field whiteSpace UnityEngine.UIElements.WhiteSpace
---@field width UnityEngine.UIElements.Length
---@field wordSpacing UnityEngine.UIElements.Length
UnityEngine.UIElements.StyleSheets.InitialStyle = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.InitialStyle UnityEngine.UIElements.StyleSheets.InitialStyle
CS.UnityEngine.UIElements.StyleSheets.InitialStyle = UnityEngine.UIElements.StyleSheets.InitialStyle

---@return UnityEngine.UIElements.ComputedStyle&
function UnityEngine.UIElements.StyleSheets.InitialStyle.Get() end
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.InitialStyle.Acquire() end

---@class UnityEngine.UIElements.StyleSheets.MatchResult : System.ValueType
---@field errorCode UnityEngine.UIElements.StyleSheets.MatchResultErrorCode
---@field errorValue string
---@field success boolean
UnityEngine.UIElements.StyleSheets.MatchResult = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.MatchResult UnityEngine.UIElements.StyleSheets.MatchResult
CS.UnityEngine.UIElements.StyleSheets.MatchResult = UnityEngine.UIElements.StyleSheets.MatchResult


---@class UnityEngine.UIElements.StyleSheets.MatchResultErrorCode
---@field None UnityEngine.UIElements.StyleSheets.MatchResultErrorCode
---@field Syntax UnityEngine.UIElements.StyleSheets.MatchResultErrorCode
---@field EmptyValue UnityEngine.UIElements.StyleSheets.MatchResultErrorCode
---@field ExpectedEndOfValue UnityEngine.UIElements.StyleSheets.MatchResultErrorCode
UnityEngine.UIElements.StyleSheets.MatchResultErrorCode = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.MatchResultErrorCode UnityEngine.UIElements.StyleSheets.MatchResultErrorCode
CS.UnityEngine.UIElements.StyleSheets.MatchResultErrorCode = UnityEngine.UIElements.StyleSheets.MatchResultErrorCode


---@class UnityEngine.UIElements.StyleSheets.MatchResultInfo : System.ValueType
---@field success boolean
---@field triggerPseudoMask UnityEngine.UIElements.PseudoStates
---@field dependencyPseudoMask UnityEngine.UIElements.PseudoStates
UnityEngine.UIElements.StyleSheets.MatchResultInfo = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.MatchResultInfo UnityEngine.UIElements.StyleSheets.MatchResultInfo
CS.UnityEngine.UIElements.StyleSheets.MatchResultInfo = UnityEngine.UIElements.StyleSheets.MatchResultInfo

---@param success boolean
---@param triggerPseudoMask UnityEngine.UIElements.PseudoStates
---@param dependencyPseudoMask UnityEngine.UIElements.PseudoStates
---@return UnityEngine.UIElements.StyleSheets.MatchResultInfo
function UnityEngine.UIElements.StyleSheets.MatchResultInfo.New(success, triggerPseudoMask, dependencyPseudoMask) end

---@class UnityEngine.UIElements.StyleSheets.ScalableImage : System.ValueType
---@field normalImage UnityEngine.Texture2D
---@field highResolutionImage UnityEngine.Texture2D
UnityEngine.UIElements.StyleSheets.ScalableImage = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.ScalableImage UnityEngine.UIElements.StyleSheets.ScalableImage
CS.UnityEngine.UIElements.StyleSheets.ScalableImage = UnityEngine.UIElements.StyleSheets.ScalableImage

---@return string
function UnityEngine.UIElements.StyleSheets.ScalableImage:ToString() end

---@class UnityEngine.UIElements.StyleSheets.SelectorMatchRecord : System.ValueType
---@field sheet UnityEngine.UIElements.StyleSheet
---@field styleSheetIndexInStack number
---@field complexSelector UnityEngine.UIElements.StyleComplexSelector
UnityEngine.UIElements.StyleSheets.SelectorMatchRecord = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.SelectorMatchRecord UnityEngine.UIElements.StyleSheets.SelectorMatchRecord
CS.UnityEngine.UIElements.StyleSheets.SelectorMatchRecord = UnityEngine.UIElements.StyleSheets.SelectorMatchRecord

---@param sheet UnityEngine.UIElements.StyleSheet
---@param styleSheetIndexInStack number
---@return UnityEngine.UIElements.StyleSheets.SelectorMatchRecord
function UnityEngine.UIElements.StyleSheets.SelectorMatchRecord.New(sheet, styleSheetIndexInStack) end
---@param a UnityEngine.UIElements.StyleSheets.SelectorMatchRecord
---@param b UnityEngine.UIElements.StyleSheets.SelectorMatchRecord
---@return number
function UnityEngine.UIElements.StyleSheets.SelectorMatchRecord.Compare(a, b) end

---@class UnityEngine.UIElements.StyleSheets.ShorthandApplicator : System.Object
UnityEngine.UIElements.StyleSheets.ShorthandApplicator = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.ShorthandApplicator UnityEngine.UIElements.StyleSheets.ShorthandApplicator
CS.UnityEngine.UIElements.StyleSheets.ShorthandApplicator = UnityEngine.UIElements.StyleSheets.ShorthandApplicator

---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyBackgroundPosition(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyBorderColor(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyBorderRadius(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyBorderWidth(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyFlex(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyMargin(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyPadding(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyTransition(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyUnityBackgroundScaleMode(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param ref_computedStyle UnityEngine.UIElements.ComputedStyle
---@return UnityEngine.UIElements.ComputedStyle
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.ApplyUnityTextOutline(reader, ref_computedStyle) end
---@param reader UnityEngine.UIElements.StyleSheets.StylePropertyReader
---@param out_backgroundPositionX UnityEngine.UIElements.BackgroundPosition
---@param out_backgroundPositionY UnityEngine.UIElements.BackgroundPosition
---@param out_backgroundRepeat UnityEngine.UIElements.BackgroundRepeat
---@param out_backgroundSize UnityEngine.UIElements.BackgroundSize
---@return UnityEngine.UIElements.BackgroundPosition, UnityEngine.UIElements.BackgroundPosition, UnityEngine.UIElements.BackgroundRepeat, UnityEngine.UIElements.BackgroundSize
function UnityEngine.UIElements.StyleSheets.ShorthandApplicator.CompileUnityBackgroundScaleMode(reader, out_backgroundPositionX, out_backgroundPositionY, out_backgroundRepeat, out_backgroundSize) end

---@class UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field Align UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field BackgroundPositionKeyword UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field BackgroundSizeType UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field DisplayStyle UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field EasingMode UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field FlexDirection UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field FontStyle UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field Justify UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field Overflow UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field OverflowClipBox UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field OverflowInternal UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field Position UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field Repeat UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field RepeatXY UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field ScaleMode UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field TextAnchor UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field TextOverflow UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field TextOverflowPosition UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field TransformOriginOffset UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field Visibility UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field WhiteSpace UnityEngine.UIElements.StyleSheets.StyleEnumType
---@field Wrap UnityEngine.UIElements.StyleSheets.StyleEnumType
UnityEngine.UIElements.StyleSheets.StyleEnumType = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleEnumType UnityEngine.UIElements.StyleSheets.StyleEnumType
CS.UnityEngine.UIElements.StyleSheets.StyleEnumType = UnityEngine.UIElements.StyleSheets.StyleEnumType


---@class UnityEngine.UIElements.StyleSheets.StyleMatcher : UnityEngine.UIElements.StyleSheets.BaseStyleMatcher
---@field valueCount number
---@field isCurrentVariable boolean
---@field isCurrentComma boolean
UnityEngine.UIElements.StyleSheets.StyleMatcher = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleMatcher UnityEngine.UIElements.StyleSheets.StyleMatcher
CS.UnityEngine.UIElements.StyleSheets.StyleMatcher = UnityEngine.UIElements.StyleSheets.StyleMatcher

---@return UnityEngine.UIElements.StyleSheets.StyleMatcher
function UnityEngine.UIElements.StyleSheets.StyleMatcher.New() end
---@param exp UnityEngine.UIElements.StyleSheets.Syntax.Expression
---@param propertyValue string
---@return UnityEngine.UIElements.StyleSheets.MatchResult
function UnityEngine.UIElements.StyleSheets.StyleMatcher:Match(exp, propertyValue) end

---@class UnityEngine.UIElements.StyleSheets.StylePropertyCache : System.Object
UnityEngine.UIElements.StyleSheets.StylePropertyCache = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StylePropertyCache UnityEngine.UIElements.StyleSheets.StylePropertyCache
CS.UnityEngine.UIElements.StyleSheets.StylePropertyCache = UnityEngine.UIElements.StyleSheets.StylePropertyCache

---@param name string
---@param out_syntax string
---@return boolean, string
function UnityEngine.UIElements.StyleSheets.StylePropertyCache.TryGetSyntax(name, out_syntax) end
---@param name string
---@param out_syntax string
---@return boolean, string
function UnityEngine.UIElements.StyleSheets.StylePropertyCache.TryGetNonTerminalValue(name, out_syntax) end
---@param name string
---@return string
function UnityEngine.UIElements.StyleSheets.StylePropertyCache.FindClosestPropertyName(name) end

---@class UnityEngine.UIElements.StyleSheets.StylePropertyGroup
---@field Inherited UnityEngine.UIElements.StyleSheets.StylePropertyGroup
---@field Layout UnityEngine.UIElements.StyleSheets.StylePropertyGroup
---@field Rare UnityEngine.UIElements.StyleSheets.StylePropertyGroup
---@field Shorthand UnityEngine.UIElements.StyleSheets.StylePropertyGroup
---@field Transform UnityEngine.UIElements.StyleSheets.StylePropertyGroup
---@field Transition UnityEngine.UIElements.StyleSheets.StylePropertyGroup
---@field Visual UnityEngine.UIElements.StyleSheets.StylePropertyGroup
UnityEngine.UIElements.StyleSheets.StylePropertyGroup = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StylePropertyGroup UnityEngine.UIElements.StyleSheets.StylePropertyGroup
CS.UnityEngine.UIElements.StyleSheets.StylePropertyGroup = UnityEngine.UIElements.StyleSheets.StylePropertyGroup


---@class UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Unknown UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Custom UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field AlignContent UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field AlignItems UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field AlignSelf UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field All UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BackgroundColor UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BackgroundImage UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BackgroundPosition UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BackgroundPositionX UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BackgroundPositionY UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BackgroundRepeat UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BackgroundSize UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderBottomColor UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderBottomLeftRadius UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderBottomRightRadius UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderBottomWidth UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderColor UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderLeftColor UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderLeftWidth UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderRadius UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderRightColor UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderRightWidth UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderTopColor UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderTopLeftRadius UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderTopRightRadius UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderTopWidth UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field BorderWidth UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Bottom UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Color UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Cursor UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Display UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Flex UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field FlexBasis UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field FlexDirection UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field FlexGrow UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field FlexShrink UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field FlexWrap UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field FontSize UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Height UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field JustifyContent UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Left UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field LetterSpacing UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Margin UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field MarginBottom UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field MarginLeft UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field MarginRight UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field MarginTop UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field MaxHeight UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field MaxWidth UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field MinHeight UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field MinWidth UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Opacity UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Overflow UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Padding UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field PaddingBottom UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field PaddingLeft UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field PaddingRight UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field PaddingTop UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Position UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Right UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Rotate UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Scale UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field TextOverflow UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field TextShadow UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Top UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field TransformOrigin UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Transition UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field TransitionDelay UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field TransitionDuration UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field TransitionProperty UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field TransitionTimingFunction UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Translate UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityBackgroundImageTintColor UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityBackgroundScaleMode UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityFont UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityFontDefinition UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityFontStyleAndWeight UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityOverflowClipBox UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityParagraphSpacing UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnitySliceBottom UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnitySliceLeft UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnitySliceRight UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnitySliceScale UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnitySliceTop UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityTextAlign UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityTextOutline UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityTextOutlineColor UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityTextOutlineWidth UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field UnityTextOverflowPosition UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Visibility UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field WhiteSpace UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field Width UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field WordSpacing UnityEngine.UIElements.StyleSheets.StylePropertyId
UnityEngine.UIElements.StyleSheets.StylePropertyId = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StylePropertyId UnityEngine.UIElements.StyleSheets.StylePropertyId
CS.UnityEngine.UIElements.StyleSheets.StylePropertyId = UnityEngine.UIElements.StyleSheets.StylePropertyId


---@class UnityEngine.UIElements.StyleSheets.StylePropertyReader : System.Object
---@field property UnityEngine.UIElements.StyleProperty
---@field propertyId UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field valueCount number
---@field dpiScaling number
UnityEngine.UIElements.StyleSheets.StylePropertyReader = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StylePropertyReader UnityEngine.UIElements.StyleSheets.StylePropertyReader
CS.UnityEngine.UIElements.StyleSheets.StylePropertyReader = UnityEngine.UIElements.StyleSheets.StylePropertyReader

---@return UnityEngine.UIElements.StyleSheets.StylePropertyReader
function UnityEngine.UIElements.StyleSheets.StylePropertyReader.New() end
---@overload fun(valCount: number, val1: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val2: UnityEngine.UIElements.StyleSheets.StylePropertyValue, zVvalue: UnityEngine.UIElements.StyleSheets.StylePropertyValue) : UnityEngine.UIElements.TransformOrigin
---@param index number
---@return UnityEngine.UIElements.TransformOrigin
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadTransformOrigin(index) end
---@overload fun(valCount: number, val1: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val2: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val3: UnityEngine.UIElements.StyleSheets.StylePropertyValue) : UnityEngine.UIElements.Translate
---@param index number
---@return UnityEngine.UIElements.Translate
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadTranslate(index) end
---@overload fun(valCount: number, val1: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val2: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val3: UnityEngine.UIElements.StyleSheets.StylePropertyValue) : UnityEngine.UIElements.Scale
---@param index number
---@return UnityEngine.UIElements.Scale
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadScale(index) end
---@overload fun(valCount: number, val1: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val2: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val3: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val4: UnityEngine.UIElements.StyleSheets.StylePropertyValue) : UnityEngine.UIElements.Rotate
---@param index number
---@return UnityEngine.UIElements.Rotate
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadRotate(index) end
---@param value UnityEngine.UIElements.StyleSheets.StylePropertyValue
---@return UnityEngine.UIElements.Angle
function UnityEngine.UIElements.StyleSheets.StylePropertyReader.ReadAngle(value) end
---@param valCount number
---@param val1 UnityEngine.UIElements.StyleSheets.StylePropertyValue
---@param val2 UnityEngine.UIElements.StyleSheets.StylePropertyValue
---@param keyword UnityEngine.UIElements.BackgroundPositionKeyword
---@return UnityEngine.UIElements.BackgroundPosition
function UnityEngine.UIElements.StyleSheets.StylePropertyReader.ReadBackgroundPosition(valCount, val1, val2, keyword) end
---@overload fun(valCount: number, val1: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val2: UnityEngine.UIElements.StyleSheets.StylePropertyValue) : UnityEngine.UIElements.BackgroundRepeat
---@param index number
---@return UnityEngine.UIElements.BackgroundRepeat
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadBackgroundRepeat(index) end
---@overload fun(valCount: number, val1: UnityEngine.UIElements.StyleSheets.StylePropertyValue, val2: UnityEngine.UIElements.StyleSheets.StylePropertyValue) : UnityEngine.UIElements.BackgroundSize
---@param index number
---@return UnityEngine.UIElements.BackgroundSize
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadBackgroundSize(index) end
---@param sheet UnityEngine.UIElements.StyleSheet
---@param selector UnityEngine.UIElements.StyleComplexSelector
---@param varContext UnityEngine.UIElements.StyleVariableContext
---@param dpiScaling number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:SetContext(sheet, selector, varContext, dpiScaling) end
---@param sheet UnityEngine.UIElements.StyleSheet
---@param properties UnityEngine.UIElements.StyleProperty[]
---@param propertyIds UnityEngine.UIElements.StyleSheets.StylePropertyId[]
---@param dpiScaling number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:SetInlineContext(sheet, properties, propertyIds, dpiScaling) end
---@return UnityEngine.UIElements.StyleSheets.StylePropertyId
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:MoveNextProperty() end
---@param index number
---@return UnityEngine.UIElements.StyleSheets.StylePropertyValue
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:GetValue(index) end
---@param index number
---@return UnityEngine.UIElements.StyleValueType
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:GetValueType(index) end
---@param index number
---@param type UnityEngine.UIElements.StyleValueType
---@return boolean
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:IsValueType(index, type) end
---@param index number
---@param keyword UnityEngine.UIElements.StyleValueKeyword
---@return boolean
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:IsKeyword(index, keyword) end
---@param index number
---@return string
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadAsString(index) end
---@param index number
---@return UnityEngine.UIElements.Length
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadLength(index) end
---@param index number
---@return UnityEngine.UIElements.TimeValue
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadTimeValue(index) end
---@param index number
---@return number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadFloat(index) end
---@param index number
---@return number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadInt(index) end
---@param index number
---@return UnityEngine.Color
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadColor(index) end
---@param enumType UnityEngine.UIElements.StyleSheets.StyleEnumType
---@param index number
---@return number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadEnum(enumType, index) end
---@param index number
---@return UnityEngine.UIElements.FontDefinition
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadFontDefinition(index) end
---@param index number
---@return UnityEngine.Font
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadFont(index) end
---@param index number
---@return UnityEngine.UIElements.Background
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadBackground(index) end
---@param index number
---@return UnityEngine.UIElements.Cursor
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadCursor(index) end
---@param index number
---@return UnityEngine.UIElements.TextShadow
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadTextShadow(index) end
---@param index number
---@return UnityEngine.UIElements.BackgroundPosition
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadBackgroundPositionX(index) end
---@param index number
---@return UnityEngine.UIElements.BackgroundPosition
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadBackgroundPositionY(index) end
---@param list System.Collections.Generic.List
---@param index number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadListEasingFunction(list, index) end
---@param list System.Collections.Generic.List
---@param index number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadListTimeValue(list, index) end
---@param list System.Collections.Generic.List
---@param index number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadListStylePropertyName(list, index) end
---@param list System.Collections.Generic.List
---@param index number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader:ReadListString(list, index) end

---@class UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction : System.MulticastDelegate
UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction
CS.UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction = UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction
function UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction.New(object, method) end
---@param sheet UnityEngine.UIElements.StyleSheet
---@param handle UnityEngine.UIElements.StyleValueHandle
---@return number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction:Invoke(sheet, handle) end
---@param sheet UnityEngine.UIElements.StyleSheet
---@param handle UnityEngine.UIElements.StyleValueHandle
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction:BeginInvoke(sheet, handle, callback, object) end
---@param result System.IAsyncResult
---@return number
function UnityEngine.UIElements.StyleSheets.StylePropertyReader.GetCursorIdFunction:EndInvoke(result) end

---@class UnityEngine.UIElements.StyleSheets.StylePropertyUtil : System.Object
---@field k_GroupOffset number
UnityEngine.UIElements.StyleSheets.StylePropertyUtil = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StylePropertyUtil UnityEngine.UIElements.StyleSheets.StylePropertyUtil
CS.UnityEngine.UIElements.StyleSheets.StylePropertyUtil = UnityEngine.UIElements.StyleSheets.StylePropertyUtil

---@param enumType UnityEngine.UIElements.StyleSheets.StyleEnumType
---@param value string
---@param out_intValue number
---@return boolean, number
function UnityEngine.UIElements.StyleSheets.StylePropertyUtil.TryGetEnumIntValue(enumType, value, out_intValue) end
---@param shorthand UnityEngine.UIElements.StyleSheets.StylePropertyId
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return boolean
function UnityEngine.UIElements.StyleSheets.StylePropertyUtil.IsMatchingShorthand(shorthand, id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return System.Collections.Generic.IEnumerable
function UnityEngine.UIElements.StyleSheets.StylePropertyUtil.GetAllowedAssetTypesForProperty(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return boolean
function UnityEngine.UIElements.StyleSheets.StylePropertyUtil.IsAnimatable(id) end
---@return System.Collections.Generic.IEnumerable
function UnityEngine.UIElements.StyleSheets.StylePropertyUtil.AllPropertyIds() end

---@class UnityEngine.UIElements.StyleSheets.StylePropertyValue : System.ValueType
---@field sheet UnityEngine.UIElements.StyleSheet
---@field handle UnityEngine.UIElements.StyleValueHandle
UnityEngine.UIElements.StyleSheets.StylePropertyValue = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StylePropertyValue UnityEngine.UIElements.StyleSheets.StylePropertyValue
CS.UnityEngine.UIElements.StyleSheets.StylePropertyValue = UnityEngine.UIElements.StyleSheets.StylePropertyValue


---@class UnityEngine.UIElements.StyleSheets.StylePropertyValueMatcher : UnityEngine.UIElements.StyleSheets.BaseStyleMatcher
---@field valueCount number
---@field isCurrentVariable boolean
---@field isCurrentComma boolean
UnityEngine.UIElements.StyleSheets.StylePropertyValueMatcher = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StylePropertyValueMatcher UnityEngine.UIElements.StyleSheets.StylePropertyValueMatcher
CS.UnityEngine.UIElements.StyleSheets.StylePropertyValueMatcher = UnityEngine.UIElements.StyleSheets.StylePropertyValueMatcher

---@return UnityEngine.UIElements.StyleSheets.StylePropertyValueMatcher
function UnityEngine.UIElements.StyleSheets.StylePropertyValueMatcher.New() end
---@param exp UnityEngine.UIElements.StyleSheets.Syntax.Expression
---@param values System.Collections.Generic.List
---@return UnityEngine.UIElements.StyleSheets.MatchResult
function UnityEngine.UIElements.StyleSheets.StylePropertyValueMatcher:Match(exp, values) end

---@class UnityEngine.UIElements.StyleSheets.StylePropertyValueParser : System.Object
UnityEngine.UIElements.StyleSheets.StylePropertyValueParser = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StylePropertyValueParser UnityEngine.UIElements.StyleSheets.StylePropertyValueParser
CS.UnityEngine.UIElements.StyleSheets.StylePropertyValueParser = UnityEngine.UIElements.StyleSheets.StylePropertyValueParser

---@return UnityEngine.UIElements.StyleSheets.StylePropertyValueParser
function UnityEngine.UIElements.StyleSheets.StylePropertyValueParser.New() end
---@param propertyValue string
---@return string[]
function UnityEngine.UIElements.StyleSheets.StylePropertyValueParser:Parse(propertyValue) end

---@class UnityEngine.UIElements.StyleSheets.StyleSelectorHelper : System.Object
UnityEngine.UIElements.StyleSheets.StyleSelectorHelper = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleSelectorHelper UnityEngine.UIElements.StyleSheets.StyleSelectorHelper
CS.UnityEngine.UIElements.StyleSheets.StyleSelectorHelper = UnityEngine.UIElements.StyleSheets.StyleSelectorHelper

---@param element UnityEngine.UIElements.VisualElement
---@param selector UnityEngine.UIElements.StyleSelector
---@return UnityEngine.UIElements.StyleSheets.MatchResultInfo
function UnityEngine.UIElements.StyleSheets.StyleSelectorHelper.MatchesSelector(element, selector) end
---@param element UnityEngine.UIElements.VisualElement
---@param complexSelector UnityEngine.UIElements.StyleComplexSelector
---@param processResult System.Action | function
---@return boolean
function UnityEngine.UIElements.StyleSheets.StyleSelectorHelper.MatchRightToLeft(element, complexSelector, processResult) end
---@overload fun(context: UnityEngine.UIElements.StyleMatchingContext, matchedSelectors: System.Collections.Generic.List)
---@param context UnityEngine.UIElements.StyleMatchingContext
---@param matchedSelectors System.Collections.Generic.List
---@param parentSheetIndex number
function UnityEngine.UIElements.StyleSheets.StyleSelectorHelper.FindMatches(context, matchedSelectors, parentSheetIndex) end

---@class UnityEngine.UIElements.StyleSheets.StyleSheetBuilder : System.Object
---@field currentProperty UnityEngine.UIElements.StyleProperty
UnityEngine.UIElements.StyleSheets.StyleSheetBuilder = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleSheetBuilder UnityEngine.UIElements.StyleSheets.StyleSheetBuilder
CS.UnityEngine.UIElements.StyleSheets.StyleSheetBuilder = UnityEngine.UIElements.StyleSheets.StyleSheetBuilder

---@return UnityEngine.UIElements.StyleSheets.StyleSheetBuilder
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.New() end
---@param ruleLine number
---@return UnityEngine.UIElements.StyleRule
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:BeginRule(ruleLine) end
---@param specificity number
---@return UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:BeginComplexSelector(specificity) end
---@param parts UnityEngine.UIElements.StyleSelectorPart[]
---@param previousRelationsip UnityEngine.UIElements.StyleSelectorRelationship
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:AddSimpleSelector(parts, previousRelationsip) end
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:EndComplexSelector() end
---@param name string
---@param line number
---@return UnityEngine.UIElements.StyleProperty
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:BeginProperty(name, line) end
---@param importStruct UnityEngine.UIElements.StyleSheet.ImportStruct
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:AddImport(importStruct) end
---@overload fun(self: UnityEngine.UIElements.StyleSheets.StyleSheetBuilder, value: number)
---@overload fun(self: UnityEngine.UIElements.StyleSheets.StyleSheetBuilder, value: UnityEngine.UIElements.StyleSheets.Dimension)
---@overload fun(self: UnityEngine.UIElements.StyleSheets.StyleSheetBuilder, keyword: UnityEngine.UIElements.StyleValueKeyword)
---@overload fun(self: UnityEngine.UIElements.StyleSheets.StyleSheetBuilder, _function: UnityEngine.UIElements.StyleValueFunction)
---@overload fun(self: UnityEngine.UIElements.StyleSheets.StyleSheetBuilder, value: string, type: UnityEngine.UIElements.StyleValueType)
---@overload fun(self: UnityEngine.UIElements.StyleSheets.StyleSheetBuilder, value: UnityEngine.Color)
---@overload fun(self: UnityEngine.UIElements.StyleSheets.StyleSheetBuilder, value: UnityEngine.Object)
---@param value UnityEngine.UIElements.StyleSheets.ScalableImage
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:AddValue(value) end
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:AddCommaSeparator() end
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:EndProperty() end
---@return number
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:EndRule() end
---@param writeTo UnityEngine.UIElements.StyleSheet
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder:BuildTo(writeTo) end

---@class UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState
---@field Init UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState
---@field Rule UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState
---@field ComplexSelector UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState
---@field Property UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState
UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState
CS.UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState = UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.BuilderState


---@class UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope : System.ValueType
UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope
CS.UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope = UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope

---@param builder UnityEngine.UIElements.StyleSheets.StyleSheetBuilder
---@return UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope.New(builder) end
function UnityEngine.UIElements.StyleSheets.StyleSheetBuilder.ComplexSelectorScope:Dispose() end

---@class UnityEngine.UIElements.StyleSheets.StyleSheetCache : System.Object
UnityEngine.UIElements.StyleSheets.StyleSheetCache = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleSheetCache UnityEngine.UIElements.StyleSheets.StyleSheetCache
CS.UnityEngine.UIElements.StyleSheets.StyleSheetCache = UnityEngine.UIElements.StyleSheets.StyleSheetCache


---@class UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey : System.ValueType
---@field sheetInstanceID number
---@field index number
UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey
CS.UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey = UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey

---@param sheet UnityEngine.UIElements.StyleSheet
---@param index number
---@return UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey
function UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey.New(sheet, index) end

---@class UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer : System.Object
UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer
CS.UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer = UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer

---@return UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer
function UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer.New() end
---@param x UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey
---@param y UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey
---@return boolean
function UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer:Equals(x, y) end
---@param key UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKey
---@return number
function UnityEngine.UIElements.StyleSheets.StyleSheetCache.SheetHandleKeyComparer:GetHashCode(key) end

---@class UnityEngine.UIElements.StyleSheets.StyleSheetColor : System.Object
UnityEngine.UIElements.StyleSheets.StyleSheetColor = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleSheetColor UnityEngine.UIElements.StyleSheets.StyleSheetColor
CS.UnityEngine.UIElements.StyleSheets.StyleSheetColor = UnityEngine.UIElements.StyleSheets.StyleSheetColor

---@param name string
---@param out_color UnityEngine.Color
---@return boolean, UnityEngine.Color
function UnityEngine.UIElements.StyleSheets.StyleSheetColor.TryGetColor(name, out_color) end

---@class UnityEngine.UIElements.StyleSheets.StyleSheetExtensions : System.Object
UnityEngine.UIElements.StyleSheets.StyleSheetExtensions = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleSheetExtensions UnityEngine.UIElements.StyleSheets.StyleSheetExtensions
CS.UnityEngine.UIElements.StyleSheets.StyleSheetExtensions = UnityEngine.UIElements.StyleSheets.StyleSheetExtensions

---@param sheet UnityEngine.UIElements.StyleSheet
---@param handle UnityEngine.UIElements.StyleValueHandle
---@return string
function UnityEngine.UIElements.StyleSheets.StyleSheetExtensions.ReadAsString(sheet, handle) end
---@param handle UnityEngine.UIElements.StyleValueHandle
---@return boolean
function UnityEngine.UIElements.StyleSheets.StyleSheetExtensions.IsVarFunction(handle) end

---@class UnityEngine.UIElements.StyleSheets.StyleValidationResult : System.ValueType
---@field status UnityEngine.UIElements.StyleSheets.StyleValidationStatus
---@field message string
---@field errorValue string
---@field hint string
---@field success boolean
UnityEngine.UIElements.StyleSheets.StyleValidationResult = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleValidationResult UnityEngine.UIElements.StyleSheets.StyleValidationResult
CS.UnityEngine.UIElements.StyleSheets.StyleValidationResult = UnityEngine.UIElements.StyleSheets.StyleValidationResult


---@class UnityEngine.UIElements.StyleSheets.StyleValidationStatus
---@field Ok UnityEngine.UIElements.StyleSheets.StyleValidationStatus
---@field Error UnityEngine.UIElements.StyleSheets.StyleValidationStatus
---@field Warning UnityEngine.UIElements.StyleSheets.StyleValidationStatus
UnityEngine.UIElements.StyleSheets.StyleValidationStatus = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleValidationStatus UnityEngine.UIElements.StyleSheets.StyleValidationStatus
CS.UnityEngine.UIElements.StyleSheets.StyleValidationStatus = UnityEngine.UIElements.StyleSheets.StyleValidationStatus


---@class UnityEngine.UIElements.StyleSheets.StyleValidator : System.Object
UnityEngine.UIElements.StyleSheets.StyleValidator = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleValidator UnityEngine.UIElements.StyleSheets.StyleValidator
CS.UnityEngine.UIElements.StyleSheets.StyleValidator = UnityEngine.UIElements.StyleSheets.StyleValidator

---@return UnityEngine.UIElements.StyleSheets.StyleValidator
function UnityEngine.UIElements.StyleSheets.StyleValidator.New() end
---@param name string
---@param value string
---@return UnityEngine.UIElements.StyleSheets.StyleValidationResult
function UnityEngine.UIElements.StyleSheets.StyleValidator:ValidateProperty(name, value) end

---@class UnityEngine.UIElements.StyleSheets.StyleValue : System.ValueType
---@field id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field keyword UnityEngine.UIElements.StyleKeyword
---@field number number
---@field length UnityEngine.UIElements.Length
---@field color UnityEngine.Color
---@field resource System.Runtime.InteropServices.GCHandle
---@field position UnityEngine.UIElements.BackgroundPosition
---@field _repeat UnityEngine.UIElements.BackgroundRepeat
UnityEngine.UIElements.StyleSheets.StyleValue = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleValue UnityEngine.UIElements.StyleSheets.StyleValue
CS.UnityEngine.UIElements.StyleSheets.StyleValue = UnityEngine.UIElements.StyleSheets.StyleValue


---@class UnityEngine.UIElements.StyleSheets.StyleValueManaged : System.ValueType
---@field id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@field keyword UnityEngine.UIElements.StyleKeyword
---@field value System.Object
UnityEngine.UIElements.StyleSheets.StyleValueManaged = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.StyleValueManaged UnityEngine.UIElements.StyleSheets.StyleValueManaged
CS.UnityEngine.UIElements.StyleSheets.StyleValueManaged = UnityEngine.UIElements.StyleSheets.StyleValueManaged


---@class UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field None UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field Number UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field Integer UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field Length UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field Percentage UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field Color UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field Resource UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field Url UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field Time UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field Angle UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field CustomIdent UnityEngine.UIElements.StyleSheets.Syntax.DataType
UnityEngine.UIElements.StyleSheets.Syntax.DataType = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.DataType UnityEngine.UIElements.StyleSheets.Syntax.DataType
CS.UnityEngine.UIElements.StyleSheets.Syntax.DataType = UnityEngine.UIElements.StyleSheets.Syntax.DataType


---@class UnityEngine.UIElements.StyleSheets.Syntax.Expression : System.Object
---@field type UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType
---@field multiplier UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplier
---@field dataType UnityEngine.UIElements.StyleSheets.Syntax.DataType
---@field combinator UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator
---@field subExpressions UnityEngine.UIElements.StyleSheets.Syntax.Expression[]
---@field keyword string
UnityEngine.UIElements.StyleSheets.Syntax.Expression = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.Expression UnityEngine.UIElements.StyleSheets.Syntax.Expression
CS.UnityEngine.UIElements.StyleSheets.Syntax.Expression = UnityEngine.UIElements.StyleSheets.Syntax.Expression

---@param type UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType
---@return UnityEngine.UIElements.StyleSheets.Syntax.Expression
function UnityEngine.UIElements.StyleSheets.Syntax.Expression.New(type) end

---@class UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator
---@field None UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator
---@field Or UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator
---@field OrOr UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator
---@field AndAnd UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator
---@field Juxtaposition UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator
---@field Group UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator
UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator
CS.UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator = UnityEngine.UIElements.StyleSheets.Syntax.ExpressionCombinator


---@class UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplier : System.ValueType
---@field Infinity number
---@field min number
---@field max number
---@field type UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplier = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplier UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplier
CS.UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplier = UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplier

---@param type UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
---@return UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplier
function UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplier.New(type) end

---@class UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
---@field None UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
---@field ZeroOrMore UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
---@field OneOrMore UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
---@field ZeroOrOne UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
---@field Ranges UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
---@field OneOrMoreComma UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
---@field GroupAtLeastOne UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType
CS.UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType = UnityEngine.UIElements.StyleSheets.Syntax.ExpressionMultiplierType


---@class UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType
---@field Unknown UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType
---@field Data UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType
---@field Keyword UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType
---@field Combinator UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType
UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType
CS.UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType = UnityEngine.UIElements.StyleSheets.Syntax.ExpressionType


---@class UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser : System.Object
UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser
CS.UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser = UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser

---@return UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser
function UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser.New() end
---@param syntax string
---@return UnityEngine.UIElements.StyleSheets.Syntax.Expression
function UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxParser:Parse(syntax) end

---@class UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken : System.ValueType
---@field type UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field text string
---@field number number
UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken
CS.UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken = UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken

---@overload fun(t: UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType) : UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken
---@overload fun(type: UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType, text: string) : UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken
---@param type UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@param number number
---@return UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken
function UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken.New(type, number) end

---@class UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer : System.Object
---@field current UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken
UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer
CS.UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer = UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer

---@return UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer
function UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer.New() end
---@return UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken
function UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer:MoveNext() end
---@return UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxToken
function UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer:PeekNext() end
---@param syntax string
function UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenizer:Tokenize(syntax) end

---@class UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field Unknown UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field String UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field Number UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field Space UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field SingleBar UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field DoubleBar UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field DoubleAmpersand UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field Comma UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field SingleQuote UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field Asterisk UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field Plus UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field QuestionMark UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field HashMark UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field ExclamationPoint UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field OpenBracket UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field CloseBracket UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field OpenBrace UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field CloseBrace UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field LessThan UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field GreaterThan UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
---@field End UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType = {}
---@alias CS.UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType
CS.UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType = UnityEngine.UIElements.StyleSheets.Syntax.StyleSyntaxTokenType


---@class UnityEngine.UIElements.StyleTextShadow : System.ValueType
---@field value UnityEngine.UIElements.TextShadow
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleTextShadow = {}
---@alias CS.UnityEngine.UIElements.StyleTextShadow UnityEngine.UIElements.StyleTextShadow
CS.UnityEngine.UIElements.StyleTextShadow = UnityEngine.UIElements.StyleTextShadow

---@overload fun(v: UnityEngine.UIElements.TextShadow) : UnityEngine.UIElements.StyleTextShadow
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleTextShadow
function UnityEngine.UIElements.StyleTextShadow.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleTextShadow, other: UnityEngine.UIElements.StyleTextShadow) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleTextShadow:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleTextShadow:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleTextShadow:ToString() end

---@class UnityEngine.UIElements.StyleTransformOrigin : System.ValueType
---@field value UnityEngine.UIElements.TransformOrigin
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleTransformOrigin = {}
---@alias CS.UnityEngine.UIElements.StyleTransformOrigin UnityEngine.UIElements.StyleTransformOrigin
CS.UnityEngine.UIElements.StyleTransformOrigin = UnityEngine.UIElements.StyleTransformOrigin

---@overload fun(v: UnityEngine.UIElements.TransformOrigin) : UnityEngine.UIElements.StyleTransformOrigin
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleTransformOrigin
function UnityEngine.UIElements.StyleTransformOrigin.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleTransformOrigin, other: UnityEngine.UIElements.StyleTransformOrigin) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleTransformOrigin:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleTransformOrigin:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleTransformOrigin:ToString() end

---@class UnityEngine.UIElements.StyleTranslate : System.ValueType
---@field value UnityEngine.UIElements.Translate
---@field keyword UnityEngine.UIElements.StyleKeyword
UnityEngine.UIElements.StyleTranslate = {}
---@alias CS.UnityEngine.UIElements.StyleTranslate UnityEngine.UIElements.StyleTranslate
CS.UnityEngine.UIElements.StyleTranslate = UnityEngine.UIElements.StyleTranslate

---@overload fun(v: UnityEngine.UIElements.Translate) : UnityEngine.UIElements.StyleTranslate
---@param keyword UnityEngine.UIElements.StyleKeyword
---@return UnityEngine.UIElements.StyleTranslate
function UnityEngine.UIElements.StyleTranslate.New(keyword) end
---@overload fun(self: UnityEngine.UIElements.StyleTranslate, other: UnityEngine.UIElements.StyleTranslate) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.StyleTranslate:Equals(obj) end
---@return number
function UnityEngine.UIElements.StyleTranslate:GetHashCode() end
---@return string
function UnityEngine.UIElements.StyleTranslate:ToString() end

---@class UnityEngine.UIElements.StyleValueCollection : System.Object
UnityEngine.UIElements.StyleValueCollection = {}
---@alias CS.UnityEngine.UIElements.StyleValueCollection UnityEngine.UIElements.StyleValueCollection
CS.UnityEngine.UIElements.StyleValueCollection = UnityEngine.UIElements.StyleValueCollection

---@return UnityEngine.UIElements.StyleValueCollection
function UnityEngine.UIElements.StyleValueCollection.New() end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StyleLength
function UnityEngine.UIElements.StyleValueCollection:GetStyleLength(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StyleFloat
function UnityEngine.UIElements.StyleValueCollection:GetStyleFloat(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StyleInt
function UnityEngine.UIElements.StyleValueCollection:GetStyleInt(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StyleColor
function UnityEngine.UIElements.StyleValueCollection:GetStyleColor(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StyleBackground
function UnityEngine.UIElements.StyleValueCollection:GetStyleBackground(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StyleBackgroundPosition
function UnityEngine.UIElements.StyleValueCollection:GetStyleBackgroundPosition(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StyleBackgroundRepeat
function UnityEngine.UIElements.StyleValueCollection:GetStyleBackgroundRepeat(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StyleFont
function UnityEngine.UIElements.StyleValueCollection:GetStyleFont(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@return UnityEngine.UIElements.StyleFontDefinition
function UnityEngine.UIElements.StyleValueCollection:GetStyleFontDefinition(id) end
---@param id UnityEngine.UIElements.StyleSheets.StylePropertyId
---@param ref_value UnityEngine.UIElements.StyleSheets.StyleValue
---@return boolean, UnityEngine.UIElements.StyleSheets.StyleValue
function UnityEngine.UIElements.StyleValueCollection:TryGetStyleValue(id, ref_value) end
---@param value UnityEngine.UIElements.StyleSheets.StyleValue
function UnityEngine.UIElements.StyleValueCollection:SetStyleValue(value) end

---@class UnityEngine.UIElements.StyleValueExtensions : System.Object
UnityEngine.UIElements.StyleValueExtensions = {}
---@alias CS.UnityEngine.UIElements.StyleValueExtensions UnityEngine.UIElements.StyleValueExtensions
CS.UnityEngine.UIElements.StyleValueExtensions = UnityEngine.UIElements.StyleValueExtensions


---@class UnityEngine.UIElements.StyleValueFunction
---@field Unknown UnityEngine.UIElements.StyleValueFunction
---@field Var UnityEngine.UIElements.StyleValueFunction
---@field Env UnityEngine.UIElements.StyleValueFunction
---@field LinearGradient UnityEngine.UIElements.StyleValueFunction
UnityEngine.UIElements.StyleValueFunction = {}
---@alias CS.UnityEngine.UIElements.StyleValueFunction UnityEngine.UIElements.StyleValueFunction
CS.UnityEngine.UIElements.StyleValueFunction = UnityEngine.UIElements.StyleValueFunction

---@return string
function UnityEngine.UIElements.StyleValueFunction:ToUssString() end

---@class UnityEngine.UIElements.StyleValueFunctionExtension : System.Object
---@field k_Var string
---@field k_Env string
---@field k_LinearGradient string
UnityEngine.UIElements.StyleValueFunctionExtension = {}
---@alias CS.UnityEngine.UIElements.StyleValueFunctionExtension UnityEngine.UIElements.StyleValueFunctionExtension
CS.UnityEngine.UIElements.StyleValueFunctionExtension = UnityEngine.UIElements.StyleValueFunctionExtension

---@param ussValue string
---@return UnityEngine.UIElements.StyleValueFunction
function UnityEngine.UIElements.StyleValueFunctionExtension.FromUssString(ussValue) end
---@param svf UnityEngine.UIElements.StyleValueFunction
---@return string
function UnityEngine.UIElements.StyleValueFunctionExtension.ToUssString(svf) end

---@class UnityEngine.UIElements.StyleValueHandle : System.ValueType
---@field valueType UnityEngine.UIElements.StyleValueType
UnityEngine.UIElements.StyleValueHandle = {}
---@alias CS.UnityEngine.UIElements.StyleValueHandle UnityEngine.UIElements.StyleValueHandle
CS.UnityEngine.UIElements.StyleValueHandle = UnityEngine.UIElements.StyleValueHandle

---@return boolean
function UnityEngine.UIElements.StyleValueHandle:IsVarFunction() end

---@class UnityEngine.UIElements.StyleValueKeyword
---@field Inherit UnityEngine.UIElements.StyleValueKeyword
---@field Initial UnityEngine.UIElements.StyleValueKeyword
---@field Auto UnityEngine.UIElements.StyleValueKeyword
---@field Unset UnityEngine.UIElements.StyleValueKeyword
---@field True UnityEngine.UIElements.StyleValueKeyword
---@field False UnityEngine.UIElements.StyleValueKeyword
---@field None UnityEngine.UIElements.StyleValueKeyword
UnityEngine.UIElements.StyleValueKeyword = {}
---@alias CS.UnityEngine.UIElements.StyleValueKeyword UnityEngine.UIElements.StyleValueKeyword
CS.UnityEngine.UIElements.StyleValueKeyword = UnityEngine.UIElements.StyleValueKeyword

---@return string
function UnityEngine.UIElements.StyleValueKeyword:ToUssString() end

---@class UnityEngine.UIElements.StyleValueKeywordExtension : System.Object
UnityEngine.UIElements.StyleValueKeywordExtension = {}
---@alias CS.UnityEngine.UIElements.StyleValueKeywordExtension UnityEngine.UIElements.StyleValueKeywordExtension
CS.UnityEngine.UIElements.StyleValueKeywordExtension = UnityEngine.UIElements.StyleValueKeywordExtension

---@param svk UnityEngine.UIElements.StyleValueKeyword
---@return string
function UnityEngine.UIElements.StyleValueKeywordExtension.ToUssString(svk) end

---@class UnityEngine.UIElements.StyleValueType
---@field Invalid UnityEngine.UIElements.StyleValueType
---@field Keyword UnityEngine.UIElements.StyleValueType
---@field Float UnityEngine.UIElements.StyleValueType
---@field Dimension UnityEngine.UIElements.StyleValueType
---@field Color UnityEngine.UIElements.StyleValueType
---@field ResourcePath UnityEngine.UIElements.StyleValueType
---@field AssetReference UnityEngine.UIElements.StyleValueType
---@field Enum UnityEngine.UIElements.StyleValueType
---@field Variable UnityEngine.UIElements.StyleValueType
---@field String UnityEngine.UIElements.StyleValueType
---@field Function UnityEngine.UIElements.StyleValueType
---@field CommaSeparator UnityEngine.UIElements.StyleValueType
---@field ScalableImage UnityEngine.UIElements.StyleValueType
---@field MissingAssetReference UnityEngine.UIElements.StyleValueType
UnityEngine.UIElements.StyleValueType = {}
---@alias CS.UnityEngine.UIElements.StyleValueType UnityEngine.UIElements.StyleValueType
CS.UnityEngine.UIElements.StyleValueType = UnityEngine.UIElements.StyleValueType


---@class UnityEngine.UIElements.StyleVariable : System.ValueType
---@field name string
---@field sheet UnityEngine.UIElements.StyleSheet
---@field handles UnityEngine.UIElements.StyleValueHandle[]
UnityEngine.UIElements.StyleVariable = {}
---@alias CS.UnityEngine.UIElements.StyleVariable UnityEngine.UIElements.StyleVariable
CS.UnityEngine.UIElements.StyleVariable = UnityEngine.UIElements.StyleVariable

---@param name string
---@param sheet UnityEngine.UIElements.StyleSheet
---@param handles UnityEngine.UIElements.StyleValueHandle[]
---@return UnityEngine.UIElements.StyleVariable
function UnityEngine.UIElements.StyleVariable.New(name, sheet, handles) end
---@return number
function UnityEngine.UIElements.StyleVariable:GetHashCode() end

---@class UnityEngine.UIElements.StyleVariableContext : System.Object
---@field none UnityEngine.UIElements.StyleVariableContext
---@field variables System.Collections.Generic.List
UnityEngine.UIElements.StyleVariableContext = {}
---@alias CS.UnityEngine.UIElements.StyleVariableContext UnityEngine.UIElements.StyleVariableContext
CS.UnityEngine.UIElements.StyleVariableContext = UnityEngine.UIElements.StyleVariableContext

---@overload fun() : UnityEngine.UIElements.StyleVariableContext
---@param other UnityEngine.UIElements.StyleVariableContext
---@return UnityEngine.UIElements.StyleVariableContext
function UnityEngine.UIElements.StyleVariableContext.New(other) end
---@param sv UnityEngine.UIElements.StyleVariable
function UnityEngine.UIElements.StyleVariableContext:Add(sv) end
---@param other UnityEngine.UIElements.StyleVariableContext
function UnityEngine.UIElements.StyleVariableContext:AddInitialRange(other) end
function UnityEngine.UIElements.StyleVariableContext:Clear() end
---@param name string
---@param out_v UnityEngine.UIElements.StyleVariable
---@return boolean, UnityEngine.UIElements.StyleVariable
function UnityEngine.UIElements.StyleVariableContext:TryFindVariable(name, out_v) end
---@return number
function UnityEngine.UIElements.StyleVariableContext:GetVariableHash() end

---@class UnityEngine.UIElements.StyleVariableResolver : System.Object
---@field resolvedValues System.Collections.Generic.List
---@field variableContext UnityEngine.UIElements.StyleVariableContext
UnityEngine.UIElements.StyleVariableResolver = {}
---@alias CS.UnityEngine.UIElements.StyleVariableResolver UnityEngine.UIElements.StyleVariableResolver
CS.UnityEngine.UIElements.StyleVariableResolver = UnityEngine.UIElements.StyleVariableResolver

---@return UnityEngine.UIElements.StyleVariableResolver
function UnityEngine.UIElements.StyleVariableResolver.New() end
---@param property UnityEngine.UIElements.StyleProperty
---@param sheet UnityEngine.UIElements.StyleSheet
---@param handles UnityEngine.UIElements.StyleValueHandle[]
function UnityEngine.UIElements.StyleVariableResolver:Init(property, sheet, handles) end
---@param handle UnityEngine.UIElements.StyleValueHandle
function UnityEngine.UIElements.StyleVariableResolver:AddValue(handle) end
---@param ref_index number
---@return boolean, number
function UnityEngine.UIElements.StyleVariableResolver:ResolveVarFunction(ref_index) end
---@return boolean
function UnityEngine.UIElements.StyleVariableResolver:ValidateResolvedValues() end

---@class UnityEngine.UIElements.StyleVariableResolver.ResolveContext : System.ValueType
---@field sheet UnityEngine.UIElements.StyleSheet
---@field handles UnityEngine.UIElements.StyleValueHandle[]
UnityEngine.UIElements.StyleVariableResolver.ResolveContext = {}
---@alias CS.UnityEngine.UIElements.StyleVariableResolver.ResolveContext UnityEngine.UIElements.StyleVariableResolver.ResolveContext
CS.UnityEngine.UIElements.StyleVariableResolver.ResolveContext = UnityEngine.UIElements.StyleVariableResolver.ResolveContext


---@class UnityEngine.UIElements.StyleVariableResolver.Result
---@field Valid UnityEngine.UIElements.StyleVariableResolver.Result
---@field Invalid UnityEngine.UIElements.StyleVariableResolver.Result
---@field NotFound UnityEngine.UIElements.StyleVariableResolver.Result
UnityEngine.UIElements.StyleVariableResolver.Result = {}
---@alias CS.UnityEngine.UIElements.StyleVariableResolver.Result UnityEngine.UIElements.StyleVariableResolver.Result
CS.UnityEngine.UIElements.StyleVariableResolver.Result = UnityEngine.UIElements.StyleVariableResolver.Result


---@class UnityEngine.UIElements.TemplateAsset : UnityEngine.UIElements.VisualElementAsset
---@field templateAlias string
---@field attributeOverrides System.Collections.Generic.List
UnityEngine.UIElements.TemplateAsset = {}
---@alias CS.UnityEngine.UIElements.TemplateAsset UnityEngine.UIElements.TemplateAsset
CS.UnityEngine.UIElements.TemplateAsset = UnityEngine.UIElements.TemplateAsset

---@param templateAlias string
---@param fullTypeName string
---@return UnityEngine.UIElements.TemplateAsset
function UnityEngine.UIElements.TemplateAsset.New(templateAlias, fullTypeName) end
---@param slotName string
---@param resId number
function UnityEngine.UIElements.TemplateAsset:AddSlotUsage(slotName, resId) end

---@class UnityEngine.UIElements.TemplateAsset.AttributeOverride : System.ValueType
---@field m_ElementName string
---@field m_AttributeName string
---@field m_Value string
UnityEngine.UIElements.TemplateAsset.AttributeOverride = {}
---@alias CS.UnityEngine.UIElements.TemplateAsset.AttributeOverride UnityEngine.UIElements.TemplateAsset.AttributeOverride
CS.UnityEngine.UIElements.TemplateAsset.AttributeOverride = UnityEngine.UIElements.TemplateAsset.AttributeOverride


---@class UnityEngine.UIElements.TemplateContainer : UnityEngine.UIElements.BindableElement
---@field templateId string
---@field templateSource UnityEngine.UIElements.VisualTreeAsset
---@field contentContainer UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.TemplateContainer = {}
---@alias CS.UnityEngine.UIElements.TemplateContainer UnityEngine.UIElements.TemplateContainer
CS.UnityEngine.UIElements.TemplateContainer = UnityEngine.UIElements.TemplateContainer

---@overload fun() : UnityEngine.UIElements.TemplateContainer
---@param templateId string
---@return UnityEngine.UIElements.TemplateContainer
function UnityEngine.UIElements.TemplateContainer.New(templateId) end

---@class UnityEngine.UIElements.TemplateContainer.UxmlFactory : UnityEngine.UIElements.UxmlFactory
---@field uxmlName string
---@field uxmlQualifiedName string
UnityEngine.UIElements.TemplateContainer.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.TemplateContainer.UxmlFactory UnityEngine.UIElements.TemplateContainer.UxmlFactory
CS.UnityEngine.UIElements.TemplateContainer.UxmlFactory = UnityEngine.UIElements.TemplateContainer.UxmlFactory

---@return UnityEngine.UIElements.TemplateContainer.UxmlFactory
function UnityEngine.UIElements.TemplateContainer.UxmlFactory.New() end

---@class UnityEngine.UIElements.TemplateContainer.UxmlTraits : UnityEngine.UIElements.BindableElement.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.TemplateContainer.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.TemplateContainer.UxmlTraits UnityEngine.UIElements.TemplateContainer.UxmlTraits
CS.UnityEngine.UIElements.TemplateContainer.UxmlTraits = UnityEngine.UIElements.TemplateContainer.UxmlTraits

---@return UnityEngine.UIElements.TemplateContainer.UxmlTraits
function UnityEngine.UIElements.TemplateContainer.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.TemplateContainer.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.TextEditingManipulator : System.Object
UnityEngine.UIElements.TextEditingManipulator = {}
---@alias CS.UnityEngine.UIElements.TextEditingManipulator UnityEngine.UIElements.TextEditingManipulator
CS.UnityEngine.UIElements.TextEditingManipulator = UnityEngine.UIElements.TextEditingManipulator

---@param textElement UnityEngine.UIElements.TextElement
---@return UnityEngine.UIElements.TextEditingManipulator
function UnityEngine.UIElements.TextEditingManipulator.New(textElement) end

---@class UnityEngine.UIElements.TextEditorEventHandler : System.Object
UnityEngine.UIElements.TextEditorEventHandler = {}
---@alias CS.UnityEngine.UIElements.TextEditorEventHandler UnityEngine.UIElements.TextEditorEventHandler
CS.UnityEngine.UIElements.TextEditorEventHandler = UnityEngine.UIElements.TextEditorEventHandler

---@param evt UnityEngine.UIElements.EventBase
function UnityEngine.UIElements.TextEditorEventHandler:ExecuteDefaultActionAtTarget(evt) end

---@class UnityEngine.UIElements.TextElement : UnityEngine.UIElements.BindableElement
---@field ussClassName string
---@field text string
---@field enableRichText boolean
---@field parseEscapeSequences boolean
---@field displayTooltipWhenElided boolean
---@field isElided boolean
---@field experimental UnityEngine.UIElements.ITextElementExperimentalFeatures
---@field selection UnityEngine.UIElements.ITextSelection
UnityEngine.UIElements.TextElement = {}
---@alias CS.UnityEngine.UIElements.TextElement UnityEngine.UIElements.TextElement
CS.UnityEngine.UIElements.TextElement = UnityEngine.UIElements.TextElement

---@return UnityEngine.UIElements.TextElement
function UnityEngine.UIElements.TextElement.New() end
---@param textToMeasure string
---@param width number
---@param widthMode UnityEngine.UIElements.VisualElement.MeasureMode
---@param height number
---@param heightMode UnityEngine.UIElements.VisualElement.MeasureMode
---@return UnityEngine.Vector2
function UnityEngine.UIElements.TextElement:MeasureTextSize(textToMeasure, width, widthMode, height, heightMode) end

---@class UnityEngine.UIElements.TextElement.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.TextElement.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.TextElement.UxmlFactory UnityEngine.UIElements.TextElement.UxmlFactory
CS.UnityEngine.UIElements.TextElement.UxmlFactory = UnityEngine.UIElements.TextElement.UxmlFactory

---@return UnityEngine.UIElements.TextElement.UxmlFactory
function UnityEngine.UIElements.TextElement.UxmlFactory.New() end

---@class UnityEngine.UIElements.TextElement.UxmlTraits : UnityEngine.UIElements.BindableElement.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.TextElement.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.TextElement.UxmlTraits UnityEngine.UIElements.TextElement.UxmlTraits
CS.UnityEngine.UIElements.TextElement.UxmlTraits = UnityEngine.UIElements.TextElement.UxmlTraits

---@return UnityEngine.UIElements.TextElement.UxmlTraits
function UnityEngine.UIElements.TextElement.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.TextElement.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.TextField : UnityEngine.UIElements.TextInputBaseField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
---@field multiline boolean
---@field value string
UnityEngine.UIElements.TextField = {}
---@alias CS.UnityEngine.UIElements.TextField UnityEngine.UIElements.TextField
CS.UnityEngine.UIElements.TextField = UnityEngine.UIElements.TextField

---@overload fun() : UnityEngine.UIElements.TextField
---@overload fun(maxLength: number, multiline: boolean, isPasswordField: boolean, maskChar: System.Char) : UnityEngine.UIElements.TextField
---@overload fun(label: string) : UnityEngine.UIElements.TextField
---@param label string
---@param maxLength number
---@param multiline boolean
---@param isPasswordField boolean
---@param maskChar System.Char
---@return UnityEngine.UIElements.TextField
function UnityEngine.UIElements.TextField.New(label, maxLength, multiline, isPasswordField, maskChar) end
---@param newValue string
function UnityEngine.UIElements.TextField:SetValueWithoutNotify(newValue) end

---@class UnityEngine.UIElements.TextField.TextInput : UnityEngine.UIElements.TextInputBaseField.TextInputBase
---@field multiline boolean
---@field isPasswordField boolean
UnityEngine.UIElements.TextField.TextInput = {}
---@alias CS.UnityEngine.UIElements.TextField.TextInput UnityEngine.UIElements.TextField.TextInput
CS.UnityEngine.UIElements.TextField.TextInput = UnityEngine.UIElements.TextField.TextInput

---@return UnityEngine.UIElements.TextField.TextInput
function UnityEngine.UIElements.TextField.TextInput.New() end

---@class UnityEngine.UIElements.TextField.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.TextField.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.TextField.UxmlFactory UnityEngine.UIElements.TextField.UxmlFactory
CS.UnityEngine.UIElements.TextField.UxmlFactory = UnityEngine.UIElements.TextField.UxmlFactory

---@return UnityEngine.UIElements.TextField.UxmlFactory
function UnityEngine.UIElements.TextField.UxmlFactory.New() end

---@class UnityEngine.UIElements.TextField.UxmlTraits : UnityEngine.UIElements.TextInputBaseField.UxmlTraits
UnityEngine.UIElements.TextField.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.TextField.UxmlTraits UnityEngine.UIElements.TextField.UxmlTraits
CS.UnityEngine.UIElements.TextField.UxmlTraits = UnityEngine.UIElements.TextField.UxmlTraits

---@return UnityEngine.UIElements.TextField.UxmlTraits
function UnityEngine.UIElements.TextField.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.TextField.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.TextInputBaseField : UnityEngine.UIElements.BaseField[TValueType]
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
---@field singleLineInputUssClassName string
---@field multilineInputUssClassName string
---@field textInputUssName string
---@field text string
---@field isReadOnly boolean
---@field isPasswordField boolean
---@field autoCorrection boolean
---@field hideMobileInput boolean
---@field keyboardType UnityEngine.TouchScreenKeyboardType
---@field touchScreenKeyboard UnityEngine.TouchScreenKeyboard
---@field textSelection UnityEngine.UIElements.ITextSelection
---@field textEdition UnityEngine.UIElements.ITextEdition
---@field selectionColor UnityEngine.Color
---@field cursorColor UnityEngine.Color
---@field cursorIndex number
---@field cursorPosition UnityEngine.Vector2
---@field selectIndex number
---@field selectAllOnFocus boolean
---@field selectAllOnMouseUp boolean
---@field maxLength number
---@field doubleClickSelectsWord boolean
---@field tripleClickSelectsLine boolean
---@field isDelayed boolean
---@field maskChar System.Char
UnityEngine.UIElements.TextInputBaseField = {}
---@alias CS.UnityEngine.UIElements.TextInputBaseField UnityEngine.UIElements.TextInputBaseField
CS.UnityEngine.UIElements.TextInputBaseField = UnityEngine.UIElements.TextInputBaseField

function UnityEngine.UIElements.TextInputBaseField:SelectAll() end
function UnityEngine.UIElements.TextInputBaseField:SelectNone() end
---@param cursorIndex number
---@param selectionIndex number
function UnityEngine.UIElements.TextInputBaseField:SelectRange(cursorIndex, selectionIndex) end
---@param sv UnityEngine.UIElements.ScrollerVisibility
---@return boolean
function UnityEngine.UIElements.TextInputBaseField:SetVerticalScrollerVisibility(sv) end
---@param textToMeasure string
---@param width number
---@param widthMode UnityEngine.UIElements.VisualElement.MeasureMode
---@param height number
---@param heightMode UnityEngine.UIElements.VisualElement.MeasureMode
---@return UnityEngine.Vector2
function UnityEngine.UIElements.TextInputBaseField:MeasureTextSize(textToMeasure, width, widthMode, height, heightMode) end

---@class UnityEngine.UIElements.TextInputBaseField.TextInputBase : UnityEngine.UIElements.VisualElement
---@field innerComponentsModifierName string
---@field innerTextElementUssClassName string
---@field horizontalVariantInnerTextElementUssClassName string
---@field verticalVariantInnerTextElementUssClassName string
---@field verticalHorizontalVariantInnerTextElementUssClassName string
---@field innerScrollviewUssClassName string
---@field innerViewportUssClassName string
---@field innerContentContainerUssClassName string
---@field textSelection UnityEngine.UIElements.ITextSelection
---@field textEdition UnityEngine.UIElements.ITextEdition
---@field isReadOnly boolean
---@field maxLength number
---@field maskChar System.Char
---@field isPasswordField boolean
---@field selectionColor UnityEngine.Color
---@field cursorColor UnityEngine.Color
---@field cursorIndex number
---@field selectIndex number
---@field doubleClickSelectsWord boolean
---@field tripleClickSelectsLine boolean
---@field text string
UnityEngine.UIElements.TextInputBaseField.TextInputBase = {}
---@alias CS.UnityEngine.UIElements.TextInputBaseField.TextInputBase UnityEngine.UIElements.TextInputBaseField.TextInputBase
CS.UnityEngine.UIElements.TextInputBaseField.TextInputBase = UnityEngine.UIElements.TextInputBaseField.TextInputBase

function UnityEngine.UIElements.TextInputBaseField.TextInputBase:SelectAll() end
function UnityEngine.UIElements.TextInputBaseField.TextInputBase:ResetValueAndText() end

---@class UnityEngine.UIElements.TextInputBaseField.UxmlTraits : UnityEngine.UIElements.BaseFieldTraits
UnityEngine.UIElements.TextInputBaseField.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.TextInputBaseField.UxmlTraits UnityEngine.UIElements.TextInputBaseField.UxmlTraits
CS.UnityEngine.UIElements.TextInputBaseField.UxmlTraits = UnityEngine.UIElements.TextInputBaseField.UxmlTraits

---@return UnityEngine.UIElements.TextInputBaseField.UxmlTraits
function UnityEngine.UIElements.TextInputBaseField.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.TextInputBaseField.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.TextNative : System.Object
UnityEngine.UIElements.TextNative = {}
---@alias CS.UnityEngine.UIElements.TextNative UnityEngine.UIElements.TextNative
CS.UnityEngine.UIElements.TextNative = UnityEngine.UIElements.TextNative

---@param settings UnityEngine.UIElements.TextNativeSettings
---@param rect UnityEngine.Rect
---@param cursorIndex number
---@return UnityEngine.Vector2
function UnityEngine.UIElements.TextNative.GetCursorPosition(settings, rect, cursorIndex) end
---@param settings UnityEngine.UIElements.TextNativeSettings
---@return number
function UnityEngine.UIElements.TextNative.ComputeTextWidth(settings) end
---@param settings UnityEngine.UIElements.TextNativeSettings
---@return number
function UnityEngine.UIElements.TextNative.ComputeTextHeight(settings) end
---@param settings UnityEngine.UIElements.TextNativeSettings
---@return Unity.Collections.NativeArray
function UnityEngine.UIElements.TextNative.GetVertices(settings) end
---@param settings UnityEngine.UIElements.TextNativeSettings
---@param screenRect UnityEngine.Rect
---@return UnityEngine.Vector2
function UnityEngine.UIElements.TextNative.GetOffset(settings, screenRect) end
---@param worldMatrix UnityEngine.Matrix4x4
---@param pixelsPerPoint number
---@return number
function UnityEngine.UIElements.TextNative.ComputeTextScaling(worldMatrix, pixelsPerPoint) end

---@class UnityEngine.UIElements.TextNativeSettings : System.ValueType
---@field text string
---@field font UnityEngine.Font
---@field size number
---@field scaling number
---@field style UnityEngine.FontStyle
---@field color UnityEngine.Color
---@field anchor UnityEngine.TextAnchor
---@field wordWrap boolean
---@field wordWrapWidth number
---@field richText boolean
UnityEngine.UIElements.TextNativeSettings = {}
---@alias CS.UnityEngine.UIElements.TextNativeSettings UnityEngine.UIElements.TextNativeSettings
CS.UnityEngine.UIElements.TextNativeSettings = UnityEngine.UIElements.TextNativeSettings


---@class UnityEngine.UIElements.TextOverflow
---@field Clip UnityEngine.UIElements.TextOverflow
---@field Ellipsis UnityEngine.UIElements.TextOverflow
UnityEngine.UIElements.TextOverflow = {}
---@alias CS.UnityEngine.UIElements.TextOverflow UnityEngine.UIElements.TextOverflow
CS.UnityEngine.UIElements.TextOverflow = UnityEngine.UIElements.TextOverflow


---@class UnityEngine.UIElements.TextOverflowPosition
---@field End UnityEngine.UIElements.TextOverflowPosition
---@field Start UnityEngine.UIElements.TextOverflowPosition
---@field Middle UnityEngine.UIElements.TextOverflowPosition
UnityEngine.UIElements.TextOverflowPosition = {}
---@alias CS.UnityEngine.UIElements.TextOverflowPosition UnityEngine.UIElements.TextOverflowPosition
CS.UnityEngine.UIElements.TextOverflowPosition = UnityEngine.UIElements.TextOverflowPosition


---@class UnityEngine.UIElements.TextSelectingManipulator : System.Object
UnityEngine.UIElements.TextSelectingManipulator = {}
---@alias CS.UnityEngine.UIElements.TextSelectingManipulator UnityEngine.UIElements.TextSelectingManipulator
CS.UnityEngine.UIElements.TextSelectingManipulator = UnityEngine.UIElements.TextSelectingManipulator

---@param textElement UnityEngine.UIElements.TextElement
---@return UnityEngine.UIElements.TextSelectingManipulator
function UnityEngine.UIElements.TextSelectingManipulator.New(textElement) end

---@class UnityEngine.UIElements.TextShadow : System.ValueType
---@field offset UnityEngine.Vector2
---@field blurRadius number
---@field color UnityEngine.Color
UnityEngine.UIElements.TextShadow = {}
---@alias CS.UnityEngine.UIElements.TextShadow UnityEngine.UIElements.TextShadow
CS.UnityEngine.UIElements.TextShadow = UnityEngine.UIElements.TextShadow

---@overload fun(self: UnityEngine.UIElements.TextShadow, obj: System.Object) : boolean
---@param other UnityEngine.UIElements.TextShadow
---@return boolean
function UnityEngine.UIElements.TextShadow:Equals(other) end
---@return number
function UnityEngine.UIElements.TextShadow:GetHashCode() end
---@return string
function UnityEngine.UIElements.TextShadow:ToString() end

---@class UnityEngine.UIElements.TextureId : System.ValueType
---@field invalid UnityEngine.UIElements.TextureId
---@field index number
UnityEngine.UIElements.TextureId = {}
---@alias CS.UnityEngine.UIElements.TextureId UnityEngine.UIElements.TextureId
CS.UnityEngine.UIElements.TextureId = UnityEngine.UIElements.TextureId

---@param index number
---@return UnityEngine.UIElements.TextureId
function UnityEngine.UIElements.TextureId.New(index) end
---@return boolean
function UnityEngine.UIElements.TextureId:IsValid() end
---@return number
function UnityEngine.UIElements.TextureId:ConvertToGpu() end
---@overload fun(self: UnityEngine.UIElements.TextureId, obj: System.Object) : boolean
---@param other UnityEngine.UIElements.TextureId
---@return boolean
function UnityEngine.UIElements.TextureId:Equals(other) end
---@return number
function UnityEngine.UIElements.TextureId:GetHashCode() end

---@class UnityEngine.UIElements.TextureRegistry : System.Object
---@field instance UnityEngine.UIElements.TextureRegistry
UnityEngine.UIElements.TextureRegistry = {}
---@alias CS.UnityEngine.UIElements.TextureRegistry UnityEngine.UIElements.TextureRegistry
CS.UnityEngine.UIElements.TextureRegistry = UnityEngine.UIElements.TextureRegistry

---@return UnityEngine.UIElements.TextureRegistry
function UnityEngine.UIElements.TextureRegistry.New() end
---@param id UnityEngine.UIElements.TextureId
---@return UnityEngine.Texture
function UnityEngine.UIElements.TextureRegistry:GetTexture(id) end
---@return UnityEngine.UIElements.TextureId
function UnityEngine.UIElements.TextureRegistry:AllocAndAcquireDynamic() end
---@param id UnityEngine.UIElements.TextureId
---@param texture UnityEngine.Texture
function UnityEngine.UIElements.TextureRegistry:UpdateDynamic(id, texture) end
---@overload fun(self: UnityEngine.UIElements.TextureRegistry, tex: UnityEngine.Texture) : UnityEngine.UIElements.TextureId
---@param id UnityEngine.UIElements.TextureId
function UnityEngine.UIElements.TextureRegistry:Acquire(id) end
---@param id UnityEngine.UIElements.TextureId
function UnityEngine.UIElements.TextureRegistry:Release(id) end
---@param texture UnityEngine.Texture
---@return UnityEngine.UIElements.TextureId
function UnityEngine.UIElements.TextureRegistry:TextureToId(texture) end
---@return UnityEngine.UIElements.TextureRegistry.Statistics
function UnityEngine.UIElements.TextureRegistry:GatherStatistics() end

---@class UnityEngine.UIElements.TextureRegistry.Statistics : System.ValueType
---@field freeIdsCount number
---@field createdIdsCount number
---@field allocatedIdsTotalCount number
---@field allocatedIdsDynamicCount number
---@field allocatedIdsStaticCount number
---@field availableIdsCount number
UnityEngine.UIElements.TextureRegistry.Statistics = {}
---@alias CS.UnityEngine.UIElements.TextureRegistry.Statistics UnityEngine.UIElements.TextureRegistry.Statistics
CS.UnityEngine.UIElements.TextureRegistry.Statistics = UnityEngine.UIElements.TextureRegistry.Statistics


---@class UnityEngine.UIElements.TextureRegistry.TextureInfo : System.ValueType
---@field texture UnityEngine.Texture
---@field dynamic boolean
---@field refCount number
UnityEngine.UIElements.TextureRegistry.TextureInfo = {}
---@alias CS.UnityEngine.UIElements.TextureRegistry.TextureInfo UnityEngine.UIElements.TextureRegistry.TextureInfo
CS.UnityEngine.UIElements.TextureRegistry.TextureInfo = UnityEngine.UIElements.TextureRegistry.TextureInfo


---@class UnityEngine.UIElements.TextUtilities : System.Object
UnityEngine.UIElements.TextUtilities = {}
---@alias CS.UnityEngine.UIElements.TextUtilities UnityEngine.UIElements.TextUtilities
CS.UnityEngine.UIElements.TextUtilities = UnityEngine.UIElements.TextUtilities


---@class UnityEngine.UIElements.TextValueField : UnityEngine.UIElements.TextInputBaseField[TValueType]
---@field formatString string
---@field value TValueType
UnityEngine.UIElements.TextValueField = {}
---@alias CS.UnityEngine.UIElements.TextValueField UnityEngine.UIElements.TextValueField
CS.UnityEngine.UIElements.TextValueField = UnityEngine.UIElements.TextValueField

---@param delta UnityEngine.Vector3
---@param speed UnityEngine.UIElements.DeltaSpeed
---@param startValue TValueType
function UnityEngine.UIElements.TextValueField:ApplyInputDeviceDelta(delta, speed, startValue) end
function UnityEngine.UIElements.TextValueField:StartDragging() end
function UnityEngine.UIElements.TextValueField:StopDragging() end
---@param newValue TValueType
function UnityEngine.UIElements.TextValueField:SetValueWithoutNotify(newValue) end

---@class UnityEngine.UIElements.TextValueField.TextValueInput : UnityEngine.UIElements.TextInputBaseField.TextInputBase[TValueType]
---@field formatString string
UnityEngine.UIElements.TextValueField.TextValueInput = {}
---@alias CS.UnityEngine.UIElements.TextValueField.TextValueInput UnityEngine.UIElements.TextValueField.TextValueInput
CS.UnityEngine.UIElements.TextValueField.TextValueInput = UnityEngine.UIElements.TextValueField.TextValueInput

---@param delta UnityEngine.Vector3
---@param speed UnityEngine.UIElements.DeltaSpeed
---@param startValue TValueType
function UnityEngine.UIElements.TextValueField.TextValueInput:ApplyInputDeviceDelta(delta, speed, startValue) end
function UnityEngine.UIElements.TextValueField.TextValueInput:StartDragging() end
function UnityEngine.UIElements.TextValueField.TextValueInput:StopDragging() end

---@class UnityEngine.UIElements.TextValueFieldTraits : UnityEngine.UIElements.BaseFieldTraits[TValueType,TValueUxmlAttributeType]
UnityEngine.UIElements.TextValueFieldTraits = {}
---@alias CS.UnityEngine.UIElements.TextValueFieldTraits UnityEngine.UIElements.TextValueFieldTraits
CS.UnityEngine.UIElements.TextValueFieldTraits = UnityEngine.UIElements.TextValueFieldTraits

---@return UnityEngine.UIElements.TextValueFieldTraits
function UnityEngine.UIElements.TextValueFieldTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.TextValueFieldTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.TextVertex : System.ValueType
---@field position UnityEngine.Vector3
---@field color UnityEngine.Color32
---@field uv0 UnityEngine.Vector2
UnityEngine.UIElements.TextVertex = {}
---@alias CS.UnityEngine.UIElements.TextVertex UnityEngine.UIElements.TextVertex
CS.UnityEngine.UIElements.TextVertex = UnityEngine.UIElements.TextVertex


---@class UnityEngine.UIElements.ThemeStyleSheet : UnityEngine.UIElements.StyleSheet
UnityEngine.UIElements.ThemeStyleSheet = {}
---@alias CS.UnityEngine.UIElements.ThemeStyleSheet UnityEngine.UIElements.ThemeStyleSheet
CS.UnityEngine.UIElements.ThemeStyleSheet = UnityEngine.UIElements.ThemeStyleSheet

---@return UnityEngine.UIElements.ThemeStyleSheet
function UnityEngine.UIElements.ThemeStyleSheet.New() end

---@class UnityEngine.UIElements.TimeMsFunction : System.MulticastDelegate
UnityEngine.UIElements.TimeMsFunction = {}
---@alias CS.UnityEngine.UIElements.TimeMsFunction UnityEngine.UIElements.TimeMsFunction
CS.UnityEngine.UIElements.TimeMsFunction = UnityEngine.UIElements.TimeMsFunction

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.UIElements.TimeMsFunction
function UnityEngine.UIElements.TimeMsFunction.New(object, method) end
---@return number
function UnityEngine.UIElements.TimeMsFunction:Invoke() end
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.UIElements.TimeMsFunction:BeginInvoke(callback, object) end
---@param result System.IAsyncResult
---@return number
function UnityEngine.UIElements.TimeMsFunction:EndInvoke(result) end

---@class UnityEngine.UIElements.TimerEventScheduler : System.Object
UnityEngine.UIElements.TimerEventScheduler = {}
---@alias CS.UnityEngine.UIElements.TimerEventScheduler UnityEngine.UIElements.TimerEventScheduler
CS.UnityEngine.UIElements.TimerEventScheduler = UnityEngine.UIElements.TimerEventScheduler

---@return UnityEngine.UIElements.TimerEventScheduler
function UnityEngine.UIElements.TimerEventScheduler.New() end
---@param item UnityEngine.UIElements.ScheduledItem
function UnityEngine.UIElements.TimerEventScheduler:Schedule(item) end
---@param timerUpdateEvent System.Action | function
---@param delayMs number
---@return UnityEngine.UIElements.ScheduledItem
function UnityEngine.UIElements.TimerEventScheduler:ScheduleOnce(timerUpdateEvent, delayMs) end
---@param timerUpdateEvent System.Action | function
---@param delayMs number
---@param intervalMs number
---@param stopCondition System.Func
---@return UnityEngine.UIElements.ScheduledItem
function UnityEngine.UIElements.TimerEventScheduler:ScheduleUntil(timerUpdateEvent, delayMs, intervalMs, stopCondition) end
---@param timerUpdateEvent System.Action | function
---@param delayMs number
---@param intervalMs number
---@param durationMs number
---@return UnityEngine.UIElements.ScheduledItem
function UnityEngine.UIElements.TimerEventScheduler:ScheduleForDuration(timerUpdateEvent, delayMs, intervalMs, durationMs) end
---@param item UnityEngine.UIElements.ScheduledItem
function UnityEngine.UIElements.TimerEventScheduler:Unschedule(item) end
function UnityEngine.UIElements.TimerEventScheduler:UpdateScheduledEvents() end

---@class UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem : UnityEngine.UIElements.ScheduledItem
UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem = {}
---@alias CS.UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem
CS.UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem = UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem

---@param updateEvent System.Action | function
---@return UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem
function UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem.New(updateEvent) end
---@param state UnityEngine.UIElements.TimerState
function UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem:PerformTimerUpdate(state) end
---@return string
function UnityEngine.UIElements.TimerEventScheduler.TimerEventSchedulerItem:ToString() end

---@class UnityEngine.UIElements.TimerState : System.ValueType
---@field start number
---@field now number
---@field deltaTime number
UnityEngine.UIElements.TimerState = {}
---@alias CS.UnityEngine.UIElements.TimerState UnityEngine.UIElements.TimerState
CS.UnityEngine.UIElements.TimerState = UnityEngine.UIElements.TimerState

---@overload fun(self: UnityEngine.UIElements.TimerState, obj: System.Object) : boolean
---@param other UnityEngine.UIElements.TimerState
---@return boolean
function UnityEngine.UIElements.TimerState:Equals(other) end
---@return number
function UnityEngine.UIElements.TimerState:GetHashCode() end

---@class UnityEngine.UIElements.TimeUnit
---@field Second UnityEngine.UIElements.TimeUnit
---@field Millisecond UnityEngine.UIElements.TimeUnit
UnityEngine.UIElements.TimeUnit = {}
---@alias CS.UnityEngine.UIElements.TimeUnit UnityEngine.UIElements.TimeUnit
CS.UnityEngine.UIElements.TimeUnit = UnityEngine.UIElements.TimeUnit


---@class UnityEngine.UIElements.TimeValue : System.ValueType
---@field value number
---@field unit UnityEngine.UIElements.TimeUnit
UnityEngine.UIElements.TimeValue = {}
---@alias CS.UnityEngine.UIElements.TimeValue UnityEngine.UIElements.TimeValue
CS.UnityEngine.UIElements.TimeValue = UnityEngine.UIElements.TimeValue

---@overload fun(value: number) : UnityEngine.UIElements.TimeValue
---@param value number
---@param unit UnityEngine.UIElements.TimeUnit
---@return UnityEngine.UIElements.TimeValue
function UnityEngine.UIElements.TimeValue.New(value, unit) end
---@overload fun(self: UnityEngine.UIElements.TimeValue, other: UnityEngine.UIElements.TimeValue) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.TimeValue:Equals(obj) end
---@return number
function UnityEngine.UIElements.TimeValue:GetHashCode() end
---@return string
function UnityEngine.UIElements.TimeValue:ToString() end

---@class UnityEngine.UIElements.Toggle : UnityEngine.UIElements.BaseBoolField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
---@field checkmarkUssClassName string
---@field textUssClassName string
---@field mixedValuesUssClassName string
UnityEngine.UIElements.Toggle = {}
---@alias CS.UnityEngine.UIElements.Toggle UnityEngine.UIElements.Toggle
CS.UnityEngine.UIElements.Toggle = UnityEngine.UIElements.Toggle

---@overload fun() : UnityEngine.UIElements.Toggle
---@param label string
---@return UnityEngine.UIElements.Toggle
function UnityEngine.UIElements.Toggle.New(label) end

---@class UnityEngine.UIElements.Toggle.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.Toggle.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.Toggle.UxmlFactory UnityEngine.UIElements.Toggle.UxmlFactory
CS.UnityEngine.UIElements.Toggle.UxmlFactory = UnityEngine.UIElements.Toggle.UxmlFactory

---@return UnityEngine.UIElements.Toggle.UxmlFactory
function UnityEngine.UIElements.Toggle.UxmlFactory.New() end

---@class UnityEngine.UIElements.Toggle.UxmlTraits : UnityEngine.UIElements.BaseFieldTraits
UnityEngine.UIElements.Toggle.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.Toggle.UxmlTraits UnityEngine.UIElements.Toggle.UxmlTraits
CS.UnityEngine.UIElements.Toggle.UxmlTraits = UnityEngine.UIElements.Toggle.UxmlTraits

---@return UnityEngine.UIElements.Toggle.UxmlTraits
function UnityEngine.UIElements.Toggle.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.Toggle.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.TooltipEvent : UnityEngine.UIElements.EventBase
---@field tooltip string
---@field rect UnityEngine.Rect
UnityEngine.UIElements.TooltipEvent = {}
---@alias CS.UnityEngine.UIElements.TooltipEvent UnityEngine.UIElements.TooltipEvent
CS.UnityEngine.UIElements.TooltipEvent = UnityEngine.UIElements.TooltipEvent

---@return UnityEngine.UIElements.TooltipEvent
function UnityEngine.UIElements.TooltipEvent.New() end

---@class UnityEngine.UIElements.TouchScreenTextEditorEventHandler : UnityEngine.UIElements.TextEditorEventHandler
UnityEngine.UIElements.TouchScreenTextEditorEventHandler = {}
---@alias CS.UnityEngine.UIElements.TouchScreenTextEditorEventHandler UnityEngine.UIElements.TouchScreenTextEditorEventHandler
CS.UnityEngine.UIElements.TouchScreenTextEditorEventHandler = UnityEngine.UIElements.TouchScreenTextEditorEventHandler

---@param textElement UnityEngine.UIElements.TextElement
---@param editingUtilities UnityEngine.TextEditingUtilities
---@return UnityEngine.UIElements.TouchScreenTextEditorEventHandler
function UnityEngine.UIElements.TouchScreenTextEditorEventHandler.New(textElement, editingUtilities) end
---@param evt UnityEngine.UIElements.EventBase
function UnityEngine.UIElements.TouchScreenTextEditorEventHandler:ExecuteDefaultActionAtTarget(evt) end

---@class UnityEngine.UIElements.TransformData : System.ValueType
---@field rotate UnityEngine.UIElements.Rotate
---@field scale UnityEngine.UIElements.Scale
---@field transformOrigin UnityEngine.UIElements.TransformOrigin
---@field translate UnityEngine.UIElements.Translate
UnityEngine.UIElements.TransformData = {}
---@alias CS.UnityEngine.UIElements.TransformData UnityEngine.UIElements.TransformData
CS.UnityEngine.UIElements.TransformData = UnityEngine.UIElements.TransformData

---@return UnityEngine.UIElements.TransformData
function UnityEngine.UIElements.TransformData:Copy() end
---@param ref_other UnityEngine.UIElements.TransformData
---@return UnityEngine.UIElements.TransformData
function UnityEngine.UIElements.TransformData:CopyFrom(ref_other) end
---@overload fun(self: UnityEngine.UIElements.TransformData, other: UnityEngine.UIElements.TransformData) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.TransformData:Equals(obj) end
---@return number
function UnityEngine.UIElements.TransformData:GetHashCode() end

---@class UnityEngine.UIElements.TransformOrigin : System.ValueType
---@field x UnityEngine.UIElements.Length
---@field y UnityEngine.UIElements.Length
---@field z number
UnityEngine.UIElements.TransformOrigin = {}
---@alias CS.UnityEngine.UIElements.TransformOrigin UnityEngine.UIElements.TransformOrigin
CS.UnityEngine.UIElements.TransformOrigin = UnityEngine.UIElements.TransformOrigin

---@overload fun(x: UnityEngine.UIElements.Length, y: UnityEngine.UIElements.Length, z: number) : UnityEngine.UIElements.TransformOrigin
---@param x UnityEngine.UIElements.Length
---@param y UnityEngine.UIElements.Length
---@return UnityEngine.UIElements.TransformOrigin
function UnityEngine.UIElements.TransformOrigin.New(x, y) end
---@return UnityEngine.UIElements.TransformOrigin
function UnityEngine.UIElements.TransformOrigin.Initial() end
---@overload fun(self: UnityEngine.UIElements.TransformOrigin, other: UnityEngine.UIElements.TransformOrigin) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.TransformOrigin:Equals(obj) end
---@return number
function UnityEngine.UIElements.TransformOrigin:GetHashCode() end
---@return string
function UnityEngine.UIElements.TransformOrigin:ToString() end

---@class UnityEngine.UIElements.TransformOriginField : UnityEngine.UIElements.BaseField
UnityEngine.UIElements.TransformOriginField = {}
---@alias CS.UnityEngine.UIElements.TransformOriginField UnityEngine.UIElements.TransformOriginField
CS.UnityEngine.UIElements.TransformOriginField = UnityEngine.UIElements.TransformOriginField

---@overload fun() : UnityEngine.UIElements.TransformOriginField
---@overload fun(label: string) : UnityEngine.UIElements.TransformOriginField
---@param label string
---@param to UnityEngine.UIElements.TransformOrigin
---@return UnityEngine.UIElements.TransformOriginField
function UnityEngine.UIElements.TransformOriginField.New(label, to) end
---@param to UnityEngine.UIElements.TransformOrigin
function UnityEngine.UIElements.TransformOriginField:SetValueWithoutNotify(to) end

---@class UnityEngine.UIElements.TransformOriginOffset
---@field Left UnityEngine.UIElements.TransformOriginOffset
---@field Right UnityEngine.UIElements.TransformOriginOffset
---@field Top UnityEngine.UIElements.TransformOriginOffset
---@field Bottom UnityEngine.UIElements.TransformOriginOffset
---@field Center UnityEngine.UIElements.TransformOriginOffset
UnityEngine.UIElements.TransformOriginOffset = {}
---@alias CS.UnityEngine.UIElements.TransformOriginOffset UnityEngine.UIElements.TransformOriginOffset
CS.UnityEngine.UIElements.TransformOriginOffset = UnityEngine.UIElements.TransformOriginOffset


---@class UnityEngine.UIElements.TransitionCancelEvent : UnityEngine.UIElements.TransitionEventBase
UnityEngine.UIElements.TransitionCancelEvent = {}
---@alias CS.UnityEngine.UIElements.TransitionCancelEvent UnityEngine.UIElements.TransitionCancelEvent
CS.UnityEngine.UIElements.TransitionCancelEvent = UnityEngine.UIElements.TransitionCancelEvent

---@return UnityEngine.UIElements.TransitionCancelEvent
function UnityEngine.UIElements.TransitionCancelEvent.New() end

---@class UnityEngine.UIElements.TransitionData : System.ValueType
---@field transitionDelay System.Collections.Generic.List
---@field transitionDuration System.Collections.Generic.List
---@field transitionProperty System.Collections.Generic.List
---@field transitionTimingFunction System.Collections.Generic.List
UnityEngine.UIElements.TransitionData = {}
---@alias CS.UnityEngine.UIElements.TransitionData UnityEngine.UIElements.TransitionData
CS.UnityEngine.UIElements.TransitionData = UnityEngine.UIElements.TransitionData

---@return UnityEngine.UIElements.TransitionData
function UnityEngine.UIElements.TransitionData:Copy() end
---@param ref_other UnityEngine.UIElements.TransitionData
---@return UnityEngine.UIElements.TransitionData
function UnityEngine.UIElements.TransitionData:CopyFrom(ref_other) end
---@overload fun(self: UnityEngine.UIElements.TransitionData, other: UnityEngine.UIElements.TransitionData) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.TransitionData:Equals(obj) end
---@return number
function UnityEngine.UIElements.TransitionData:GetHashCode() end

---@class UnityEngine.UIElements.TransitionEndEvent : UnityEngine.UIElements.TransitionEventBase
UnityEngine.UIElements.TransitionEndEvent = {}
---@alias CS.UnityEngine.UIElements.TransitionEndEvent UnityEngine.UIElements.TransitionEndEvent
CS.UnityEngine.UIElements.TransitionEndEvent = UnityEngine.UIElements.TransitionEndEvent

---@return UnityEngine.UIElements.TransitionEndEvent
function UnityEngine.UIElements.TransitionEndEvent.New() end

---@class UnityEngine.UIElements.TransitionEventBase : UnityEngine.UIElements.EventBase[T]
---@field stylePropertyNames UnityEngine.UIElements.StylePropertyNameCollection
---@field elapsedTime number
UnityEngine.UIElements.TransitionEventBase = {}
---@alias CS.UnityEngine.UIElements.TransitionEventBase UnityEngine.UIElements.TransitionEventBase
CS.UnityEngine.UIElements.TransitionEventBase = UnityEngine.UIElements.TransitionEventBase

---@param stylePropertyName UnityEngine.UIElements.StylePropertyName
---@param elapsedTime number
---@return T
function UnityEngine.UIElements.TransitionEventBase.GetPooled(stylePropertyName, elapsedTime) end
---@param stylePropertyName UnityEngine.UIElements.StylePropertyName
---@return boolean
function UnityEngine.UIElements.TransitionEventBase:AffectsProperty(stylePropertyName) end

---@class UnityEngine.UIElements.TransitionRunEvent : UnityEngine.UIElements.TransitionEventBase
UnityEngine.UIElements.TransitionRunEvent = {}
---@alias CS.UnityEngine.UIElements.TransitionRunEvent UnityEngine.UIElements.TransitionRunEvent
CS.UnityEngine.UIElements.TransitionRunEvent = UnityEngine.UIElements.TransitionRunEvent

---@return UnityEngine.UIElements.TransitionRunEvent
function UnityEngine.UIElements.TransitionRunEvent.New() end

---@class UnityEngine.UIElements.TransitionStartEvent : UnityEngine.UIElements.TransitionEventBase
UnityEngine.UIElements.TransitionStartEvent = {}
---@alias CS.UnityEngine.UIElements.TransitionStartEvent UnityEngine.UIElements.TransitionStartEvent
CS.UnityEngine.UIElements.TransitionStartEvent = UnityEngine.UIElements.TransitionStartEvent

---@return UnityEngine.UIElements.TransitionStartEvent
function UnityEngine.UIElements.TransitionStartEvent.New() end

---@class UnityEngine.UIElements.Translate : System.ValueType
---@field x UnityEngine.UIElements.Length
---@field y UnityEngine.UIElements.Length
---@field z number
UnityEngine.UIElements.Translate = {}
---@alias CS.UnityEngine.UIElements.Translate UnityEngine.UIElements.Translate
CS.UnityEngine.UIElements.Translate = UnityEngine.UIElements.Translate

---@overload fun(x: UnityEngine.UIElements.Length, y: UnityEngine.UIElements.Length, z: number) : UnityEngine.UIElements.Translate
---@param x UnityEngine.UIElements.Length
---@param y UnityEngine.UIElements.Length
---@return UnityEngine.UIElements.Translate
function UnityEngine.UIElements.Translate.New(x, y) end
---@return UnityEngine.UIElements.Translate
function UnityEngine.UIElements.Translate.None() end
---@overload fun(self: UnityEngine.UIElements.Translate, other: UnityEngine.UIElements.Translate) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.Translate:Equals(obj) end
---@return number
function UnityEngine.UIElements.Translate:GetHashCode() end
---@return string
function UnityEngine.UIElements.Translate:ToString() end

---@class UnityEngine.UIElements.TranslateField : UnityEngine.UIElements.BaseField
UnityEngine.UIElements.TranslateField = {}
---@alias CS.UnityEngine.UIElements.TranslateField UnityEngine.UIElements.TranslateField
CS.UnityEngine.UIElements.TranslateField = UnityEngine.UIElements.TranslateField

---@overload fun() : UnityEngine.UIElements.TranslateField
---@overload fun(label: string) : UnityEngine.UIElements.TranslateField
---@param label string
---@param t UnityEngine.UIElements.Translate
---@return UnityEngine.UIElements.TranslateField
function UnityEngine.UIElements.TranslateField.New(label, t) end
---@param t UnityEngine.UIElements.Translate
function UnityEngine.UIElements.TranslateField:SetValueWithoutNotify(t) end

---@class UnityEngine.UIElements.TreeData : System.ValueType
---@field rootItemIds System.Collections.Generic.IEnumerable
UnityEngine.UIElements.TreeData = {}
---@alias CS.UnityEngine.UIElements.TreeData UnityEngine.UIElements.TreeData
CS.UnityEngine.UIElements.TreeData = UnityEngine.UIElements.TreeData

---@param rootItems System.Collections.Generic.IList[UnityEngine.UIElements.TreeViewItemData[T]]
---@return UnityEngine.UIElements.TreeData
function UnityEngine.UIElements.TreeData.New(rootItems) end
---@param id number
---@return UnityEngine.UIElements.TreeViewItemData[T]
function UnityEngine.UIElements.TreeData:GetDataForId(id) end
---@param id number
---@return number
function UnityEngine.UIElements.TreeData:GetParentId(id) end
---@param item UnityEngine.UIElements.TreeViewItemData[T]
---@param parentId number
---@param childIndex number
function UnityEngine.UIElements.TreeData:AddItem(item, parentId, childIndex) end
---@param id number
---@return boolean
function UnityEngine.UIElements.TreeData:TryRemove(id) end
---@param id number
---@param newParentId number
---@param childIndex number
function UnityEngine.UIElements.TreeData:Move(id, newParentId, childIndex) end
---@param childId number
---@param ancestorId number
---@return boolean
function UnityEngine.UIElements.TreeData:HasAncestor(childId, ancestorId) end

---@class UnityEngine.UIElements.TreeDataController : System.Object
UnityEngine.UIElements.TreeDataController = {}
---@alias CS.UnityEngine.UIElements.TreeDataController UnityEngine.UIElements.TreeDataController
CS.UnityEngine.UIElements.TreeDataController = UnityEngine.UIElements.TreeDataController

---@return UnityEngine.UIElements.TreeDataController
function UnityEngine.UIElements.TreeDataController.New() end
---@param rootItems System.Collections.Generic.IList[UnityEngine.UIElements.TreeViewItemData[T]]
function UnityEngine.UIElements.TreeDataController:SetRootItems(rootItems) end
---@param ref_item UnityEngine.UIElements.TreeViewItemData[T]
---@param parentId number
---@param childIndex number
---@return UnityEngine.UIElements.TreeViewItemData[T]
function UnityEngine.UIElements.TreeDataController:AddItem(ref_item, parentId, childIndex) end
---@param id number
---@return boolean
function UnityEngine.UIElements.TreeDataController:TryRemoveItem(id) end
---@param id number
---@return UnityEngine.UIElements.TreeViewItemData[T]
function UnityEngine.UIElements.TreeDataController:GetTreeItemDataForId(id) end
---@param id number
---@return T
function UnityEngine.UIElements.TreeDataController:GetDataForId(id) end
---@param id number
---@return number
function UnityEngine.UIElements.TreeDataController:GetParentId(id) end
---@param id number
---@return boolean
function UnityEngine.UIElements.TreeDataController:HasChildren(id) end
---@param id number
---@return System.Collections.Generic.IEnumerable
function UnityEngine.UIElements.TreeDataController:GetChildrenIds(id) end
---@param id number
---@param newParentId number
---@param childIndex number
function UnityEngine.UIElements.TreeDataController:Move(id, newParentId, childIndex) end
---@param childId number
---@param id number
---@return boolean
function UnityEngine.UIElements.TreeDataController:IsChildOf(childId, id) end
---@param rootIds System.Collections.Generic.IEnumerable
---@return System.Collections.Generic.IEnumerable
function UnityEngine.UIElements.TreeDataController:GetAllItemIds(rootIds) end

---@class UnityEngine.UIElements.TreeItem : System.ValueType
---@field invalidId number
---@field id number
---@field parentId number
---@field childrenIds System.Collections.Generic.IEnumerable
---@field hasChildren boolean
UnityEngine.UIElements.TreeItem = {}
---@alias CS.UnityEngine.UIElements.TreeItem UnityEngine.UIElements.TreeItem
CS.UnityEngine.UIElements.TreeItem = UnityEngine.UIElements.TreeItem

---@param id number
---@param parentId number
---@param childrenIds System.Collections.Generic.IEnumerable
---@return UnityEngine.UIElements.TreeItem
function UnityEngine.UIElements.TreeItem.New(id, parentId, childrenIds) end

---@class UnityEngine.UIElements.TreeView : UnityEngine.UIElements.BaseTreeView
---@field makeItem System.Func
---@field bindItem System.Action | function
---@field unbindItem System.Action | function
---@field destroyItem System.Action | function
---@field viewController UnityEngine.UIElements.TreeViewController
UnityEngine.UIElements.TreeView = {}
---@alias CS.UnityEngine.UIElements.TreeView UnityEngine.UIElements.TreeView
CS.UnityEngine.UIElements.TreeView = UnityEngine.UIElements.TreeView

---@overload fun() : UnityEngine.UIElements.TreeView
---@overload fun(makeItem: System.Func, bindItem: System.Action | function) : UnityEngine.UIElements.TreeView
---@param itemHeight number
---@param makeItem System.Func
---@param bindItem System.Action | function
---@return UnityEngine.UIElements.TreeView
function UnityEngine.UIElements.TreeView.New(itemHeight, makeItem, bindItem) end

---@class UnityEngine.UIElements.TreeView.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.TreeView.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.TreeView.UxmlFactory UnityEngine.UIElements.TreeView.UxmlFactory
CS.UnityEngine.UIElements.TreeView.UxmlFactory = UnityEngine.UIElements.TreeView.UxmlFactory

---@return UnityEngine.UIElements.TreeView.UxmlFactory
function UnityEngine.UIElements.TreeView.UxmlFactory.New() end

---@class UnityEngine.UIElements.TreeView.UxmlTraits : UnityEngine.UIElements.BaseTreeView.UxmlTraits
UnityEngine.UIElements.TreeView.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.TreeView.UxmlTraits UnityEngine.UIElements.TreeView.UxmlTraits
CS.UnityEngine.UIElements.TreeView.UxmlTraits = UnityEngine.UIElements.TreeView.UxmlTraits

---@return UnityEngine.UIElements.TreeView.UxmlTraits
function UnityEngine.UIElements.TreeView.UxmlTraits.New() end

---@class UnityEngine.UIElements.TreeViewController : UnityEngine.UIElements.BaseTreeViewController
UnityEngine.UIElements.TreeViewController = {}
---@alias CS.UnityEngine.UIElements.TreeViewController UnityEngine.UIElements.TreeViewController
CS.UnityEngine.UIElements.TreeViewController = UnityEngine.UIElements.TreeViewController


---@class UnityEngine.UIElements.TreeViewHelpers : System.Object
UnityEngine.UIElements.TreeViewHelpers = {}
---@alias CS.UnityEngine.UIElements.TreeViewHelpers UnityEngine.UIElements.TreeViewHelpers
CS.UnityEngine.UIElements.TreeViewHelpers = UnityEngine.UIElements.TreeViewHelpers


---@class UnityEngine.UIElements.TreeViewItemData : System.ValueType
---@field id number
---@field data T
---@field children System.Collections.Generic.IEnumerable[UnityEngine.UIElements.TreeViewItemData[T]]
---@field hasChildren boolean
UnityEngine.UIElements.TreeViewItemData = {}
---@alias CS.UnityEngine.UIElements.TreeViewItemData UnityEngine.UIElements.TreeViewItemData
CS.UnityEngine.UIElements.TreeViewItemData = UnityEngine.UIElements.TreeViewItemData

---@param id number
---@param data T
---@param children System.Collections.Generic.List[UnityEngine.UIElements.TreeViewItemData[T]]
---@return UnityEngine.UIElements.TreeViewItemData
function UnityEngine.UIElements.TreeViewItemData.New(id, data, children) end

---@class UnityEngine.UIElements.TreeViewItemWrapper : System.ValueType
---@field item UnityEngine.UIElements.TreeItem
---@field depth number
---@field id number
---@field parentId number
---@field childrenIds System.Collections.Generic.IEnumerable
---@field hasChildren boolean
UnityEngine.UIElements.TreeViewItemWrapper = {}
---@alias CS.UnityEngine.UIElements.TreeViewItemWrapper UnityEngine.UIElements.TreeViewItemWrapper
CS.UnityEngine.UIElements.TreeViewItemWrapper = UnityEngine.UIElements.TreeViewItemWrapper

---@param item UnityEngine.UIElements.TreeItem
---@param depth number
---@return UnityEngine.UIElements.TreeViewItemWrapper
function UnityEngine.UIElements.TreeViewItemWrapper.New(item, depth) end

---@class UnityEngine.UIElements.TreeViewReorderableDragAndDropController : UnityEngine.UIElements.BaseReorderableDragAndDropController
UnityEngine.UIElements.TreeViewReorderableDragAndDropController = {}
---@alias CS.UnityEngine.UIElements.TreeViewReorderableDragAndDropController UnityEngine.UIElements.TreeViewReorderableDragAndDropController
CS.UnityEngine.UIElements.TreeViewReorderableDragAndDropController = UnityEngine.UIElements.TreeViewReorderableDragAndDropController

---@param view UnityEngine.UIElements.BaseTreeView
---@return UnityEngine.UIElements.TreeViewReorderableDragAndDropController
function UnityEngine.UIElements.TreeViewReorderableDragAndDropController.New(view) end
---@param itemIds System.Collections.Generic.IEnumerable
---@param skipText boolean
---@return UnityEngine.UIElements.StartDragArgs
function UnityEngine.UIElements.TreeViewReorderableDragAndDropController:SetupDragAndDrop(itemIds, skipText) end
---@param args UnityEngine.UIElements.IListDragAndDropArgs
---@return UnityEngine.UIElements.DragVisualMode
function UnityEngine.UIElements.TreeViewReorderableDragAndDropController:HandleDragAndDrop(args) end
---@param args UnityEngine.UIElements.IListDragAndDropArgs
function UnityEngine.UIElements.TreeViewReorderableDragAndDropController:OnDrop(args) end
function UnityEngine.UIElements.TreeViewReorderableDragAndDropController:DragCleanup() end
---@param item UnityEngine.UIElements.ReusableCollectionItem
---@param pointerPosition UnityEngine.Vector2
function UnityEngine.UIElements.TreeViewReorderableDragAndDropController:HandleAutoExpand(item, pointerPosition) end

---@class UnityEngine.UIElements.TreeViewReorderableDragAndDropController.DropData : System.Object
---@field expandedIdsBeforeDrag number[]
---@field draggedIds number[]
---@field lastItemId number
---@field expandItemBeginTimerMs number
---@field expandItemBeginPosition UnityEngine.Vector2
UnityEngine.UIElements.TreeViewReorderableDragAndDropController.DropData = {}
---@alias CS.UnityEngine.UIElements.TreeViewReorderableDragAndDropController.DropData UnityEngine.UIElements.TreeViewReorderableDragAndDropController.DropData
CS.UnityEngine.UIElements.TreeViewReorderableDragAndDropController.DropData = UnityEngine.UIElements.TreeViewReorderableDragAndDropController.DropData

---@return UnityEngine.UIElements.TreeViewReorderableDragAndDropController.DropData
function UnityEngine.UIElements.TreeViewReorderableDragAndDropController.DropData.New() end

---@class UnityEngine.UIElements.TrickleDown
---@field NoTrickleDown UnityEngine.UIElements.TrickleDown
---@field TrickleDown UnityEngine.UIElements.TrickleDown
UnityEngine.UIElements.TrickleDown = {}
---@alias CS.UnityEngine.UIElements.TrickleDown UnityEngine.UIElements.TrickleDown
CS.UnityEngine.UIElements.TrickleDown = UnityEngine.UIElements.TrickleDown


---@class UnityEngine.UIElements.TwoPaneSplitView : UnityEngine.UIElements.VisualElement
---@field fixedPane UnityEngine.UIElements.VisualElement
---@field flexedPane UnityEngine.UIElements.VisualElement
---@field fixedPaneIndex number
---@field fixedPaneInitialDimension number
---@field orientation UnityEngine.UIElements.TwoPaneSplitViewOrientation
---@field contentContainer UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.TwoPaneSplitView = {}
---@alias CS.UnityEngine.UIElements.TwoPaneSplitView UnityEngine.UIElements.TwoPaneSplitView
CS.UnityEngine.UIElements.TwoPaneSplitView = UnityEngine.UIElements.TwoPaneSplitView

---@overload fun() : UnityEngine.UIElements.TwoPaneSplitView
---@param fixedPaneIndex number
---@param fixedPaneStartDimension number
---@param orientation UnityEngine.UIElements.TwoPaneSplitViewOrientation
---@return UnityEngine.UIElements.TwoPaneSplitView
function UnityEngine.UIElements.TwoPaneSplitView.New(fixedPaneIndex, fixedPaneStartDimension, orientation) end
---@param index number
function UnityEngine.UIElements.TwoPaneSplitView:CollapseChild(index) end
function UnityEngine.UIElements.TwoPaneSplitView:UnCollapse() end

---@class UnityEngine.UIElements.TwoPaneSplitView.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.TwoPaneSplitView.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.TwoPaneSplitView.UxmlFactory UnityEngine.UIElements.TwoPaneSplitView.UxmlFactory
CS.UnityEngine.UIElements.TwoPaneSplitView.UxmlFactory = UnityEngine.UIElements.TwoPaneSplitView.UxmlFactory

---@return UnityEngine.UIElements.TwoPaneSplitView.UxmlFactory
function UnityEngine.UIElements.TwoPaneSplitView.UxmlFactory.New() end

---@class UnityEngine.UIElements.TwoPaneSplitView.UxmlTraits : UnityEngine.UIElements.VisualElement.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.TwoPaneSplitView.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.TwoPaneSplitView.UxmlTraits UnityEngine.UIElements.TwoPaneSplitView.UxmlTraits
CS.UnityEngine.UIElements.TwoPaneSplitView.UxmlTraits = UnityEngine.UIElements.TwoPaneSplitView.UxmlTraits

---@return UnityEngine.UIElements.TwoPaneSplitView.UxmlTraits
function UnityEngine.UIElements.TwoPaneSplitView.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.TwoPaneSplitView.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.TwoPaneSplitViewOrientation
---@field Horizontal UnityEngine.UIElements.TwoPaneSplitViewOrientation
---@field Vertical UnityEngine.UIElements.TwoPaneSplitViewOrientation
UnityEngine.UIElements.TwoPaneSplitViewOrientation = {}
---@alias CS.UnityEngine.UIElements.TwoPaneSplitViewOrientation UnityEngine.UIElements.TwoPaneSplitViewOrientation
CS.UnityEngine.UIElements.TwoPaneSplitViewOrientation = UnityEngine.UIElements.TwoPaneSplitViewOrientation


---@class UnityEngine.UIElements.TwoPaneSplitViewResizer : UnityEngine.UIElements.PointerManipulator
UnityEngine.UIElements.TwoPaneSplitViewResizer = {}
---@alias CS.UnityEngine.UIElements.TwoPaneSplitViewResizer UnityEngine.UIElements.TwoPaneSplitViewResizer
CS.UnityEngine.UIElements.TwoPaneSplitViewResizer = UnityEngine.UIElements.TwoPaneSplitViewResizer

---@param splitView UnityEngine.UIElements.TwoPaneSplitView
---@param dir number
---@return UnityEngine.UIElements.TwoPaneSplitViewResizer
function UnityEngine.UIElements.TwoPaneSplitViewResizer.New(splitView, dir) end
---@param delta number
function UnityEngine.UIElements.TwoPaneSplitViewResizer:ApplyDelta(delta) end

---@class UnityEngine.UIElements.TypedUxmlAttributeDescription : UnityEngine.UIElements.UxmlAttributeDescription
---@field defaultValue T
---@field defaultValueAsString string
UnityEngine.UIElements.TypedUxmlAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.TypedUxmlAttributeDescription UnityEngine.UIElements.TypedUxmlAttributeDescription
CS.UnityEngine.UIElements.TypedUxmlAttributeDescription = UnityEngine.UIElements.TypedUxmlAttributeDescription

---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return T
function UnityEngine.UIElements.TypedUxmlAttributeDescription:GetValueFromBag(bag, cc) end

---@class UnityEngine.UIElements.UIDocument : UnityEngine.MonoBehaviour
---@field panelSettings UnityEngine.UIElements.PanelSettings
---@field parentUI UnityEngine.UIElements.UIDocument
---@field visualTreeAsset UnityEngine.UIElements.VisualTreeAsset
---@field rootVisualElement UnityEngine.UIElements.VisualElement
---@field sortingOrder number
UnityEngine.UIElements.UIDocument = {}
---@alias CS.UnityEngine.UIElements.UIDocument UnityEngine.UIElements.UIDocument
CS.UnityEngine.UIElements.UIDocument = UnityEngine.UIElements.UIDocument


---@class UnityEngine.UIElements.UIDocumentHierarchicalIndex : System.ValueType
UnityEngine.UIElements.UIDocumentHierarchicalIndex = {}
---@alias CS.UnityEngine.UIElements.UIDocumentHierarchicalIndex UnityEngine.UIElements.UIDocumentHierarchicalIndex
CS.UnityEngine.UIElements.UIDocumentHierarchicalIndex = UnityEngine.UIElements.UIDocumentHierarchicalIndex

---@param other UnityEngine.UIElements.UIDocumentHierarchicalIndex
---@return number
function UnityEngine.UIElements.UIDocumentHierarchicalIndex:CompareTo(other) end
---@return string
function UnityEngine.UIElements.UIDocumentHierarchicalIndex:ToString() end

---@class UnityEngine.UIElements.UIDocumentHierarchicalIndexComparer : System.Object
UnityEngine.UIElements.UIDocumentHierarchicalIndexComparer = {}
---@alias CS.UnityEngine.UIElements.UIDocumentHierarchicalIndexComparer UnityEngine.UIElements.UIDocumentHierarchicalIndexComparer
CS.UnityEngine.UIElements.UIDocumentHierarchicalIndexComparer = UnityEngine.UIElements.UIDocumentHierarchicalIndexComparer

---@return UnityEngine.UIElements.UIDocumentHierarchicalIndexComparer
function UnityEngine.UIElements.UIDocumentHierarchicalIndexComparer.New() end
---@param x UnityEngine.UIElements.UIDocumentHierarchicalIndex
---@param y UnityEngine.UIElements.UIDocumentHierarchicalIndex
---@return number
function UnityEngine.UIElements.UIDocumentHierarchicalIndexComparer:Compare(x, y) end

---@class UnityEngine.UIElements.UIDocumentHierarchyUtil : System.Object
UnityEngine.UIElements.UIDocumentHierarchyUtil = {}
---@alias CS.UnityEngine.UIElements.UIDocumentHierarchyUtil UnityEngine.UIElements.UIDocumentHierarchyUtil
CS.UnityEngine.UIElements.UIDocumentHierarchyUtil = UnityEngine.UIElements.UIDocumentHierarchyUtil


---@class UnityEngine.UIElements.UIDocumentList : System.Object
UnityEngine.UIElements.UIDocumentList = {}
---@alias CS.UnityEngine.UIElements.UIDocumentList UnityEngine.UIElements.UIDocumentList
CS.UnityEngine.UIElements.UIDocumentList = UnityEngine.UIElements.UIDocumentList

---@return UnityEngine.UIElements.UIDocumentList
function UnityEngine.UIElements.UIDocumentList.New() end

---@class UnityEngine.UIElements.UIElementsBridge : System.Object
UnityEngine.UIElements.UIElementsBridge = {}
---@alias CS.UnityEngine.UIElements.UIElementsBridge UnityEngine.UIElements.UIElementsBridge
CS.UnityEngine.UIElements.UIElementsBridge = UnityEngine.UIElements.UIElementsBridge

---@param value number
function UnityEngine.UIElements.UIElementsBridge:SetWantsMouseJumping(value) end

---@class UnityEngine.UIElements.UIElementsPackageUtility : System.Object
UnityEngine.UIElements.UIElementsPackageUtility = {}
---@alias CS.UnityEngine.UIElements.UIElementsPackageUtility UnityEngine.UIElements.UIElementsPackageUtility
CS.UnityEngine.UIElements.UIElementsPackageUtility = UnityEngine.UIElements.UIElementsPackageUtility


---@class UnityEngine.UIElements.UIElementsRuntimeUtility : System.Object
UnityEngine.UIElements.UIElementsRuntimeUtility = {}
---@alias CS.UnityEngine.UIElements.UIElementsRuntimeUtility UnityEngine.UIElements.UIElementsRuntimeUtility
CS.UnityEngine.UIElements.UIElementsRuntimeUtility = UnityEngine.UIElements.UIElementsRuntimeUtility

---@param systemEvent UnityEngine.Event
---@return UnityEngine.UIElements.EventBase
function UnityEngine.UIElements.UIElementsRuntimeUtility.CreateEvent(systemEvent) end
---@param ownerObject UnityEngine.ScriptableObject
---@param createDelegate UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate
---@return UnityEngine.UIElements.BaseRuntimePanel
function UnityEngine.UIElements.UIElementsRuntimeUtility.FindOrCreateRuntimePanel(ownerObject, createDelegate) end
---@param ownerObject UnityEngine.ScriptableObject
function UnityEngine.UIElements.UIElementsRuntimeUtility.DisposeRuntimePanel(ownerObject) end
function UnityEngine.UIElements.UIElementsRuntimeUtility.RepaintOverlayPanels() end
function UnityEngine.UIElements.UIElementsRuntimeUtility.RepaintOffscreenPanels() end
---@param panel UnityEngine.UIElements.BaseRuntimePanel
function UnityEngine.UIElements.UIElementsRuntimeUtility.RepaintOverlayPanel(panel) end
---@param eventSystem UnityEngine.Object
function UnityEngine.UIElements.UIElementsRuntimeUtility.RegisterEventSystem(eventSystem) end
---@param eventSystem UnityEngine.Object
function UnityEngine.UIElements.UIElementsRuntimeUtility.UnregisterEventSystem(eventSystem) end
function UnityEngine.UIElements.UIElementsRuntimeUtility.UpdateRuntimePanels() end
function UnityEngine.UIElements.UIElementsRuntimeUtility.RegisterPlayerloopCallback() end
function UnityEngine.UIElements.UIElementsRuntimeUtility.UnregisterPlayerloopCallback() end

---@class UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate : System.MulticastDelegate
UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate = {}
---@alias CS.UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate
CS.UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate = UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate
function UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate.New(object, method) end
---@param ownerObject UnityEngine.ScriptableObject
---@return UnityEngine.UIElements.BaseRuntimePanel
function UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate:Invoke(ownerObject) end
---@param ownerObject UnityEngine.ScriptableObject
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate:BeginInvoke(ownerObject, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.UIElements.BaseRuntimePanel
function UnityEngine.UIElements.UIElementsRuntimeUtility.CreateRuntimePanelDelegate:EndInvoke(result) end

---@class UnityEngine.UIElements.UIElementsRuntimeUtilityNative : System.Object
UnityEngine.UIElements.UIElementsRuntimeUtilityNative = {}
---@alias CS.UnityEngine.UIElements.UIElementsRuntimeUtilityNative UnityEngine.UIElements.UIElementsRuntimeUtilityNative
CS.UnityEngine.UIElements.UIElementsRuntimeUtilityNative = UnityEngine.UIElements.UIElementsRuntimeUtilityNative

function UnityEngine.UIElements.UIElementsRuntimeUtilityNative.RepaintOverlayPanels() end
function UnityEngine.UIElements.UIElementsRuntimeUtilityNative.UpdateRuntimePanels() end
function UnityEngine.UIElements.UIElementsRuntimeUtilityNative.RepaintOffscreenPanels() end
function UnityEngine.UIElements.UIElementsRuntimeUtilityNative.RegisterPlayerloopCallback() end
function UnityEngine.UIElements.UIElementsRuntimeUtilityNative.UnregisterPlayerloopCallback() end
function UnityEngine.UIElements.UIElementsRuntimeUtilityNative.VisualElementCreation() end

---@class UnityEngine.UIElements.UIElementsUtility : System.Object
---@field hiddenClassName string
UnityEngine.UIElements.UIElementsUtility = {}
---@alias CS.UnityEngine.UIElements.UIElementsUtility UnityEngine.UIElements.UIElementsUtility
CS.UnityEngine.UIElements.UIElementsUtility = UnityEngine.UIElements.UIElementsUtility

---@param instanceID number
---@param panel UnityEngine.UIElements.Panel
function UnityEngine.UIElements.UIElementsUtility.RegisterCachedPanel(instanceID, panel) end
---@param instanceID number
function UnityEngine.UIElements.UIElementsUtility.RemoveCachedPanel(instanceID) end
---@param instanceID number
---@param out_panel UnityEngine.UIElements.Panel
---@return boolean, UnityEngine.UIElements.Panel
function UnityEngine.UIElements.UIElementsUtility.TryGetPanel(instanceID, out_panel) end

---@class UnityEngine.UIElements.UIEventRegistration : System.Object
UnityEngine.UIElements.UIEventRegistration = {}
---@alias CS.UnityEngine.UIElements.UIEventRegistration UnityEngine.UIElements.UIEventRegistration
CS.UnityEngine.UIElements.UIEventRegistration = UnityEngine.UIElements.UIEventRegistration


---@class UnityEngine.UIElements.UIPainter2D : System.Object
UnityEngine.UIElements.UIPainter2D = {}
---@alias CS.UnityEngine.UIElements.UIPainter2D UnityEngine.UIElements.UIPainter2D
CS.UnityEngine.UIElements.UIPainter2D = UnityEngine.UIElements.UIPainter2D

---@param computeBBox boolean
---@return System.IntPtr
function UnityEngine.UIElements.UIPainter2D.Create(computeBBox) end
---@param handle System.IntPtr
function UnityEngine.UIElements.UIPainter2D.Destroy(handle) end
---@param handle System.IntPtr
function UnityEngine.UIElements.UIPainter2D.Reset(handle) end
---@param handle System.IntPtr
---@return number
function UnityEngine.UIElements.UIPainter2D.GetLineWidth(handle) end
---@param handle System.IntPtr
---@param value number
function UnityEngine.UIElements.UIPainter2D.SetLineWidth(handle, value) end
---@param handle System.IntPtr
---@return UnityEngine.Color
function UnityEngine.UIElements.UIPainter2D.GetStrokeColor(handle) end
---@param handle System.IntPtr
---@param value UnityEngine.Color
function UnityEngine.UIElements.UIPainter2D.SetStrokeColor(handle, value) end
---@param handle System.IntPtr
---@return UnityEngine.Gradient
function UnityEngine.UIElements.UIPainter2D.GetStrokeGradient(handle) end
---@param handle System.IntPtr
---@param gradient UnityEngine.Gradient
function UnityEngine.UIElements.UIPainter2D.SetStrokeGradient(handle, gradient) end
---@param handle System.IntPtr
---@return UnityEngine.Color
function UnityEngine.UIElements.UIPainter2D.GetFillColor(handle) end
---@param handle System.IntPtr
---@param value UnityEngine.Color
function UnityEngine.UIElements.UIPainter2D.SetFillColor(handle, value) end
---@param handle System.IntPtr
---@return UnityEngine.UIElements.LineJoin
function UnityEngine.UIElements.UIPainter2D.GetLineJoin(handle) end
---@param handle System.IntPtr
---@param value UnityEngine.UIElements.LineJoin
function UnityEngine.UIElements.UIPainter2D.SetLineJoin(handle, value) end
---@param handle System.IntPtr
---@return UnityEngine.UIElements.LineCap
function UnityEngine.UIElements.UIPainter2D.GetLineCap(handle) end
---@param handle System.IntPtr
---@param value UnityEngine.UIElements.LineCap
function UnityEngine.UIElements.UIPainter2D.SetLineCap(handle, value) end
---@param handle System.IntPtr
---@return number
function UnityEngine.UIElements.UIPainter2D.GetMiterLimit(handle) end
---@param handle System.IntPtr
---@param value number
function UnityEngine.UIElements.UIPainter2D.SetMiterLimit(handle, value) end
---@param handle System.IntPtr
function UnityEngine.UIElements.UIPainter2D.BeginPath(handle) end
---@param handle System.IntPtr
---@param pos UnityEngine.Vector2
function UnityEngine.UIElements.UIPainter2D.MoveTo(handle, pos) end
---@param handle System.IntPtr
---@param pos UnityEngine.Vector2
function UnityEngine.UIElements.UIPainter2D.LineTo(handle, pos) end
---@param handle System.IntPtr
---@param p1 UnityEngine.Vector2
---@param p2 UnityEngine.Vector2
---@param radius number
function UnityEngine.UIElements.UIPainter2D.ArcTo(handle, p1, p2, radius) end
---@param handle System.IntPtr
---@param center UnityEngine.Vector2
---@param radius number
---@param startAngleRads number
---@param endAngleRads number
---@param direction UnityEngine.UIElements.ArcDirection
function UnityEngine.UIElements.UIPainter2D.Arc(handle, center, radius, startAngleRads, endAngleRads, direction) end
---@param handle System.IntPtr
---@param p1 UnityEngine.Vector2
---@param p2 UnityEngine.Vector2
---@param p3 UnityEngine.Vector2
function UnityEngine.UIElements.UIPainter2D.BezierCurveTo(handle, p1, p2, p3) end
---@param handle System.IntPtr
---@param p1 UnityEngine.Vector2
---@param p2 UnityEngine.Vector2
function UnityEngine.UIElements.UIPainter2D.QuadraticCurveTo(handle, p1, p2) end
---@param handle System.IntPtr
function UnityEngine.UIElements.UIPainter2D.ClosePath(handle) end
---@param handle System.IntPtr
---@return UnityEngine.Rect
function UnityEngine.UIElements.UIPainter2D.GetBBox(handle) end
---@param handle System.IntPtr
---@return UnityEngine.UIElements.MeshWriteDataInterface
function UnityEngine.UIElements.UIPainter2D.Stroke(handle) end
---@param handle System.IntPtr
---@param fillRule UnityEngine.UIElements.FillRule
---@return UnityEngine.UIElements.MeshWriteDataInterface
function UnityEngine.UIElements.UIPainter2D.Fill(handle, fillRule) end

---@class UnityEngine.UIElements.UIR.Alloc : System.ValueType
---@field start number
---@field size number
UnityEngine.UIElements.UIR.Alloc = {}
---@alias CS.UnityEngine.UIElements.UIR.Alloc UnityEngine.UIElements.UIR.Alloc
CS.UnityEngine.UIElements.UIR.Alloc = UnityEngine.UIElements.UIR.Alloc


---@class UnityEngine.UIElements.UIR.Allocator2D : System.Object
---@field minSize UnityEngine.Vector2Int
---@field maxSize UnityEngine.Vector2Int
---@field maxAllocSize UnityEngine.Vector2Int
UnityEngine.UIElements.UIR.Allocator2D = {}
---@alias CS.UnityEngine.UIElements.UIR.Allocator2D UnityEngine.UIElements.UIR.Allocator2D
CS.UnityEngine.UIElements.UIR.Allocator2D = UnityEngine.UIElements.UIR.Allocator2D

---@overload fun(minSize: number, maxSize: number, rowHeightBias: number) : UnityEngine.UIElements.UIR.Allocator2D
---@param minSize UnityEngine.Vector2Int
---@param maxSize UnityEngine.Vector2Int
---@param rowHeightBias number
---@return UnityEngine.UIElements.UIR.Allocator2D
function UnityEngine.UIElements.UIR.Allocator2D.New(minSize, maxSize, rowHeightBias) end
---@param width number
---@param height number
---@param out_alloc2D UnityEngine.UIElements.UIR.Allocator2D.Alloc2D
---@return boolean, UnityEngine.UIElements.UIR.Allocator2D.Alloc2D
function UnityEngine.UIElements.UIR.Allocator2D:TryAllocate(width, height, out_alloc2D) end
---@param alloc2D UnityEngine.UIElements.UIR.Allocator2D.Alloc2D
function UnityEngine.UIElements.UIR.Allocator2D:Free(alloc2D) end

---@class UnityEngine.UIElements.UIR.Allocator2D.Alloc2D : System.ValueType
---@field rect UnityEngine.RectInt
---@field row UnityEngine.UIElements.UIR.Allocator2D.Row
---@field alloc UnityEngine.UIElements.UIR.Alloc
UnityEngine.UIElements.UIR.Allocator2D.Alloc2D = {}
---@alias CS.UnityEngine.UIElements.UIR.Allocator2D.Alloc2D UnityEngine.UIElements.UIR.Allocator2D.Alloc2D
CS.UnityEngine.UIElements.UIR.Allocator2D.Alloc2D = UnityEngine.UIElements.UIR.Allocator2D.Alloc2D

---@param row UnityEngine.UIElements.UIR.Allocator2D.Row
---@param alloc UnityEngine.UIElements.UIR.Alloc
---@param width number
---@param height number
---@return UnityEngine.UIElements.UIR.Allocator2D.Alloc2D
function UnityEngine.UIElements.UIR.Allocator2D.Alloc2D.New(row, alloc, width, height) end

---@class UnityEngine.UIElements.UIR.Allocator2D.Area : System.Object
---@field rect UnityEngine.RectInt
---@field allocator UnityEngine.UIElements.UIR.BestFitAllocator
UnityEngine.UIElements.UIR.Allocator2D.Area = {}
---@alias CS.UnityEngine.UIElements.UIR.Allocator2D.Area UnityEngine.UIElements.UIR.Allocator2D.Area
CS.UnityEngine.UIElements.UIR.Allocator2D.Area = UnityEngine.UIElements.UIR.Allocator2D.Area

---@param rect UnityEngine.RectInt
---@return UnityEngine.UIElements.UIR.Allocator2D.Area
function UnityEngine.UIElements.UIR.Allocator2D.Area.New(rect) end

---@class UnityEngine.UIElements.UIR.Allocator2D.Row : UnityEngine.UIElements.UIR.LinkedPoolItem
---@field pool UnityEngine.UIElements.UIR.LinkedPool
---@field rect UnityEngine.RectInt
---@field area UnityEngine.UIElements.UIR.Allocator2D.Area
---@field allocator UnityEngine.UIElements.UIR.BestFitAllocator
---@field alloc UnityEngine.UIElements.UIR.Alloc
---@field next UnityEngine.UIElements.UIR.Allocator2D.Row
UnityEngine.UIElements.UIR.Allocator2D.Row = {}
---@alias CS.UnityEngine.UIElements.UIR.Allocator2D.Row UnityEngine.UIElements.UIR.Allocator2D.Row
CS.UnityEngine.UIElements.UIR.Allocator2D.Row = UnityEngine.UIElements.UIR.Allocator2D.Row

---@return UnityEngine.UIElements.UIR.Allocator2D.Row
function UnityEngine.UIElements.UIR.Allocator2D.Row.New() end

---@class UnityEngine.UIElements.UIR.BaseShaderInfoStorage : System.Object
---@field texture UnityEngine.Texture2D
UnityEngine.UIElements.UIR.BaseShaderInfoStorage = {}
---@alias CS.UnityEngine.UIElements.UIR.BaseShaderInfoStorage UnityEngine.UIElements.UIR.BaseShaderInfoStorage
CS.UnityEngine.UIElements.UIR.BaseShaderInfoStorage = UnityEngine.UIElements.UIR.BaseShaderInfoStorage

---@param width number
---@param height number
---@param out_uvs UnityEngine.RectInt
---@return boolean, UnityEngine.RectInt
function UnityEngine.UIElements.UIR.BaseShaderInfoStorage:AllocateRect(width, height, out_uvs) end
---@param x number
---@param y number
---@param color UnityEngine.Color
function UnityEngine.UIElements.UIR.BaseShaderInfoStorage:SetTexel(x, y, color) end
function UnityEngine.UIElements.UIR.BaseShaderInfoStorage:UpdateTexture() end
function UnityEngine.UIElements.UIR.BaseShaderInfoStorage:Dispose() end

---@class UnityEngine.UIElements.UIR.BasicNode : UnityEngine.UIElements.UIR.LinkedPoolItem[UnityEngine.UIElements.UIR.BasicNode[T]]
---@field next UnityEngine.UIElements.UIR.BasicNode
---@field data T
UnityEngine.UIElements.UIR.BasicNode = {}
---@alias CS.UnityEngine.UIElements.UIR.BasicNode UnityEngine.UIElements.UIR.BasicNode
CS.UnityEngine.UIElements.UIR.BasicNode = UnityEngine.UIElements.UIR.BasicNode

---@return UnityEngine.UIElements.UIR.BasicNode
function UnityEngine.UIElements.UIR.BasicNode.New() end
---@param ref_first UnityEngine.UIElements.UIR.BasicNode
---@return UnityEngine.UIElements.UIR.BasicNode
function UnityEngine.UIElements.UIR.BasicNode:InsertFirst(ref_first) end

---@class UnityEngine.UIElements.UIR.BasicNodePool : UnityEngine.UIElements.UIR.LinkedPool[UnityEngine.UIElements.UIR.BasicNode[T]]
UnityEngine.UIElements.UIR.BasicNodePool = {}
---@alias CS.UnityEngine.UIElements.UIR.BasicNodePool UnityEngine.UIElements.UIR.BasicNodePool
CS.UnityEngine.UIElements.UIR.BasicNodePool = UnityEngine.UIElements.UIR.BasicNodePool

---@return UnityEngine.UIElements.UIR.BasicNodePool
function UnityEngine.UIElements.UIR.BasicNodePool.New() end

---@class UnityEngine.UIElements.UIR.BestFitAllocator : System.Object
---@field totalSize number
---@field highWatermark number
UnityEngine.UIElements.UIR.BestFitAllocator = {}
---@alias CS.UnityEngine.UIElements.UIR.BestFitAllocator UnityEngine.UIElements.UIR.BestFitAllocator
CS.UnityEngine.UIElements.UIR.BestFitAllocator = UnityEngine.UIElements.UIR.BestFitAllocator

---@param size number
---@return UnityEngine.UIElements.UIR.BestFitAllocator
function UnityEngine.UIElements.UIR.BestFitAllocator.New(size) end
---@param size number
---@return UnityEngine.UIElements.UIR.Alloc
function UnityEngine.UIElements.UIR.BestFitAllocator:Allocate(size) end
---@param alloc UnityEngine.UIElements.UIR.Alloc
function UnityEngine.UIElements.UIR.BestFitAllocator:Free(alloc) end

---@class UnityEngine.UIElements.UIR.BestFitAllocator.Block : UnityEngine.UIElements.UIR.LinkedPoolItem
---@field start number
---@field _end number
---@field prev UnityEngine.UIElements.UIR.BestFitAllocator.Block
---@field next UnityEngine.UIElements.UIR.BestFitAllocator.Block
---@field prevAvailable UnityEngine.UIElements.UIR.BestFitAllocator.Block
---@field nextAvailable UnityEngine.UIElements.UIR.BestFitAllocator.Block
---@field allocated boolean
---@field size number
UnityEngine.UIElements.UIR.BestFitAllocator.Block = {}
---@alias CS.UnityEngine.UIElements.UIR.BestFitAllocator.Block UnityEngine.UIElements.UIR.BestFitAllocator.Block
CS.UnityEngine.UIElements.UIR.BestFitAllocator.Block = UnityEngine.UIElements.UIR.BestFitAllocator.Block

---@return UnityEngine.UIElements.UIR.BestFitAllocator.Block
function UnityEngine.UIElements.UIR.BestFitAllocator.Block.New() end

---@class UnityEngine.UIElements.UIR.BestFitAllocator.BlockPool : UnityEngine.UIElements.UIR.LinkedPool
UnityEngine.UIElements.UIR.BestFitAllocator.BlockPool = {}
---@alias CS.UnityEngine.UIElements.UIR.BestFitAllocator.BlockPool UnityEngine.UIElements.UIR.BestFitAllocator.BlockPool
CS.UnityEngine.UIElements.UIR.BestFitAllocator.BlockPool = UnityEngine.UIElements.UIR.BestFitAllocator.BlockPool

---@return UnityEngine.UIElements.UIR.BestFitAllocator.BlockPool
function UnityEngine.UIElements.UIR.BestFitAllocator.BlockPool.New() end

---@class UnityEngine.UIElements.UIR.BitmapAllocator32 : System.ValueType
---@field kPageWidth number
---@field entryWidth number
---@field entryHeight number
UnityEngine.UIElements.UIR.BitmapAllocator32 = {}
---@alias CS.UnityEngine.UIElements.UIR.BitmapAllocator32 UnityEngine.UIElements.UIR.BitmapAllocator32
CS.UnityEngine.UIElements.UIR.BitmapAllocator32 = UnityEngine.UIElements.UIR.BitmapAllocator32

---@param pageHeight number
---@param entryWidth number
---@param entryHeight number
function UnityEngine.UIElements.UIR.BitmapAllocator32:Construct(pageHeight, entryWidth, entryHeight) end
---@param firstPageX number
---@param firstPageY number
function UnityEngine.UIElements.UIR.BitmapAllocator32:ForceFirstAlloc(firstPageX, firstPageY) end
---@param storage UnityEngine.UIElements.UIR.BaseShaderInfoStorage
---@return UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.BitmapAllocator32:Allocate(storage) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.BitmapAllocator32:Free(alloc) end

---@class UnityEngine.UIElements.UIR.BitmapAllocator32.Page : System.ValueType
---@field x number
---@field y number
---@field freeSlots number
UnityEngine.UIElements.UIR.BitmapAllocator32.Page = {}
---@alias CS.UnityEngine.UIElements.UIR.BitmapAllocator32.Page UnityEngine.UIElements.UIR.BitmapAllocator32.Page
CS.UnityEngine.UIElements.UIR.BitmapAllocator32.Page = UnityEngine.UIElements.UIR.BitmapAllocator32.Page


---@class UnityEngine.UIElements.UIR.BMPAlloc : System.ValueType
---@field Invalid UnityEngine.UIElements.UIR.BMPAlloc
---@field page number
---@field pageLine number
---@field bitIndex number
---@field ownedState UnityEngine.UIElements.UIR.OwnedState
UnityEngine.UIElements.UIR.BMPAlloc = {}
---@alias CS.UnityEngine.UIElements.UIR.BMPAlloc UnityEngine.UIElements.UIR.BMPAlloc
CS.UnityEngine.UIElements.UIR.BMPAlloc = UnityEngine.UIElements.UIR.BMPAlloc

---@param other UnityEngine.UIElements.UIR.BMPAlloc
---@return boolean
function UnityEngine.UIElements.UIR.BMPAlloc:Equals(other) end
---@return boolean
function UnityEngine.UIElements.UIR.BMPAlloc:IsValid() end
---@return string
function UnityEngine.UIElements.UIR.BMPAlloc:ToString() end

---@class UnityEngine.UIElements.UIR.ChainBuilderStats : System.ValueType
---@field elementsAdded number
---@field elementsRemoved number
---@field recursiveClipUpdates number
---@field recursiveClipUpdatesExpanded number
---@field nonRecursiveClipUpdates number
---@field recursiveTransformUpdates number
---@field recursiveTransformUpdatesExpanded number
---@field recursiveOpacityUpdates number
---@field recursiveOpacityUpdatesExpanded number
---@field opacityIdUpdates number
---@field colorUpdates number
---@field colorUpdatesExpanded number
---@field recursiveVisualUpdates number
---@field recursiveVisualUpdatesExpanded number
---@field nonRecursiveVisualUpdates number
---@field dirtyProcessed number
---@field nudgeTransformed number
---@field boneTransformed number
---@field skipTransformed number
---@field visualUpdateTransformed number
---@field updatedMeshAllocations number
---@field newMeshAllocations number
---@field groupTransformElementsChanged number
---@field immedateRenderersActive number
UnityEngine.UIElements.UIR.ChainBuilderStats = {}
---@alias CS.UnityEngine.UIElements.UIR.ChainBuilderStats UnityEngine.UIElements.UIR.ChainBuilderStats
CS.UnityEngine.UIElements.UIR.ChainBuilderStats = UnityEngine.UIElements.UIR.ChainBuilderStats


---@class UnityEngine.UIElements.UIR.CommandType
---@field Draw UnityEngine.UIElements.UIR.CommandType
---@field ImmediateCull UnityEngine.UIElements.UIR.CommandType
---@field Immediate UnityEngine.UIElements.UIR.CommandType
---@field PushView UnityEngine.UIElements.UIR.CommandType
---@field PopView UnityEngine.UIElements.UIR.CommandType
---@field PushScissor UnityEngine.UIElements.UIR.CommandType
---@field PopScissor UnityEngine.UIElements.UIR.CommandType
---@field PushRenderTexture UnityEngine.UIElements.UIR.CommandType
---@field PopRenderTexture UnityEngine.UIElements.UIR.CommandType
---@field BlitToPreviousRT UnityEngine.UIElements.UIR.CommandType
---@field PushDefaultMaterial UnityEngine.UIElements.UIR.CommandType
---@field PopDefaultMaterial UnityEngine.UIElements.UIR.CommandType
UnityEngine.UIElements.UIR.CommandType = {}
---@alias CS.UnityEngine.UIElements.UIR.CommandType UnityEngine.UIElements.UIR.CommandType
CS.UnityEngine.UIElements.UIR.CommandType = UnityEngine.UIElements.UIR.CommandType


---@class UnityEngine.UIElements.UIR.ConvertMeshJobData : System.ValueType
---@field vertSrc System.IntPtr
---@field vertDst System.IntPtr
---@field vertCount number
---@field transform UnityEngine.Matrix4x4
---@field transformUVs number
---@field xformClipPages UnityEngine.Color32
---@field ids UnityEngine.Color32
---@field addFlags UnityEngine.Color32
---@field opacityPage UnityEngine.Color32
---@field textCoreSettingsPage UnityEngine.Color32
---@field isText number
---@field textureId number
---@field indexSrc System.IntPtr
---@field indexDst System.IntPtr
---@field indexCount number
---@field indexOffset number
---@field flipIndices number
UnityEngine.UIElements.UIR.ConvertMeshJobData = {}
---@alias CS.UnityEngine.UIElements.UIR.ConvertMeshJobData UnityEngine.UIElements.UIR.ConvertMeshJobData
CS.UnityEngine.UIElements.UIR.ConvertMeshJobData = UnityEngine.UIElements.UIR.ConvertMeshJobData


---@class UnityEngine.UIElements.UIR.CopyClosingMeshJobData : System.ValueType
---@field vertSrc System.IntPtr
---@field vertDst System.IntPtr
---@field vertCount number
---@field indexSrc System.IntPtr
---@field indexDst System.IntPtr
---@field indexCount number
---@field indexOffset number
UnityEngine.UIElements.UIR.CopyClosingMeshJobData = {}
---@alias CS.UnityEngine.UIElements.UIR.CopyClosingMeshJobData UnityEngine.UIElements.UIR.CopyClosingMeshJobData
CS.UnityEngine.UIElements.UIR.CopyClosingMeshJobData = UnityEngine.UIElements.UIR.CopyClosingMeshJobData


---@class UnityEngine.UIElements.UIR.DetachedAllocator : System.Object
---@field meshes System.Collections.Generic.List
UnityEngine.UIElements.UIR.DetachedAllocator = {}
---@alias CS.UnityEngine.UIElements.UIR.DetachedAllocator UnityEngine.UIElements.UIR.DetachedAllocator
CS.UnityEngine.UIElements.UIR.DetachedAllocator = UnityEngine.UIElements.UIR.DetachedAllocator

---@return UnityEngine.UIElements.UIR.DetachedAllocator
function UnityEngine.UIElements.UIR.DetachedAllocator.New() end
function UnityEngine.UIElements.UIR.DetachedAllocator:Dispose() end
---@param vertexCount number
---@param indexCount number
---@return UnityEngine.UIElements.MeshWriteData
function UnityEngine.UIElements.UIR.DetachedAllocator:Alloc(vertexCount, indexCount) end
function UnityEngine.UIElements.UIR.DetachedAllocator:Clear() end

---@class UnityEngine.UIElements.UIR.DrawBufferRange : System.ValueType
---@field firstIndex number
---@field indexCount number
---@field minIndexVal number
---@field vertsReferenced number
UnityEngine.UIElements.UIR.DrawBufferRange = {}
---@alias CS.UnityEngine.UIElements.UIR.DrawBufferRange UnityEngine.UIElements.UIR.DrawBufferRange
CS.UnityEngine.UIElements.UIR.DrawBufferRange = UnityEngine.UIElements.UIR.DrawBufferRange


---@class UnityEngine.UIElements.UIR.DrawParams : System.Object
UnityEngine.UIElements.UIR.DrawParams = {}
---@alias CS.UnityEngine.UIElements.UIR.DrawParams UnityEngine.UIElements.UIR.DrawParams
CS.UnityEngine.UIElements.UIR.DrawParams = UnityEngine.UIElements.UIR.DrawParams

---@return UnityEngine.UIElements.UIR.DrawParams
function UnityEngine.UIElements.UIR.DrawParams.New() end
function UnityEngine.UIElements.UIR.DrawParams:Reset() end

---@class UnityEngine.UIElements.UIR.GfxUpdateBufferRange : System.ValueType
---@field offsetFromWriteStart number
---@field size number
---@field source System.UIntPtr
UnityEngine.UIElements.UIR.GfxUpdateBufferRange = {}
---@alias CS.UnityEngine.UIElements.UIR.GfxUpdateBufferRange UnityEngine.UIElements.UIR.GfxUpdateBufferRange
CS.UnityEngine.UIElements.UIR.GfxUpdateBufferRange = UnityEngine.UIElements.UIR.GfxUpdateBufferRange


---@class UnityEngine.UIElements.UIR.GPUBufferAllocator : System.Object
---@field isEmpty boolean
UnityEngine.UIElements.UIR.GPUBufferAllocator = {}
---@alias CS.UnityEngine.UIElements.UIR.GPUBufferAllocator UnityEngine.UIElements.UIR.GPUBufferAllocator
CS.UnityEngine.UIElements.UIR.GPUBufferAllocator = UnityEngine.UIElements.UIR.GPUBufferAllocator

---@param maxSize number
---@return UnityEngine.UIElements.UIR.GPUBufferAllocator
function UnityEngine.UIElements.UIR.GPUBufferAllocator.New(maxSize) end
---@param size number
---@param shortLived boolean
---@return UnityEngine.UIElements.UIR.Alloc
function UnityEngine.UIElements.UIR.GPUBufferAllocator:Allocate(size, shortLived) end
---@param alloc UnityEngine.UIElements.UIR.Alloc
function UnityEngine.UIElements.UIR.GPUBufferAllocator:Free(alloc) end
---@return UnityEngine.UIElements.UIR.HeapStatistics
function UnityEngine.UIElements.UIR.GPUBufferAllocator:GatherStatistics() end

---@class UnityEngine.UIElements.UIR.GradientRemap : UnityEngine.UIElements.UIR.LinkedPoolItem
---@field origIndex number
---@field destIndex number
---@field location UnityEngine.RectInt
---@field next UnityEngine.UIElements.UIR.GradientRemap
---@field atlas UnityEngine.UIElements.TextureId
UnityEngine.UIElements.UIR.GradientRemap = {}
---@alias CS.UnityEngine.UIElements.UIR.GradientRemap UnityEngine.UIElements.UIR.GradientRemap
CS.UnityEngine.UIElements.UIR.GradientRemap = UnityEngine.UIElements.UIR.GradientRemap

---@return UnityEngine.UIElements.UIR.GradientRemap
function UnityEngine.UIElements.UIR.GradientRemap.New() end
function UnityEngine.UIElements.UIR.GradientRemap:Reset() end

---@class UnityEngine.UIElements.UIR.GradientRemapPool : UnityEngine.UIElements.UIR.LinkedPool
UnityEngine.UIElements.UIR.GradientRemapPool = {}
---@alias CS.UnityEngine.UIElements.UIR.GradientRemapPool UnityEngine.UIElements.UIR.GradientRemapPool
CS.UnityEngine.UIElements.UIR.GradientRemapPool = UnityEngine.UIElements.UIR.GradientRemapPool

---@return UnityEngine.UIElements.UIR.GradientRemapPool
function UnityEngine.UIElements.UIR.GradientRemapPool.New() end

---@class UnityEngine.UIElements.UIR.GradientSettingsAtlas : System.Object
---@field atlas UnityEngine.Texture2D
---@field MustCommit boolean
UnityEngine.UIElements.UIR.GradientSettingsAtlas = {}
---@alias CS.UnityEngine.UIElements.UIR.GradientSettingsAtlas UnityEngine.UIElements.UIR.GradientSettingsAtlas
CS.UnityEngine.UIElements.UIR.GradientSettingsAtlas = UnityEngine.UIElements.UIR.GradientSettingsAtlas

---@param length number
---@return UnityEngine.UIElements.UIR.GradientSettingsAtlas
function UnityEngine.UIElements.UIR.GradientSettingsAtlas.New(length) end
function UnityEngine.UIElements.UIR.GradientSettingsAtlas:Dispose() end
function UnityEngine.UIElements.UIR.GradientSettingsAtlas:Reset() end
---@param count number
---@return UnityEngine.UIElements.UIR.Alloc
function UnityEngine.UIElements.UIR.GradientSettingsAtlas:Add(count) end
---@param alloc UnityEngine.UIElements.UIR.Alloc
function UnityEngine.UIElements.UIR.GradientSettingsAtlas:Remove(alloc) end
---@param alloc UnityEngine.UIElements.UIR.Alloc
---@param settings UnityEngine.UIElements.GradientSettings[]
---@param remap UnityEngine.UIElements.UIR.GradientRemap
function UnityEngine.UIElements.UIR.GradientSettingsAtlas:Write(alloc, settings, remap) end
function UnityEngine.UIElements.UIR.GradientSettingsAtlas:Commit() end

---@class UnityEngine.UIElements.UIR.GradientSettingsAtlas.RawTexture : System.ValueType
---@field rgba UnityEngine.Color32[]
---@field width number
---@field height number
UnityEngine.UIElements.UIR.GradientSettingsAtlas.RawTexture = {}
---@alias CS.UnityEngine.UIElements.UIR.GradientSettingsAtlas.RawTexture UnityEngine.UIElements.UIR.GradientSettingsAtlas.RawTexture
CS.UnityEngine.UIElements.UIR.GradientSettingsAtlas.RawTexture = UnityEngine.UIElements.UIR.GradientSettingsAtlas.RawTexture

---@param v0 number
---@param v1 number
---@param destX number
---@param destY number
function UnityEngine.UIElements.UIR.GradientSettingsAtlas.RawTexture:WriteRawInt2Packed(v0, v1, destX, destY) end
---@param f0 number
---@param f1 number
---@param f2 number
---@param f3 number
---@param destX number
---@param destY number
function UnityEngine.UIElements.UIR.GradientSettingsAtlas.RawTexture:WriteRawFloat4Packed(f0, f1, f2, f3, destX, destY) end

---@class UnityEngine.UIElements.UIR.HeapStatistics : System.ValueType
---@field numAllocs number
---@field totalSize number
---@field allocatedSize number
---@field freeSize number
---@field largestAvailableBlock number
---@field availableBlocksCount number
---@field blockCount number
---@field highWatermark number
---@field fragmentation number
---@field subAllocators UnityEngine.UIElements.UIR.HeapStatistics[]
UnityEngine.UIElements.UIR.HeapStatistics = {}
---@alias CS.UnityEngine.UIElements.UIR.HeapStatistics UnityEngine.UIElements.UIR.HeapStatistics
CS.UnityEngine.UIElements.UIR.HeapStatistics = UnityEngine.UIElements.UIR.HeapStatistics


---@class UnityEngine.UIElements.UIR.Implementation.ClipMethod
---@field Undetermined UnityEngine.UIElements.UIR.Implementation.ClipMethod
---@field NotClipped UnityEngine.UIElements.UIR.Implementation.ClipMethod
---@field Scissor UnityEngine.UIElements.UIR.Implementation.ClipMethod
---@field ShaderDiscard UnityEngine.UIElements.UIR.Implementation.ClipMethod
---@field Stencil UnityEngine.UIElements.UIR.Implementation.ClipMethod
UnityEngine.UIElements.UIR.Implementation.ClipMethod = {}
---@alias CS.UnityEngine.UIElements.UIR.Implementation.ClipMethod UnityEngine.UIElements.UIR.Implementation.ClipMethod
CS.UnityEngine.UIElements.UIR.Implementation.ClipMethod = UnityEngine.UIElements.UIR.Implementation.ClipMethod


---@class UnityEngine.UIElements.UIR.Implementation.CommandGenerator : System.Object
UnityEngine.UIElements.UIR.Implementation.CommandGenerator = {}
---@alias CS.UnityEngine.UIElements.UIR.Implementation.CommandGenerator UnityEngine.UIElements.UIR.Implementation.CommandGenerator
CS.UnityEngine.UIElements.UIR.Implementation.CommandGenerator = UnityEngine.UIElements.UIR.Implementation.CommandGenerator

---@param renderChain UnityEngine.UIElements.UIR.RenderChain
---@param ve UnityEngine.UIElements.VisualElement
---@param ref_stats UnityEngine.UIElements.UIR.ChainBuilderStats
---@return UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.ClosingInfo, UnityEngine.UIElements.UIR.ChainBuilderStats
function UnityEngine.UIElements.UIR.Implementation.CommandGenerator.PaintElement(renderChain, ve, ref_stats) end
---@param ve UnityEngine.UIElements.VisualElement
---@param closingInfo UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.ClosingInfo
---@param renderChain UnityEngine.UIElements.UIR.RenderChain
---@param ref_stats UnityEngine.UIElements.UIR.ChainBuilderStats
---@return UnityEngine.UIElements.UIR.ChainBuilderStats
function UnityEngine.UIElements.UIR.Implementation.CommandGenerator.ClosePaintElement(ve, closingInfo, renderChain, ref_stats) end
---@param ve UnityEngine.UIElements.VisualElement
---@param renderChain UnityEngine.UIElements.UIR.RenderChain
function UnityEngine.UIElements.UIR.Implementation.CommandGenerator.UpdateOpacityId(ve, renderChain) end
---@param ve UnityEngine.UIElements.VisualElement
---@param renderChain UnityEngine.UIElements.UIR.RenderChain
---@param device UnityEngine.UIElements.UIR.UIRenderDevice
---@return boolean
function UnityEngine.UIElements.UIR.Implementation.CommandGenerator.NudgeVerticesToNewSpace(ve, renderChain, device) end
---@param renderChain UnityEngine.UIElements.UIR.RenderChain
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UIR.Implementation.CommandGenerator.ResetCommands(renderChain, ve) end

---@class UnityEngine.UIElements.UIR.Implementation.RenderEvents : System.Object
UnityEngine.UIElements.UIR.Implementation.RenderEvents = {}
---@alias CS.UnityEngine.UIElements.UIR.Implementation.RenderEvents UnityEngine.UIElements.UIR.Implementation.RenderEvents
CS.UnityEngine.UIElements.UIR.Implementation.RenderEvents = UnityEngine.UIElements.UIR.Implementation.RenderEvents


---@class UnityEngine.UIElements.UIR.Implementation.UIRStylePainter : System.Object
---@field meshGenerationContext UnityEngine.UIElements.MeshGenerationContext
---@field currentElement UnityEngine.UIElements.VisualElement
---@field entries System.Collections.Generic.List
---@field closingInfo UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.ClosingInfo
---@field totalVertices number
---@field totalIndices number
---@field visualElement UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.UIR.Implementation.UIRStylePainter = {}
---@alias CS.UnityEngine.UIElements.UIR.Implementation.UIRStylePainter UnityEngine.UIElements.UIR.Implementation.UIRStylePainter
CS.UnityEngine.UIElements.UIR.Implementation.UIRStylePainter = UnityEngine.UIElements.UIR.Implementation.UIRStylePainter

---@param renderChain UnityEngine.UIElements.UIR.RenderChain
---@return UnityEngine.UIElements.UIR.Implementation.UIRStylePainter
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.New(renderChain) end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:Begin(ve) end
---@param cmd UnityEngine.UIElements.UIR.RenderChainCommand
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:LandClipUnregisterMeshDrawCommand(cmd) end
---@param vertices Unity.Collections.NativeSlice
---@param indices Unity.Collections.NativeSlice
---@param indexOffset number
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:LandClipRegisterMesh(vertices, indices, indexOffset) end
---@param vertexCount number
---@param indexCount number
---@param texture UnityEngine.UIElements.TextureId
---@param material UnityEngine.Material
---@param flags UnityEngine.UIElements.MeshGenerationContext.MeshFlags
---@return UnityEngine.UIElements.MeshWriteData
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:AddGradientsEntry(vertexCount, indexCount, texture, material, flags) end
---@param vertexCount number
---@param indexCount number
---@param texture UnityEngine.Texture
---@param material UnityEngine.Material
---@param flags UnityEngine.UIElements.MeshGenerationContext.MeshFlags
---@return UnityEngine.UIElements.MeshWriteData
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:DrawMesh(vertexCount, indexCount, texture, material, flags) end
---@param meshData UnityEngine.UIElements.MeshWriteDataInterface
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:BuildRawEntryFromNativeMesh(meshData) end
---@overload fun(self: UnityEngine.UIElements.UIR.Implementation.UIRStylePainter, te: UnityEngine.UIElements.TextElement)
---@param text string
---@param pos UnityEngine.Vector2
---@param fontSize number
---@param color UnityEngine.Color
---@param font UnityEngine.TextCore.Text.FontAsset
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:DrawText(text, pos, fontSize, color, font) end
---@param rectParams UnityEngine.UIElements.MeshGenerationContextUtils.RectangleParams
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:DrawRectangle(rectParams) end
---@param borderParams UnityEngine.UIElements.MeshGenerationContextUtils.BorderParams
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:DrawBorder(borderParams) end
---@param callback System.Action | function
---@param cullingEnabled boolean
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:DrawImmediate(callback, cullingEnabled) end
---@overload fun(self: UnityEngine.UIElements.UIR.Implementation.UIRStylePainter, vectorImage: UnityEngine.UIElements.VectorImage, offset: UnityEngine.Vector2, rotationAngle: UnityEngine.UIElements.Angle, scale: UnityEngine.Vector2)
---@param rectParams UnityEngine.UIElements.MeshGenerationContextUtils.RectangleParams
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:DrawVectorImage(rectParams) end
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:DrawVisualElementBackground() end
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:DrawVisualElementBorder() end
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:ApplyVisualElementClipping() end
---@param rectParams UnityEngine.UIElements.MeshGenerationContextUtils.RectangleParams
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:DrawSprite(rectParams) end
---@param vi UnityEngine.UIElements.VectorImage
---@param out_settingIndexOffset number
---@param out_texture UnityEngine.UIElements.TextureId
---@return number, UnityEngine.UIElements.TextureId
function UnityEngine.UIElements.UIR.Implementation.UIRStylePainter:RegisterVectorImageGradient(vi, out_settingIndexOffset, out_texture) end

---@class UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.ClosingInfo : System.ValueType
---@field needsClosing boolean
---@field popViewMatrix boolean
---@field popScissorClip boolean
---@field blitAndPopRenderTexture boolean
---@field PopDefaultMaterial boolean
---@field clipUnregisterDrawCommand UnityEngine.UIElements.UIR.RenderChainCommand
---@field clipperRegisterVertices Unity.Collections.NativeSlice
---@field clipperRegisterIndices Unity.Collections.NativeSlice
---@field clipperRegisterIndexOffset number
---@field maskStencilRef number
UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.ClosingInfo = {}
---@alias CS.UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.ClosingInfo UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.ClosingInfo
CS.UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.ClosingInfo = UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.ClosingInfo


---@class UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.Entry : System.ValueType
---@field vertices Unity.Collections.NativeSlice
---@field indices Unity.Collections.NativeSlice
---@field material UnityEngine.Material
---@field fontTexSDFScale number
---@field texture UnityEngine.UIElements.TextureId
---@field customCommand UnityEngine.UIElements.UIR.RenderChainCommand
---@field clipRectID UnityEngine.UIElements.UIR.BMPAlloc
---@field addFlags UnityEngine.UIElements.UIR.VertexFlags
---@field uvIsDisplacement boolean
---@field isTextEntry boolean
---@field isClipRegisterEntry boolean
---@field stencilRef number
---@field maskDepth number
UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.Entry = {}
---@alias CS.UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.Entry UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.Entry
CS.UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.Entry = UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.Entry


---@class UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.RepeatRectUV : System.ValueType
---@field rect UnityEngine.Rect
---@field uv UnityEngine.Rect
UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.RepeatRectUV = {}
---@alias CS.UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.RepeatRectUV UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.RepeatRectUV
CS.UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.RepeatRectUV = UnityEngine.UIElements.UIR.Implementation.UIRStylePainter.RepeatRectUV


---@class UnityEngine.UIElements.UIR.JobManager : System.Object
UnityEngine.UIElements.UIR.JobManager = {}
---@alias CS.UnityEngine.UIElements.UIR.JobManager UnityEngine.UIElements.UIR.JobManager
CS.UnityEngine.UIElements.UIR.JobManager = UnityEngine.UIElements.UIR.JobManager

---@return UnityEngine.UIElements.UIR.JobManager
function UnityEngine.UIElements.UIR.JobManager.New() end
---@overload fun(self: UnityEngine.UIElements.UIR.JobManager, ref_job: UnityEngine.UIElements.UIR.NudgeJobData) : UnityEngine.UIElements.UIR.NudgeJobData
---@overload fun(self: UnityEngine.UIElements.UIR.JobManager, ref_job: UnityEngine.UIElements.UIR.ConvertMeshJobData) : UnityEngine.UIElements.UIR.ConvertMeshJobData
---@param ref_job UnityEngine.UIElements.UIR.CopyClosingMeshJobData
---@return UnityEngine.UIElements.UIR.CopyClosingMeshJobData
function UnityEngine.UIElements.UIR.JobManager:Add(ref_job) end
function UnityEngine.UIElements.UIR.JobManager:CompleteNudgeJobs() end
function UnityEngine.UIElements.UIR.JobManager:CompleteConvertMeshJobs() end
function UnityEngine.UIElements.UIR.JobManager:CompleteClosingMeshJobs() end
function UnityEngine.UIElements.UIR.JobManager:Dispose() end

---@class UnityEngine.UIElements.UIR.JobMerger : System.Object
UnityEngine.UIElements.UIR.JobMerger = {}
---@alias CS.UnityEngine.UIElements.UIR.JobMerger UnityEngine.UIElements.UIR.JobMerger
CS.UnityEngine.UIElements.UIR.JobMerger = UnityEngine.UIElements.UIR.JobMerger

---@param capacity number
---@return UnityEngine.UIElements.UIR.JobMerger
function UnityEngine.UIElements.UIR.JobMerger.New(capacity) end
---@param job Unity.Jobs.JobHandle
function UnityEngine.UIElements.UIR.JobMerger:Add(job) end
---@return Unity.Jobs.JobHandle
function UnityEngine.UIElements.UIR.JobMerger:MergeAndReset() end
function UnityEngine.UIElements.UIR.JobMerger:Dispose() end

---@class UnityEngine.UIElements.UIR.JobProcessor : System.Object
UnityEngine.UIElements.UIR.JobProcessor = {}
---@alias CS.UnityEngine.UIElements.UIR.JobProcessor UnityEngine.UIElements.UIR.JobProcessor
CS.UnityEngine.UIElements.UIR.JobProcessor = UnityEngine.UIElements.UIR.JobProcessor


---@class UnityEngine.UIElements.UIR.LinkedPool : System.Object
---@field Count number
UnityEngine.UIElements.UIR.LinkedPool = {}
---@alias CS.UnityEngine.UIElements.UIR.LinkedPool UnityEngine.UIElements.UIR.LinkedPool
CS.UnityEngine.UIElements.UIR.LinkedPool = UnityEngine.UIElements.UIR.LinkedPool

---@param createFunc System.Func[T]
---@param resetAction System.Action[T]
---@param limit number
---@return UnityEngine.UIElements.UIR.LinkedPool
function UnityEngine.UIElements.UIR.LinkedPool.New(createFunc, resetAction, limit) end
function UnityEngine.UIElements.UIR.LinkedPool:Clear() end
---@return T
function UnityEngine.UIElements.UIR.LinkedPool:Get() end
---@param item T
function UnityEngine.UIElements.UIR.LinkedPool:Return(item) end

---@class UnityEngine.UIElements.UIR.LinkedPoolItem : System.Object
UnityEngine.UIElements.UIR.LinkedPoolItem = {}
---@alias CS.UnityEngine.UIElements.UIR.LinkedPoolItem UnityEngine.UIElements.UIR.LinkedPoolItem
CS.UnityEngine.UIElements.UIR.LinkedPoolItem = UnityEngine.UIElements.UIR.LinkedPoolItem

---@return UnityEngine.UIElements.UIR.LinkedPoolItem
function UnityEngine.UIElements.UIR.LinkedPoolItem.New() end

---@class UnityEngine.UIElements.UIR.MeshBuilder : System.Object
UnityEngine.UIElements.UIR.MeshBuilder = {}
---@alias CS.UnityEngine.UIElements.UIR.MeshBuilder UnityEngine.UIElements.UIR.MeshBuilder
CS.UnityEngine.UIElements.UIR.MeshBuilder = UnityEngine.UIElements.UIR.MeshBuilder


---@class UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData : System.ValueType
UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData = {}
---@alias CS.UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData
CS.UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData = UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData


---@class UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator : System.MulticastDelegate
UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator = {}
---@alias CS.UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator
CS.UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator = UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator
function UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator.New(object, method) end
---@param vertexCount number
---@param indexCount number
---@param ref_allocatorData UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData
---@return UnityEngine.UIElements.MeshWriteData, UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData
function UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator:Invoke(vertexCount, indexCount, ref_allocatorData) end
---@param vertexCount number
---@param indexCount number
---@param ref_allocatorData UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult, UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData
function UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator:BeginInvoke(vertexCount, indexCount, ref_allocatorData, callback, object) end
---@param ref_allocatorData UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData
---@param result System.IAsyncResult
---@return UnityEngine.UIElements.MeshWriteData, UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData
function UnityEngine.UIElements.UIR.MeshBuilder.AllocMeshData.Allocator:EndInvoke(ref_allocatorData, result) end

---@class UnityEngine.UIElements.UIR.MeshHandle : UnityEngine.UIElements.UIR.LinkedPoolItem
UnityEngine.UIElements.UIR.MeshHandle = {}
---@alias CS.UnityEngine.UIElements.UIR.MeshHandle UnityEngine.UIElements.UIR.MeshHandle
CS.UnityEngine.UIElements.UIR.MeshHandle = UnityEngine.UIElements.UIR.MeshHandle

---@return UnityEngine.UIElements.UIR.MeshHandle
function UnityEngine.UIElements.UIR.MeshHandle.New() end

---@class UnityEngine.UIElements.UIR.NativePagedList : System.Object
UnityEngine.UIElements.UIR.NativePagedList = {}
---@alias CS.UnityEngine.UIElements.UIR.NativePagedList UnityEngine.UIElements.UIR.NativePagedList
CS.UnityEngine.UIElements.UIR.NativePagedList = UnityEngine.UIElements.UIR.NativePagedList

---@param poolCapacity number
---@return UnityEngine.UIElements.UIR.NativePagedList
function UnityEngine.UIElements.UIR.NativePagedList.New(poolCapacity) end
---@overload fun(self: UnityEngine.UIElements.UIR.NativePagedList, ref_data: T) : T
---@param data T
function UnityEngine.UIElements.UIR.NativePagedList:Add(data) end
---@return System.Collections.Generic.List[Unity.Collections.NativeSlice[T]]
function UnityEngine.UIElements.UIR.NativePagedList:GetPages() end
function UnityEngine.UIElements.UIR.NativePagedList:Reset() end
function UnityEngine.UIElements.UIR.NativePagedList:Dispose() end

---@class UnityEngine.UIElements.UIR.NudgeJobData : System.ValueType
---@field src System.IntPtr
---@field dst System.IntPtr
---@field count number
---@field closingSrc System.IntPtr
---@field closingDst System.IntPtr
---@field closingCount number
---@field transform UnityEngine.Matrix4x4
---@field vertsBeforeUVDisplacement number
---@field vertsAfterUVDisplacement number
UnityEngine.UIElements.UIR.NudgeJobData = {}
---@alias CS.UnityEngine.UIElements.UIR.NudgeJobData UnityEngine.UIElements.UIR.NudgeJobData
CS.UnityEngine.UIElements.UIR.NudgeJobData = UnityEngine.UIElements.UIR.NudgeJobData


---@class UnityEngine.UIElements.UIR.OpacityIdAccelerator : System.Object
UnityEngine.UIElements.UIR.OpacityIdAccelerator = {}
---@alias CS.UnityEngine.UIElements.UIR.OpacityIdAccelerator UnityEngine.UIElements.UIR.OpacityIdAccelerator
CS.UnityEngine.UIElements.UIR.OpacityIdAccelerator = UnityEngine.UIElements.UIR.OpacityIdAccelerator

---@return UnityEngine.UIElements.UIR.OpacityIdAccelerator
function UnityEngine.UIElements.UIR.OpacityIdAccelerator.New() end
---@param oldVerts Unity.Collections.NativeSlice
---@param newVerts Unity.Collections.NativeSlice
---@param opacityData UnityEngine.Color32
---@param vertexCount number
function UnityEngine.UIElements.UIR.OpacityIdAccelerator:CreateJob(oldVerts, newVerts, opacityData, vertexCount) end
function UnityEngine.UIElements.UIR.OpacityIdAccelerator:CompleteJobs() end
function UnityEngine.UIElements.UIR.OpacityIdAccelerator:Dispose() end

---@class UnityEngine.UIElements.UIR.OpacityIdAccelerator.OpacityIdUpdateJob : System.ValueType
---@field oldVerts Unity.Collections.NativeSlice
---@field newVerts Unity.Collections.NativeSlice
---@field opacityData UnityEngine.Color32
UnityEngine.UIElements.UIR.OpacityIdAccelerator.OpacityIdUpdateJob = {}
---@alias CS.UnityEngine.UIElements.UIR.OpacityIdAccelerator.OpacityIdUpdateJob UnityEngine.UIElements.UIR.OpacityIdAccelerator.OpacityIdUpdateJob
CS.UnityEngine.UIElements.UIR.OpacityIdAccelerator.OpacityIdUpdateJob = UnityEngine.UIElements.UIR.OpacityIdAccelerator.OpacityIdUpdateJob

---@param i number
function UnityEngine.UIElements.UIR.OpacityIdAccelerator.OpacityIdUpdateJob:Execute(i) end

---@class UnityEngine.UIElements.UIR.OwnedState
---@field Inherited UnityEngine.UIElements.UIR.OwnedState
---@field Owned UnityEngine.UIElements.UIR.OwnedState
UnityEngine.UIElements.UIR.OwnedState = {}
---@alias CS.UnityEngine.UIElements.UIR.OwnedState UnityEngine.UIElements.UIR.OwnedState
CS.UnityEngine.UIElements.UIR.OwnedState = UnityEngine.UIElements.UIR.OwnedState


---@class UnityEngine.UIElements.UIR.Page : System.Object
---@field vertices UnityEngine.UIElements.UIR.Page.DataSet
---@field indices UnityEngine.UIElements.UIR.Page.DataSet
---@field next UnityEngine.UIElements.UIR.Page
---@field framesEmpty number
---@field isEmpty boolean
UnityEngine.UIElements.UIR.Page = {}
---@alias CS.UnityEngine.UIElements.UIR.Page UnityEngine.UIElements.UIR.Page
CS.UnityEngine.UIElements.UIR.Page = UnityEngine.UIElements.UIR.Page

---@param vertexMaxCount number
---@param indexMaxCount number
---@param maxQueuedFrameCount number
---@param mockPage boolean
---@return UnityEngine.UIElements.UIR.Page
function UnityEngine.UIElements.UIR.Page.New(vertexMaxCount, indexMaxCount, maxQueuedFrameCount, mockPage) end
function UnityEngine.UIElements.UIR.Page:Dispose() end

---@class UnityEngine.UIElements.UIR.Page.DataSet : System.Object
---@field gpuData UnityEngine.UIElements.UIR.Utility.GPUBuffer[T]
---@field cpuData Unity.Collections.NativeArray[T]
---@field updateRanges Unity.Collections.NativeArray
---@field allocator UnityEngine.UIElements.UIR.GPUBufferAllocator
UnityEngine.UIElements.UIR.Page.DataSet = {}
---@alias CS.UnityEngine.UIElements.UIR.Page.DataSet UnityEngine.UIElements.UIR.Page.DataSet
CS.UnityEngine.UIElements.UIR.Page.DataSet = UnityEngine.UIElements.UIR.Page.DataSet

---@param bufferType UnityEngine.UIElements.UIR.Utility.GPUBufferType
---@param totalCount number
---@param maxQueuedFrameCount number
---@param updateRangePoolSize number
---@param mockBuffer boolean
---@return UnityEngine.UIElements.UIR.Page.DataSet
function UnityEngine.UIElements.UIR.Page.DataSet.New(bufferType, totalCount, maxQueuedFrameCount, updateRangePoolSize, mockBuffer) end
---@overload fun(self: UnityEngine.UIElements.UIR.Page.DataSet)
---@param disposing boolean
function UnityEngine.UIElements.UIR.Page.DataSet:Dispose(disposing) end
---@param start number
---@param size number
function UnityEngine.UIElements.UIR.Page.DataSet:RegisterUpdate(start, size) end
function UnityEngine.UIElements.UIR.Page.DataSet:SendUpdates() end
function UnityEngine.UIElements.UIR.Page.DataSet:SendFullRange() end
function UnityEngine.UIElements.UIR.Page.DataSet:SendPartialRanges() end

---@class UnityEngine.UIElements.UIR.RenderChain : System.Object
---@field opacityIdAccelerator UnityEngine.UIElements.UIR.OpacityIdAccelerator
UnityEngine.UIElements.UIR.RenderChain = {}
---@alias CS.UnityEngine.UIElements.UIR.RenderChain UnityEngine.UIElements.UIR.RenderChain
CS.UnityEngine.UIElements.UIR.RenderChain = UnityEngine.UIElements.UIR.RenderChain

---@param panel UnityEngine.UIElements.BaseVisualElementPanel
---@return UnityEngine.UIElements.UIR.RenderChain
function UnityEngine.UIElements.UIR.RenderChain.New(panel) end
function UnityEngine.UIElements.UIR.RenderChain:Dispose() end
function UnityEngine.UIElements.UIR.RenderChain:ProcessChanges() end
function UnityEngine.UIElements.UIR.RenderChain:Render() end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UIR.RenderChain:UIEOnChildAdded(ve) end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UIR.RenderChain:UIEOnChildrenReordered(ve) end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UIR.RenderChain:UIEOnChildRemoving(ve) end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UIR.RenderChain:UIEOnRenderHintsChanged(ve) end
---@param ve UnityEngine.UIElements.VisualElement
---@param hierarchical boolean
function UnityEngine.UIElements.UIR.RenderChain:UIEOnClippingChanged(ve, hierarchical) end
---@param ve UnityEngine.UIElements.VisualElement
---@param hierarchical boolean
function UnityEngine.UIElements.UIR.RenderChain:UIEOnOpacityChanged(ve, hierarchical) end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UIR.RenderChain:UIEOnColorChanged(ve) end
---@param ve UnityEngine.UIElements.VisualElement
---@param transformChanged boolean
---@param clipRectSizeChanged boolean
function UnityEngine.UIElements.UIR.RenderChain:UIEOnTransformOrSizeChanged(ve, transformChanged, clipRectSizeChanged) end
---@param ve UnityEngine.UIElements.VisualElement
---@param hierarchical boolean
function UnityEngine.UIElements.UIR.RenderChain:UIEOnVisualsChanged(ve, hierarchical) end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UIR.RenderChain:UIEOnOpacityIdChanged(ve) end
---@param ve UnityEngine.UIElements.VisualElement
---@param src UnityEngine.Texture
---@param id UnityEngine.UIElements.TextureId
---@param isAtlas boolean
function UnityEngine.UIElements.UIR.RenderChain:InsertTexture(ve, src, id, isAtlas) end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UIR.RenderChain:ResetTextures(ve) end

---@class UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking : System.ValueType
---@field heads System.Collections.Generic.List
---@field tails System.Collections.Generic.List
---@field minDepths number[]
---@field maxDepths number[]
---@field dirtyID number
UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking = {}
---@alias CS.UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking
CS.UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking = UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking

---@param maxDepth number
function UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking:EnsureFits(maxDepth) end
---@param ve UnityEngine.UIElements.VisualElement
---@param dirtyTypes UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@param dirtyTypeClass UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses
function UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking:RegisterDirty(ve, dirtyTypes, dirtyTypeClass) end
---@param ve UnityEngine.UIElements.VisualElement
---@param dirtyTypesInverse UnityEngine.UIElements.UIR.RenderDataDirtyTypes
function UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking:ClearDirty(ve, dirtyTypesInverse) end
function UnityEngine.UIElements.UIR.RenderChain.DepthOrderedDirtyTracking:Reset() end

---@class UnityEngine.UIElements.UIR.RenderChain.RenderChainStaticIndexAllocator : System.ValueType
UnityEngine.UIElements.UIR.RenderChain.RenderChainStaticIndexAllocator = {}
---@alias CS.UnityEngine.UIElements.UIR.RenderChain.RenderChainStaticIndexAllocator UnityEngine.UIElements.UIR.RenderChain.RenderChainStaticIndexAllocator
CS.UnityEngine.UIElements.UIR.RenderChain.RenderChainStaticIndexAllocator = UnityEngine.UIElements.UIR.RenderChain.RenderChainStaticIndexAllocator

---@param renderChain UnityEngine.UIElements.UIR.RenderChain
---@return number
function UnityEngine.UIElements.UIR.RenderChain.RenderChainStaticIndexAllocator.AllocateIndex(renderChain) end
---@param index number
function UnityEngine.UIElements.UIR.RenderChain.RenderChainStaticIndexAllocator.FreeIndex(index) end
---@param index number
---@return UnityEngine.UIElements.UIR.RenderChain
function UnityEngine.UIElements.UIR.RenderChain.RenderChainStaticIndexAllocator.AccessIndex(index) end

---@class UnityEngine.UIElements.UIR.RenderChain.RenderNodeData : System.ValueType
---@field standardMaterial UnityEngine.Material
---@field initialMaterial UnityEngine.Material
---@field matPropBlock UnityEngine.MaterialPropertyBlock
---@field firstCommand UnityEngine.UIElements.UIR.RenderChainCommand
---@field device UnityEngine.UIElements.UIR.UIRenderDevice
---@field vectorAtlas UnityEngine.Texture
---@field shaderInfoAtlas UnityEngine.Texture
---@field dpiScale number
---@field transformConstants Unity.Collections.NativeSlice
---@field clipRectConstants Unity.Collections.NativeSlice
UnityEngine.UIElements.UIR.RenderChain.RenderNodeData = {}
---@alias CS.UnityEngine.UIElements.UIR.RenderChain.RenderNodeData UnityEngine.UIElements.UIR.RenderChain.RenderNodeData
CS.UnityEngine.UIElements.UIR.RenderChain.RenderNodeData = UnityEngine.UIElements.UIR.RenderChain.RenderNodeData


---@class UnityEngine.UIElements.UIR.RenderChainCommand : UnityEngine.UIElements.UIR.LinkedPoolItem
UnityEngine.UIElements.UIR.RenderChainCommand = {}
---@alias CS.UnityEngine.UIElements.UIR.RenderChainCommand UnityEngine.UIElements.UIR.RenderChainCommand
CS.UnityEngine.UIElements.UIR.RenderChainCommand = UnityEngine.UIElements.UIR.RenderChainCommand

---@return UnityEngine.UIElements.UIR.RenderChainCommand
function UnityEngine.UIElements.UIR.RenderChainCommand.New() end

---@class UnityEngine.UIElements.UIR.RenderChainVEData : System.ValueType
---@field worldTransformScaleZero boolean
---@field isIgnoringDynamicColorHint boolean
UnityEngine.UIElements.UIR.RenderChainVEData = {}
---@alias CS.UnityEngine.UIElements.UIR.RenderChainVEData UnityEngine.UIElements.UIR.RenderChainVEData
CS.UnityEngine.UIElements.UIR.RenderChainVEData = UnityEngine.UIElements.UIR.RenderChainVEData


---@class UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses
---@field Clipping UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses
---@field Opacity UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses
---@field Color UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses
---@field TransformSize UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses
---@field Visuals UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses
---@field Count UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses
UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses = {}
---@alias CS.UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses
CS.UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses = UnityEngine.UIElements.UIR.RenderDataDirtyTypeClasses


---@class UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field None UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field Transform UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field ClipRectSize UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field Clipping UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field ClippingHierarchy UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field Visuals UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field VisualsHierarchy UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field VisualsOpacityId UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field Opacity UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field OpacityHierarchy UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field Color UnityEngine.UIElements.UIR.RenderDataDirtyTypes
---@field AllVisuals UnityEngine.UIElements.UIR.RenderDataDirtyTypes
UnityEngine.UIElements.UIR.RenderDataDirtyTypes = {}
---@alias CS.UnityEngine.UIElements.UIR.RenderDataDirtyTypes UnityEngine.UIElements.UIR.RenderDataDirtyTypes
CS.UnityEngine.UIElements.UIR.RenderDataDirtyTypes = UnityEngine.UIElements.UIR.RenderDataDirtyTypes


---@class UnityEngine.UIElements.UIR.RenderDataFlags
---@field IsIgnoringDynamicColorHint UnityEngine.UIElements.UIR.RenderDataFlags
UnityEngine.UIElements.UIR.RenderDataFlags = {}
---@alias CS.UnityEngine.UIElements.UIR.RenderDataFlags UnityEngine.UIElements.UIR.RenderDataFlags
CS.UnityEngine.UIElements.UIR.RenderDataFlags = UnityEngine.UIElements.UIR.RenderDataFlags


---@class UnityEngine.UIElements.UIR.ShaderInfoStorage : UnityEngine.UIElements.UIR.BaseShaderInfoStorage
---@field texture UnityEngine.Texture2D
UnityEngine.UIElements.UIR.ShaderInfoStorage = {}
---@alias CS.UnityEngine.UIElements.UIR.ShaderInfoStorage UnityEngine.UIElements.UIR.ShaderInfoStorage
CS.UnityEngine.UIElements.UIR.ShaderInfoStorage = UnityEngine.UIElements.UIR.ShaderInfoStorage

---@param format UnityEngine.TextureFormat
---@param convert System.Func[UnityEngine.Color,T]
---@param initialSize number
---@param maxSize number
---@return UnityEngine.UIElements.UIR.ShaderInfoStorage
function UnityEngine.UIElements.UIR.ShaderInfoStorage.New(format, convert, initialSize, maxSize) end
---@param width number
---@param height number
---@param out_uvs UnityEngine.RectInt
---@return boolean, UnityEngine.RectInt
function UnityEngine.UIElements.UIR.ShaderInfoStorage:AllocateRect(width, height, out_uvs) end
---@param x number
---@param y number
---@param color UnityEngine.Color
function UnityEngine.UIElements.UIR.ShaderInfoStorage:SetTexel(x, y, color) end
function UnityEngine.UIElements.UIR.ShaderInfoStorage:UpdateTexture() end

---@class UnityEngine.UIElements.UIR.ShaderInfoStorageRGBA32 : UnityEngine.UIElements.UIR.ShaderInfoStorage
UnityEngine.UIElements.UIR.ShaderInfoStorageRGBA32 = {}
---@alias CS.UnityEngine.UIElements.UIR.ShaderInfoStorageRGBA32 UnityEngine.UIElements.UIR.ShaderInfoStorageRGBA32
CS.UnityEngine.UIElements.UIR.ShaderInfoStorageRGBA32 = UnityEngine.UIElements.UIR.ShaderInfoStorageRGBA32

---@param initialSize number
---@param maxSize number
---@return UnityEngine.UIElements.UIR.ShaderInfoStorageRGBA32
function UnityEngine.UIElements.UIR.ShaderInfoStorageRGBA32.New(initialSize, maxSize) end

---@class UnityEngine.UIElements.UIR.ShaderInfoStorageRGBAFloat : UnityEngine.UIElements.UIR.ShaderInfoStorage
UnityEngine.UIElements.UIR.ShaderInfoStorageRGBAFloat = {}
---@alias CS.UnityEngine.UIElements.UIR.ShaderInfoStorageRGBAFloat UnityEngine.UIElements.UIR.ShaderInfoStorageRGBAFloat
CS.UnityEngine.UIElements.UIR.ShaderInfoStorageRGBAFloat = UnityEngine.UIElements.UIR.ShaderInfoStorageRGBAFloat

---@param initialSize number
---@param maxSize number
---@return UnityEngine.UIElements.UIR.ShaderInfoStorageRGBAFloat
function UnityEngine.UIElements.UIR.ShaderInfoStorageRGBAFloat.New(initialSize, maxSize) end

---@class UnityEngine.UIElements.UIR.Shaders : System.Object
---@field k_AtlasBlit string
---@field k_Editor string
---@field k_Runtime string
---@field k_RuntimeWorld string
---@field k_GraphView string
---@field k_ColorConversionBlit string
UnityEngine.UIElements.UIR.Shaders = {}
---@alias CS.UnityEngine.UIElements.UIR.Shaders UnityEngine.UIElements.UIR.Shaders
CS.UnityEngine.UIElements.UIR.Shaders = UnityEngine.UIElements.UIR.Shaders


---@class UnityEngine.UIElements.UIR.State : System.ValueType
---@field material UnityEngine.Material
---@field texture UnityEngine.UIElements.TextureId
---@field stencilRef number
---@field sdfScale number
UnityEngine.UIElements.UIR.State = {}
---@alias CS.UnityEngine.UIElements.UIR.State UnityEngine.UIElements.UIR.State
CS.UnityEngine.UIElements.UIR.State = UnityEngine.UIElements.UIR.State


---@class UnityEngine.UIElements.UIR.TempAllocator : System.Object
UnityEngine.UIElements.UIR.TempAllocator = {}
---@alias CS.UnityEngine.UIElements.UIR.TempAllocator UnityEngine.UIElements.UIR.TempAllocator
CS.UnityEngine.UIElements.UIR.TempAllocator = UnityEngine.UIElements.UIR.TempAllocator

---@param poolCapacity number
---@param excessMinCapacity number
---@param excessMaxCapacity number
---@return UnityEngine.UIElements.UIR.TempAllocator
function UnityEngine.UIElements.UIR.TempAllocator.New(poolCapacity, excessMinCapacity, excessMaxCapacity) end
function UnityEngine.UIElements.UIR.TempAllocator:Dispose() end
---@param count number
---@return Unity.Collections.NativeSlice[T]
function UnityEngine.UIElements.UIR.TempAllocator:Alloc(count) end
function UnityEngine.UIElements.UIR.TempAllocator:Reset() end
---@return UnityEngine.UIElements.UIR.TempAllocator.Statistics[T]
function UnityEngine.UIElements.UIR.TempAllocator:GatherStatistics() end

---@class UnityEngine.UIElements.UIR.TempAllocator.Page : System.ValueType
---@field array Unity.Collections.NativeArray[T]
---@field used number
UnityEngine.UIElements.UIR.TempAllocator.Page = {}
---@alias CS.UnityEngine.UIElements.UIR.TempAllocator.Page UnityEngine.UIElements.UIR.TempAllocator.Page
CS.UnityEngine.UIElements.UIR.TempAllocator.Page = UnityEngine.UIElements.UIR.TempAllocator.Page


---@class UnityEngine.UIElements.UIR.TempAllocator.PageStatistics : System.ValueType
---@field size number
---@field used number
UnityEngine.UIElements.UIR.TempAllocator.PageStatistics = {}
---@alias CS.UnityEngine.UIElements.UIR.TempAllocator.PageStatistics UnityEngine.UIElements.UIR.TempAllocator.PageStatistics
CS.UnityEngine.UIElements.UIR.TempAllocator.PageStatistics = UnityEngine.UIElements.UIR.TempAllocator.PageStatistics


---@class UnityEngine.UIElements.UIR.TempAllocator.Statistics : System.ValueType
---@field pool UnityEngine.UIElements.UIR.TempAllocator.PageStatistics[T]
---@field excess UnityEngine.UIElements.UIR.TempAllocator.PageStatistics[T][]
UnityEngine.UIElements.UIR.TempAllocator.Statistics = {}
---@alias CS.UnityEngine.UIElements.UIR.TempAllocator.Statistics UnityEngine.UIElements.UIR.TempAllocator.Statistics
CS.UnityEngine.UIElements.UIR.TempAllocator.Statistics = UnityEngine.UIElements.UIR.TempAllocator.Statistics


---@class UnityEngine.UIElements.UIR.TextCoreSettings : System.ValueType
---@field faceColor UnityEngine.Color
---@field outlineColor UnityEngine.Color
---@field outlineWidth number
---@field underlayColor UnityEngine.Color
---@field underlayOffset UnityEngine.Vector2
---@field underlaySoftness number
UnityEngine.UIElements.UIR.TextCoreSettings = {}
---@alias CS.UnityEngine.UIElements.UIR.TextCoreSettings UnityEngine.UIElements.UIR.TextCoreSettings
CS.UnityEngine.UIElements.UIR.TextCoreSettings = UnityEngine.UIElements.UIR.TextCoreSettings

---@overload fun(self: UnityEngine.UIElements.UIR.TextCoreSettings, obj: System.Object) : boolean
---@param other UnityEngine.UIElements.UIR.TextCoreSettings
---@return boolean
function UnityEngine.UIElements.UIR.TextCoreSettings:Equals(other) end
---@return number
function UnityEngine.UIElements.UIR.TextCoreSettings:GetHashCode() end

---@class UnityEngine.UIElements.UIR.TextureBlitter : System.Object
---@field queueLength number
UnityEngine.UIElements.UIR.TextureBlitter = {}
---@alias CS.UnityEngine.UIElements.UIR.TextureBlitter UnityEngine.UIElements.UIR.TextureBlitter
CS.UnityEngine.UIElements.UIR.TextureBlitter = UnityEngine.UIElements.UIR.TextureBlitter

---@param capacity number
---@return UnityEngine.UIElements.UIR.TextureBlitter
function UnityEngine.UIElements.UIR.TextureBlitter.New(capacity) end
function UnityEngine.UIElements.UIR.TextureBlitter:Dispose() end
---@param src UnityEngine.Texture
---@param srcRect UnityEngine.RectInt
---@param dstPos UnityEngine.Vector2Int
---@param addBorder boolean
---@param tint UnityEngine.Color
function UnityEngine.UIElements.UIR.TextureBlitter:QueueBlit(src, srcRect, dstPos, addBorder, tint) end
---@param dst UnityEngine.RenderTexture
---@param src UnityEngine.Texture
---@param srcRect UnityEngine.RectInt
---@param dstPos UnityEngine.Vector2Int
---@param addBorder boolean
---@param tint UnityEngine.Color
function UnityEngine.UIElements.UIR.TextureBlitter:BlitOneNow(dst, src, srcRect, dstPos, addBorder, tint) end
---@param dst UnityEngine.RenderTexture
function UnityEngine.UIElements.UIR.TextureBlitter:Commit(dst) end
function UnityEngine.UIElements.UIR.TextureBlitter:Reset() end

---@class UnityEngine.UIElements.UIR.TextureBlitter.BlitInfo : System.ValueType
---@field src UnityEngine.Texture
---@field srcRect UnityEngine.RectInt
---@field dstPos UnityEngine.Vector2Int
---@field border number
---@field tint UnityEngine.Color
UnityEngine.UIElements.UIR.TextureBlitter.BlitInfo = {}
---@alias CS.UnityEngine.UIElements.UIR.TextureBlitter.BlitInfo UnityEngine.UIElements.UIR.TextureBlitter.BlitInfo
CS.UnityEngine.UIElements.UIR.TextureBlitter.BlitInfo = UnityEngine.UIElements.UIR.TextureBlitter.BlitInfo


---@class UnityEngine.UIElements.UIR.TextureEntry : System.ValueType
---@field source UnityEngine.Texture
---@field actual UnityEngine.UIElements.TextureId
---@field replaced boolean
UnityEngine.UIElements.UIR.TextureEntry = {}
---@alias CS.UnityEngine.UIElements.UIR.TextureEntry UnityEngine.UIElements.UIR.TextureEntry
CS.UnityEngine.UIElements.UIR.TextureEntry = UnityEngine.UIElements.UIR.TextureEntry


---@class UnityEngine.UIElements.UIR.TextureSlotManager : System.Object
---@field FreeSlots number
UnityEngine.UIElements.UIR.TextureSlotManager = {}
---@alias CS.UnityEngine.UIElements.UIR.TextureSlotManager UnityEngine.UIElements.UIR.TextureSlotManager
CS.UnityEngine.UIElements.UIR.TextureSlotManager = UnityEngine.UIElements.UIR.TextureSlotManager

---@return UnityEngine.UIElements.UIR.TextureSlotManager
function UnityEngine.UIElements.UIR.TextureSlotManager.New() end
function UnityEngine.UIElements.UIR.TextureSlotManager:Reset() end
function UnityEngine.UIElements.UIR.TextureSlotManager:StartNewBatch() end
---@param id UnityEngine.UIElements.TextureId
---@return number
function UnityEngine.UIElements.UIR.TextureSlotManager:IndexOf(id) end
---@param slotIndex number
function UnityEngine.UIElements.UIR.TextureSlotManager:MarkUsed(slotIndex) end
---@return number
function UnityEngine.UIElements.UIR.TextureSlotManager:FindOldestSlot() end
---@param id UnityEngine.UIElements.TextureId
---@param sdfScale number
---@param slot number
---@param mat UnityEngine.MaterialPropertyBlock
function UnityEngine.UIElements.UIR.TextureSlotManager:Bind(id, sdfScale, slot, mat) end
---@param slotIndex number
---@param id UnityEngine.UIElements.TextureId
---@param textureWidth number
---@param textureHeight number
---@param sdfScale number
function UnityEngine.UIElements.UIR.TextureSlotManager:SetGpuData(slotIndex, id, textureWidth, textureHeight, sdfScale) end

---@class UnityEngine.UIElements.UIR.Transform3x4 : System.ValueType
---@field v0 UnityEngine.Vector4
---@field v1 UnityEngine.Vector4
---@field v2 UnityEngine.Vector4
UnityEngine.UIElements.UIR.Transform3x4 = {}
---@alias CS.UnityEngine.UIElements.UIR.Transform3x4 UnityEngine.UIElements.UIR.Transform3x4
CS.UnityEngine.UIElements.UIR.Transform3x4 = UnityEngine.UIElements.UIR.Transform3x4


---@class UnityEngine.UIElements.UIR.UIRenderDevice : System.Object
UnityEngine.UIElements.UIR.UIRenderDevice = {}
---@alias CS.UnityEngine.UIElements.UIR.UIRenderDevice UnityEngine.UIElements.UIR.UIRenderDevice
CS.UnityEngine.UIElements.UIR.UIRenderDevice = UnityEngine.UIElements.UIR.UIRenderDevice

---@param initialVertexCapacity number
---@param initialIndexCapacity number
---@return UnityEngine.UIElements.UIR.UIRenderDevice
function UnityEngine.UIElements.UIR.UIRenderDevice.New(initialVertexCapacity, initialIndexCapacity) end
function UnityEngine.UIElements.UIR.UIRenderDevice.ProcessDeviceFreeQueue() end
function UnityEngine.UIElements.UIR.UIRenderDevice:Dispose() end
---@param vertexCount number
---@param indexCount number
---@param out_vertexData Unity.Collections.NativeSlice
---@param out_indexData Unity.Collections.NativeSlice
---@param out_indexOffset number
---@return UnityEngine.UIElements.UIR.MeshHandle, Unity.Collections.NativeSlice, Unity.Collections.NativeSlice, number
function UnityEngine.UIElements.UIR.UIRenderDevice:Allocate(vertexCount, indexCount, out_vertexData, out_indexData, out_indexOffset) end
---@overload fun(self: UnityEngine.UIElements.UIR.UIRenderDevice, mesh: UnityEngine.UIElements.UIR.MeshHandle, vertexCount: number, out_vertexData: Unity.Collections.NativeSlice) : Unity.Collections.NativeSlice
---@param mesh UnityEngine.UIElements.UIR.MeshHandle
---@param vertexCount number
---@param indexCount number
---@param out_vertexData Unity.Collections.NativeSlice
---@param out_indexData Unity.Collections.NativeSlice
---@param out_indexOffset number
---@return Unity.Collections.NativeSlice, Unity.Collections.NativeSlice, number
function UnityEngine.UIElements.UIR.UIRenderDevice:Update(mesh, vertexCount, indexCount, out_vertexData, out_indexData, out_indexOffset) end
---@param mesh UnityEngine.UIElements.UIR.MeshHandle
function UnityEngine.UIElements.UIR.UIRenderDevice:Free(mesh) end
function UnityEngine.UIElements.UIR.UIRenderDevice:OnFrameRenderingBegin() end
---@param head UnityEngine.UIElements.UIR.RenderChainCommand
---@param initialMat UnityEngine.Material
---@param defaultMat UnityEngine.Material
---@param gradientSettings UnityEngine.Texture
---@param shaderInfo UnityEngine.Texture
---@param pixelsPerPoint number
---@param transforms Unity.Collections.NativeSlice
---@param clipRects Unity.Collections.NativeSlice
---@param stateMatProps UnityEngine.MaterialPropertyBlock
---@param allowMaterialChange boolean
---@param ref_immediateException System.Exception
---@return System.Exception
function UnityEngine.UIElements.UIR.UIRenderDevice:EvaluateChain(head, initialMat, defaultMat, gradientSettings, shaderInfo, pixelsPerPoint, transforms, clipRects, stateMatProps, allowMaterialChange, ref_immediateException) end
function UnityEngine.UIElements.UIR.UIRenderDevice:AdvanceFrame() end

---@class UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics : System.ValueType
---@field pages UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics.PageStatistics[]
---@field freesDeferred number[]
---@field completeInit boolean
UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics = {}
---@alias CS.UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics
CS.UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics = UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics


---@class UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics.PageStatistics : System.ValueType
UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics.PageStatistics = {}
---@alias CS.UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics.PageStatistics UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics.PageStatistics
CS.UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics.PageStatistics = UnityEngine.UIElements.UIR.UIRenderDevice.AllocationStatistics.PageStatistics


---@class UnityEngine.UIElements.UIR.UIRenderDevice.AllocToFree : System.ValueType
---@field alloc UnityEngine.UIElements.UIR.Alloc
---@field page UnityEngine.UIElements.UIR.Page
---@field vertices boolean
UnityEngine.UIElements.UIR.UIRenderDevice.AllocToFree = {}
---@alias CS.UnityEngine.UIElements.UIR.UIRenderDevice.AllocToFree UnityEngine.UIElements.UIR.UIRenderDevice.AllocToFree
CS.UnityEngine.UIElements.UIR.UIRenderDevice.AllocToFree = UnityEngine.UIElements.UIR.UIRenderDevice.AllocToFree


---@class UnityEngine.UIElements.UIR.UIRenderDevice.AllocToUpdate : System.ValueType
---@field id number
---@field allocTime number
---@field meshHandle UnityEngine.UIElements.UIR.MeshHandle
---@field permAllocVerts UnityEngine.UIElements.UIR.Alloc
---@field permAllocIndices UnityEngine.UIElements.UIR.Alloc
---@field permPage UnityEngine.UIElements.UIR.Page
---@field copyBackIndices boolean
UnityEngine.UIElements.UIR.UIRenderDevice.AllocToUpdate = {}
---@alias CS.UnityEngine.UIElements.UIR.UIRenderDevice.AllocToUpdate UnityEngine.UIElements.UIR.UIRenderDevice.AllocToUpdate
CS.UnityEngine.UIElements.UIR.UIRenderDevice.AllocToUpdate = UnityEngine.UIElements.UIR.UIRenderDevice.AllocToUpdate


---@class UnityEngine.UIElements.UIR.UIRenderDevice.DeviceToFree : System.ValueType
---@field handle number
---@field page UnityEngine.UIElements.UIR.Page
UnityEngine.UIElements.UIR.UIRenderDevice.DeviceToFree = {}
---@alias CS.UnityEngine.UIElements.UIR.UIRenderDevice.DeviceToFree UnityEngine.UIElements.UIR.UIRenderDevice.DeviceToFree
CS.UnityEngine.UIElements.UIR.UIRenderDevice.DeviceToFree = UnityEngine.UIElements.UIR.UIRenderDevice.DeviceToFree

function UnityEngine.UIElements.UIR.UIRenderDevice.DeviceToFree:Dispose() end

---@class UnityEngine.UIElements.UIR.UIRenderDevice.DrawStatistics : System.ValueType
---@field currentFrameIndex number
---@field totalIndices number
---@field commandCount number
---@field drawCommandCount number
---@field materialSetCount number
---@field drawRangeCount number
---@field drawRangeCallCount number
---@field immediateDraws number
---@field stencilRefChanges number
UnityEngine.UIElements.UIR.UIRenderDevice.DrawStatistics = {}
---@alias CS.UnityEngine.UIElements.UIR.UIRenderDevice.DrawStatistics UnityEngine.UIElements.UIR.UIRenderDevice.DrawStatistics
CS.UnityEngine.UIElements.UIR.UIRenderDevice.DrawStatistics = UnityEngine.UIElements.UIR.UIRenderDevice.DrawStatistics


---@class UnityEngine.UIElements.UIR.UIRenderDevice.EvaluationState : System.ValueType
---@field stateMatProps UnityEngine.MaterialPropertyBlock
---@field defaultMat UnityEngine.Material
---@field curState UnityEngine.UIElements.UIR.State
---@field curPage UnityEngine.UIElements.UIR.Page
---@field mustApplyMaterial boolean
---@field mustApplyCommonBlock boolean
---@field mustApplyStateBlock boolean
---@field mustApplyStencil boolean
UnityEngine.UIElements.UIR.UIRenderDevice.EvaluationState = {}
---@alias CS.UnityEngine.UIElements.UIR.UIRenderDevice.EvaluationState UnityEngine.UIElements.UIR.UIRenderDevice.EvaluationState
CS.UnityEngine.UIElements.UIR.UIRenderDevice.EvaluationState = UnityEngine.UIElements.UIR.UIRenderDevice.EvaluationState


---@class UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator : System.ValueType
---@field identityTransform UnityEngine.UIElements.UIR.BMPAlloc
---@field infiniteClipRect UnityEngine.UIElements.UIR.BMPAlloc
---@field fullOpacity UnityEngine.UIElements.UIR.BMPAlloc
---@field clearColor UnityEngine.UIElements.UIR.BMPAlloc
---@field defaultTextCoreSettings UnityEngine.UIElements.UIR.BMPAlloc
---@field transformConstants Unity.Collections.NativeSlice
---@field clipRectConstants Unity.Collections.NativeSlice
---@field atlas UnityEngine.Texture
---@field internalAtlasCreated boolean
UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator = {}
---@alias CS.UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator
CS.UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator = UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator

function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:Construct() end
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:Dispose() end
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:IssuePendingStorageChanges() end
---@return UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:AllocTransform() end
---@return UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:AllocClipRect() end
---@return UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:AllocOpacity() end
---@return UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:AllocColor() end
---@param settings UnityEngine.UIElements.UIR.TextCoreSettings
---@return UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:AllocTextCoreSettings(settings) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@param xform UnityEngine.Matrix4x4
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:SetTransformValue(alloc, xform) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@param clipRect UnityEngine.Vector4
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:SetClipRectValue(alloc, clipRect) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@param opacity number
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:SetOpacityValue(alloc, opacity) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@param color UnityEngine.Color
---@param isEditorContext boolean
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:SetColorValue(alloc, color, isEditorContext) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@param settings UnityEngine.UIElements.UIR.TextCoreSettings
---@param isEditorContext boolean
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:SetTextCoreSettingValue(alloc, settings, isEditorContext) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:FreeTransform(alloc) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:FreeClipRect(alloc) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:FreeOpacity(alloc) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:FreeColor(alloc) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:FreeTextCoreSettings(alloc) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@return UnityEngine.Color32
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:TransformAllocToVertexData(alloc) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@return UnityEngine.Color32
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:ClipRectAllocToVertexData(alloc) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@return UnityEngine.Color32
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:OpacityAllocToVertexData(alloc) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@return UnityEngine.Color32
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:ColorAllocToVertexData(alloc) end
---@param alloc UnityEngine.UIElements.UIR.BMPAlloc
---@return UnityEngine.Color32
function UnityEngine.UIElements.UIR.UIRVEShaderInfoAllocator:TextCoreSettingsToVertexData(alloc) end

---@class UnityEngine.UIElements.UIR.Utility : System.Object
UnityEngine.UIElements.UIR.Utility = {}
---@alias CS.UnityEngine.UIElements.UIR.Utility UnityEngine.UIElements.UIR.Utility
CS.UnityEngine.UIElements.UIR.Utility = UnityEngine.UIElements.UIR.Utility

---@return UnityEngine.UIElements.UIR.Utility
function UnityEngine.UIElements.UIR.Utility.New() end
---@param vertexAttributes UnityEngine.Rendering.VertexAttributeDescriptor[]
---@return System.IntPtr
function UnityEngine.UIElements.UIR.Utility.GetVertexDeclaration(vertexAttributes) end
---@param camera UnityEngine.Camera
---@param material UnityEngine.Material
---@param transform UnityEngine.Matrix4x4
---@param aabb UnityEngine.Bounds
---@param renderLayer number
---@param shadowCasting number
---@param receiveShadows boolean
---@param sameDistanceSortPriority number
---@param sceneCullingMask number
---@param rendererCallbackFlags number
---@param userData System.IntPtr
---@param userDataSize number
function UnityEngine.UIElements.UIR.Utility.RegisterIntermediateRenderer(camera, material, transform, aabb, renderLayer, shadowCasting, receiveShadows, sameDistanceSortPriority, sceneCullingMask, rendererCallbackFlags, userData, userDataSize) end
---@param ib System.IntPtr
---@param vertexStreams System.IntPtr*
---@param streamCount number
---@param ranges System.IntPtr
---@param rangeCount number
---@param vertexDecl System.IntPtr
function UnityEngine.UIElements.UIR.Utility.DrawRanges(ib, vertexStreams, streamCount, ranges, rangeCount, vertexDecl) end
---@param props UnityEngine.MaterialPropertyBlock
function UnityEngine.UIElements.UIR.Utility.SetPropertyBlock(props) end
---@param scissorRect UnityEngine.RectInt
function UnityEngine.UIElements.UIR.Utility.SetScissorRect(scissorRect) end
function UnityEngine.UIElements.UIR.Utility.DisableScissor() end
---@return boolean
function UnityEngine.UIElements.UIR.Utility.IsScissorEnabled() end
---@param stencilState UnityEngine.Rendering.StencilState
---@return System.IntPtr
function UnityEngine.UIElements.UIR.Utility.CreateStencilState(stencilState) end
---@param stencilState System.IntPtr
---@param stencilRef number
function UnityEngine.UIElements.UIR.Utility.SetStencilState(stencilState, stencilRef) end
---@return boolean
function UnityEngine.UIElements.UIR.Utility.HasMappedBufferRange() end
---@return number
function UnityEngine.UIElements.UIR.Utility.InsertCPUFence() end
---@param fence number
---@return boolean
function UnityEngine.UIElements.UIR.Utility.CPUFencePassed(fence) end
---@param fence number
function UnityEngine.UIElements.UIR.Utility.WaitForCPUFencePassed(fence) end
function UnityEngine.UIElements.UIR.Utility.SyncRenderThread() end
---@return UnityEngine.RectInt
function UnityEngine.UIElements.UIR.Utility.GetActiveViewport() end
function UnityEngine.UIElements.UIR.Utility.ProfileDrawChainBegin() end
function UnityEngine.UIElements.UIR.Utility.ProfileDrawChainEnd() end
---@param subscribe boolean
function UnityEngine.UIElements.UIR.Utility.NotifyOfUIREvents(subscribe) end
---@return UnityEngine.Matrix4x4
function UnityEngine.UIElements.UIR.Utility.GetUnityProjectionMatrix() end
---@return UnityEngine.Matrix4x4
function UnityEngine.UIElements.UIR.Utility.GetDeviceProjectionMatrix() end
---@return boolean
function UnityEngine.UIElements.UIR.Utility.DebugIsMainThread() end

---@class UnityEngine.UIElements.UIR.Utility.GPUBuffer : System.Object
---@field ElementStride number
---@field Count number
UnityEngine.UIElements.UIR.Utility.GPUBuffer = {}
---@alias CS.UnityEngine.UIElements.UIR.Utility.GPUBuffer UnityEngine.UIElements.UIR.Utility.GPUBuffer
CS.UnityEngine.UIElements.UIR.Utility.GPUBuffer = UnityEngine.UIElements.UIR.Utility.GPUBuffer

---@param elementCount number
---@param type UnityEngine.UIElements.UIR.Utility.GPUBufferType
---@return UnityEngine.UIElements.UIR.Utility.GPUBuffer
function UnityEngine.UIElements.UIR.Utility.GPUBuffer.New(elementCount, type) end
function UnityEngine.UIElements.UIR.Utility.GPUBuffer:Dispose() end
---@param ranges Unity.Collections.NativeSlice
---@param rangesMin number
---@param rangesMax number
function UnityEngine.UIElements.UIR.Utility.GPUBuffer:UpdateRanges(ranges, rangesMin, rangesMax) end

---@class UnityEngine.UIElements.UIR.Utility.GPUBufferType
---@field Vertex UnityEngine.UIElements.UIR.Utility.GPUBufferType
---@field Index UnityEngine.UIElements.UIR.Utility.GPUBufferType
UnityEngine.UIElements.UIR.Utility.GPUBufferType = {}
---@alias CS.UnityEngine.UIElements.UIR.Utility.GPUBufferType UnityEngine.UIElements.UIR.Utility.GPUBufferType
CS.UnityEngine.UIElements.UIR.Utility.GPUBufferType = UnityEngine.UIElements.UIR.Utility.GPUBufferType


---@class UnityEngine.UIElements.UIR.Utility.RendererCallbacks
---@field RendererCallback_Init UnityEngine.UIElements.UIR.Utility.RendererCallbacks
---@field RendererCallback_Exec UnityEngine.UIElements.UIR.Utility.RendererCallbacks
---@field RendererCallback_Cleanup UnityEngine.UIElements.UIR.Utility.RendererCallbacks
UnityEngine.UIElements.UIR.Utility.RendererCallbacks = {}
---@alias CS.UnityEngine.UIElements.UIR.Utility.RendererCallbacks UnityEngine.UIElements.UIR.Utility.RendererCallbacks
CS.UnityEngine.UIElements.UIR.Utility.RendererCallbacks = UnityEngine.UIElements.UIR.Utility.RendererCallbacks


---@class UnityEngine.UIElements.UIR.VectorImageManager : System.Object
---@field instances System.Collections.Generic.List
---@field atlas UnityEngine.Texture2D
UnityEngine.UIElements.UIR.VectorImageManager = {}
---@alias CS.UnityEngine.UIElements.UIR.VectorImageManager UnityEngine.UIElements.UIR.VectorImageManager
CS.UnityEngine.UIElements.UIR.VectorImageManager = UnityEngine.UIElements.UIR.VectorImageManager

---@param atlas UnityEngine.UIElements.AtlasBase
---@return UnityEngine.UIElements.UIR.VectorImageManager
function UnityEngine.UIElements.UIR.VectorImageManager.New(atlas) end
function UnityEngine.UIElements.UIR.VectorImageManager:Dispose() end
function UnityEngine.UIElements.UIR.VectorImageManager:Reset() end
function UnityEngine.UIElements.UIR.VectorImageManager:Commit() end
---@param vi UnityEngine.UIElements.VectorImage
---@param context UnityEngine.UIElements.VisualElement
---@return UnityEngine.UIElements.UIR.GradientRemap
function UnityEngine.UIElements.UIR.VectorImageManager:AddUser(vi, context) end
---@param vi UnityEngine.UIElements.VectorImage
function UnityEngine.UIElements.UIR.VectorImageManager:RemoveUser(vi) end

---@class UnityEngine.UIElements.UIR.VectorImageRenderInfo : UnityEngine.UIElements.UIR.LinkedPoolItem
---@field useCount number
---@field firstGradientRemap UnityEngine.UIElements.UIR.GradientRemap
---@field gradientSettingsAlloc UnityEngine.UIElements.UIR.Alloc
UnityEngine.UIElements.UIR.VectorImageRenderInfo = {}
---@alias CS.UnityEngine.UIElements.UIR.VectorImageRenderInfo UnityEngine.UIElements.UIR.VectorImageRenderInfo
CS.UnityEngine.UIElements.UIR.VectorImageRenderInfo = UnityEngine.UIElements.UIR.VectorImageRenderInfo

---@return UnityEngine.UIElements.UIR.VectorImageRenderInfo
function UnityEngine.UIElements.UIR.VectorImageRenderInfo.New() end
function UnityEngine.UIElements.UIR.VectorImageRenderInfo:Reset() end

---@class UnityEngine.UIElements.UIR.VectorImageRenderInfoPool : UnityEngine.UIElements.UIR.LinkedPool
UnityEngine.UIElements.UIR.VectorImageRenderInfoPool = {}
---@alias CS.UnityEngine.UIElements.UIR.VectorImageRenderInfoPool UnityEngine.UIElements.UIR.VectorImageRenderInfoPool
CS.UnityEngine.UIElements.UIR.VectorImageRenderInfoPool = UnityEngine.UIElements.UIR.VectorImageRenderInfoPool

---@return UnityEngine.UIElements.UIR.VectorImageRenderInfoPool
function UnityEngine.UIElements.UIR.VectorImageRenderInfoPool.New() end

---@class UnityEngine.UIElements.UIR.VertexFlags
---@field IsSolid UnityEngine.UIElements.UIR.VertexFlags
---@field IsText UnityEngine.UIElements.UIR.VertexFlags
---@field IsTextured UnityEngine.UIElements.UIR.VertexFlags
---@field IsDynamic UnityEngine.UIElements.UIR.VertexFlags
---@field IsSvgGradients UnityEngine.UIElements.UIR.VertexFlags
---@field IsGraphViewEdge UnityEngine.UIElements.UIR.VertexFlags
UnityEngine.UIElements.UIR.VertexFlags = {}
---@alias CS.UnityEngine.UIElements.UIR.VertexFlags UnityEngine.UIElements.UIR.VertexFlags
CS.UnityEngine.UIElements.UIR.VertexFlags = UnityEngine.UIElements.UIR.VertexFlags


---@class UnityEngine.UIElements.UIRAtlasAllocator : System.Object
---@field maxAtlasSize number
---@field maxImageWidth number
---@field maxImageHeight number
---@field virtualWidth number
---@field virtualHeight number
---@field physicalWidth number
---@field physicalHeight number
UnityEngine.UIElements.UIRAtlasAllocator = {}
---@alias CS.UnityEngine.UIElements.UIRAtlasAllocator UnityEngine.UIElements.UIRAtlasAllocator
CS.UnityEngine.UIElements.UIRAtlasAllocator = UnityEngine.UIElements.UIRAtlasAllocator

---@param initialAtlasSize number
---@param maxAtlasSize number
---@param sidePadding number
---@return UnityEngine.UIElements.UIRAtlasAllocator
function UnityEngine.UIElements.UIRAtlasAllocator.New(initialAtlasSize, maxAtlasSize, sidePadding) end
function UnityEngine.UIElements.UIRAtlasAllocator:Dispose() end
---@param width number
---@param height number
---@param out_location UnityEngine.RectInt
---@return boolean, UnityEngine.RectInt
function UnityEngine.UIElements.UIRAtlasAllocator:TryAllocate(width, height, out_location) end

---@class UnityEngine.UIElements.UIRAtlasAllocator.AreaNode : System.Object
---@field rect UnityEngine.RectInt
---@field previous UnityEngine.UIElements.UIRAtlasAllocator.AreaNode
---@field next UnityEngine.UIElements.UIRAtlasAllocator.AreaNode
UnityEngine.UIElements.UIRAtlasAllocator.AreaNode = {}
---@alias CS.UnityEngine.UIElements.UIRAtlasAllocator.AreaNode UnityEngine.UIElements.UIRAtlasAllocator.AreaNode
CS.UnityEngine.UIElements.UIRAtlasAllocator.AreaNode = UnityEngine.UIElements.UIRAtlasAllocator.AreaNode

---@return UnityEngine.UIElements.UIRAtlasAllocator.AreaNode
function UnityEngine.UIElements.UIRAtlasAllocator.AreaNode.New() end
---@param rect UnityEngine.RectInt
---@return UnityEngine.UIElements.UIRAtlasAllocator.AreaNode
function UnityEngine.UIElements.UIRAtlasAllocator.AreaNode.Acquire(rect) end
function UnityEngine.UIElements.UIRAtlasAllocator.AreaNode:Release() end
function UnityEngine.UIElements.UIRAtlasAllocator.AreaNode:RemoveFromChain() end
---@param previous UnityEngine.UIElements.UIRAtlasAllocator.AreaNode
function UnityEngine.UIElements.UIRAtlasAllocator.AreaNode:AddAfter(previous) end

---@class UnityEngine.UIElements.UIRAtlasAllocator.Row : System.Object
---@field Cursor number
---@field offsetX number
---@field offsetY number
---@field width number
---@field height number
UnityEngine.UIElements.UIRAtlasAllocator.Row = {}
---@alias CS.UnityEngine.UIElements.UIRAtlasAllocator.Row UnityEngine.UIElements.UIRAtlasAllocator.Row
CS.UnityEngine.UIElements.UIRAtlasAllocator.Row = UnityEngine.UIElements.UIRAtlasAllocator.Row

---@return UnityEngine.UIElements.UIRAtlasAllocator.Row
function UnityEngine.UIElements.UIRAtlasAllocator.Row.New() end
---@param offsetX number
---@param offsetY number
---@param width number
---@param height number
---@return UnityEngine.UIElements.UIRAtlasAllocator.Row
function UnityEngine.UIElements.UIRAtlasAllocator.Row.Acquire(offsetX, offsetY, width, height) end
function UnityEngine.UIElements.UIRAtlasAllocator.Row:Release() end

---@class UnityEngine.UIElements.UIRLayoutUpdater : UnityEngine.UIElements.BaseVisualTreeUpdater
---@field profilerMarker Unity.Profiling.ProfilerMarker
UnityEngine.UIElements.UIRLayoutUpdater = {}
---@alias CS.UnityEngine.UIElements.UIRLayoutUpdater UnityEngine.UIElements.UIRLayoutUpdater
CS.UnityEngine.UIElements.UIRLayoutUpdater = UnityEngine.UIElements.UIRLayoutUpdater

---@return UnityEngine.UIElements.UIRLayoutUpdater
function UnityEngine.UIElements.UIRLayoutUpdater.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param versionChangeType UnityEngine.UIElements.VersionChangeType
function UnityEngine.UIElements.UIRLayoutUpdater:OnVersionChanged(ve, versionChangeType) end
function UnityEngine.UIElements.UIRLayoutUpdater:Update() end

---@class UnityEngine.UIElements.UIRRepaintUpdater : UnityEngine.UIElements.BaseVisualTreeUpdater
---@field profilerMarker Unity.Profiling.ProfilerMarker
---@field drawStats boolean
---@field breakBatches boolean
UnityEngine.UIElements.UIRRepaintUpdater = {}
---@alias CS.UnityEngine.UIElements.UIRRepaintUpdater UnityEngine.UIElements.UIRRepaintUpdater
CS.UnityEngine.UIElements.UIRRepaintUpdater = UnityEngine.UIElements.UIRRepaintUpdater

---@return UnityEngine.UIElements.UIRRepaintUpdater
function UnityEngine.UIElements.UIRRepaintUpdater.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param versionChangeType UnityEngine.UIElements.VersionChangeType
function UnityEngine.UIElements.UIRRepaintUpdater:OnVersionChanged(ve, versionChangeType) end
function UnityEngine.UIElements.UIRRepaintUpdater:Update() end

---@class UnityEngine.UIElements.UIRUtility : System.Object
---@field k_DefaultShaderName string
---@field k_DefaultWorldSpaceShaderName string
---@field k_Epsilon number
---@field k_ClearZ number
---@field k_MeshPosZ number
---@field k_MaskPosZ number
---@field k_MaxMaskDepth number
UnityEngine.UIElements.UIRUtility = {}
---@alias CS.UnityEngine.UIElements.UIRUtility UnityEngine.UIElements.UIRUtility
CS.UnityEngine.UIElements.UIRUtility = UnityEngine.UIElements.UIRUtility

---@param maskDepth number
---@param stencilRef number
---@return boolean
function UnityEngine.UIElements.UIRUtility.ShapeWindingIsClockwise(maskDepth, stencilRef) end
---@param rc UnityEngine.Rect
---@return UnityEngine.Vector4
function UnityEngine.UIElements.UIRUtility.ToVector4(rc) end
---@param ve UnityEngine.UIElements.VisualElement
---@return boolean
function UnityEngine.UIElements.UIRUtility.IsRoundRect(ve) end
---@param rotation UnityEngine.Quaternion
---@param ref_point UnityEngine.Vector2
---@return UnityEngine.Vector2
function UnityEngine.UIElements.UIRUtility.Multiply2D(rotation, ref_point) end
---@param ve UnityEngine.UIElements.VisualElement
---@return boolean
function UnityEngine.UIElements.UIRUtility.IsVectorImageBackground(ve) end
---@param ve UnityEngine.UIElements.VisualElement
---@return boolean
function UnityEngine.UIElements.UIRUtility.IsElementSelfHidden(ve) end
---@param obj UnityEngine.Object
function UnityEngine.UIElements.UIRUtility.Destroy(obj) end
---@param n number
---@return number
function UnityEngine.UIElements.UIRUtility.GetPrevPow2(n) end
---@param n number
---@return number
function UnityEngine.UIElements.UIRUtility.GetNextPow2(n) end
---@param n number
---@return number
function UnityEngine.UIElements.UIRUtility.GetNextPow2Exp(n) end

---@class UnityEngine.UIElements.UITKTextHandle : UnityEngine.TextCore.Text.TextHandle
---@field MeasuredSizes UnityEngine.Vector2
---@field RoundedSizes UnityEngine.Vector2
UnityEngine.UIElements.UITKTextHandle = {}
---@alias CS.UnityEngine.UIElements.UITKTextHandle UnityEngine.UIElements.UITKTextHandle
CS.UnityEngine.UIElements.UITKTextHandle = UnityEngine.UIElements.UITKTextHandle

---@param te UnityEngine.UIElements.TextElement
---@return UnityEngine.UIElements.UITKTextHandle
function UnityEngine.UIElements.UITKTextHandle.New(te) end
---@param textToMeasure string
---@param wordWrap boolean
---@param width number
---@param height number
---@return number
function UnityEngine.UIElements.UITKTextHandle:ComputeTextWidth(textToMeasure, wordWrap, width, height) end
---@param textToMeasure string
---@param width number
---@param height number
---@return number
function UnityEngine.UIElements.UITKTextHandle:ComputeTextHeight(textToMeasure, width, height) end
---@return UnityEngine.TextCore.Text.TextInfo
function UnityEngine.UIElements.UITKTextHandle:Update() end

---@class UnityEngine.UIElements.UnsignedIntegerField : UnityEngine.UIElements.TextValueField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.UnsignedIntegerField = {}
---@alias CS.UnityEngine.UIElements.UnsignedIntegerField UnityEngine.UIElements.UnsignedIntegerField
CS.UnityEngine.UIElements.UnsignedIntegerField = UnityEngine.UIElements.UnsignedIntegerField

---@overload fun() : UnityEngine.UIElements.UnsignedIntegerField
---@overload fun(maxLength: number) : UnityEngine.UIElements.UnsignedIntegerField
---@param label string
---@param maxLength number
---@return UnityEngine.UIElements.UnsignedIntegerField
function UnityEngine.UIElements.UnsignedIntegerField.New(label, maxLength) end
---@param delta UnityEngine.Vector3
---@param speed UnityEngine.UIElements.DeltaSpeed
---@param startValue number
function UnityEngine.UIElements.UnsignedIntegerField:ApplyInputDeviceDelta(delta, speed, startValue) end

---@class UnityEngine.UIElements.UnsignedIntegerField.UnsignedIntegerInput : UnityEngine.UIElements.TextValueField.TextValueInput
UnityEngine.UIElements.UnsignedIntegerField.UnsignedIntegerInput = {}
---@alias CS.UnityEngine.UIElements.UnsignedIntegerField.UnsignedIntegerInput UnityEngine.UIElements.UnsignedIntegerField.UnsignedIntegerInput
CS.UnityEngine.UIElements.UnsignedIntegerField.UnsignedIntegerInput = UnityEngine.UIElements.UnsignedIntegerField.UnsignedIntegerInput

---@param delta UnityEngine.Vector3
---@param speed UnityEngine.UIElements.DeltaSpeed
---@param startValue number
function UnityEngine.UIElements.UnsignedIntegerField.UnsignedIntegerInput:ApplyInputDeviceDelta(delta, speed, startValue) end

---@class UnityEngine.UIElements.UnsignedIntegerField.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.UnsignedIntegerField.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.UnsignedIntegerField.UxmlFactory UnityEngine.UIElements.UnsignedIntegerField.UxmlFactory
CS.UnityEngine.UIElements.UnsignedIntegerField.UxmlFactory = UnityEngine.UIElements.UnsignedIntegerField.UxmlFactory

---@return UnityEngine.UIElements.UnsignedIntegerField.UxmlFactory
function UnityEngine.UIElements.UnsignedIntegerField.UxmlFactory.New() end

---@class UnityEngine.UIElements.UnsignedIntegerField.UxmlTraits : UnityEngine.UIElements.TextValueFieldTraits
UnityEngine.UIElements.UnsignedIntegerField.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.UnsignedIntegerField.UxmlTraits UnityEngine.UIElements.UnsignedIntegerField.UxmlTraits
CS.UnityEngine.UIElements.UnsignedIntegerField.UxmlTraits = UnityEngine.UIElements.UnsignedIntegerField.UxmlTraits

---@return UnityEngine.UIElements.UnsignedIntegerField.UxmlTraits
function UnityEngine.UIElements.UnsignedIntegerField.UxmlTraits.New() end

---@class UnityEngine.UIElements.UnsignedLongField : UnityEngine.UIElements.TextValueField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.UnsignedLongField = {}
---@alias CS.UnityEngine.UIElements.UnsignedLongField UnityEngine.UIElements.UnsignedLongField
CS.UnityEngine.UIElements.UnsignedLongField = UnityEngine.UIElements.UnsignedLongField

---@overload fun() : UnityEngine.UIElements.UnsignedLongField
---@overload fun(maxLength: number) : UnityEngine.UIElements.UnsignedLongField
---@param label string
---@param maxLength number
---@return UnityEngine.UIElements.UnsignedLongField
function UnityEngine.UIElements.UnsignedLongField.New(label, maxLength) end
---@param delta UnityEngine.Vector3
---@param speed UnityEngine.UIElements.DeltaSpeed
---@param startValue number
function UnityEngine.UIElements.UnsignedLongField:ApplyInputDeviceDelta(delta, speed, startValue) end

---@class UnityEngine.UIElements.UnsignedLongField.UnsignedLongInput : UnityEngine.UIElements.TextValueField.TextValueInput
UnityEngine.UIElements.UnsignedLongField.UnsignedLongInput = {}
---@alias CS.UnityEngine.UIElements.UnsignedLongField.UnsignedLongInput UnityEngine.UIElements.UnsignedLongField.UnsignedLongInput
CS.UnityEngine.UIElements.UnsignedLongField.UnsignedLongInput = UnityEngine.UIElements.UnsignedLongField.UnsignedLongInput

---@param delta UnityEngine.Vector3
---@param speed UnityEngine.UIElements.DeltaSpeed
---@param startValue number
function UnityEngine.UIElements.UnsignedLongField.UnsignedLongInput:ApplyInputDeviceDelta(delta, speed, startValue) end

---@class UnityEngine.UIElements.UnsignedLongField.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.UnsignedLongField.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.UnsignedLongField.UxmlFactory UnityEngine.UIElements.UnsignedLongField.UxmlFactory
CS.UnityEngine.UIElements.UnsignedLongField.UxmlFactory = UnityEngine.UIElements.UnsignedLongField.UxmlFactory

---@return UnityEngine.UIElements.UnsignedLongField.UxmlFactory
function UnityEngine.UIElements.UnsignedLongField.UxmlFactory.New() end

---@class UnityEngine.UIElements.UnsignedLongField.UxmlTraits : UnityEngine.UIElements.TextValueFieldTraits
UnityEngine.UIElements.UnsignedLongField.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.UnsignedLongField.UxmlTraits UnityEngine.UIElements.UnsignedLongField.UxmlTraits
CS.UnityEngine.UIElements.UnsignedLongField.UxmlTraits = UnityEngine.UIElements.UnsignedLongField.UxmlTraits

---@return UnityEngine.UIElements.UnsignedLongField.UxmlTraits
function UnityEngine.UIElements.UnsignedLongField.UxmlTraits.New() end

---@class UnityEngine.UIElements.UpgradeConstants : System.Object
---@field EditorNamespace string
---@field EditorAssembly string
UnityEngine.UIElements.UpgradeConstants = {}
---@alias CS.UnityEngine.UIElements.UpgradeConstants UnityEngine.UIElements.UpgradeConstants
CS.UnityEngine.UIElements.UpgradeConstants = UnityEngine.UIElements.UpgradeConstants

---@return UnityEngine.UIElements.UpgradeConstants
function UnityEngine.UIElements.UpgradeConstants.New() end

---@class UnityEngine.UIElements.UQuery : System.Object
UnityEngine.UIElements.UQuery = {}
---@alias CS.UnityEngine.UIElements.UQuery UnityEngine.UIElements.UQuery
CS.UnityEngine.UIElements.UQuery = UnityEngine.UIElements.UQuery


---@class UnityEngine.UIElements.UQuery.FirstQueryMatcher : UnityEngine.UIElements.UQuery.SingleQueryMatcher
---@field Instance UnityEngine.UIElements.UQuery.FirstQueryMatcher
UnityEngine.UIElements.UQuery.FirstQueryMatcher = {}
---@alias CS.UnityEngine.UIElements.UQuery.FirstQueryMatcher UnityEngine.UIElements.UQuery.FirstQueryMatcher
CS.UnityEngine.UIElements.UQuery.FirstQueryMatcher = UnityEngine.UIElements.UQuery.FirstQueryMatcher

---@return UnityEngine.UIElements.UQuery.FirstQueryMatcher
function UnityEngine.UIElements.UQuery.FirstQueryMatcher.New() end
---@return UnityEngine.UIElements.UQuery.SingleQueryMatcher
function UnityEngine.UIElements.UQuery.FirstQueryMatcher:CreateNew() end

---@class UnityEngine.UIElements.UQuery.IndexQueryMatcher : UnityEngine.UIElements.UQuery.SingleQueryMatcher
---@field Instance UnityEngine.UIElements.UQuery.IndexQueryMatcher
---@field matchIndex number
UnityEngine.UIElements.UQuery.IndexQueryMatcher = {}
---@alias CS.UnityEngine.UIElements.UQuery.IndexQueryMatcher UnityEngine.UIElements.UQuery.IndexQueryMatcher
CS.UnityEngine.UIElements.UQuery.IndexQueryMatcher = UnityEngine.UIElements.UQuery.IndexQueryMatcher

---@return UnityEngine.UIElements.UQuery.IndexQueryMatcher
function UnityEngine.UIElements.UQuery.IndexQueryMatcher.New() end
---@param root UnityEngine.UIElements.VisualElement
---@param matchers System.Collections.Generic.List
function UnityEngine.UIElements.UQuery.IndexQueryMatcher:Run(root, matchers) end
---@return UnityEngine.UIElements.UQuery.SingleQueryMatcher
function UnityEngine.UIElements.UQuery.IndexQueryMatcher:CreateNew() end

---@class UnityEngine.UIElements.UQuery.IsOfType : System.Object
---@field s_Instance UnityEngine.UIElements.UQuery.IsOfType
UnityEngine.UIElements.UQuery.IsOfType = {}
---@alias CS.UnityEngine.UIElements.UQuery.IsOfType UnityEngine.UIElements.UQuery.IsOfType
CS.UnityEngine.UIElements.UQuery.IsOfType = UnityEngine.UIElements.UQuery.IsOfType

---@return UnityEngine.UIElements.UQuery.IsOfType
function UnityEngine.UIElements.UQuery.IsOfType.New() end
---@param e System.Object
---@return boolean
function UnityEngine.UIElements.UQuery.IsOfType:Predicate(e) end

---@class UnityEngine.UIElements.UQuery.IVisualPredicateWrapper
UnityEngine.UIElements.UQuery.IVisualPredicateWrapper = {}
---@alias CS.UnityEngine.UIElements.UQuery.IVisualPredicateWrapper UnityEngine.UIElements.UQuery.IVisualPredicateWrapper
CS.UnityEngine.UIElements.UQuery.IVisualPredicateWrapper = UnityEngine.UIElements.UQuery.IVisualPredicateWrapper

---@param e System.Object
---@return boolean
function UnityEngine.UIElements.UQuery.IVisualPredicateWrapper:Predicate(e) end

---@class UnityEngine.UIElements.UQuery.LastQueryMatcher : UnityEngine.UIElements.UQuery.SingleQueryMatcher
---@field Instance UnityEngine.UIElements.UQuery.LastQueryMatcher
UnityEngine.UIElements.UQuery.LastQueryMatcher = {}
---@alias CS.UnityEngine.UIElements.UQuery.LastQueryMatcher UnityEngine.UIElements.UQuery.LastQueryMatcher
CS.UnityEngine.UIElements.UQuery.LastQueryMatcher = UnityEngine.UIElements.UQuery.LastQueryMatcher

---@return UnityEngine.UIElements.UQuery.LastQueryMatcher
function UnityEngine.UIElements.UQuery.LastQueryMatcher.New() end
---@return UnityEngine.UIElements.UQuery.SingleQueryMatcher
function UnityEngine.UIElements.UQuery.LastQueryMatcher:CreateNew() end

---@class UnityEngine.UIElements.UQuery.PredicateWrapper : System.Object
UnityEngine.UIElements.UQuery.PredicateWrapper = {}
---@alias CS.UnityEngine.UIElements.UQuery.PredicateWrapper UnityEngine.UIElements.UQuery.PredicateWrapper
CS.UnityEngine.UIElements.UQuery.PredicateWrapper = UnityEngine.UIElements.UQuery.PredicateWrapper

---@param p System.Func[T,System.Boolean]
---@return UnityEngine.UIElements.UQuery.PredicateWrapper
function UnityEngine.UIElements.UQuery.PredicateWrapper.New(p) end
---@param e System.Object
---@return boolean
function UnityEngine.UIElements.UQuery.PredicateWrapper:Predicate(e) end

---@class UnityEngine.UIElements.UQuery.SingleQueryMatcher : UnityEngine.UIElements.UQuery.UQueryMatcher
---@field match UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.UQuery.SingleQueryMatcher = {}
---@alias CS.UnityEngine.UIElements.UQuery.SingleQueryMatcher UnityEngine.UIElements.UQuery.SingleQueryMatcher
CS.UnityEngine.UIElements.UQuery.SingleQueryMatcher = UnityEngine.UIElements.UQuery.SingleQueryMatcher

---@param root UnityEngine.UIElements.VisualElement
---@param matchers System.Collections.Generic.List
function UnityEngine.UIElements.UQuery.SingleQueryMatcher:Run(root, matchers) end
---@return boolean
function UnityEngine.UIElements.UQuery.SingleQueryMatcher:IsInUse() end
---@return UnityEngine.UIElements.UQuery.SingleQueryMatcher
function UnityEngine.UIElements.UQuery.SingleQueryMatcher:CreateNew() end

---@class UnityEngine.UIElements.UQuery.UQueryMatcher : UnityEngine.UIElements.StyleSheets.HierarchyTraversal
UnityEngine.UIElements.UQuery.UQueryMatcher = {}
---@alias CS.UnityEngine.UIElements.UQuery.UQueryMatcher UnityEngine.UIElements.UQuery.UQueryMatcher
CS.UnityEngine.UIElements.UQuery.UQueryMatcher = UnityEngine.UIElements.UQuery.UQueryMatcher

---@param element UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UQuery.UQueryMatcher:Traverse(element) end
---@param element UnityEngine.UIElements.VisualElement
---@param depth number
function UnityEngine.UIElements.UQuery.UQueryMatcher:TraverseRecursive(element, depth) end
---@param root UnityEngine.UIElements.VisualElement
---@param matchers System.Collections.Generic.List
function UnityEngine.UIElements.UQuery.UQueryMatcher:Run(root, matchers) end

---@class UnityEngine.UIElements.UQueryBuilder : System.ValueType
UnityEngine.UIElements.UQueryBuilder = {}
---@alias CS.UnityEngine.UIElements.UQueryBuilder UnityEngine.UIElements.UQueryBuilder
CS.UnityEngine.UIElements.UQueryBuilder = UnityEngine.UIElements.UQueryBuilder

---@param visualElement UnityEngine.UIElements.VisualElement
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder.New(visualElement) end
---@param classname string
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:Class(classname) end
---@param id string
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:Name(id) end
---@param selectorPredicate System.Func[T,System.Boolean]
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:Where(selectorPredicate) end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:Active() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:NotActive() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:Visible() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:NotVisible() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:Hovered() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:NotHovered() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:Checked() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:NotChecked() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:Enabled() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:NotEnabled() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:Focused() end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryBuilder:NotFocused() end
---@return UnityEngine.UIElements.UQueryState[T]
function UnityEngine.UIElements.UQueryBuilder:Build() end
---@return T
function UnityEngine.UIElements.UQueryBuilder:First() end
---@return T
function UnityEngine.UIElements.UQueryBuilder:Last() end
---@overload fun(self: UnityEngine.UIElements.UQueryBuilder) : System.Collections.Generic.List[T]
---@param results System.Collections.Generic.List[T]
function UnityEngine.UIElements.UQueryBuilder:ToList(results) end
---@param index number
---@return T
function UnityEngine.UIElements.UQueryBuilder:AtIndex(index) end
---@param funcCall System.Action[T]
function UnityEngine.UIElements.UQueryBuilder:ForEach(funcCall) end
---@overload fun(self: UnityEngine.UIElements.UQueryBuilder, other: UnityEngine.UIElements.UQueryBuilder) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.UQueryBuilder:Equals(obj) end
---@return number
function UnityEngine.UIElements.UQueryBuilder:GetHashCode() end

---@class UnityEngine.UIElements.UQueryExtensions : System.Object
UnityEngine.UIElements.UQueryExtensions = {}
---@alias CS.UnityEngine.UIElements.UQueryExtensions UnityEngine.UIElements.UQueryExtensions
CS.UnityEngine.UIElements.UQueryExtensions = UnityEngine.UIElements.UQueryExtensions

---@overload fun(e: UnityEngine.UIElements.VisualElement, name: string, classes: string[]) : UnityEngine.UIElements.VisualElement
---@param e UnityEngine.UIElements.VisualElement
---@param name string
---@param className string
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UQueryExtensions.Q(e, name, className) end
---@overload fun(e: UnityEngine.UIElements.VisualElement, name: string, classes: string[]) : UnityEngine.UIElements.UQueryBuilder
---@overload fun(e: UnityEngine.UIElements.VisualElement, name: string, className: string) : UnityEngine.UIElements.UQueryBuilder
---@param e UnityEngine.UIElements.VisualElement
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.UQueryExtensions.Query(e) end

---@class UnityEngine.UIElements.UQueryExtensions.MissingVisualElementException : System.Exception
UnityEngine.UIElements.UQueryExtensions.MissingVisualElementException = {}
---@alias CS.UnityEngine.UIElements.UQueryExtensions.MissingVisualElementException UnityEngine.UIElements.UQueryExtensions.MissingVisualElementException
CS.UnityEngine.UIElements.UQueryExtensions.MissingVisualElementException = UnityEngine.UIElements.UQueryExtensions.MissingVisualElementException

---@overload fun() : UnityEngine.UIElements.UQueryExtensions.MissingVisualElementException
---@param message string
---@return UnityEngine.UIElements.UQueryExtensions.MissingVisualElementException
function UnityEngine.UIElements.UQueryExtensions.MissingVisualElementException.New(message) end

---@class UnityEngine.UIElements.UQueryState : System.ValueType
UnityEngine.UIElements.UQueryState = {}
---@alias CS.UnityEngine.UIElements.UQueryState UnityEngine.UIElements.UQueryState
CS.UnityEngine.UIElements.UQueryState = UnityEngine.UIElements.UQueryState

---@param element UnityEngine.UIElements.VisualElement
---@return UnityEngine.UIElements.UQueryState
function UnityEngine.UIElements.UQueryState:RebuildOn(element) end
---@return T
function UnityEngine.UIElements.UQueryState:First() end
---@return T
function UnityEngine.UIElements.UQueryState:Last() end
---@overload fun(self: UnityEngine.UIElements.UQueryState, results: System.Collections.Generic.List[T])
---@return System.Collections.Generic.List[T]
function UnityEngine.UIElements.UQueryState:ToList() end
---@param index number
---@return T
function UnityEngine.UIElements.UQueryState:AtIndex(index) end
---@param funcCall System.Action[T]
function UnityEngine.UIElements.UQueryState:ForEach(funcCall) end
---@return UnityEngine.UIElements.UQueryState.Enumerator[T]
function UnityEngine.UIElements.UQueryState:GetEnumerator() end
---@overload fun(self: UnityEngine.UIElements.UQueryState, other: UnityEngine.UIElements.UQueryState) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.UQueryState:Equals(obj) end
---@return number
function UnityEngine.UIElements.UQueryState:GetHashCode() end

---@class UnityEngine.UIElements.UQueryState.ActionQueryMatcher : UnityEngine.UIElements.UQuery.UQueryMatcher
UnityEngine.UIElements.UQueryState.ActionQueryMatcher = {}
---@alias CS.UnityEngine.UIElements.UQueryState.ActionQueryMatcher UnityEngine.UIElements.UQueryState.ActionQueryMatcher
CS.UnityEngine.UIElements.UQueryState.ActionQueryMatcher = UnityEngine.UIElements.UQueryState.ActionQueryMatcher

---@return UnityEngine.UIElements.UQueryState.ActionQueryMatcher
function UnityEngine.UIElements.UQueryState.ActionQueryMatcher.New() end

---@class UnityEngine.UIElements.UQueryState.DelegateQueryMatcher : UnityEngine.UIElements.UQuery.UQueryMatcher
---@field s_Instance UnityEngine.UIElements.UQueryState.DelegateQueryMatcher
---@field callBack System.Func[T,TReturnType]
---@field result System.Collections.Generic.List[TReturnType]
UnityEngine.UIElements.UQueryState.DelegateQueryMatcher = {}
---@alias CS.UnityEngine.UIElements.UQueryState.DelegateQueryMatcher UnityEngine.UIElements.UQueryState.DelegateQueryMatcher
CS.UnityEngine.UIElements.UQueryState.DelegateQueryMatcher = UnityEngine.UIElements.UQueryState.DelegateQueryMatcher

---@return UnityEngine.UIElements.UQueryState.DelegateQueryMatcher
function UnityEngine.UIElements.UQueryState.DelegateQueryMatcher.New() end

---@class UnityEngine.UIElements.UQueryState.Enumerator : System.ValueType
---@field Current T
UnityEngine.UIElements.UQueryState.Enumerator = {}
---@alias CS.UnityEngine.UIElements.UQueryState.Enumerator UnityEngine.UIElements.UQueryState.Enumerator
CS.UnityEngine.UIElements.UQueryState.Enumerator = UnityEngine.UIElements.UQueryState.Enumerator

---@return boolean
function UnityEngine.UIElements.UQueryState.Enumerator:MoveNext() end
function UnityEngine.UIElements.UQueryState.Enumerator:Reset() end
function UnityEngine.UIElements.UQueryState.Enumerator:Dispose() end

---@class UnityEngine.UIElements.UQueryState.ListQueryMatcher : UnityEngine.UIElements.UQuery.UQueryMatcher
---@field matches System.Collections.Generic.List[TElement]
UnityEngine.UIElements.UQueryState.ListQueryMatcher = {}
---@alias CS.UnityEngine.UIElements.UQueryState.ListQueryMatcher UnityEngine.UIElements.UQueryState.ListQueryMatcher
CS.UnityEngine.UIElements.UQueryState.ListQueryMatcher = UnityEngine.UIElements.UQueryState.ListQueryMatcher

---@return UnityEngine.UIElements.UQueryState.ListQueryMatcher
function UnityEngine.UIElements.UQueryState.ListQueryMatcher.New() end
function UnityEngine.UIElements.UQueryState.ListQueryMatcher:Reset() end

---@class UnityEngine.UIElements.UsageHints
---@field None UnityEngine.UIElements.UsageHints
---@field DynamicTransform UnityEngine.UIElements.UsageHints
---@field GroupTransform UnityEngine.UIElements.UsageHints
---@field MaskContainer UnityEngine.UIElements.UsageHints
---@field DynamicColor UnityEngine.UIElements.UsageHints
UnityEngine.UIElements.UsageHints = {}
---@alias CS.UnityEngine.UIElements.UsageHints UnityEngine.UIElements.UsageHints
CS.UnityEngine.UIElements.UsageHints = UnityEngine.UIElements.UsageHints


---@class UnityEngine.UIElements.UxmlAsset : System.Object
---@field fullTypeName string
---@field id number
---@field orderInDocument number
---@field parentId number
UnityEngine.UIElements.UxmlAsset = {}
---@alias CS.UnityEngine.UIElements.UxmlAsset UnityEngine.UIElements.UxmlAsset
CS.UnityEngine.UIElements.UxmlAsset = UnityEngine.UIElements.UxmlAsset

---@param fullTypeName string
---@return UnityEngine.UIElements.UxmlAsset
function UnityEngine.UIElements.UxmlAsset.New(fullTypeName) end
---@return System.Collections.Generic.List
function UnityEngine.UIElements.UxmlAsset:GetProperties() end
---@return boolean
function UnityEngine.UIElements.UxmlAsset:HasParent() end
---@param attributeName string
---@return boolean
function UnityEngine.UIElements.UxmlAsset:HasAttribute(attributeName) end
---@param attributeName string
---@return string
function UnityEngine.UIElements.UxmlAsset:GetAttributeValue(attributeName) end
---@param propertyName string
---@param out_value string
---@return boolean, string
function UnityEngine.UIElements.UxmlAsset:TryGetAttributeValue(propertyName, out_value) end
---@param name string
---@param value string
function UnityEngine.UIElements.UxmlAsset:SetAttribute(name, value) end
---@param attributeName string
function UnityEngine.UIElements.UxmlAsset:RemoveAttribute(attributeName) end

---@class UnityEngine.UIElements.UxmlAssetAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription[T]
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlAssetAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlAssetAttributeDescription UnityEngine.UIElements.UxmlAssetAttributeDescription
CS.UnityEngine.UIElements.UxmlAssetAttributeDescription = UnityEngine.UIElements.UxmlAssetAttributeDescription

---@return UnityEngine.UIElements.UxmlAssetAttributeDescription
function UnityEngine.UIElements.UxmlAssetAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return T
function UnityEngine.UIElements.UxmlAssetAttributeDescription:GetValueFromBag(bag, cc) end

---@class UnityEngine.UIElements.UxmlAttributeDescription : System.Object
---@field name string
---@field obsoleteNames System.Collections.Generic.IEnumerable
---@field type string
---@field typeNamespace string
---@field defaultValueAsString string
---@field use UnityEngine.UIElements.UxmlAttributeDescription.Use
---@field restriction UnityEngine.UIElements.UxmlTypeRestriction
UnityEngine.UIElements.UxmlAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlAttributeDescription UnityEngine.UIElements.UxmlAttributeDescription
CS.UnityEngine.UIElements.UxmlAttributeDescription = UnityEngine.UIElements.UxmlAttributeDescription


---@class UnityEngine.UIElements.UxmlAttributeDescription.Use
---@field None UnityEngine.UIElements.UxmlAttributeDescription.Use
---@field Optional UnityEngine.UIElements.UxmlAttributeDescription.Use
---@field Prohibited UnityEngine.UIElements.UxmlAttributeDescription.Use
---@field Required UnityEngine.UIElements.UxmlAttributeDescription.Use
UnityEngine.UIElements.UxmlAttributeDescription.Use = {}
---@alias CS.UnityEngine.UIElements.UxmlAttributeDescription.Use UnityEngine.UIElements.UxmlAttributeDescription.Use
CS.UnityEngine.UIElements.UxmlAttributeDescription.Use = UnityEngine.UIElements.UxmlAttributeDescription.Use


---@class UnityEngine.UIElements.UxmlAttributeOverridesFactory : UnityEngine.UIElements.UxmlFactory
---@field uxmlName string
---@field uxmlQualifiedName string
---@field substituteForTypeName string
---@field substituteForTypeNamespace string
---@field substituteForTypeQualifiedName string
UnityEngine.UIElements.UxmlAttributeOverridesFactory = {}
---@alias CS.UnityEngine.UIElements.UxmlAttributeOverridesFactory UnityEngine.UIElements.UxmlAttributeOverridesFactory
CS.UnityEngine.UIElements.UxmlAttributeOverridesFactory = UnityEngine.UIElements.UxmlAttributeOverridesFactory

---@return UnityEngine.UIElements.UxmlAttributeOverridesFactory
function UnityEngine.UIElements.UxmlAttributeOverridesFactory.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UxmlAttributeOverridesFactory:Create(bag, cc) end

---@class UnityEngine.UIElements.UxmlAttributeOverridesTraits : UnityEngine.UIElements.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.UxmlAttributeOverridesTraits = {}
---@alias CS.UnityEngine.UIElements.UxmlAttributeOverridesTraits UnityEngine.UIElements.UxmlAttributeOverridesTraits
CS.UnityEngine.UIElements.UxmlAttributeOverridesTraits = UnityEngine.UIElements.UxmlAttributeOverridesTraits

---@return UnityEngine.UIElements.UxmlAttributeOverridesTraits
function UnityEngine.UIElements.UxmlAttributeOverridesTraits.New() end

---@class UnityEngine.UIElements.UxmlBoolAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlBoolAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlBoolAttributeDescription UnityEngine.UIElements.UxmlBoolAttributeDescription
CS.UnityEngine.UIElements.UxmlBoolAttributeDescription = UnityEngine.UIElements.UxmlBoolAttributeDescription

---@return UnityEngine.UIElements.UxmlBoolAttributeDescription
function UnityEngine.UIElements.UxmlBoolAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return boolean
function UnityEngine.UIElements.UxmlBoolAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value boolean
---@return boolean, boolean
function UnityEngine.UIElements.UxmlBoolAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlChildElementDescription : System.Object
---@field elementName string
---@field elementNamespace string
UnityEngine.UIElements.UxmlChildElementDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlChildElementDescription UnityEngine.UIElements.UxmlChildElementDescription
CS.UnityEngine.UIElements.UxmlChildElementDescription = UnityEngine.UIElements.UxmlChildElementDescription

---@param t System.Type
---@return UnityEngine.UIElements.UxmlChildElementDescription
function UnityEngine.UIElements.UxmlChildElementDescription.New(t) end

---@class UnityEngine.UIElements.UxmlColorAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlColorAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlColorAttributeDescription UnityEngine.UIElements.UxmlColorAttributeDescription
CS.UnityEngine.UIElements.UxmlColorAttributeDescription = UnityEngine.UIElements.UxmlColorAttributeDescription

---@return UnityEngine.UIElements.UxmlColorAttributeDescription
function UnityEngine.UIElements.UxmlColorAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return UnityEngine.Color
function UnityEngine.UIElements.UxmlColorAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value UnityEngine.Color
---@return boolean, UnityEngine.Color
function UnityEngine.UIElements.UxmlColorAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlDoubleAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlDoubleAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlDoubleAttributeDescription UnityEngine.UIElements.UxmlDoubleAttributeDescription
CS.UnityEngine.UIElements.UxmlDoubleAttributeDescription = UnityEngine.UIElements.UxmlDoubleAttributeDescription

---@return UnityEngine.UIElements.UxmlDoubleAttributeDescription
function UnityEngine.UIElements.UxmlDoubleAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return number
function UnityEngine.UIElements.UxmlDoubleAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value number
---@return boolean, number
function UnityEngine.UIElements.UxmlDoubleAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlEnumAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription[T]
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlEnumAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlEnumAttributeDescription UnityEngine.UIElements.UxmlEnumAttributeDescription
CS.UnityEngine.UIElements.UxmlEnumAttributeDescription = UnityEngine.UIElements.UxmlEnumAttributeDescription

---@return UnityEngine.UIElements.UxmlEnumAttributeDescription
function UnityEngine.UIElements.UxmlEnumAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return T
function UnityEngine.UIElements.UxmlEnumAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value T
---@return boolean, T
function UnityEngine.UIElements.UxmlEnumAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlEnumeration : UnityEngine.UIElements.UxmlTypeRestriction
---@field values System.Collections.Generic.IEnumerable
UnityEngine.UIElements.UxmlEnumeration = {}
---@alias CS.UnityEngine.UIElements.UxmlEnumeration UnityEngine.UIElements.UxmlEnumeration
CS.UnityEngine.UIElements.UxmlEnumeration = UnityEngine.UIElements.UxmlEnumeration

---@return UnityEngine.UIElements.UxmlEnumeration
function UnityEngine.UIElements.UxmlEnumeration.New() end
---@param other UnityEngine.UIElements.UxmlTypeRestriction
---@return boolean
function UnityEngine.UIElements.UxmlEnumeration:Equals(other) end

---@class UnityEngine.UIElements.UxmlFactory : UnityEngine.UIElements.UxmlFactory[TCreatedType,UnityEngine.UIElements.VisualElement.UxmlTraits]
UnityEngine.UIElements.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.UxmlFactory UnityEngine.UIElements.UxmlFactory
CS.UnityEngine.UIElements.UxmlFactory = UnityEngine.UIElements.UxmlFactory

---@return UnityEngine.UIElements.UxmlFactory
function UnityEngine.UIElements.UxmlFactory.New() end

---@class UnityEngine.UIElements.UxmlFactory : UnityEngine.UIElements.BaseUxmlFactory[TCreatedType,TTraits]
UnityEngine.UIElements.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.UxmlFactory UnityEngine.UIElements.UxmlFactory
CS.UnityEngine.UIElements.UxmlFactory = UnityEngine.UIElements.UxmlFactory

---@return UnityEngine.UIElements.UxmlFactory
function UnityEngine.UIElements.UxmlFactory.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UxmlFactory:Create(bag, cc) end

---@class UnityEngine.UIElements.UxmlFloatAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlFloatAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlFloatAttributeDescription UnityEngine.UIElements.UxmlFloatAttributeDescription
CS.UnityEngine.UIElements.UxmlFloatAttributeDescription = UnityEngine.UIElements.UxmlFloatAttributeDescription

---@return UnityEngine.UIElements.UxmlFloatAttributeDescription
function UnityEngine.UIElements.UxmlFloatAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return number
function UnityEngine.UIElements.UxmlFloatAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value number
---@return boolean, number
function UnityEngine.UIElements.UxmlFloatAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlGenericAttributeNames : System.Object
UnityEngine.UIElements.UxmlGenericAttributeNames = {}
---@alias CS.UnityEngine.UIElements.UxmlGenericAttributeNames UnityEngine.UIElements.UxmlGenericAttributeNames
CS.UnityEngine.UIElements.UxmlGenericAttributeNames = UnityEngine.UIElements.UxmlGenericAttributeNames

---@return UnityEngine.UIElements.UxmlGenericAttributeNames
function UnityEngine.UIElements.UxmlGenericAttributeNames.New() end

---@class UnityEngine.UIElements.UxmlHash128AttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlHash128AttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlHash128AttributeDescription UnityEngine.UIElements.UxmlHash128AttributeDescription
CS.UnityEngine.UIElements.UxmlHash128AttributeDescription = UnityEngine.UIElements.UxmlHash128AttributeDescription

---@return UnityEngine.UIElements.UxmlHash128AttributeDescription
function UnityEngine.UIElements.UxmlHash128AttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return UnityEngine.Hash128
function UnityEngine.UIElements.UxmlHash128AttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value UnityEngine.Hash128
---@return boolean, UnityEngine.Hash128
function UnityEngine.UIElements.UxmlHash128AttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlIntAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlIntAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlIntAttributeDescription UnityEngine.UIElements.UxmlIntAttributeDescription
CS.UnityEngine.UIElements.UxmlIntAttributeDescription = UnityEngine.UIElements.UxmlIntAttributeDescription

---@return UnityEngine.UIElements.UxmlIntAttributeDescription
function UnityEngine.UIElements.UxmlIntAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return number
function UnityEngine.UIElements.UxmlIntAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value number
---@return boolean, number
function UnityEngine.UIElements.UxmlIntAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlLongAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlLongAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlLongAttributeDescription UnityEngine.UIElements.UxmlLongAttributeDescription
CS.UnityEngine.UIElements.UxmlLongAttributeDescription = UnityEngine.UIElements.UxmlLongAttributeDescription

---@return UnityEngine.UIElements.UxmlLongAttributeDescription
function UnityEngine.UIElements.UxmlLongAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return number
function UnityEngine.UIElements.UxmlLongAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value number
---@return boolean, number
function UnityEngine.UIElements.UxmlLongAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlObjectAsset : UnityEngine.UIElements.UxmlAsset
UnityEngine.UIElements.UxmlObjectAsset = {}
---@alias CS.UnityEngine.UIElements.UxmlObjectAsset UnityEngine.UIElements.UxmlObjectAsset
CS.UnityEngine.UIElements.UxmlObjectAsset = UnityEngine.UIElements.UxmlObjectAsset

---@param fullTypeName string
---@return UnityEngine.UIElements.UxmlObjectAsset
function UnityEngine.UIElements.UxmlObjectAsset.New(fullTypeName) end

---@class UnityEngine.UIElements.UxmlObjectAttributeDescription : System.Object
---@field defaultValue T
UnityEngine.UIElements.UxmlObjectAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlObjectAttributeDescription UnityEngine.UIElements.UxmlObjectAttributeDescription
CS.UnityEngine.UIElements.UxmlObjectAttributeDescription = UnityEngine.UIElements.UxmlObjectAttributeDescription

---@return UnityEngine.UIElements.UxmlObjectAttributeDescription
function UnityEngine.UIElements.UxmlObjectAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return T
function UnityEngine.UIElements.UxmlObjectAttributeDescription:GetValueFromBag(bag, cc) end

---@class UnityEngine.UIElements.UxmlObjectFactory : UnityEngine.UIElements.BaseUxmlFactory[TCreatedType,TTraits]
UnityEngine.UIElements.UxmlObjectFactory = {}
---@alias CS.UnityEngine.UIElements.UxmlObjectFactory UnityEngine.UIElements.UxmlObjectFactory
CS.UnityEngine.UIElements.UxmlObjectFactory = UnityEngine.UIElements.UxmlObjectFactory

---@return UnityEngine.UIElements.UxmlObjectFactory
function UnityEngine.UIElements.UxmlObjectFactory.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return TCreatedType
function UnityEngine.UIElements.UxmlObjectFactory:CreateObject(bag, cc) end

---@class UnityEngine.UIElements.UxmlObjectFactoryRegistry : System.Object
UnityEngine.UIElements.UxmlObjectFactoryRegistry = {}
---@alias CS.UnityEngine.UIElements.UxmlObjectFactoryRegistry UnityEngine.UIElements.UxmlObjectFactoryRegistry
CS.UnityEngine.UIElements.UxmlObjectFactoryRegistry = UnityEngine.UIElements.UxmlObjectFactoryRegistry

---@return UnityEngine.UIElements.UxmlObjectFactoryRegistry
function UnityEngine.UIElements.UxmlObjectFactoryRegistry.New() end

---@class UnityEngine.UIElements.UxmlObjectListAttributeDescription : UnityEngine.UIElements.UxmlObjectAttributeDescription[System.Collections.Generic.List[T]]
UnityEngine.UIElements.UxmlObjectListAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlObjectListAttributeDescription UnityEngine.UIElements.UxmlObjectListAttributeDescription
CS.UnityEngine.UIElements.UxmlObjectListAttributeDescription = UnityEngine.UIElements.UxmlObjectListAttributeDescription

---@return UnityEngine.UIElements.UxmlObjectListAttributeDescription
function UnityEngine.UIElements.UxmlObjectListAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return System.Collections.Generic.List[T]
function UnityEngine.UIElements.UxmlObjectListAttributeDescription:GetValueFromBag(bag, cc) end

---@class UnityEngine.UIElements.UxmlObjectTraits : UnityEngine.UIElements.BaseUxmlTraits
UnityEngine.UIElements.UxmlObjectTraits = {}
---@alias CS.UnityEngine.UIElements.UxmlObjectTraits UnityEngine.UIElements.UxmlObjectTraits
CS.UnityEngine.UIElements.UxmlObjectTraits = UnityEngine.UIElements.UxmlObjectTraits

---@param ref_obj T
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return T
function UnityEngine.UIElements.UxmlObjectTraits:Init(ref_obj, bag, cc) end

---@class UnityEngine.UIElements.UxmlRootElementFactory : UnityEngine.UIElements.UxmlFactory
---@field uxmlName string
---@field uxmlQualifiedName string
---@field substituteForTypeName string
---@field substituteForTypeNamespace string
---@field substituteForTypeQualifiedName string
UnityEngine.UIElements.UxmlRootElementFactory = {}
---@alias CS.UnityEngine.UIElements.UxmlRootElementFactory UnityEngine.UIElements.UxmlRootElementFactory
CS.UnityEngine.UIElements.UxmlRootElementFactory = UnityEngine.UIElements.UxmlRootElementFactory

---@return UnityEngine.UIElements.UxmlRootElementFactory
function UnityEngine.UIElements.UxmlRootElementFactory.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UxmlRootElementFactory:Create(bag, cc) end

---@class UnityEngine.UIElements.UxmlRootElementTraits : UnityEngine.UIElements.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.UxmlRootElementTraits = {}
---@alias CS.UnityEngine.UIElements.UxmlRootElementTraits UnityEngine.UIElements.UxmlRootElementTraits
CS.UnityEngine.UIElements.UxmlRootElementTraits = UnityEngine.UIElements.UxmlRootElementTraits

---@return UnityEngine.UIElements.UxmlRootElementTraits
function UnityEngine.UIElements.UxmlRootElementTraits.New() end

---@class UnityEngine.UIElements.UxmlStringAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlStringAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlStringAttributeDescription UnityEngine.UIElements.UxmlStringAttributeDescription
CS.UnityEngine.UIElements.UxmlStringAttributeDescription = UnityEngine.UIElements.UxmlStringAttributeDescription

---@return UnityEngine.UIElements.UxmlStringAttributeDescription
function UnityEngine.UIElements.UxmlStringAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return string
function UnityEngine.UIElements.UxmlStringAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value string
---@return boolean, string
function UnityEngine.UIElements.UxmlStringAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlStyleFactory : UnityEngine.UIElements.UxmlFactory
---@field uxmlName string
---@field uxmlQualifiedName string
---@field substituteForTypeName string
---@field substituteForTypeNamespace string
---@field substituteForTypeQualifiedName string
UnityEngine.UIElements.UxmlStyleFactory = {}
---@alias CS.UnityEngine.UIElements.UxmlStyleFactory UnityEngine.UIElements.UxmlStyleFactory
CS.UnityEngine.UIElements.UxmlStyleFactory = UnityEngine.UIElements.UxmlStyleFactory

---@return UnityEngine.UIElements.UxmlStyleFactory
function UnityEngine.UIElements.UxmlStyleFactory.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UxmlStyleFactory:Create(bag, cc) end

---@class UnityEngine.UIElements.UxmlStyleTraits : UnityEngine.UIElements.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.UxmlStyleTraits = {}
---@alias CS.UnityEngine.UIElements.UxmlStyleTraits UnityEngine.UIElements.UxmlStyleTraits
CS.UnityEngine.UIElements.UxmlStyleTraits = UnityEngine.UIElements.UxmlStyleTraits

---@return UnityEngine.UIElements.UxmlStyleTraits
function UnityEngine.UIElements.UxmlStyleTraits.New() end

---@class UnityEngine.UIElements.UxmlTemplateFactory : UnityEngine.UIElements.UxmlFactory
---@field uxmlName string
---@field uxmlQualifiedName string
---@field substituteForTypeName string
---@field substituteForTypeNamespace string
---@field substituteForTypeQualifiedName string
UnityEngine.UIElements.UxmlTemplateFactory = {}
---@alias CS.UnityEngine.UIElements.UxmlTemplateFactory UnityEngine.UIElements.UxmlTemplateFactory
CS.UnityEngine.UIElements.UxmlTemplateFactory = UnityEngine.UIElements.UxmlTemplateFactory

---@return UnityEngine.UIElements.UxmlTemplateFactory
function UnityEngine.UIElements.UxmlTemplateFactory.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.UxmlTemplateFactory:Create(bag, cc) end

---@class UnityEngine.UIElements.UxmlTemplateTraits : UnityEngine.UIElements.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.UxmlTemplateTraits = {}
---@alias CS.UnityEngine.UIElements.UxmlTemplateTraits UnityEngine.UIElements.UxmlTemplateTraits
CS.UnityEngine.UIElements.UxmlTemplateTraits = UnityEngine.UIElements.UxmlTemplateTraits

---@return UnityEngine.UIElements.UxmlTemplateTraits
function UnityEngine.UIElements.UxmlTemplateTraits.New() end

---@class UnityEngine.UIElements.UxmlTraits : UnityEngine.UIElements.BaseUxmlTraits
UnityEngine.UIElements.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.UxmlTraits UnityEngine.UIElements.UxmlTraits
CS.UnityEngine.UIElements.UxmlTraits = UnityEngine.UIElements.UxmlTraits

---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.UxmlTypeAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlTypeAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlTypeAttributeDescription UnityEngine.UIElements.UxmlTypeAttributeDescription
CS.UnityEngine.UIElements.UxmlTypeAttributeDescription = UnityEngine.UIElements.UxmlTypeAttributeDescription

---@return UnityEngine.UIElements.UxmlTypeAttributeDescription
function UnityEngine.UIElements.UxmlTypeAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return System.Type
function UnityEngine.UIElements.UxmlTypeAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value System.Type
---@return boolean, System.Type
function UnityEngine.UIElements.UxmlTypeAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlTypeRestriction : System.Object
UnityEngine.UIElements.UxmlTypeRestriction = {}
---@alias CS.UnityEngine.UIElements.UxmlTypeRestriction UnityEngine.UIElements.UxmlTypeRestriction
CS.UnityEngine.UIElements.UxmlTypeRestriction = UnityEngine.UIElements.UxmlTypeRestriction

---@param other UnityEngine.UIElements.UxmlTypeRestriction
---@return boolean
function UnityEngine.UIElements.UxmlTypeRestriction:Equals(other) end

---@class UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription
CS.UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription = UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription

---@return UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription
function UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return number
function UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value number
---@return boolean, number
function UnityEngine.UIElements.UxmlUnsignedIntAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription : UnityEngine.UIElements.TypedUxmlAttributeDescription
---@field defaultValueAsString string
UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription = {}
---@alias CS.UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription
CS.UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription = UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription

---@return UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription
function UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription.New() end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@return number
function UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription:GetValueFromBag(bag, cc) end
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
---@param ref_value number
---@return boolean, number
function UnityEngine.UIElements.UxmlUnsignedLongAttributeDescription:TryGetValueFromBag(bag, cc, ref_value) end

---@class UnityEngine.UIElements.UxmlValueBounds : UnityEngine.UIElements.UxmlTypeRestriction
---@field min string
---@field max string
---@field excludeMin boolean
---@field excludeMax boolean
UnityEngine.UIElements.UxmlValueBounds = {}
---@alias CS.UnityEngine.UIElements.UxmlValueBounds UnityEngine.UIElements.UxmlValueBounds
CS.UnityEngine.UIElements.UxmlValueBounds = UnityEngine.UIElements.UxmlValueBounds

---@return UnityEngine.UIElements.UxmlValueBounds
function UnityEngine.UIElements.UxmlValueBounds.New() end
---@param other UnityEngine.UIElements.UxmlTypeRestriction
---@return boolean
function UnityEngine.UIElements.UxmlValueBounds:Equals(other) end

---@class UnityEngine.UIElements.UxmlValueMatches : UnityEngine.UIElements.UxmlTypeRestriction
---@field regex string
UnityEngine.UIElements.UxmlValueMatches = {}
---@alias CS.UnityEngine.UIElements.UxmlValueMatches UnityEngine.UIElements.UxmlValueMatches
CS.UnityEngine.UIElements.UxmlValueMatches = UnityEngine.UIElements.UxmlValueMatches

---@return UnityEngine.UIElements.UxmlValueMatches
function UnityEngine.UIElements.UxmlValueMatches.New() end
---@param other UnityEngine.UIElements.UxmlTypeRestriction
---@return boolean
function UnityEngine.UIElements.UxmlValueMatches:Equals(other) end

---@class UnityEngine.UIElements.ValidateCommandEvent : UnityEngine.UIElements.CommandEventBase
UnityEngine.UIElements.ValidateCommandEvent = {}
---@alias CS.UnityEngine.UIElements.ValidateCommandEvent UnityEngine.UIElements.ValidateCommandEvent
CS.UnityEngine.UIElements.ValidateCommandEvent = UnityEngine.UIElements.ValidateCommandEvent

---@return UnityEngine.UIElements.ValidateCommandEvent
function UnityEngine.UIElements.ValidateCommandEvent.New() end

---@class UnityEngine.UIElements.Vector2Field : UnityEngine.UIElements.BaseCompositeField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.Vector2Field = {}
---@alias CS.UnityEngine.UIElements.Vector2Field UnityEngine.UIElements.Vector2Field
CS.UnityEngine.UIElements.Vector2Field = UnityEngine.UIElements.Vector2Field

---@overload fun() : UnityEngine.UIElements.Vector2Field
---@param label string
---@return UnityEngine.UIElements.Vector2Field
function UnityEngine.UIElements.Vector2Field.New(label) end

---@class UnityEngine.UIElements.Vector2Field.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.Vector2Field.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.Vector2Field.UxmlFactory UnityEngine.UIElements.Vector2Field.UxmlFactory
CS.UnityEngine.UIElements.Vector2Field.UxmlFactory = UnityEngine.UIElements.Vector2Field.UxmlFactory

---@return UnityEngine.UIElements.Vector2Field.UxmlFactory
function UnityEngine.UIElements.Vector2Field.UxmlFactory.New() end

---@class UnityEngine.UIElements.Vector2Field.UxmlTraits : UnityEngine.UIElements.BaseField.UxmlTraits
UnityEngine.UIElements.Vector2Field.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.Vector2Field.UxmlTraits UnityEngine.UIElements.Vector2Field.UxmlTraits
CS.UnityEngine.UIElements.Vector2Field.UxmlTraits = UnityEngine.UIElements.Vector2Field.UxmlTraits

---@return UnityEngine.UIElements.Vector2Field.UxmlTraits
function UnityEngine.UIElements.Vector2Field.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.Vector2Field.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.Vector2IntField : UnityEngine.UIElements.BaseCompositeField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.Vector2IntField = {}
---@alias CS.UnityEngine.UIElements.Vector2IntField UnityEngine.UIElements.Vector2IntField
CS.UnityEngine.UIElements.Vector2IntField = UnityEngine.UIElements.Vector2IntField

---@overload fun() : UnityEngine.UIElements.Vector2IntField
---@param label string
---@return UnityEngine.UIElements.Vector2IntField
function UnityEngine.UIElements.Vector2IntField.New(label) end

---@class UnityEngine.UIElements.Vector2IntField.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.Vector2IntField.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.Vector2IntField.UxmlFactory UnityEngine.UIElements.Vector2IntField.UxmlFactory
CS.UnityEngine.UIElements.Vector2IntField.UxmlFactory = UnityEngine.UIElements.Vector2IntField.UxmlFactory

---@return UnityEngine.UIElements.Vector2IntField.UxmlFactory
function UnityEngine.UIElements.Vector2IntField.UxmlFactory.New() end

---@class UnityEngine.UIElements.Vector2IntField.UxmlTraits : UnityEngine.UIElements.BaseField.UxmlTraits
UnityEngine.UIElements.Vector2IntField.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.Vector2IntField.UxmlTraits UnityEngine.UIElements.Vector2IntField.UxmlTraits
CS.UnityEngine.UIElements.Vector2IntField.UxmlTraits = UnityEngine.UIElements.Vector2IntField.UxmlTraits

---@return UnityEngine.UIElements.Vector2IntField.UxmlTraits
function UnityEngine.UIElements.Vector2IntField.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.Vector2IntField.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.Vector3Field : UnityEngine.UIElements.BaseCompositeField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.Vector3Field = {}
---@alias CS.UnityEngine.UIElements.Vector3Field UnityEngine.UIElements.Vector3Field
CS.UnityEngine.UIElements.Vector3Field = UnityEngine.UIElements.Vector3Field

---@overload fun() : UnityEngine.UIElements.Vector3Field
---@param label string
---@return UnityEngine.UIElements.Vector3Field
function UnityEngine.UIElements.Vector3Field.New(label) end

---@class UnityEngine.UIElements.Vector3Field.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.Vector3Field.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.Vector3Field.UxmlFactory UnityEngine.UIElements.Vector3Field.UxmlFactory
CS.UnityEngine.UIElements.Vector3Field.UxmlFactory = UnityEngine.UIElements.Vector3Field.UxmlFactory

---@return UnityEngine.UIElements.Vector3Field.UxmlFactory
function UnityEngine.UIElements.Vector3Field.UxmlFactory.New() end

---@class UnityEngine.UIElements.Vector3Field.UxmlTraits : UnityEngine.UIElements.BaseField.UxmlTraits
UnityEngine.UIElements.Vector3Field.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.Vector3Field.UxmlTraits UnityEngine.UIElements.Vector3Field.UxmlTraits
CS.UnityEngine.UIElements.Vector3Field.UxmlTraits = UnityEngine.UIElements.Vector3Field.UxmlTraits

---@return UnityEngine.UIElements.Vector3Field.UxmlTraits
function UnityEngine.UIElements.Vector3Field.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.Vector3Field.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.Vector3IntField : UnityEngine.UIElements.BaseCompositeField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.Vector3IntField = {}
---@alias CS.UnityEngine.UIElements.Vector3IntField UnityEngine.UIElements.Vector3IntField
CS.UnityEngine.UIElements.Vector3IntField = UnityEngine.UIElements.Vector3IntField

---@overload fun() : UnityEngine.UIElements.Vector3IntField
---@param label string
---@return UnityEngine.UIElements.Vector3IntField
function UnityEngine.UIElements.Vector3IntField.New(label) end

---@class UnityEngine.UIElements.Vector3IntField.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.Vector3IntField.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.Vector3IntField.UxmlFactory UnityEngine.UIElements.Vector3IntField.UxmlFactory
CS.UnityEngine.UIElements.Vector3IntField.UxmlFactory = UnityEngine.UIElements.Vector3IntField.UxmlFactory

---@return UnityEngine.UIElements.Vector3IntField.UxmlFactory
function UnityEngine.UIElements.Vector3IntField.UxmlFactory.New() end

---@class UnityEngine.UIElements.Vector3IntField.UxmlTraits : UnityEngine.UIElements.BaseField.UxmlTraits
UnityEngine.UIElements.Vector3IntField.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.Vector3IntField.UxmlTraits UnityEngine.UIElements.Vector3IntField.UxmlTraits
CS.UnityEngine.UIElements.Vector3IntField.UxmlTraits = UnityEngine.UIElements.Vector3IntField.UxmlTraits

---@return UnityEngine.UIElements.Vector3IntField.UxmlTraits
function UnityEngine.UIElements.Vector3IntField.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.Vector3IntField.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.Vector4Field : UnityEngine.UIElements.BaseCompositeField
---@field ussClassName string
---@field labelUssClassName string
---@field inputUssClassName string
UnityEngine.UIElements.Vector4Field = {}
---@alias CS.UnityEngine.UIElements.Vector4Field UnityEngine.UIElements.Vector4Field
CS.UnityEngine.UIElements.Vector4Field = UnityEngine.UIElements.Vector4Field

---@overload fun() : UnityEngine.UIElements.Vector4Field
---@param label string
---@return UnityEngine.UIElements.Vector4Field
function UnityEngine.UIElements.Vector4Field.New(label) end

---@class UnityEngine.UIElements.Vector4Field.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.Vector4Field.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.Vector4Field.UxmlFactory UnityEngine.UIElements.Vector4Field.UxmlFactory
CS.UnityEngine.UIElements.Vector4Field.UxmlFactory = UnityEngine.UIElements.Vector4Field.UxmlFactory

---@return UnityEngine.UIElements.Vector4Field.UxmlFactory
function UnityEngine.UIElements.Vector4Field.UxmlFactory.New() end

---@class UnityEngine.UIElements.Vector4Field.UxmlTraits : UnityEngine.UIElements.BaseField.UxmlTraits
UnityEngine.UIElements.Vector4Field.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.Vector4Field.UxmlTraits UnityEngine.UIElements.Vector4Field.UxmlTraits
CS.UnityEngine.UIElements.Vector4Field.UxmlTraits = UnityEngine.UIElements.Vector4Field.UxmlTraits

---@return UnityEngine.UIElements.Vector4Field.UxmlTraits
function UnityEngine.UIElements.Vector4Field.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.Vector4Field.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.VectorImage : UnityEngine.ScriptableObject
---@field width number
---@field height number
UnityEngine.UIElements.VectorImage = {}
---@alias CS.UnityEngine.UIElements.VectorImage UnityEngine.UIElements.VectorImage
CS.UnityEngine.UIElements.VectorImage = UnityEngine.UIElements.VectorImage

---@return UnityEngine.UIElements.VectorImage
function UnityEngine.UIElements.VectorImage.New() end

---@class UnityEngine.UIElements.VectorImageVertex : System.ValueType
---@field position UnityEngine.Vector3
---@field tint UnityEngine.Color32
---@field uv UnityEngine.Vector2
---@field settingIndex number
---@field flags UnityEngine.Color32
---@field circle UnityEngine.Vector4
UnityEngine.UIElements.VectorImageVertex = {}
---@alias CS.UnityEngine.UIElements.VectorImageVertex UnityEngine.UIElements.VectorImageVertex
CS.UnityEngine.UIElements.VectorImageVertex = UnityEngine.UIElements.VectorImageVertex


---@class UnityEngine.UIElements.VersionChangeType
---@field Bindings UnityEngine.UIElements.VersionChangeType
---@field ViewData UnityEngine.UIElements.VersionChangeType
---@field Hierarchy UnityEngine.UIElements.VersionChangeType
---@field Layout UnityEngine.UIElements.VersionChangeType
---@field StyleSheet UnityEngine.UIElements.VersionChangeType
---@field Styles UnityEngine.UIElements.VersionChangeType
---@field Overflow UnityEngine.UIElements.VersionChangeType
---@field BorderRadius UnityEngine.UIElements.VersionChangeType
---@field BorderWidth UnityEngine.UIElements.VersionChangeType
---@field Transform UnityEngine.UIElements.VersionChangeType
---@field Size UnityEngine.UIElements.VersionChangeType
---@field Repaint UnityEngine.UIElements.VersionChangeType
---@field Opacity UnityEngine.UIElements.VersionChangeType
---@field Color UnityEngine.UIElements.VersionChangeType
---@field RenderHints UnityEngine.UIElements.VersionChangeType
---@field TransitionProperty UnityEngine.UIElements.VersionChangeType
---@field EventCallbackCategories UnityEngine.UIElements.VersionChangeType
---@field Picking UnityEngine.UIElements.VersionChangeType
UnityEngine.UIElements.VersionChangeType = {}
---@alias CS.UnityEngine.UIElements.VersionChangeType UnityEngine.UIElements.VersionChangeType
CS.UnityEngine.UIElements.VersionChangeType = UnityEngine.UIElements.VersionChangeType


---@class UnityEngine.UIElements.Vertex : System.ValueType
---@field nearZ number
---@field position UnityEngine.Vector3
---@field tint UnityEngine.Color32
---@field uv UnityEngine.Vector2
UnityEngine.UIElements.Vertex = {}
---@alias CS.UnityEngine.UIElements.Vertex UnityEngine.UIElements.Vertex
CS.UnityEngine.UIElements.Vertex = UnityEngine.UIElements.Vertex


---@class UnityEngine.UIElements.VerticalVirtualizationController : UnityEngine.UIElements.CollectionVirtualizationController
---@field activeItems System.Collections.Generic.IEnumerable
---@field visibleItemCount number
---@field firstVisibleIndex number
UnityEngine.UIElements.VerticalVirtualizationController = {}
---@alias CS.UnityEngine.UIElements.VerticalVirtualizationController UnityEngine.UIElements.VerticalVirtualizationController
CS.UnityEngine.UIElements.VerticalVirtualizationController = UnityEngine.UIElements.VerticalVirtualizationController

---@param rebuild boolean
function UnityEngine.UIElements.VerticalVirtualizationController:Refresh(rebuild) end
---@param leafTarget UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VerticalVirtualizationController:OnFocus(leafTarget) end
---@param willFocus UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VerticalVirtualizationController:OnBlur(willFocus) end
function UnityEngine.UIElements.VerticalVirtualizationController:UpdateBackground() end

---@class UnityEngine.UIElements.Visibility
---@field Visible UnityEngine.UIElements.Visibility
---@field Hidden UnityEngine.UIElements.Visibility
UnityEngine.UIElements.Visibility = {}
---@alias CS.UnityEngine.UIElements.Visibility UnityEngine.UIElements.Visibility
CS.UnityEngine.UIElements.Visibility = UnityEngine.UIElements.Visibility


---@class UnityEngine.UIElements.VisualData : System.ValueType
---@field backgroundColor UnityEngine.Color
---@field backgroundImage UnityEngine.UIElements.Background
---@field backgroundPositionX UnityEngine.UIElements.BackgroundPosition
---@field backgroundPositionY UnityEngine.UIElements.BackgroundPosition
---@field backgroundRepeat UnityEngine.UIElements.BackgroundRepeat
---@field backgroundSize UnityEngine.UIElements.BackgroundSize
---@field borderBottomColor UnityEngine.Color
---@field borderBottomLeftRadius UnityEngine.UIElements.Length
---@field borderBottomRightRadius UnityEngine.UIElements.Length
---@field borderLeftColor UnityEngine.Color
---@field borderRightColor UnityEngine.Color
---@field borderTopColor UnityEngine.Color
---@field borderTopLeftRadius UnityEngine.UIElements.Length
---@field borderTopRightRadius UnityEngine.UIElements.Length
---@field opacity number
---@field overflow UnityEngine.UIElements.OverflowInternal
UnityEngine.UIElements.VisualData = {}
---@alias CS.UnityEngine.UIElements.VisualData UnityEngine.UIElements.VisualData
CS.UnityEngine.UIElements.VisualData = UnityEngine.UIElements.VisualData

---@return UnityEngine.UIElements.VisualData
function UnityEngine.UIElements.VisualData:Copy() end
---@param ref_other UnityEngine.UIElements.VisualData
---@return UnityEngine.UIElements.VisualData
function UnityEngine.UIElements.VisualData:CopyFrom(ref_other) end
---@overload fun(self: UnityEngine.UIElements.VisualData, other: UnityEngine.UIElements.VisualData) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.VisualData:Equals(obj) end
---@return number
function UnityEngine.UIElements.VisualData:GetHashCode() end

---@class UnityEngine.UIElements.VisualElement : UnityEngine.UIElements.Focusable
---@field disabledUssClassName string
---@field resolvedStyle UnityEngine.UIElements.IResolvedStyle
---@field viewDataKey string
---@field userData System.Object
---@field canGrabFocus boolean
---@field focusController UnityEngine.UIElements.FocusController
---@field usageHints UnityEngine.UIElements.UsageHints
---@field transform UnityEngine.UIElements.ITransform
---@field layout UnityEngine.Rect
---@field contentRect UnityEngine.Rect
---@field worldBound UnityEngine.Rect
---@field localBound UnityEngine.Rect
---@field worldTransform UnityEngine.Matrix4x4
---@field pickingMode UnityEngine.UIElements.PickingMode
---@field name string
---@field enabledInHierarchy boolean
---@field enabledSelf boolean
---@field languageDirection UnityEngine.UIElements.LanguageDirection
---@field visible boolean
---@field generateVisualContent System.Action | function
---@field experimental UnityEngine.UIElements.IExperimentalFeatures
---@field hierarchy UnityEngine.UIElements.VisualElement.Hierarchy
---@field parent UnityEngine.UIElements.VisualElement
---@field panel UnityEngine.UIElements.IPanel
---@field contentContainer UnityEngine.UIElements.VisualElement
---@field visualTreeAssetSource UnityEngine.UIElements.VisualTreeAsset
---@field Item UnityEngine.UIElements.VisualElement
---@field childCount number
---@field schedule UnityEngine.UIElements.IVisualElementScheduler
---@field style UnityEngine.UIElements.IStyle
---@field customStyle UnityEngine.UIElements.ICustomStyle
---@field styleSheets UnityEngine.UIElements.VisualElementStyleSheetSet
---@field tooltip string
UnityEngine.UIElements.VisualElement = {}
---@alias CS.UnityEngine.UIElements.VisualElement UnityEngine.UIElements.VisualElement
CS.UnityEngine.UIElements.VisualElement = UnityEngine.UIElements.VisualElement

---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement.New() end
function UnityEngine.UIElements.VisualElement:Focus() end
---@param e UnityEngine.UIElements.EventBase
function UnityEngine.UIElements.VisualElement:SendEvent(e) end
---@param value boolean
function UnityEngine.UIElements.VisualElement:SetEnabled(value) end
function UnityEngine.UIElements.VisualElement:MarkDirtyRepaint() end
---@param localPoint UnityEngine.Vector2
---@return boolean
function UnityEngine.UIElements.VisualElement:ContainsPoint(localPoint) end
---@param rectangle UnityEngine.Rect
---@return boolean
function UnityEngine.UIElements.VisualElement:Overlaps(rectangle) end
---@return string
function UnityEngine.UIElements.VisualElement:ToString() end
---@return System.Collections.Generic.IEnumerable
function UnityEngine.UIElements.VisualElement:GetClasses() end
function UnityEngine.UIElements.VisualElement:ClearClassList() end
---@param className string
function UnityEngine.UIElements.VisualElement:AddToClassList(className) end
---@param className string
function UnityEngine.UIElements.VisualElement:RemoveFromClassList(className) end
---@param className string
function UnityEngine.UIElements.VisualElement:ToggleInClassList(className) end
---@param className string
---@param enable boolean
function UnityEngine.UIElements.VisualElement:EnableInClassList(className, enable) end
---@param cls string
---@return boolean
function UnityEngine.UIElements.VisualElement:ClassListContains(cls) end
---@return System.Object
function UnityEngine.UIElements.VisualElement:FindAncestorUserData() end
---@param child UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement:Add(child) end
---@param index number
---@param element UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement:Insert(index, element) end
---@param element UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement:Remove(element) end
---@param index number
function UnityEngine.UIElements.VisualElement:RemoveAt(index) end
function UnityEngine.UIElements.VisualElement:Clear() end
---@param index number
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement:ElementAt(index) end
---@param element UnityEngine.UIElements.VisualElement
---@return number
function UnityEngine.UIElements.VisualElement:IndexOf(element) end
---@return System.Collections.Generic.IEnumerable
function UnityEngine.UIElements.VisualElement:Children() end
---@param comp System.Comparison
function UnityEngine.UIElements.VisualElement:Sort(comp) end
function UnityEngine.UIElements.VisualElement:BringToFront() end
function UnityEngine.UIElements.VisualElement:SendToBack() end
---@param sibling UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement:PlaceBehind(sibling) end
---@param sibling UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement:PlaceInFront(sibling) end
function UnityEngine.UIElements.VisualElement:RemoveFromHierarchy() end
---@param child UnityEngine.UIElements.VisualElement
---@return boolean
function UnityEngine.UIElements.VisualElement:Contains(child) end
---@param other UnityEngine.UIElements.VisualElement
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement:FindCommonAncestor(other) end
---@return boolean
function UnityEngine.UIElements.VisualElement:IsChecked() end
---@return boolean
function UnityEngine.UIElements.VisualElement:IsHovered() end
---@param isChecked boolean
function UnityEngine.UIElements.VisualElement:SetChecked(isChecked) end
---@param name string
---@param classes string[]
---@return T
function UnityEngine.UIElements.VisualElement:Q(name, classes) end
---@param name string
---@param classes string[]
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement:Q(name, classes) end
---@param name string
---@param className string
---@return T
function UnityEngine.UIElements.VisualElement:Q(name, className) end
---@param name string
---@param className string
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement:Q(name, className) end
---@param name string
---@param classes string[]
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.VisualElement:Query(name, classes) end
---@param name string
---@param className string
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.VisualElement:Query(name, className) end
---@param name string
---@param classes string[]
---@return UnityEngine.UIElements.UQueryBuilder[T]
function UnityEngine.UIElements.VisualElement:Query(name, classes) end
---@param name string
---@param className string
---@return UnityEngine.UIElements.UQueryBuilder[T]
function UnityEngine.UIElements.VisualElement:Query(name, className) end
---@return UnityEngine.UIElements.UQueryBuilder
function UnityEngine.UIElements.VisualElement:Query() end
---@param withHashCode boolean
---@return string
function UnityEngine.UIElements.VisualElement:GetDisplayName(withHashCode) end
function UnityEngine.UIElements.VisualElement:StretchToParentSize() end
function UnityEngine.UIElements.VisualElement:StretchToParentWidth() end
---@param manipulator UnityEngine.UIElements.IManipulator
function UnityEngine.UIElements.VisualElement:AddManipulator(manipulator) end
---@param manipulator UnityEngine.UIElements.IManipulator
function UnityEngine.UIElements.VisualElement:RemoveManipulator(manipulator) end
---@param p UnityEngine.Vector2
---@return UnityEngine.Vector2
function UnityEngine.UIElements.VisualElement:WorldToLocal(p) end
---@param p UnityEngine.Vector2
---@return UnityEngine.Vector2
function UnityEngine.UIElements.VisualElement:LocalToWorld(p) end
---@param r UnityEngine.Rect
---@return UnityEngine.Rect
function UnityEngine.UIElements.VisualElement:WorldToLocal(r) end
---@param r UnityEngine.Rect
---@return UnityEngine.Rect
function UnityEngine.UIElements.VisualElement:LocalToWorld(r) end
---@param dest UnityEngine.UIElements.VisualElement
---@param point UnityEngine.Vector2
---@return UnityEngine.Vector2
function UnityEngine.UIElements.VisualElement:ChangeCoordinatesTo(dest, point) end
---@param dest UnityEngine.UIElements.VisualElement
---@param rect UnityEngine.Rect
---@return UnityEngine.Rect
function UnityEngine.UIElements.VisualElement:ChangeCoordinatesTo(dest, rect) end

---@class UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem : UnityEngine.UIElements.ScheduledItem
---@field isScheduled boolean
---@field element UnityEngine.UIElements.VisualElement
---@field isActive boolean
UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem = {}
---@alias CS.UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem
CS.UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem = UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem

---@param delayMs number
---@return UnityEngine.UIElements.IVisualElementScheduledItem
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:StartingIn(delayMs) end
---@param stopCondition System.Func
---@return UnityEngine.UIElements.IVisualElementScheduledItem
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:Until(stopCondition) end
---@param durationMs number
---@return UnityEngine.UIElements.IVisualElementScheduledItem
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:ForDuration(durationMs) end
---@param intervalMs number
---@return UnityEngine.UIElements.IVisualElementScheduledItem
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:Every(intervalMs) end
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:Resume() end
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:Pause() end
---@param delayMs number
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:ExecuteLater(delayMs) end
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:OnPanelActivate() end
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:OnPanelDeactivate() end
---@return boolean
function UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem:CanBeActivated() end

---@class UnityEngine.UIElements.VisualElement.CustomStyleAccess : System.Object
UnityEngine.UIElements.VisualElement.CustomStyleAccess = {}
---@alias CS.UnityEngine.UIElements.VisualElement.CustomStyleAccess UnityEngine.UIElements.VisualElement.CustomStyleAccess
CS.UnityEngine.UIElements.VisualElement.CustomStyleAccess = UnityEngine.UIElements.VisualElement.CustomStyleAccess

---@return UnityEngine.UIElements.VisualElement.CustomStyleAccess
function UnityEngine.UIElements.VisualElement.CustomStyleAccess.New() end
---@param customProperties System.Collections.Generic.Dictionary
---@param dpiScaling number
function UnityEngine.UIElements.VisualElement.CustomStyleAccess:SetContext(customProperties, dpiScaling) end
---@overload fun(self: UnityEngine.UIElements.VisualElement.CustomStyleAccess, property: UnityEngine.UIElements.CustomStyleProperty, out_value: number) : boolean, number
---@overload fun(self: UnityEngine.UIElements.VisualElement.CustomStyleAccess, property: UnityEngine.UIElements.CustomStyleProperty, out_value: number) : boolean, number
---@overload fun(self: UnityEngine.UIElements.VisualElement.CustomStyleAccess, property: UnityEngine.UIElements.CustomStyleProperty, out_value: boolean) : boolean, boolean
---@overload fun(self: UnityEngine.UIElements.VisualElement.CustomStyleAccess, property: UnityEngine.UIElements.CustomStyleProperty, out_value: UnityEngine.Color) : boolean, UnityEngine.Color
---@overload fun(self: UnityEngine.UIElements.VisualElement.CustomStyleAccess, property: UnityEngine.UIElements.CustomStyleProperty, out_value: UnityEngine.Texture2D) : boolean, UnityEngine.Texture2D
---@overload fun(self: UnityEngine.UIElements.VisualElement.CustomStyleAccess, property: UnityEngine.UIElements.CustomStyleProperty, out_value: UnityEngine.Sprite) : boolean, UnityEngine.Sprite
---@overload fun(self: UnityEngine.UIElements.VisualElement.CustomStyleAccess, property: UnityEngine.UIElements.CustomStyleProperty, out_value: UnityEngine.UIElements.VectorImage) : boolean, UnityEngine.UIElements.VectorImage
---@param property UnityEngine.UIElements.CustomStyleProperty
---@param out_value string
---@return boolean, string
function UnityEngine.UIElements.VisualElement.CustomStyleAccess:TryGetValue(property, out_value) end

---@class UnityEngine.UIElements.VisualElement.Hierarchy : System.ValueType
---@field parent UnityEngine.UIElements.VisualElement
---@field childCount number
---@field Item UnityEngine.UIElements.VisualElement
UnityEngine.UIElements.VisualElement.Hierarchy = {}
---@alias CS.UnityEngine.UIElements.VisualElement.Hierarchy UnityEngine.UIElements.VisualElement.Hierarchy
CS.UnityEngine.UIElements.VisualElement.Hierarchy = UnityEngine.UIElements.VisualElement.Hierarchy

---@param child UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement.Hierarchy:Add(child) end
---@param index number
---@param child UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement.Hierarchy:Insert(index, child) end
---@param child UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement.Hierarchy:Remove(child) end
---@param index number
function UnityEngine.UIElements.VisualElement.Hierarchy:RemoveAt(index) end
function UnityEngine.UIElements.VisualElement.Hierarchy:Clear() end
---@param element UnityEngine.UIElements.VisualElement
---@return number
function UnityEngine.UIElements.VisualElement.Hierarchy:IndexOf(element) end
---@param index number
---@return UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElement.Hierarchy:ElementAt(index) end
---@return System.Collections.Generic.IEnumerable
function UnityEngine.UIElements.VisualElement.Hierarchy:Children() end
---@param comp System.Comparison
function UnityEngine.UIElements.VisualElement.Hierarchy:Sort(comp) end
---@overload fun(self: UnityEngine.UIElements.VisualElement.Hierarchy, other: UnityEngine.UIElements.VisualElement.Hierarchy) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.VisualElement.Hierarchy:Equals(obj) end
---@return number
function UnityEngine.UIElements.VisualElement.Hierarchy:GetHashCode() end

---@class UnityEngine.UIElements.VisualElement.MeasureMode
---@field Undefined UnityEngine.UIElements.VisualElement.MeasureMode
---@field Exactly UnityEngine.UIElements.VisualElement.MeasureMode
---@field AtMost UnityEngine.UIElements.VisualElement.MeasureMode
UnityEngine.UIElements.VisualElement.MeasureMode = {}
---@alias CS.UnityEngine.UIElements.VisualElement.MeasureMode UnityEngine.UIElements.VisualElement.MeasureMode
CS.UnityEngine.UIElements.VisualElement.MeasureMode = UnityEngine.UIElements.VisualElement.MeasureMode


---@class UnityEngine.UIElements.VisualElement.RenderTargetMode
---@field None UnityEngine.UIElements.VisualElement.RenderTargetMode
---@field NoColorConversion UnityEngine.UIElements.VisualElement.RenderTargetMode
---@field LinearToGamma UnityEngine.UIElements.VisualElement.RenderTargetMode
---@field GammaToLinear UnityEngine.UIElements.VisualElement.RenderTargetMode
UnityEngine.UIElements.VisualElement.RenderTargetMode = {}
---@alias CS.UnityEngine.UIElements.VisualElement.RenderTargetMode UnityEngine.UIElements.VisualElement.RenderTargetMode
CS.UnityEngine.UIElements.VisualElement.RenderTargetMode = UnityEngine.UIElements.VisualElement.RenderTargetMode


---@class UnityEngine.UIElements.VisualElement.SimpleScheduledItem : UnityEngine.UIElements.VisualElement.VisualElementScheduledItem
UnityEngine.UIElements.VisualElement.SimpleScheduledItem = {}
---@alias CS.UnityEngine.UIElements.VisualElement.SimpleScheduledItem UnityEngine.UIElements.VisualElement.SimpleScheduledItem
CS.UnityEngine.UIElements.VisualElement.SimpleScheduledItem = UnityEngine.UIElements.VisualElement.SimpleScheduledItem

---@param handler UnityEngine.UIElements.VisualElement
---@param updateEvent System.Action | function
---@return UnityEngine.UIElements.VisualElement.SimpleScheduledItem
function UnityEngine.UIElements.VisualElement.SimpleScheduledItem.New(handler, updateEvent) end
---@param state UnityEngine.UIElements.TimerState
function UnityEngine.UIElements.VisualElement.SimpleScheduledItem:PerformTimerUpdate(state) end

---@class UnityEngine.UIElements.VisualElement.TimerStateScheduledItem : UnityEngine.UIElements.VisualElement.VisualElementScheduledItem
UnityEngine.UIElements.VisualElement.TimerStateScheduledItem = {}
---@alias CS.UnityEngine.UIElements.VisualElement.TimerStateScheduledItem UnityEngine.UIElements.VisualElement.TimerStateScheduledItem
CS.UnityEngine.UIElements.VisualElement.TimerStateScheduledItem = UnityEngine.UIElements.VisualElement.TimerStateScheduledItem

---@param handler UnityEngine.UIElements.VisualElement
---@param updateEvent System.Action | function
---@return UnityEngine.UIElements.VisualElement.TimerStateScheduledItem
function UnityEngine.UIElements.VisualElement.TimerStateScheduledItem.New(handler, updateEvent) end
---@param state UnityEngine.UIElements.TimerState
function UnityEngine.UIElements.VisualElement.TimerStateScheduledItem:PerformTimerUpdate(state) end

---@class UnityEngine.UIElements.VisualElement.TypeData : System.Object
---@field type System.Type
---@field fullTypeName string
---@field typeName string
---@field typeNamespace string
UnityEngine.UIElements.VisualElement.TypeData = {}
---@alias CS.UnityEngine.UIElements.VisualElement.TypeData UnityEngine.UIElements.VisualElement.TypeData
CS.UnityEngine.UIElements.VisualElement.TypeData = UnityEngine.UIElements.VisualElement.TypeData

---@param type System.Type
---@return UnityEngine.UIElements.VisualElement.TypeData
function UnityEngine.UIElements.VisualElement.TypeData.New(type) end

---@class UnityEngine.UIElements.VisualElement.UxmlFactory : UnityEngine.UIElements.UxmlFactory
UnityEngine.UIElements.VisualElement.UxmlFactory = {}
---@alias CS.UnityEngine.UIElements.VisualElement.UxmlFactory UnityEngine.UIElements.VisualElement.UxmlFactory
CS.UnityEngine.UIElements.VisualElement.UxmlFactory = UnityEngine.UIElements.VisualElement.UxmlFactory

---@return UnityEngine.UIElements.VisualElement.UxmlFactory
function UnityEngine.UIElements.VisualElement.UxmlFactory.New() end

---@class UnityEngine.UIElements.VisualElement.UxmlTraits : UnityEngine.UIElements.UxmlTraits
---@field uxmlChildElementsDescription System.Collections.Generic.IEnumerable
UnityEngine.UIElements.VisualElement.UxmlTraits = {}
---@alias CS.UnityEngine.UIElements.VisualElement.UxmlTraits UnityEngine.UIElements.VisualElement.UxmlTraits
CS.UnityEngine.UIElements.VisualElement.UxmlTraits = UnityEngine.UIElements.VisualElement.UxmlTraits

---@return UnityEngine.UIElements.VisualElement.UxmlTraits
function UnityEngine.UIElements.VisualElement.UxmlTraits.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param bag UnityEngine.UIElements.IUxmlAttributes
---@param cc UnityEngine.UIElements.CreationContext
function UnityEngine.UIElements.VisualElement.UxmlTraits:Init(ve, bag, cc) end

---@class UnityEngine.UIElements.VisualElement.VisualElementScheduledItem : UnityEngine.UIElements.VisualElement.BaseVisualElementScheduledItem
---@field updateEvent ActionType
UnityEngine.UIElements.VisualElement.VisualElementScheduledItem = {}
---@alias CS.UnityEngine.UIElements.VisualElement.VisualElementScheduledItem UnityEngine.UIElements.VisualElement.VisualElementScheduledItem
CS.UnityEngine.UIElements.VisualElement.VisualElementScheduledItem = UnityEngine.UIElements.VisualElement.VisualElementScheduledItem

---@param item UnityEngine.UIElements.ScheduledItem
---@param updateEvent ActionType
---@return boolean
function UnityEngine.UIElements.VisualElement.VisualElementScheduledItem.Matches(item, updateEvent) end

---@class UnityEngine.UIElements.VisualElementAnimationSystem : UnityEngine.UIElements.BaseVisualTreeUpdater
---@field profilerMarker Unity.Profiling.ProfilerMarker
UnityEngine.UIElements.VisualElementAnimationSystem = {}
---@alias CS.UnityEngine.UIElements.VisualElementAnimationSystem UnityEngine.UIElements.VisualElementAnimationSystem
CS.UnityEngine.UIElements.VisualElementAnimationSystem = UnityEngine.UIElements.VisualElementAnimationSystem

---@return UnityEngine.UIElements.VisualElementAnimationSystem
function UnityEngine.UIElements.VisualElementAnimationSystem.New() end
---@param anim UnityEngine.UIElements.Experimental.IValueAnimationUpdate
function UnityEngine.UIElements.VisualElementAnimationSystem:UnregisterAnimation(anim) end
---@param anims System.Collections.Generic.List
function UnityEngine.UIElements.VisualElementAnimationSystem:UnregisterAnimations(anims) end
---@param anim UnityEngine.UIElements.Experimental.IValueAnimationUpdate
function UnityEngine.UIElements.VisualElementAnimationSystem:RegisterAnimation(anim) end
---@param anims System.Collections.Generic.List
function UnityEngine.UIElements.VisualElementAnimationSystem:RegisterAnimations(anims) end
function UnityEngine.UIElements.VisualElementAnimationSystem:Update() end
---@param ve UnityEngine.UIElements.VisualElement
---@param versionChangeType UnityEngine.UIElements.VersionChangeType
function UnityEngine.UIElements.VisualElementAnimationSystem:OnVersionChanged(ve, versionChangeType) end

---@class UnityEngine.UIElements.VisualElementAsset : UnityEngine.UIElements.UxmlAsset
---@field ruleIndex number
---@field classes string[]
---@field stylesheetPaths System.Collections.Generic.List
---@field hasStylesheetPaths boolean
---@field stylesheets System.Collections.Generic.List
---@field hasStylesheets boolean
UnityEngine.UIElements.VisualElementAsset = {}
---@alias CS.UnityEngine.UIElements.VisualElementAsset UnityEngine.UIElements.VisualElementAsset
CS.UnityEngine.UIElements.VisualElementAsset = UnityEngine.UIElements.VisualElementAsset

---@param fullTypeName string
---@return UnityEngine.UIElements.VisualElementAsset
function UnityEngine.UIElements.VisualElementAsset.New(fullTypeName) end
function UnityEngine.UIElements.VisualElementAsset:OnBeforeSerialize() end
function UnityEngine.UIElements.VisualElementAsset:OnAfterDeserialize() end

---@class UnityEngine.UIElements.VisualElementDebugExtensions : System.Object
UnityEngine.UIElements.VisualElementDebugExtensions = {}
---@alias CS.UnityEngine.UIElements.VisualElementDebugExtensions UnityEngine.UIElements.VisualElementDebugExtensions
CS.UnityEngine.UIElements.VisualElementDebugExtensions = UnityEngine.UIElements.VisualElementDebugExtensions

---@param ve UnityEngine.UIElements.VisualElement
---@param withHashCode boolean
---@return string
function UnityEngine.UIElements.VisualElementDebugExtensions.GetDisplayName(ve, withHashCode) end

---@class UnityEngine.UIElements.VisualElementExtensions : System.Object
UnityEngine.UIElements.VisualElementExtensions = {}
---@alias CS.UnityEngine.UIElements.VisualElementExtensions UnityEngine.UIElements.VisualElementExtensions
CS.UnityEngine.UIElements.VisualElementExtensions = UnityEngine.UIElements.VisualElementExtensions

---@param elem UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElementExtensions.StretchToParentSize(elem) end
---@param elem UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualElementExtensions.StretchToParentWidth(elem) end
---@param ele UnityEngine.UIElements.VisualElement
---@param manipulator UnityEngine.UIElements.IManipulator
function UnityEngine.UIElements.VisualElementExtensions.AddManipulator(ele, manipulator) end
---@param ele UnityEngine.UIElements.VisualElement
---@param manipulator UnityEngine.UIElements.IManipulator
function UnityEngine.UIElements.VisualElementExtensions.RemoveManipulator(ele, manipulator) end
---@overload fun(ele: UnityEngine.UIElements.VisualElement, p: UnityEngine.Vector2) : UnityEngine.Vector2
---@param ele UnityEngine.UIElements.VisualElement
---@param r UnityEngine.Rect
---@return UnityEngine.Rect
function UnityEngine.UIElements.VisualElementExtensions.WorldToLocal(ele, r) end
---@overload fun(ele: UnityEngine.UIElements.VisualElement, p: UnityEngine.Vector2) : UnityEngine.Vector2
---@param ele UnityEngine.UIElements.VisualElement
---@param r UnityEngine.Rect
---@return UnityEngine.Rect
function UnityEngine.UIElements.VisualElementExtensions.LocalToWorld(ele, r) end
---@overload fun(src: UnityEngine.UIElements.VisualElement, dest: UnityEngine.UIElements.VisualElement, point: UnityEngine.Vector2) : UnityEngine.Vector2
---@param src UnityEngine.UIElements.VisualElement
---@param dest UnityEngine.UIElements.VisualElement
---@param rect UnityEngine.Rect
---@return UnityEngine.Rect
function UnityEngine.UIElements.VisualElementExtensions.ChangeCoordinatesTo(src, dest, rect) end

---@class UnityEngine.UIElements.VisualElementFactoryRegistry : System.Object
UnityEngine.UIElements.VisualElementFactoryRegistry = {}
---@alias CS.UnityEngine.UIElements.VisualElementFactoryRegistry UnityEngine.UIElements.VisualElementFactoryRegistry
CS.UnityEngine.UIElements.VisualElementFactoryRegistry = UnityEngine.UIElements.VisualElementFactoryRegistry

---@return UnityEngine.UIElements.VisualElementFactoryRegistry
function UnityEngine.UIElements.VisualElementFactoryRegistry.New() end

---@class UnityEngine.UIElements.VisualElementFlags
---@field WorldTransformDirty UnityEngine.UIElements.VisualElementFlags
---@field WorldTransformInverseDirty UnityEngine.UIElements.VisualElementFlags
---@field WorldClipDirty UnityEngine.UIElements.VisualElementFlags
---@field BoundingBoxDirty UnityEngine.UIElements.VisualElementFlags
---@field WorldBoundingBoxDirty UnityEngine.UIElements.VisualElementFlags
---@field EventCallbackParentCategoriesDirty UnityEngine.UIElements.VisualElementFlags
---@field LayoutManual UnityEngine.UIElements.VisualElementFlags
---@field CompositeRoot UnityEngine.UIElements.VisualElementFlags
---@field RequireMeasureFunction UnityEngine.UIElements.VisualElementFlags
---@field EnableViewDataPersistence UnityEngine.UIElements.VisualElementFlags
---@field DisableClipping UnityEngine.UIElements.VisualElementFlags
---@field NeedsAttachToPanelEvent UnityEngine.UIElements.VisualElementFlags
---@field HierarchyDisplayed UnityEngine.UIElements.VisualElementFlags
---@field StyleInitialized UnityEngine.UIElements.VisualElementFlags
---@field Init UnityEngine.UIElements.VisualElementFlags
UnityEngine.UIElements.VisualElementFlags = {}
---@alias CS.UnityEngine.UIElements.VisualElementFlags UnityEngine.UIElements.VisualElementFlags
CS.UnityEngine.UIElements.VisualElementFlags = UnityEngine.UIElements.VisualElementFlags


---@class UnityEngine.UIElements.VisualElementFocusChangeDirection : UnityEngine.UIElements.FocusChangeDirection
---@field left UnityEngine.UIElements.FocusChangeDirection
---@field right UnityEngine.UIElements.FocusChangeDirection
UnityEngine.UIElements.VisualElementFocusChangeDirection = {}
---@alias CS.UnityEngine.UIElements.VisualElementFocusChangeDirection UnityEngine.UIElements.VisualElementFocusChangeDirection
CS.UnityEngine.UIElements.VisualElementFocusChangeDirection = UnityEngine.UIElements.VisualElementFocusChangeDirection


---@class UnityEngine.UIElements.VisualElementFocusChangeTarget : UnityEngine.UIElements.FocusChangeDirection
---@field target UnityEngine.UIElements.Focusable
UnityEngine.UIElements.VisualElementFocusChangeTarget = {}
---@alias CS.UnityEngine.UIElements.VisualElementFocusChangeTarget UnityEngine.UIElements.VisualElementFocusChangeTarget
CS.UnityEngine.UIElements.VisualElementFocusChangeTarget = UnityEngine.UIElements.VisualElementFocusChangeTarget

---@return UnityEngine.UIElements.VisualElementFocusChangeTarget
function UnityEngine.UIElements.VisualElementFocusChangeTarget.New() end
---@param target UnityEngine.UIElements.Focusable
---@return UnityEngine.UIElements.VisualElementFocusChangeTarget
function UnityEngine.UIElements.VisualElementFocusChangeTarget.GetPooled(target) end

---@class UnityEngine.UIElements.VisualElementFocusRing : System.Object
---@field defaultFocusOrder UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder
UnityEngine.UIElements.VisualElementFocusRing = {}
---@alias CS.UnityEngine.UIElements.VisualElementFocusRing UnityEngine.UIElements.VisualElementFocusRing
CS.UnityEngine.UIElements.VisualElementFocusRing = UnityEngine.UIElements.VisualElementFocusRing

---@param root UnityEngine.UIElements.VisualElement
---@param dfo UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder
---@return UnityEngine.UIElements.VisualElementFocusRing
function UnityEngine.UIElements.VisualElementFocusRing.New(root, dfo) end
---@param currentFocusable UnityEngine.UIElements.Focusable
---@param e UnityEngine.UIElements.EventBase
---@return UnityEngine.UIElements.FocusChangeDirection
function UnityEngine.UIElements.VisualElementFocusRing:GetFocusChangeDirection(currentFocusable, e) end
---@param currentFocusable UnityEngine.UIElements.Focusable
---@param direction UnityEngine.UIElements.FocusChangeDirection
---@return UnityEngine.UIElements.Focusable
function UnityEngine.UIElements.VisualElementFocusRing:GetNextFocusable(currentFocusable, direction) end

---@class UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder
---@field ChildOrder UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder
---@field PositionXY UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder
---@field PositionYX UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder
UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder = {}
---@alias CS.UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder
CS.UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder = UnityEngine.UIElements.VisualElementFocusRing.DefaultFocusOrder


---@class UnityEngine.UIElements.VisualElementFocusRing.FocusRingRecord : System.Object
---@field m_AutoIndex number
---@field m_Focusable UnityEngine.UIElements.Focusable
---@field m_IsSlot boolean
---@field m_ScopeNavigationOrder System.Collections.Generic.List
UnityEngine.UIElements.VisualElementFocusRing.FocusRingRecord = {}
---@alias CS.UnityEngine.UIElements.VisualElementFocusRing.FocusRingRecord UnityEngine.UIElements.VisualElementFocusRing.FocusRingRecord
CS.UnityEngine.UIElements.VisualElementFocusRing.FocusRingRecord = UnityEngine.UIElements.VisualElementFocusRing.FocusRingRecord

---@return UnityEngine.UIElements.VisualElementFocusRing.FocusRingRecord
function UnityEngine.UIElements.VisualElementFocusRing.FocusRingRecord.New() end

---@class UnityEngine.UIElements.VisualElementListPool : System.Object
UnityEngine.UIElements.VisualElementListPool = {}
---@alias CS.UnityEngine.UIElements.VisualElementListPool UnityEngine.UIElements.VisualElementListPool
CS.UnityEngine.UIElements.VisualElementListPool = UnityEngine.UIElements.VisualElementListPool

---@param elements System.Collections.Generic.List
---@return System.Collections.Generic.List
function UnityEngine.UIElements.VisualElementListPool.Copy(elements) end
---@param initialCapacity number
---@return System.Collections.Generic.List
function UnityEngine.UIElements.VisualElementListPool.Get(initialCapacity) end
---@param elements System.Collections.Generic.List
function UnityEngine.UIElements.VisualElementListPool.Release(elements) end

---@class UnityEngine.UIElements.VisualElementPanelActivator : System.Object
---@field isActive boolean
---@field isDetaching boolean
UnityEngine.UIElements.VisualElementPanelActivator = {}
---@alias CS.UnityEngine.UIElements.VisualElementPanelActivator UnityEngine.UIElements.VisualElementPanelActivator
CS.UnityEngine.UIElements.VisualElementPanelActivator = UnityEngine.UIElements.VisualElementPanelActivator

---@param activatable UnityEngine.UIElements.IVisualElementPanelActivatable
---@return UnityEngine.UIElements.VisualElementPanelActivator
function UnityEngine.UIElements.VisualElementPanelActivator.New(activatable) end
---@param action boolean
function UnityEngine.UIElements.VisualElementPanelActivator:SetActive(action) end
function UnityEngine.UIElements.VisualElementPanelActivator:SendActivation() end
function UnityEngine.UIElements.VisualElementPanelActivator:SendDeactivation() end

---@class UnityEngine.UIElements.VisualElementStyleSheetSet : System.ValueType
---@field count number
---@field Item UnityEngine.UIElements.StyleSheet
UnityEngine.UIElements.VisualElementStyleSheetSet = {}
---@alias CS.UnityEngine.UIElements.VisualElementStyleSheetSet UnityEngine.UIElements.VisualElementStyleSheetSet
CS.UnityEngine.UIElements.VisualElementStyleSheetSet = UnityEngine.UIElements.VisualElementStyleSheetSet

---@param styleSheet UnityEngine.UIElements.StyleSheet
function UnityEngine.UIElements.VisualElementStyleSheetSet:Add(styleSheet) end
function UnityEngine.UIElements.VisualElementStyleSheetSet:Clear() end
---@param styleSheet UnityEngine.UIElements.StyleSheet
---@return boolean
function UnityEngine.UIElements.VisualElementStyleSheetSet:Remove(styleSheet) end
---@param styleSheet UnityEngine.UIElements.StyleSheet
---@return boolean
function UnityEngine.UIElements.VisualElementStyleSheetSet:Contains(styleSheet) end
---@overload fun(self: UnityEngine.UIElements.VisualElementStyleSheetSet, other: UnityEngine.UIElements.VisualElementStyleSheetSet) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.UIElements.VisualElementStyleSheetSet:Equals(obj) end
---@return number
function UnityEngine.UIElements.VisualElementStyleSheetSet:GetHashCode() end

---@class UnityEngine.UIElements.VisualElementUtils : System.Object
UnityEngine.UIElements.VisualElementUtils = {}
---@alias CS.UnityEngine.UIElements.VisualElementUtils UnityEngine.UIElements.VisualElementUtils
CS.UnityEngine.UIElements.VisualElementUtils = UnityEngine.UIElements.VisualElementUtils

---@param nameBase string
---@return string
function UnityEngine.UIElements.VisualElementUtils.GetUniqueName(nameBase) end

---@class UnityEngine.UIElements.VisualTreeAsset : UnityEngine.ScriptableObject
---@field importedWithErrors boolean
---@field importedWithWarnings boolean
---@field templateDependencies System.Collections.Generic.IEnumerable
---@field stylesheets System.Collections.Generic.IEnumerable
---@field contentHash number
UnityEngine.UIElements.VisualTreeAsset = {}
---@alias CS.UnityEngine.UIElements.VisualTreeAsset UnityEngine.UIElements.VisualTreeAsset
CS.UnityEngine.UIElements.VisualTreeAsset = UnityEngine.UIElements.VisualTreeAsset

---@return UnityEngine.UIElements.VisualTreeAsset
function UnityEngine.UIElements.VisualTreeAsset.New() end
---@overload fun(self: UnityEngine.UIElements.VisualTreeAsset) : UnityEngine.UIElements.TemplateContainer
---@param bindingPath string
---@return UnityEngine.UIElements.TemplateContainer
function UnityEngine.UIElements.VisualTreeAsset:Instantiate(bindingPath) end
---@overload fun(self: UnityEngine.UIElements.VisualTreeAsset) : UnityEngine.UIElements.TemplateContainer
---@overload fun(self: UnityEngine.UIElements.VisualTreeAsset, bindingPath: string) : UnityEngine.UIElements.TemplateContainer
---@overload fun(self: UnityEngine.UIElements.VisualTreeAsset, target: UnityEngine.UIElements.VisualElement)
---@param target UnityEngine.UIElements.VisualElement
---@param out_firstElementIndex number
---@param out_elementAddedCount number
---@return number, number
function UnityEngine.UIElements.VisualTreeAsset:CloneTree(target, out_firstElementIndex, out_elementAddedCount) end

---@class UnityEngine.UIElements.VisualTreeAsset.AssetEntry : System.ValueType
---@field path string
---@field typeFullName string
---@field asset UnityEngine.Object
---@field type System.Type
UnityEngine.UIElements.VisualTreeAsset.AssetEntry = {}
---@alias CS.UnityEngine.UIElements.VisualTreeAsset.AssetEntry UnityEngine.UIElements.VisualTreeAsset.AssetEntry
CS.UnityEngine.UIElements.VisualTreeAsset.AssetEntry = UnityEngine.UIElements.VisualTreeAsset.AssetEntry

---@param path string
---@param type System.Type
---@param asset UnityEngine.Object
---@return UnityEngine.UIElements.VisualTreeAsset.AssetEntry
function UnityEngine.UIElements.VisualTreeAsset.AssetEntry.New(path, type, asset) end

---@class UnityEngine.UIElements.VisualTreeAsset.SlotDefinition : System.ValueType
---@field name string
---@field insertionPointId number
UnityEngine.UIElements.VisualTreeAsset.SlotDefinition = {}
---@alias CS.UnityEngine.UIElements.VisualTreeAsset.SlotDefinition UnityEngine.UIElements.VisualTreeAsset.SlotDefinition
CS.UnityEngine.UIElements.VisualTreeAsset.SlotDefinition = UnityEngine.UIElements.VisualTreeAsset.SlotDefinition


---@class UnityEngine.UIElements.VisualTreeAsset.SlotUsageEntry : System.ValueType
---@field slotName string
---@field assetId number
UnityEngine.UIElements.VisualTreeAsset.SlotUsageEntry = {}
---@alias CS.UnityEngine.UIElements.VisualTreeAsset.SlotUsageEntry UnityEngine.UIElements.VisualTreeAsset.SlotUsageEntry
CS.UnityEngine.UIElements.VisualTreeAsset.SlotUsageEntry = UnityEngine.UIElements.VisualTreeAsset.SlotUsageEntry

---@param slotName string
---@param assetId number
---@return UnityEngine.UIElements.VisualTreeAsset.SlotUsageEntry
function UnityEngine.UIElements.VisualTreeAsset.SlotUsageEntry.New(slotName, assetId) end

---@class UnityEngine.UIElements.VisualTreeAsset.UsingEntry : System.ValueType
---@field alias string
---@field path string
---@field asset UnityEngine.UIElements.VisualTreeAsset
UnityEngine.UIElements.VisualTreeAsset.UsingEntry = {}
---@alias CS.UnityEngine.UIElements.VisualTreeAsset.UsingEntry UnityEngine.UIElements.VisualTreeAsset.UsingEntry
CS.UnityEngine.UIElements.VisualTreeAsset.UsingEntry = UnityEngine.UIElements.VisualTreeAsset.UsingEntry

---@overload fun(alias: string, path: string) : UnityEngine.UIElements.VisualTreeAsset.UsingEntry
---@param alias string
---@param asset UnityEngine.UIElements.VisualTreeAsset
---@return UnityEngine.UIElements.VisualTreeAsset.UsingEntry
function UnityEngine.UIElements.VisualTreeAsset.UsingEntry.New(alias, asset) end

---@class UnityEngine.UIElements.VisualTreeAsset.UsingEntryComparer : System.Object
UnityEngine.UIElements.VisualTreeAsset.UsingEntryComparer = {}
---@alias CS.UnityEngine.UIElements.VisualTreeAsset.UsingEntryComparer UnityEngine.UIElements.VisualTreeAsset.UsingEntryComparer
CS.UnityEngine.UIElements.VisualTreeAsset.UsingEntryComparer = UnityEngine.UIElements.VisualTreeAsset.UsingEntryComparer

---@return UnityEngine.UIElements.VisualTreeAsset.UsingEntryComparer
function UnityEngine.UIElements.VisualTreeAsset.UsingEntryComparer.New() end
---@param x UnityEngine.UIElements.VisualTreeAsset.UsingEntry
---@param y UnityEngine.UIElements.VisualTreeAsset.UsingEntry
---@return number
function UnityEngine.UIElements.VisualTreeAsset.UsingEntryComparer:Compare(x, y) end

---@class UnityEngine.UIElements.VisualTreeAsset.UxmlObjectEntry : System.ValueType
---@field parentId number
---@field uxmlObjectAssets System.Collections.Generic.List
UnityEngine.UIElements.VisualTreeAsset.UxmlObjectEntry = {}
---@alias CS.UnityEngine.UIElements.VisualTreeAsset.UxmlObjectEntry UnityEngine.UIElements.VisualTreeAsset.UxmlObjectEntry
CS.UnityEngine.UIElements.VisualTreeAsset.UxmlObjectEntry = UnityEngine.UIElements.VisualTreeAsset.UxmlObjectEntry

---@param parentId number
---@param uxmlObjectAssets System.Collections.Generic.List
---@return UnityEngine.UIElements.VisualTreeAsset.UxmlObjectEntry
function UnityEngine.UIElements.VisualTreeAsset.UxmlObjectEntry.New(parentId, uxmlObjectAssets) end

---@class UnityEngine.UIElements.VisualTreeBindingsUpdater : UnityEngine.UIElements.BaseVisualTreeHierarchyTrackerUpdater
---@field disableBindingsThrottling boolean
---@field profilerMarker Unity.Profiling.ProfilerMarker
---@field temporaryObjectCache System.Collections.Generic.Dictionary
UnityEngine.UIElements.VisualTreeBindingsUpdater = {}
---@alias CS.UnityEngine.UIElements.VisualTreeBindingsUpdater UnityEngine.UIElements.VisualTreeBindingsUpdater
CS.UnityEngine.UIElements.VisualTreeBindingsUpdater = UnityEngine.UIElements.VisualTreeBindingsUpdater

---@return UnityEngine.UIElements.VisualTreeBindingsUpdater
function UnityEngine.UIElements.VisualTreeBindingsUpdater.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param b UnityEngine.UIElements.IBinding
function UnityEngine.UIElements.VisualTreeBindingsUpdater.SetAdditionalBinding(ve, b) end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualTreeBindingsUpdater.ClearAdditionalBinding(ve) end
---@param ve UnityEngine.UIElements.VisualElement
---@return UnityEngine.UIElements.IBinding
function UnityEngine.UIElements.VisualTreeBindingsUpdater.GetAdditionalBinding(ve) end
---@param ve UnityEngine.UIElements.VisualElement
---@param req UnityEngine.UIElements.IBindingRequest
function UnityEngine.UIElements.VisualTreeBindingsUpdater.AddBindingRequest(ve, req) end
---@param ve UnityEngine.UIElements.VisualElement
---@param req UnityEngine.UIElements.IBindingRequest
function UnityEngine.UIElements.VisualTreeBindingsUpdater.RemoveBindingRequest(ve, req) end
---@param ve UnityEngine.UIElements.VisualElement
function UnityEngine.UIElements.VisualTreeBindingsUpdater.ClearBindingRequests(ve) end
---@param startTime number
---@return boolean
function UnityEngine.UIElements.VisualTreeBindingsUpdater.ShouldThrottle(startTime) end
---@param ve UnityEngine.UIElements.VisualElement
---@param versionChangeType UnityEngine.UIElements.VersionChangeType
function UnityEngine.UIElements.VisualTreeBindingsUpdater:OnVersionChanged(ve, versionChangeType) end
function UnityEngine.UIElements.VisualTreeBindingsUpdater:PerformTrackingOperations() end
function UnityEngine.UIElements.VisualTreeBindingsUpdater:Update() end

---@class UnityEngine.UIElements.VisualTreeBindingsUpdater.RequestObjectListPool : UnityEngine.UIElements.ObjectListPool
UnityEngine.UIElements.VisualTreeBindingsUpdater.RequestObjectListPool = {}
---@alias CS.UnityEngine.UIElements.VisualTreeBindingsUpdater.RequestObjectListPool UnityEngine.UIElements.VisualTreeBindingsUpdater.RequestObjectListPool
CS.UnityEngine.UIElements.VisualTreeBindingsUpdater.RequestObjectListPool = UnityEngine.UIElements.VisualTreeBindingsUpdater.RequestObjectListPool

---@return UnityEngine.UIElements.VisualTreeBindingsUpdater.RequestObjectListPool
function UnityEngine.UIElements.VisualTreeBindingsUpdater.RequestObjectListPool.New() end

---@class UnityEngine.UIElements.VisualTreeEditorUpdatePhase
---@field AssetChange UnityEngine.UIElements.VisualTreeEditorUpdatePhase
---@field Count UnityEngine.UIElements.VisualTreeEditorUpdatePhase
UnityEngine.UIElements.VisualTreeEditorUpdatePhase = {}
---@alias CS.UnityEngine.UIElements.VisualTreeEditorUpdatePhase UnityEngine.UIElements.VisualTreeEditorUpdatePhase
CS.UnityEngine.UIElements.VisualTreeEditorUpdatePhase = UnityEngine.UIElements.VisualTreeEditorUpdatePhase


---@class UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater : UnityEngine.UIElements.BaseVisualTreeUpdater
---@field profilerMarker Unity.Profiling.ProfilerMarker
UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater = {}
---@alias CS.UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater
CS.UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater = UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater

---@return UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater
function UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param versionChangeType UnityEngine.UIElements.VersionChangeType
function UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater:OnVersionChanged(ve, versionChangeType) end
function UnityEngine.UIElements.VisualTreeHierarchyFlagsUpdater:Update() end

---@class UnityEngine.UIElements.VisualTreeStyleUpdater : UnityEngine.UIElements.BaseVisualTreeUpdater
---@field traversal UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal
---@field profilerMarker Unity.Profiling.ProfilerMarker
UnityEngine.UIElements.VisualTreeStyleUpdater = {}
---@alias CS.UnityEngine.UIElements.VisualTreeStyleUpdater UnityEngine.UIElements.VisualTreeStyleUpdater
CS.UnityEngine.UIElements.VisualTreeStyleUpdater = UnityEngine.UIElements.VisualTreeStyleUpdater

---@return UnityEngine.UIElements.VisualTreeStyleUpdater
function UnityEngine.UIElements.VisualTreeStyleUpdater.New() end
function UnityEngine.UIElements.VisualTreeStyleUpdater:DirtyStyleSheets() end
---@param ve UnityEngine.UIElements.VisualElement
---@param versionChangeType UnityEngine.UIElements.VersionChangeType
function UnityEngine.UIElements.VisualTreeStyleUpdater:OnVersionChanged(ve, versionChangeType) end
function UnityEngine.UIElements.VisualTreeStyleUpdater:Update() end

---@class UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal : UnityEngine.UIElements.StyleSheets.HierarchyTraversal
---@field styleMatchingContext UnityEngine.UIElements.StyleMatchingContext
UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal = {}
---@alias CS.UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal
CS.UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal = UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal

---@return UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal
function UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal.New() end
---@param pixelsPerPoint number
function UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal:PrepareTraversal(pixelsPerPoint) end
---@param ve UnityEngine.UIElements.VisualElement
---@param versionChangeType UnityEngine.UIElements.VersionChangeType
function UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal:AddChangedElement(ve, versionChangeType) end
function UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal:Clear() end
---@param element UnityEngine.UIElements.VisualElement
---@param depth number
function UnityEngine.UIElements.VisualTreeStyleUpdaterTraversal:TraverseRecursive(element, depth) end

---@class UnityEngine.UIElements.VisualTreeUpdatePhase
---@field ViewData UnityEngine.UIElements.VisualTreeUpdatePhase
---@field Bindings UnityEngine.UIElements.VisualTreeUpdatePhase
---@field Animation UnityEngine.UIElements.VisualTreeUpdatePhase
---@field Styles UnityEngine.UIElements.VisualTreeUpdatePhase
---@field Layout UnityEngine.UIElements.VisualTreeUpdatePhase
---@field TransformClip UnityEngine.UIElements.VisualTreeUpdatePhase
---@field Repaint UnityEngine.UIElements.VisualTreeUpdatePhase
---@field Count UnityEngine.UIElements.VisualTreeUpdatePhase
UnityEngine.UIElements.VisualTreeUpdatePhase = {}
---@alias CS.UnityEngine.UIElements.VisualTreeUpdatePhase UnityEngine.UIElements.VisualTreeUpdatePhase
CS.UnityEngine.UIElements.VisualTreeUpdatePhase = UnityEngine.UIElements.VisualTreeUpdatePhase


---@class UnityEngine.UIElements.VisualTreeUpdater : System.Object
---@field visualTreeEditorUpdater UnityEngine.UIElements.IVisualTreeEditorUpdater
UnityEngine.UIElements.VisualTreeUpdater = {}
---@alias CS.UnityEngine.UIElements.VisualTreeUpdater UnityEngine.UIElements.VisualTreeUpdater
CS.UnityEngine.UIElements.VisualTreeUpdater = UnityEngine.UIElements.VisualTreeUpdater

---@param panel UnityEngine.UIElements.BaseVisualElementPanel
---@return UnityEngine.UIElements.VisualTreeUpdater
function UnityEngine.UIElements.VisualTreeUpdater.New(panel) end
function UnityEngine.UIElements.VisualTreeUpdater:Dispose() end
function UnityEngine.UIElements.VisualTreeUpdater:UpdateVisualTree() end
---@param phase UnityEngine.UIElements.VisualTreeUpdatePhase
function UnityEngine.UIElements.VisualTreeUpdater:UpdateVisualTreePhase(phase) end
---@param ve UnityEngine.UIElements.VisualElement
---@param versionChangeType UnityEngine.UIElements.VersionChangeType
function UnityEngine.UIElements.VisualTreeUpdater:OnVersionChanged(ve, versionChangeType) end
function UnityEngine.UIElements.VisualTreeUpdater:DirtyStyleSheets() end
---@param updater UnityEngine.UIElements.IVisualTreeUpdater
---@param phase UnityEngine.UIElements.VisualTreeUpdatePhase
function UnityEngine.UIElements.VisualTreeUpdater:SetUpdater(updater, phase) end
---@param phase UnityEngine.UIElements.VisualTreeUpdatePhase
---@return UnityEngine.UIElements.IVisualTreeUpdater
function UnityEngine.UIElements.VisualTreeUpdater:GetUpdater(phase) end

---@class UnityEngine.UIElements.VisualTreeUpdater.UpdaterArray : System.Object
---@field Item UnityEngine.UIElements.IVisualTreeUpdater
---@field Item UnityEngine.UIElements.IVisualTreeUpdater
UnityEngine.UIElements.VisualTreeUpdater.UpdaterArray = {}
---@alias CS.UnityEngine.UIElements.VisualTreeUpdater.UpdaterArray UnityEngine.UIElements.VisualTreeUpdater.UpdaterArray
CS.UnityEngine.UIElements.VisualTreeUpdater.UpdaterArray = UnityEngine.UIElements.VisualTreeUpdater.UpdaterArray

---@return UnityEngine.UIElements.VisualTreeUpdater.UpdaterArray
function UnityEngine.UIElements.VisualTreeUpdater.UpdaterArray.New() end

---@class UnityEngine.UIElements.VisualTreeViewDataUpdater : UnityEngine.UIElements.BaseVisualTreeUpdater
---@field profilerMarker Unity.Profiling.ProfilerMarker
UnityEngine.UIElements.VisualTreeViewDataUpdater = {}
---@alias CS.UnityEngine.UIElements.VisualTreeViewDataUpdater UnityEngine.UIElements.VisualTreeViewDataUpdater
CS.UnityEngine.UIElements.VisualTreeViewDataUpdater = UnityEngine.UIElements.VisualTreeViewDataUpdater

---@return UnityEngine.UIElements.VisualTreeViewDataUpdater
function UnityEngine.UIElements.VisualTreeViewDataUpdater.New() end
---@param ve UnityEngine.UIElements.VisualElement
---@param versionChangeType UnityEngine.UIElements.VersionChangeType
function UnityEngine.UIElements.VisualTreeViewDataUpdater:OnVersionChanged(ve, versionChangeType) end
function UnityEngine.UIElements.VisualTreeViewDataUpdater:Update() end

---@class UnityEngine.UIElements.WheelEvent : UnityEngine.UIElements.MouseEventBase
---@field delta UnityEngine.Vector3
UnityEngine.UIElements.WheelEvent = {}
---@alias CS.UnityEngine.UIElements.WheelEvent UnityEngine.UIElements.WheelEvent
CS.UnityEngine.UIElements.WheelEvent = UnityEngine.UIElements.WheelEvent

---@return UnityEngine.UIElements.WheelEvent
function UnityEngine.UIElements.WheelEvent.New() end
---@param systemEvent UnityEngine.Event
---@return UnityEngine.UIElements.WheelEvent
function UnityEngine.UIElements.WheelEvent.GetPooled(systemEvent) end

---@class UnityEngine.UIElements.WhiteSpace
---@field Normal UnityEngine.UIElements.WhiteSpace
---@field NoWrap UnityEngine.UIElements.WhiteSpace
UnityEngine.UIElements.WhiteSpace = {}
---@alias CS.UnityEngine.UIElements.WhiteSpace UnityEngine.UIElements.WhiteSpace
CS.UnityEngine.UIElements.WhiteSpace = UnityEngine.UIElements.WhiteSpace


---@class UnityEngine.UIElements.Wrap
---@field NoWrap UnityEngine.UIElements.Wrap
---@field Wrap UnityEngine.UIElements.Wrap
---@field WrapReverse UnityEngine.UIElements.Wrap
UnityEngine.UIElements.Wrap = {}
---@alias CS.UnityEngine.UIElements.Wrap UnityEngine.UIElements.Wrap
CS.UnityEngine.UIElements.Wrap = UnityEngine.UIElements.Wrap


---@class UnityEngine.UILineInfo : System.ValueType
---@field startCharIdx number
---@field height number
---@field topY number
---@field leading number
UnityEngine.UILineInfo = {}
---@alias CS.UnityEngine.UILineInfo UnityEngine.UILineInfo
CS.UnityEngine.UILineInfo = UnityEngine.UILineInfo


---@class UnityEngine.UINumericFieldsUtils : System.Object
---@field k_AllowedCharactersForFloat string
---@field k_AllowedCharactersForInt string
---@field k_DoubleFieldFormatString string
---@field k_FloatFieldFormatString string
---@field k_IntFieldFormatString string
UnityEngine.UINumericFieldsUtils = {}
---@alias CS.UnityEngine.UINumericFieldsUtils UnityEngine.UINumericFieldsUtils
CS.UnityEngine.UINumericFieldsUtils = UnityEngine.UINumericFieldsUtils

---@overload fun(str: string, out_value: number) : boolean, number
---@overload fun(str: string, out_value: number, out_expr: UnityEngine.ExpressionEvaluator.Expression) : boolean, number, UnityEngine.ExpressionEvaluator.Expression
---@param str string
---@param initialValueAsString string
---@param out_value number
---@param out_expression UnityEngine.ExpressionEvaluator.Expression
---@return boolean, number, UnityEngine.ExpressionEvaluator.Expression
function UnityEngine.UINumericFieldsUtils.TryConvertStringToDouble(str, initialValueAsString, out_value, out_expression) end
---@param str string
---@param initialValueAsString string
---@param out_value number
---@param out_expression UnityEngine.ExpressionEvaluator.Expression
---@return boolean, number, UnityEngine.ExpressionEvaluator.Expression
function UnityEngine.UINumericFieldsUtils.TryConvertStringToFloat(str, initialValueAsString, out_value, out_expression) end
---@overload fun(str: string, out_value: number) : boolean, number
---@overload fun(str: string, out_value: number, out_expr: UnityEngine.ExpressionEvaluator.Expression) : boolean, number, UnityEngine.ExpressionEvaluator.Expression
---@param str string
---@param initialValueAsString string
---@param out_value number
---@param out_expression UnityEngine.ExpressionEvaluator.Expression
---@return boolean, number, UnityEngine.ExpressionEvaluator.Expression
function UnityEngine.UINumericFieldsUtils.TryConvertStringToLong(str, initialValueAsString, out_value, out_expression) end
---@overload fun(str: string, out_value: number, out_expr: UnityEngine.ExpressionEvaluator.Expression) : boolean, number, UnityEngine.ExpressionEvaluator.Expression
---@param str string
---@param initialValueAsString string
---@param out_value number
---@param out_expression UnityEngine.ExpressionEvaluator.Expression
---@return boolean, number, UnityEngine.ExpressionEvaluator.Expression
function UnityEngine.UINumericFieldsUtils.TryConvertStringToULong(str, initialValueAsString, out_value, out_expression) end
---@param str string
---@param initialValueAsString string
---@param out_value number
---@param out_expression UnityEngine.ExpressionEvaluator.Expression
---@return boolean, number, UnityEngine.ExpressionEvaluator.Expression
function UnityEngine.UINumericFieldsUtils.TryConvertStringToInt(str, initialValueAsString, out_value, out_expression) end
---@param str string
---@param initialValueAsString string
---@param out_value number
---@param out_expression UnityEngine.ExpressionEvaluator.Expression
---@return boolean, number, UnityEngine.ExpressionEvaluator.Expression
function UnityEngine.UINumericFieldsUtils.TryConvertStringToUInt(str, initialValueAsString, out_value, out_expression) end

---@class UnityEngine.UISystemProfilerApi : System.Object
UnityEngine.UISystemProfilerApi = {}
---@alias CS.UnityEngine.UISystemProfilerApi UnityEngine.UISystemProfilerApi
CS.UnityEngine.UISystemProfilerApi = UnityEngine.UISystemProfilerApi

---@param type UnityEngine.UISystemProfilerApi.SampleType
function UnityEngine.UISystemProfilerApi.BeginSample(type) end
---@param type UnityEngine.UISystemProfilerApi.SampleType
function UnityEngine.UISystemProfilerApi.EndSample(type) end
---@param name string
---@param obj UnityEngine.Object
function UnityEngine.UISystemProfilerApi.AddMarker(name, obj) end

---@class UnityEngine.UISystemProfilerApi.SampleType
---@field Layout UnityEngine.UISystemProfilerApi.SampleType
---@field Render UnityEngine.UISystemProfilerApi.SampleType
UnityEngine.UISystemProfilerApi.SampleType = {}
---@alias CS.UnityEngine.UISystemProfilerApi.SampleType UnityEngine.UISystemProfilerApi.SampleType
CS.UnityEngine.UISystemProfilerApi.SampleType = UnityEngine.UISystemProfilerApi.SampleType


---@class UnityEngine.UIVertex : System.ValueType
---@field simpleVert UnityEngine.UIVertex
---@field position UnityEngine.Vector3
---@field normal UnityEngine.Vector3
---@field tangent UnityEngine.Vector4
---@field color UnityEngine.Color32
---@field uv0 UnityEngine.Vector4
---@field uv1 UnityEngine.Vector4
---@field uv2 UnityEngine.Vector4
---@field uv3 UnityEngine.Vector4
UnityEngine.UIVertex = {}
---@alias CS.UnityEngine.UIVertex UnityEngine.UIVertex
CS.UnityEngine.UIVertex = UnityEngine.UIVertex


---@class UnityEngine.UnassignedReferenceException : System.SystemException
UnityEngine.UnassignedReferenceException = {}
---@alias CS.UnityEngine.UnassignedReferenceException UnityEngine.UnassignedReferenceException
CS.UnityEngine.UnassignedReferenceException = UnityEngine.UnassignedReferenceException

---@overload fun() : UnityEngine.UnassignedReferenceException
---@overload fun(message: string) : UnityEngine.UnassignedReferenceException
---@param message string
---@param innerException System.Exception
---@return UnityEngine.UnassignedReferenceException
function UnityEngine.UnassignedReferenceException.New(message, innerException) end

---@class UnityEngine.UnhandledExceptionHandler : System.Object
UnityEngine.UnhandledExceptionHandler = {}
---@alias CS.UnityEngine.UnhandledExceptionHandler UnityEngine.UnhandledExceptionHandler
CS.UnityEngine.UnhandledExceptionHandler = UnityEngine.UnhandledExceptionHandler

---@return UnityEngine.UnhandledExceptionHandler
function UnityEngine.UnhandledExceptionHandler.New() end

---@class UnityEngine.UnityAPICompatibilityVersionAttribute : System.Attribute
---@field version string
UnityEngine.UnityAPICompatibilityVersionAttribute = {}
---@alias CS.UnityEngine.UnityAPICompatibilityVersionAttribute UnityEngine.UnityAPICompatibilityVersionAttribute
CS.UnityEngine.UnityAPICompatibilityVersionAttribute = UnityEngine.UnityAPICompatibilityVersionAttribute

---@overload fun(version: string) : UnityEngine.UnityAPICompatibilityVersionAttribute
---@overload fun(version: string, checkOnlyUnityVersion: boolean) : UnityEngine.UnityAPICompatibilityVersionAttribute
---@param version string
---@param configurationAssembliesHashes string[]
---@return UnityEngine.UnityAPICompatibilityVersionAttribute
function UnityEngine.UnityAPICompatibilityVersionAttribute.New(version, configurationAssembliesHashes) end

---@class UnityEngine.UnityEngineModuleAssembly : System.Attribute
UnityEngine.UnityEngineModuleAssembly = {}
---@alias CS.UnityEngine.UnityEngineModuleAssembly UnityEngine.UnityEngineModuleAssembly
CS.UnityEngine.UnityEngineModuleAssembly = UnityEngine.UnityEngineModuleAssembly

---@return UnityEngine.UnityEngineModuleAssembly
function UnityEngine.UnityEngineModuleAssembly.New() end

---@class UnityEngine.UnityEventQueueSystem : System.Object
UnityEngine.UnityEventQueueSystem = {}
---@alias CS.UnityEngine.UnityEventQueueSystem UnityEngine.UnityEventQueueSystem
CS.UnityEngine.UnityEventQueueSystem = UnityEngine.UnityEventQueueSystem

---@return UnityEngine.UnityEventQueueSystem
function UnityEngine.UnityEventQueueSystem.New() end
---@param eventPayloadName string
---@return string
function UnityEngine.UnityEventQueueSystem.GenerateEventIdForPayload(eventPayloadName) end
---@return System.IntPtr
function UnityEngine.UnityEventQueueSystem.GetGlobalEventQueue() end

---@class UnityEngine.UnityException : System.SystemException
UnityEngine.UnityException = {}
---@alias CS.UnityEngine.UnityException UnityEngine.UnityException
CS.UnityEngine.UnityException = UnityEngine.UnityException

---@overload fun() : UnityEngine.UnityException
---@overload fun(message: string) : UnityEngine.UnityException
---@param message string
---@param innerException System.Exception
---@return UnityEngine.UnityException
function UnityEngine.UnityException.New(message, innerException) end

---@class UnityEngine.UnityLogWriter : System.IO.TextWriter
---@field Encoding System.Text.Encoding
UnityEngine.UnityLogWriter = {}
---@alias CS.UnityEngine.UnityLogWriter UnityEngine.UnityLogWriter
CS.UnityEngine.UnityLogWriter = UnityEngine.UnityLogWriter

---@return UnityEngine.UnityLogWriter
function UnityEngine.UnityLogWriter.New() end
---@param s string
function UnityEngine.UnityLogWriter.WriteStringToUnityLog(s) end
function UnityEngine.UnityLogWriter.Init() end
---@overload fun(self: UnityEngine.UnityLogWriter, value: System.Char)
---@overload fun(self: UnityEngine.UnityLogWriter, s: string)
---@param buffer System.Char[]
---@param index number
---@param count number
function UnityEngine.UnityLogWriter:Write(buffer, index, count) end

---@class UnityEngine.UnityString : System.Object
UnityEngine.UnityString = {}
---@alias CS.UnityEngine.UnityString UnityEngine.UnityString
CS.UnityEngine.UnityString = UnityEngine.UnityString

---@return UnityEngine.UnityString
function UnityEngine.UnityString.New() end
---@param fmt string
---@param args System.Object[]
---@return string
function UnityEngine.UnityString.Format(fmt, args) end

---@class UnityEngine.UnitySynchronizationContext : System.Threading.SynchronizationContext
UnityEngine.UnitySynchronizationContext = {}
---@alias CS.UnityEngine.UnitySynchronizationContext UnityEngine.UnitySynchronizationContext
CS.UnityEngine.UnitySynchronizationContext = UnityEngine.UnitySynchronizationContext

---@param callback System.Threading.SendOrPostCallback
---@param state System.Object
function UnityEngine.UnitySynchronizationContext:Send(callback, state) end
function UnityEngine.UnitySynchronizationContext:OperationStarted() end
function UnityEngine.UnitySynchronizationContext:OperationCompleted() end
---@param callback System.Threading.SendOrPostCallback
---@param state System.Object
function UnityEngine.UnitySynchronizationContext:Post(callback, state) end
---@return System.Threading.SynchronizationContext
function UnityEngine.UnitySynchronizationContext:CreateCopy() end
function UnityEngine.UnitySynchronizationContext:Exec() end

---@class UnityEngine.UnitySynchronizationContext.WorkRequest : System.ValueType
UnityEngine.UnitySynchronizationContext.WorkRequest = {}
---@alias CS.UnityEngine.UnitySynchronizationContext.WorkRequest UnityEngine.UnitySynchronizationContext.WorkRequest
CS.UnityEngine.UnitySynchronizationContext.WorkRequest = UnityEngine.UnitySynchronizationContext.WorkRequest

---@param callback System.Threading.SendOrPostCallback
---@param state System.Object
---@param waitHandle System.Threading.ManualResetEvent
---@return UnityEngine.UnitySynchronizationContext.WorkRequest
function UnityEngine.UnitySynchronizationContext.WorkRequest.New(callback, state, waitHandle) end
function UnityEngine.UnitySynchronizationContext.WorkRequest:Invoke() end

---@class UnityEngine.UserAuthorization
---@field WebCam UnityEngine.UserAuthorization
---@field Microphone UnityEngine.UserAuthorization
UnityEngine.UserAuthorization = {}
---@alias CS.UnityEngine.UserAuthorization UnityEngine.UserAuthorization
CS.UnityEngine.UserAuthorization = UnityEngine.UserAuthorization


---@class UnityEngine.Vector2 : System.ValueType
---@field kEpsilon number
---@field kEpsilonNormalSqrt number
---@field x number
---@field y number
---@field zero UnityEngine.Vector2
---@field one UnityEngine.Vector2
---@field up UnityEngine.Vector2
---@field down UnityEngine.Vector2
---@field left UnityEngine.Vector2
---@field right UnityEngine.Vector2
---@field positiveInfinity UnityEngine.Vector2
---@field negativeInfinity UnityEngine.Vector2
---@field Item number
---@field normalized UnityEngine.Vector2
---@field magnitude number
---@field sqrMagnitude number
UnityEngine.Vector2 = {}
---@alias CS.UnityEngine.Vector2 UnityEngine.Vector2
CS.UnityEngine.Vector2 = UnityEngine.Vector2

---@param x number
---@param y number
---@return UnityEngine.Vector2
function UnityEngine.Vector2.New(x, y) end
---@param a UnityEngine.Vector2
---@param b UnityEngine.Vector2
---@param t number
---@return UnityEngine.Vector2
function UnityEngine.Vector2.Lerp(a, b, t) end
---@param a UnityEngine.Vector2
---@param b UnityEngine.Vector2
---@param t number
---@return UnityEngine.Vector2
function UnityEngine.Vector2.LerpUnclamped(a, b, t) end
---@param current UnityEngine.Vector2
---@param target UnityEngine.Vector2
---@param maxDistanceDelta number
---@return UnityEngine.Vector2
function UnityEngine.Vector2.MoveTowards(current, target, maxDistanceDelta) end
---@overload fun(a: UnityEngine.Vector2, b: UnityEngine.Vector2) : UnityEngine.Vector2
---@param scale UnityEngine.Vector2
function UnityEngine.Vector2:Scale(scale) end
---@param inDirection UnityEngine.Vector2
---@param inNormal UnityEngine.Vector2
---@return UnityEngine.Vector2
function UnityEngine.Vector2.Reflect(inDirection, inNormal) end
---@param inDirection UnityEngine.Vector2
---@return UnityEngine.Vector2
function UnityEngine.Vector2.Perpendicular(inDirection) end
---@param lhs UnityEngine.Vector2
---@param rhs UnityEngine.Vector2
---@return number
function UnityEngine.Vector2.Dot(lhs, rhs) end
---@param from UnityEngine.Vector2
---@param to UnityEngine.Vector2
---@return number
function UnityEngine.Vector2.Angle(from, to) end
---@param from UnityEngine.Vector2
---@param to UnityEngine.Vector2
---@return number
function UnityEngine.Vector2.SignedAngle(from, to) end
---@param a UnityEngine.Vector2
---@param b UnityEngine.Vector2
---@return number
function UnityEngine.Vector2.Distance(a, b) end
---@param vector UnityEngine.Vector2
---@param maxLength number
---@return UnityEngine.Vector2
function UnityEngine.Vector2.ClampMagnitude(vector, maxLength) end
---@overload fun(a: UnityEngine.Vector2) : number
---@return number
function UnityEngine.Vector2:SqrMagnitude() end
---@param lhs UnityEngine.Vector2
---@param rhs UnityEngine.Vector2
---@return UnityEngine.Vector2
function UnityEngine.Vector2.Min(lhs, rhs) end
---@param lhs UnityEngine.Vector2
---@param rhs UnityEngine.Vector2
---@return UnityEngine.Vector2
function UnityEngine.Vector2.Max(lhs, rhs) end
---@overload fun(current: UnityEngine.Vector2, target: UnityEngine.Vector2, ref_currentVelocity: UnityEngine.Vector2, smoothTime: number, maxSpeed: number) : UnityEngine.Vector2, UnityEngine.Vector2
---@overload fun(current: UnityEngine.Vector2, target: UnityEngine.Vector2, ref_currentVelocity: UnityEngine.Vector2, smoothTime: number) : UnityEngine.Vector2, UnityEngine.Vector2
---@param current UnityEngine.Vector2
---@param target UnityEngine.Vector2
---@param ref_currentVelocity UnityEngine.Vector2
---@param smoothTime number
---@param maxSpeed number
---@param deltaTime number
---@return UnityEngine.Vector2, UnityEngine.Vector2
function UnityEngine.Vector2.SmoothDamp(current, target, ref_currentVelocity, smoothTime, maxSpeed, deltaTime) end
---@param newX number
---@param newY number
function UnityEngine.Vector2:Set(newX, newY) end
function UnityEngine.Vector2:Normalize() end
---@overload fun(self: UnityEngine.Vector2) : string
---@overload fun(self: UnityEngine.Vector2, format: string) : string
---@param format string
---@param formatProvider System.IFormatProvider
---@return string
function UnityEngine.Vector2:ToString(format, formatProvider) end
---@return number
function UnityEngine.Vector2:GetHashCode() end
---@overload fun(self: UnityEngine.Vector2, other: System.Object) : boolean
---@param other UnityEngine.Vector2
---@return boolean
function UnityEngine.Vector2:Equals(other) end

---@class UnityEngine.Vector2Int : System.ValueType
---@field zero UnityEngine.Vector2Int
---@field one UnityEngine.Vector2Int
---@field up UnityEngine.Vector2Int
---@field down UnityEngine.Vector2Int
---@field left UnityEngine.Vector2Int
---@field right UnityEngine.Vector2Int
---@field x number
---@field y number
---@field Item number
---@field magnitude number
---@field sqrMagnitude number
UnityEngine.Vector2Int = {}
---@alias CS.UnityEngine.Vector2Int UnityEngine.Vector2Int
CS.UnityEngine.Vector2Int = UnityEngine.Vector2Int

---@param x number
---@param y number
---@return UnityEngine.Vector2Int
function UnityEngine.Vector2Int.New(x, y) end
---@param a UnityEngine.Vector2Int
---@param b UnityEngine.Vector2Int
---@return number
function UnityEngine.Vector2Int.Distance(a, b) end
---@param lhs UnityEngine.Vector2Int
---@param rhs UnityEngine.Vector2Int
---@return UnityEngine.Vector2Int
function UnityEngine.Vector2Int.Min(lhs, rhs) end
---@param lhs UnityEngine.Vector2Int
---@param rhs UnityEngine.Vector2Int
---@return UnityEngine.Vector2Int
function UnityEngine.Vector2Int.Max(lhs, rhs) end
---@overload fun(a: UnityEngine.Vector2Int, b: UnityEngine.Vector2Int) : UnityEngine.Vector2Int
---@param scale UnityEngine.Vector2Int
function UnityEngine.Vector2Int:Scale(scale) end
---@param v UnityEngine.Vector2
---@return UnityEngine.Vector2Int
function UnityEngine.Vector2Int.FloorToInt(v) end
---@param v UnityEngine.Vector2
---@return UnityEngine.Vector2Int
function UnityEngine.Vector2Int.CeilToInt(v) end
---@param v UnityEngine.Vector2
---@return UnityEngine.Vector2Int
function UnityEngine.Vector2Int.RoundToInt(v) end
---@param x number
---@param y number
function UnityEngine.Vector2Int:Set(x, y) end
---@param min UnityEngine.Vector2Int
---@param max UnityEngine.Vector2Int
function UnityEngine.Vector2Int:Clamp(min, max) end
---@overload fun(self: UnityEngine.Vector2Int, other: System.Object) : boolean
---@param other UnityEngine.Vector2Int
---@return boolean
function UnityEngine.Vector2Int:Equals(other) end
---@return number
function UnityEngine.Vector2Int:GetHashCode() end
---@overload fun(self: UnityEngine.Vector2Int) : string
---@overload fun(self: UnityEngine.Vector2Int, format: string) : string
---@param format string
---@param formatProvider System.IFormatProvider
---@return string
function UnityEngine.Vector2Int:ToString(format, formatProvider) end

---@class UnityEngine.Vector3 : System.ValueType
---@field kEpsilon number
---@field kEpsilonNormalSqrt number
---@field x number
---@field y number
---@field z number
---@field zero UnityEngine.Vector3
---@field one UnityEngine.Vector3
---@field forward UnityEngine.Vector3
---@field back UnityEngine.Vector3
---@field up UnityEngine.Vector3
---@field down UnityEngine.Vector3
---@field left UnityEngine.Vector3
---@field right UnityEngine.Vector3
---@field positiveInfinity UnityEngine.Vector3
---@field negativeInfinity UnityEngine.Vector3
---@field Item number
---@field normalized UnityEngine.Vector3
---@field magnitude number
---@field sqrMagnitude number
UnityEngine.Vector3 = {}
---@alias CS.UnityEngine.Vector3 UnityEngine.Vector3
CS.UnityEngine.Vector3 = UnityEngine.Vector3

---@overload fun(x: number, y: number, z: number) : UnityEngine.Vector3
---@param x number
---@param y number
---@return UnityEngine.Vector3
function UnityEngine.Vector3.New(x, y) end
---@param a UnityEngine.Vector3
---@param b UnityEngine.Vector3
---@param t number
---@return UnityEngine.Vector3
function UnityEngine.Vector3.Slerp(a, b, t) end
---@param a UnityEngine.Vector3
---@param b UnityEngine.Vector3
---@param t number
---@return UnityEngine.Vector3
function UnityEngine.Vector3.SlerpUnclamped(a, b, t) end
---@overload fun(ref_normal: UnityEngine.Vector3, ref_tangent: UnityEngine.Vector3) : UnityEngine.Vector3, UnityEngine.Vector3
---@param ref_normal UnityEngine.Vector3
---@param ref_tangent UnityEngine.Vector3
---@param ref_binormal UnityEngine.Vector3
---@return UnityEngine.Vector3, UnityEngine.Vector3, UnityEngine.Vector3
function UnityEngine.Vector3.OrthoNormalize(ref_normal, ref_tangent, ref_binormal) end
---@param current UnityEngine.Vector3
---@param target UnityEngine.Vector3
---@param maxRadiansDelta number
---@param maxMagnitudeDelta number
---@return UnityEngine.Vector3
function UnityEngine.Vector3.RotateTowards(current, target, maxRadiansDelta, maxMagnitudeDelta) end
---@param a UnityEngine.Vector3
---@param b UnityEngine.Vector3
---@param t number
---@return UnityEngine.Vector3
function UnityEngine.Vector3.Lerp(a, b, t) end
---@param a UnityEngine.Vector3
---@param b UnityEngine.Vector3
---@param t number
---@return UnityEngine.Vector3
function UnityEngine.Vector3.LerpUnclamped(a, b, t) end
---@param current UnityEngine.Vector3
---@param target UnityEngine.Vector3
---@param maxDistanceDelta number
---@return UnityEngine.Vector3
function UnityEngine.Vector3.MoveTowards(current, target, maxDistanceDelta) end
---@overload fun(current: UnityEngine.Vector3, target: UnityEngine.Vector3, ref_currentVelocity: UnityEngine.Vector3, smoothTime: number, maxSpeed: number) : UnityEngine.Vector3, UnityEngine.Vector3
---@overload fun(current: UnityEngine.Vector3, target: UnityEngine.Vector3, ref_currentVelocity: UnityEngine.Vector3, smoothTime: number) : UnityEngine.Vector3, UnityEngine.Vector3
---@param current UnityEngine.Vector3
---@param target UnityEngine.Vector3
---@param ref_currentVelocity UnityEngine.Vector3
---@param smoothTime number
---@param maxSpeed number
---@param deltaTime number
---@return UnityEngine.Vector3, UnityEngine.Vector3
function UnityEngine.Vector3.SmoothDamp(current, target, ref_currentVelocity, smoothTime, maxSpeed, deltaTime) end
---@overload fun(a: UnityEngine.Vector3, b: UnityEngine.Vector3) : UnityEngine.Vector3
---@param scale UnityEngine.Vector3
function UnityEngine.Vector3:Scale(scale) end
---@param lhs UnityEngine.Vector3
---@param rhs UnityEngine.Vector3
---@return UnityEngine.Vector3
function UnityEngine.Vector3.Cross(lhs, rhs) end
---@param inDirection UnityEngine.Vector3
---@param inNormal UnityEngine.Vector3
---@return UnityEngine.Vector3
function UnityEngine.Vector3.Reflect(inDirection, inNormal) end
---@overload fun(value: UnityEngine.Vector3) : UnityEngine.Vector3
function UnityEngine.Vector3:Normalize() end
---@param lhs UnityEngine.Vector3
---@param rhs UnityEngine.Vector3
---@return number
function UnityEngine.Vector3.Dot(lhs, rhs) end
---@param vector UnityEngine.Vector3
---@param onNormal UnityEngine.Vector3
---@return UnityEngine.Vector3
function UnityEngine.Vector3.Project(vector, onNormal) end
---@param vector UnityEngine.Vector3
---@param planeNormal UnityEngine.Vector3
---@return UnityEngine.Vector3
function UnityEngine.Vector3.ProjectOnPlane(vector, planeNormal) end
---@param from UnityEngine.Vector3
---@param to UnityEngine.Vector3
---@return number
function UnityEngine.Vector3.Angle(from, to) end
---@param from UnityEngine.Vector3
---@param to UnityEngine.Vector3
---@param axis UnityEngine.Vector3
---@return number
function UnityEngine.Vector3.SignedAngle(from, to, axis) end
---@param a UnityEngine.Vector3
---@param b UnityEngine.Vector3
---@return number
function UnityEngine.Vector3.Distance(a, b) end
---@param vector UnityEngine.Vector3
---@param maxLength number
---@return UnityEngine.Vector3
function UnityEngine.Vector3.ClampMagnitude(vector, maxLength) end
---@param vector UnityEngine.Vector3
---@return number
function UnityEngine.Vector3.Magnitude(vector) end
---@param vector UnityEngine.Vector3
---@return number
function UnityEngine.Vector3.SqrMagnitude(vector) end
---@param lhs UnityEngine.Vector3
---@param rhs UnityEngine.Vector3
---@return UnityEngine.Vector3
function UnityEngine.Vector3.Min(lhs, rhs) end
---@param lhs UnityEngine.Vector3
---@param rhs UnityEngine.Vector3
---@return UnityEngine.Vector3
function UnityEngine.Vector3.Max(lhs, rhs) end
---@param newX number
---@param newY number
---@param newZ number
function UnityEngine.Vector3:Set(newX, newY, newZ) end
---@return number
function UnityEngine.Vector3:GetHashCode() end
---@overload fun(self: UnityEngine.Vector3, other: System.Object) : boolean
---@param other UnityEngine.Vector3
---@return boolean
function UnityEngine.Vector3:Equals(other) end
---@overload fun(self: UnityEngine.Vector3) : string
---@overload fun(self: UnityEngine.Vector3, format: string) : string
---@param format string
---@param formatProvider System.IFormatProvider
---@return string
function UnityEngine.Vector3:ToString(format, formatProvider) end

---@class UnityEngine.Vector3Int : System.ValueType
---@field zero UnityEngine.Vector3Int
---@field one UnityEngine.Vector3Int
---@field up UnityEngine.Vector3Int
---@field down UnityEngine.Vector3Int
---@field left UnityEngine.Vector3Int
---@field right UnityEngine.Vector3Int
---@field forward UnityEngine.Vector3Int
---@field back UnityEngine.Vector3Int
---@field x number
---@field y number
---@field z number
---@field Item number
---@field magnitude number
---@field sqrMagnitude number
UnityEngine.Vector3Int = {}
---@alias CS.UnityEngine.Vector3Int UnityEngine.Vector3Int
CS.UnityEngine.Vector3Int = UnityEngine.Vector3Int

---@overload fun(x: number, y: number) : UnityEngine.Vector3Int
---@param x number
---@param y number
---@param z number
---@return UnityEngine.Vector3Int
function UnityEngine.Vector3Int.New(x, y, z) end
---@param a UnityEngine.Vector3Int
---@param b UnityEngine.Vector3Int
---@return number
function UnityEngine.Vector3Int.Distance(a, b) end
---@param lhs UnityEngine.Vector3Int
---@param rhs UnityEngine.Vector3Int
---@return UnityEngine.Vector3Int
function UnityEngine.Vector3Int.Min(lhs, rhs) end
---@param lhs UnityEngine.Vector3Int
---@param rhs UnityEngine.Vector3Int
---@return UnityEngine.Vector3Int
function UnityEngine.Vector3Int.Max(lhs, rhs) end
---@overload fun(a: UnityEngine.Vector3Int, b: UnityEngine.Vector3Int) : UnityEngine.Vector3Int
---@param scale UnityEngine.Vector3Int
function UnityEngine.Vector3Int:Scale(scale) end
---@param v UnityEngine.Vector3
---@return UnityEngine.Vector3Int
function UnityEngine.Vector3Int.FloorToInt(v) end
---@param v UnityEngine.Vector3
---@return UnityEngine.Vector3Int
function UnityEngine.Vector3Int.CeilToInt(v) end
---@param v UnityEngine.Vector3
---@return UnityEngine.Vector3Int
function UnityEngine.Vector3Int.RoundToInt(v) end
---@param x number
---@param y number
---@param z number
function UnityEngine.Vector3Int:Set(x, y, z) end
---@param min UnityEngine.Vector3Int
---@param max UnityEngine.Vector3Int
function UnityEngine.Vector3Int:Clamp(min, max) end
---@overload fun(self: UnityEngine.Vector3Int, other: System.Object) : boolean
---@param other UnityEngine.Vector3Int
---@return boolean
function UnityEngine.Vector3Int:Equals(other) end
---@return number
function UnityEngine.Vector3Int:GetHashCode() end
---@overload fun(self: UnityEngine.Vector3Int) : string
---@overload fun(self: UnityEngine.Vector3Int, format: string) : string
---@param format string
---@param formatProvider System.IFormatProvider
---@return string
function UnityEngine.Vector3Int:ToString(format, formatProvider) end

---@class UnityEngine.Vector4 : System.ValueType
---@field kEpsilon number
---@field x number
---@field y number
---@field z number
---@field w number
---@field zero UnityEngine.Vector4
---@field one UnityEngine.Vector4
---@field positiveInfinity UnityEngine.Vector4
---@field negativeInfinity UnityEngine.Vector4
---@field Item number
---@field normalized UnityEngine.Vector4
---@field magnitude number
---@field sqrMagnitude number
UnityEngine.Vector4 = {}
---@alias CS.UnityEngine.Vector4 UnityEngine.Vector4
CS.UnityEngine.Vector4 = UnityEngine.Vector4

---@overload fun(x: number, y: number, z: number, w: number) : UnityEngine.Vector4
---@overload fun(x: number, y: number, z: number) : UnityEngine.Vector4
---@param x number
---@param y number
---@return UnityEngine.Vector4
function UnityEngine.Vector4.New(x, y) end
---@param a UnityEngine.Vector4
---@param b UnityEngine.Vector4
---@param t number
---@return UnityEngine.Vector4
function UnityEngine.Vector4.Lerp(a, b, t) end
---@param a UnityEngine.Vector4
---@param b UnityEngine.Vector4
---@param t number
---@return UnityEngine.Vector4
function UnityEngine.Vector4.LerpUnclamped(a, b, t) end
---@param current UnityEngine.Vector4
---@param target UnityEngine.Vector4
---@param maxDistanceDelta number
---@return UnityEngine.Vector4
function UnityEngine.Vector4.MoveTowards(current, target, maxDistanceDelta) end
---@overload fun(a: UnityEngine.Vector4, b: UnityEngine.Vector4) : UnityEngine.Vector4
---@param scale UnityEngine.Vector4
function UnityEngine.Vector4:Scale(scale) end
---@overload fun(a: UnityEngine.Vector4) : UnityEngine.Vector4
function UnityEngine.Vector4:Normalize() end
---@param a UnityEngine.Vector4
---@param b UnityEngine.Vector4
---@return number
function UnityEngine.Vector4.Dot(a, b) end
---@param a UnityEngine.Vector4
---@param b UnityEngine.Vector4
---@return UnityEngine.Vector4
function UnityEngine.Vector4.Project(a, b) end
---@param a UnityEngine.Vector4
---@param b UnityEngine.Vector4
---@return number
function UnityEngine.Vector4.Distance(a, b) end
---@param a UnityEngine.Vector4
---@return number
function UnityEngine.Vector4.Magnitude(a) end
---@param lhs UnityEngine.Vector4
---@param rhs UnityEngine.Vector4
---@return UnityEngine.Vector4
function UnityEngine.Vector4.Min(lhs, rhs) end
---@param lhs UnityEngine.Vector4
---@param rhs UnityEngine.Vector4
---@return UnityEngine.Vector4
function UnityEngine.Vector4.Max(lhs, rhs) end
---@overload fun(a: UnityEngine.Vector4) : number
---@return number
function UnityEngine.Vector4:SqrMagnitude() end
---@param newX number
---@param newY number
---@param newZ number
---@param newW number
function UnityEngine.Vector4:Set(newX, newY, newZ, newW) end
---@return number
function UnityEngine.Vector4:GetHashCode() end
---@overload fun(self: UnityEngine.Vector4, other: System.Object) : boolean
---@param other UnityEngine.Vector4
---@return boolean
function UnityEngine.Vector4:Equals(other) end
---@overload fun(self: UnityEngine.Vector4) : string
---@overload fun(self: UnityEngine.Vector4, format: string) : string
---@param format string
---@param formatProvider System.IFormatProvider
---@return string
function UnityEngine.Vector4:ToString(format, formatProvider) end

---@class UnityEngine.VerticalWrapMode
---@field Truncate UnityEngine.VerticalWrapMode
---@field Overflow UnityEngine.VerticalWrapMode
UnityEngine.VerticalWrapMode = {}
---@alias CS.UnityEngine.VerticalWrapMode UnityEngine.VerticalWrapMode
CS.UnityEngine.VerticalWrapMode = UnityEngine.VerticalWrapMode


---@class UnityEngine.VFX.VFXBatchedEffectInfo : System.ValueType
---@field vfxAsset UnityEngine.VFX.VisualEffectAsset
---@field activeBatchCount number
---@field inactiveBatchCount number
---@field activeInstanceCount number
---@field unbatchedInstanceCount number
---@field totalInstanceCapacity number
---@field maxInstancePerBatchCapacity number
---@field totalGPUSizeInBytes number
---@field totalCPUSizeInBytes number
UnityEngine.VFX.VFXBatchedEffectInfo = {}
---@alias CS.UnityEngine.VFX.VFXBatchedEffectInfo UnityEngine.VFX.VFXBatchedEffectInfo
CS.UnityEngine.VFX.VFXBatchedEffectInfo = UnityEngine.VFX.VFXBatchedEffectInfo


---@class UnityEngine.VFX.VFXBatchInfo : System.ValueType
---@field capacity number
---@field activeInstanceCount number
UnityEngine.VFX.VFXBatchInfo = {}
---@alias CS.UnityEngine.VFX.VFXBatchInfo UnityEngine.VFX.VFXBatchInfo
CS.UnityEngine.VFX.VFXBatchInfo = UnityEngine.VFX.VFXBatchInfo


---@class UnityEngine.VFX.VFXCameraBufferTypes
---@field None UnityEngine.VFX.VFXCameraBufferTypes
---@field Depth UnityEngine.VFX.VFXCameraBufferTypes
---@field Color UnityEngine.VFX.VFXCameraBufferTypes
---@field Normal UnityEngine.VFX.VFXCameraBufferTypes
UnityEngine.VFX.VFXCameraBufferTypes = {}
---@alias CS.UnityEngine.VFX.VFXCameraBufferTypes UnityEngine.VFX.VFXCameraBufferTypes
CS.UnityEngine.VFX.VFXCameraBufferTypes = UnityEngine.VFX.VFXCameraBufferTypes


---@class UnityEngine.VFX.VFXCameraXRSettings : System.ValueType
---@field viewTotal number
---@field viewCount number
---@field viewOffset number
UnityEngine.VFX.VFXCameraXRSettings = {}
---@alias CS.UnityEngine.VFX.VFXCameraXRSettings UnityEngine.VFX.VFXCameraXRSettings
CS.UnityEngine.VFX.VFXCameraXRSettings = UnityEngine.VFX.VFXCameraXRSettings


---@class UnityEngine.VFX.VFXCullingFlags
---@field CullNone UnityEngine.VFX.VFXCullingFlags
---@field CullSimulation UnityEngine.VFX.VFXCullingFlags
---@field CullBoundsUpdate UnityEngine.VFX.VFXCullingFlags
---@field CullDefault UnityEngine.VFX.VFXCullingFlags
UnityEngine.VFX.VFXCullingFlags = {}
---@alias CS.UnityEngine.VFX.VFXCullingFlags UnityEngine.VFX.VFXCullingFlags
CS.UnityEngine.VFX.VFXCullingFlags = UnityEngine.VFX.VFXCullingFlags


---@class UnityEngine.VFX.VFXEventAttribute : System.Object
UnityEngine.VFX.VFXEventAttribute = {}
---@alias CS.UnityEngine.VFX.VFXEventAttribute UnityEngine.VFX.VFXEventAttribute
CS.UnityEngine.VFX.VFXEventAttribute = UnityEngine.VFX.VFXEventAttribute

---@param original UnityEngine.VFX.VFXEventAttribute
---@return UnityEngine.VFX.VFXEventAttribute
function UnityEngine.VFX.VFXEventAttribute.New(original) end
function UnityEngine.VFX.VFXEventAttribute:Dispose() end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXEventAttribute:HasBool(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXEventAttribute:HasInt(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXEventAttribute:HasUint(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXEventAttribute:HasFloat(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXEventAttribute:HasVector2(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXEventAttribute:HasVector3(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXEventAttribute:HasVector4(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXEventAttribute:HasMatrix4x4(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number, b: boolean)
---@param name string
---@param b boolean
function UnityEngine.VFX.VFXEventAttribute:SetBool(name, b) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number, i: number)
---@param name string
---@param i number
function UnityEngine.VFX.VFXEventAttribute:SetInt(name, i) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number, i: number)
---@param name string
---@param i number
function UnityEngine.VFX.VFXEventAttribute:SetUint(name, i) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number, f: number)
---@param name string
---@param f number
function UnityEngine.VFX.VFXEventAttribute:SetFloat(name, f) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number, v: UnityEngine.Vector2)
---@param name string
---@param v UnityEngine.Vector2
function UnityEngine.VFX.VFXEventAttribute:SetVector2(name, v) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number, v: UnityEngine.Vector3)
---@param name string
---@param v UnityEngine.Vector3
function UnityEngine.VFX.VFXEventAttribute:SetVector3(name, v) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number, v: UnityEngine.Vector4)
---@param name string
---@param v UnityEngine.Vector4
function UnityEngine.VFX.VFXEventAttribute:SetVector4(name, v) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number, v: UnityEngine.Matrix4x4)
---@param name string
---@param v UnityEngine.Matrix4x4
function UnityEngine.VFX.VFXEventAttribute:SetMatrix4x4(name, v) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXEventAttribute:GetBool(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : number
---@param name string
---@return number
function UnityEngine.VFX.VFXEventAttribute:GetInt(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : number
---@param name string
---@return number
function UnityEngine.VFX.VFXEventAttribute:GetUint(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : number
---@param name string
---@return number
function UnityEngine.VFX.VFXEventAttribute:GetFloat(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : UnityEngine.Vector2
---@param name string
---@return UnityEngine.Vector2
function UnityEngine.VFX.VFXEventAttribute:GetVector2(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : UnityEngine.Vector3
---@param name string
---@return UnityEngine.Vector3
function UnityEngine.VFX.VFXEventAttribute:GetVector3(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : UnityEngine.Vector4
---@param name string
---@return UnityEngine.Vector4
function UnityEngine.VFX.VFXEventAttribute:GetVector4(name) end
---@overload fun(self: UnityEngine.VFX.VFXEventAttribute, nameID: number) : UnityEngine.Matrix4x4
---@param name string
---@return UnityEngine.Matrix4x4
function UnityEngine.VFX.VFXEventAttribute:GetMatrix4x4(name) end
---@param eventAttibute UnityEngine.VFX.VFXEventAttribute
function UnityEngine.VFX.VFXEventAttribute:CopyValuesFrom(eventAttibute) end

---@class UnityEngine.VFX.VFXExposedProperty : System.ValueType
---@field name string
---@field type System.Type
UnityEngine.VFX.VFXExposedProperty = {}
---@alias CS.UnityEngine.VFX.VFXExposedProperty UnityEngine.VFX.VFXExposedProperty
CS.UnityEngine.VFX.VFXExposedProperty = UnityEngine.VFX.VFXExposedProperty


---@class UnityEngine.VFX.VFXExpressionOperation
---@field None UnityEngine.VFX.VFXExpressionOperation
---@field Value UnityEngine.VFX.VFXExpressionOperation
---@field Combine2f UnityEngine.VFX.VFXExpressionOperation
---@field Combine3f UnityEngine.VFX.VFXExpressionOperation
---@field Combine4f UnityEngine.VFX.VFXExpressionOperation
---@field ExtractComponent UnityEngine.VFX.VFXExpressionOperation
---@field DeltaTime UnityEngine.VFX.VFXExpressionOperation
---@field TotalTime UnityEngine.VFX.VFXExpressionOperation
---@field SystemSeed UnityEngine.VFX.VFXExpressionOperation
---@field LocalToWorld UnityEngine.VFX.VFXExpressionOperation
---@field WorldToLocal UnityEngine.VFX.VFXExpressionOperation
---@field FrameIndex UnityEngine.VFX.VFXExpressionOperation
---@field PlayRate UnityEngine.VFX.VFXExpressionOperation
---@field UnscaledDeltaTime UnityEngine.VFX.VFXExpressionOperation
---@field ManagerMaxDeltaTime UnityEngine.VFX.VFXExpressionOperation
---@field ManagerFixedTimeStep UnityEngine.VFX.VFXExpressionOperation
---@field GameDeltaTime UnityEngine.VFX.VFXExpressionOperation
---@field GameUnscaledDeltaTime UnityEngine.VFX.VFXExpressionOperation
---@field GameSmoothDeltaTime UnityEngine.VFX.VFXExpressionOperation
---@field GameTotalTime UnityEngine.VFX.VFXExpressionOperation
---@field GameUnscaledTotalTime UnityEngine.VFX.VFXExpressionOperation
---@field GameTotalTimeSinceSceneLoad UnityEngine.VFX.VFXExpressionOperation
---@field GameTimeScale UnityEngine.VFX.VFXExpressionOperation
---@field Sin UnityEngine.VFX.VFXExpressionOperation
---@field Cos UnityEngine.VFX.VFXExpressionOperation
---@field Tan UnityEngine.VFX.VFXExpressionOperation
---@field ASin UnityEngine.VFX.VFXExpressionOperation
---@field ACos UnityEngine.VFX.VFXExpressionOperation
---@field ATan UnityEngine.VFX.VFXExpressionOperation
---@field Abs UnityEngine.VFX.VFXExpressionOperation
---@field Sign UnityEngine.VFX.VFXExpressionOperation
---@field Saturate UnityEngine.VFX.VFXExpressionOperation
---@field Ceil UnityEngine.VFX.VFXExpressionOperation
---@field Round UnityEngine.VFX.VFXExpressionOperation
---@field Frac UnityEngine.VFX.VFXExpressionOperation
---@field Floor UnityEngine.VFX.VFXExpressionOperation
---@field Log2 UnityEngine.VFX.VFXExpressionOperation
---@field Mul UnityEngine.VFX.VFXExpressionOperation
---@field Divide UnityEngine.VFX.VFXExpressionOperation
---@field Add UnityEngine.VFX.VFXExpressionOperation
---@field Subtract UnityEngine.VFX.VFXExpressionOperation
---@field Min UnityEngine.VFX.VFXExpressionOperation
---@field Max UnityEngine.VFX.VFXExpressionOperation
---@field Pow UnityEngine.VFX.VFXExpressionOperation
---@field ATan2 UnityEngine.VFX.VFXExpressionOperation
---@field TRSToMatrix UnityEngine.VFX.VFXExpressionOperation
---@field InverseMatrix UnityEngine.VFX.VFXExpressionOperation
---@field InverseTRSMatrix UnityEngine.VFX.VFXExpressionOperation
---@field TransposeMatrix UnityEngine.VFX.VFXExpressionOperation
---@field ExtractPositionFromMatrix UnityEngine.VFX.VFXExpressionOperation
---@field ExtractAnglesFromMatrix UnityEngine.VFX.VFXExpressionOperation
---@field ExtractScaleFromMatrix UnityEngine.VFX.VFXExpressionOperation
---@field TransformMatrix UnityEngine.VFX.VFXExpressionOperation
---@field TransformPos UnityEngine.VFX.VFXExpressionOperation
---@field TransformVec UnityEngine.VFX.VFXExpressionOperation
---@field TransformDir UnityEngine.VFX.VFXExpressionOperation
---@field TransformVector4 UnityEngine.VFX.VFXExpressionOperation
---@field Vector3sToMatrix UnityEngine.VFX.VFXExpressionOperation
---@field Vector4sToMatrix UnityEngine.VFX.VFXExpressionOperation
---@field MatrixToVector3s UnityEngine.VFX.VFXExpressionOperation
---@field MatrixToVector4s UnityEngine.VFX.VFXExpressionOperation
---@field SampleCurve UnityEngine.VFX.VFXExpressionOperation
---@field SampleGradient UnityEngine.VFX.VFXExpressionOperation
---@field SampleMeshVertexFloat UnityEngine.VFX.VFXExpressionOperation
---@field SampleMeshVertexFloat2 UnityEngine.VFX.VFXExpressionOperation
---@field SampleMeshVertexFloat3 UnityEngine.VFX.VFXExpressionOperation
---@field SampleMeshVertexFloat4 UnityEngine.VFX.VFXExpressionOperation
---@field SampleMeshVertexColor UnityEngine.VFX.VFXExpressionOperation
---@field SampleMeshIndex UnityEngine.VFX.VFXExpressionOperation
---@field VertexBufferFromMesh UnityEngine.VFX.VFXExpressionOperation
---@field VertexBufferFromSkinnedMeshRenderer UnityEngine.VFX.VFXExpressionOperation
---@field IndexBufferFromMesh UnityEngine.VFX.VFXExpressionOperation
---@field MeshFromSkinnedMeshRenderer UnityEngine.VFX.VFXExpressionOperation
---@field RootBoneTransformFromSkinnedMeshRenderer UnityEngine.VFX.VFXExpressionOperation
---@field BakeCurve UnityEngine.VFX.VFXExpressionOperation
---@field BakeGradient UnityEngine.VFX.VFXExpressionOperation
---@field BitwiseLeftShift UnityEngine.VFX.VFXExpressionOperation
---@field BitwiseRightShift UnityEngine.VFX.VFXExpressionOperation
---@field BitwiseOr UnityEngine.VFX.VFXExpressionOperation
---@field BitwiseAnd UnityEngine.VFX.VFXExpressionOperation
---@field BitwiseXor UnityEngine.VFX.VFXExpressionOperation
---@field BitwiseComplement UnityEngine.VFX.VFXExpressionOperation
---@field CastUintToFloat UnityEngine.VFX.VFXExpressionOperation
---@field CastIntToFloat UnityEngine.VFX.VFXExpressionOperation
---@field CastFloatToUint UnityEngine.VFX.VFXExpressionOperation
---@field CastIntToUint UnityEngine.VFX.VFXExpressionOperation
---@field CastFloatToInt UnityEngine.VFX.VFXExpressionOperation
---@field CastUintToInt UnityEngine.VFX.VFXExpressionOperation
---@field CastIntToBool UnityEngine.VFX.VFXExpressionOperation
---@field CastUintToBool UnityEngine.VFX.VFXExpressionOperation
---@field CastFloatToBool UnityEngine.VFX.VFXExpressionOperation
---@field CastBoolToInt UnityEngine.VFX.VFXExpressionOperation
---@field CastBoolToUint UnityEngine.VFX.VFXExpressionOperation
---@field CastBoolToFloat UnityEngine.VFX.VFXExpressionOperation
---@field RGBtoHSV UnityEngine.VFX.VFXExpressionOperation
---@field HSVtoRGB UnityEngine.VFX.VFXExpressionOperation
---@field Condition UnityEngine.VFX.VFXExpressionOperation
---@field Branch UnityEngine.VFX.VFXExpressionOperation
---@field GenerateRandom UnityEngine.VFX.VFXExpressionOperation
---@field GenerateFixedRandom UnityEngine.VFX.VFXExpressionOperation
---@field ExtractMatrixFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field ExtractFOVFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field ExtractNearPlaneFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field ExtractFarPlaneFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field ExtractAspectRatioFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field ExtractPixelDimensionsFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field ExtractScaledPixelDimensionsFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field ExtractLensShiftFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field GetBufferFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field IsMainCameraOrthographic UnityEngine.VFX.VFXExpressionOperation
---@field GetOrthographicSizeFromMainCamera UnityEngine.VFX.VFXExpressionOperation
---@field LogicalAnd UnityEngine.VFX.VFXExpressionOperation
---@field LogicalOr UnityEngine.VFX.VFXExpressionOperation
---@field LogicalNot UnityEngine.VFX.VFXExpressionOperation
---@field ValueNoise1D UnityEngine.VFX.VFXExpressionOperation
---@field ValueNoise2D UnityEngine.VFX.VFXExpressionOperation
---@field ValueNoise3D UnityEngine.VFX.VFXExpressionOperation
---@field ValueCurlNoise2D UnityEngine.VFX.VFXExpressionOperation
---@field ValueCurlNoise3D UnityEngine.VFX.VFXExpressionOperation
---@field PerlinNoise1D UnityEngine.VFX.VFXExpressionOperation
---@field PerlinNoise2D UnityEngine.VFX.VFXExpressionOperation
---@field PerlinNoise3D UnityEngine.VFX.VFXExpressionOperation
---@field PerlinCurlNoise2D UnityEngine.VFX.VFXExpressionOperation
---@field PerlinCurlNoise3D UnityEngine.VFX.VFXExpressionOperation
---@field CellularNoise1D UnityEngine.VFX.VFXExpressionOperation
---@field CellularNoise2D UnityEngine.VFX.VFXExpressionOperation
---@field CellularNoise3D UnityEngine.VFX.VFXExpressionOperation
---@field CellularCurlNoise2D UnityEngine.VFX.VFXExpressionOperation
---@field CellularCurlNoise3D UnityEngine.VFX.VFXExpressionOperation
---@field VoroNoise2D UnityEngine.VFX.VFXExpressionOperation
---@field MeshVertexCount UnityEngine.VFX.VFXExpressionOperation
---@field MeshChannelOffset UnityEngine.VFX.VFXExpressionOperation
---@field MeshChannelInfos UnityEngine.VFX.VFXExpressionOperation
---@field MeshVertexStride UnityEngine.VFX.VFXExpressionOperation
---@field MeshIndexCount UnityEngine.VFX.VFXExpressionOperation
---@field MeshIndexFormat UnityEngine.VFX.VFXExpressionOperation
---@field BufferStride UnityEngine.VFX.VFXExpressionOperation
---@field BufferCount UnityEngine.VFX.VFXExpressionOperation
---@field TextureWidth UnityEngine.VFX.VFXExpressionOperation
---@field TextureHeight UnityEngine.VFX.VFXExpressionOperation
---@field TextureDepth UnityEngine.VFX.VFXExpressionOperation
---@field ReadEventAttribute UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateNewLoop UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateLoopState UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateSpawnCount UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateDeltaTime UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateTotalTime UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateDelayBeforeLoop UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateLoopDuration UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateDelayAfterLoop UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateLoopIndex UnityEngine.VFX.VFXExpressionOperation
---@field SpawnerStateLoopCount UnityEngine.VFX.VFXExpressionOperation
UnityEngine.VFX.VFXExpressionOperation = {}
---@alias CS.UnityEngine.VFX.VFXExpressionOperation UnityEngine.VFX.VFXExpressionOperation
CS.UnityEngine.VFX.VFXExpressionOperation = UnityEngine.VFX.VFXExpressionOperation


---@class UnityEngine.VFX.VFXExpressionValues : System.Object
UnityEngine.VFX.VFXExpressionValues = {}
---@alias CS.UnityEngine.VFX.VFXExpressionValues UnityEngine.VFX.VFXExpressionValues
CS.UnityEngine.VFX.VFXExpressionValues = UnityEngine.VFX.VFXExpressionValues

---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VFXExpressionValues:GetBool(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : number
---@param name string
---@return number
function UnityEngine.VFX.VFXExpressionValues:GetInt(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : number
---@param name string
---@return number
function UnityEngine.VFX.VFXExpressionValues:GetUInt(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : number
---@param name string
---@return number
function UnityEngine.VFX.VFXExpressionValues:GetFloat(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : UnityEngine.Vector2
---@param name string
---@return UnityEngine.Vector2
function UnityEngine.VFX.VFXExpressionValues:GetVector2(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : UnityEngine.Vector3
---@param name string
---@return UnityEngine.Vector3
function UnityEngine.VFX.VFXExpressionValues:GetVector3(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : UnityEngine.Vector4
---@param name string
---@return UnityEngine.Vector4
function UnityEngine.VFX.VFXExpressionValues:GetVector4(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : UnityEngine.Matrix4x4
---@param name string
---@return UnityEngine.Matrix4x4
function UnityEngine.VFX.VFXExpressionValues:GetMatrix4x4(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : UnityEngine.Texture
---@param name string
---@return UnityEngine.Texture
function UnityEngine.VFX.VFXExpressionValues:GetTexture(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : UnityEngine.Mesh
---@param name string
---@return UnityEngine.Mesh
function UnityEngine.VFX.VFXExpressionValues:GetMesh(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : UnityEngine.AnimationCurve
---@param name string
---@return UnityEngine.AnimationCurve
function UnityEngine.VFX.VFXExpressionValues:GetAnimationCurve(name) end
---@overload fun(self: UnityEngine.VFX.VFXExpressionValues, nameID: number) : UnityEngine.Gradient
---@param name string
---@return UnityEngine.Gradient
function UnityEngine.VFX.VFXExpressionValues:GetGradient(name) end

---@class UnityEngine.VFX.VFXInstancingDisabledReason
---@field None UnityEngine.VFX.VFXInstancingDisabledReason
---@field IndirectDraw UnityEngine.VFX.VFXInstancingDisabledReason
---@field OutputEvent UnityEngine.VFX.VFXInstancingDisabledReason
---@field GPUEvent UnityEngine.VFX.VFXInstancingDisabledReason
---@field AutomaticBounds UnityEngine.VFX.VFXInstancingDisabledReason
---@field MeshOutput UnityEngine.VFX.VFXInstancingDisabledReason
---@field ExposedObject UnityEngine.VFX.VFXInstancingDisabledReason
---@field Unknown UnityEngine.VFX.VFXInstancingDisabledReason
UnityEngine.VFX.VFXInstancingDisabledReason = {}
---@alias CS.UnityEngine.VFX.VFXInstancingDisabledReason UnityEngine.VFX.VFXInstancingDisabledReason
CS.UnityEngine.VFX.VFXInstancingDisabledReason = UnityEngine.VFX.VFXInstancingDisabledReason


---@class UnityEngine.VFX.VFXInstancingMode
---@field Disabled UnityEngine.VFX.VFXInstancingMode
---@field Auto UnityEngine.VFX.VFXInstancingMode
---@field Custom UnityEngine.VFX.VFXInstancingMode
UnityEngine.VFX.VFXInstancingMode = {}
---@alias CS.UnityEngine.VFX.VFXInstancingMode UnityEngine.VFX.VFXInstancingMode
CS.UnityEngine.VFX.VFXInstancingMode = UnityEngine.VFX.VFXInstancingMode


---@class UnityEngine.VFX.VFXMainCameraBufferFallback
---@field NoFallback UnityEngine.VFX.VFXMainCameraBufferFallback
---@field PreferMainCamera UnityEngine.VFX.VFXMainCameraBufferFallback
---@field PreferSceneCamera UnityEngine.VFX.VFXMainCameraBufferFallback
UnityEngine.VFX.VFXMainCameraBufferFallback = {}
---@alias CS.UnityEngine.VFX.VFXMainCameraBufferFallback UnityEngine.VFX.VFXMainCameraBufferFallback
CS.UnityEngine.VFX.VFXMainCameraBufferFallback = UnityEngine.VFX.VFXMainCameraBufferFallback


---@class UnityEngine.VFX.VFXManager : System.Object
---@field fixedTimeStep number
---@field maxDeltaTime number
---@field delayCullingFrames number
UnityEngine.VFX.VFXManager = {}
---@alias CS.UnityEngine.VFX.VFXManager UnityEngine.VFX.VFXManager
CS.UnityEngine.VFX.VFXManager = UnityEngine.VFX.VFXManager

---@return UnityEngine.VFX.VisualEffect[]
function UnityEngine.VFX.VFXManager.GetComponents() end
function UnityEngine.VFX.VFXManager.FlushEmptyBatches() end
---@param vfx UnityEngine.VFX.VisualEffectAsset
---@return UnityEngine.VFX.VFXBatchedEffectInfo
function UnityEngine.VFX.VFXManager.GetBatchedEffectInfo(vfx) end
---@param infos System.Collections.Generic.List
function UnityEngine.VFX.VFXManager.GetBatchedEffectInfos(infos) end
---@overload fun(cam: UnityEngine.Camera)
---@param cam UnityEngine.Camera
---@param camXRSettings UnityEngine.VFX.VFXCameraXRSettings
function UnityEngine.VFX.VFXManager.PrepareCamera(cam, camXRSettings) end
---@param cam UnityEngine.Camera
---@param cmd UnityEngine.Rendering.CommandBuffer
---@param camXRSettings UnityEngine.VFX.VFXCameraXRSettings
---@param results UnityEngine.Rendering.CullingResults
function UnityEngine.VFX.VFXManager.ProcessCameraCommand(cam, cmd, camXRSettings, results) end
---@param cam UnityEngine.Camera
---@return UnityEngine.VFX.VFXCameraBufferTypes
function UnityEngine.VFX.VFXManager.IsCameraBufferNeeded(cam) end
---@param cam UnityEngine.Camera
---@param type UnityEngine.VFX.VFXCameraBufferTypes
---@param buffer UnityEngine.Texture
---@param x number
---@param y number
---@param width number
---@param height number
function UnityEngine.VFX.VFXManager.SetCameraBuffer(cam, type, buffer, x, y, width, height) end

---@class UnityEngine.VFX.VFXOutputEventArgs : System.ValueType
---@field nameId number
---@field eventAttribute UnityEngine.VFX.VFXEventAttribute
UnityEngine.VFX.VFXOutputEventArgs = {}
---@alias CS.UnityEngine.VFX.VFXOutputEventArgs UnityEngine.VFX.VFXOutputEventArgs
CS.UnityEngine.VFX.VFXOutputEventArgs = UnityEngine.VFX.VFXOutputEventArgs

---@param nameId number
---@param eventAttribute UnityEngine.VFX.VFXEventAttribute
---@return UnityEngine.VFX.VFXOutputEventArgs
function UnityEngine.VFX.VFXOutputEventArgs.New(nameId, eventAttribute) end

---@class UnityEngine.VFX.VFXParticleSystemInfo : System.ValueType
---@field aliveCount number
---@field capacity number
---@field sleeping boolean
---@field bounds UnityEngine.Bounds
UnityEngine.VFX.VFXParticleSystemInfo = {}
---@alias CS.UnityEngine.VFX.VFXParticleSystemInfo UnityEngine.VFX.VFXParticleSystemInfo
CS.UnityEngine.VFX.VFXParticleSystemInfo = UnityEngine.VFX.VFXParticleSystemInfo

---@param aliveCount number
---@param capacity number
---@param sleeping boolean
---@param bounds UnityEngine.Bounds
---@return UnityEngine.VFX.VFXParticleSystemInfo
function UnityEngine.VFX.VFXParticleSystemInfo.New(aliveCount, capacity, sleeping, bounds) end

---@class UnityEngine.VFX.VFXRenderer : UnityEngine.Renderer
UnityEngine.VFX.VFXRenderer = {}
---@alias CS.UnityEngine.VFX.VFXRenderer UnityEngine.VFX.VFXRenderer
CS.UnityEngine.VFX.VFXRenderer = UnityEngine.VFX.VFXRenderer

---@return UnityEngine.VFX.VFXRenderer
function UnityEngine.VFX.VFXRenderer.New() end

---@class UnityEngine.VFX.VFXSkinnedMeshFrame
---@field Current UnityEngine.VFX.VFXSkinnedMeshFrame
---@field Previous UnityEngine.VFX.VFXSkinnedMeshFrame
UnityEngine.VFX.VFXSkinnedMeshFrame = {}
---@alias CS.UnityEngine.VFX.VFXSkinnedMeshFrame UnityEngine.VFX.VFXSkinnedMeshFrame
CS.UnityEngine.VFX.VFXSkinnedMeshFrame = UnityEngine.VFX.VFXSkinnedMeshFrame


---@class UnityEngine.VFX.VFXSkinnedTransform
---@field LocalRootBoneTransform UnityEngine.VFX.VFXSkinnedTransform
---@field WorldRootBoneTransform UnityEngine.VFX.VFXSkinnedTransform
UnityEngine.VFX.VFXSkinnedTransform = {}
---@alias CS.UnityEngine.VFX.VFXSkinnedTransform UnityEngine.VFX.VFXSkinnedTransform
CS.UnityEngine.VFX.VFXSkinnedTransform = UnityEngine.VFX.VFXSkinnedTransform


---@class UnityEngine.VFX.VFXSpawnerCallbacks : UnityEngine.ScriptableObject
UnityEngine.VFX.VFXSpawnerCallbacks = {}
---@alias CS.UnityEngine.VFX.VFXSpawnerCallbacks UnityEngine.VFX.VFXSpawnerCallbacks
CS.UnityEngine.VFX.VFXSpawnerCallbacks = UnityEngine.VFX.VFXSpawnerCallbacks

---@param state UnityEngine.VFX.VFXSpawnerState
---@param vfxValues UnityEngine.VFX.VFXExpressionValues
---@param vfxComponent UnityEngine.VFX.VisualEffect
function UnityEngine.VFX.VFXSpawnerCallbacks:OnPlay(state, vfxValues, vfxComponent) end
---@param state UnityEngine.VFX.VFXSpawnerState
---@param vfxValues UnityEngine.VFX.VFXExpressionValues
---@param vfxComponent UnityEngine.VFX.VisualEffect
function UnityEngine.VFX.VFXSpawnerCallbacks:OnUpdate(state, vfxValues, vfxComponent) end
---@param state UnityEngine.VFX.VFXSpawnerState
---@param vfxValues UnityEngine.VFX.VFXExpressionValues
---@param vfxComponent UnityEngine.VFX.VisualEffect
function UnityEngine.VFX.VFXSpawnerCallbacks:OnStop(state, vfxValues, vfxComponent) end

---@class UnityEngine.VFX.VFXSpawnerLoopState
---@field Finished UnityEngine.VFX.VFXSpawnerLoopState
---@field DelayingBeforeLoop UnityEngine.VFX.VFXSpawnerLoopState
---@field Looping UnityEngine.VFX.VFXSpawnerLoopState
---@field DelayingAfterLoop UnityEngine.VFX.VFXSpawnerLoopState
UnityEngine.VFX.VFXSpawnerLoopState = {}
---@alias CS.UnityEngine.VFX.VFXSpawnerLoopState UnityEngine.VFX.VFXSpawnerLoopState
CS.UnityEngine.VFX.VFXSpawnerLoopState = UnityEngine.VFX.VFXSpawnerLoopState


---@class UnityEngine.VFX.VFXSpawnerState : System.Object
---@field playing boolean
---@field newLoop boolean
---@field loopState UnityEngine.VFX.VFXSpawnerLoopState
---@field spawnCount number
---@field deltaTime number
---@field totalTime number
---@field delayBeforeLoop number
---@field loopDuration number
---@field delayAfterLoop number
---@field loopIndex number
---@field loopCount number
---@field vfxEventAttribute UnityEngine.VFX.VFXEventAttribute
UnityEngine.VFX.VFXSpawnerState = {}
---@alias CS.UnityEngine.VFX.VFXSpawnerState UnityEngine.VFX.VFXSpawnerState
CS.UnityEngine.VFX.VFXSpawnerState = UnityEngine.VFX.VFXSpawnerState

---@return UnityEngine.VFX.VFXSpawnerState
function UnityEngine.VFX.VFXSpawnerState.New() end
function UnityEngine.VFX.VFXSpawnerState:Dispose() end

---@class UnityEngine.VFX.VFXSystemFlag
---@field SystemDefault UnityEngine.VFX.VFXSystemFlag
---@field SystemHasKill UnityEngine.VFX.VFXSystemFlag
---@field SystemHasIndirectBuffer UnityEngine.VFX.VFXSystemFlag
---@field SystemReceivedEventGPU UnityEngine.VFX.VFXSystemFlag
---@field SystemHasStrips UnityEngine.VFX.VFXSystemFlag
---@field SystemNeedsComputeBounds UnityEngine.VFX.VFXSystemFlag
---@field SystemAutomaticBounds UnityEngine.VFX.VFXSystemFlag
---@field SystemInWorldSpace UnityEngine.VFX.VFXSystemFlag
---@field SystemHasDirectLink UnityEngine.VFX.VFXSystemFlag
---@field SystemHasAttributeBuffer UnityEngine.VFX.VFXSystemFlag
---@field SystemUsesInstancedRendering UnityEngine.VFX.VFXSystemFlag
UnityEngine.VFX.VFXSystemFlag = {}
---@alias CS.UnityEngine.VFX.VFXSystemFlag UnityEngine.VFX.VFXSystemFlag
CS.UnityEngine.VFX.VFXSystemFlag = UnityEngine.VFX.VFXSystemFlag


---@class UnityEngine.VFX.VFXSystemType
---@field Spawner UnityEngine.VFX.VFXSystemType
---@field Particle UnityEngine.VFX.VFXSystemType
---@field Mesh UnityEngine.VFX.VFXSystemType
---@field OutputEvent UnityEngine.VFX.VFXSystemType
UnityEngine.VFX.VFXSystemType = {}
---@alias CS.UnityEngine.VFX.VFXSystemType UnityEngine.VFX.VFXSystemType
CS.UnityEngine.VFX.VFXSystemType = UnityEngine.VFX.VFXSystemType


---@class UnityEngine.VFX.VFXTaskType
---@field None UnityEngine.VFX.VFXTaskType
---@field Spawner UnityEngine.VFX.VFXTaskType
---@field Initialize UnityEngine.VFX.VFXTaskType
---@field Update UnityEngine.VFX.VFXTaskType
---@field Output UnityEngine.VFX.VFXTaskType
---@field CameraSort UnityEngine.VFX.VFXTaskType
---@field PerCameraUpdate UnityEngine.VFX.VFXTaskType
---@field PerCameraSort UnityEngine.VFX.VFXTaskType
---@field PerOutputSort UnityEngine.VFX.VFXTaskType
---@field GlobalSort UnityEngine.VFX.VFXTaskType
---@field ParticlePointOutput UnityEngine.VFX.VFXTaskType
---@field ParticleLineOutput UnityEngine.VFX.VFXTaskType
---@field ParticleQuadOutput UnityEngine.VFX.VFXTaskType
---@field ParticleHexahedronOutput UnityEngine.VFX.VFXTaskType
---@field ParticleMeshOutput UnityEngine.VFX.VFXTaskType
---@field ParticleTriangleOutput UnityEngine.VFX.VFXTaskType
---@field ParticleOctagonOutput UnityEngine.VFX.VFXTaskType
---@field ConstantRateSpawner UnityEngine.VFX.VFXTaskType
---@field BurstSpawner UnityEngine.VFX.VFXTaskType
---@field PeriodicBurstSpawner UnityEngine.VFX.VFXTaskType
---@field VariableRateSpawner UnityEngine.VFX.VFXTaskType
---@field CustomCallbackSpawner UnityEngine.VFX.VFXTaskType
---@field SetAttributeSpawner UnityEngine.VFX.VFXTaskType
---@field EvaluateExpressionsSpawner UnityEngine.VFX.VFXTaskType
UnityEngine.VFX.VFXTaskType = {}
---@alias CS.UnityEngine.VFX.VFXTaskType UnityEngine.VFX.VFXTaskType
CS.UnityEngine.VFX.VFXTaskType = UnityEngine.VFX.VFXTaskType


---@class UnityEngine.VFX.VFXUpdateMode
---@field FixedDeltaTime UnityEngine.VFX.VFXUpdateMode
---@field DeltaTime UnityEngine.VFX.VFXUpdateMode
---@field IgnoreTimeScale UnityEngine.VFX.VFXUpdateMode
---@field ExactFixedTimeStep UnityEngine.VFX.VFXUpdateMode
---@field DeltaTimeAndIgnoreTimeScale UnityEngine.VFX.VFXUpdateMode
---@field FixedDeltaAndExactTime UnityEngine.VFX.VFXUpdateMode
---@field FixedDeltaAndExactTimeAndIgnoreTimeScale UnityEngine.VFX.VFXUpdateMode
UnityEngine.VFX.VFXUpdateMode = {}
---@alias CS.UnityEngine.VFX.VFXUpdateMode UnityEngine.VFX.VFXUpdateMode
CS.UnityEngine.VFX.VFXUpdateMode = UnityEngine.VFX.VFXUpdateMode


---@class UnityEngine.VFX.VFXValueType
---@field None UnityEngine.VFX.VFXValueType
---@field Float UnityEngine.VFX.VFXValueType
---@field Float2 UnityEngine.VFX.VFXValueType
---@field Float3 UnityEngine.VFX.VFXValueType
---@field Float4 UnityEngine.VFX.VFXValueType
---@field Int32 UnityEngine.VFX.VFXValueType
---@field Uint32 UnityEngine.VFX.VFXValueType
---@field Texture2D UnityEngine.VFX.VFXValueType
---@field Texture2DArray UnityEngine.VFX.VFXValueType
---@field Texture3D UnityEngine.VFX.VFXValueType
---@field TextureCube UnityEngine.VFX.VFXValueType
---@field TextureCubeArray UnityEngine.VFX.VFXValueType
---@field CameraBuffer UnityEngine.VFX.VFXValueType
---@field Matrix4x4 UnityEngine.VFX.VFXValueType
---@field Curve UnityEngine.VFX.VFXValueType
---@field ColorGradient UnityEngine.VFX.VFXValueType
---@field Mesh UnityEngine.VFX.VFXValueType
---@field Spline UnityEngine.VFX.VFXValueType
---@field Boolean UnityEngine.VFX.VFXValueType
---@field Buffer UnityEngine.VFX.VFXValueType
---@field SkinnedMeshRenderer UnityEngine.VFX.VFXValueType
UnityEngine.VFX.VFXValueType = {}
---@alias CS.UnityEngine.VFX.VFXValueType UnityEngine.VFX.VFXValueType
CS.UnityEngine.VFX.VFXValueType = UnityEngine.VFX.VFXValueType


---@class UnityEngine.VFX.VisualEffect : UnityEngine.Behaviour
---@field outputEventReceived System.Action | function
---@field pause boolean
---@field playRate number
---@field startSeed number
---@field resetSeedOnPlay boolean
---@field initialEventID number
---@field initialEventName string
---@field culled boolean
---@field visualEffectAsset UnityEngine.VFX.VisualEffectAsset
---@field aliveParticleCount number
UnityEngine.VFX.VisualEffect = {}
---@alias CS.UnityEngine.VFX.VisualEffect UnityEngine.VFX.VisualEffect
CS.UnityEngine.VFX.VisualEffect = UnityEngine.VFX.VisualEffect

---@return UnityEngine.VFX.VisualEffect
function UnityEngine.VFX.VisualEffect.New() end
---@return UnityEngine.VFX.VFXEventAttribute
function UnityEngine.VFX.VisualEffect:CreateVFXEventAttribute() end
---@overload fun(self: UnityEngine.VFX.VisualEffect, eventNameID: number, eventAttribute: UnityEngine.VFX.VFXEventAttribute)
---@overload fun(self: UnityEngine.VFX.VisualEffect, eventName: string, eventAttribute: UnityEngine.VFX.VFXEventAttribute)
---@overload fun(self: UnityEngine.VFX.VisualEffect, eventNameID: number)
---@param eventName string
function UnityEngine.VFX.VisualEffect:SendEvent(eventName) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, eventAttribute: UnityEngine.VFX.VFXEventAttribute)
function UnityEngine.VFX.VisualEffect:Play() end
---@overload fun(self: UnityEngine.VFX.VisualEffect, eventAttribute: UnityEngine.VFX.VFXEventAttribute)
function UnityEngine.VFX.VisualEffect:Stop() end
function UnityEngine.VFX.VisualEffect:Reinit() end
function UnityEngine.VFX.VisualEffect:AdvanceOneFrame() end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number)
---@param name string
function UnityEngine.VFX.VisualEffect:ResetOverride(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.Rendering.TextureDimension
---@param name string
---@return UnityEngine.Rendering.TextureDimension
function UnityEngine.VFX.VisualEffect:GetTextureDimension(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasBool(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasInt(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasUInt(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasFloat(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasVector2(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasVector3(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasVector4(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasMatrix4x4(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasTexture(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasAnimationCurve(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasGradient(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasMesh(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasSkinnedMeshRenderer(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasGraphicsBuffer(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, b: boolean)
---@param name string
---@param b boolean
function UnityEngine.VFX.VisualEffect:SetBool(name, b) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, i: number)
---@param name string
---@param i number
function UnityEngine.VFX.VisualEffect:SetInt(name, i) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, i: number)
---@param name string
---@param i number
function UnityEngine.VFX.VisualEffect:SetUInt(name, i) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, f: number)
---@param name string
---@param f number
function UnityEngine.VFX.VisualEffect:SetFloat(name, f) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, v: UnityEngine.Vector2)
---@param name string
---@param v UnityEngine.Vector2
function UnityEngine.VFX.VisualEffect:SetVector2(name, v) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, v: UnityEngine.Vector3)
---@param name string
---@param v UnityEngine.Vector3
function UnityEngine.VFX.VisualEffect:SetVector3(name, v) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, v: UnityEngine.Vector4)
---@param name string
---@param v UnityEngine.Vector4
function UnityEngine.VFX.VisualEffect:SetVector4(name, v) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, v: UnityEngine.Matrix4x4)
---@param name string
---@param v UnityEngine.Matrix4x4
function UnityEngine.VFX.VisualEffect:SetMatrix4x4(name, v) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, t: UnityEngine.Texture)
---@param name string
---@param t UnityEngine.Texture
function UnityEngine.VFX.VisualEffect:SetTexture(name, t) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, c: UnityEngine.AnimationCurve)
---@param name string
---@param c UnityEngine.AnimationCurve
function UnityEngine.VFX.VisualEffect:SetAnimationCurve(name, c) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, g: UnityEngine.Gradient)
---@param name string
---@param g UnityEngine.Gradient
function UnityEngine.VFX.VisualEffect:SetGradient(name, g) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, m: UnityEngine.Mesh)
---@param name string
---@param m UnityEngine.Mesh
function UnityEngine.VFX.VisualEffect:SetMesh(name, m) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, m: UnityEngine.SkinnedMeshRenderer)
---@param name string
---@param m UnityEngine.SkinnedMeshRenderer
function UnityEngine.VFX.VisualEffect:SetSkinnedMeshRenderer(name, m) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, g: UnityEngine.GraphicsBuffer)
---@param name string
---@param g UnityEngine.GraphicsBuffer
function UnityEngine.VFX.VisualEffect:SetGraphicsBuffer(name, g) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:GetBool(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : number
---@param name string
---@return number
function UnityEngine.VFX.VisualEffect:GetInt(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : number
---@param name string
---@return number
function UnityEngine.VFX.VisualEffect:GetUInt(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : number
---@param name string
---@return number
function UnityEngine.VFX.VisualEffect:GetFloat(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.Vector2
---@param name string
---@return UnityEngine.Vector2
function UnityEngine.VFX.VisualEffect:GetVector2(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.Vector3
---@param name string
---@return UnityEngine.Vector3
function UnityEngine.VFX.VisualEffect:GetVector3(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.Vector4
---@param name string
---@return UnityEngine.Vector4
function UnityEngine.VFX.VisualEffect:GetVector4(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.Matrix4x4
---@param name string
---@return UnityEngine.Matrix4x4
function UnityEngine.VFX.VisualEffect:GetMatrix4x4(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.Texture
---@param name string
---@return UnityEngine.Texture
function UnityEngine.VFX.VisualEffect:GetTexture(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.Mesh
---@param name string
---@return UnityEngine.Mesh
function UnityEngine.VFX.VisualEffect:GetMesh(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.SkinnedMeshRenderer
---@param name string
---@return UnityEngine.SkinnedMeshRenderer
function UnityEngine.VFX.VisualEffect:GetSkinnedMeshRenderer(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.Gradient
---@param name string
---@return UnityEngine.Gradient
function UnityEngine.VFX.VisualEffect:GetGradient(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.AnimationCurve
---@param name string
---@return UnityEngine.AnimationCurve
function UnityEngine.VFX.VisualEffect:GetAnimationCurve(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.VFX.VFXParticleSystemInfo
---@param name string
---@return UnityEngine.VFX.VFXParticleSystemInfo
function UnityEngine.VFX.VisualEffect:GetParticleSystemInfo(name) end
---@return boolean
function UnityEngine.VFX.VisualEffect:HasAnySystemAwake() end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number, spawnState: UnityEngine.VFX.VFXSpawnerState)
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : UnityEngine.VFX.VFXSpawnerState
---@param name string
---@return UnityEngine.VFX.VFXSpawnerState
function UnityEngine.VFX.VisualEffect:GetSpawnSystemInfo(name) end
---@overload fun(self: UnityEngine.VFX.VisualEffect, nameID: number) : boolean
---@param name string
---@return boolean
function UnityEngine.VFX.VisualEffect:HasSystem(name) end
---@param names System.Collections.Generic.List
function UnityEngine.VFX.VisualEffect:GetSystemNames(names) end
---@param names System.Collections.Generic.List
function UnityEngine.VFX.VisualEffect:GetParticleSystemNames(names) end
---@param names System.Collections.Generic.List
function UnityEngine.VFX.VisualEffect:GetOutputEventNames(names) end
---@param names System.Collections.Generic.List
function UnityEngine.VFX.VisualEffect:GetSpawnSystemNames(names) end
---@param stepDeltaTime number
---@param stepCount number
function UnityEngine.VFX.VisualEffect:Simulate(stepDeltaTime, stepCount) end

---@class UnityEngine.VFX.VisualEffectAsset : UnityEngine.VFX.VisualEffectObject
---@field PlayEventName string
---@field StopEventName string
---@field PlayEventID number
---@field StopEventID number
UnityEngine.VFX.VisualEffectAsset = {}
---@alias CS.UnityEngine.VFX.VisualEffectAsset UnityEngine.VFX.VisualEffectAsset
CS.UnityEngine.VFX.VisualEffectAsset = UnityEngine.VFX.VisualEffectAsset

---@return UnityEngine.VFX.VisualEffectAsset
function UnityEngine.VFX.VisualEffectAsset.New() end
---@overload fun(self: UnityEngine.VFX.VisualEffectAsset, nameID: number) : UnityEngine.Rendering.TextureDimension
---@param name string
---@return UnityEngine.Rendering.TextureDimension
function UnityEngine.VFX.VisualEffectAsset:GetTextureDimension(name) end
---@param exposedProperties System.Collections.Generic.List
function UnityEngine.VFX.VisualEffectAsset:GetExposedProperties(exposedProperties) end
---@param names System.Collections.Generic.List
function UnityEngine.VFX.VisualEffectAsset:GetEvents(names) end

---@class UnityEngine.VFX.VisualEffectObject : UnityEngine.Object
UnityEngine.VFX.VisualEffectObject = {}
---@alias CS.UnityEngine.VFX.VisualEffectObject UnityEngine.VFX.VisualEffectObject
CS.UnityEngine.VFX.VisualEffectObject = UnityEngine.VFX.VisualEffectObject


---@class UnityEngine.Video.Video3DLayout
---@field No3D UnityEngine.Video.Video3DLayout
---@field SideBySide3D UnityEngine.Video.Video3DLayout
---@field OverUnder3D UnityEngine.Video.Video3DLayout
UnityEngine.Video.Video3DLayout = {}
---@alias CS.UnityEngine.Video.Video3DLayout UnityEngine.Video.Video3DLayout
CS.UnityEngine.Video.Video3DLayout = UnityEngine.Video.Video3DLayout


---@class UnityEngine.Video.VideoAspectRatio
---@field NoScaling UnityEngine.Video.VideoAspectRatio
---@field FitVertically UnityEngine.Video.VideoAspectRatio
---@field FitHorizontally UnityEngine.Video.VideoAspectRatio
---@field FitInside UnityEngine.Video.VideoAspectRatio
---@field FitOutside UnityEngine.Video.VideoAspectRatio
---@field Stretch UnityEngine.Video.VideoAspectRatio
UnityEngine.Video.VideoAspectRatio = {}
---@alias CS.UnityEngine.Video.VideoAspectRatio UnityEngine.Video.VideoAspectRatio
CS.UnityEngine.Video.VideoAspectRatio = UnityEngine.Video.VideoAspectRatio


---@class UnityEngine.Video.VideoAudioOutputMode
---@field None UnityEngine.Video.VideoAudioOutputMode
---@field AudioSource UnityEngine.Video.VideoAudioOutputMode
---@field Direct UnityEngine.Video.VideoAudioOutputMode
---@field APIOnly UnityEngine.Video.VideoAudioOutputMode
UnityEngine.Video.VideoAudioOutputMode = {}
---@alias CS.UnityEngine.Video.VideoAudioOutputMode UnityEngine.Video.VideoAudioOutputMode
CS.UnityEngine.Video.VideoAudioOutputMode = UnityEngine.Video.VideoAudioOutputMode


---@class UnityEngine.Video.VideoClip : UnityEngine.Object
---@field originalPath string
---@field frameCount number
---@field frameRate number
---@field length number
---@field width number
---@field height number
---@field pixelAspectRatioNumerator number
---@field pixelAspectRatioDenominator number
---@field sRGB boolean
---@field audioTrackCount number
UnityEngine.Video.VideoClip = {}
---@alias CS.UnityEngine.Video.VideoClip UnityEngine.Video.VideoClip
CS.UnityEngine.Video.VideoClip = UnityEngine.Video.VideoClip

---@param audioTrackIdx number
---@return number
function UnityEngine.Video.VideoClip:GetAudioChannelCount(audioTrackIdx) end
---@param audioTrackIdx number
---@return number
function UnityEngine.Video.VideoClip:GetAudioSampleRate(audioTrackIdx) end
---@param audioTrackIdx number
---@return string
function UnityEngine.Video.VideoClip:GetAudioLanguage(audioTrackIdx) end

---@class UnityEngine.Video.VideoPlayer : UnityEngine.Behaviour
---@field controlledAudioTrackMaxCount number
---@field source UnityEngine.Video.VideoSource
---@field timeUpdateMode UnityEngine.Video.VideoTimeUpdateMode
---@field url string
---@field clip UnityEngine.Video.VideoClip
---@field renderMode UnityEngine.Video.VideoRenderMode
---@field canSetTimeUpdateMode boolean
---@field targetCamera UnityEngine.Camera
---@field targetTexture UnityEngine.RenderTexture
---@field targetMaterialRenderer UnityEngine.Renderer
---@field targetMaterialProperty string
---@field aspectRatio UnityEngine.Video.VideoAspectRatio
---@field targetCameraAlpha number
---@field targetCamera3DLayout UnityEngine.Video.Video3DLayout
---@field texture UnityEngine.Texture
---@field isPrepared boolean
---@field waitForFirstFrame boolean
---@field playOnAwake boolean
---@field isPlaying boolean
---@field isPaused boolean
---@field canSetTime boolean
---@field time number
---@field frame number
---@field clockTime number
---@field canStep boolean
---@field canSetPlaybackSpeed boolean
---@field playbackSpeed number
---@field isLooping boolean
---@field timeReference UnityEngine.Video.VideoTimeReference
---@field externalReferenceTime number
---@field canSetSkipOnDrop boolean
---@field skipOnDrop boolean
---@field frameCount number
---@field frameRate number
---@field length number
---@field width number
---@field height number
---@field pixelAspectRatioNumerator number
---@field pixelAspectRatioDenominator number
---@field audioTrackCount number
---@field controlledAudioTrackCount number
---@field audioOutputMode UnityEngine.Video.VideoAudioOutputMode
---@field canSetDirectAudioVolume boolean
---@field sendFrameReadyEvents boolean
UnityEngine.Video.VideoPlayer = {}
---@alias CS.UnityEngine.Video.VideoPlayer UnityEngine.Video.VideoPlayer
CS.UnityEngine.Video.VideoPlayer = UnityEngine.Video.VideoPlayer

---@return UnityEngine.Video.VideoPlayer
function UnityEngine.Video.VideoPlayer.New() end
function UnityEngine.Video.VideoPlayer:Prepare() end
function UnityEngine.Video.VideoPlayer:Play() end
function UnityEngine.Video.VideoPlayer:Pause() end
function UnityEngine.Video.VideoPlayer:Stop() end
function UnityEngine.Video.VideoPlayer:StepForward() end
---@param trackIndex number
---@return string
function UnityEngine.Video.VideoPlayer:GetAudioLanguageCode(trackIndex) end
---@param trackIndex number
---@return number
function UnityEngine.Video.VideoPlayer:GetAudioChannelCount(trackIndex) end
---@param trackIndex number
---@return number
function UnityEngine.Video.VideoPlayer:GetAudioSampleRate(trackIndex) end
---@param trackIndex number
---@param enabled boolean
function UnityEngine.Video.VideoPlayer:EnableAudioTrack(trackIndex, enabled) end
---@param trackIndex number
---@return boolean
function UnityEngine.Video.VideoPlayer:IsAudioTrackEnabled(trackIndex) end
---@param trackIndex number
---@return number
function UnityEngine.Video.VideoPlayer:GetDirectAudioVolume(trackIndex) end
---@param trackIndex number
---@param volume number
function UnityEngine.Video.VideoPlayer:SetDirectAudioVolume(trackIndex, volume) end
---@param trackIndex number
---@return boolean
function UnityEngine.Video.VideoPlayer:GetDirectAudioMute(trackIndex) end
---@param trackIndex number
---@param mute boolean
function UnityEngine.Video.VideoPlayer:SetDirectAudioMute(trackIndex, mute) end
---@param trackIndex number
---@return UnityEngine.AudioSource
function UnityEngine.Video.VideoPlayer:GetTargetAudioSource(trackIndex) end
---@param trackIndex number
---@param source UnityEngine.AudioSource
function UnityEngine.Video.VideoPlayer:SetTargetAudioSource(trackIndex, source) end
---@param trackIndex number
---@return UnityEngine.Experimental.Audio.AudioSampleProvider
function UnityEngine.Video.VideoPlayer:GetAudioSampleProvider(trackIndex) end

---@class UnityEngine.Video.VideoPlayer.ErrorEventHandler : System.MulticastDelegate
UnityEngine.Video.VideoPlayer.ErrorEventHandler = {}
---@alias CS.UnityEngine.Video.VideoPlayer.ErrorEventHandler UnityEngine.Video.VideoPlayer.ErrorEventHandler
CS.UnityEngine.Video.VideoPlayer.ErrorEventHandler = UnityEngine.Video.VideoPlayer.ErrorEventHandler

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Video.VideoPlayer.ErrorEventHandler
function UnityEngine.Video.VideoPlayer.ErrorEventHandler.New(object, method) end
---@param source UnityEngine.Video.VideoPlayer
---@param message string
function UnityEngine.Video.VideoPlayer.ErrorEventHandler:Invoke(source, message) end
---@param source UnityEngine.Video.VideoPlayer
---@param message string
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Video.VideoPlayer.ErrorEventHandler:BeginInvoke(source, message, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Video.VideoPlayer.ErrorEventHandler:EndInvoke(result) end

---@class UnityEngine.Video.VideoPlayer.EventHandler : System.MulticastDelegate
UnityEngine.Video.VideoPlayer.EventHandler = {}
---@alias CS.UnityEngine.Video.VideoPlayer.EventHandler UnityEngine.Video.VideoPlayer.EventHandler
CS.UnityEngine.Video.VideoPlayer.EventHandler = UnityEngine.Video.VideoPlayer.EventHandler

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Video.VideoPlayer.EventHandler
function UnityEngine.Video.VideoPlayer.EventHandler.New(object, method) end
---@param source UnityEngine.Video.VideoPlayer
function UnityEngine.Video.VideoPlayer.EventHandler:Invoke(source) end
---@param source UnityEngine.Video.VideoPlayer
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Video.VideoPlayer.EventHandler:BeginInvoke(source, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Video.VideoPlayer.EventHandler:EndInvoke(result) end

---@class UnityEngine.Video.VideoPlayer.FrameReadyEventHandler : System.MulticastDelegate
UnityEngine.Video.VideoPlayer.FrameReadyEventHandler = {}
---@alias CS.UnityEngine.Video.VideoPlayer.FrameReadyEventHandler UnityEngine.Video.VideoPlayer.FrameReadyEventHandler
CS.UnityEngine.Video.VideoPlayer.FrameReadyEventHandler = UnityEngine.Video.VideoPlayer.FrameReadyEventHandler

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Video.VideoPlayer.FrameReadyEventHandler
function UnityEngine.Video.VideoPlayer.FrameReadyEventHandler.New(object, method) end
---@param source UnityEngine.Video.VideoPlayer
---@param frameIdx number
function UnityEngine.Video.VideoPlayer.FrameReadyEventHandler:Invoke(source, frameIdx) end
---@param source UnityEngine.Video.VideoPlayer
---@param frameIdx number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Video.VideoPlayer.FrameReadyEventHandler:BeginInvoke(source, frameIdx, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Video.VideoPlayer.FrameReadyEventHandler:EndInvoke(result) end

---@class UnityEngine.Video.VideoPlayer.TimeEventHandler : System.MulticastDelegate
UnityEngine.Video.VideoPlayer.TimeEventHandler = {}
---@alias CS.UnityEngine.Video.VideoPlayer.TimeEventHandler UnityEngine.Video.VideoPlayer.TimeEventHandler
CS.UnityEngine.Video.VideoPlayer.TimeEventHandler = UnityEngine.Video.VideoPlayer.TimeEventHandler

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Video.VideoPlayer.TimeEventHandler
function UnityEngine.Video.VideoPlayer.TimeEventHandler.New(object, method) end
---@param source UnityEngine.Video.VideoPlayer
---@param seconds number
function UnityEngine.Video.VideoPlayer.TimeEventHandler:Invoke(source, seconds) end
---@param source UnityEngine.Video.VideoPlayer
---@param seconds number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Video.VideoPlayer.TimeEventHandler:BeginInvoke(source, seconds, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Video.VideoPlayer.TimeEventHandler:EndInvoke(result) end

---@class UnityEngine.Video.VideoRenderMode
---@field CameraFarPlane UnityEngine.Video.VideoRenderMode
---@field CameraNearPlane UnityEngine.Video.VideoRenderMode
---@field RenderTexture UnityEngine.Video.VideoRenderMode
---@field MaterialOverride UnityEngine.Video.VideoRenderMode
---@field APIOnly UnityEngine.Video.VideoRenderMode
UnityEngine.Video.VideoRenderMode = {}
---@alias CS.UnityEngine.Video.VideoRenderMode UnityEngine.Video.VideoRenderMode
CS.UnityEngine.Video.VideoRenderMode = UnityEngine.Video.VideoRenderMode


---@class UnityEngine.Video.VideoSource
---@field VideoClip UnityEngine.Video.VideoSource
---@field Url UnityEngine.Video.VideoSource
UnityEngine.Video.VideoSource = {}
---@alias CS.UnityEngine.Video.VideoSource UnityEngine.Video.VideoSource
CS.UnityEngine.Video.VideoSource = UnityEngine.Video.VideoSource


---@class UnityEngine.Video.VideoTimeReference
---@field Freerun UnityEngine.Video.VideoTimeReference
---@field InternalTime UnityEngine.Video.VideoTimeReference
---@field ExternalTime UnityEngine.Video.VideoTimeReference
UnityEngine.Video.VideoTimeReference = {}
---@alias CS.UnityEngine.Video.VideoTimeReference UnityEngine.Video.VideoTimeReference
CS.UnityEngine.Video.VideoTimeReference = UnityEngine.Video.VideoTimeReference


---@class UnityEngine.Video.VideoTimeSource
UnityEngine.Video.VideoTimeSource = {}
---@alias CS.UnityEngine.Video.VideoTimeSource UnityEngine.Video.VideoTimeSource
CS.UnityEngine.Video.VideoTimeSource = UnityEngine.Video.VideoTimeSource


---@class UnityEngine.Video.VideoTimeUpdateMode
---@field DSPTime UnityEngine.Video.VideoTimeUpdateMode
---@field GameTime UnityEngine.Video.VideoTimeUpdateMode
---@field UnscaledGameTime UnityEngine.Video.VideoTimeUpdateMode
UnityEngine.Video.VideoTimeUpdateMode = {}
---@alias CS.UnityEngine.Video.VideoTimeUpdateMode UnityEngine.Video.VideoTimeUpdateMode
CS.UnityEngine.Video.VideoTimeUpdateMode = UnityEngine.Video.VideoTimeUpdateMode


---@class UnityEngine.VRTextureUsage
---@field None UnityEngine.VRTextureUsage
---@field OneEye UnityEngine.VRTextureUsage
---@field TwoEyes UnityEngine.VRTextureUsage
---@field DeviceSpecific UnityEngine.VRTextureUsage
UnityEngine.VRTextureUsage = {}
---@alias CS.UnityEngine.VRTextureUsage UnityEngine.VRTextureUsage
CS.UnityEngine.VRTextureUsage = UnityEngine.VRTextureUsage


---@class UnityEngine.WaitForEndOfFrame : UnityEngine.YieldInstruction
UnityEngine.WaitForEndOfFrame = {}
---@alias CS.UnityEngine.WaitForEndOfFrame UnityEngine.WaitForEndOfFrame
CS.UnityEngine.WaitForEndOfFrame = UnityEngine.WaitForEndOfFrame

---@return UnityEngine.WaitForEndOfFrame
function UnityEngine.WaitForEndOfFrame.New() end

---@class UnityEngine.WaitForFixedUpdate : UnityEngine.YieldInstruction
UnityEngine.WaitForFixedUpdate = {}
---@alias CS.UnityEngine.WaitForFixedUpdate UnityEngine.WaitForFixedUpdate
CS.UnityEngine.WaitForFixedUpdate = UnityEngine.WaitForFixedUpdate

---@return UnityEngine.WaitForFixedUpdate
function UnityEngine.WaitForFixedUpdate.New() end

---@class UnityEngine.WaitForSeconds : UnityEngine.YieldInstruction
UnityEngine.WaitForSeconds = {}
---@alias CS.UnityEngine.WaitForSeconds UnityEngine.WaitForSeconds
CS.UnityEngine.WaitForSeconds = UnityEngine.WaitForSeconds

---@param seconds number
---@return UnityEngine.WaitForSeconds
function UnityEngine.WaitForSeconds.New(seconds) end

---@class UnityEngine.WaitForSecondsRealtime : UnityEngine.CustomYieldInstruction
---@field waitTime number
---@field keepWaiting boolean
UnityEngine.WaitForSecondsRealtime = {}
---@alias CS.UnityEngine.WaitForSecondsRealtime UnityEngine.WaitForSecondsRealtime
CS.UnityEngine.WaitForSecondsRealtime = UnityEngine.WaitForSecondsRealtime

---@param time number
---@return UnityEngine.WaitForSecondsRealtime
function UnityEngine.WaitForSecondsRealtime.New(time) end
function UnityEngine.WaitForSecondsRealtime:Reset() end

---@class UnityEngine.WaitUntil : UnityEngine.CustomYieldInstruction
---@field keepWaiting boolean
UnityEngine.WaitUntil = {}
---@alias CS.UnityEngine.WaitUntil UnityEngine.WaitUntil
CS.UnityEngine.WaitUntil = UnityEngine.WaitUntil

---@param predicate System.Func
---@return UnityEngine.WaitUntil
function UnityEngine.WaitUntil.New(predicate) end

---@class UnityEngine.WaitWhile : UnityEngine.CustomYieldInstruction
---@field keepWaiting boolean
UnityEngine.WaitWhile = {}
---@alias CS.UnityEngine.WaitWhile UnityEngine.WaitWhile
CS.UnityEngine.WaitWhile = UnityEngine.WaitWhile

---@param predicate System.Func
---@return UnityEngine.WaitWhile
function UnityEngine.WaitWhile.New(predicate) end

---@class UnityEngine.WebCamDevice : System.ValueType
---@field name string
---@field isFrontFacing boolean
---@field kind UnityEngine.WebCamKind
---@field depthCameraName string
---@field isAutoFocusPointSupported boolean
---@field availableResolutions UnityEngine.Resolution[]
UnityEngine.WebCamDevice = {}
---@alias CS.UnityEngine.WebCamDevice UnityEngine.WebCamDevice
CS.UnityEngine.WebCamDevice = UnityEngine.WebCamDevice


---@class UnityEngine.WebCamFlags
---@field FrontFacing UnityEngine.WebCamFlags
---@field AutoFocusPointSupported UnityEngine.WebCamFlags
UnityEngine.WebCamFlags = {}
---@alias CS.UnityEngine.WebCamFlags UnityEngine.WebCamFlags
CS.UnityEngine.WebCamFlags = UnityEngine.WebCamFlags


---@class UnityEngine.WebCamKind
---@field WideAngle UnityEngine.WebCamKind
---@field Telephoto UnityEngine.WebCamKind
---@field ColorAndDepth UnityEngine.WebCamKind
---@field UltraWideAngle UnityEngine.WebCamKind
UnityEngine.WebCamKind = {}
---@alias CS.UnityEngine.WebCamKind UnityEngine.WebCamKind
CS.UnityEngine.WebCamKind = UnityEngine.WebCamKind


---@class UnityEngine.WebCamTexture : UnityEngine.Texture
---@field devices UnityEngine.WebCamDevice[]
---@field isPlaying boolean
---@field deviceName string
---@field requestedFPS number
---@field requestedWidth number
---@field requestedHeight number
---@field videoRotationAngle number
---@field videoVerticallyMirrored boolean
---@field didUpdateThisFrame boolean
---@field autoFocusPoint System.Nullable
---@field isDepth boolean
UnityEngine.WebCamTexture = {}
---@alias CS.UnityEngine.WebCamTexture UnityEngine.WebCamTexture
CS.UnityEngine.WebCamTexture = UnityEngine.WebCamTexture

---@overload fun(deviceName: string, requestedWidth: number, requestedHeight: number, requestedFPS: number) : UnityEngine.WebCamTexture
---@overload fun(deviceName: string, requestedWidth: number, requestedHeight: number) : UnityEngine.WebCamTexture
---@overload fun(deviceName: string) : UnityEngine.WebCamTexture
---@overload fun(requestedWidth: number, requestedHeight: number, requestedFPS: number) : UnityEngine.WebCamTexture
---@overload fun(requestedWidth: number, requestedHeight: number) : UnityEngine.WebCamTexture
---@return UnityEngine.WebCamTexture
function UnityEngine.WebCamTexture.New() end
function UnityEngine.WebCamTexture:Play() end
function UnityEngine.WebCamTexture:Pause() end
function UnityEngine.WebCamTexture:Stop() end
---@param x number
---@param y number
---@return UnityEngine.Color
function UnityEngine.WebCamTexture:GetPixel(x, y) end
---@overload fun(self: UnityEngine.WebCamTexture) : UnityEngine.Color[]
---@param x number
---@param y number
---@param blockWidth number
---@param blockHeight number
---@return UnityEngine.Color[]
function UnityEngine.WebCamTexture:GetPixels(x, y, blockWidth, blockHeight) end
---@overload fun(self: UnityEngine.WebCamTexture) : UnityEngine.Color32[]
---@param colors UnityEngine.Color32[]
---@return UnityEngine.Color32[]
function UnityEngine.WebCamTexture:GetPixels32(colors) end

---@class UnityEngine.WeightedMode
---@field None UnityEngine.WeightedMode
---@field In UnityEngine.WeightedMode
---@field Out UnityEngine.WeightedMode
---@field Both UnityEngine.WeightedMode
UnityEngine.WeightedMode = {}
---@alias CS.UnityEngine.WeightedMode UnityEngine.WeightedMode
CS.UnityEngine.WeightedMode = UnityEngine.WeightedMode


---@class UnityEngine.WheelCollider : UnityEngine.Collider
---@field center UnityEngine.Vector3
---@field radius number
---@field suspensionDistance number
---@field suspensionSpring UnityEngine.JointSpring
---@field suspensionExpansionLimited boolean
---@field forceAppPointDistance number
---@field mass number
---@field wheelDampingRate number
---@field forwardFriction UnityEngine.WheelFrictionCurve
---@field sidewaysFriction UnityEngine.WheelFrictionCurve
---@field motorTorque number
---@field brakeTorque number
---@field steerAngle number
---@field isGrounded boolean
---@field rpm number
---@field sprungMass number
---@field rotationSpeed number
UnityEngine.WheelCollider = {}
---@alias CS.UnityEngine.WheelCollider UnityEngine.WheelCollider
CS.UnityEngine.WheelCollider = UnityEngine.WheelCollider

---@return UnityEngine.WheelCollider
function UnityEngine.WheelCollider.New() end
function UnityEngine.WheelCollider:ResetSprungMasses() end
---@param speedThreshold number
---@param stepsBelowThreshold number
---@param stepsAboveThreshold number
function UnityEngine.WheelCollider:ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold) end
---@param out_pos UnityEngine.Vector3
---@param out_quat UnityEngine.Quaternion
---@return UnityEngine.Vector3, UnityEngine.Quaternion
function UnityEngine.WheelCollider:GetWorldPose(out_pos, out_quat) end
---@param out_hit UnityEngine.WheelHit
---@return boolean, UnityEngine.WheelHit
function UnityEngine.WheelCollider:GetGroundHit(out_hit) end

---@class UnityEngine.WheelFrictionCurve : System.ValueType
---@field extremumSlip number
---@field extremumValue number
---@field asymptoteSlip number
---@field asymptoteValue number
---@field stiffness number
UnityEngine.WheelFrictionCurve = {}
---@alias CS.UnityEngine.WheelFrictionCurve UnityEngine.WheelFrictionCurve
CS.UnityEngine.WheelFrictionCurve = UnityEngine.WheelFrictionCurve


---@class UnityEngine.WheelHit : System.ValueType
---@field collider UnityEngine.Collider
---@field point UnityEngine.Vector3
---@field normal UnityEngine.Vector3
---@field forwardDir UnityEngine.Vector3
---@field sidewaysDir UnityEngine.Vector3
---@field force number
---@field forwardSlip number
---@field sidewaysSlip number
UnityEngine.WheelHit = {}
---@alias CS.UnityEngine.WheelHit UnityEngine.WheelHit
CS.UnityEngine.WheelHit = UnityEngine.WheelHit


---@class UnityEngine.WheelJoint2D : UnityEngine.AnchoredJoint2D
---@field suspension UnityEngine.JointSuspension2D
---@field useMotor boolean
---@field motor UnityEngine.JointMotor2D
---@field jointTranslation number
---@field jointLinearSpeed number
---@field jointSpeed number
---@field jointAngle number
UnityEngine.WheelJoint2D = {}
---@alias CS.UnityEngine.WheelJoint2D UnityEngine.WheelJoint2D
CS.UnityEngine.WheelJoint2D = UnityEngine.WheelJoint2D

---@return UnityEngine.WheelJoint2D
function UnityEngine.WheelJoint2D.New() end
---@param timeStep number
---@return number
function UnityEngine.WheelJoint2D:GetMotorTorque(timeStep) end

---@class UnityEngine.WhitePoint
---@field Unknown UnityEngine.WhitePoint
---@field D65 UnityEngine.WhitePoint
UnityEngine.WhitePoint = {}
---@alias CS.UnityEngine.WhitePoint UnityEngine.WhitePoint
CS.UnityEngine.WhitePoint = UnityEngine.WhitePoint


---@class UnityEngine.Windows.CrashReporting : System.Object
---@field crashReportFolder string
UnityEngine.Windows.CrashReporting = {}
---@alias CS.UnityEngine.Windows.CrashReporting UnityEngine.Windows.CrashReporting
CS.UnityEngine.Windows.CrashReporting = UnityEngine.Windows.CrashReporting


---@class UnityEngine.Windows.Crypto : System.Object
UnityEngine.Windows.Crypto = {}
---@alias CS.UnityEngine.Windows.Crypto UnityEngine.Windows.Crypto
CS.UnityEngine.Windows.Crypto = UnityEngine.Windows.Crypto

---@param buffer number[]
---@return number[]
function UnityEngine.Windows.Crypto.ComputeMD5Hash(buffer) end
---@param buffer number[]
---@return number[]
function UnityEngine.Windows.Crypto.ComputeSHA1Hash(buffer) end

---@class UnityEngine.Windows.Directory : System.Object
---@field temporaryFolder string
---@field localFolder string
---@field roamingFolder string
UnityEngine.Windows.Directory = {}
---@alias CS.UnityEngine.Windows.Directory UnityEngine.Windows.Directory
CS.UnityEngine.Windows.Directory = UnityEngine.Windows.Directory

---@param path string
function UnityEngine.Windows.Directory.CreateDirectory(path) end
---@param path string
---@return boolean
function UnityEngine.Windows.Directory.Exists(path) end
---@param path string
function UnityEngine.Windows.Directory.Delete(path) end

---@class UnityEngine.Windows.File : System.Object
UnityEngine.Windows.File = {}
---@alias CS.UnityEngine.Windows.File UnityEngine.Windows.File
CS.UnityEngine.Windows.File = UnityEngine.Windows.File

---@param path string
---@return number[]
function UnityEngine.Windows.File.ReadAllBytes(path) end
---@param path string
---@param bytes number[]
function UnityEngine.Windows.File.WriteAllBytes(path, bytes) end
---@param path string
---@return boolean
function UnityEngine.Windows.File.Exists(path) end
---@param path string
function UnityEngine.Windows.File.Delete(path) end

---@class UnityEngine.Windows.Input : System.Object
UnityEngine.Windows.Input = {}
---@alias CS.UnityEngine.Windows.Input UnityEngine.Windows.Input
CS.UnityEngine.Windows.Input = UnityEngine.Windows.Input

---@overload fun(rawInputHeaderIndices: System.IntPtr, rawInputDataIndices: System.IntPtr, indicesCount: number, rawInputData: System.IntPtr, rawInputDataSize: number)
---@param rawInputHeaderIndices System.UInt32*
---@param rawInputDataIndices System.UInt32*
---@param indicesCount number
---@param rawInputData System.Byte*
---@param rawInputDataSize number
function UnityEngine.Windows.Input.ForwardRawInput(rawInputHeaderIndices, rawInputDataIndices, indicesCount, rawInputData, rawInputDataSize) end

---@class UnityEngine.Windows.LicenseInformation : System.Object
---@field isOnAppTrial boolean
UnityEngine.Windows.LicenseInformation = {}
---@alias CS.UnityEngine.Windows.LicenseInformation UnityEngine.Windows.LicenseInformation
CS.UnityEngine.Windows.LicenseInformation = UnityEngine.Windows.LicenseInformation

---@return string
function UnityEngine.Windows.LicenseInformation.PurchaseApp() end

---@class UnityEngine.Windows.Speech.ConfidenceLevel
---@field High UnityEngine.Windows.Speech.ConfidenceLevel
---@field Medium UnityEngine.Windows.Speech.ConfidenceLevel
---@field Low UnityEngine.Windows.Speech.ConfidenceLevel
---@field Rejected UnityEngine.Windows.Speech.ConfidenceLevel
UnityEngine.Windows.Speech.ConfidenceLevel = {}
---@alias CS.UnityEngine.Windows.Speech.ConfidenceLevel UnityEngine.Windows.Speech.ConfidenceLevel
CS.UnityEngine.Windows.Speech.ConfidenceLevel = UnityEngine.Windows.Speech.ConfidenceLevel


---@class UnityEngine.Windows.Speech.DictationCompletionCause
---@field Complete UnityEngine.Windows.Speech.DictationCompletionCause
---@field AudioQualityFailure UnityEngine.Windows.Speech.DictationCompletionCause
---@field Canceled UnityEngine.Windows.Speech.DictationCompletionCause
---@field TimeoutExceeded UnityEngine.Windows.Speech.DictationCompletionCause
---@field PauseLimitExceeded UnityEngine.Windows.Speech.DictationCompletionCause
---@field NetworkFailure UnityEngine.Windows.Speech.DictationCompletionCause
---@field MicrophoneUnavailable UnityEngine.Windows.Speech.DictationCompletionCause
---@field UnknownError UnityEngine.Windows.Speech.DictationCompletionCause
UnityEngine.Windows.Speech.DictationCompletionCause = {}
---@alias CS.UnityEngine.Windows.Speech.DictationCompletionCause UnityEngine.Windows.Speech.DictationCompletionCause
CS.UnityEngine.Windows.Speech.DictationCompletionCause = UnityEngine.Windows.Speech.DictationCompletionCause


---@class UnityEngine.Windows.Speech.DictationRecognizer : System.Object
---@field Status UnityEngine.Windows.Speech.SpeechSystemStatus
---@field AutoSilenceTimeoutSeconds number
---@field InitialSilenceTimeoutSeconds number
UnityEngine.Windows.Speech.DictationRecognizer = {}
---@alias CS.UnityEngine.Windows.Speech.DictationRecognizer UnityEngine.Windows.Speech.DictationRecognizer
CS.UnityEngine.Windows.Speech.DictationRecognizer = UnityEngine.Windows.Speech.DictationRecognizer

---@overload fun() : UnityEngine.Windows.Speech.DictationRecognizer
---@overload fun(confidenceLevel: UnityEngine.Windows.Speech.ConfidenceLevel) : UnityEngine.Windows.Speech.DictationRecognizer
---@overload fun(topic: UnityEngine.Windows.Speech.DictationTopicConstraint) : UnityEngine.Windows.Speech.DictationRecognizer
---@param minimumConfidence UnityEngine.Windows.Speech.ConfidenceLevel
---@param topic UnityEngine.Windows.Speech.DictationTopicConstraint
---@return UnityEngine.Windows.Speech.DictationRecognizer
function UnityEngine.Windows.Speech.DictationRecognizer.New(minimumConfidence, topic) end
function UnityEngine.Windows.Speech.DictationRecognizer:Start() end
function UnityEngine.Windows.Speech.DictationRecognizer:Stop() end
function UnityEngine.Windows.Speech.DictationRecognizer:Dispose() end

---@class UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate : System.MulticastDelegate
UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate = {}
---@alias CS.UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate
CS.UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate = UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate
function UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate.New(object, method) end
---@param cause UnityEngine.Windows.Speech.DictationCompletionCause
function UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate:Invoke(cause) end
---@param cause UnityEngine.Windows.Speech.DictationCompletionCause
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate:BeginInvoke(cause, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.Speech.DictationRecognizer.DictationCompletedDelegate:EndInvoke(result) end

---@class UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler : System.MulticastDelegate
UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler = {}
---@alias CS.UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler
CS.UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler = UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler
function UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler.New(object, method) end
---@param error string
---@param hresult number
function UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler:Invoke(error, hresult) end
---@param error string
---@param hresult number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler:BeginInvoke(error, hresult, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.Speech.DictationRecognizer.DictationErrorHandler:EndInvoke(result) end

---@class UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate : System.MulticastDelegate
UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate = {}
---@alias CS.UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate
CS.UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate = UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate
function UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate.New(object, method) end
---@param text string
function UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate:Invoke(text) end
---@param text string
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate:BeginInvoke(text, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.Speech.DictationRecognizer.DictationHypothesisDelegate:EndInvoke(result) end

---@class UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate : System.MulticastDelegate
UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate = {}
---@alias CS.UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate
CS.UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate = UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate
function UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate.New(object, method) end
---@param text string
---@param confidence UnityEngine.Windows.Speech.ConfidenceLevel
function UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate:Invoke(text, confidence) end
---@param text string
---@param confidence UnityEngine.Windows.Speech.ConfidenceLevel
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate:BeginInvoke(text, confidence, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.Speech.DictationRecognizer.DictationResultDelegate:EndInvoke(result) end

---@class UnityEngine.Windows.Speech.DictationTopicConstraint
---@field WebSearch UnityEngine.Windows.Speech.DictationTopicConstraint
---@field Form UnityEngine.Windows.Speech.DictationTopicConstraint
---@field Dictation UnityEngine.Windows.Speech.DictationTopicConstraint
UnityEngine.Windows.Speech.DictationTopicConstraint = {}
---@alias CS.UnityEngine.Windows.Speech.DictationTopicConstraint UnityEngine.Windows.Speech.DictationTopicConstraint
CS.UnityEngine.Windows.Speech.DictationTopicConstraint = UnityEngine.Windows.Speech.DictationTopicConstraint


---@class UnityEngine.Windows.Speech.GrammarRecognizer : UnityEngine.Windows.Speech.PhraseRecognizer
---@field GrammarFilePath string
UnityEngine.Windows.Speech.GrammarRecognizer = {}
---@alias CS.UnityEngine.Windows.Speech.GrammarRecognizer UnityEngine.Windows.Speech.GrammarRecognizer
CS.UnityEngine.Windows.Speech.GrammarRecognizer = UnityEngine.Windows.Speech.GrammarRecognizer

---@overload fun(grammarFilePath: string) : UnityEngine.Windows.Speech.GrammarRecognizer
---@param grammarFilePath string
---@param minimumConfidence UnityEngine.Windows.Speech.ConfidenceLevel
---@return UnityEngine.Windows.Speech.GrammarRecognizer
function UnityEngine.Windows.Speech.GrammarRecognizer.New(grammarFilePath, minimumConfidence) end

---@class UnityEngine.Windows.Speech.KeywordRecognizer : UnityEngine.Windows.Speech.PhraseRecognizer
---@field Keywords System.Collections.Generic.IEnumerable
UnityEngine.Windows.Speech.KeywordRecognizer = {}
---@alias CS.UnityEngine.Windows.Speech.KeywordRecognizer UnityEngine.Windows.Speech.KeywordRecognizer
CS.UnityEngine.Windows.Speech.KeywordRecognizer = UnityEngine.Windows.Speech.KeywordRecognizer

---@overload fun(keywords: string[]) : UnityEngine.Windows.Speech.KeywordRecognizer
---@param keywords string[]
---@param minimumConfidence UnityEngine.Windows.Speech.ConfidenceLevel
---@return UnityEngine.Windows.Speech.KeywordRecognizer
function UnityEngine.Windows.Speech.KeywordRecognizer.New(keywords, minimumConfidence) end

---@class UnityEngine.Windows.Speech.PhraseRecognitionSystem : System.Object
---@field isSupported boolean
---@field Status UnityEngine.Windows.Speech.SpeechSystemStatus
UnityEngine.Windows.Speech.PhraseRecognitionSystem = {}
---@alias CS.UnityEngine.Windows.Speech.PhraseRecognitionSystem UnityEngine.Windows.Speech.PhraseRecognitionSystem
CS.UnityEngine.Windows.Speech.PhraseRecognitionSystem = UnityEngine.Windows.Speech.PhraseRecognitionSystem

function UnityEngine.Windows.Speech.PhraseRecognitionSystem.Restart() end
function UnityEngine.Windows.Speech.PhraseRecognitionSystem.Shutdown() end

---@class UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate : System.MulticastDelegate
UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate = {}
---@alias CS.UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate
CS.UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate = UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate
function UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate.New(object, method) end
---@param errorCode UnityEngine.Windows.Speech.SpeechError
function UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate:Invoke(errorCode) end
---@param errorCode UnityEngine.Windows.Speech.SpeechError
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate:BeginInvoke(errorCode, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.Speech.PhraseRecognitionSystem.ErrorDelegate:EndInvoke(result) end

---@class UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate : System.MulticastDelegate
UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate = {}
---@alias CS.UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate
CS.UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate = UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate
function UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate.New(object, method) end
---@param status UnityEngine.Windows.Speech.SpeechSystemStatus
function UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate:Invoke(status) end
---@param status UnityEngine.Windows.Speech.SpeechSystemStatus
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate:BeginInvoke(status, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.Speech.PhraseRecognitionSystem.StatusDelegate:EndInvoke(result) end

---@class UnityEngine.Windows.Speech.PhraseRecognizedEventArgs : System.ValueType
---@field confidence UnityEngine.Windows.Speech.ConfidenceLevel
---@field semanticMeanings UnityEngine.Windows.Speech.SemanticMeaning[]
---@field text string
---@field phraseStartTime System.DateTime
---@field phraseDuration System.TimeSpan
UnityEngine.Windows.Speech.PhraseRecognizedEventArgs = {}
---@alias CS.UnityEngine.Windows.Speech.PhraseRecognizedEventArgs UnityEngine.Windows.Speech.PhraseRecognizedEventArgs
CS.UnityEngine.Windows.Speech.PhraseRecognizedEventArgs = UnityEngine.Windows.Speech.PhraseRecognizedEventArgs


---@class UnityEngine.Windows.Speech.PhraseRecognizer : System.Object
---@field IsRunning boolean
UnityEngine.Windows.Speech.PhraseRecognizer = {}
---@alias CS.UnityEngine.Windows.Speech.PhraseRecognizer UnityEngine.Windows.Speech.PhraseRecognizer
CS.UnityEngine.Windows.Speech.PhraseRecognizer = UnityEngine.Windows.Speech.PhraseRecognizer

function UnityEngine.Windows.Speech.PhraseRecognizer:Start() end
function UnityEngine.Windows.Speech.PhraseRecognizer:Stop() end
function UnityEngine.Windows.Speech.PhraseRecognizer:Dispose() end

---@class UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate : System.MulticastDelegate
UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate = {}
---@alias CS.UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate
CS.UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate = UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate
function UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate.New(object, method) end
---@param args UnityEngine.Windows.Speech.PhraseRecognizedEventArgs
function UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate:Invoke(args) end
---@param args UnityEngine.Windows.Speech.PhraseRecognizedEventArgs
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate:BeginInvoke(args, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.Speech.PhraseRecognizer.PhraseRecognizedDelegate:EndInvoke(result) end

---@class UnityEngine.Windows.Speech.SemanticMeaning : System.ValueType
---@field key string
---@field values string[]
UnityEngine.Windows.Speech.SemanticMeaning = {}
---@alias CS.UnityEngine.Windows.Speech.SemanticMeaning UnityEngine.Windows.Speech.SemanticMeaning
CS.UnityEngine.Windows.Speech.SemanticMeaning = UnityEngine.Windows.Speech.SemanticMeaning


---@class UnityEngine.Windows.Speech.SpeechError
---@field NoError UnityEngine.Windows.Speech.SpeechError
---@field TopicLanguageNotSupported UnityEngine.Windows.Speech.SpeechError
---@field GrammarLanguageMismatch UnityEngine.Windows.Speech.SpeechError
---@field GrammarCompilationFailure UnityEngine.Windows.Speech.SpeechError
---@field AudioQualityFailure UnityEngine.Windows.Speech.SpeechError
---@field PauseLimitExceeded UnityEngine.Windows.Speech.SpeechError
---@field TimeoutExceeded UnityEngine.Windows.Speech.SpeechError
---@field NetworkFailure UnityEngine.Windows.Speech.SpeechError
---@field MicrophoneUnavailable UnityEngine.Windows.Speech.SpeechError
---@field UnknownError UnityEngine.Windows.Speech.SpeechError
UnityEngine.Windows.Speech.SpeechError = {}
---@alias CS.UnityEngine.Windows.Speech.SpeechError UnityEngine.Windows.Speech.SpeechError
CS.UnityEngine.Windows.Speech.SpeechError = UnityEngine.Windows.Speech.SpeechError


---@class UnityEngine.Windows.Speech.SpeechSystemStatus
---@field Stopped UnityEngine.Windows.Speech.SpeechSystemStatus
---@field Running UnityEngine.Windows.Speech.SpeechSystemStatus
---@field Failed UnityEngine.Windows.Speech.SpeechSystemStatus
UnityEngine.Windows.Speech.SpeechSystemStatus = {}
---@alias CS.UnityEngine.Windows.Speech.SpeechSystemStatus UnityEngine.Windows.Speech.SpeechSystemStatus
CS.UnityEngine.Windows.Speech.SpeechSystemStatus = UnityEngine.Windows.Speech.SpeechSystemStatus


---@class UnityEngine.Windows.WebCam.CameraParameters : System.ValueType
---@field hologramOpacity number
---@field frameRate number
---@field cameraResolutionWidth number
---@field cameraResolutionHeight number
---@field pixelFormat UnityEngine.Windows.WebCam.CapturePixelFormat
UnityEngine.Windows.WebCam.CameraParameters = {}
---@alias CS.UnityEngine.Windows.WebCam.CameraParameters UnityEngine.Windows.WebCam.CameraParameters
CS.UnityEngine.Windows.WebCam.CameraParameters = UnityEngine.Windows.WebCam.CameraParameters

---@param webCamMode UnityEngine.Windows.WebCam.WebCamMode
---@return UnityEngine.Windows.WebCam.CameraParameters
function UnityEngine.Windows.WebCam.CameraParameters.New(webCamMode) end

---@class UnityEngine.Windows.WebCam.CapturePixelFormat
---@field BGRA32 UnityEngine.Windows.WebCam.CapturePixelFormat
---@field NV12 UnityEngine.Windows.WebCam.CapturePixelFormat
---@field JPEG UnityEngine.Windows.WebCam.CapturePixelFormat
---@field PNG UnityEngine.Windows.WebCam.CapturePixelFormat
UnityEngine.Windows.WebCam.CapturePixelFormat = {}
---@alias CS.UnityEngine.Windows.WebCam.CapturePixelFormat UnityEngine.Windows.WebCam.CapturePixelFormat
CS.UnityEngine.Windows.WebCam.CapturePixelFormat = UnityEngine.Windows.WebCam.CapturePixelFormat


---@class UnityEngine.Windows.WebCam.PhotoCapture : System.Object
---@field SupportedResolutions System.Collections.Generic.IEnumerable
UnityEngine.Windows.WebCam.PhotoCapture = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCapture UnityEngine.Windows.WebCam.PhotoCapture
CS.UnityEngine.Windows.WebCam.PhotoCapture = UnityEngine.Windows.WebCam.PhotoCapture

---@overload fun(showHolograms: boolean, onCreatedCallback: UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback)
---@param onCreatedCallback UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback
function UnityEngine.Windows.WebCam.PhotoCapture.CreateAsync(onCreatedCallback) end
---@param setupParams UnityEngine.Windows.WebCam.CameraParameters
---@param onPhotoModeStartedCallback UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback
function UnityEngine.Windows.WebCam.PhotoCapture:StartPhotoModeAsync(setupParams, onPhotoModeStartedCallback) end
---@param onPhotoModeStoppedCallback UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback
function UnityEngine.Windows.WebCam.PhotoCapture:StopPhotoModeAsync(onPhotoModeStoppedCallback) end
---@overload fun(self: UnityEngine.Windows.WebCam.PhotoCapture, filename: string, fileOutputFormat: UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat, onCapturedPhotoToDiskCallback: UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback)
---@param onCapturedPhotoToMemoryCallback UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback
function UnityEngine.Windows.WebCam.PhotoCapture:TakePhotoAsync(onCapturedPhotoToMemoryCallback) end
---@return System.IntPtr
function UnityEngine.Windows.WebCam.PhotoCapture:GetUnsafePointerToVideoDeviceController() end
function UnityEngine.Windows.WebCam.PhotoCapture:Dispose() end

---@class UnityEngine.Windows.WebCam.PhotoCapture.CaptureResultType
---@field Success UnityEngine.Windows.WebCam.PhotoCapture.CaptureResultType
---@field UnknownError UnityEngine.Windows.WebCam.PhotoCapture.CaptureResultType
UnityEngine.Windows.WebCam.PhotoCapture.CaptureResultType = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCapture.CaptureResultType UnityEngine.Windows.WebCam.PhotoCapture.CaptureResultType
CS.UnityEngine.Windows.WebCam.PhotoCapture.CaptureResultType = UnityEngine.Windows.WebCam.PhotoCapture.CaptureResultType


---@class UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback
CS.UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback = UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback
function UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback.New(object, method) end
---@param result UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback:Invoke(result) end
---@param result UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback:BeginInvoke(result, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToDiskCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback
CS.UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback = UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback
function UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback.New(object, method) end
---@param result UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult
---@param photoCaptureFrame UnityEngine.Windows.WebCam.PhotoCaptureFrame
function UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback:Invoke(result, photoCaptureFrame) end
---@param result UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult
---@param photoCaptureFrame UnityEngine.Windows.WebCam.PhotoCaptureFrame
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback:BeginInvoke(result, photoCaptureFrame, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnCapturedToMemoryCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback
CS.UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback = UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback
function UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback.New(object, method) end
---@param captureObject UnityEngine.Windows.WebCam.PhotoCapture
function UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback:Invoke(captureObject) end
---@param captureObject UnityEngine.Windows.WebCam.PhotoCapture
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback:BeginInvoke(captureObject, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnCaptureResourceCreatedCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback
CS.UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback = UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback
function UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback.New(object, method) end
---@param result UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback:Invoke(result) end
---@param result UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback:BeginInvoke(result, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStartedCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback
CS.UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback = UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback
function UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback.New(object, method) end
---@param result UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback:Invoke(result) end
---@param result UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback:BeginInvoke(result, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.PhotoCapture.OnPhotoModeStoppedCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult : System.ValueType
---@field resultType UnityEngine.Windows.WebCam.PhotoCapture.CaptureResultType
---@field hResult number
---@field success boolean
UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult
CS.UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult = UnityEngine.Windows.WebCam.PhotoCapture.PhotoCaptureResult


---@class UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat
---@field PNG UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat
---@field JPG UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat
UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat
CS.UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat = UnityEngine.Windows.WebCam.PhotoCaptureFileOutputFormat


---@class UnityEngine.Windows.WebCam.PhotoCaptureFrame : System.Object
---@field dataLength number
---@field hasLocationData boolean
---@field pixelFormat UnityEngine.Windows.WebCam.CapturePixelFormat
UnityEngine.Windows.WebCam.PhotoCaptureFrame = {}
---@alias CS.UnityEngine.Windows.WebCam.PhotoCaptureFrame UnityEngine.Windows.WebCam.PhotoCaptureFrame
CS.UnityEngine.Windows.WebCam.PhotoCaptureFrame = UnityEngine.Windows.WebCam.PhotoCaptureFrame

---@param out_cameraToWorldMatrix UnityEngine.Matrix4x4
---@return boolean, UnityEngine.Matrix4x4
function UnityEngine.Windows.WebCam.PhotoCaptureFrame:TryGetCameraToWorldMatrix(out_cameraToWorldMatrix) end
---@overload fun(self: UnityEngine.Windows.WebCam.PhotoCaptureFrame, out_projectionMatrix: UnityEngine.Matrix4x4) : boolean, UnityEngine.Matrix4x4
---@param nearClipPlane number
---@param farClipPlane number
---@param out_projectionMatrix UnityEngine.Matrix4x4
---@return boolean, UnityEngine.Matrix4x4
function UnityEngine.Windows.WebCam.PhotoCaptureFrame:TryGetProjectionMatrix(nearClipPlane, farClipPlane, out_projectionMatrix) end
---@param targetTexture UnityEngine.Texture2D
function UnityEngine.Windows.WebCam.PhotoCaptureFrame:UploadImageDataToTexture(targetTexture) end
---@return System.IntPtr
function UnityEngine.Windows.WebCam.PhotoCaptureFrame:GetUnsafePointerToBuffer() end
---@param byteBuffer System.Collections.Generic.List
function UnityEngine.Windows.WebCam.PhotoCaptureFrame:CopyRawImageDataIntoBuffer(byteBuffer) end
function UnityEngine.Windows.WebCam.PhotoCaptureFrame:Dispose() end

---@class UnityEngine.Windows.WebCam.VideoCapture : System.Object
---@field SupportedResolutions System.Collections.Generic.IEnumerable
---@field IsRecording boolean
UnityEngine.Windows.WebCam.VideoCapture = {}
---@alias CS.UnityEngine.Windows.WebCam.VideoCapture UnityEngine.Windows.WebCam.VideoCapture
CS.UnityEngine.Windows.WebCam.VideoCapture = UnityEngine.Windows.WebCam.VideoCapture

---@param resolution UnityEngine.Resolution
---@return System.Collections.Generic.IEnumerable
function UnityEngine.Windows.WebCam.VideoCapture.GetSupportedFrameRatesForResolution(resolution) end
---@overload fun(showHolograms: boolean, onCreatedCallback: UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback)
---@param onCreatedCallback UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback
function UnityEngine.Windows.WebCam.VideoCapture.CreateAsync(onCreatedCallback) end
---@param setupParams UnityEngine.Windows.WebCam.CameraParameters
---@param audioState UnityEngine.Windows.WebCam.VideoCapture.AudioState
---@param onVideoModeStartedCallback UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback
function UnityEngine.Windows.WebCam.VideoCapture:StartVideoModeAsync(setupParams, audioState, onVideoModeStartedCallback) end
---@param onVideoModeStoppedCallback UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback
function UnityEngine.Windows.WebCam.VideoCapture:StopVideoModeAsync(onVideoModeStoppedCallback) end
---@param filename string
---@param onStartedRecordingVideoCallback UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback
function UnityEngine.Windows.WebCam.VideoCapture:StartRecordingAsync(filename, onStartedRecordingVideoCallback) end
---@param onStoppedRecordingVideoCallback UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback
function UnityEngine.Windows.WebCam.VideoCapture:StopRecordingAsync(onStoppedRecordingVideoCallback) end
---@return System.IntPtr
function UnityEngine.Windows.WebCam.VideoCapture:GetUnsafePointerToVideoDeviceController() end
function UnityEngine.Windows.WebCam.VideoCapture:Dispose() end

---@class UnityEngine.Windows.WebCam.VideoCapture.AudioState
---@field MicAudio UnityEngine.Windows.WebCam.VideoCapture.AudioState
---@field ApplicationAudio UnityEngine.Windows.WebCam.VideoCapture.AudioState
---@field ApplicationAndMicAudio UnityEngine.Windows.WebCam.VideoCapture.AudioState
---@field None UnityEngine.Windows.WebCam.VideoCapture.AudioState
UnityEngine.Windows.WebCam.VideoCapture.AudioState = {}
---@alias CS.UnityEngine.Windows.WebCam.VideoCapture.AudioState UnityEngine.Windows.WebCam.VideoCapture.AudioState
CS.UnityEngine.Windows.WebCam.VideoCapture.AudioState = UnityEngine.Windows.WebCam.VideoCapture.AudioState


---@class UnityEngine.Windows.WebCam.VideoCapture.CaptureResultType
---@field Success UnityEngine.Windows.WebCam.VideoCapture.CaptureResultType
---@field UnknownError UnityEngine.Windows.WebCam.VideoCapture.CaptureResultType
UnityEngine.Windows.WebCam.VideoCapture.CaptureResultType = {}
---@alias CS.UnityEngine.Windows.WebCam.VideoCapture.CaptureResultType UnityEngine.Windows.WebCam.VideoCapture.CaptureResultType
CS.UnityEngine.Windows.WebCam.VideoCapture.CaptureResultType = UnityEngine.Windows.WebCam.VideoCapture.CaptureResultType


---@class UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback
CS.UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback = UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback
function UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback.New(object, method) end
---@param result UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult
function UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback:Invoke(result) end
---@param result UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback:BeginInvoke(result, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnStartedRecordingVideoCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback
CS.UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback = UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback
function UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback.New(object, method) end
---@param result UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult
function UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback:Invoke(result) end
---@param result UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback:BeginInvoke(result, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnStoppedRecordingVideoCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback
CS.UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback = UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback.New(object, method) end
---@param captureObject UnityEngine.Windows.WebCam.VideoCapture
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback:Invoke(captureObject) end
---@param captureObject UnityEngine.Windows.WebCam.VideoCapture
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback:BeginInvoke(captureObject, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoCaptureResourceCreatedCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback
CS.UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback = UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback.New(object, method) end
---@param result UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback:Invoke(result) end
---@param result UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback:BeginInvoke(result, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStartedCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback : System.MulticastDelegate
UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback = {}
---@alias CS.UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback
CS.UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback = UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback.New(object, method) end
---@param result UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback:Invoke(result) end
---@param result UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback:BeginInvoke(result, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Windows.WebCam.VideoCapture.OnVideoModeStoppedCallback:EndInvoke(result) end

---@class UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult : System.ValueType
---@field resultType UnityEngine.Windows.WebCam.VideoCapture.CaptureResultType
---@field hResult number
---@field success boolean
UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult = {}
---@alias CS.UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult
CS.UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult = UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult


---@class UnityEngine.Windows.WebCam.WebCam : System.Object
---@field Mode UnityEngine.Windows.WebCam.WebCamMode
UnityEngine.Windows.WebCam.WebCam = {}
---@alias CS.UnityEngine.Windows.WebCam.WebCam UnityEngine.Windows.WebCam.WebCam
CS.UnityEngine.Windows.WebCam.WebCam = UnityEngine.Windows.WebCam.WebCam

---@return UnityEngine.Windows.WebCam.WebCam
function UnityEngine.Windows.WebCam.WebCam.New() end

---@class UnityEngine.Windows.WebCam.WebCamMode
---@field None UnityEngine.Windows.WebCam.WebCamMode
---@field PhotoMode UnityEngine.Windows.WebCam.WebCamMode
---@field VideoMode UnityEngine.Windows.WebCam.WebCamMode
UnityEngine.Windows.WebCam.WebCamMode = {}
---@alias CS.UnityEngine.Windows.WebCam.WebCamMode UnityEngine.Windows.WebCam.WebCamMode
CS.UnityEngine.Windows.WebCam.WebCamMode = UnityEngine.Windows.WebCam.WebCamMode


---@class UnityEngine.WindZone : UnityEngine.Component
---@field mode UnityEngine.WindZoneMode
---@field radius number
---@field windMain number
---@field windTurbulence number
---@field windPulseMagnitude number
---@field windPulseFrequency number
UnityEngine.WindZone = {}
---@alias CS.UnityEngine.WindZone UnityEngine.WindZone
CS.UnityEngine.WindZone = UnityEngine.WindZone

---@return UnityEngine.WindZone
function UnityEngine.WindZone.New() end

---@class UnityEngine.WindZoneMode
---@field Directional UnityEngine.WindZoneMode
---@field Spherical UnityEngine.WindZoneMode
UnityEngine.WindZoneMode = {}
---@alias CS.UnityEngine.WindZoneMode UnityEngine.WindZoneMode
CS.UnityEngine.WindZoneMode = UnityEngine.WindZoneMode


---@class UnityEngine.WrapMode
---@field Once UnityEngine.WrapMode
---@field Loop UnityEngine.WrapMode
---@field PingPong UnityEngine.WrapMode
---@field Default UnityEngine.WrapMode
---@field ClampForever UnityEngine.WrapMode
---@field Clamp UnityEngine.WrapMode
UnityEngine.WrapMode = {}
---@alias CS.UnityEngine.WrapMode UnityEngine.WrapMode
CS.UnityEngine.WrapMode = UnityEngine.WrapMode


---@class UnityEngine.WritableAttribute : System.Attribute
UnityEngine.WritableAttribute = {}
---@alias CS.UnityEngine.WritableAttribute UnityEngine.WritableAttribute
CS.UnityEngine.WritableAttribute = UnityEngine.WritableAttribute

---@return UnityEngine.WritableAttribute
function UnityEngine.WritableAttribute.New() end

---@class UnityEngine.WSA.AppCallbackItem : System.MulticastDelegate
UnityEngine.WSA.AppCallbackItem = {}
---@alias CS.UnityEngine.WSA.AppCallbackItem UnityEngine.WSA.AppCallbackItem
CS.UnityEngine.WSA.AppCallbackItem = UnityEngine.WSA.AppCallbackItem

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.WSA.AppCallbackItem
function UnityEngine.WSA.AppCallbackItem.New(object, method) end
function UnityEngine.WSA.AppCallbackItem:Invoke() end
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.WSA.AppCallbackItem:BeginInvoke(callback, object) end
---@param result System.IAsyncResult
function UnityEngine.WSA.AppCallbackItem:EndInvoke(result) end

---@class UnityEngine.WSA.Application : System.Object
---@field arguments string
---@field advertisingIdentifier string
UnityEngine.WSA.Application = {}
---@alias CS.UnityEngine.WSA.Application UnityEngine.WSA.Application
CS.UnityEngine.WSA.Application = UnityEngine.WSA.Application

---@return UnityEngine.WSA.Application
function UnityEngine.WSA.Application.New() end
---@param item UnityEngine.WSA.AppCallbackItem
---@param waitUntilDone boolean
function UnityEngine.WSA.Application.InvokeOnAppThread(item, waitUntilDone) end
---@param item UnityEngine.WSA.AppCallbackItem
---@param waitUntilDone boolean
function UnityEngine.WSA.Application.InvokeOnUIThread(item, waitUntilDone) end
---@return boolean
function UnityEngine.WSA.Application.RunningOnAppThread() end
---@return boolean
function UnityEngine.WSA.Application.RunningOnUIThread() end

---@class UnityEngine.WSA.Cursor : System.Object
UnityEngine.WSA.Cursor = {}
---@alias CS.UnityEngine.WSA.Cursor UnityEngine.WSA.Cursor
CS.UnityEngine.WSA.Cursor = UnityEngine.WSA.Cursor

---@param id number
function UnityEngine.WSA.Cursor.SetCustomCursor(id) end

---@class UnityEngine.WSA.Folder
---@field Installation UnityEngine.WSA.Folder
---@field Temporary UnityEngine.WSA.Folder
---@field Local UnityEngine.WSA.Folder
---@field Roaming UnityEngine.WSA.Folder
---@field CameraRoll UnityEngine.WSA.Folder
---@field DocumentsLibrary UnityEngine.WSA.Folder
---@field HomeGroup UnityEngine.WSA.Folder
---@field MediaServerDevices UnityEngine.WSA.Folder
---@field MusicLibrary UnityEngine.WSA.Folder
---@field PicturesLibrary UnityEngine.WSA.Folder
---@field Playlists UnityEngine.WSA.Folder
---@field RemovableDevices UnityEngine.WSA.Folder
---@field SavedPictures UnityEngine.WSA.Folder
---@field VideosLibrary UnityEngine.WSA.Folder
UnityEngine.WSA.Folder = {}
---@alias CS.UnityEngine.WSA.Folder UnityEngine.WSA.Folder
CS.UnityEngine.WSA.Folder = UnityEngine.WSA.Folder


---@class UnityEngine.WSA.Launcher : System.Object
UnityEngine.WSA.Launcher = {}
---@alias CS.UnityEngine.WSA.Launcher UnityEngine.WSA.Launcher
CS.UnityEngine.WSA.Launcher = UnityEngine.WSA.Launcher

---@return UnityEngine.WSA.Launcher
function UnityEngine.WSA.Launcher.New() end
---@param folder UnityEngine.WSA.Folder
---@param relativeFilePath string
---@param showWarning boolean
function UnityEngine.WSA.Launcher.LaunchFile(folder, relativeFilePath, showWarning) end
---@param fileExtension string
function UnityEngine.WSA.Launcher.LaunchFileWithPicker(fileExtension) end
---@param uri string
---@param showWarning boolean
function UnityEngine.WSA.Launcher.LaunchUri(uri, showWarning) end

---@class UnityEngine.WSA.SecondaryTileData : System.ValueType
---@field arguments string
---@field backgroundColorSet boolean
---@field displayName string
---@field foregroundText UnityEngine.WSA.TileForegroundText
---@field lockScreenBadgeLogo string
---@field lockScreenDisplayBadgeAndTileText boolean
---@field phoneticName string
---@field roamingEnabled boolean
---@field showNameOnSquare150x150Logo boolean
---@field showNameOnSquare310x310Logo boolean
---@field showNameOnWide310x150Logo boolean
---@field square150x150Logo string
---@field square30x30Logo string
---@field square310x310Logo string
---@field square70x70Logo string
---@field tileId string
---@field wide310x150Logo string
---@field backgroundColor UnityEngine.Color32
UnityEngine.WSA.SecondaryTileData = {}
---@alias CS.UnityEngine.WSA.SecondaryTileData UnityEngine.WSA.SecondaryTileData
CS.UnityEngine.WSA.SecondaryTileData = UnityEngine.WSA.SecondaryTileData

---@param id string
---@param displayName string
---@return UnityEngine.WSA.SecondaryTileData
function UnityEngine.WSA.SecondaryTileData.New(id, displayName) end

---@class UnityEngine.WSA.Tile : System.Object
---@field main UnityEngine.WSA.Tile
---@field id string
---@field hasUserConsent boolean
---@field exists boolean
UnityEngine.WSA.Tile = {}
---@alias CS.UnityEngine.WSA.Tile UnityEngine.WSA.Tile
CS.UnityEngine.WSA.Tile = UnityEngine.WSA.Tile

---@param templ UnityEngine.WSA.TileTemplate
---@return string
function UnityEngine.WSA.Tile.GetTemplate(templ) end
---@param tileId string
---@return boolean
function UnityEngine.WSA.Tile.Exists(tileId) end
---@overload fun(data: UnityEngine.WSA.SecondaryTileData) : UnityEngine.WSA.Tile
---@overload fun(data: UnityEngine.WSA.SecondaryTileData, pos: UnityEngine.Vector2) : UnityEngine.WSA.Tile
---@param data UnityEngine.WSA.SecondaryTileData
---@param area UnityEngine.Rect
---@return UnityEngine.WSA.Tile
function UnityEngine.WSA.Tile.CreateOrUpdateSecondary(data, area) end
---@param tileId string
---@return UnityEngine.WSA.Tile
function UnityEngine.WSA.Tile.GetSecondary(tileId) end
---@return UnityEngine.WSA.Tile[]
function UnityEngine.WSA.Tile.GetSecondaries() end
---@overload fun(tileId: string)
---@overload fun(tileId: string, pos: UnityEngine.Vector2)
---@param tileId string
---@param area UnityEngine.Rect
function UnityEngine.WSA.Tile.DeleteSecondary(tileId, area) end
---@overload fun(self: UnityEngine.WSA.Tile, xml: string)
---@param medium string
---@param wide string
---@param large string
---@param text string
function UnityEngine.WSA.Tile:Update(medium, wide, large, text) end
---@param uri string
---@param interval number
function UnityEngine.WSA.Tile:PeriodicUpdate(uri, interval) end
function UnityEngine.WSA.Tile:StopPeriodicUpdate() end
---@param image string
function UnityEngine.WSA.Tile:UpdateBadgeImage(image) end
---@param number number
function UnityEngine.WSA.Tile:UpdateBadgeNumber(number) end
function UnityEngine.WSA.Tile:RemoveBadge() end
---@param uri string
---@param interval number
function UnityEngine.WSA.Tile:PeriodicBadgeUpdate(uri, interval) end
function UnityEngine.WSA.Tile:StopPeriodicBadgeUpdate() end
---@overload fun(self: UnityEngine.WSA.Tile)
---@overload fun(self: UnityEngine.WSA.Tile, pos: UnityEngine.Vector2)
---@param area UnityEngine.Rect
function UnityEngine.WSA.Tile:Delete(area) end

---@class UnityEngine.WSA.TileForegroundText
---@field Default UnityEngine.WSA.TileForegroundText
---@field Dark UnityEngine.WSA.TileForegroundText
---@field Light UnityEngine.WSA.TileForegroundText
UnityEngine.WSA.TileForegroundText = {}
---@alias CS.UnityEngine.WSA.TileForegroundText UnityEngine.WSA.TileForegroundText
CS.UnityEngine.WSA.TileForegroundText = UnityEngine.WSA.TileForegroundText


---@class UnityEngine.WSA.TileTemplate
---@field TileSquare150x150Image UnityEngine.WSA.TileTemplate
---@field TileSquare150x150Block UnityEngine.WSA.TileTemplate
---@field TileSquare150x150Text01 UnityEngine.WSA.TileTemplate
---@field TileSquare150x150Text02 UnityEngine.WSA.TileTemplate
---@field TileSquare150x150Text03 UnityEngine.WSA.TileTemplate
---@field TileSquare150x150Text04 UnityEngine.WSA.TileTemplate
---@field TileSquare150x150PeekImageAndText01 UnityEngine.WSA.TileTemplate
---@field TileSquare150x150PeekImageAndText02 UnityEngine.WSA.TileTemplate
---@field TileSquare150x150PeekImageAndText03 UnityEngine.WSA.TileTemplate
---@field TileSquare150x150PeekImageAndText04 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Image UnityEngine.WSA.TileTemplate
---@field TileWide310x150ImageCollection UnityEngine.WSA.TileTemplate
---@field TileWide310x150ImageAndText01 UnityEngine.WSA.TileTemplate
---@field TileWide310x150ImageAndText02 UnityEngine.WSA.TileTemplate
---@field TileWide310x150BlockAndText01 UnityEngine.WSA.TileTemplate
---@field TileWide310x150BlockAndText02 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImageCollection01 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImageCollection02 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImageCollection03 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImageCollection04 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImageCollection05 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImageCollection06 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImageAndText01 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImageAndText02 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImage01 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImage02 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImage03 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImage04 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImage05 UnityEngine.WSA.TileTemplate
---@field TileWide310x150PeekImage06 UnityEngine.WSA.TileTemplate
---@field TileWide310x150SmallImageAndText01 UnityEngine.WSA.TileTemplate
---@field TileWide310x150SmallImageAndText02 UnityEngine.WSA.TileTemplate
---@field TileWide310x150SmallImageAndText03 UnityEngine.WSA.TileTemplate
---@field TileWide310x150SmallImageAndText04 UnityEngine.WSA.TileTemplate
---@field TileWide310x150SmallImageAndText05 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text01 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text02 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text03 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text04 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text05 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text06 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text07 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text08 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text09 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text10 UnityEngine.WSA.TileTemplate
---@field TileWide310x150Text11 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310BlockAndText01 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310BlockAndText02 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Image UnityEngine.WSA.TileTemplate
---@field TileSquare310x310ImageAndText01 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310ImageAndText02 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310ImageAndTextOverlay01 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310ImageAndTextOverlay02 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310ImageAndTextOverlay03 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310ImageCollectionAndText01 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310ImageCollectionAndText02 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310ImageCollection UnityEngine.WSA.TileTemplate
---@field TileSquare310x310SmallImagesAndTextList01 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310SmallImagesAndTextList02 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310SmallImagesAndTextList03 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310SmallImagesAndTextList04 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Text01 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Text02 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Text03 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Text04 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Text05 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Text06 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Text07 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Text08 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310TextList01 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310TextList02 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310TextList03 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310SmallImageAndText01 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310SmallImagesAndTextList05 UnityEngine.WSA.TileTemplate
---@field TileSquare310x310Text09 UnityEngine.WSA.TileTemplate
---@field TileSquare71x71IconWithBadge UnityEngine.WSA.TileTemplate
---@field TileSquare150x150IconWithBadge UnityEngine.WSA.TileTemplate
---@field TileWide310x150IconWithBadgeAndText UnityEngine.WSA.TileTemplate
---@field TileSquare71x71Image UnityEngine.WSA.TileTemplate
---@field TileTall150x310Image UnityEngine.WSA.TileTemplate
---@field TileSquare99x99IconWithBadge UnityEngine.WSA.TileTemplate
---@field TileSquare210x210IconWithBadge UnityEngine.WSA.TileTemplate
---@field TileWide432x210IconWithBadgeAndText UnityEngine.WSA.TileTemplate
UnityEngine.WSA.TileTemplate = {}
---@alias CS.UnityEngine.WSA.TileTemplate UnityEngine.WSA.TileTemplate
CS.UnityEngine.WSA.TileTemplate = UnityEngine.WSA.TileTemplate


---@class UnityEngine.WSA.Toast : System.Object
---@field arguments string
---@field activated boolean
---@field dismissed boolean
---@field dismissedByUser boolean
UnityEngine.WSA.Toast = {}
---@alias CS.UnityEngine.WSA.Toast UnityEngine.WSA.Toast
CS.UnityEngine.WSA.Toast = UnityEngine.WSA.Toast

---@param templ UnityEngine.WSA.ToastTemplate
---@return string
function UnityEngine.WSA.Toast.GetTemplate(templ) end
---@overload fun(xml: string) : UnityEngine.WSA.Toast
---@param image string
---@param text string
---@return UnityEngine.WSA.Toast
function UnityEngine.WSA.Toast.Create(image, text) end
function UnityEngine.WSA.Toast:Show() end
function UnityEngine.WSA.Toast:Hide() end

---@class UnityEngine.WSA.ToastTemplate
---@field ToastImageAndText01 UnityEngine.WSA.ToastTemplate
---@field ToastImageAndText02 UnityEngine.WSA.ToastTemplate
---@field ToastImageAndText03 UnityEngine.WSA.ToastTemplate
---@field ToastImageAndText04 UnityEngine.WSA.ToastTemplate
---@field ToastText01 UnityEngine.WSA.ToastTemplate
---@field ToastText02 UnityEngine.WSA.ToastTemplate
---@field ToastText03 UnityEngine.WSA.ToastTemplate
---@field ToastText04 UnityEngine.WSA.ToastTemplate
UnityEngine.WSA.ToastTemplate = {}
---@alias CS.UnityEngine.WSA.ToastTemplate UnityEngine.WSA.ToastTemplate
CS.UnityEngine.WSA.ToastTemplate = UnityEngine.WSA.ToastTemplate


---@class UnityEngine.WSA.WindowActivated : System.MulticastDelegate
UnityEngine.WSA.WindowActivated = {}
---@alias CS.UnityEngine.WSA.WindowActivated UnityEngine.WSA.WindowActivated
CS.UnityEngine.WSA.WindowActivated = UnityEngine.WSA.WindowActivated

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.WSA.WindowActivated
function UnityEngine.WSA.WindowActivated.New(object, method) end
---@param state UnityEngine.WSA.WindowActivationState
function UnityEngine.WSA.WindowActivated:Invoke(state) end
---@param state UnityEngine.WSA.WindowActivationState
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.WSA.WindowActivated:BeginInvoke(state, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.WSA.WindowActivated:EndInvoke(result) end

---@class UnityEngine.WSA.WindowActivationState
---@field CodeActivated UnityEngine.WSA.WindowActivationState
---@field Deactivated UnityEngine.WSA.WindowActivationState
---@field PointerActivated UnityEngine.WSA.WindowActivationState
UnityEngine.WSA.WindowActivationState = {}
---@alias CS.UnityEngine.WSA.WindowActivationState UnityEngine.WSA.WindowActivationState
CS.UnityEngine.WSA.WindowActivationState = UnityEngine.WSA.WindowActivationState


---@class UnityEngine.WSA.WindowSizeChanged : System.MulticastDelegate
UnityEngine.WSA.WindowSizeChanged = {}
---@alias CS.UnityEngine.WSA.WindowSizeChanged UnityEngine.WSA.WindowSizeChanged
CS.UnityEngine.WSA.WindowSizeChanged = UnityEngine.WSA.WindowSizeChanged

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.WSA.WindowSizeChanged
function UnityEngine.WSA.WindowSizeChanged.New(object, method) end
---@param width number
---@param height number
function UnityEngine.WSA.WindowSizeChanged:Invoke(width, height) end
---@param width number
---@param height number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.WSA.WindowSizeChanged:BeginInvoke(width, height, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.WSA.WindowSizeChanged:EndInvoke(result) end

---@class UnityEngine.WWW : UnityEngine.CustomYieldInstruction
---@field assetBundle UnityEngine.AssetBundle
---@field bytes number[]
---@field bytesDownloaded number
---@field error string
---@field isDone boolean
---@field progress number
---@field responseHeaders System.Collections.Generic.Dictionary
---@field text string
---@field texture UnityEngine.Texture2D
---@field textureNonReadable UnityEngine.Texture2D
---@field threadPriority UnityEngine.ThreadPriority
---@field uploadProgress number
---@field url string
---@field keepWaiting boolean
UnityEngine.WWW = {}
---@alias CS.UnityEngine.WWW UnityEngine.WWW
CS.UnityEngine.WWW = UnityEngine.WWW

---@overload fun(url: string) : UnityEngine.WWW
---@overload fun(url: string, form: UnityEngine.WWWForm) : UnityEngine.WWW
---@overload fun(url: string, postData: number[]) : UnityEngine.WWW
---@overload fun(url: string, postData: number[], headers: System.Collections.Hashtable) : UnityEngine.WWW
---@param url string
---@param postData number[]
---@param headers System.Collections.Generic.Dictionary
---@return UnityEngine.WWW
function UnityEngine.WWW.New(url, postData, headers) end
---@overload fun(s: string) : string
---@param s string
---@param e System.Text.Encoding
---@return string
function UnityEngine.WWW.EscapeURL(s, e) end
---@overload fun(s: string) : string
---@param s string
---@param e System.Text.Encoding
---@return string
function UnityEngine.WWW.UnEscapeURL(s, e) end
---@overload fun(url: string, version: number) : UnityEngine.WWW
---@overload fun(url: string, version: number, crc: number) : UnityEngine.WWW
---@overload fun(url: string, hash: UnityEngine.Hash128) : UnityEngine.WWW
---@overload fun(url: string, hash: UnityEngine.Hash128, crc: number) : UnityEngine.WWW
---@param url string
---@param cachedBundle UnityEngine.CachedAssetBundle
---@param crc number
---@return UnityEngine.WWW
function UnityEngine.WWW.LoadFromCacheOrDownload(url, cachedBundle, crc) end
---@param texture UnityEngine.Texture2D
function UnityEngine.WWW:LoadImageIntoTexture(texture) end
function UnityEngine.WWW:Dispose() end
---@overload fun(self: UnityEngine.WWW) : UnityEngine.AudioClip
---@overload fun(self: UnityEngine.WWW, threeD: boolean) : UnityEngine.AudioClip
---@overload fun(self: UnityEngine.WWW, threeD: boolean, stream: boolean) : UnityEngine.AudioClip
---@param threeD boolean
---@param stream boolean
---@param audioType UnityEngine.AudioType
---@return UnityEngine.AudioClip
function UnityEngine.WWW:GetAudioClip(threeD, stream, audioType) end
---@overload fun(self: UnityEngine.WWW) : UnityEngine.AudioClip
---@overload fun(self: UnityEngine.WWW, threeD: boolean) : UnityEngine.AudioClip
---@param threeD boolean
---@param audioType UnityEngine.AudioType
---@return UnityEngine.AudioClip
function UnityEngine.WWW:GetAudioClipCompressed(threeD, audioType) end
---@return UnityEngine.AudioClip
function UnityEngine.WWW:GetAudioClip() end
---@param threeD boolean
---@return UnityEngine.AudioClip
function UnityEngine.WWW:GetAudioClip(threeD) end
---@param threeD boolean
---@param stream boolean
---@return UnityEngine.AudioClip
function UnityEngine.WWW:GetAudioClip(threeD, stream) end
---@param threeD boolean
---@param stream boolean
---@param audioType UnityEngine.AudioType
---@return UnityEngine.AudioClip
function UnityEngine.WWW:GetAudioClip(threeD, stream, audioType) end
---@return UnityEngine.AudioClip
function UnityEngine.WWW:GetAudioClipCompressed() end
---@param threeD boolean
---@return UnityEngine.AudioClip
function UnityEngine.WWW:GetAudioClipCompressed(threeD) end
---@param threeD boolean
---@param audioType UnityEngine.AudioType
---@return UnityEngine.AudioClip
function UnityEngine.WWW:GetAudioClipCompressed(threeD, audioType) end
---@return UnityEngine.MovieTexture
function UnityEngine.WWW:GetMovieTexture() end

---@class UnityEngine.WWWAudioExtensions : System.Object
UnityEngine.WWWAudioExtensions = {}
---@alias CS.UnityEngine.WWWAudioExtensions UnityEngine.WWWAudioExtensions
CS.UnityEngine.WWWAudioExtensions = UnityEngine.WWWAudioExtensions


---@class UnityEngine.WWWForm : System.Object
---@field headers System.Collections.Generic.Dictionary
---@field data number[]
UnityEngine.WWWForm = {}
---@alias CS.UnityEngine.WWWForm UnityEngine.WWWForm
CS.UnityEngine.WWWForm = UnityEngine.WWWForm

---@return UnityEngine.WWWForm
function UnityEngine.WWWForm.New() end
---@overload fun(self: UnityEngine.WWWForm, fieldName: string, value: string)
---@overload fun(self: UnityEngine.WWWForm, fieldName: string, value: string, e: System.Text.Encoding)
---@param fieldName string
---@param i number
function UnityEngine.WWWForm:AddField(fieldName, i) end
---@overload fun(self: UnityEngine.WWWForm, fieldName: string, contents: number[])
---@overload fun(self: UnityEngine.WWWForm, fieldName: string, contents: number[], fileName: string)
---@param fieldName string
---@param contents number[]
---@param fileName string
---@param mimeType string
function UnityEngine.WWWForm:AddBinaryData(fieldName, contents, fileName, mimeType) end

---@class UnityEngine.WWWTranscoder : System.Object
UnityEngine.WWWTranscoder = {}
---@alias CS.UnityEngine.WWWTranscoder UnityEngine.WWWTranscoder
CS.UnityEngine.WWWTranscoder = UnityEngine.WWWTranscoder

---@return UnityEngine.WWWTranscoder
function UnityEngine.WWWTranscoder.New() end
---@overload fun(toEncode: string) : string
---@overload fun(toEncode: string, e: System.Text.Encoding) : string
---@param toEncode number[]
---@return number[]
function UnityEngine.WWWTranscoder.URLEncode(toEncode) end
---@overload fun(toEncode: string) : string
---@overload fun(toEncode: string, e: System.Text.Encoding) : string
---@param toEncode number[]
---@return number[]
function UnityEngine.WWWTranscoder.DataEncode(toEncode) end
---@overload fun(toEncode: string) : string
---@overload fun(toEncode: string, e: System.Text.Encoding) : string
---@param toEncode number[]
---@return number[]
function UnityEngine.WWWTranscoder.QPEncode(toEncode) end
---@param input number[]
---@param escapeChar number
---@param space number[]
---@param forbidden number[]
---@param uppercase boolean
---@return number[]
function UnityEngine.WWWTranscoder.Encode(input, escapeChar, space, forbidden, uppercase) end
---@overload fun(toEncode: string) : string
---@overload fun(toEncode: string, e: System.Text.Encoding) : string
---@param toEncode number[]
---@return number[]
function UnityEngine.WWWTranscoder.URLDecode(toEncode) end
---@overload fun(toDecode: string) : string
---@overload fun(toDecode: string, e: System.Text.Encoding) : string
---@param toDecode number[]
---@return number[]
function UnityEngine.WWWTranscoder.DataDecode(toDecode) end
---@overload fun(toEncode: string) : string
---@overload fun(toEncode: string, e: System.Text.Encoding) : string
---@param toEncode number[]
---@return number[]
function UnityEngine.WWWTranscoder.QPDecode(toEncode) end
---@param input number[]
---@param escapeChar number
---@param space number[]
---@return number[]
function UnityEngine.WWWTranscoder.Decode(input, escapeChar, space) end
---@overload fun(s: string) : boolean
---@overload fun(s: string, e: System.Text.Encoding) : boolean
---@param input System.Byte*
---@param inputLength number
---@return boolean
function UnityEngine.WWWTranscoder.SevenBitClean(input, inputLength) end

---@class UnityEngine.XR.AvailableTrackingData
---@field None UnityEngine.XR.AvailableTrackingData
---@field PositionAvailable UnityEngine.XR.AvailableTrackingData
---@field RotationAvailable UnityEngine.XR.AvailableTrackingData
---@field VelocityAvailable UnityEngine.XR.AvailableTrackingData
---@field AngularVelocityAvailable UnityEngine.XR.AvailableTrackingData
---@field AccelerationAvailable UnityEngine.XR.AvailableTrackingData
---@field AngularAccelerationAvailable UnityEngine.XR.AvailableTrackingData
UnityEngine.XR.AvailableTrackingData = {}
---@alias CS.UnityEngine.XR.AvailableTrackingData UnityEngine.XR.AvailableTrackingData
CS.UnityEngine.XR.AvailableTrackingData = UnityEngine.XR.AvailableTrackingData


---@class UnityEngine.XR.Bone : System.ValueType
UnityEngine.XR.Bone = {}
---@alias CS.UnityEngine.XR.Bone UnityEngine.XR.Bone
CS.UnityEngine.XR.Bone = UnityEngine.XR.Bone

---@param out_position UnityEngine.Vector3
---@return boolean, UnityEngine.Vector3
function UnityEngine.XR.Bone:TryGetPosition(out_position) end
---@param out_rotation UnityEngine.Quaternion
---@return boolean, UnityEngine.Quaternion
function UnityEngine.XR.Bone:TryGetRotation(out_rotation) end
---@param out_parentBone UnityEngine.XR.Bone
---@return boolean, UnityEngine.XR.Bone
function UnityEngine.XR.Bone:TryGetParentBone(out_parentBone) end
---@param childBones System.Collections.Generic.List
---@return boolean
function UnityEngine.XR.Bone:TryGetChildBones(childBones) end
---@overload fun(self: UnityEngine.XR.Bone, obj: System.Object) : boolean
---@param other UnityEngine.XR.Bone
---@return boolean
function UnityEngine.XR.Bone:Equals(other) end
---@return number
function UnityEngine.XR.Bone:GetHashCode() end

---@class UnityEngine.XR.CommonUsages : System.Object
---@field isTracked UnityEngine.XR.InputFeatureUsage
---@field primaryButton UnityEngine.XR.InputFeatureUsage
---@field primaryTouch UnityEngine.XR.InputFeatureUsage
---@field secondaryButton UnityEngine.XR.InputFeatureUsage
---@field secondaryTouch UnityEngine.XR.InputFeatureUsage
---@field gripButton UnityEngine.XR.InputFeatureUsage
---@field triggerButton UnityEngine.XR.InputFeatureUsage
---@field menuButton UnityEngine.XR.InputFeatureUsage
---@field primary2DAxisClick UnityEngine.XR.InputFeatureUsage
---@field primary2DAxisTouch UnityEngine.XR.InputFeatureUsage
---@field secondary2DAxisClick UnityEngine.XR.InputFeatureUsage
---@field secondary2DAxisTouch UnityEngine.XR.InputFeatureUsage
---@field userPresence UnityEngine.XR.InputFeatureUsage
---@field trackingState UnityEngine.XR.InputFeatureUsage
---@field batteryLevel UnityEngine.XR.InputFeatureUsage
---@field trigger UnityEngine.XR.InputFeatureUsage
---@field grip UnityEngine.XR.InputFeatureUsage
---@field primary2DAxis UnityEngine.XR.InputFeatureUsage
---@field secondary2DAxis UnityEngine.XR.InputFeatureUsage
---@field devicePosition UnityEngine.XR.InputFeatureUsage
---@field leftEyePosition UnityEngine.XR.InputFeatureUsage
---@field rightEyePosition UnityEngine.XR.InputFeatureUsage
---@field centerEyePosition UnityEngine.XR.InputFeatureUsage
---@field colorCameraPosition UnityEngine.XR.InputFeatureUsage
---@field deviceVelocity UnityEngine.XR.InputFeatureUsage
---@field deviceAngularVelocity UnityEngine.XR.InputFeatureUsage
---@field leftEyeVelocity UnityEngine.XR.InputFeatureUsage
---@field leftEyeAngularVelocity UnityEngine.XR.InputFeatureUsage
---@field rightEyeVelocity UnityEngine.XR.InputFeatureUsage
---@field rightEyeAngularVelocity UnityEngine.XR.InputFeatureUsage
---@field centerEyeVelocity UnityEngine.XR.InputFeatureUsage
---@field centerEyeAngularVelocity UnityEngine.XR.InputFeatureUsage
---@field colorCameraVelocity UnityEngine.XR.InputFeatureUsage
---@field colorCameraAngularVelocity UnityEngine.XR.InputFeatureUsage
---@field deviceAcceleration UnityEngine.XR.InputFeatureUsage
---@field deviceAngularAcceleration UnityEngine.XR.InputFeatureUsage
---@field leftEyeAcceleration UnityEngine.XR.InputFeatureUsage
---@field leftEyeAngularAcceleration UnityEngine.XR.InputFeatureUsage
---@field rightEyeAcceleration UnityEngine.XR.InputFeatureUsage
---@field rightEyeAngularAcceleration UnityEngine.XR.InputFeatureUsage
---@field centerEyeAcceleration UnityEngine.XR.InputFeatureUsage
---@field centerEyeAngularAcceleration UnityEngine.XR.InputFeatureUsage
---@field colorCameraAcceleration UnityEngine.XR.InputFeatureUsage
---@field colorCameraAngularAcceleration UnityEngine.XR.InputFeatureUsage
---@field deviceRotation UnityEngine.XR.InputFeatureUsage
---@field leftEyeRotation UnityEngine.XR.InputFeatureUsage
---@field rightEyeRotation UnityEngine.XR.InputFeatureUsage
---@field centerEyeRotation UnityEngine.XR.InputFeatureUsage
---@field colorCameraRotation UnityEngine.XR.InputFeatureUsage
---@field handData UnityEngine.XR.InputFeatureUsage
---@field eyesData UnityEngine.XR.InputFeatureUsage
UnityEngine.XR.CommonUsages = {}
---@alias CS.UnityEngine.XR.CommonUsages UnityEngine.XR.CommonUsages
CS.UnityEngine.XR.CommonUsages = UnityEngine.XR.CommonUsages


---@class UnityEngine.XR.ConnectionChangeType
---@field Connected UnityEngine.XR.ConnectionChangeType
---@field Disconnected UnityEngine.XR.ConnectionChangeType
---@field ConfigChange UnityEngine.XR.ConnectionChangeType
UnityEngine.XR.ConnectionChangeType = {}
---@alias CS.UnityEngine.XR.ConnectionChangeType UnityEngine.XR.ConnectionChangeType
CS.UnityEngine.XR.ConnectionChangeType = UnityEngine.XR.ConnectionChangeType


---@class UnityEngine.XR.Eyes : System.ValueType
UnityEngine.XR.Eyes = {}
---@alias CS.UnityEngine.XR.Eyes UnityEngine.XR.Eyes
CS.UnityEngine.XR.Eyes = UnityEngine.XR.Eyes

---@param out_position UnityEngine.Vector3
---@return boolean, UnityEngine.Vector3
function UnityEngine.XR.Eyes:TryGetLeftEyePosition(out_position) end
---@param out_position UnityEngine.Vector3
---@return boolean, UnityEngine.Vector3
function UnityEngine.XR.Eyes:TryGetRightEyePosition(out_position) end
---@param out_rotation UnityEngine.Quaternion
---@return boolean, UnityEngine.Quaternion
function UnityEngine.XR.Eyes:TryGetLeftEyeRotation(out_rotation) end
---@param out_rotation UnityEngine.Quaternion
---@return boolean, UnityEngine.Quaternion
function UnityEngine.XR.Eyes:TryGetRightEyeRotation(out_rotation) end
---@param out_fixationPoint UnityEngine.Vector3
---@return boolean, UnityEngine.Vector3
function UnityEngine.XR.Eyes:TryGetFixationPoint(out_fixationPoint) end
---@param out_openAmount number
---@return boolean, number
function UnityEngine.XR.Eyes:TryGetLeftEyeOpenAmount(out_openAmount) end
---@param out_openAmount number
---@return boolean, number
function UnityEngine.XR.Eyes:TryGetRightEyeOpenAmount(out_openAmount) end
---@overload fun(self: UnityEngine.XR.Eyes, obj: System.Object) : boolean
---@param other UnityEngine.XR.Eyes
---@return boolean
function UnityEngine.XR.Eyes:Equals(other) end
---@return number
function UnityEngine.XR.Eyes:GetHashCode() end

---@class UnityEngine.XR.EyeSide
---@field Left UnityEngine.XR.EyeSide
---@field Right UnityEngine.XR.EyeSide
UnityEngine.XR.EyeSide = {}
---@alias CS.UnityEngine.XR.EyeSide UnityEngine.XR.EyeSide
CS.UnityEngine.XR.EyeSide = UnityEngine.XR.EyeSide


---@class UnityEngine.XR.GameViewRenderMode
---@field None UnityEngine.XR.GameViewRenderMode
---@field LeftEye UnityEngine.XR.GameViewRenderMode
---@field RightEye UnityEngine.XR.GameViewRenderMode
---@field BothEyes UnityEngine.XR.GameViewRenderMode
---@field OcclusionMesh UnityEngine.XR.GameViewRenderMode
UnityEngine.XR.GameViewRenderMode = {}
---@alias CS.UnityEngine.XR.GameViewRenderMode UnityEngine.XR.GameViewRenderMode
CS.UnityEngine.XR.GameViewRenderMode = UnityEngine.XR.GameViewRenderMode


---@class UnityEngine.XR.Hand : System.ValueType
UnityEngine.XR.Hand = {}
---@alias CS.UnityEngine.XR.Hand UnityEngine.XR.Hand
CS.UnityEngine.XR.Hand = UnityEngine.XR.Hand

---@param out_boneOut UnityEngine.XR.Bone
---@return boolean, UnityEngine.XR.Bone
function UnityEngine.XR.Hand:TryGetRootBone(out_boneOut) end
---@param finger UnityEngine.XR.HandFinger
---@param bonesOut System.Collections.Generic.List
---@return boolean
function UnityEngine.XR.Hand:TryGetFingerBones(finger, bonesOut) end
---@overload fun(self: UnityEngine.XR.Hand, obj: System.Object) : boolean
---@param other UnityEngine.XR.Hand
---@return boolean
function UnityEngine.XR.Hand:Equals(other) end
---@return number
function UnityEngine.XR.Hand:GetHashCode() end

---@class UnityEngine.XR.HandFinger
---@field Thumb UnityEngine.XR.HandFinger
---@field Index UnityEngine.XR.HandFinger
---@field Middle UnityEngine.XR.HandFinger
---@field Ring UnityEngine.XR.HandFinger
---@field Pinky UnityEngine.XR.HandFinger
UnityEngine.XR.HandFinger = {}
---@alias CS.UnityEngine.XR.HandFinger UnityEngine.XR.HandFinger
CS.UnityEngine.XR.HandFinger = UnityEngine.XR.HandFinger


---@class UnityEngine.XR.HapticCapabilities : System.ValueType
---@field numChannels number
---@field supportsImpulse boolean
---@field supportsBuffer boolean
---@field bufferFrequencyHz number
---@field bufferMaxSize number
---@field bufferOptimalSize number
UnityEngine.XR.HapticCapabilities = {}
---@alias CS.UnityEngine.XR.HapticCapabilities UnityEngine.XR.HapticCapabilities
CS.UnityEngine.XR.HapticCapabilities = UnityEngine.XR.HapticCapabilities

---@overload fun(self: UnityEngine.XR.HapticCapabilities, obj: System.Object) : boolean
---@param other UnityEngine.XR.HapticCapabilities
---@return boolean
function UnityEngine.XR.HapticCapabilities:Equals(other) end
---@return number
function UnityEngine.XR.HapticCapabilities:GetHashCode() end

---@class UnityEngine.XR.HashCodeHelper : System.Object
UnityEngine.XR.HashCodeHelper = {}
---@alias CS.UnityEngine.XR.HashCodeHelper UnityEngine.XR.HashCodeHelper
CS.UnityEngine.XR.HashCodeHelper = UnityEngine.XR.HashCodeHelper

---@overload fun(hash1: number, hash2: number) : number
---@overload fun(hash1: number, hash2: number, hash3: number) : number
---@overload fun(hash1: number, hash2: number, hash3: number, hash4: number) : number
---@overload fun(hash1: number, hash2: number, hash3: number, hash4: number, hash5: number) : number
---@overload fun(hash1: number, hash2: number, hash3: number, hash4: number, hash5: number, hash6: number) : number
---@overload fun(hash1: number, hash2: number, hash3: number, hash4: number, hash5: number, hash6: number, hash7: number) : number
---@param hash1 number
---@param hash2 number
---@param hash3 number
---@param hash4 number
---@param hash5 number
---@param hash6 number
---@param hash7 number
---@param hash8 number
---@return number
function UnityEngine.XR.HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5, hash6, hash7, hash8) end

---@class UnityEngine.XR.InputDevice : System.ValueType
---@field subsystem UnityEngine.XR.XRInputSubsystem
---@field isValid boolean
---@field name string
---@field manufacturer string
---@field serialNumber string
---@field characteristics UnityEngine.XR.InputDeviceCharacteristics
UnityEngine.XR.InputDevice = {}
---@alias CS.UnityEngine.XR.InputDevice UnityEngine.XR.InputDevice
CS.UnityEngine.XR.InputDevice = UnityEngine.XR.InputDevice

---@param channel number
---@param amplitude number
---@param duration number
---@return boolean
function UnityEngine.XR.InputDevice:SendHapticImpulse(channel, amplitude, duration) end
---@param channel number
---@param buffer number[]
---@return boolean
function UnityEngine.XR.InputDevice:SendHapticBuffer(channel, buffer) end
---@param out_capabilities UnityEngine.XR.HapticCapabilities
---@return boolean, UnityEngine.XR.HapticCapabilities
function UnityEngine.XR.InputDevice:TryGetHapticCapabilities(out_capabilities) end
function UnityEngine.XR.InputDevice:StopHaptics() end
---@param featureUsages System.Collections.Generic.List
---@return boolean
function UnityEngine.XR.InputDevice:TryGetFeatureUsages(featureUsages) end
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: boolean) : boolean, boolean
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: number) : boolean, number
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: number) : boolean, number
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: UnityEngine.Vector2) : boolean, UnityEngine.Vector2
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: UnityEngine.Vector3) : boolean, UnityEngine.Vector3
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: UnityEngine.Quaternion) : boolean, UnityEngine.Quaternion
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: UnityEngine.XR.Hand) : boolean, UnityEngine.XR.Hand
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: UnityEngine.XR.Bone) : boolean, UnityEngine.XR.Bone
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: UnityEngine.XR.Eyes) : boolean, UnityEngine.XR.Eyes
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, value: number[]) : boolean
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, out_value: UnityEngine.XR.InputTrackingState) : boolean, UnityEngine.XR.InputTrackingState
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, time: System.DateTime, out_value: boolean) : boolean, boolean
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, time: System.DateTime, out_value: number) : boolean, number
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, time: System.DateTime, out_value: number) : boolean, number
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, time: System.DateTime, out_value: UnityEngine.Vector2) : boolean, UnityEngine.Vector2
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, time: System.DateTime, out_value: UnityEngine.Vector3) : boolean, UnityEngine.Vector3
---@overload fun(self: UnityEngine.XR.InputDevice, usage: UnityEngine.XR.InputFeatureUsage, time: System.DateTime, out_value: UnityEngine.Quaternion) : boolean, UnityEngine.Quaternion
---@param usage UnityEngine.XR.InputFeatureUsage
---@param time System.DateTime
---@param out_value UnityEngine.XR.InputTrackingState
---@return boolean, UnityEngine.XR.InputTrackingState
function UnityEngine.XR.InputDevice:TryGetFeatureValue(usage, time, out_value) end
---@overload fun(self: UnityEngine.XR.InputDevice, obj: System.Object) : boolean
---@param other UnityEngine.XR.InputDevice
---@return boolean
function UnityEngine.XR.InputDevice:Equals(other) end
---@return number
function UnityEngine.XR.InputDevice:GetHashCode() end

---@class UnityEngine.XR.InputDeviceCharacteristics
---@field None UnityEngine.XR.InputDeviceCharacteristics
---@field HeadMounted UnityEngine.XR.InputDeviceCharacteristics
---@field Camera UnityEngine.XR.InputDeviceCharacteristics
---@field HeldInHand UnityEngine.XR.InputDeviceCharacteristics
---@field HandTracking UnityEngine.XR.InputDeviceCharacteristics
---@field EyeTracking UnityEngine.XR.InputDeviceCharacteristics
---@field TrackedDevice UnityEngine.XR.InputDeviceCharacteristics
---@field Controller UnityEngine.XR.InputDeviceCharacteristics
---@field TrackingReference UnityEngine.XR.InputDeviceCharacteristics
---@field Left UnityEngine.XR.InputDeviceCharacteristics
---@field Right UnityEngine.XR.InputDeviceCharacteristics
---@field Simulated6DOF UnityEngine.XR.InputDeviceCharacteristics
UnityEngine.XR.InputDeviceCharacteristics = {}
---@alias CS.UnityEngine.XR.InputDeviceCharacteristics UnityEngine.XR.InputDeviceCharacteristics
CS.UnityEngine.XR.InputDeviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics


---@class UnityEngine.XR.InputDeviceRole
---@field Unknown UnityEngine.XR.InputDeviceRole
---@field Generic UnityEngine.XR.InputDeviceRole
---@field LeftHanded UnityEngine.XR.InputDeviceRole
---@field RightHanded UnityEngine.XR.InputDeviceRole
---@field GameController UnityEngine.XR.InputDeviceRole
---@field TrackingReference UnityEngine.XR.InputDeviceRole
---@field HardwareTracker UnityEngine.XR.InputDeviceRole
---@field LegacyController UnityEngine.XR.InputDeviceRole
UnityEngine.XR.InputDeviceRole = {}
---@alias CS.UnityEngine.XR.InputDeviceRole UnityEngine.XR.InputDeviceRole
CS.UnityEngine.XR.InputDeviceRole = UnityEngine.XR.InputDeviceRole


---@class UnityEngine.XR.InputDevices : System.Object
UnityEngine.XR.InputDevices = {}
---@alias CS.UnityEngine.XR.InputDevices UnityEngine.XR.InputDevices
CS.UnityEngine.XR.InputDevices = UnityEngine.XR.InputDevices

---@return UnityEngine.XR.InputDevices
function UnityEngine.XR.InputDevices.New() end
---@param node UnityEngine.XR.XRNode
---@return UnityEngine.XR.InputDevice
function UnityEngine.XR.InputDevices.GetDeviceAtXRNode(node) end
---@param node UnityEngine.XR.XRNode
---@param inputDevices System.Collections.Generic.List
function UnityEngine.XR.InputDevices.GetDevicesAtXRNode(node, inputDevices) end
---@param inputDevices System.Collections.Generic.List
function UnityEngine.XR.InputDevices.GetDevices(inputDevices) end
---@param desiredCharacteristics UnityEngine.XR.InputDeviceCharacteristics
---@param inputDevices System.Collections.Generic.List
function UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, inputDevices) end

---@class UnityEngine.XR.InputFeatureType
---@field Custom UnityEngine.XR.InputFeatureType
---@field Binary UnityEngine.XR.InputFeatureType
---@field DiscreteStates UnityEngine.XR.InputFeatureType
---@field Axis1D UnityEngine.XR.InputFeatureType
---@field Axis2D UnityEngine.XR.InputFeatureType
---@field Axis3D UnityEngine.XR.InputFeatureType
---@field Rotation UnityEngine.XR.InputFeatureType
---@field Hand UnityEngine.XR.InputFeatureType
---@field Bone UnityEngine.XR.InputFeatureType
---@field Eyes UnityEngine.XR.InputFeatureType
---@field kUnityXRInputFeatureTypeInvalid UnityEngine.XR.InputFeatureType
UnityEngine.XR.InputFeatureType = {}
---@alias CS.UnityEngine.XR.InputFeatureType UnityEngine.XR.InputFeatureType
CS.UnityEngine.XR.InputFeatureType = UnityEngine.XR.InputFeatureType


---@class UnityEngine.XR.InputFeatureUsage : System.ValueType
---@field name string
---@field type System.Type
UnityEngine.XR.InputFeatureUsage = {}
---@alias CS.UnityEngine.XR.InputFeatureUsage UnityEngine.XR.InputFeatureUsage
CS.UnityEngine.XR.InputFeatureUsage = UnityEngine.XR.InputFeatureUsage

---@overload fun(self: UnityEngine.XR.InputFeatureUsage, obj: System.Object) : boolean
---@param other UnityEngine.XR.InputFeatureUsage
---@return boolean
function UnityEngine.XR.InputFeatureUsage:Equals(other) end
---@return number
function UnityEngine.XR.InputFeatureUsage:GetHashCode() end

---@class UnityEngine.XR.InputFeatureUsage : System.ValueType
---@field name string
UnityEngine.XR.InputFeatureUsage = {}
---@alias CS.UnityEngine.XR.InputFeatureUsage UnityEngine.XR.InputFeatureUsage
CS.UnityEngine.XR.InputFeatureUsage = UnityEngine.XR.InputFeatureUsage

---@param usageName string
---@return UnityEngine.XR.InputFeatureUsage
function UnityEngine.XR.InputFeatureUsage.New(usageName) end
---@overload fun(self: UnityEngine.XR.InputFeatureUsage, obj: System.Object) : boolean
---@param other UnityEngine.XR.InputFeatureUsage
---@return boolean
function UnityEngine.XR.InputFeatureUsage:Equals(other) end
---@return number
function UnityEngine.XR.InputFeatureUsage:GetHashCode() end

---@class UnityEngine.XR.InputTracking : System.Object
UnityEngine.XR.InputTracking = {}
---@alias CS.UnityEngine.XR.InputTracking UnityEngine.XR.InputTracking
CS.UnityEngine.XR.InputTracking = UnityEngine.XR.InputTracking

---@param nodeStates System.Collections.Generic.List
function UnityEngine.XR.InputTracking.GetNodeStates(nodeStates) end

---@class UnityEngine.XR.InputTracking.TrackingStateEventType
---@field NodeAdded UnityEngine.XR.InputTracking.TrackingStateEventType
---@field NodeRemoved UnityEngine.XR.InputTracking.TrackingStateEventType
---@field TrackingAcquired UnityEngine.XR.InputTracking.TrackingStateEventType
---@field TrackingLost UnityEngine.XR.InputTracking.TrackingStateEventType
UnityEngine.XR.InputTracking.TrackingStateEventType = {}
---@alias CS.UnityEngine.XR.InputTracking.TrackingStateEventType UnityEngine.XR.InputTracking.TrackingStateEventType
CS.UnityEngine.XR.InputTracking.TrackingStateEventType = UnityEngine.XR.InputTracking.TrackingStateEventType


---@class UnityEngine.XR.InputTrackingState
---@field None UnityEngine.XR.InputTrackingState
---@field Position UnityEngine.XR.InputTrackingState
---@field Rotation UnityEngine.XR.InputTrackingState
---@field Velocity UnityEngine.XR.InputTrackingState
---@field AngularVelocity UnityEngine.XR.InputTrackingState
---@field Acceleration UnityEngine.XR.InputTrackingState
---@field AngularAcceleration UnityEngine.XR.InputTrackingState
---@field All UnityEngine.XR.InputTrackingState
UnityEngine.XR.InputTrackingState = {}
---@alias CS.UnityEngine.XR.InputTrackingState UnityEngine.XR.InputTrackingState
CS.UnityEngine.XR.InputTrackingState = UnityEngine.XR.InputTrackingState


---@class UnityEngine.XR.MeshChangeState
---@field Added UnityEngine.XR.MeshChangeState
---@field Updated UnityEngine.XR.MeshChangeState
---@field Removed UnityEngine.XR.MeshChangeState
---@field Unchanged UnityEngine.XR.MeshChangeState
UnityEngine.XR.MeshChangeState = {}
---@alias CS.UnityEngine.XR.MeshChangeState UnityEngine.XR.MeshChangeState
CS.UnityEngine.XR.MeshChangeState = UnityEngine.XR.MeshChangeState


---@class UnityEngine.XR.MeshGenerationOptions
---@field None UnityEngine.XR.MeshGenerationOptions
---@field ConsumeTransform UnityEngine.XR.MeshGenerationOptions
UnityEngine.XR.MeshGenerationOptions = {}
---@alias CS.UnityEngine.XR.MeshGenerationOptions UnityEngine.XR.MeshGenerationOptions
CS.UnityEngine.XR.MeshGenerationOptions = UnityEngine.XR.MeshGenerationOptions


---@class UnityEngine.XR.MeshGenerationResult : System.ValueType
---@field MeshId UnityEngine.XR.MeshId
---@field Mesh UnityEngine.Mesh
---@field MeshCollider UnityEngine.MeshCollider
---@field Status UnityEngine.XR.MeshGenerationStatus
---@field Attributes UnityEngine.XR.MeshVertexAttributes
---@field Timestamp number
---@field Position UnityEngine.Vector3
---@field Rotation UnityEngine.Quaternion
---@field Scale UnityEngine.Vector3
UnityEngine.XR.MeshGenerationResult = {}
---@alias CS.UnityEngine.XR.MeshGenerationResult UnityEngine.XR.MeshGenerationResult
CS.UnityEngine.XR.MeshGenerationResult = UnityEngine.XR.MeshGenerationResult

---@overload fun(self: UnityEngine.XR.MeshGenerationResult, obj: System.Object) : boolean
---@param other UnityEngine.XR.MeshGenerationResult
---@return boolean
function UnityEngine.XR.MeshGenerationResult:Equals(other) end
---@return number
function UnityEngine.XR.MeshGenerationResult:GetHashCode() end

---@class UnityEngine.XR.MeshGenerationStatus
---@field Success UnityEngine.XR.MeshGenerationStatus
---@field InvalidMeshId UnityEngine.XR.MeshGenerationStatus
---@field GenerationAlreadyInProgress UnityEngine.XR.MeshGenerationStatus
---@field Canceled UnityEngine.XR.MeshGenerationStatus
---@field UnknownError UnityEngine.XR.MeshGenerationStatus
UnityEngine.XR.MeshGenerationStatus = {}
---@alias CS.UnityEngine.XR.MeshGenerationStatus UnityEngine.XR.MeshGenerationStatus
CS.UnityEngine.XR.MeshGenerationStatus = UnityEngine.XR.MeshGenerationStatus


---@class UnityEngine.XR.MeshId : System.ValueType
---@field InvalidId UnityEngine.XR.MeshId
UnityEngine.XR.MeshId = {}
---@alias CS.UnityEngine.XR.MeshId UnityEngine.XR.MeshId
CS.UnityEngine.XR.MeshId = UnityEngine.XR.MeshId

---@return string
function UnityEngine.XR.MeshId:ToString() end
---@return number
function UnityEngine.XR.MeshId:GetHashCode() end
---@overload fun(self: UnityEngine.XR.MeshId, obj: System.Object) : boolean
---@param other UnityEngine.XR.MeshId
---@return boolean
function UnityEngine.XR.MeshId:Equals(other) end

---@class UnityEngine.XR.MeshInfo : System.ValueType
---@field MeshId UnityEngine.XR.MeshId
---@field ChangeState UnityEngine.XR.MeshChangeState
---@field PriorityHint number
UnityEngine.XR.MeshInfo = {}
---@alias CS.UnityEngine.XR.MeshInfo UnityEngine.XR.MeshInfo
CS.UnityEngine.XR.MeshInfo = UnityEngine.XR.MeshInfo

---@overload fun(self: UnityEngine.XR.MeshInfo, obj: System.Object) : boolean
---@param other UnityEngine.XR.MeshInfo
---@return boolean
function UnityEngine.XR.MeshInfo:Equals(other) end
---@return number
function UnityEngine.XR.MeshInfo:GetHashCode() end

---@class UnityEngine.XR.MeshTransform : System.ValueType
---@field MeshId UnityEngine.XR.MeshId
---@field Timestamp number
---@field Position UnityEngine.Vector3
---@field Rotation UnityEngine.Quaternion
---@field Scale UnityEngine.Vector3
UnityEngine.XR.MeshTransform = {}
---@alias CS.UnityEngine.XR.MeshTransform UnityEngine.XR.MeshTransform
CS.UnityEngine.XR.MeshTransform = UnityEngine.XR.MeshTransform

---@param ref_meshId UnityEngine.XR.MeshId
---@param timestamp number
---@param ref_position UnityEngine.Vector3
---@param ref_rotation UnityEngine.Quaternion
---@param ref_scale UnityEngine.Vector3
---@return UnityEngine.XR.MeshTransform, UnityEngine.XR.MeshId, UnityEngine.Vector3, UnityEngine.Quaternion, UnityEngine.Vector3
function UnityEngine.XR.MeshTransform.New(ref_meshId, timestamp, ref_position, ref_rotation, ref_scale) end
---@overload fun(self: UnityEngine.XR.MeshTransform, obj: System.Object) : boolean
---@param other UnityEngine.XR.MeshTransform
---@return boolean
function UnityEngine.XR.MeshTransform:Equals(other) end
---@return number
function UnityEngine.XR.MeshTransform:GetHashCode() end

---@class UnityEngine.XR.MeshVertexAttributes
---@field None UnityEngine.XR.MeshVertexAttributes
---@field Normals UnityEngine.XR.MeshVertexAttributes
---@field Tangents UnityEngine.XR.MeshVertexAttributes
---@field UVs UnityEngine.XR.MeshVertexAttributes
---@field Colors UnityEngine.XR.MeshVertexAttributes
UnityEngine.XR.MeshVertexAttributes = {}
---@alias CS.UnityEngine.XR.MeshVertexAttributes UnityEngine.XR.MeshVertexAttributes
CS.UnityEngine.XR.MeshVertexAttributes = UnityEngine.XR.MeshVertexAttributes


---@class UnityEngine.XR.Provider.XRStats : System.Object
UnityEngine.XR.Provider.XRStats = {}
---@alias CS.UnityEngine.XR.Provider.XRStats UnityEngine.XR.Provider.XRStats
CS.UnityEngine.XR.Provider.XRStats = UnityEngine.XR.Provider.XRStats

---@param xrSubsystem UnityEngine.IntegratedSubsystem
---@param tag string
---@param out_value number
---@return boolean, number
function UnityEngine.XR.Provider.XRStats.TryGetStat(xrSubsystem, tag, out_value) end

---@class UnityEngine.XR.Tango.PoseData : System.ValueType
---@field orientation_x number
---@field orientation_y number
---@field orientation_z number
---@field orientation_w number
---@field translation_x number
---@field translation_y number
---@field translation_z number
---@field statusCode UnityEngine.XR.Tango.PoseStatus
---@field rotation UnityEngine.Quaternion
---@field position UnityEngine.Vector3
UnityEngine.XR.Tango.PoseData = {}
---@alias CS.UnityEngine.XR.Tango.PoseData UnityEngine.XR.Tango.PoseData
CS.UnityEngine.XR.Tango.PoseData = UnityEngine.XR.Tango.PoseData


---@class UnityEngine.XR.Tango.PoseStatus
---@field Initializing UnityEngine.XR.Tango.PoseStatus
---@field Valid UnityEngine.XR.Tango.PoseStatus
---@field Invalid UnityEngine.XR.Tango.PoseStatus
---@field Unknown UnityEngine.XR.Tango.PoseStatus
UnityEngine.XR.Tango.PoseStatus = {}
---@alias CS.UnityEngine.XR.Tango.PoseStatus UnityEngine.XR.Tango.PoseStatus
CS.UnityEngine.XR.Tango.PoseStatus = UnityEngine.XR.Tango.PoseStatus


---@class UnityEngine.XR.Tango.TangoInputTracking : System.Object
UnityEngine.XR.Tango.TangoInputTracking = {}
---@alias CS.UnityEngine.XR.Tango.TangoInputTracking UnityEngine.XR.Tango.TangoInputTracking
CS.UnityEngine.XR.Tango.TangoInputTracking = UnityEngine.XR.Tango.TangoInputTracking


---@class UnityEngine.XR.TimeConverter : System.Object
---@field now System.DateTime
UnityEngine.XR.TimeConverter = {}
---@alias CS.UnityEngine.XR.TimeConverter UnityEngine.XR.TimeConverter
CS.UnityEngine.XR.TimeConverter = UnityEngine.XR.TimeConverter

---@param date System.DateTime
---@return number
function UnityEngine.XR.TimeConverter.LocalDateTimeToUnixTimeMilliseconds(date) end
---@param unixTimeInMilliseconds number
---@return System.DateTime
function UnityEngine.XR.TimeConverter.UnixTimeMillisecondsToLocalDateTime(unixTimeInMilliseconds) end

---@class UnityEngine.XR.TrackingOriginModeFlags
---@field Unknown UnityEngine.XR.TrackingOriginModeFlags
---@field Device UnityEngine.XR.TrackingOriginModeFlags
---@field Floor UnityEngine.XR.TrackingOriginModeFlags
---@field TrackingReference UnityEngine.XR.TrackingOriginModeFlags
---@field Unbounded UnityEngine.XR.TrackingOriginModeFlags
UnityEngine.XR.TrackingOriginModeFlags = {}
---@alias CS.UnityEngine.XR.TrackingOriginModeFlags UnityEngine.XR.TrackingOriginModeFlags
CS.UnityEngine.XR.TrackingOriginModeFlags = UnityEngine.XR.TrackingOriginModeFlags


---@class UnityEngine.XR.TrackingSpaceType
---@field Stationary UnityEngine.XR.TrackingSpaceType
---@field RoomScale UnityEngine.XR.TrackingSpaceType
UnityEngine.XR.TrackingSpaceType = {}
---@alias CS.UnityEngine.XR.TrackingSpaceType UnityEngine.XR.TrackingSpaceType
CS.UnityEngine.XR.TrackingSpaceType = UnityEngine.XR.TrackingSpaceType


---@class UnityEngine.XR.WSA.Input.DeleteMe
---@field Please UnityEngine.XR.WSA.Input.DeleteMe
UnityEngine.XR.WSA.Input.DeleteMe = {}
---@alias CS.UnityEngine.XR.WSA.Input.DeleteMe UnityEngine.XR.WSA.Input.DeleteMe
CS.UnityEngine.XR.WSA.Input.DeleteMe = UnityEngine.XR.WSA.Input.DeleteMe


---@class UnityEngine.XR.WSA.RemoteDeviceVersion
---@field V1 UnityEngine.XR.WSA.RemoteDeviceVersion
---@field V2 UnityEngine.XR.WSA.RemoteDeviceVersion
UnityEngine.XR.WSA.RemoteDeviceVersion = {}
---@alias CS.UnityEngine.XR.WSA.RemoteDeviceVersion UnityEngine.XR.WSA.RemoteDeviceVersion
CS.UnityEngine.XR.WSA.RemoteDeviceVersion = UnityEngine.XR.WSA.RemoteDeviceVersion


---@class UnityEngine.XR.XRDevice : System.Object
---@field refreshRate number
---@field fovZoomFactor number
UnityEngine.XR.XRDevice = {}
---@alias CS.UnityEngine.XR.XRDevice UnityEngine.XR.XRDevice
CS.UnityEngine.XR.XRDevice = UnityEngine.XR.XRDevice

---@return System.IntPtr
function UnityEngine.XR.XRDevice.GetNativePtr() end
---@param camera UnityEngine.Camera
---@param disabled boolean
function UnityEngine.XR.XRDevice.DisableAutoXRCameraTracking(camera, disabled) end
function UnityEngine.XR.XRDevice.UpdateEyeTextureMSAASetting() end

---@class UnityEngine.XR.XRDisplaySubsystem : UnityEngine.IntegratedSubsystem
---@field displayOpaque boolean
---@field contentProtectionEnabled boolean
---@field scaleOfAllViewports number
---@field scaleOfAllRenderTargets number
---@field zNear number
---@field zFar number
---@field sRGB boolean
---@field occlusionMaskScale number
---@field foveatedRenderingLevel number
---@field foveatedRenderingFlags UnityEngine.XR.XRDisplaySubsystem.FoveatedRenderingFlags
---@field textureLayout UnityEngine.XR.XRDisplaySubsystem.TextureLayout
---@field supportedTextureLayouts UnityEngine.XR.XRDisplaySubsystem.TextureLayout
---@field reprojectionMode UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode
---@field disableLegacyRenderer boolean
---@field hdrOutputSettings UnityEngine.HDROutputSettings
UnityEngine.XR.XRDisplaySubsystem = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystem UnityEngine.XR.XRDisplaySubsystem
CS.UnityEngine.XR.XRDisplaySubsystem = UnityEngine.XR.XRDisplaySubsystem

---@return UnityEngine.XR.XRDisplaySubsystem
function UnityEngine.XR.XRDisplaySubsystem.New() end
---@param transform UnityEngine.Transform
---@param nodeType UnityEngine.XR.XRDisplaySubsystem.LateLatchNode
function UnityEngine.XR.XRDisplaySubsystem:MarkTransformLateLatched(transform, nodeType) end
---@param point UnityEngine.Vector3
---@param normal UnityEngine.Vector3
---@param velocity UnityEngine.Vector3
function UnityEngine.XR.XRDisplaySubsystem:SetFocusPlane(point, normal, velocity) end
---@param level number
function UnityEngine.XR.XRDisplaySubsystem:SetMSAALevel(level) end
---@return number
function UnityEngine.XR.XRDisplaySubsystem:GetRenderPassCount() end
---@param renderPassIndex number
---@param out_renderPass UnityEngine.XR.XRDisplaySubsystem.XRRenderPass
---@return UnityEngine.XR.XRDisplaySubsystem.XRRenderPass
function UnityEngine.XR.XRDisplaySubsystem:GetRenderPass(renderPassIndex, out_renderPass) end
---@param camera UnityEngine.Camera
function UnityEngine.XR.XRDisplaySubsystem:EndRecordingIfLateLatched(camera) end
---@param camera UnityEngine.Camera
function UnityEngine.XR.XRDisplaySubsystem:BeginRecordingIfLateLatched(camera) end
---@param camera UnityEngine.Camera
---@param cullingPassIndex number
---@param out_scriptableCullingParameters UnityEngine.Rendering.ScriptableCullingParameters
---@return UnityEngine.Rendering.ScriptableCullingParameters
function UnityEngine.XR.XRDisplaySubsystem:GetCullingParameters(camera, cullingPassIndex, out_scriptableCullingParameters) end
---@param out_gpuTimeLastFrame number
---@return boolean, number
function UnityEngine.XR.XRDisplaySubsystem:TryGetAppGPUTimeLastFrame(out_gpuTimeLastFrame) end
---@param out_gpuTimeLastFrameCompositor number
---@return boolean, number
function UnityEngine.XR.XRDisplaySubsystem:TryGetCompositorGPUTimeLastFrame(out_gpuTimeLastFrameCompositor) end
---@param out_droppedFrameCount number
---@return boolean, number
function UnityEngine.XR.XRDisplaySubsystem:TryGetDroppedFrameCount(out_droppedFrameCount) end
---@param out_framePresentCount number
---@return boolean, number
function UnityEngine.XR.XRDisplaySubsystem:TryGetFramePresentCount(out_framePresentCount) end
---@param out_displayRefreshRate number
---@return boolean, number
function UnityEngine.XR.XRDisplaySubsystem:TryGetDisplayRefreshRate(out_displayRefreshRate) end
---@param out_motionToPhoton number
---@return boolean, number
function UnityEngine.XR.XRDisplaySubsystem:TryGetMotionToPhoton(out_motionToPhoton) end
---@param unityXrRenderTextureId number
---@return UnityEngine.RenderTexture
function UnityEngine.XR.XRDisplaySubsystem:GetRenderTexture(unityXrRenderTextureId) end
---@param renderPass number
---@return UnityEngine.RenderTexture
function UnityEngine.XR.XRDisplaySubsystem:GetRenderTextureForRenderPass(renderPass) end
---@param renderPass number
---@return UnityEngine.RenderTexture
function UnityEngine.XR.XRDisplaySubsystem:GetSharedDepthTextureForRenderPass(renderPass) end
---@return number
function UnityEngine.XR.XRDisplaySubsystem:GetPreferredMirrorBlitMode() end
---@param blitMode number
function UnityEngine.XR.XRDisplaySubsystem:SetPreferredMirrorBlitMode(blitMode) end
---@param mirrorRt UnityEngine.RenderTexture
---@param out_outDesc UnityEngine.XR.XRDisplaySubsystem.XRMirrorViewBlitDesc
---@param mode number
---@return boolean, UnityEngine.XR.XRDisplaySubsystem.XRMirrorViewBlitDesc
function UnityEngine.XR.XRDisplaySubsystem:GetMirrorViewBlitDesc(mirrorRt, out_outDesc, mode) end
---@param cmd UnityEngine.Rendering.CommandBuffer
---@param allowGraphicsStateInvalidate boolean
---@param mode number
---@return boolean
function UnityEngine.XR.XRDisplaySubsystem:AddGraphicsThreadMirrorViewBlit(cmd, allowGraphicsStateInvalidate, mode) end

---@class UnityEngine.XR.XRDisplaySubsystem.FoveatedRenderingFlags
---@field None UnityEngine.XR.XRDisplaySubsystem.FoveatedRenderingFlags
---@field GazeAllowed UnityEngine.XR.XRDisplaySubsystem.FoveatedRenderingFlags
UnityEngine.XR.XRDisplaySubsystem.FoveatedRenderingFlags = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystem.FoveatedRenderingFlags UnityEngine.XR.XRDisplaySubsystem.FoveatedRenderingFlags
CS.UnityEngine.XR.XRDisplaySubsystem.FoveatedRenderingFlags = UnityEngine.XR.XRDisplaySubsystem.FoveatedRenderingFlags


---@class UnityEngine.XR.XRDisplaySubsystem.LateLatchNode
---@field Head UnityEngine.XR.XRDisplaySubsystem.LateLatchNode
---@field LeftHand UnityEngine.XR.XRDisplaySubsystem.LateLatchNode
---@field RightHand UnityEngine.XR.XRDisplaySubsystem.LateLatchNode
UnityEngine.XR.XRDisplaySubsystem.LateLatchNode = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystem.LateLatchNode UnityEngine.XR.XRDisplaySubsystem.LateLatchNode
CS.UnityEngine.XR.XRDisplaySubsystem.LateLatchNode = UnityEngine.XR.XRDisplaySubsystem.LateLatchNode


---@class UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode
---@field Unspecified UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode
---@field PositionAndOrientation UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode
---@field OrientationOnly UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode
---@field None UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode
UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode
CS.UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode = UnityEngine.XR.XRDisplaySubsystem.ReprojectionMode


---@class UnityEngine.XR.XRDisplaySubsystem.TextureLayout
---@field Texture2DArray UnityEngine.XR.XRDisplaySubsystem.TextureLayout
---@field SingleTexture2D UnityEngine.XR.XRDisplaySubsystem.TextureLayout
---@field SeparateTexture2Ds UnityEngine.XR.XRDisplaySubsystem.TextureLayout
UnityEngine.XR.XRDisplaySubsystem.TextureLayout = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystem.TextureLayout UnityEngine.XR.XRDisplaySubsystem.TextureLayout
CS.UnityEngine.XR.XRDisplaySubsystem.TextureLayout = UnityEngine.XR.XRDisplaySubsystem.TextureLayout


---@class UnityEngine.XR.XRDisplaySubsystem.XRBlitParams : System.ValueType
---@field srcTex UnityEngine.RenderTexture
---@field srcTexArraySlice number
---@field srcRect UnityEngine.Rect
---@field destRect UnityEngine.Rect
---@field foveatedRenderingInfo System.IntPtr
---@field srcHdrEncoded boolean
---@field srcHdrColorGamut UnityEngine.ColorGamut
---@field srcHdrMaxLuminance number
UnityEngine.XR.XRDisplaySubsystem.XRBlitParams = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystem.XRBlitParams UnityEngine.XR.XRDisplaySubsystem.XRBlitParams
CS.UnityEngine.XR.XRDisplaySubsystem.XRBlitParams = UnityEngine.XR.XRDisplaySubsystem.XRBlitParams


---@class UnityEngine.XR.XRDisplaySubsystem.XRMirrorViewBlitDesc : System.ValueType
---@field nativeBlitAvailable boolean
---@field nativeBlitInvalidStates boolean
---@field blitParamsCount number
UnityEngine.XR.XRDisplaySubsystem.XRMirrorViewBlitDesc = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystem.XRMirrorViewBlitDesc UnityEngine.XR.XRDisplaySubsystem.XRMirrorViewBlitDesc
CS.UnityEngine.XR.XRDisplaySubsystem.XRMirrorViewBlitDesc = UnityEngine.XR.XRDisplaySubsystem.XRMirrorViewBlitDesc

---@param blitParameterIndex number
---@param out_blitParameter UnityEngine.XR.XRDisplaySubsystem.XRBlitParams
---@return UnityEngine.XR.XRDisplaySubsystem.XRBlitParams
function UnityEngine.XR.XRDisplaySubsystem.XRMirrorViewBlitDesc:GetBlitParameter(blitParameterIndex, out_blitParameter) end

---@class UnityEngine.XR.XRDisplaySubsystem.XRRenderParameter : System.ValueType
---@field view UnityEngine.Matrix4x4
---@field projection UnityEngine.Matrix4x4
---@field viewport UnityEngine.Rect
---@field occlusionMesh UnityEngine.Mesh
---@field textureArraySlice number
---@field previousView UnityEngine.Matrix4x4
---@field isPreviousViewValid boolean
UnityEngine.XR.XRDisplaySubsystem.XRRenderParameter = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystem.XRRenderParameter UnityEngine.XR.XRDisplaySubsystem.XRRenderParameter
CS.UnityEngine.XR.XRDisplaySubsystem.XRRenderParameter = UnityEngine.XR.XRDisplaySubsystem.XRRenderParameter


---@class UnityEngine.XR.XRDisplaySubsystem.XRRenderPass : System.ValueType
---@field renderPassIndex number
---@field renderTarget UnityEngine.Rendering.RenderTargetIdentifier
---@field renderTargetDesc UnityEngine.RenderTextureDescriptor
---@field hasMotionVectorPass boolean
---@field motionVectorRenderTarget UnityEngine.Rendering.RenderTargetIdentifier
---@field motionVectorRenderTargetDesc UnityEngine.RenderTextureDescriptor
---@field shouldFillOutDepth boolean
---@field cullingPassIndex number
---@field foveatedRenderingInfo System.IntPtr
UnityEngine.XR.XRDisplaySubsystem.XRRenderPass = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystem.XRRenderPass UnityEngine.XR.XRDisplaySubsystem.XRRenderPass
CS.UnityEngine.XR.XRDisplaySubsystem.XRRenderPass = UnityEngine.XR.XRDisplaySubsystem.XRRenderPass

---@param camera UnityEngine.Camera
---@param renderParameterIndex number
---@param out_renderParameter UnityEngine.XR.XRDisplaySubsystem.XRRenderParameter
---@return UnityEngine.XR.XRDisplaySubsystem.XRRenderParameter
function UnityEngine.XR.XRDisplaySubsystem.XRRenderPass:GetRenderParameter(camera, renderParameterIndex, out_renderParameter) end
---@return number
function UnityEngine.XR.XRDisplaySubsystem.XRRenderPass:GetRenderParameterCount() end

---@class UnityEngine.XR.XRDisplaySubsystemDescriptor : UnityEngine.IntegratedSubsystemDescriptor
---@field disablesLegacyVr boolean
---@field enableBackBufferMSAA boolean
UnityEngine.XR.XRDisplaySubsystemDescriptor = {}
---@alias CS.UnityEngine.XR.XRDisplaySubsystemDescriptor UnityEngine.XR.XRDisplaySubsystemDescriptor
CS.UnityEngine.XR.XRDisplaySubsystemDescriptor = UnityEngine.XR.XRDisplaySubsystemDescriptor

---@return UnityEngine.XR.XRDisplaySubsystemDescriptor
function UnityEngine.XR.XRDisplaySubsystemDescriptor.New() end
---@return number
function UnityEngine.XR.XRDisplaySubsystemDescriptor:GetAvailableMirrorBlitModeCount() end
---@param index number
---@param out_mode UnityEngine.XR.XRMirrorViewBlitModeDesc
---@return UnityEngine.XR.XRMirrorViewBlitModeDesc
function UnityEngine.XR.XRDisplaySubsystemDescriptor:GetMirrorBlitModeByIndex(index, out_mode) end

---@class UnityEngine.XR.XRInputSubsystem : UnityEngine.IntegratedSubsystem
UnityEngine.XR.XRInputSubsystem = {}
---@alias CS.UnityEngine.XR.XRInputSubsystem UnityEngine.XR.XRInputSubsystem
CS.UnityEngine.XR.XRInputSubsystem = UnityEngine.XR.XRInputSubsystem

---@return UnityEngine.XR.XRInputSubsystem
function UnityEngine.XR.XRInputSubsystem.New() end
---@return boolean
function UnityEngine.XR.XRInputSubsystem:TryRecenter() end
---@param devices System.Collections.Generic.List
---@return boolean
function UnityEngine.XR.XRInputSubsystem:TryGetInputDevices(devices) end
---@param origin UnityEngine.XR.TrackingOriginModeFlags
---@return boolean
function UnityEngine.XR.XRInputSubsystem:TrySetTrackingOriginMode(origin) end
---@return UnityEngine.XR.TrackingOriginModeFlags
function UnityEngine.XR.XRInputSubsystem:GetTrackingOriginMode() end
---@return UnityEngine.XR.TrackingOriginModeFlags
function UnityEngine.XR.XRInputSubsystem:GetSupportedTrackingOriginModes() end
---@param boundaryPoints System.Collections.Generic.List
---@return boolean
function UnityEngine.XR.XRInputSubsystem:TryGetBoundaryPoints(boundaryPoints) end

---@class UnityEngine.XR.XRInputSubsystemDescriptor : UnityEngine.IntegratedSubsystemDescriptor
---@field disablesLegacyInput boolean
UnityEngine.XR.XRInputSubsystemDescriptor = {}
---@alias CS.UnityEngine.XR.XRInputSubsystemDescriptor UnityEngine.XR.XRInputSubsystemDescriptor
CS.UnityEngine.XR.XRInputSubsystemDescriptor = UnityEngine.XR.XRInputSubsystemDescriptor

---@return UnityEngine.XR.XRInputSubsystemDescriptor
function UnityEngine.XR.XRInputSubsystemDescriptor.New() end

---@class UnityEngine.XR.XRMeshSubsystem : UnityEngine.IntegratedSubsystem
---@field meshDensity number
UnityEngine.XR.XRMeshSubsystem = {}
---@alias CS.UnityEngine.XR.XRMeshSubsystem UnityEngine.XR.XRMeshSubsystem
CS.UnityEngine.XR.XRMeshSubsystem = UnityEngine.XR.XRMeshSubsystem

---@return UnityEngine.XR.XRMeshSubsystem
function UnityEngine.XR.XRMeshSubsystem.New() end
---@param meshInfosOut System.Collections.Generic.List
---@return boolean
function UnityEngine.XR.XRMeshSubsystem:TryGetMeshInfos(meshInfosOut) end
---@overload fun(self: UnityEngine.XR.XRMeshSubsystem, meshId: UnityEngine.XR.MeshId, mesh: UnityEngine.Mesh, meshCollider: UnityEngine.MeshCollider, attributes: UnityEngine.XR.MeshVertexAttributes, onMeshGenerationComplete: System.Action | function)
---@param meshId UnityEngine.XR.MeshId
---@param mesh UnityEngine.Mesh
---@param meshCollider UnityEngine.MeshCollider
---@param attributes UnityEngine.XR.MeshVertexAttributes
---@param onMeshGenerationComplete System.Action | function
---@param options UnityEngine.XR.MeshGenerationOptions
function UnityEngine.XR.XRMeshSubsystem:GenerateMeshAsync(meshId, mesh, meshCollider, attributes, onMeshGenerationComplete, options) end
---@param origin UnityEngine.Vector3
---@param extents UnityEngine.Vector3
---@return boolean
function UnityEngine.XR.XRMeshSubsystem:SetBoundingVolume(origin, extents) end
---@param allocator Unity.Collections.Allocator
---@return Unity.Collections.NativeArray
function UnityEngine.XR.XRMeshSubsystem:GetUpdatedMeshTransforms(allocator) end

---@class UnityEngine.XR.XRMeshSubsystem.MeshTransformList : System.ValueType
---@field Count number
---@field Data System.IntPtr
UnityEngine.XR.XRMeshSubsystem.MeshTransformList = {}
---@alias CS.UnityEngine.XR.XRMeshSubsystem.MeshTransformList UnityEngine.XR.XRMeshSubsystem.MeshTransformList
CS.UnityEngine.XR.XRMeshSubsystem.MeshTransformList = UnityEngine.XR.XRMeshSubsystem.MeshTransformList

---@param self System.IntPtr
---@return UnityEngine.XR.XRMeshSubsystem.MeshTransformList
function UnityEngine.XR.XRMeshSubsystem.MeshTransformList.New(self) end
function UnityEngine.XR.XRMeshSubsystem.MeshTransformList:Dispose() end

---@class UnityEngine.XR.XRMeshSubsystemDescriptor : UnityEngine.IntegratedSubsystemDescriptor
UnityEngine.XR.XRMeshSubsystemDescriptor = {}
---@alias CS.UnityEngine.XR.XRMeshSubsystemDescriptor UnityEngine.XR.XRMeshSubsystemDescriptor
CS.UnityEngine.XR.XRMeshSubsystemDescriptor = UnityEngine.XR.XRMeshSubsystemDescriptor

---@return UnityEngine.XR.XRMeshSubsystemDescriptor
function UnityEngine.XR.XRMeshSubsystemDescriptor.New() end

---@class UnityEngine.XR.XRMirrorViewBlitMode : System.ValueType
---@field Default number
---@field LeftEye number
---@field RightEye number
---@field SideBySide number
---@field SideBySideOcclusionMesh number
---@field Distort number
---@field None number
UnityEngine.XR.XRMirrorViewBlitMode = {}
---@alias CS.UnityEngine.XR.XRMirrorViewBlitMode UnityEngine.XR.XRMirrorViewBlitMode
CS.UnityEngine.XR.XRMirrorViewBlitMode = UnityEngine.XR.XRMirrorViewBlitMode


---@class UnityEngine.XR.XRMirrorViewBlitModeDesc : System.ValueType
---@field blitMode number
---@field blitModeDesc string
UnityEngine.XR.XRMirrorViewBlitModeDesc = {}
---@alias CS.UnityEngine.XR.XRMirrorViewBlitModeDesc UnityEngine.XR.XRMirrorViewBlitModeDesc
CS.UnityEngine.XR.XRMirrorViewBlitModeDesc = UnityEngine.XR.XRMirrorViewBlitModeDesc


---@class UnityEngine.XR.XRNode
---@field LeftEye UnityEngine.XR.XRNode
---@field RightEye UnityEngine.XR.XRNode
---@field CenterEye UnityEngine.XR.XRNode
---@field Head UnityEngine.XR.XRNode
---@field LeftHand UnityEngine.XR.XRNode
---@field RightHand UnityEngine.XR.XRNode
---@field GameController UnityEngine.XR.XRNode
---@field TrackingReference UnityEngine.XR.XRNode
---@field HardwareTracker UnityEngine.XR.XRNode
UnityEngine.XR.XRNode = {}
---@alias CS.UnityEngine.XR.XRNode UnityEngine.XR.XRNode
CS.UnityEngine.XR.XRNode = UnityEngine.XR.XRNode


---@class UnityEngine.XR.XRNodeState : System.ValueType
---@field uniqueID number
---@field nodeType UnityEngine.XR.XRNode
---@field tracked boolean
---@field position UnityEngine.Vector3
---@field rotation UnityEngine.Quaternion
---@field velocity UnityEngine.Vector3
---@field angularVelocity UnityEngine.Vector3
---@field acceleration UnityEngine.Vector3
---@field angularAcceleration UnityEngine.Vector3
UnityEngine.XR.XRNodeState = {}
---@alias CS.UnityEngine.XR.XRNodeState UnityEngine.XR.XRNodeState
CS.UnityEngine.XR.XRNodeState = UnityEngine.XR.XRNodeState

---@param out_position UnityEngine.Vector3
---@return boolean, UnityEngine.Vector3
function UnityEngine.XR.XRNodeState:TryGetPosition(out_position) end
---@param out_rotation UnityEngine.Quaternion
---@return boolean, UnityEngine.Quaternion
function UnityEngine.XR.XRNodeState:TryGetRotation(out_rotation) end
---@param out_velocity UnityEngine.Vector3
---@return boolean, UnityEngine.Vector3
function UnityEngine.XR.XRNodeState:TryGetVelocity(out_velocity) end
---@param out_angularVelocity UnityEngine.Vector3
---@return boolean, UnityEngine.Vector3
function UnityEngine.XR.XRNodeState:TryGetAngularVelocity(out_angularVelocity) end
---@param out_acceleration UnityEngine.Vector3
---@return boolean, UnityEngine.Vector3
function UnityEngine.XR.XRNodeState:TryGetAcceleration(out_acceleration) end
---@param out_angularAcceleration UnityEngine.Vector3
---@return boolean, UnityEngine.Vector3
function UnityEngine.XR.XRNodeState:TryGetAngularAcceleration(out_angularAcceleration) end

---@class UnityEngine.XR.XRSettings : System.Object
---@field enabled boolean
---@field gameViewRenderMode UnityEngine.XR.GameViewRenderMode
---@field isDeviceActive boolean
---@field showDeviceView boolean
---@field eyeTextureResolutionScale number
---@field eyeTextureWidth number
---@field eyeTextureHeight number
---@field eyeTextureDesc UnityEngine.RenderTextureDescriptor
---@field deviceEyeTextureDimension UnityEngine.Rendering.TextureDimension
---@field renderViewportScale number
---@field occlusionMaskScale number
---@field useOcclusionMesh boolean
---@field loadedDeviceName string
---@field supportedDevices string[]
---@field stereoRenderingMode UnityEngine.XR.XRSettings.StereoRenderingMode
UnityEngine.XR.XRSettings = {}
---@alias CS.UnityEngine.XR.XRSettings UnityEngine.XR.XRSettings
CS.UnityEngine.XR.XRSettings = UnityEngine.XR.XRSettings


---@class UnityEngine.XR.XRSettings.StereoRenderingMode
---@field MultiPass UnityEngine.XR.XRSettings.StereoRenderingMode
---@field SinglePass UnityEngine.XR.XRSettings.StereoRenderingMode
---@field SinglePassInstanced UnityEngine.XR.XRSettings.StereoRenderingMode
---@field SinglePassMultiview UnityEngine.XR.XRSettings.StereoRenderingMode
UnityEngine.XR.XRSettings.StereoRenderingMode = {}
---@alias CS.UnityEngine.XR.XRSettings.StereoRenderingMode UnityEngine.XR.XRSettings.StereoRenderingMode
CS.UnityEngine.XR.XRSettings.StereoRenderingMode = UnityEngine.XR.XRSettings.StereoRenderingMode


---@class UnityEngine.XR.XRStats : System.Object
UnityEngine.XR.XRStats = {}
---@alias CS.UnityEngine.XR.XRStats UnityEngine.XR.XRStats
CS.UnityEngine.XR.XRStats = UnityEngine.XR.XRStats

---@param out_gpuTimeLastFrame number
---@return boolean, number
function UnityEngine.XR.XRStats.TryGetGPUTimeLastFrame(out_gpuTimeLastFrame) end
---@param out_droppedFrameCount number
---@return boolean, number
function UnityEngine.XR.XRStats.TryGetDroppedFrameCount(out_droppedFrameCount) end
---@param out_framePresentCount number
---@return boolean, number
function UnityEngine.XR.XRStats.TryGetFramePresentCount(out_framePresentCount) end

---@class UnityEngine.YieldInstruction : System.Object
UnityEngine.YieldInstruction = {}
---@alias CS.UnityEngine.YieldInstruction UnityEngine.YieldInstruction
CS.UnityEngine.YieldInstruction = UnityEngine.YieldInstruction

---@return UnityEngine.YieldInstruction
function UnityEngine.YieldInstruction.New() end

---@class UnityEngine.Yoga.BaselineFunction : System.MulticastDelegate
UnityEngine.Yoga.BaselineFunction = {}
---@alias CS.UnityEngine.Yoga.BaselineFunction UnityEngine.Yoga.BaselineFunction
CS.UnityEngine.Yoga.BaselineFunction = UnityEngine.Yoga.BaselineFunction

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Yoga.BaselineFunction
function UnityEngine.Yoga.BaselineFunction.New(object, method) end
---@param node UnityEngine.Yoga.YogaNode
---@param width number
---@param height number
---@return number
function UnityEngine.Yoga.BaselineFunction:Invoke(node, width, height) end
---@param node UnityEngine.Yoga.YogaNode
---@param width number
---@param height number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Yoga.BaselineFunction:BeginInvoke(node, width, height, callback, object) end
---@param result System.IAsyncResult
---@return number
function UnityEngine.Yoga.BaselineFunction:EndInvoke(result) end

---@class UnityEngine.Yoga.Logger : System.MulticastDelegate
UnityEngine.Yoga.Logger = {}
---@alias CS.UnityEngine.Yoga.Logger UnityEngine.Yoga.Logger
CS.UnityEngine.Yoga.Logger = UnityEngine.Yoga.Logger

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Yoga.Logger
function UnityEngine.Yoga.Logger.New(object, method) end
---@param config UnityEngine.Yoga.YogaConfig
---@param node UnityEngine.Yoga.YogaNode
---@param level UnityEngine.Yoga.YogaLogLevel
---@param message string
function UnityEngine.Yoga.Logger:Invoke(config, node, level, message) end
---@param config UnityEngine.Yoga.YogaConfig
---@param node UnityEngine.Yoga.YogaNode
---@param level UnityEngine.Yoga.YogaLogLevel
---@param message string
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Yoga.Logger:BeginInvoke(config, node, level, message, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Yoga.Logger:EndInvoke(result) end

---@class UnityEngine.Yoga.MeasureFunction : System.MulticastDelegate
UnityEngine.Yoga.MeasureFunction = {}
---@alias CS.UnityEngine.Yoga.MeasureFunction UnityEngine.Yoga.MeasureFunction
CS.UnityEngine.Yoga.MeasureFunction = UnityEngine.Yoga.MeasureFunction

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Yoga.MeasureFunction
function UnityEngine.Yoga.MeasureFunction.New(object, method) end
---@param node UnityEngine.Yoga.YogaNode
---@param width number
---@param widthMode UnityEngine.Yoga.YogaMeasureMode
---@param height number
---@param heightMode UnityEngine.Yoga.YogaMeasureMode
---@return UnityEngine.Yoga.YogaSize
function UnityEngine.Yoga.MeasureFunction:Invoke(node, width, widthMode, height, heightMode) end
---@param node UnityEngine.Yoga.YogaNode
---@param width number
---@param widthMode UnityEngine.Yoga.YogaMeasureMode
---@param height number
---@param heightMode UnityEngine.Yoga.YogaMeasureMode
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Yoga.MeasureFunction:BeginInvoke(node, width, widthMode, height, heightMode, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.Yoga.YogaSize
function UnityEngine.Yoga.MeasureFunction:EndInvoke(result) end

---@class UnityEngine.Yoga.MeasureOutput : System.Object
UnityEngine.Yoga.MeasureOutput = {}
---@alias CS.UnityEngine.Yoga.MeasureOutput UnityEngine.Yoga.MeasureOutput
CS.UnityEngine.Yoga.MeasureOutput = UnityEngine.Yoga.MeasureOutput

---@return UnityEngine.Yoga.MeasureOutput
function UnityEngine.Yoga.MeasureOutput.New() end
---@param width number
---@param height number
---@return UnityEngine.Yoga.YogaSize
function UnityEngine.Yoga.MeasureOutput.Make(width, height) end

---@class UnityEngine.Yoga.Native : System.Object
UnityEngine.Yoga.Native = {}
---@alias CS.UnityEngine.Yoga.Native UnityEngine.Yoga.Native
CS.UnityEngine.Yoga.Native = UnityEngine.Yoga.Native

---@param config System.IntPtr
---@return System.IntPtr
function UnityEngine.Yoga.Native.YGNodeNewWithConfig(config) end
---@param ygNode System.IntPtr
function UnityEngine.Yoga.Native.YGNodeFree(ygNode) end
---@param node System.IntPtr
function UnityEngine.Yoga.Native.YGNodeReset(node) end
---@param ygNode System.IntPtr
---@param node UnityEngine.Yoga.YogaNode
function UnityEngine.Yoga.Native.YGSetManagedObject(ygNode, node) end
---@param ygNode System.IntPtr
---@param config System.IntPtr
function UnityEngine.Yoga.Native.YGNodeSetConfig(ygNode, config) end
---@return System.IntPtr
function UnityEngine.Yoga.Native.YGConfigGetDefault() end
---@return System.IntPtr
function UnityEngine.Yoga.Native.YGConfigNew() end
---@param config System.IntPtr
function UnityEngine.Yoga.Native.YGConfigFree(config) end
---@return number
function UnityEngine.Yoga.Native.YGNodeGetInstanceCount() end
---@return number
function UnityEngine.Yoga.Native.YGConfigGetInstanceCount() end
---@param config System.IntPtr
---@param feature UnityEngine.Yoga.YogaExperimentalFeature
---@param enabled boolean
function UnityEngine.Yoga.Native.YGConfigSetExperimentalFeatureEnabled(config, feature, enabled) end
---@param config System.IntPtr
---@param feature UnityEngine.Yoga.YogaExperimentalFeature
---@return boolean
function UnityEngine.Yoga.Native.YGConfigIsExperimentalFeatureEnabled(config, feature) end
---@param config System.IntPtr
---@param useWebDefaults boolean
function UnityEngine.Yoga.Native.YGConfigSetUseWebDefaults(config, useWebDefaults) end
---@param config System.IntPtr
---@return boolean
function UnityEngine.Yoga.Native.YGConfigGetUseWebDefaults(config) end
---@param config System.IntPtr
---@param pixelsInPoint number
function UnityEngine.Yoga.Native.YGConfigSetPointScaleFactor(config, pixelsInPoint) end
---@param config System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGConfigGetPointScaleFactor(config) end
---@param node System.IntPtr
---@param child System.IntPtr
---@param index number
function UnityEngine.Yoga.Native.YGNodeInsertChild(node, child, index) end
---@param node System.IntPtr
---@param child System.IntPtr
function UnityEngine.Yoga.Native.YGNodeRemoveChild(node, child) end
---@param node System.IntPtr
---@param availableWidth number
---@param availableHeight number
---@param parentDirection UnityEngine.Yoga.YogaDirection
function UnityEngine.Yoga.Native.YGNodeCalculateLayout(node, availableWidth, availableHeight, parentDirection) end
---@param node System.IntPtr
function UnityEngine.Yoga.Native.YGNodeMarkDirty(node) end
---@param node System.IntPtr
---@return boolean
function UnityEngine.Yoga.Native.YGNodeIsDirty(node) end
---@param node System.IntPtr
---@param options UnityEngine.Yoga.YogaPrintOptions
function UnityEngine.Yoga.Native.YGNodePrint(node, options) end
---@param dstNode System.IntPtr
---@param srcNode System.IntPtr
function UnityEngine.Yoga.Native.YGNodeCopyStyle(dstNode, srcNode) end
---@param node System.IntPtr
function UnityEngine.Yoga.Native.YGNodeSetMeasureFunc(node) end
---@param node System.IntPtr
function UnityEngine.Yoga.Native.YGNodeRemoveMeasureFunc(node) end
---@param node UnityEngine.Yoga.YogaNode
---@param width number
---@param widthMode UnityEngine.Yoga.YogaMeasureMode
---@param height number
---@param heightMode UnityEngine.Yoga.YogaMeasureMode
---@param returnValueAddress System.IntPtr
function UnityEngine.Yoga.Native.YGNodeMeasureInvoke(node, width, widthMode, height, heightMode, returnValueAddress) end
---@param node System.IntPtr
function UnityEngine.Yoga.Native.YGNodeSetBaselineFunc(node) end
---@param node System.IntPtr
function UnityEngine.Yoga.Native.YGNodeRemoveBaselineFunc(node) end
---@param node UnityEngine.Yoga.YogaNode
---@param width number
---@param height number
---@param returnValueAddress System.IntPtr
function UnityEngine.Yoga.Native.YGNodeBaselineInvoke(node, width, height, returnValueAddress) end
---@param node System.IntPtr
---@param hasNewLayout boolean
function UnityEngine.Yoga.Native.YGNodeSetHasNewLayout(node, hasNewLayout) end
---@param node System.IntPtr
---@return boolean
function UnityEngine.Yoga.Native.YGNodeGetHasNewLayout(node) end
---@param node System.IntPtr
---@param direction UnityEngine.Yoga.YogaDirection
function UnityEngine.Yoga.Native.YGNodeStyleSetDirection(node, direction) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaDirection
function UnityEngine.Yoga.Native.YGNodeStyleGetDirection(node) end
---@param node System.IntPtr
---@param flexDirection UnityEngine.Yoga.YogaFlexDirection
function UnityEngine.Yoga.Native.YGNodeStyleSetFlexDirection(node, flexDirection) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaFlexDirection
function UnityEngine.Yoga.Native.YGNodeStyleGetFlexDirection(node) end
---@param node System.IntPtr
---@param justifyContent UnityEngine.Yoga.YogaJustify
function UnityEngine.Yoga.Native.YGNodeStyleSetJustifyContent(node, justifyContent) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaJustify
function UnityEngine.Yoga.Native.YGNodeStyleGetJustifyContent(node) end
---@param node System.IntPtr
---@param alignContent UnityEngine.Yoga.YogaAlign
function UnityEngine.Yoga.Native.YGNodeStyleSetAlignContent(node, alignContent) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaAlign
function UnityEngine.Yoga.Native.YGNodeStyleGetAlignContent(node) end
---@param node System.IntPtr
---@param alignItems UnityEngine.Yoga.YogaAlign
function UnityEngine.Yoga.Native.YGNodeStyleSetAlignItems(node, alignItems) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaAlign
function UnityEngine.Yoga.Native.YGNodeStyleGetAlignItems(node) end
---@param node System.IntPtr
---@param alignSelf UnityEngine.Yoga.YogaAlign
function UnityEngine.Yoga.Native.YGNodeStyleSetAlignSelf(node, alignSelf) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaAlign
function UnityEngine.Yoga.Native.YGNodeStyleGetAlignSelf(node) end
---@param node System.IntPtr
---@param positionType UnityEngine.Yoga.YogaPositionType
function UnityEngine.Yoga.Native.YGNodeStyleSetPositionType(node, positionType) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaPositionType
function UnityEngine.Yoga.Native.YGNodeStyleGetPositionType(node) end
---@param node System.IntPtr
---@param flexWrap UnityEngine.Yoga.YogaWrap
function UnityEngine.Yoga.Native.YGNodeStyleSetFlexWrap(node, flexWrap) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaWrap
function UnityEngine.Yoga.Native.YGNodeStyleGetFlexWrap(node) end
---@param node System.IntPtr
---@param flexWrap UnityEngine.Yoga.YogaOverflow
function UnityEngine.Yoga.Native.YGNodeStyleSetOverflow(node, flexWrap) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaOverflow
function UnityEngine.Yoga.Native.YGNodeStyleGetOverflow(node) end
---@param node System.IntPtr
---@param display UnityEngine.Yoga.YogaDisplay
function UnityEngine.Yoga.Native.YGNodeStyleSetDisplay(node, display) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaDisplay
function UnityEngine.Yoga.Native.YGNodeStyleGetDisplay(node) end
---@param node System.IntPtr
---@param flex number
function UnityEngine.Yoga.Native.YGNodeStyleSetFlex(node, flex) end
---@param node System.IntPtr
---@param flexGrow number
function UnityEngine.Yoga.Native.YGNodeStyleSetFlexGrow(node, flexGrow) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeStyleGetFlexGrow(node) end
---@param node System.IntPtr
---@param flexShrink number
function UnityEngine.Yoga.Native.YGNodeStyleSetFlexShrink(node, flexShrink) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeStyleGetFlexShrink(node) end
---@param node System.IntPtr
---@param flexBasis number
function UnityEngine.Yoga.Native.YGNodeStyleSetFlexBasis(node, flexBasis) end
---@param node System.IntPtr
---@param flexBasis number
function UnityEngine.Yoga.Native.YGNodeStyleSetFlexBasisPercent(node, flexBasis) end
---@param node System.IntPtr
function UnityEngine.Yoga.Native.YGNodeStyleSetFlexBasisAuto(node) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetFlexBasis(node) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeGetComputedFlexBasis(node) end
---@param node System.IntPtr
---@param width number
function UnityEngine.Yoga.Native.YGNodeStyleSetWidth(node, width) end
---@param node System.IntPtr
---@param width number
function UnityEngine.Yoga.Native.YGNodeStyleSetWidthPercent(node, width) end
---@param node System.IntPtr
function UnityEngine.Yoga.Native.YGNodeStyleSetWidthAuto(node) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetWidth(node) end
---@param node System.IntPtr
---@param height number
function UnityEngine.Yoga.Native.YGNodeStyleSetHeight(node, height) end
---@param node System.IntPtr
---@param height number
function UnityEngine.Yoga.Native.YGNodeStyleSetHeightPercent(node, height) end
---@param node System.IntPtr
function UnityEngine.Yoga.Native.YGNodeStyleSetHeightAuto(node) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetHeight(node) end
---@param node System.IntPtr
---@param minWidth number
function UnityEngine.Yoga.Native.YGNodeStyleSetMinWidth(node, minWidth) end
---@param node System.IntPtr
---@param minWidth number
function UnityEngine.Yoga.Native.YGNodeStyleSetMinWidthPercent(node, minWidth) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetMinWidth(node) end
---@param node System.IntPtr
---@param minHeight number
function UnityEngine.Yoga.Native.YGNodeStyleSetMinHeight(node, minHeight) end
---@param node System.IntPtr
---@param minHeight number
function UnityEngine.Yoga.Native.YGNodeStyleSetMinHeightPercent(node, minHeight) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetMinHeight(node) end
---@param node System.IntPtr
---@param maxWidth number
function UnityEngine.Yoga.Native.YGNodeStyleSetMaxWidth(node, maxWidth) end
---@param node System.IntPtr
---@param maxWidth number
function UnityEngine.Yoga.Native.YGNodeStyleSetMaxWidthPercent(node, maxWidth) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetMaxWidth(node) end
---@param node System.IntPtr
---@param maxHeight number
function UnityEngine.Yoga.Native.YGNodeStyleSetMaxHeight(node, maxHeight) end
---@param node System.IntPtr
---@param maxHeight number
function UnityEngine.Yoga.Native.YGNodeStyleSetMaxHeightPercent(node, maxHeight) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetMaxHeight(node) end
---@param node System.IntPtr
---@param aspectRatio number
function UnityEngine.Yoga.Native.YGNodeStyleSetAspectRatio(node, aspectRatio) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeStyleGetAspectRatio(node) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@param position number
function UnityEngine.Yoga.Native.YGNodeStyleSetPosition(node, edge, position) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@param position number
function UnityEngine.Yoga.Native.YGNodeStyleSetPositionPercent(node, edge, position) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetPosition(node, edge) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@param margin number
function UnityEngine.Yoga.Native.YGNodeStyleSetMargin(node, edge, margin) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@param margin number
function UnityEngine.Yoga.Native.YGNodeStyleSetMarginPercent(node, edge, margin) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
function UnityEngine.Yoga.Native.YGNodeStyleSetMarginAuto(node, edge) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetMargin(node, edge) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@param padding number
function UnityEngine.Yoga.Native.YGNodeStyleSetPadding(node, edge, padding) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@param padding number
function UnityEngine.Yoga.Native.YGNodeStyleSetPaddingPercent(node, edge, padding) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.Native.YGNodeStyleGetPadding(node, edge) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@param border number
function UnityEngine.Yoga.Native.YGNodeStyleSetBorder(node, edge, border) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@return number
function UnityEngine.Yoga.Native.YGNodeStyleGetBorder(node, edge) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeLayoutGetLeft(node) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeLayoutGetTop(node) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeLayoutGetRight(node) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeLayoutGetBottom(node) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeLayoutGetWidth(node) end
---@param node System.IntPtr
---@return number
function UnityEngine.Yoga.Native.YGNodeLayoutGetHeight(node) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@return number
function UnityEngine.Yoga.Native.YGNodeLayoutGetMargin(node, edge) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@return number
function UnityEngine.Yoga.Native.YGNodeLayoutGetPadding(node, edge) end
---@param node System.IntPtr
---@param edge UnityEngine.Yoga.YogaEdge
---@return number
function UnityEngine.Yoga.Native.YGNodeLayoutGetBorder(node, edge) end
---@param node System.IntPtr
---@return UnityEngine.Yoga.YogaDirection
function UnityEngine.Yoga.Native.YGNodeLayoutGetDirection(node) end

---@class UnityEngine.Yoga.YogaAlign
---@field Auto UnityEngine.Yoga.YogaAlign
---@field FlexStart UnityEngine.Yoga.YogaAlign
---@field Center UnityEngine.Yoga.YogaAlign
---@field FlexEnd UnityEngine.Yoga.YogaAlign
---@field Stretch UnityEngine.Yoga.YogaAlign
---@field Baseline UnityEngine.Yoga.YogaAlign
---@field SpaceBetween UnityEngine.Yoga.YogaAlign
---@field SpaceAround UnityEngine.Yoga.YogaAlign
UnityEngine.Yoga.YogaAlign = {}
---@alias CS.UnityEngine.Yoga.YogaAlign UnityEngine.Yoga.YogaAlign
CS.UnityEngine.Yoga.YogaAlign = UnityEngine.Yoga.YogaAlign


---@class UnityEngine.Yoga.YogaBaselineFunc : System.MulticastDelegate
UnityEngine.Yoga.YogaBaselineFunc = {}
---@alias CS.UnityEngine.Yoga.YogaBaselineFunc UnityEngine.Yoga.YogaBaselineFunc
CS.UnityEngine.Yoga.YogaBaselineFunc = UnityEngine.Yoga.YogaBaselineFunc

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Yoga.YogaBaselineFunc
function UnityEngine.Yoga.YogaBaselineFunc.New(object, method) end
---@param unmanagedNodePtr System.IntPtr
---@param width number
---@param height number
---@return number
function UnityEngine.Yoga.YogaBaselineFunc:Invoke(unmanagedNodePtr, width, height) end
---@param unmanagedNodePtr System.IntPtr
---@param width number
---@param height number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Yoga.YogaBaselineFunc:BeginInvoke(unmanagedNodePtr, width, height, callback, object) end
---@param result System.IAsyncResult
---@return number
function UnityEngine.Yoga.YogaBaselineFunc:EndInvoke(result) end

---@class UnityEngine.Yoga.YogaConfig : System.Object
---@field Logger UnityEngine.Yoga.Logger
---@field UseWebDefaults boolean
---@field PointScaleFactor number
UnityEngine.Yoga.YogaConfig = {}
---@alias CS.UnityEngine.Yoga.YogaConfig UnityEngine.Yoga.YogaConfig
CS.UnityEngine.Yoga.YogaConfig = UnityEngine.Yoga.YogaConfig

---@return UnityEngine.Yoga.YogaConfig
function UnityEngine.Yoga.YogaConfig.New() end
---@return number
function UnityEngine.Yoga.YogaConfig.GetInstanceCount() end
---@param logger UnityEngine.Yoga.Logger
function UnityEngine.Yoga.YogaConfig.SetDefaultLogger(logger) end
---@param feature UnityEngine.Yoga.YogaExperimentalFeature
---@param enabled boolean
function UnityEngine.Yoga.YogaConfig:SetExperimentalFeatureEnabled(feature, enabled) end
---@param feature UnityEngine.Yoga.YogaExperimentalFeature
---@return boolean
function UnityEngine.Yoga.YogaConfig:IsExperimentalFeatureEnabled(feature) end

---@class UnityEngine.Yoga.YogaConstants : System.Object
---@field Undefined number
UnityEngine.Yoga.YogaConstants = {}
---@alias CS.UnityEngine.Yoga.YogaConstants UnityEngine.Yoga.YogaConstants
CS.UnityEngine.Yoga.YogaConstants = UnityEngine.Yoga.YogaConstants

---@overload fun(value: number) : boolean
---@param value UnityEngine.Yoga.YogaValue
---@return boolean
function UnityEngine.Yoga.YogaConstants.IsUndefined(value) end

---@class UnityEngine.Yoga.YogaDimension
---@field Width UnityEngine.Yoga.YogaDimension
---@field Height UnityEngine.Yoga.YogaDimension
UnityEngine.Yoga.YogaDimension = {}
---@alias CS.UnityEngine.Yoga.YogaDimension UnityEngine.Yoga.YogaDimension
CS.UnityEngine.Yoga.YogaDimension = UnityEngine.Yoga.YogaDimension


---@class UnityEngine.Yoga.YogaDirection
---@field Inherit UnityEngine.Yoga.YogaDirection
---@field LTR UnityEngine.Yoga.YogaDirection
---@field RTL UnityEngine.Yoga.YogaDirection
UnityEngine.Yoga.YogaDirection = {}
---@alias CS.UnityEngine.Yoga.YogaDirection UnityEngine.Yoga.YogaDirection
CS.UnityEngine.Yoga.YogaDirection = UnityEngine.Yoga.YogaDirection


---@class UnityEngine.Yoga.YogaDisplay
---@field Flex UnityEngine.Yoga.YogaDisplay
---@field None UnityEngine.Yoga.YogaDisplay
UnityEngine.Yoga.YogaDisplay = {}
---@alias CS.UnityEngine.Yoga.YogaDisplay UnityEngine.Yoga.YogaDisplay
CS.UnityEngine.Yoga.YogaDisplay = UnityEngine.Yoga.YogaDisplay


---@class UnityEngine.Yoga.YogaEdge
---@field Left UnityEngine.Yoga.YogaEdge
---@field Top UnityEngine.Yoga.YogaEdge
---@field Right UnityEngine.Yoga.YogaEdge
---@field Bottom UnityEngine.Yoga.YogaEdge
---@field Start UnityEngine.Yoga.YogaEdge
---@field End UnityEngine.Yoga.YogaEdge
---@field Horizontal UnityEngine.Yoga.YogaEdge
---@field Vertical UnityEngine.Yoga.YogaEdge
---@field All UnityEngine.Yoga.YogaEdge
UnityEngine.Yoga.YogaEdge = {}
---@alias CS.UnityEngine.Yoga.YogaEdge UnityEngine.Yoga.YogaEdge
CS.UnityEngine.Yoga.YogaEdge = UnityEngine.Yoga.YogaEdge


---@class UnityEngine.Yoga.YogaExperimentalFeature
---@field WebFlexBasis UnityEngine.Yoga.YogaExperimentalFeature
UnityEngine.Yoga.YogaExperimentalFeature = {}
---@alias CS.UnityEngine.Yoga.YogaExperimentalFeature UnityEngine.Yoga.YogaExperimentalFeature
CS.UnityEngine.Yoga.YogaExperimentalFeature = UnityEngine.Yoga.YogaExperimentalFeature


---@class UnityEngine.Yoga.YogaFlexDirection
---@field Column UnityEngine.Yoga.YogaFlexDirection
---@field ColumnReverse UnityEngine.Yoga.YogaFlexDirection
---@field Row UnityEngine.Yoga.YogaFlexDirection
---@field RowReverse UnityEngine.Yoga.YogaFlexDirection
UnityEngine.Yoga.YogaFlexDirection = {}
---@alias CS.UnityEngine.Yoga.YogaFlexDirection UnityEngine.Yoga.YogaFlexDirection
CS.UnityEngine.Yoga.YogaFlexDirection = UnityEngine.Yoga.YogaFlexDirection


---@class UnityEngine.Yoga.YogaJustify
---@field FlexStart UnityEngine.Yoga.YogaJustify
---@field Center UnityEngine.Yoga.YogaJustify
---@field FlexEnd UnityEngine.Yoga.YogaJustify
---@field SpaceBetween UnityEngine.Yoga.YogaJustify
---@field SpaceAround UnityEngine.Yoga.YogaJustify
UnityEngine.Yoga.YogaJustify = {}
---@alias CS.UnityEngine.Yoga.YogaJustify UnityEngine.Yoga.YogaJustify
CS.UnityEngine.Yoga.YogaJustify = UnityEngine.Yoga.YogaJustify


---@class UnityEngine.Yoga.YogaLogger : System.MulticastDelegate
UnityEngine.Yoga.YogaLogger = {}
---@alias CS.UnityEngine.Yoga.YogaLogger UnityEngine.Yoga.YogaLogger
CS.UnityEngine.Yoga.YogaLogger = UnityEngine.Yoga.YogaLogger

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Yoga.YogaLogger
function UnityEngine.Yoga.YogaLogger.New(object, method) end
---@param unmanagedConfigPtr System.IntPtr
---@param unmanagedNotePtr System.IntPtr
---@param level UnityEngine.Yoga.YogaLogLevel
---@param message string
function UnityEngine.Yoga.YogaLogger:Invoke(unmanagedConfigPtr, unmanagedNotePtr, level, message) end
---@param unmanagedConfigPtr System.IntPtr
---@param unmanagedNotePtr System.IntPtr
---@param level UnityEngine.Yoga.YogaLogLevel
---@param message string
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Yoga.YogaLogger:BeginInvoke(unmanagedConfigPtr, unmanagedNotePtr, level, message, callback, object) end
---@param result System.IAsyncResult
function UnityEngine.Yoga.YogaLogger:EndInvoke(result) end

---@class UnityEngine.Yoga.YogaLogLevel
---@field Error UnityEngine.Yoga.YogaLogLevel
---@field Warn UnityEngine.Yoga.YogaLogLevel
---@field Info UnityEngine.Yoga.YogaLogLevel
---@field Debug UnityEngine.Yoga.YogaLogLevel
---@field Verbose UnityEngine.Yoga.YogaLogLevel
---@field Fatal UnityEngine.Yoga.YogaLogLevel
UnityEngine.Yoga.YogaLogLevel = {}
---@alias CS.UnityEngine.Yoga.YogaLogLevel UnityEngine.Yoga.YogaLogLevel
CS.UnityEngine.Yoga.YogaLogLevel = UnityEngine.Yoga.YogaLogLevel


---@class UnityEngine.Yoga.YogaMeasureFunc : System.MulticastDelegate
UnityEngine.Yoga.YogaMeasureFunc = {}
---@alias CS.UnityEngine.Yoga.YogaMeasureFunc UnityEngine.Yoga.YogaMeasureFunc
CS.UnityEngine.Yoga.YogaMeasureFunc = UnityEngine.Yoga.YogaMeasureFunc

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngine.Yoga.YogaMeasureFunc
function UnityEngine.Yoga.YogaMeasureFunc.New(object, method) end
---@param unmanagedNodePtr System.IntPtr
---@param width number
---@param widthMode UnityEngine.Yoga.YogaMeasureMode
---@param height number
---@param heightMode UnityEngine.Yoga.YogaMeasureMode
---@return UnityEngine.Yoga.YogaSize
function UnityEngine.Yoga.YogaMeasureFunc:Invoke(unmanagedNodePtr, width, widthMode, height, heightMode) end
---@param unmanagedNodePtr System.IntPtr
---@param width number
---@param widthMode UnityEngine.Yoga.YogaMeasureMode
---@param height number
---@param heightMode UnityEngine.Yoga.YogaMeasureMode
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngine.Yoga.YogaMeasureFunc:BeginInvoke(unmanagedNodePtr, width, widthMode, height, heightMode, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.Yoga.YogaSize
function UnityEngine.Yoga.YogaMeasureFunc:EndInvoke(result) end

---@class UnityEngine.Yoga.YogaMeasureMode
---@field Undefined UnityEngine.Yoga.YogaMeasureMode
---@field Exactly UnityEngine.Yoga.YogaMeasureMode
---@field AtMost UnityEngine.Yoga.YogaMeasureMode
UnityEngine.Yoga.YogaMeasureMode = {}
---@alias CS.UnityEngine.Yoga.YogaMeasureMode UnityEngine.Yoga.YogaMeasureMode
CS.UnityEngine.Yoga.YogaMeasureMode = UnityEngine.Yoga.YogaMeasureMode


---@class UnityEngine.Yoga.YogaNode : System.Object
---@field IsDirty boolean
---@field HasNewLayout boolean
---@field Parent UnityEngine.Yoga.YogaNode
---@field IsMeasureDefined boolean
---@field IsBaselineDefined boolean
---@field StyleDirection UnityEngine.Yoga.YogaDirection
---@field FlexDirection UnityEngine.Yoga.YogaFlexDirection
---@field JustifyContent UnityEngine.Yoga.YogaJustify
---@field Display UnityEngine.Yoga.YogaDisplay
---@field AlignItems UnityEngine.Yoga.YogaAlign
---@field AlignSelf UnityEngine.Yoga.YogaAlign
---@field AlignContent UnityEngine.Yoga.YogaAlign
---@field PositionType UnityEngine.Yoga.YogaPositionType
---@field Wrap UnityEngine.Yoga.YogaWrap
---@field Flex number
---@field FlexGrow number
---@field FlexShrink number
---@field FlexBasis UnityEngine.Yoga.YogaValue
---@field Width UnityEngine.Yoga.YogaValue
---@field Height UnityEngine.Yoga.YogaValue
---@field MaxWidth UnityEngine.Yoga.YogaValue
---@field MaxHeight UnityEngine.Yoga.YogaValue
---@field MinWidth UnityEngine.Yoga.YogaValue
---@field MinHeight UnityEngine.Yoga.YogaValue
---@field AspectRatio number
---@field LayoutX number
---@field LayoutY number
---@field LayoutRight number
---@field LayoutBottom number
---@field LayoutWidth number
---@field LayoutHeight number
---@field LayoutDirection UnityEngine.Yoga.YogaDirection
---@field Overflow UnityEngine.Yoga.YogaOverflow
---@field Data System.Object
---@field Item UnityEngine.Yoga.YogaNode
---@field Count number
---@field Left UnityEngine.Yoga.YogaValue
---@field Top UnityEngine.Yoga.YogaValue
---@field Right UnityEngine.Yoga.YogaValue
---@field Bottom UnityEngine.Yoga.YogaValue
---@field Start UnityEngine.Yoga.YogaValue
---@field End UnityEngine.Yoga.YogaValue
---@field MarginLeft UnityEngine.Yoga.YogaValue
---@field MarginTop UnityEngine.Yoga.YogaValue
---@field MarginRight UnityEngine.Yoga.YogaValue
---@field MarginBottom UnityEngine.Yoga.YogaValue
---@field MarginStart UnityEngine.Yoga.YogaValue
---@field MarginEnd UnityEngine.Yoga.YogaValue
---@field MarginHorizontal UnityEngine.Yoga.YogaValue
---@field MarginVertical UnityEngine.Yoga.YogaValue
---@field Margin UnityEngine.Yoga.YogaValue
---@field PaddingLeft UnityEngine.Yoga.YogaValue
---@field PaddingTop UnityEngine.Yoga.YogaValue
---@field PaddingRight UnityEngine.Yoga.YogaValue
---@field PaddingBottom UnityEngine.Yoga.YogaValue
---@field PaddingStart UnityEngine.Yoga.YogaValue
---@field PaddingEnd UnityEngine.Yoga.YogaValue
---@field PaddingHorizontal UnityEngine.Yoga.YogaValue
---@field PaddingVertical UnityEngine.Yoga.YogaValue
---@field Padding UnityEngine.Yoga.YogaValue
---@field BorderLeftWidth number
---@field BorderTopWidth number
---@field BorderRightWidth number
---@field BorderBottomWidth number
---@field BorderStartWidth number
---@field BorderEndWidth number
---@field BorderWidth number
---@field LayoutMarginLeft number
---@field LayoutMarginTop number
---@field LayoutMarginRight number
---@field LayoutMarginBottom number
---@field LayoutMarginStart number
---@field LayoutMarginEnd number
---@field LayoutPaddingLeft number
---@field LayoutPaddingTop number
---@field LayoutPaddingRight number
---@field LayoutPaddingBottom number
---@field LayoutBorderLeft number
---@field LayoutBorderTop number
---@field LayoutBorderRight number
---@field LayoutBorderBottom number
---@field LayoutPaddingStart number
---@field LayoutPaddingEnd number
---@field ComputedFlexBasis number
UnityEngine.Yoga.YogaNode = {}
---@alias CS.UnityEngine.Yoga.YogaNode UnityEngine.Yoga.YogaNode
CS.UnityEngine.Yoga.YogaNode = UnityEngine.Yoga.YogaNode

---@overload fun(config: UnityEngine.Yoga.YogaConfig) : UnityEngine.Yoga.YogaNode
---@param srcNode UnityEngine.Yoga.YogaNode
---@return UnityEngine.Yoga.YogaNode
function UnityEngine.Yoga.YogaNode.New(srcNode) end
---@param node UnityEngine.Yoga.YogaNode
---@param width number
---@param widthMode UnityEngine.Yoga.YogaMeasureMode
---@param height number
---@param heightMode UnityEngine.Yoga.YogaMeasureMode
---@return UnityEngine.Yoga.YogaSize
function UnityEngine.Yoga.YogaNode.MeasureInternal(node, width, widthMode, height, heightMode) end
---@param node UnityEngine.Yoga.YogaNode
---@param width number
---@param height number
---@return number
function UnityEngine.Yoga.YogaNode.BaselineInternal(node, width, height) end
---@return number
function UnityEngine.Yoga.YogaNode.GetInstanceCount() end
function UnityEngine.Yoga.YogaNode:Reset() end
function UnityEngine.Yoga.YogaNode:MarkDirty() end
function UnityEngine.Yoga.YogaNode:MarkHasNewLayout() end
---@param srcNode UnityEngine.Yoga.YogaNode
function UnityEngine.Yoga.YogaNode:CopyStyle(srcNode) end
function UnityEngine.Yoga.YogaNode:MarkLayoutSeen() end
---@param f1 number
---@param f2 number
---@return boolean
function UnityEngine.Yoga.YogaNode:ValuesEqual(f1, f2) end
---@param index number
---@param node UnityEngine.Yoga.YogaNode
function UnityEngine.Yoga.YogaNode:Insert(index, node) end
---@param index number
function UnityEngine.Yoga.YogaNode:RemoveAt(index) end
---@param child UnityEngine.Yoga.YogaNode
function UnityEngine.Yoga.YogaNode:AddChild(child) end
---@param child UnityEngine.Yoga.YogaNode
function UnityEngine.Yoga.YogaNode:RemoveChild(child) end
function UnityEngine.Yoga.YogaNode:Clear() end
---@param node UnityEngine.Yoga.YogaNode
---@return number
function UnityEngine.Yoga.YogaNode:IndexOf(node) end
---@param measureFunction UnityEngine.Yoga.MeasureFunction
function UnityEngine.Yoga.YogaNode:SetMeasureFunction(measureFunction) end
---@param baselineFunction UnityEngine.Yoga.BaselineFunction
function UnityEngine.Yoga.YogaNode:SetBaselineFunction(baselineFunction) end
---@param width number
---@param height number
function UnityEngine.Yoga.YogaNode:CalculateLayout(width, height) end
---@param options UnityEngine.Yoga.YogaPrintOptions
---@return string
function UnityEngine.Yoga.YogaNode:Print(options) end
---@return System.Collections.Generic.IEnumerator
function UnityEngine.Yoga.YogaNode:GetEnumerator() end

---@class UnityEngine.Yoga.YogaNodeType
---@field Default UnityEngine.Yoga.YogaNodeType
---@field Text UnityEngine.Yoga.YogaNodeType
UnityEngine.Yoga.YogaNodeType = {}
---@alias CS.UnityEngine.Yoga.YogaNodeType UnityEngine.Yoga.YogaNodeType
CS.UnityEngine.Yoga.YogaNodeType = UnityEngine.Yoga.YogaNodeType


---@class UnityEngine.Yoga.YogaOverflow
---@field Visible UnityEngine.Yoga.YogaOverflow
---@field Hidden UnityEngine.Yoga.YogaOverflow
---@field Scroll UnityEngine.Yoga.YogaOverflow
UnityEngine.Yoga.YogaOverflow = {}
---@alias CS.UnityEngine.Yoga.YogaOverflow UnityEngine.Yoga.YogaOverflow
CS.UnityEngine.Yoga.YogaOverflow = UnityEngine.Yoga.YogaOverflow


---@class UnityEngine.Yoga.YogaPositionType
---@field Relative UnityEngine.Yoga.YogaPositionType
---@field Absolute UnityEngine.Yoga.YogaPositionType
UnityEngine.Yoga.YogaPositionType = {}
---@alias CS.UnityEngine.Yoga.YogaPositionType UnityEngine.Yoga.YogaPositionType
CS.UnityEngine.Yoga.YogaPositionType = UnityEngine.Yoga.YogaPositionType


---@class UnityEngine.Yoga.YogaPrintOptions
---@field Layout UnityEngine.Yoga.YogaPrintOptions
---@field Style UnityEngine.Yoga.YogaPrintOptions
---@field Children UnityEngine.Yoga.YogaPrintOptions
UnityEngine.Yoga.YogaPrintOptions = {}
---@alias CS.UnityEngine.Yoga.YogaPrintOptions UnityEngine.Yoga.YogaPrintOptions
CS.UnityEngine.Yoga.YogaPrintOptions = UnityEngine.Yoga.YogaPrintOptions


---@class UnityEngine.Yoga.YogaSize : System.ValueType
---@field width number
---@field height number
UnityEngine.Yoga.YogaSize = {}
---@alias CS.UnityEngine.Yoga.YogaSize UnityEngine.Yoga.YogaSize
CS.UnityEngine.Yoga.YogaSize = UnityEngine.Yoga.YogaSize


---@class UnityEngine.Yoga.YogaUnit
---@field Undefined UnityEngine.Yoga.YogaUnit
---@field Point UnityEngine.Yoga.YogaUnit
---@field Percent UnityEngine.Yoga.YogaUnit
---@field Auto UnityEngine.Yoga.YogaUnit
UnityEngine.Yoga.YogaUnit = {}
---@alias CS.UnityEngine.Yoga.YogaUnit UnityEngine.Yoga.YogaUnit
CS.UnityEngine.Yoga.YogaUnit = UnityEngine.Yoga.YogaUnit


---@class UnityEngine.Yoga.YogaValue : System.ValueType
---@field Unit UnityEngine.Yoga.YogaUnit
---@field Value number
UnityEngine.Yoga.YogaValue = {}
---@alias CS.UnityEngine.Yoga.YogaValue UnityEngine.Yoga.YogaValue
CS.UnityEngine.Yoga.YogaValue = UnityEngine.Yoga.YogaValue

---@param value number
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.YogaValue.Point(value) end
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.YogaValue.Undefined() end
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.YogaValue.Auto() end
---@param value number
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.YogaValue.Percent(value) end
---@overload fun(self: UnityEngine.Yoga.YogaValue, other: UnityEngine.Yoga.YogaValue) : boolean
---@param obj System.Object
---@return boolean
function UnityEngine.Yoga.YogaValue:Equals(obj) end
---@return number
function UnityEngine.Yoga.YogaValue:GetHashCode() end

---@class UnityEngine.Yoga.YogaValueExtensions : System.Object
UnityEngine.Yoga.YogaValueExtensions = {}
---@alias CS.UnityEngine.Yoga.YogaValueExtensions UnityEngine.Yoga.YogaValueExtensions
CS.UnityEngine.Yoga.YogaValueExtensions = UnityEngine.Yoga.YogaValueExtensions

---@overload fun(value: number) : UnityEngine.Yoga.YogaValue
---@param value number
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.YogaValueExtensions.Percent(value) end
---@overload fun(value: number) : UnityEngine.Yoga.YogaValue
---@param value number
---@return UnityEngine.Yoga.YogaValue
function UnityEngine.Yoga.YogaValueExtensions.Pt(value) end

---@class UnityEngine.Yoga.YogaWrap
---@field NoWrap UnityEngine.Yoga.YogaWrap
---@field Wrap UnityEngine.Yoga.YogaWrap
---@field WrapReverse UnityEngine.Yoga.YogaWrap
UnityEngine.Yoga.YogaWrap = {}
---@alias CS.UnityEngine.Yoga.YogaWrap UnityEngine.Yoga.YogaWrap
CS.UnityEngine.Yoga.YogaWrap = UnityEngine.Yoga.YogaWrap


---@class UnityEngineInternal.APIUpdaterRuntimeServices : System.Object
UnityEngineInternal.APIUpdaterRuntimeServices = {}
---@alias CS.UnityEngineInternal.APIUpdaterRuntimeServices UnityEngineInternal.APIUpdaterRuntimeServices
CS.UnityEngineInternal.APIUpdaterRuntimeServices = UnityEngineInternal.APIUpdaterRuntimeServices

---@return UnityEngineInternal.APIUpdaterRuntimeServices
function UnityEngineInternal.APIUpdaterRuntimeServices.New() end

---@class UnityEngineInternal.GenericStack : System.Collections.Stack
UnityEngineInternal.GenericStack = {}
---@alias CS.UnityEngineInternal.GenericStack UnityEngineInternal.GenericStack
CS.UnityEngineInternal.GenericStack = UnityEngineInternal.GenericStack

---@return UnityEngineInternal.GenericStack
function UnityEngineInternal.GenericStack.New() end

---@class UnityEngineInternal.GIDebugVisualisation : System.Object
---@field cycleMode boolean
---@field pauseCycleMode boolean
---@field texType UnityEngineInternal.GITextureType
UnityEngineInternal.GIDebugVisualisation = {}
---@alias CS.UnityEngineInternal.GIDebugVisualisation UnityEngineInternal.GIDebugVisualisation
CS.UnityEngineInternal.GIDebugVisualisation = UnityEngineInternal.GIDebugVisualisation

function UnityEngineInternal.GIDebugVisualisation.ResetRuntimeInputTextures() end
function UnityEngineInternal.GIDebugVisualisation.PlayCycleMode() end
function UnityEngineInternal.GIDebugVisualisation.PauseCycleMode() end
function UnityEngineInternal.GIDebugVisualisation.StopCycleMode() end
---@param skip number
function UnityEngineInternal.GIDebugVisualisation.CycleSkipSystems(skip) end
---@param skip number
function UnityEngineInternal.GIDebugVisualisation.CycleSkipInstances(skip) end

---@class UnityEngineInternal.GITextureType
---@field Charting UnityEngineInternal.GITextureType
---@field Albedo UnityEngineInternal.GITextureType
---@field Emissive UnityEngineInternal.GITextureType
---@field Irradiance UnityEngineInternal.GITextureType
---@field Directionality UnityEngineInternal.GITextureType
---@field Baked UnityEngineInternal.GITextureType
---@field BakedDirectional UnityEngineInternal.GITextureType
---@field InputWorkspace UnityEngineInternal.GITextureType
---@field BakedShadowMask UnityEngineInternal.GITextureType
---@field BakedAlbedo UnityEngineInternal.GITextureType
---@field BakedEmissive UnityEngineInternal.GITextureType
---@field BakedCharting UnityEngineInternal.GITextureType
---@field BakedTexelValidity UnityEngineInternal.GITextureType
---@field BakedUVOverlap UnityEngineInternal.GITextureType
---@field BakedLightmapCulling UnityEngineInternal.GITextureType
UnityEngineInternal.GITextureType = {}
---@alias CS.UnityEngineInternal.GITextureType UnityEngineInternal.GITextureType
CS.UnityEngineInternal.GITextureType = UnityEngineInternal.GITextureType


---@class UnityEngineInternal.GraphicsDeviceDebug : System.Object
UnityEngineInternal.GraphicsDeviceDebug = {}
---@alias CS.UnityEngineInternal.GraphicsDeviceDebug UnityEngineInternal.GraphicsDeviceDebug
CS.UnityEngineInternal.GraphicsDeviceDebug = UnityEngineInternal.GraphicsDeviceDebug


---@class UnityEngineInternal.GraphicsDeviceDebugSettings : System.ValueType
---@field sleepAtStartOfGraphicsJobs number
---@field sleepBeforeTextureUpload number
UnityEngineInternal.GraphicsDeviceDebugSettings = {}
---@alias CS.UnityEngineInternal.GraphicsDeviceDebugSettings UnityEngineInternal.GraphicsDeviceDebugSettings
CS.UnityEngineInternal.GraphicsDeviceDebugSettings = UnityEngineInternal.GraphicsDeviceDebugSettings


---@class UnityEngineInternal.Input.NativeInputEvent : System.ValueType
---@field structSize number
---@field type UnityEngineInternal.Input.NativeInputEventType
---@field sizeInBytes number
---@field deviceId number
---@field time number
---@field eventId number
UnityEngineInternal.Input.NativeInputEvent = {}
---@alias CS.UnityEngineInternal.Input.NativeInputEvent UnityEngineInternal.Input.NativeInputEvent
CS.UnityEngineInternal.Input.NativeInputEvent = UnityEngineInternal.Input.NativeInputEvent

---@param type UnityEngineInternal.Input.NativeInputEventType
---@param sizeInBytes number
---@param deviceId number
---@param time number
---@return UnityEngineInternal.Input.NativeInputEvent
function UnityEngineInternal.Input.NativeInputEvent.New(type, sizeInBytes, deviceId, time) end

---@class UnityEngineInternal.Input.NativeInputEventBuffer : System.ValueType
---@field structSize number
---@field eventBuffer System.Void*
---@field eventCount number
---@field sizeInBytes number
---@field capacityInBytes number
UnityEngineInternal.Input.NativeInputEventBuffer = {}
---@alias CS.UnityEngineInternal.Input.NativeInputEventBuffer UnityEngineInternal.Input.NativeInputEventBuffer
CS.UnityEngineInternal.Input.NativeInputEventBuffer = UnityEngineInternal.Input.NativeInputEventBuffer


---@class UnityEngineInternal.Input.NativeInputEventType
---@field DeviceRemoved UnityEngineInternal.Input.NativeInputEventType
---@field DeviceConfigChanged UnityEngineInternal.Input.NativeInputEventType
---@field Text UnityEngineInternal.Input.NativeInputEventType
---@field State UnityEngineInternal.Input.NativeInputEventType
---@field Delta UnityEngineInternal.Input.NativeInputEventType
UnityEngineInternal.Input.NativeInputEventType = {}
---@alias CS.UnityEngineInternal.Input.NativeInputEventType UnityEngineInternal.Input.NativeInputEventType
CS.UnityEngineInternal.Input.NativeInputEventType = UnityEngineInternal.Input.NativeInputEventType


---@class UnityEngineInternal.Input.NativeInputSystem : System.Object
---@field onUpdate UnityEngineInternal.Input.NativeUpdateCallback
---@field onBeforeUpdate System.Action | function
---@field onShouldRunUpdate System.Func
---@field onDeviceDiscovered System.Action | function
---@field currentTime number
---@field currentTimeOffsetToRealtimeSinceStartup number
UnityEngineInternal.Input.NativeInputSystem = {}
---@alias CS.UnityEngineInternal.Input.NativeInputSystem UnityEngineInternal.Input.NativeInputSystem
CS.UnityEngineInternal.Input.NativeInputSystem = UnityEngineInternal.Input.NativeInputSystem

---@return UnityEngineInternal.Input.NativeInputSystem
function UnityEngineInternal.Input.NativeInputSystem.New() end
---@return number
function UnityEngineInternal.Input.NativeInputSystem.AllocateDeviceId() end
---@param inputEvent System.IntPtr
function UnityEngineInternal.Input.NativeInputSystem.QueueInputEvent(inputEvent) end
---@param deviceId number
---@param code number
---@param data System.IntPtr
---@param sizeInBytes number
---@return number
function UnityEngineInternal.Input.NativeInputSystem.IOCTL(deviceId, code, data, sizeInBytes) end
---@param hertz number
function UnityEngineInternal.Input.NativeInputSystem.SetPollingFrequency(hertz) end
---@param updateType UnityEngineInternal.Input.NativeInputUpdateType
function UnityEngineInternal.Input.NativeInputSystem.Update(updateType) end

---@class UnityEngineInternal.Input.NativeInputUpdateType
---@field Dynamic UnityEngineInternal.Input.NativeInputUpdateType
---@field Fixed UnityEngineInternal.Input.NativeInputUpdateType
---@field BeforeRender UnityEngineInternal.Input.NativeInputUpdateType
---@field Editor UnityEngineInternal.Input.NativeInputUpdateType
---@field IgnoreFocus UnityEngineInternal.Input.NativeInputUpdateType
UnityEngineInternal.Input.NativeInputUpdateType = {}
---@alias CS.UnityEngineInternal.Input.NativeInputUpdateType UnityEngineInternal.Input.NativeInputUpdateType
CS.UnityEngineInternal.Input.NativeInputUpdateType = UnityEngineInternal.Input.NativeInputUpdateType


---@class UnityEngineInternal.Input.NativeUpdateCallback : System.MulticastDelegate
UnityEngineInternal.Input.NativeUpdateCallback = {}
---@alias CS.UnityEngineInternal.Input.NativeUpdateCallback UnityEngineInternal.Input.NativeUpdateCallback
CS.UnityEngineInternal.Input.NativeUpdateCallback = UnityEngineInternal.Input.NativeUpdateCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngineInternal.Input.NativeUpdateCallback
function UnityEngineInternal.Input.NativeUpdateCallback.New(object, method) end
---@param updateType UnityEngineInternal.Input.NativeInputUpdateType
---@param buffer UnityEngineInternal.Input.NativeInputEventBuffer*
function UnityEngineInternal.Input.NativeUpdateCallback:Invoke(updateType, buffer) end
---@param updateType UnityEngineInternal.Input.NativeInputUpdateType
---@param buffer UnityEngineInternal.Input.NativeInputEventBuffer*
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngineInternal.Input.NativeUpdateCallback:BeginInvoke(updateType, buffer, callback, object) end
---@param result System.IAsyncResult
function UnityEngineInternal.Input.NativeUpdateCallback:EndInvoke(result) end

---@class UnityEngineInternal.LightmapType
---@field NoLightmap UnityEngineInternal.LightmapType
---@field StaticLightmap UnityEngineInternal.LightmapType
---@field DynamicLightmap UnityEngineInternal.LightmapType
UnityEngineInternal.LightmapType = {}
---@alias CS.UnityEngineInternal.LightmapType UnityEngineInternal.LightmapType
CS.UnityEngineInternal.LightmapType = UnityEngineInternal.LightmapType


---@class UnityEngineInternal.MathfInternal : System.ValueType
---@field FloatMinNormal number
---@field FloatMinDenormal number
---@field IsFlushToZeroEnabled boolean
UnityEngineInternal.MathfInternal = {}
---@alias CS.UnityEngineInternal.MathfInternal UnityEngineInternal.MathfInternal
CS.UnityEngineInternal.MathfInternal = UnityEngineInternal.MathfInternal


---@class UnityEngineInternal.MemorylessManager : System.Object
---@field depthMemorylessMode UnityEngineInternal.MemorylessMode
UnityEngineInternal.MemorylessManager = {}
---@alias CS.UnityEngineInternal.MemorylessManager UnityEngineInternal.MemorylessManager
CS.UnityEngineInternal.MemorylessManager = UnityEngineInternal.MemorylessManager

---@return UnityEngineInternal.MemorylessManager
function UnityEngineInternal.MemorylessManager.New() end

---@class UnityEngineInternal.MemorylessMode
---@field Unused UnityEngineInternal.MemorylessMode
---@field Forced UnityEngineInternal.MemorylessMode
---@field Automatic UnityEngineInternal.MemorylessMode
UnityEngineInternal.MemorylessMode = {}
---@alias CS.UnityEngineInternal.MemorylessMode UnityEngineInternal.MemorylessMode
CS.UnityEngineInternal.MemorylessMode = UnityEngineInternal.MemorylessMode


---@class UnityEngineInternal.TypeInferenceRuleAttribute : System.Attribute
UnityEngineInternal.TypeInferenceRuleAttribute = {}
---@alias CS.UnityEngineInternal.TypeInferenceRuleAttribute UnityEngineInternal.TypeInferenceRuleAttribute
CS.UnityEngineInternal.TypeInferenceRuleAttribute = UnityEngineInternal.TypeInferenceRuleAttribute

---@overload fun(rule: UnityEngineInternal.TypeInferenceRules) : UnityEngineInternal.TypeInferenceRuleAttribute
---@param rule string
---@return UnityEngineInternal.TypeInferenceRuleAttribute
function UnityEngineInternal.TypeInferenceRuleAttribute.New(rule) end
---@return string
function UnityEngineInternal.TypeInferenceRuleAttribute:ToString() end

---@class UnityEngineInternal.TypeInferenceRules
---@field TypeReferencedByFirstArgument UnityEngineInternal.TypeInferenceRules
---@field TypeReferencedBySecondArgument UnityEngineInternal.TypeInferenceRules
---@field ArrayOfTypeReferencedByFirstArgument UnityEngineInternal.TypeInferenceRules
---@field TypeOfFirstArgument UnityEngineInternal.TypeInferenceRules
UnityEngineInternal.TypeInferenceRules = {}
---@alias CS.UnityEngineInternal.TypeInferenceRules UnityEngineInternal.TypeInferenceRules
CS.UnityEngineInternal.TypeInferenceRules = UnityEngineInternal.TypeInferenceRules


---@class UnityEngineInternal.Video.VideoAlphaLayout
---@field Native UnityEngineInternal.Video.VideoAlphaLayout
---@field Split UnityEngineInternal.Video.VideoAlphaLayout
UnityEngineInternal.Video.VideoAlphaLayout = {}
---@alias CS.UnityEngineInternal.Video.VideoAlphaLayout UnityEngineInternal.Video.VideoAlphaLayout
CS.UnityEngineInternal.Video.VideoAlphaLayout = UnityEngineInternal.Video.VideoAlphaLayout


---@class UnityEngineInternal.Video.VideoError
---@field NoErr UnityEngineInternal.Video.VideoError
---@field OutOfMemoryErr UnityEngineInternal.Video.VideoError
---@field CantReadFile UnityEngineInternal.Video.VideoError
---@field CantWriteFile UnityEngineInternal.Video.VideoError
---@field BadParams UnityEngineInternal.Video.VideoError
---@field NoData UnityEngineInternal.Video.VideoError
---@field BadPermissions UnityEngineInternal.Video.VideoError
---@field DeviceNotAvailable UnityEngineInternal.Video.VideoError
---@field ResourceNotAvailable UnityEngineInternal.Video.VideoError
---@field NetworkErr UnityEngineInternal.Video.VideoError
UnityEngineInternal.Video.VideoError = {}
---@alias CS.UnityEngineInternal.Video.VideoError UnityEngineInternal.Video.VideoError
CS.UnityEngineInternal.Video.VideoError = UnityEngineInternal.Video.VideoError


---@class UnityEngineInternal.Video.VideoPixelFormat
---@field RGB UnityEngineInternal.Video.VideoPixelFormat
---@field RGBA UnityEngineInternal.Video.VideoPixelFormat
---@field YUV UnityEngineInternal.Video.VideoPixelFormat
---@field YUVA UnityEngineInternal.Video.VideoPixelFormat
UnityEngineInternal.Video.VideoPixelFormat = {}
---@alias CS.UnityEngineInternal.Video.VideoPixelFormat UnityEngineInternal.Video.VideoPixelFormat
CS.UnityEngineInternal.Video.VideoPixelFormat = UnityEngineInternal.Video.VideoPixelFormat


---@class UnityEngineInternal.Video.VideoPlayback : System.Object
UnityEngineInternal.Video.VideoPlayback = {}
---@alias CS.UnityEngineInternal.Video.VideoPlayback UnityEngineInternal.Video.VideoPlayback
CS.UnityEngineInternal.Video.VideoPlayback = UnityEngineInternal.Video.VideoPlayback

---@return UnityEngineInternal.Video.VideoPlayback
function UnityEngineInternal.Video.VideoPlayback.New() end
function UnityEngineInternal.Video.VideoPlayback:StartPlayback() end
function UnityEngineInternal.Video.VideoPlayback:PausePlayback() end
function UnityEngineInternal.Video.VideoPlayback:StopPlayback() end
---@return UnityEngineInternal.Video.VideoError
function UnityEngineInternal.Video.VideoPlayback:GetStatus() end
---@return boolean
function UnityEngineInternal.Video.VideoPlayback:IsReady() end
---@return boolean
function UnityEngineInternal.Video.VideoPlayback:IsPlaying() end
function UnityEngineInternal.Video.VideoPlayback:Step() end
---@return boolean
function UnityEngineInternal.Video.VideoPlayback:CanStep() end
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetWidth() end
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetHeight() end
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetFrameRate() end
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetDuration() end
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetFrameCount() end
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetPixelAspectRatioNumerator() end
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetPixelAspectRatioDenominator() end
---@return UnityEngineInternal.Video.VideoPixelFormat
function UnityEngineInternal.Video.VideoPlayback:GetPixelFormat() end
---@return boolean
function UnityEngineInternal.Video.VideoPlayback:CanNotSkipOnDrop() end
---@param skipOnDrop boolean
function UnityEngineInternal.Video.VideoPlayback:SetSkipOnDrop(skipOnDrop) end
---@param texture UnityEngine.Texture
---@param out_outputFrameNum number
---@return boolean, number
function UnityEngineInternal.Video.VideoPlayback:GetTexture(texture, out_outputFrameNum) end
---@param frameIndex number
---@param seekCompletedCallback UnityEngineInternal.Video.VideoPlayback.Callback
function UnityEngineInternal.Video.VideoPlayback:SeekToFrame(frameIndex, seekCompletedCallback) end
---@param secs number
---@param seekCompletedCallback UnityEngineInternal.Video.VideoPlayback.Callback
function UnityEngineInternal.Video.VideoPlayback:SeekToTime(secs, seekCompletedCallback) end
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetPlaybackSpeed() end
---@param value number
function UnityEngineInternal.Video.VideoPlayback:SetPlaybackSpeed(value) end
---@return boolean
function UnityEngineInternal.Video.VideoPlayback:GetLoop() end
---@param value boolean
function UnityEngineInternal.Video.VideoPlayback:SetLoop(value) end
---@param enable boolean
function UnityEngineInternal.Video.VideoPlayback:SetAdjustToLinearSpace(enable) end
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetAudioTrackCount() end
---@param trackIdx number
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetAudioChannelCount(trackIdx) end
---@param trackIdx number
---@return number
function UnityEngineInternal.Video.VideoPlayback:GetAudioSampleRate(trackIdx) end
---@param trackIdx number
---@return string
function UnityEngineInternal.Video.VideoPlayback:GetAudioLanguageCode(trackIdx) end
---@param trackIdx number
---@param enabled boolean
---@param softwareOutput boolean
---@param audioSource UnityEngine.AudioSource
function UnityEngineInternal.Video.VideoPlayback:SetAudioTarget(trackIdx, enabled, softwareOutput, audioSource) end
---@param trackIndex number
---@return UnityEngine.Experimental.Audio.AudioSampleProvider
function UnityEngineInternal.Video.VideoPlayback:GetAudioSampleProvider(trackIndex) end

---@class UnityEngineInternal.Video.VideoPlayback.Callback : System.MulticastDelegate
UnityEngineInternal.Video.VideoPlayback.Callback = {}
---@alias CS.UnityEngineInternal.Video.VideoPlayback.Callback UnityEngineInternal.Video.VideoPlayback.Callback
CS.UnityEngineInternal.Video.VideoPlayback.Callback = UnityEngineInternal.Video.VideoPlayback.Callback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngineInternal.Video.VideoPlayback.Callback
function UnityEngineInternal.Video.VideoPlayback.Callback.New(object, method) end
function UnityEngineInternal.Video.VideoPlayback.Callback:Invoke() end
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngineInternal.Video.VideoPlayback.Callback:BeginInvoke(callback, object) end
---@param result System.IAsyncResult
function UnityEngineInternal.Video.VideoPlayback.Callback:EndInvoke(result) end

---@class UnityEngineInternal.Video.VideoPlaybackMgr : System.Object
---@field videoPlaybackCount number
UnityEngineInternal.Video.VideoPlaybackMgr = {}
---@alias CS.UnityEngineInternal.Video.VideoPlaybackMgr UnityEngineInternal.Video.VideoPlaybackMgr
CS.UnityEngineInternal.Video.VideoPlaybackMgr = UnityEngineInternal.Video.VideoPlaybackMgr

---@return UnityEngineInternal.Video.VideoPlaybackMgr
function UnityEngineInternal.Video.VideoPlaybackMgr.New() end
function UnityEngineInternal.Video.VideoPlaybackMgr:Dispose() end
---@param fileName string
---@param errorCallback UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback
---@param readyCallback UnityEngineInternal.Video.VideoPlaybackMgr.Callback
---@param reachedEndCallback UnityEngineInternal.Video.VideoPlaybackMgr.Callback
---@param splitAlpha boolean
---@return UnityEngineInternal.Video.VideoPlayback
function UnityEngineInternal.Video.VideoPlaybackMgr:CreateVideoPlayback(fileName, errorCallback, readyCallback, reachedEndCallback, splitAlpha) end
---@param playback UnityEngineInternal.Video.VideoPlayback
function UnityEngineInternal.Video.VideoPlaybackMgr:ReleaseVideoPlayback(playback) end
function UnityEngineInternal.Video.VideoPlaybackMgr:Update() end

---@class UnityEngineInternal.Video.VideoPlaybackMgr.Callback : System.MulticastDelegate
UnityEngineInternal.Video.VideoPlaybackMgr.Callback = {}
---@alias CS.UnityEngineInternal.Video.VideoPlaybackMgr.Callback UnityEngineInternal.Video.VideoPlaybackMgr.Callback
CS.UnityEngineInternal.Video.VideoPlaybackMgr.Callback = UnityEngineInternal.Video.VideoPlaybackMgr.Callback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngineInternal.Video.VideoPlaybackMgr.Callback
function UnityEngineInternal.Video.VideoPlaybackMgr.Callback.New(object, method) end
function UnityEngineInternal.Video.VideoPlaybackMgr.Callback:Invoke() end
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngineInternal.Video.VideoPlaybackMgr.Callback:BeginInvoke(callback, object) end
---@param result System.IAsyncResult
function UnityEngineInternal.Video.VideoPlaybackMgr.Callback:EndInvoke(result) end

---@class UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback : System.MulticastDelegate
UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback = {}
---@alias CS.UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback
CS.UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback = UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback

---@param object System.Object
---@param method System.IntPtr
---@return UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback
function UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback.New(object, method) end
---@param message string
function UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback:Invoke(message) end
---@param message string
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback:BeginInvoke(message, callback, object) end
---@param result System.IAsyncResult
function UnityEngineInternal.Video.VideoPlaybackMgr.MessageCallback:EndInvoke(result) end

---@class UnityEngineInternal.WebRequestUtils : System.Object
UnityEngineInternal.WebRequestUtils = {}
---@alias CS.UnityEngineInternal.WebRequestUtils UnityEngineInternal.WebRequestUtils
CS.UnityEngineInternal.WebRequestUtils = UnityEngineInternal.WebRequestUtils


---@class UnityEngineInternal.XR.WSA.RemoteSpeechAccess : System.Object
UnityEngineInternal.XR.WSA.RemoteSpeechAccess = {}
---@alias CS.UnityEngineInternal.XR.WSA.RemoteSpeechAccess UnityEngineInternal.XR.WSA.RemoteSpeechAccess
CS.UnityEngineInternal.XR.WSA.RemoteSpeechAccess = UnityEngineInternal.XR.WSA.RemoteSpeechAccess

---@return UnityEngineInternal.XR.WSA.RemoteSpeechAccess
function UnityEngineInternal.XR.WSA.RemoteSpeechAccess.New() end

---@class XLua.__XLua_Gen_Delegate0 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate0 = {}
---@alias CS.XLua.__XLua_Gen_Delegate0 XLua.__XLua_Gen_Delegate0
CS.XLua.__XLua_Gen_Delegate0 = XLua.__XLua_Gen_Delegate0

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate0
function XLua.__XLua_Gen_Delegate0.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param item System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate0:BeginInvoke(arg1, item, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.__XLua_Gen_Delegate0:EndInvoke(result) end
---@param arg1 System.Object
---@param item System.Object
---@return boolean
function XLua.__XLua_Gen_Delegate0:Invoke(arg1, item) end

---@class XLua.__XLua_Gen_Delegate0 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate0 = {}
---@alias CS.XLua.__XLua_Gen_Delegate0 XLua.__XLua_Gen_Delegate0
CS.XLua.__XLua_Gen_Delegate0 = XLua.__XLua_Gen_Delegate0

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate0
function XLua.__XLua_Gen_Delegate0.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate0:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate0:EndInvoke(result) end
---@param arg1 System.Object
function XLua.__XLua_Gen_Delegate0:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate1 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate1 = {}
---@alias CS.XLua.__XLua_Gen_Delegate1 XLua.__XLua_Gen_Delegate1
CS.XLua.__XLua_Gen_Delegate1 = XLua.__XLua_Gen_Delegate1

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate1
function XLua.__XLua_Gen_Delegate1.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param itemId number
---@param out_stack Game.Items.InventoryItemStack
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult, Game.Items.InventoryItemStack
function XLua.__XLua_Gen_Delegate1:BeginInvoke(arg1, itemId, out_stack, callback, object) end
---@param out_stack Game.Items.InventoryItemStack
---@param result System.IAsyncResult
---@return boolean, Game.Items.InventoryItemStack
function XLua.__XLua_Gen_Delegate1:EndInvoke(out_stack, result) end
---@param arg1 System.Object
---@param itemId number
---@param out_stack Game.Items.InventoryItemStack
---@return boolean, Game.Items.InventoryItemStack
function XLua.__XLua_Gen_Delegate1:Invoke(arg1, itemId, out_stack) end

---@class XLua.__XLua_Gen_Delegate1 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate1 = {}
---@alias CS.XLua.__XLua_Gen_Delegate1 XLua.__XLua_Gen_Delegate1
CS.XLua.__XLua_Gen_Delegate1 = XLua.__XLua_Gen_Delegate1

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate1
function XLua.__XLua_Gen_Delegate1.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param buffId number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate1:BeginInvoke(arg1, buffId, callback, object) end
---@param result System.IAsyncResult
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.__XLua_Gen_Delegate1:EndInvoke(result) end
---@param arg1 System.Object
---@param buffId number
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.__XLua_Gen_Delegate1:Invoke(arg1, buffId) end

---@class XLua.__XLua_Gen_Delegate10 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate10 = {}
---@alias CS.XLua.__XLua_Gen_Delegate10 XLua.__XLua_Gen_Delegate10
CS.XLua.__XLua_Gen_Delegate10 = XLua.__XLua_Gen_Delegate10

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate10
function XLua.__XLua_Gen_Delegate10.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate10:BeginInvoke(arg1, position, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate10:EndInvoke(result) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate10:Invoke(arg1, position) end

---@class XLua.__XLua_Gen_Delegate10 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate10 = {}
---@alias CS.XLua.__XLua_Gen_Delegate10 XLua.__XLua_Gen_Delegate10
CS.XLua.__XLua_Gen_Delegate10 = XLua.__XLua_Gen_Delegate10

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate10
function XLua.__XLua_Gen_Delegate10.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param statType Game.Gameplay.StatType
---@param baseValue number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate10:BeginInvoke(arg1, statType, baseValue, callback, object) end
---@param result System.IAsyncResult
---@return number
function XLua.__XLua_Gen_Delegate10:EndInvoke(result) end
---@param arg1 System.Object
---@param statType Game.Gameplay.StatType
---@param baseValue number
---@return number
function XLua.__XLua_Gen_Delegate10:Invoke(arg1, statType, baseValue) end

---@class XLua.__XLua_Gen_Delegate11 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate11 = {}
---@alias CS.XLua.__XLua_Gen_Delegate11 XLua.__XLua_Gen_Delegate11
CS.XLua.__XLua_Gen_Delegate11 = XLua.__XLua_Gen_Delegate11

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate11
function XLua.__XLua_Gen_Delegate11.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate11:BeginInvoke(arg1, position, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate11:EndInvoke(result) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate11:Invoke(arg1, position) end

---@class XLua.__XLua_Gen_Delegate11 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate11 = {}
---@alias CS.XLua.__XLua_Gen_Delegate11 XLua.__XLua_Gen_Delegate11
CS.XLua.__XLua_Gen_Delegate11 = XLua.__XLua_Gen_Delegate11

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate11
function XLua.__XLua_Gen_Delegate11.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param info System.Object
---@param buff System.Object
---@param source System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate11:BeginInvoke(arg1, info, buff, source, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate11:EndInvoke(result) end
---@param arg1 System.Object
---@param info System.Object
---@param buff System.Object
---@param source System.Object
function XLua.__XLua_Gen_Delegate11:Invoke(arg1, info, buff, source) end

---@class XLua.__XLua_Gen_Delegate12 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate12 = {}
---@alias CS.XLua.__XLua_Gen_Delegate12 XLua.__XLua_Gen_Delegate12
CS.XLua.__XLua_Gen_Delegate12 = XLua.__XLua_Gen_Delegate12

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate12
function XLua.__XLua_Gen_Delegate12.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate12:BeginInvoke(arg1, position, animEffect, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate12:EndInvoke(result) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate12:Invoke(arg1, position, animEffect) end

---@class XLua.__XLua_Gen_Delegate12 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate12 = {}
---@alias CS.XLua.__XLua_Gen_Delegate12 XLua.__XLua_Gen_Delegate12
CS.XLua.__XLua_Gen_Delegate12 = XLua.__XLua_Gen_Delegate12

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate12
function XLua.__XLua_Gen_Delegate12.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate12:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
---@return number
function XLua.__XLua_Gen_Delegate12:EndInvoke(result) end
---@param arg1 System.Object
---@return number
function XLua.__XLua_Gen_Delegate12:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate13 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate13 = {}
---@alias CS.XLua.__XLua_Gen_Delegate13 XLua.__XLua_Gen_Delegate13
CS.XLua.__XLua_Gen_Delegate13 = XLua.__XLua_Gen_Delegate13

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate13
function XLua.__XLua_Gen_Delegate13.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate13:BeginInvoke(arg1, position, animEffect, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate13:EndInvoke(result) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate13:Invoke(arg1, position, animEffect) end

---@class XLua.__XLua_Gen_Delegate13 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate13 = {}
---@alias CS.XLua.__XLua_Gen_Delegate13 XLua.__XLua_Gen_Delegate13
CS.XLua.__XLua_Gen_Delegate13 = XLua.__XLua_Gen_Delegate13

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate13
function XLua.__XLua_Gen_Delegate13.New(objectInstance, functionPtr) end
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate13:BeginInvoke(callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate13:EndInvoke(result) end
function XLua.__XLua_Gen_Delegate13:Invoke() end

---@class XLua.__XLua_Gen_Delegate14 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate14 = {}
---@alias CS.XLua.__XLua_Gen_Delegate14 XLua.__XLua_Gen_Delegate14
CS.XLua.__XLua_Gen_Delegate14 = XLua.__XLua_Gen_Delegate14

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate14
function XLua.__XLua_Gen_Delegate14.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param animEffectKey System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate14:BeginInvoke(arg1, position, animEffectKey, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate14:EndInvoke(result) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param animEffectKey System.Object
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate14:Invoke(arg1, position, animEffectKey) end

---@class XLua.__XLua_Gen_Delegate14 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate14 = {}
---@alias CS.XLua.__XLua_Gen_Delegate14 XLua.__XLua_Gen_Delegate14
CS.XLua.__XLua_Gen_Delegate14 = XLua.__XLua_Gen_Delegate14

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate14
function XLua.__XLua_Gen_Delegate14.New(objectInstance, functionPtr) end
---@param info System.Object
---@param deltaTime number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate14:BeginInvoke(info, deltaTime, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate14:EndInvoke(result) end
---@param info System.Object
---@param deltaTime number
function XLua.__XLua_Gen_Delegate14:Invoke(info, deltaTime) end

---@class XLua.__XLua_Gen_Delegate15 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate15 = {}
---@alias CS.XLua.__XLua_Gen_Delegate15 XLua.__XLua_Gen_Delegate15
CS.XLua.__XLua_Gen_Delegate15 = XLua.__XLua_Gen_Delegate15

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate15
function XLua.__XLua_Gen_Delegate15.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param animEffectKey System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate15:BeginInvoke(arg1, position, animEffectKey, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate15:EndInvoke(result) end
---@param arg1 System.Object
---@param position UnityEngine.Vector3
---@param animEffectKey System.Object
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate15:Invoke(arg1, position, animEffectKey) end

---@class XLua.__XLua_Gen_Delegate15 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate15 = {}
---@alias CS.XLua.__XLua_Gen_Delegate15 XLua.__XLua_Gen_Delegate15
CS.XLua.__XLua_Gen_Delegate15 = XLua.__XLua_Gen_Delegate15

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate15
function XLua.__XLua_Gen_Delegate15.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param fsm System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate15:BeginInvoke(arg1, fsm, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate15:EndInvoke(result) end
---@param arg1 System.Object
---@param fsm System.Object
function XLua.__XLua_Gen_Delegate15:Invoke(arg1, fsm) end

---@class XLua.__XLua_Gen_Delegate16 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate16 = {}
---@alias CS.XLua.__XLua_Gen_Delegate16 XLua.__XLua_Gen_Delegate16
CS.XLua.__XLua_Gen_Delegate16 = XLua.__XLua_Gen_Delegate16

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate16
function XLua.__XLua_Gen_Delegate16.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate16:BeginInvoke(arg1, prefab, position, animEffect, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate16:EndInvoke(result) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate16:Invoke(arg1, prefab, position, animEffect) end

---@class XLua.__XLua_Gen_Delegate16 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate16 = {}
---@alias CS.XLua.__XLua_Gen_Delegate16 XLua.__XLua_Gen_Delegate16
CS.XLua.__XLua_Gen_Delegate16 = XLua.__XLua_Gen_Delegate16

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate16
function XLua.__XLua_Gen_Delegate16.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param spawnPosition UnityEngine.Vector3
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate16:BeginInvoke(arg1, spawnPosition, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate16:EndInvoke(result) end
---@param arg1 System.Object
---@param spawnPosition UnityEngine.Vector3
function XLua.__XLua_Gen_Delegate16:Invoke(arg1, spawnPosition) end

---@class XLua.__XLua_Gen_Delegate17 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate17 = {}
---@alias CS.XLua.__XLua_Gen_Delegate17 XLua.__XLua_Gen_Delegate17
CS.XLua.__XLua_Gen_Delegate17 = XLua.__XLua_Gen_Delegate17

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate17
function XLua.__XLua_Gen_Delegate17.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate17:BeginInvoke(arg1, prefab, position, animEffect, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate17:EndInvoke(result) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate17:Invoke(arg1, prefab, position, animEffect) end

---@class XLua.__XLua_Gen_Delegate17 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate17 = {}
---@alias CS.XLua.__XLua_Gen_Delegate17 XLua.__XLua_Gen_Delegate17
CS.XLua.__XLua_Gen_Delegate17 = XLua.__XLua_Gen_Delegate17

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate17
function XLua.__XLua_Gen_Delegate17.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param enemyData Game.Gameplay.EnemyData
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate17:BeginInvoke(arg1, enemyData, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate17:EndInvoke(result) end
---@param arg1 System.Object
---@param enemyData Game.Gameplay.EnemyData
function XLua.__XLua_Gen_Delegate17:Invoke(arg1, enemyData) end

---@class XLua.__XLua_Gen_Delegate18 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate18 = {}
---@alias CS.XLua.__XLua_Gen_Delegate18 XLua.__XLua_Gen_Delegate18
CS.XLua.__XLua_Gen_Delegate18 = XLua.__XLua_Gen_Delegate18

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate18
function XLua.__XLua_Gen_Delegate18.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@param animDuration number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate18:BeginInvoke(arg1, prefab, position, animEffect, animDuration, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate18:EndInvoke(result) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@param animDuration number
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate18:Invoke(arg1, prefab, position, animEffect, animDuration) end

---@class XLua.__XLua_Gen_Delegate18 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate18 = {}
---@alias CS.XLua.__XLua_Gen_Delegate18 XLua.__XLua_Gen_Delegate18
CS.XLua.__XLua_Gen_Delegate18 = XLua.__XLua_Gen_Delegate18

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate18
function XLua.__XLua_Gen_Delegate18.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param out_direction UnityEngine.Vector2
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult, UnityEngine.Vector2
function XLua.__XLua_Gen_Delegate18:BeginInvoke(arg1, out_direction, callback, object) end
---@param out_direction UnityEngine.Vector2
---@param result System.IAsyncResult
---@return boolean, UnityEngine.Vector2
function XLua.__XLua_Gen_Delegate18:EndInvoke(out_direction, result) end
---@param arg1 System.Object
---@param out_direction UnityEngine.Vector2
---@return boolean, UnityEngine.Vector2
function XLua.__XLua_Gen_Delegate18:Invoke(arg1, out_direction) end

---@class XLua.__XLua_Gen_Delegate19 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate19 = {}
---@alias CS.XLua.__XLua_Gen_Delegate19 XLua.__XLua_Gen_Delegate19
CS.XLua.__XLua_Gen_Delegate19 = XLua.__XLua_Gen_Delegate19

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate19
function XLua.__XLua_Gen_Delegate19.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@param animDuration number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate19:BeginInvoke(arg1, prefab, position, animEffect, animDuration, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate19:EndInvoke(result) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffect Game.Animation.DOTweenAnimType
---@param animDuration number
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate19:Invoke(arg1, prefab, position, animEffect, animDuration) end

---@class XLua.__XLua_Gen_Delegate19 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate19 = {}
---@alias CS.XLua.__XLua_Gen_Delegate19 XLua.__XLua_Gen_Delegate19
CS.XLua.__XLua_Gen_Delegate19 = XLua.__XLua_Gen_Delegate19

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate19
function XLua.__XLua_Gen_Delegate19.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param parameterName System.Object
---@param parameterType UnityEngine.AnimatorControllerParameterType
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate19:BeginInvoke(arg1, parameterName, parameterType, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.__XLua_Gen_Delegate19:EndInvoke(result) end
---@param arg1 System.Object
---@param parameterName System.Object
---@param parameterType UnityEngine.AnimatorControllerParameterType
---@return boolean
function XLua.__XLua_Gen_Delegate19:Invoke(arg1, parameterName, parameterType) end

---@class XLua.__XLua_Gen_Delegate2 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate2 = {}
---@alias CS.XLua.__XLua_Gen_Delegate2 XLua.__XLua_Gen_Delegate2
CS.XLua.__XLua_Gen_Delegate2 = XLua.__XLua_Gen_Delegate2

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate2
function XLua.__XLua_Gen_Delegate2.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param itemId number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate2:BeginInvoke(arg1, itemId, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.__XLua_Gen_Delegate2:EndInvoke(result) end
---@param arg1 System.Object
---@param itemId number
---@return boolean
function XLua.__XLua_Gen_Delegate2:Invoke(arg1, itemId) end

---@class XLua.__XLua_Gen_Delegate2 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate2 = {}
---@alias CS.XLua.__XLua_Gen_Delegate2 XLua.__XLua_Gen_Delegate2
CS.XLua.__XLua_Gen_Delegate2 = XLua.__XLua_Gen_Delegate2

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate2
function XLua.__XLua_Gen_Delegate2.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param buffId number
---@param source System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate2:BeginInvoke(arg1, buffId, source, callback, object) end
---@param result System.IAsyncResult
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.__XLua_Gen_Delegate2:EndInvoke(result) end
---@param arg1 System.Object
---@param buffId number
---@param source System.Object
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.__XLua_Gen_Delegate2:Invoke(arg1, buffId, source) end

---@class XLua.__XLua_Gen_Delegate20 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate20 = {}
---@alias CS.XLua.__XLua_Gen_Delegate20 XLua.__XLua_Gen_Delegate20
CS.XLua.__XLua_Gen_Delegate20 = XLua.__XLua_Gen_Delegate20

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate20
function XLua.__XLua_Gen_Delegate20.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffectKey System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate20:BeginInvoke(arg1, prefab, position, animEffectKey, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate20:EndInvoke(result) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffectKey System.Object
---@return UnityEngine.GameObject
function XLua.__XLua_Gen_Delegate20:Invoke(arg1, prefab, position, animEffectKey) end

---@class XLua.__XLua_Gen_Delegate20 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate20 = {}
---@alias CS.XLua.__XLua_Gen_Delegate20 XLua.__XLua_Gen_Delegate20
CS.XLua.__XLua_Gen_Delegate20 = XLua.__XLua_Gen_Delegate20

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate20
function XLua.__XLua_Gen_Delegate20.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param stateName System.Object
---@param restart boolean
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate20:BeginInvoke(arg1, stateName, restart, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate20:EndInvoke(result) end
---@param arg1 System.Object
---@param stateName System.Object
---@param restart boolean
function XLua.__XLua_Gen_Delegate20:Invoke(arg1, stateName, restart) end

---@class XLua.__XLua_Gen_Delegate21 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate21 = {}
---@alias CS.XLua.__XLua_Gen_Delegate21 XLua.__XLua_Gen_Delegate21
CS.XLua.__XLua_Gen_Delegate21 = XLua.__XLua_Gen_Delegate21

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate21
function XLua.__XLua_Gen_Delegate21.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffectKey System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate21:BeginInvoke(arg1, prefab, position, animEffectKey, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate21:EndInvoke(result) end
---@param arg1 System.Object
---@param prefab System.Object
---@param position UnityEngine.Vector3
---@param animEffectKey System.Object
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate21:Invoke(arg1, prefab, position, animEffectKey) end

---@class XLua.__XLua_Gen_Delegate21 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate21 = {}
---@alias CS.XLua.__XLua_Gen_Delegate21 XLua.__XLua_Gen_Delegate21
CS.XLua.__XLua_Gen_Delegate21 = XLua.__XLua_Gen_Delegate21

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate21
function XLua.__XLua_Gen_Delegate21.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate21:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
---@return UnityEngine.Vector3
function XLua.__XLua_Gen_Delegate21:EndInvoke(result) end
---@param arg1 System.Object
---@return UnityEngine.Vector3
function XLua.__XLua_Gen_Delegate21:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate22 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate22 = {}
---@alias CS.XLua.__XLua_Gen_Delegate22 XLua.__XLua_Gen_Delegate22
CS.XLua.__XLua_Gen_Delegate22 = XLua.__XLua_Gen_Delegate22

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate22
function XLua.__XLua_Gen_Delegate22.New(objectInstance, functionPtr) end
---@param animEffect Game.Animation.DOTweenAnimType
---@param animDuration number
---@param obj System.Object
---@param item System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate22:BeginInvoke(animEffect, animDuration, obj, item, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate22:EndInvoke(result) end
---@param animEffect Game.Animation.DOTweenAnimType
---@param animDuration number
---@param obj System.Object
---@param item System.Object
function XLua.__XLua_Gen_Delegate22:Invoke(animEffect, animDuration, obj, item) end

---@class XLua.__XLua_Gen_Delegate22 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate22 = {}
---@alias CS.XLua.__XLua_Gen_Delegate22 XLua.__XLua_Gen_Delegate22
CS.XLua.__XLua_Gen_Delegate22 = XLua.__XLua_Gen_Delegate22

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate22
function XLua.__XLua_Gen_Delegate22.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param applyDefaultShape boolean
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate22:BeginInvoke(arg1, applyDefaultShape, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate22:EndInvoke(result) end
---@param arg1 System.Object
---@param applyDefaultShape boolean
function XLua.__XLua_Gen_Delegate22:Invoke(arg1, applyDefaultShape) end

---@class XLua.__XLua_Gen_Delegate23 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate23 = {}
---@alias CS.XLua.__XLua_Gen_Delegate23 XLua.__XLua_Gen_Delegate23
CS.XLua.__XLua_Gen_Delegate23 = XLua.__XLua_Gen_Delegate23

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate23
function XLua.__XLua_Gen_Delegate23.New(objectInstance, functionPtr) end
---@param animEffectKey System.Object
---@param animDuration number
---@param obj System.Object
---@param item System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate23:BeginInvoke(animEffectKey, animDuration, obj, item, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate23:EndInvoke(result) end
---@param animEffectKey System.Object
---@param animDuration number
---@param obj System.Object
---@param item System.Object
function XLua.__XLua_Gen_Delegate23:Invoke(animEffectKey, animDuration, obj, item) end

---@class XLua.__XLua_Gen_Delegate23 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate23 = {}
---@alias CS.XLua.__XLua_Gen_Delegate23 XLua.__XLua_Gen_Delegate23
CS.XLua.__XLua_Gen_Delegate23 = XLua.__XLua_Gen_Delegate23

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate23
function XLua.__XLua_Gen_Delegate23.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate23:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate23:EndInvoke(result) end
---@param arg1 System.Object
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate23:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate24 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate24 = {}
---@alias CS.XLua.__XLua_Gen_Delegate24 XLua.__XLua_Gen_Delegate24
CS.XLua.__XLua_Gen_Delegate24 = XLua.__XLua_Gen_Delegate24

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate24
function XLua.__XLua_Gen_Delegate24.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param dir UnityEngine.Vector2
---@param mouseCombatBlocked boolean
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate24:BeginInvoke(arg1, dir, mouseCombatBlocked, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate24:EndInvoke(result) end
---@param arg1 System.Object
---@param dir UnityEngine.Vector2
---@param mouseCombatBlocked boolean
function XLua.__XLua_Gen_Delegate24:Invoke(arg1, dir, mouseCombatBlocked) end

---@class XLua.__XLua_Gen_Delegate25 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate25 = {}
---@alias CS.XLua.__XLua_Gen_Delegate25 XLua.__XLua_Gen_Delegate25
CS.XLua.__XLua_Gen_Delegate25 = XLua.__XLua_Gen_Delegate25

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate25
function XLua.__XLua_Gen_Delegate25.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate25:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.__XLua_Gen_Delegate25:EndInvoke(result) end
---@param arg1 System.Object
---@return boolean
function XLua.__XLua_Gen_Delegate25:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate26 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate26 = {}
---@alias CS.XLua.__XLua_Gen_Delegate26 XLua.__XLua_Gen_Delegate26
CS.XLua.__XLua_Gen_Delegate26 = XLua.__XLua_Gen_Delegate26

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate26
function XLua.__XLua_Gen_Delegate26.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate26:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
---@return System.Collections.IEnumerator
function XLua.__XLua_Gen_Delegate26:EndInvoke(result) end
---@param arg1 System.Object
---@return System.Collections.IEnumerator
function XLua.__XLua_Gen_Delegate26:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate27 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate27 = {}
---@alias CS.XLua.__XLua_Gen_Delegate27 XLua.__XLua_Gen_Delegate27
CS.XLua.__XLua_Gen_Delegate27 = XLua.__XLua_Gen_Delegate27

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate27
function XLua.__XLua_Gen_Delegate27.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate27:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
---@return number
function XLua.__XLua_Gen_Delegate27:EndInvoke(result) end
---@param arg1 System.Object
---@return number
function XLua.__XLua_Gen_Delegate27:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate28 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate28 = {}
---@alias CS.XLua.__XLua_Gen_Delegate28 XLua.__XLua_Gen_Delegate28
CS.XLua.__XLua_Gen_Delegate28 = XLua.__XLua_Gen_Delegate28

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate28
function XLua.__XLua_Gen_Delegate28.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param amount number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate28:BeginInvoke(arg1, amount, callback, object) end
---@param result System.IAsyncResult
---@return number
function XLua.__XLua_Gen_Delegate28:EndInvoke(result) end
---@param arg1 System.Object
---@param amount number
---@return number
function XLua.__XLua_Gen_Delegate28:Invoke(arg1, amount) end

---@class XLua.__XLua_Gen_Delegate29 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate29 = {}
---@alias CS.XLua.__XLua_Gen_Delegate29 XLua.__XLua_Gen_Delegate29
CS.XLua.__XLua_Gen_Delegate29 = XLua.__XLua_Gen_Delegate29

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate29
function XLua.__XLua_Gen_Delegate29.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param data System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate29:BeginInvoke(arg1, data, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate29:EndInvoke(result) end
---@param arg1 System.Object
---@param data System.Object
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate29:Invoke(arg1, data) end

---@class XLua.__XLua_Gen_Delegate3 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate3 = {}
---@alias CS.XLua.__XLua_Gen_Delegate3 XLua.__XLua_Gen_Delegate3
CS.XLua.__XLua_Gen_Delegate3 = XLua.__XLua_Gen_Delegate3

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate3
function XLua.__XLua_Gen_Delegate3.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate3:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate3:EndInvoke(result) end
---@param arg1 System.Object
function XLua.__XLua_Gen_Delegate3:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate3 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate3 = {}
---@alias CS.XLua.__XLua_Gen_Delegate3 XLua.__XLua_Gen_Delegate3
CS.XLua.__XLua_Gen_Delegate3 = XLua.__XLua_Gen_Delegate3

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate3
function XLua.__XLua_Gen_Delegate3.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param buff System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate3:BeginInvoke(arg1, buff, callback, object) end
---@param result System.IAsyncResult
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.__XLua_Gen_Delegate3:EndInvoke(result) end
---@param arg1 System.Object
---@param buff System.Object
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.__XLua_Gen_Delegate3:Invoke(arg1, buff) end

---@class XLua.__XLua_Gen_Delegate30 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate30 = {}
---@alias CS.XLua.__XLua_Gen_Delegate30 XLua.__XLua_Gen_Delegate30
CS.XLua.__XLua_Gen_Delegate30 = XLua.__XLua_Gen_Delegate30

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate30
function XLua.__XLua_Gen_Delegate30.New(objectInstance, functionPtr) end
---@param itemId number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate30:BeginInvoke(itemId, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate30:EndInvoke(result) end
---@param itemId number
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate30:Invoke(itemId) end

---@class XLua.__XLua_Gen_Delegate31 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate31 = {}
---@alias CS.XLua.__XLua_Gen_Delegate31 XLua.__XLua_Gen_Delegate31
CS.XLua.__XLua_Gen_Delegate31 = XLua.__XLua_Gen_Delegate31

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate31
function XLua.__XLua_Gen_Delegate31.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param text System.Object
---@param duration number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate31:BeginInvoke(arg1, text, duration, callback, object) end
---@param result System.IAsyncResult
---@return System.Collections.IEnumerator
function XLua.__XLua_Gen_Delegate31:EndInvoke(result) end
---@param arg1 System.Object
---@param text System.Object
---@param duration number
---@return System.Collections.IEnumerator
function XLua.__XLua_Gen_Delegate31:Invoke(arg1, text, duration) end

---@class XLua.__XLua_Gen_Delegate32 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate32 = {}
---@alias CS.XLua.__XLua_Gen_Delegate32 XLua.__XLua_Gen_Delegate32
CS.XLua.__XLua_Gen_Delegate32 = XLua.__XLua_Gen_Delegate32

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate32
function XLua.__XLua_Gen_Delegate32.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param text System.Object
---@param duration number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate32:BeginInvoke(arg1, text, duration, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate32:EndInvoke(result) end
---@param arg1 System.Object
---@param text System.Object
---@param duration number
function XLua.__XLua_Gen_Delegate32:Invoke(arg1, text, duration) end

---@class XLua.__XLua_Gen_Delegate33 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate33 = {}
---@alias CS.XLua.__XLua_Gen_Delegate33 XLua.__XLua_Gen_Delegate33
CS.XLua.__XLua_Gen_Delegate33 = XLua.__XLua_Gen_Delegate33

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate33
function XLua.__XLua_Gen_Delegate33.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param ref_dir UnityEngine.Vector2
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult, UnityEngine.Vector2
function XLua.__XLua_Gen_Delegate33:BeginInvoke(arg1, ref_dir, callback, object) end
---@param ref_dir UnityEngine.Vector2
---@param result System.IAsyncResult
---@return UnityEngine.Vector2
function XLua.__XLua_Gen_Delegate33:EndInvoke(ref_dir, result) end
---@param arg1 System.Object
---@param ref_dir UnityEngine.Vector2
---@return UnityEngine.Vector2
function XLua.__XLua_Gen_Delegate33:Invoke(arg1, ref_dir) end

---@class XLua.__XLua_Gen_Delegate34 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate34 = {}
---@alias CS.XLua.__XLua_Gen_Delegate34 XLua.__XLua_Gen_Delegate34
CS.XLua.__XLua_Gen_Delegate34 = XLua.__XLua_Gen_Delegate34

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate34
function XLua.__XLua_Gen_Delegate34.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param shootDir UnityEngine.Vector2
---@param bulletDamage number
---@param bulletSpeed number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate34:BeginInvoke(arg1, shootDir, bulletDamage, bulletSpeed, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate34:EndInvoke(result) end
---@param arg1 System.Object
---@param shootDir UnityEngine.Vector2
---@param bulletDamage number
---@param bulletSpeed number
function XLua.__XLua_Gen_Delegate34:Invoke(arg1, shootDir, bulletDamage, bulletSpeed) end

---@class XLua.__XLua_Gen_Delegate35 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate35 = {}
---@alias CS.XLua.__XLua_Gen_Delegate35 XLua.__XLua_Gen_Delegate35
CS.XLua.__XLua_Gen_Delegate35 = XLua.__XLua_Gen_Delegate35

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate35
function XLua.__XLua_Gen_Delegate35.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param dir UnityEngine.Vector2
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate35:BeginInvoke(arg1, dir, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate35:EndInvoke(result) end
---@param arg1 System.Object
---@param dir UnityEngine.Vector2
function XLua.__XLua_Gen_Delegate35:Invoke(arg1, dir) end

---@class XLua.__XLua_Gen_Delegate36 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate36 = {}
---@alias CS.XLua.__XLua_Gen_Delegate36 XLua.__XLua_Gen_Delegate36
CS.XLua.__XLua_Gen_Delegate36 = XLua.__XLua_Gen_Delegate36

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate36
function XLua.__XLua_Gen_Delegate36.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param clipAmmo number
---@param clipMaxAmmo number
---@param bagAmmo number
---@param bagMaxAmmo number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate36:BeginInvoke(arg1, clipAmmo, clipMaxAmmo, bagAmmo, bagMaxAmmo, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate36:EndInvoke(result) end
---@param arg1 System.Object
---@param clipAmmo number
---@param clipMaxAmmo number
---@param bagAmmo number
---@param bagMaxAmmo number
function XLua.__XLua_Gen_Delegate36:Invoke(arg1, clipAmmo, clipMaxAmmo, bagAmmo, bagMaxAmmo) end

---@class XLua.__XLua_Gen_Delegate37 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate37 = {}
---@alias CS.XLua.__XLua_Gen_Delegate37 XLua.__XLua_Gen_Delegate37
CS.XLua.__XLua_Gen_Delegate37 = XLua.__XLua_Gen_Delegate37

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate37
function XLua.__XLua_Gen_Delegate37.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param data Game.Gameplay.WeaponData
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate37:BeginInvoke(arg1, data, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate37:EndInvoke(result) end
---@param arg1 System.Object
---@param data Game.Gameplay.WeaponData
function XLua.__XLua_Gen_Delegate37:Invoke(arg1, data) end

---@class XLua.__XLua_Gen_Delegate38 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate38 = {}
---@alias CS.XLua.__XLua_Gen_Delegate38 XLua.__XLua_Gen_Delegate38
CS.XLua.__XLua_Gen_Delegate38 = XLua.__XLua_Gen_Delegate38

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate38
function XLua.__XLua_Gen_Delegate38.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param dir UnityEngine.Vector2
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate38:BeginInvoke(arg1, dir, callback, object) end
---@param result System.IAsyncResult
---@return Game.Gameplay.PlayerBullet
function XLua.__XLua_Gen_Delegate38:EndInvoke(result) end
---@param arg1 System.Object
---@param dir UnityEngine.Vector2
---@return Game.Gameplay.PlayerBullet
function XLua.__XLua_Gen_Delegate38:Invoke(arg1, dir) end

---@class XLua.__XLua_Gen_Delegate39 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate39 = {}
---@alias CS.XLua.__XLua_Gen_Delegate39 XLua.__XLua_Gen_Delegate39
CS.XLua.__XLua_Gen_Delegate39 = XLua.__XLua_Gen_Delegate39

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate39
function XLua.__XLua_Gen_Delegate39.New(objectInstance, functionPtr) end
---@param playerTransform System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate39:BeginInvoke(playerTransform, callback, object) end
---@param result System.IAsyncResult
---@return Game.Gameplay.EnemyBase
function XLua.__XLua_Gen_Delegate39:EndInvoke(result) end
---@param playerTransform System.Object
---@return Game.Gameplay.EnemyBase
function XLua.__XLua_Gen_Delegate39:Invoke(playerTransform) end

---@class XLua.__XLua_Gen_Delegate4 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate4 = {}
---@alias CS.XLua.__XLua_Gen_Delegate4 XLua.__XLua_Gen_Delegate4
CS.XLua.__XLua_Gen_Delegate4 = XLua.__XLua_Gen_Delegate4

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate4
function XLua.__XLua_Gen_Delegate4.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param itemId number
---@param count number
---@param database System.Object
---@param effects System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate4:BeginInvoke(arg1, itemId, count, database, effects, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.__XLua_Gen_Delegate4:EndInvoke(result) end
---@param arg1 System.Object
---@param itemId number
---@param count number
---@param database System.Object
---@param effects System.Object
---@return boolean
function XLua.__XLua_Gen_Delegate4:Invoke(arg1, itemId, count, database, effects) end

---@class XLua.__XLua_Gen_Delegate4 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate4 = {}
---@alias CS.XLua.__XLua_Gen_Delegate4 XLua.__XLua_Gen_Delegate4
CS.XLua.__XLua_Gen_Delegate4 = XLua.__XLua_Gen_Delegate4

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate4
function XLua.__XLua_Gen_Delegate4.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param buff System.Object
---@param source System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate4:BeginInvoke(arg1, buff, source, callback, object) end
---@param result System.IAsyncResult
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.__XLua_Gen_Delegate4:EndInvoke(result) end
---@param arg1 System.Object
---@param buff System.Object
---@param source System.Object
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.__XLua_Gen_Delegate4:Invoke(arg1, buff, source) end

---@class XLua.__XLua_Gen_Delegate40 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate40 = {}
---@alias CS.XLua.__XLua_Gen_Delegate40 XLua.__XLua_Gen_Delegate40
CS.XLua.__XLua_Gen_Delegate40 = XLua.__XLua_Gen_Delegate40

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate40
function XLua.__XLua_Gen_Delegate40.New(objectInstance, functionPtr) end
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate40:BeginInvoke(callback, object) end
---@param result System.IAsyncResult
---@return System.Collections.Generic.IReadOnlyList
function XLua.__XLua_Gen_Delegate40:EndInvoke(result) end
---@return System.Collections.Generic.IReadOnlyList
function XLua.__XLua_Gen_Delegate40:Invoke() end

---@class XLua.__XLua_Gen_Delegate41 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate41 = {}
---@alias CS.XLua.__XLua_Gen_Delegate41 XLua.__XLua_Gen_Delegate41
CS.XLua.__XLua_Gen_Delegate41 = XLua.__XLua_Gen_Delegate41

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate41
function XLua.__XLua_Gen_Delegate41.New(objectInstance, functionPtr) end
---@param slotIndex number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate41:BeginInvoke(slotIndex, callback, object) end
---@param result System.IAsyncResult
---@return Game.Gameplay.Save.SaveOperationResult
function XLua.__XLua_Gen_Delegate41:EndInvoke(result) end
---@param slotIndex number
---@return Game.Gameplay.Save.SaveOperationResult
function XLua.__XLua_Gen_Delegate41:Invoke(slotIndex) end

---@class XLua.__XLua_Gen_Delegate42 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate42 = {}
---@alias CS.XLua.__XLua_Gen_Delegate42 XLua.__XLua_Gen_Delegate42
CS.XLua.__XLua_Gen_Delegate42 = XLua.__XLua_Gen_Delegate42

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate42
function XLua.__XLua_Gen_Delegate42.New(objectInstance, functionPtr) end
---@param slotIndex number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate42:BeginInvoke(slotIndex, callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate42:EndInvoke(result) end
---@param slotIndex number
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate42:Invoke(slotIndex) end

---@class XLua.__XLua_Gen_Delegate43 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate43 = {}
---@alias CS.XLua.__XLua_Gen_Delegate43 XLua.__XLua_Gen_Delegate43
CS.XLua.__XLua_Gen_Delegate43 = XLua.__XLua_Gen_Delegate43

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate43
function XLua.__XLua_Gen_Delegate43.New(objectInstance, functionPtr) end
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate43:BeginInvoke(callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate43:EndInvoke(result) end
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate43:Invoke() end

---@class XLua.__XLua_Gen_Delegate44 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate44 = {}
---@alias CS.XLua.__XLua_Gen_Delegate44 XLua.__XLua_Gen_Delegate44
CS.XLua.__XLua_Gen_Delegate44 = XLua.__XLua_Gen_Delegate44

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate44
function XLua.__XLua_Gen_Delegate44.New(objectInstance, functionPtr) end
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate44:BeginInvoke(callback, object) end
---@param result System.IAsyncResult
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate44:EndInvoke(result) end
---@return System.Threading.Tasks.Task
function XLua.__XLua_Gen_Delegate44:Invoke() end

---@class XLua.__XLua_Gen_Delegate5 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate5 = {}
---@alias CS.XLua.__XLua_Gen_Delegate5 XLua.__XLua_Gen_Delegate5
CS.XLua.__XLua_Gen_Delegate5 = XLua.__XLua_Gen_Delegate5

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate5
function XLua.__XLua_Gen_Delegate5.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param enabled boolean
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate5:BeginInvoke(arg1, enabled, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate5:EndInvoke(result) end
---@param arg1 System.Object
---@param enabled boolean
function XLua.__XLua_Gen_Delegate5:Invoke(arg1, enabled) end

---@class XLua.__XLua_Gen_Delegate5 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate5 = {}
---@alias CS.XLua.__XLua_Gen_Delegate5 XLua.__XLua_Gen_Delegate5
CS.XLua.__XLua_Gen_Delegate5 = XLua.__XLua_Gen_Delegate5

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate5
function XLua.__XLua_Gen_Delegate5.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param buff System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate5:BeginInvoke(arg1, buff, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.__XLua_Gen_Delegate5:EndInvoke(result) end
---@param arg1 System.Object
---@param buff System.Object
---@return boolean
function XLua.__XLua_Gen_Delegate5:Invoke(arg1, buff) end

---@class XLua.__XLua_Gen_Delegate6 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate6 = {}
---@alias CS.XLua.__XLua_Gen_Delegate6 XLua.__XLua_Gen_Delegate6
CS.XLua.__XLua_Gen_Delegate6 = XLua.__XLua_Gen_Delegate6

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate6
function XLua.__XLua_Gen_Delegate6.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param other System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate6:BeginInvoke(arg1, other, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate6:EndInvoke(result) end
---@param arg1 System.Object
---@param other System.Object
function XLua.__XLua_Gen_Delegate6:Invoke(arg1, other) end

---@class XLua.__XLua_Gen_Delegate6 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate6 = {}
---@alias CS.XLua.__XLua_Gen_Delegate6 XLua.__XLua_Gen_Delegate6
CS.XLua.__XLua_Gen_Delegate6 = XLua.__XLua_Gen_Delegate6

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate6
function XLua.__XLua_Gen_Delegate6.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param buffId number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate6:BeginInvoke(arg1, buffId, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.__XLua_Gen_Delegate6:EndInvoke(result) end
---@param arg1 System.Object
---@param buffId number
---@return boolean
function XLua.__XLua_Gen_Delegate6:Invoke(arg1, buffId) end

---@class XLua.__XLua_Gen_Delegate7 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate7 = {}
---@alias CS.XLua.__XLua_Gen_Delegate7 XLua.__XLua_Gen_Delegate7
CS.XLua.__XLua_Gen_Delegate7 = XLua.__XLua_Gen_Delegate7

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate7
function XLua.__XLua_Gen_Delegate7.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate7:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.__XLua_Gen_Delegate7:EndInvoke(result) end
---@param arg1 System.Object
---@return boolean
function XLua.__XLua_Gen_Delegate7:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate7 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate7 = {}
---@alias CS.XLua.__XLua_Gen_Delegate7 XLua.__XLua_Gen_Delegate7
CS.XLua.__XLua_Gen_Delegate7 = XLua.__XLua_Gen_Delegate7

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate7
function XLua.__XLua_Gen_Delegate7.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param tag Game.Gameplay.BuffTag
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate7:BeginInvoke(arg1, tag, callback, object) end
---@param result System.IAsyncResult
---@return number
function XLua.__XLua_Gen_Delegate7:EndInvoke(result) end
---@param arg1 System.Object
---@param tag Game.Gameplay.BuffTag
---@return number
function XLua.__XLua_Gen_Delegate7:Invoke(arg1, tag) end

---@class XLua.__XLua_Gen_Delegate8 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate8 = {}
---@alias CS.XLua.__XLua_Gen_Delegate8 XLua.__XLua_Gen_Delegate8
CS.XLua.__XLua_Gen_Delegate8 = XLua.__XLua_Gen_Delegate8

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate8
function XLua.__XLua_Gen_Delegate8.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate8:BeginInvoke(arg1, callback, object) end
---@param result System.IAsyncResult
---@return Game.Items.ItemData
function XLua.__XLua_Gen_Delegate8:EndInvoke(result) end
---@param arg1 System.Object
---@return Game.Items.ItemData
function XLua.__XLua_Gen_Delegate8:Invoke(arg1) end

---@class XLua.__XLua_Gen_Delegate8 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate8 = {}
---@alias CS.XLua.__XLua_Gen_Delegate8 XLua.__XLua_Gen_Delegate8
CS.XLua.__XLua_Gen_Delegate8 = XLua.__XLua_Gen_Delegate8

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate8
function XLua.__XLua_Gen_Delegate8.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param buffId number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate8:BeginInvoke(arg1, buffId, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate8:EndInvoke(result) end
---@param arg1 System.Object
---@param buffId number
function XLua.__XLua_Gen_Delegate8:Invoke(arg1, buffId) end

---@class XLua.__XLua_Gen_Delegate9 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate9 = {}
---@alias CS.XLua.__XLua_Gen_Delegate9 XLua.__XLua_Gen_Delegate9
CS.XLua.__XLua_Gen_Delegate9 = XLua.__XLua_Gen_Delegate9

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate9
function XLua.__XLua_Gen_Delegate9.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param out_data Game.Items.ItemData
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult, Game.Items.ItemData
function XLua.__XLua_Gen_Delegate9:BeginInvoke(arg1, out_data, callback, object) end
---@param out_data Game.Items.ItemData
---@param result System.IAsyncResult
---@return boolean, Game.Items.ItemData
function XLua.__XLua_Gen_Delegate9:EndInvoke(out_data, result) end
---@param arg1 System.Object
---@param out_data Game.Items.ItemData
---@return boolean, Game.Items.ItemData
function XLua.__XLua_Gen_Delegate9:Invoke(arg1, out_data) end

---@class XLua.__XLua_Gen_Delegate9 : System.MulticastDelegate
XLua.__XLua_Gen_Delegate9 = {}
---@alias CS.XLua.__XLua_Gen_Delegate9 XLua.__XLua_Gen_Delegate9
CS.XLua.__XLua_Gen_Delegate9 = XLua.__XLua_Gen_Delegate9

---@param objectInstance System.Object
---@param functionPtr System.IntPtr
---@return XLua.__XLua_Gen_Delegate9
function XLua.__XLua_Gen_Delegate9.New(objectInstance, functionPtr) end
---@param arg1 System.Object
---@param savedBuffs System.Object
---@param source System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.__XLua_Gen_Delegate9:BeginInvoke(arg1, savedBuffs, source, callback, object) end
---@param result System.IAsyncResult
function XLua.__XLua_Gen_Delegate9:EndInvoke(result) end
---@param arg1 System.Object
---@param savedBuffs System.Object
---@param source System.Object
function XLua.__XLua_Gen_Delegate9:Invoke(arg1, savedBuffs, source) end

---@class XLua.AdditionalPropertiesAttribute : System.Attribute
XLua.AdditionalPropertiesAttribute = {}
---@alias CS.XLua.AdditionalPropertiesAttribute XLua.AdditionalPropertiesAttribute
CS.XLua.AdditionalPropertiesAttribute = XLua.AdditionalPropertiesAttribute

---@return XLua.AdditionalPropertiesAttribute
function XLua.AdditionalPropertiesAttribute.New() end

---@class XLua.BlackListAttribute : System.Attribute
XLua.BlackListAttribute = {}
---@alias CS.XLua.BlackListAttribute XLua.BlackListAttribute
CS.XLua.BlackListAttribute = XLua.BlackListAttribute

---@return XLua.BlackListAttribute
function XLua.BlackListAttribute.New() end

---@class XLua.Cast.Any : System.Object
---@field Target System.Object
XLua.Cast.Any = {}
---@alias CS.XLua.Cast.Any XLua.Cast.Any
CS.XLua.Cast.Any = XLua.Cast.Any

---@param i T
---@return XLua.Cast.Any
function XLua.Cast.Any.New(i) end

---@class XLua.Cast.Byte : XLua.Cast.Any
XLua.Cast.Byte = {}
---@alias CS.XLua.Cast.Byte XLua.Cast.Byte
CS.XLua.Cast.Byte = XLua.Cast.Byte

---@param i number
---@return XLua.Cast.Byte
function XLua.Cast.Byte.New(i) end

---@class XLua.Cast.Char : XLua.Cast.Any
XLua.Cast.Char = {}
---@alias CS.XLua.Cast.Char XLua.Cast.Char
CS.XLua.Cast.Char = XLua.Cast.Char

---@param i System.Char
---@return XLua.Cast.Char
function XLua.Cast.Char.New(i) end

---@class XLua.Cast.Float : XLua.Cast.Any
XLua.Cast.Float = {}
---@alias CS.XLua.Cast.Float XLua.Cast.Float
CS.XLua.Cast.Float = XLua.Cast.Float

---@param i number
---@return XLua.Cast.Float
function XLua.Cast.Float.New(i) end

---@class XLua.Cast.Int16 : XLua.Cast.Any
XLua.Cast.Int16 = {}
---@alias CS.XLua.Cast.Int16 XLua.Cast.Int16
CS.XLua.Cast.Int16 = XLua.Cast.Int16

---@param i number
---@return XLua.Cast.Int16
function XLua.Cast.Int16.New(i) end

---@class XLua.Cast.Int32 : XLua.Cast.Any
XLua.Cast.Int32 = {}
---@alias CS.XLua.Cast.Int32 XLua.Cast.Int32
CS.XLua.Cast.Int32 = XLua.Cast.Int32

---@param i number
---@return XLua.Cast.Int32
function XLua.Cast.Int32.New(i) end

---@class XLua.Cast.Int64 : XLua.Cast.Any
XLua.Cast.Int64 = {}
---@alias CS.XLua.Cast.Int64 XLua.Cast.Int64
CS.XLua.Cast.Int64 = XLua.Cast.Int64

---@param i number
---@return XLua.Cast.Int64
function XLua.Cast.Int64.New(i) end

---@class XLua.Cast.SByte : XLua.Cast.Any
XLua.Cast.SByte = {}
---@alias CS.XLua.Cast.SByte XLua.Cast.SByte
CS.XLua.Cast.SByte = XLua.Cast.SByte

---@param i number
---@return XLua.Cast.SByte
function XLua.Cast.SByte.New(i) end

---@class XLua.Cast.UInt16 : XLua.Cast.Any
XLua.Cast.UInt16 = {}
---@alias CS.XLua.Cast.UInt16 XLua.Cast.UInt16
CS.XLua.Cast.UInt16 = XLua.Cast.UInt16

---@param i number
---@return XLua.Cast.UInt16
function XLua.Cast.UInt16.New(i) end

---@class XLua.Cast.UInt32 : XLua.Cast.Any
XLua.Cast.UInt32 = {}
---@alias CS.XLua.Cast.UInt32 XLua.Cast.UInt32
CS.XLua.Cast.UInt32 = XLua.Cast.UInt32

---@param i number
---@return XLua.Cast.UInt32
function XLua.Cast.UInt32.New(i) end

---@class XLua.Cast.UInt64 : XLua.Cast.Any
XLua.Cast.UInt64 = {}
---@alias CS.XLua.Cast.UInt64 XLua.Cast.UInt64
CS.XLua.Cast.UInt64 = XLua.Cast.UInt64

---@param i number
---@return XLua.Cast.UInt64
function XLua.Cast.UInt64.New(i) end

---@class XLua.CopyByValue : System.Object
XLua.CopyByValue = {}
---@alias CS.XLua.CopyByValue XLua.CopyByValue
CS.XLua.CopyByValue = XLua.CopyByValue

---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: number) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: System.Decimal) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: UnityEngine.Vector2) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: UnityEngine.Vector3) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: UnityEngine.Vector4) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: UnityEngine.Color) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: UnityEngine.Quaternion) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: UnityEngine.Ray) : boolean
---@overload fun(buff: System.IntPtr, offset: number, field: UnityEngine.Bounds) : boolean
---@param buff System.IntPtr
---@param offset number
---@param field UnityEngine.Ray2D
---@return boolean
function XLua.CopyByValue.Pack(buff, offset, field) end
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: number) : boolean, number
---@overload fun(buff: System.IntPtr, offset: number, out_field: System.Decimal) : boolean, System.Decimal
---@overload fun(translator: XLua.ObjectTranslator, L: System.IntPtr, idx: number, out_val: UnityEngine.Vector2) : UnityEngine.Vector2
---@overload fun(buff: System.IntPtr, offset: number, out_field: UnityEngine.Vector2) : boolean, UnityEngine.Vector2
---@overload fun(translator: XLua.ObjectTranslator, L: System.IntPtr, idx: number, out_val: UnityEngine.Vector3) : UnityEngine.Vector3
---@overload fun(buff: System.IntPtr, offset: number, out_field: UnityEngine.Vector3) : boolean, UnityEngine.Vector3
---@overload fun(translator: XLua.ObjectTranslator, L: System.IntPtr, idx: number, out_val: UnityEngine.Vector4) : UnityEngine.Vector4
---@overload fun(buff: System.IntPtr, offset: number, out_field: UnityEngine.Vector4) : boolean, UnityEngine.Vector4
---@overload fun(translator: XLua.ObjectTranslator, L: System.IntPtr, idx: number, out_val: UnityEngine.Color) : UnityEngine.Color
---@overload fun(buff: System.IntPtr, offset: number, out_field: UnityEngine.Color) : boolean, UnityEngine.Color
---@overload fun(translator: XLua.ObjectTranslator, L: System.IntPtr, idx: number, out_val: UnityEngine.Quaternion) : UnityEngine.Quaternion
---@overload fun(buff: System.IntPtr, offset: number, out_field: UnityEngine.Quaternion) : boolean, UnityEngine.Quaternion
---@overload fun(translator: XLua.ObjectTranslator, L: System.IntPtr, idx: number, out_val: UnityEngine.Ray) : UnityEngine.Ray
---@overload fun(buff: System.IntPtr, offset: number, out_field: UnityEngine.Ray) : boolean, UnityEngine.Ray
---@overload fun(translator: XLua.ObjectTranslator, L: System.IntPtr, idx: number, out_val: UnityEngine.Bounds) : UnityEngine.Bounds
---@overload fun(buff: System.IntPtr, offset: number, out_field: UnityEngine.Bounds) : boolean, UnityEngine.Bounds
---@overload fun(translator: XLua.ObjectTranslator, L: System.IntPtr, idx: number, out_val: UnityEngine.Ray2D) : UnityEngine.Ray2D
---@param buff System.IntPtr
---@param offset number
---@param out_field UnityEngine.Ray2D
---@return boolean, UnityEngine.Ray2D
function XLua.CopyByValue.UnPack(buff, offset, out_field) end
---@param type System.Type
---@return boolean
function XLua.CopyByValue.IsStruct(type) end

---@class XLua.CSharpCallLuaAttribute : System.Attribute
XLua.CSharpCallLuaAttribute = {}
---@alias CS.XLua.CSharpCallLuaAttribute XLua.CSharpCallLuaAttribute
CS.XLua.CSharpCallLuaAttribute = XLua.CSharpCallLuaAttribute

---@return XLua.CSharpCallLuaAttribute
function XLua.CSharpCallLuaAttribute.New() end

---@class XLua.CSObjectWrap.XLua_Gen_Initer_Register__ : System.Object
XLua.CSObjectWrap.XLua_Gen_Initer_Register__ = {}
---@alias CS.XLua.CSObjectWrap.XLua_Gen_Initer_Register__ XLua.CSObjectWrap.XLua_Gen_Initer_Register__
CS.XLua.CSObjectWrap.XLua_Gen_Initer_Register__ = XLua.CSObjectWrap.XLua_Gen_Initer_Register__

---@return XLua.CSObjectWrap.XLua_Gen_Initer_Register__
function XLua.CSObjectWrap.XLua_Gen_Initer_Register__.New() end

---@class XLua.DelegateBridge : XLua.DelegateBridgeBase
---@field Gen_Flag boolean
XLua.DelegateBridge = {}
---@alias CS.XLua.DelegateBridge XLua.DelegateBridge
CS.XLua.DelegateBridge = XLua.DelegateBridge

---@param reference number
---@param luaenv XLua.LuaEnv
---@return XLua.DelegateBridge
function XLua.DelegateBridge.New(reference, luaenv) end
---@param L System.IntPtr
---@param nArgs number
---@param nResults number
---@param errFunc number
function XLua.DelegateBridge:PCall(L, nArgs, nResults, errFunc) end
function XLua.DelegateBridge:InvokeSessionStart() end
---@param nRet number
function XLua.DelegateBridge:Invoke(nRet) end
function XLua.DelegateBridge:InvokeSessionEnd() end
---@param p0 System.Object
function XLua.DelegateBridge:__Gen_Delegate_Imp0(p0) end
---@param p0 System.Object
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp1(p0) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector2
---@param p2 boolean
function XLua.DelegateBridge:__Gen_Delegate_Imp2(p0, p1, p2) end
---@param p0 System.Object
---@param p1 boolean
function XLua.DelegateBridge:__Gen_Delegate_Imp3(p0, p1) end
---@param p0 System.Object
---@return boolean
function XLua.DelegateBridge:__Gen_Delegate_Imp4(p0) end
---@param p0 System.Object
---@return System.Collections.IEnumerator
function XLua.DelegateBridge:__Gen_Delegate_Imp5(p0) end
---@param p0 System.Object
---@return number
function XLua.DelegateBridge:__Gen_Delegate_Imp6(p0) end
---@param p0 System.Object
---@param p1 System.Object
---@return boolean
function XLua.DelegateBridge:__Gen_Delegate_Imp7(p0, p1) end
---@param p0 System.Object
---@param p1 number
---@return number
function XLua.DelegateBridge:__Gen_Delegate_Imp8(p0, p1) end
---@param p0 System.Object
---@param p1 System.Object
function XLua.DelegateBridge:__Gen_Delegate_Imp9(p0, p1) end
---@param p0 System.Object
---@param p1 System.Object
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp10(p0, p1) end
---@param p0 number
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp11(p0) end
---@param p0 System.Object
---@param p1 number
function XLua.DelegateBridge:__Gen_Delegate_Imp12(p0, p1) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 number
---@return System.Collections.IEnumerator
function XLua.DelegateBridge:__Gen_Delegate_Imp13(p0, p1, p2) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 number
function XLua.DelegateBridge:__Gen_Delegate_Imp14(p0, p1, p2) end
---@param p0 System.Object
---@param ref_p1 UnityEngine.Vector2
---@return UnityEngine.Vector2
function XLua.DelegateBridge:__Gen_Delegate_Imp15(p0, ref_p1) end
---@param p0 System.Object
---@param p1 number
function XLua.DelegateBridge:__Gen_Delegate_Imp16(p0, p1) end
---@param p0 System.Object
---@param p1 Game.Gameplay.StatType
---@param p2 number
---@return number
function XLua.DelegateBridge:__Gen_Delegate_Imp17(p0, p1, p2) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector2
---@param p2 number
---@param p3 number
function XLua.DelegateBridge:__Gen_Delegate_Imp18(p0, p1, p2, p3) end
---@param p0 System.Object
---@param p1 number
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.DelegateBridge:__Gen_Delegate_Imp19(p0, p1) end
---@param p0 System.Object
---@param p1 number
---@param p2 System.Object
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.DelegateBridge:__Gen_Delegate_Imp20(p0, p1, p2) end
---@param p0 System.Object
---@param p1 System.Object
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.DelegateBridge:__Gen_Delegate_Imp21(p0, p1) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 System.Object
---@return Game.Gameplay.BuffRuntimeInfo
function XLua.DelegateBridge:__Gen_Delegate_Imp22(p0, p1, p2) end
---@param p0 System.Object
---@param p1 number
---@return boolean
function XLua.DelegateBridge:__Gen_Delegate_Imp23(p0, p1) end
---@param p0 System.Object
---@param p1 Game.Gameplay.BuffTag
---@return number
function XLua.DelegateBridge:__Gen_Delegate_Imp24(p0, p1) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 System.Object
function XLua.DelegateBridge:__Gen_Delegate_Imp25(p0, p1, p2) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 System.Object
---@param p3 System.Object
function XLua.DelegateBridge:__Gen_Delegate_Imp26(p0, p1, p2, p3) end
---@param p0 System.Object
---@return number
function XLua.DelegateBridge:__Gen_Delegate_Imp27(p0) end
function XLua.DelegateBridge:__Gen_Delegate_Imp28() end
---@return System.Collections.Generic.IReadOnlyList
function XLua.DelegateBridge:__Gen_Delegate_Imp29() end
---@param p0 number
---@return Game.Gameplay.Save.SaveOperationResult
function XLua.DelegateBridge:__Gen_Delegate_Imp30(p0) end
---@param p0 number
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp31(p0) end
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp32() end
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp33() end
---@param p0 System.Object
---@return UnityEngine.Vector3
function XLua.DelegateBridge:__Gen_Delegate_Imp34(p0) end
---@param p0 System.Object
---@return Game.Gameplay.EnemyBase
function XLua.DelegateBridge:__Gen_Delegate_Imp35(p0) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector3
function XLua.DelegateBridge:__Gen_Delegate_Imp36(p0, p1) end
---@param p0 System.Object
---@param p1 Game.Gameplay.EnemyData
function XLua.DelegateBridge:__Gen_Delegate_Imp37(p0, p1) end
---@param p0 System.Object
---@param out_p1 UnityEngine.Vector2
---@return boolean, UnityEngine.Vector2
function XLua.DelegateBridge:__Gen_Delegate_Imp38(p0, out_p1) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 UnityEngine.AnimatorControllerParameterType
---@return boolean
function XLua.DelegateBridge:__Gen_Delegate_Imp39(p0, p1, p2) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 boolean
function XLua.DelegateBridge:__Gen_Delegate_Imp40(p0, p1, p2) end
---@param p0 System.Object
---@param p1 number
---@param p2 number
---@param p3 number
---@param p4 number
function XLua.DelegateBridge:__Gen_Delegate_Imp41(p0, p1, p2, p3, p4) end
---@param p0 System.Object
---@param p1 Game.Gameplay.WeaponData
function XLua.DelegateBridge:__Gen_Delegate_Imp42(p0, p1) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector2
function XLua.DelegateBridge:__Gen_Delegate_Imp43(p0, p1) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector2
---@return Game.Gameplay.PlayerBullet
function XLua.DelegateBridge:__Gen_Delegate_Imp44(p0, p1) end
---@param p0 System.Object
---@return Game.Items.ItemData
function XLua.DelegateBridge:__Gen_Delegate_Imp45(p0) end
---@param p0 System.Object
---@param out_p1 Game.Items.ItemData
---@return boolean, Game.Items.ItemData
function XLua.DelegateBridge:__Gen_Delegate_Imp46(p0, out_p1) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector3
---@return UnityEngine.GameObject
function XLua.DelegateBridge:__Gen_Delegate_Imp47(p0, p1) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector3
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp48(p0, p1) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector3
---@param p2 Game.Animation.DOTweenAnimType
---@return UnityEngine.GameObject
function XLua.DelegateBridge:__Gen_Delegate_Imp49(p0, p1, p2) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector3
---@param p2 Game.Animation.DOTweenAnimType
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp50(p0, p1, p2) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector3
---@param p2 System.Object
---@return UnityEngine.GameObject
function XLua.DelegateBridge:__Gen_Delegate_Imp51(p0, p1, p2) end
---@param p0 System.Object
---@param p1 UnityEngine.Vector3
---@param p2 System.Object
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp52(p0, p1, p2) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 UnityEngine.Vector3
---@param p3 Game.Animation.DOTweenAnimType
---@return UnityEngine.GameObject
function XLua.DelegateBridge:__Gen_Delegate_Imp53(p0, p1, p2, p3) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 UnityEngine.Vector3
---@param p3 Game.Animation.DOTweenAnimType
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp54(p0, p1, p2, p3) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 UnityEngine.Vector3
---@param p3 Game.Animation.DOTweenAnimType
---@param p4 number
---@return UnityEngine.GameObject
function XLua.DelegateBridge:__Gen_Delegate_Imp55(p0, p1, p2, p3, p4) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 UnityEngine.Vector3
---@param p3 Game.Animation.DOTweenAnimType
---@param p4 number
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp56(p0, p1, p2, p3, p4) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 UnityEngine.Vector3
---@param p3 System.Object
---@return UnityEngine.GameObject
function XLua.DelegateBridge:__Gen_Delegate_Imp57(p0, p1, p2, p3) end
---@param p0 System.Object
---@param p1 System.Object
---@param p2 UnityEngine.Vector3
---@param p3 System.Object
---@return System.Threading.Tasks.Task
function XLua.DelegateBridge:__Gen_Delegate_Imp58(p0, p1, p2, p3) end
---@param p0 Game.Animation.DOTweenAnimType
---@param p1 number
---@param p2 System.Object
---@param p3 System.Object
function XLua.DelegateBridge:__Gen_Delegate_Imp59(p0, p1, p2, p3) end
---@param p0 System.Object
---@param p1 number
---@param p2 System.Object
---@param p3 System.Object
function XLua.DelegateBridge:__Gen_Delegate_Imp60(p0, p1, p2, p3) end
---@param p0 System.Object
---@param p1 number
---@param out_p2 Game.Items.InventoryItemStack
---@return boolean, Game.Items.InventoryItemStack
function XLua.DelegateBridge:__Gen_Delegate_Imp61(p0, p1, out_p2) end
---@param p0 System.Object
---@param p1 number
---@param p2 number
---@param p3 System.Object
---@param p4 System.Object
---@return boolean
function XLua.DelegateBridge:__Gen_Delegate_Imp62(p0, p1, p2, p3, p4) end
---@param type System.Type
---@return System.Delegate
function XLua.DelegateBridge:GetDelegateByType(type) end
function XLua.DelegateBridge:Action() end

---@class XLua.DelegateBridgeBase : XLua.LuaBase
XLua.DelegateBridgeBase = {}
---@alias CS.XLua.DelegateBridgeBase XLua.DelegateBridgeBase
CS.XLua.DelegateBridgeBase = XLua.DelegateBridgeBase

---@param key System.Type
---@param out_value System.Delegate
---@return boolean, System.Delegate
function XLua.DelegateBridgeBase:TryGetDelegate(key, out_value) end
---@param key System.Type
---@param value System.Delegate
function XLua.DelegateBridgeBase:AddDelegate(key, value) end
---@param type System.Type
---@return System.Delegate
function XLua.DelegateBridgeBase:GetDelegateByType(type) end

---@class XLua.DoNotGenAttribute : System.Attribute
XLua.DoNotGenAttribute = {}
---@alias CS.XLua.DoNotGenAttribute XLua.DoNotGenAttribute
CS.XLua.DoNotGenAttribute = XLua.DoNotGenAttribute

---@return XLua.DoNotGenAttribute
function XLua.DoNotGenAttribute.New() end

---@class XLua.Editor.XLuaGenPathConfig : System.Object
---@field GenPath string
XLua.Editor.XLuaGenPathConfig = {}
---@alias CS.XLua.Editor.XLuaGenPathConfig XLua.Editor.XLuaGenPathConfig
CS.XLua.Editor.XLuaGenPathConfig = XLua.Editor.XLuaGenPathConfig


---@class XLua.GCOptimizeAttribute : System.Attribute
---@field Flag XLua.OptimizeFlag
XLua.GCOptimizeAttribute = {}
---@alias CS.XLua.GCOptimizeAttribute XLua.GCOptimizeAttribute
CS.XLua.GCOptimizeAttribute = XLua.GCOptimizeAttribute

---@param flag XLua.OptimizeFlag
---@return XLua.GCOptimizeAttribute
function XLua.GCOptimizeAttribute.New(flag) end

---@class XLua.GenFlag
---@field No XLua.GenFlag
XLua.GenFlag = {}
---@alias CS.XLua.GenFlag XLua.GenFlag
CS.XLua.GenFlag = XLua.GenFlag


---@class XLua.Hotfix : System.Object
XLua.Hotfix = {}
---@alias CS.XLua.Hotfix XLua.Hotfix
CS.XLua.Hotfix = XLua.Hotfix

---@overload fun()
---@param assemblyDir string
function XLua.Hotfix.HotfixInject(assemblyDir) end

---@class XLua.HotfixAttribute : System.Attribute
---@field Flag XLua.HotfixFlag
XLua.HotfixAttribute = {}
---@alias CS.XLua.HotfixAttribute XLua.HotfixAttribute
CS.XLua.HotfixAttribute = XLua.HotfixAttribute

---@param e XLua.HotfixFlag
---@return XLua.HotfixAttribute
function XLua.HotfixAttribute.New(e) end

---@class XLua.HotfixConfig : System.Object
XLua.HotfixConfig = {}
---@alias CS.XLua.HotfixConfig XLua.HotfixConfig
CS.XLua.HotfixConfig = XLua.HotfixConfig

---@param hotfixCfg System.Collections.Generic.Dictionary
---@param cfg_check_types System.Collections.Generic.IEnumerable
function XLua.HotfixConfig.GetConfig(hotfixCfg, cfg_check_types) end
---@return System.Collections.Generic.List
function XLua.HotfixConfig.GetHotfixAssembly() end
---@return System.Collections.Generic.List
function XLua.HotfixConfig.GetHotfixAssemblyPaths() end

---@class XLua.HotfixDelegateAttribute : System.Attribute
XLua.HotfixDelegateAttribute = {}
---@alias CS.XLua.HotfixDelegateAttribute XLua.HotfixDelegateAttribute
CS.XLua.HotfixDelegateAttribute = XLua.HotfixDelegateAttribute

---@return XLua.HotfixDelegateAttribute
function XLua.HotfixDelegateAttribute.New() end

---@class XLua.HotfixDelegateBridge : System.Object
XLua.HotfixDelegateBridge = {}
---@alias CS.XLua.HotfixDelegateBridge XLua.HotfixDelegateBridge
CS.XLua.HotfixDelegateBridge = XLua.HotfixDelegateBridge

---@param idx number
---@return boolean
function XLua.HotfixDelegateBridge.xlua_get_hotfix_flag(idx) end
---@param idx number
---@return XLua.DelegateBridge
function XLua.HotfixDelegateBridge.Get(idx) end
---@param idx number
---@param val XLua.DelegateBridge
function XLua.HotfixDelegateBridge.Set(idx, val) end

---@class XLua.HotfixFlag
---@field Stateless XLua.HotfixFlag
---@field ValueTypeBoxing XLua.HotfixFlag
---@field IgnoreProperty XLua.HotfixFlag
---@field IgnoreNotPublic XLua.HotfixFlag
---@field Inline XLua.HotfixFlag
---@field IntKey XLua.HotfixFlag
---@field AdaptByDelegate XLua.HotfixFlag
---@field IgnoreCompilerGenerated XLua.HotfixFlag
---@field NoBaseProxy XLua.HotfixFlag
XLua.HotfixFlag = {}
---@alias CS.XLua.HotfixFlag XLua.HotfixFlag
CS.XLua.HotfixFlag = XLua.HotfixFlag


---@class XLua.InternalGlobals : System.Object
XLua.InternalGlobals = {}
---@alias CS.XLua.InternalGlobals XLua.InternalGlobals
CS.XLua.InternalGlobals = XLua.InternalGlobals

---@return XLua.InternalGlobals
function XLua.InternalGlobals.New() end

---@class XLua.InternalGlobals.TryArrayGet : System.MulticastDelegate
XLua.InternalGlobals.TryArrayGet = {}
---@alias CS.XLua.InternalGlobals.TryArrayGet XLua.InternalGlobals.TryArrayGet
CS.XLua.InternalGlobals.TryArrayGet = XLua.InternalGlobals.TryArrayGet

---@param object System.Object
---@param method System.IntPtr
---@return XLua.InternalGlobals.TryArrayGet
function XLua.InternalGlobals.TryArrayGet.New(object, method) end
---@param type System.Type
---@param L System.IntPtr
---@param translator XLua.ObjectTranslator
---@param obj System.Object
---@param index number
---@return boolean
function XLua.InternalGlobals.TryArrayGet:Invoke(type, L, translator, obj, index) end
---@param type System.Type
---@param L System.IntPtr
---@param translator XLua.ObjectTranslator
---@param obj System.Object
---@param index number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.InternalGlobals.TryArrayGet:BeginInvoke(type, L, translator, obj, index, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.InternalGlobals.TryArrayGet:EndInvoke(result) end

---@class XLua.InternalGlobals.TryArraySet : System.MulticastDelegate
XLua.InternalGlobals.TryArraySet = {}
---@alias CS.XLua.InternalGlobals.TryArraySet XLua.InternalGlobals.TryArraySet
CS.XLua.InternalGlobals.TryArraySet = XLua.InternalGlobals.TryArraySet

---@param object System.Object
---@param method System.IntPtr
---@return XLua.InternalGlobals.TryArraySet
function XLua.InternalGlobals.TryArraySet.New(object, method) end
---@param type System.Type
---@param L System.IntPtr
---@param translator XLua.ObjectTranslator
---@param obj System.Object
---@param array_idx number
---@param obj_idx number
---@return boolean
function XLua.InternalGlobals.TryArraySet:Invoke(type, L, translator, obj, array_idx, obj_idx) end
---@param type System.Type
---@param L System.IntPtr
---@param translator XLua.ObjectTranslator
---@param obj System.Object
---@param array_idx number
---@param obj_idx number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.InternalGlobals.TryArraySet:BeginInvoke(type, L, translator, obj, array_idx, obj_idx, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.InternalGlobals.TryArraySet:EndInvoke(result) end

---@class XLua.LazyMemberTypes
---@field Method XLua.LazyMemberTypes
---@field FieldGet XLua.LazyMemberTypes
---@field FieldSet XLua.LazyMemberTypes
---@field PropertyGet XLua.LazyMemberTypes
---@field PropertySet XLua.LazyMemberTypes
---@field Event XLua.LazyMemberTypes
XLua.LazyMemberTypes = {}
---@alias CS.XLua.LazyMemberTypes XLua.LazyMemberTypes
CS.XLua.LazyMemberTypes = XLua.LazyMemberTypes


---@class XLua.LuaBase : System.Object
XLua.LuaBase = {}
---@alias CS.XLua.LuaBase XLua.LuaBase
CS.XLua.LuaBase = XLua.LuaBase

---@overload fun(self: XLua.LuaBase)
---@param disposeManagedResources boolean
function XLua.LuaBase:Dispose(disposeManagedResources) end
---@param o System.Object
---@return boolean
function XLua.LuaBase:Equals(o) end
---@return number
function XLua.LuaBase:GetHashCode() end

---@class XLua.LuaCallCSharpAttribute : System.Attribute
---@field Flag XLua.GenFlag
XLua.LuaCallCSharpAttribute = {}
---@alias CS.XLua.LuaCallCSharpAttribute XLua.LuaCallCSharpAttribute
CS.XLua.LuaCallCSharpAttribute = XLua.LuaCallCSharpAttribute

---@param flag XLua.GenFlag
---@return XLua.LuaCallCSharpAttribute
function XLua.LuaCallCSharpAttribute.New(flag) end

---@class XLua.LuaDLL.Lua : System.Object
XLua.LuaDLL.Lua = {}
---@alias CS.XLua.LuaDLL.Lua XLua.LuaDLL.Lua
CS.XLua.LuaDLL.Lua = XLua.LuaDLL.Lua

---@return XLua.LuaDLL.Lua
function XLua.LuaDLL.Lua.New() end
---@param L System.IntPtr
---@param index number
---@return System.IntPtr
function XLua.LuaDLL.Lua.lua_tothread(L, index) end
---@return number
function XLua.LuaDLL.Lua.xlua_get_lib_version() end
---@param L System.IntPtr
---@param what XLua.LuaGCOptions
---@param data number
---@return number
function XLua.LuaDLL.Lua.lua_gc(L, what, data) end
---@param L System.IntPtr
---@param funcindex number
---@param n number
---@return System.IntPtr
function XLua.LuaDLL.Lua.lua_getupvalue(L, funcindex, n) end
---@param L System.IntPtr
---@param funcindex number
---@param n number
---@return System.IntPtr
function XLua.LuaDLL.Lua.lua_setupvalue(L, funcindex, n) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.lua_pushthread(L) end
---@param L System.IntPtr
---@param stackPos number
---@return boolean
function XLua.LuaDLL.Lua.lua_isfunction(L, stackPos) end
---@param L System.IntPtr
---@param stackPos number
---@return boolean
function XLua.LuaDLL.Lua.lua_islightuserdata(L, stackPos) end
---@param L System.IntPtr
---@param stackPos number
---@return boolean
function XLua.LuaDLL.Lua.lua_istable(L, stackPos) end
---@param L System.IntPtr
---@param stackPos number
---@return boolean
function XLua.LuaDLL.Lua.lua_isthread(L, stackPos) end
---@param L System.IntPtr
---@param message string
---@return number
function XLua.LuaDLL.Lua.luaL_error(L, message) end
---@param L System.IntPtr
---@param stackPos number
---@return number
function XLua.LuaDLL.Lua.lua_setfenv(L, stackPos) end
---@return System.IntPtr
function XLua.LuaDLL.Lua.luaL_newstate() end
---@param L System.IntPtr
function XLua.LuaDLL.Lua.lua_close(L) end
---@param L System.IntPtr
function XLua.LuaDLL.Lua.luaopen_xlua(L) end
---@param L System.IntPtr
function XLua.LuaDLL.Lua.luaL_openlibs(L) end
---@param L System.IntPtr
---@param stackPos number
---@return number
function XLua.LuaDLL.Lua.xlua_objlen(L, stackPos) end
---@param L System.IntPtr
---@param narr number
---@param nrec number
function XLua.LuaDLL.Lua.lua_createtable(L, narr, nrec) end
---@param L System.IntPtr
function XLua.LuaDLL.Lua.lua_newtable(L) end
---@param L System.IntPtr
---@param name string
---@return number
function XLua.LuaDLL.Lua.xlua_getglobal(L, name) end
---@param L System.IntPtr
---@param name string
---@return number
function XLua.LuaDLL.Lua.xlua_setglobal(L, name) end
---@param L System.IntPtr
function XLua.LuaDLL.Lua.xlua_getloaders(L) end
---@param L System.IntPtr
---@param newTop number
function XLua.LuaDLL.Lua.lua_settop(L, newTop) end
---@param L System.IntPtr
---@param amount number
function XLua.LuaDLL.Lua.lua_pop(L, amount) end
---@param L System.IntPtr
---@param newTop number
function XLua.LuaDLL.Lua.lua_insert(L, newTop) end
---@param L System.IntPtr
---@param index number
function XLua.LuaDLL.Lua.lua_remove(L, index) end
---@param L System.IntPtr
---@param index number
---@return number
function XLua.LuaDLL.Lua.lua_rawget(L, index) end
---@param L System.IntPtr
---@param index number
function XLua.LuaDLL.Lua.lua_rawset(L, index) end
---@param L System.IntPtr
---@param objIndex number
---@return number
function XLua.LuaDLL.Lua.lua_setmetatable(L, objIndex) end
---@param L System.IntPtr
---@param index1 number
---@param index2 number
---@return number
function XLua.LuaDLL.Lua.lua_rawequal(L, index1, index2) end
---@param L System.IntPtr
---@param index number
function XLua.LuaDLL.Lua.lua_pushvalue(L, index) end
---@param L System.IntPtr
---@param fn System.IntPtr
---@param n number
function XLua.LuaDLL.Lua.lua_pushcclosure(L, fn, n) end
---@param L System.IntPtr
---@param index number
function XLua.LuaDLL.Lua.lua_replace(L, index) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.lua_gettop(L) end
---@param L System.IntPtr
---@param index number
---@return XLua.LuaTypes
function XLua.LuaDLL.Lua.lua_type(L, index) end
---@param L System.IntPtr
---@param index number
---@return boolean
function XLua.LuaDLL.Lua.lua_isnil(L, index) end
---@param L System.IntPtr
---@param index number
---@return boolean
function XLua.LuaDLL.Lua.lua_isnumber(L, index) end
---@param L System.IntPtr
---@param index number
---@return boolean
function XLua.LuaDLL.Lua.lua_isboolean(L, index) end
---@overload fun(L: System.IntPtr, registryIndex: number) : number
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.luaL_ref(L) end
---@param L System.IntPtr
---@param tableIndex number
---@param index number
function XLua.LuaDLL.Lua.xlua_rawgeti(L, tableIndex, index) end
---@param L System.IntPtr
---@param tableIndex number
---@param index number
function XLua.LuaDLL.Lua.xlua_rawseti(L, tableIndex, index) end
---@param L System.IntPtr
---@param reference number
function XLua.LuaDLL.Lua.lua_getref(L, reference) end
---@param L System.IntPtr
---@param error_func_ref number
---@param func_ref number
---@return number
function XLua.LuaDLL.Lua.pcall_prepare(L, error_func_ref, func_ref) end
---@param L System.IntPtr
---@param registryIndex number
---@param reference number
function XLua.LuaDLL.Lua.luaL_unref(L, registryIndex, reference) end
---@param L System.IntPtr
---@param reference number
function XLua.LuaDLL.Lua.lua_unref(L, reference) end
---@param L System.IntPtr
---@param index number
---@return boolean
function XLua.LuaDLL.Lua.lua_isstring(L, index) end
---@param L System.IntPtr
---@param index number
---@return boolean
function XLua.LuaDLL.Lua.lua_isinteger(L, index) end
---@param L System.IntPtr
function XLua.LuaDLL.Lua.lua_pushnil(L) end
---@param L System.IntPtr
---@param _function XLua.LuaDLL.lua_CSFunction
---@param n number
function XLua.LuaDLL.Lua.lua_pushstdcallcfunction(L, _function, n) end
---@param n number
---@return number
function XLua.LuaDLL.Lua.xlua_upvalueindex(n) end
---@param L System.IntPtr
---@param nArgs number
---@param nResults number
---@param errfunc number
---@return number
function XLua.LuaDLL.Lua.lua_pcall(L, nArgs, nResults, errfunc) end
---@param L System.IntPtr
---@param index number
---@return number
function XLua.LuaDLL.Lua.lua_tonumber(L, index) end
---@param L System.IntPtr
---@param index number
---@return number
function XLua.LuaDLL.Lua.xlua_tointeger(L, index) end
---@param L System.IntPtr
---@param index number
---@return number
function XLua.LuaDLL.Lua.xlua_touint(L, index) end
---@param L System.IntPtr
---@param index number
---@return boolean
function XLua.LuaDLL.Lua.lua_toboolean(L, index) end
---@param L System.IntPtr
---@param index number
---@return System.IntPtr
function XLua.LuaDLL.Lua.lua_topointer(L, index) end
---@param L System.IntPtr
---@param index number
---@param out_strLen System.IntPtr
---@return System.IntPtr, System.IntPtr
function XLua.LuaDLL.Lua.lua_tolstring(L, index, out_strLen) end
---@param L System.IntPtr
---@param index number
---@return string
function XLua.LuaDLL.Lua.lua_tostring(L, index) end
---@param L System.IntPtr
---@param panicf XLua.LuaDLL.lua_CSFunction
---@return System.IntPtr
function XLua.LuaDLL.Lua.lua_atpanic(L, panicf) end
---@param L System.IntPtr
---@param number number
function XLua.LuaDLL.Lua.lua_pushnumber(L, number) end
---@param L System.IntPtr
---@param value boolean
function XLua.LuaDLL.Lua.lua_pushboolean(L, value) end
---@param L System.IntPtr
---@param value number
function XLua.LuaDLL.Lua.xlua_pushinteger(L, value) end
---@param L System.IntPtr
---@param value number
function XLua.LuaDLL.Lua.xlua_pushuint(L, value) end
---@overload fun(L: System.IntPtr, str: string)
---@param L System.IntPtr
---@param str number[]
function XLua.LuaDLL.Lua.lua_pushstring(L, str) end
---@param L System.IntPtr
---@param str number[]
---@param size number
function XLua.LuaDLL.Lua.xlua_pushlstring(L, str, size) end
---@param L System.IntPtr
---@param str string
function XLua.LuaDLL.Lua.xlua_pushasciistring(L, str) end
---@param L System.IntPtr
---@param index number
---@return number[]
function XLua.LuaDLL.Lua.lua_tobytes(L, index) end
---@param L System.IntPtr
---@param meta string
---@return number
function XLua.LuaDLL.Lua.luaL_newmetatable(L, meta) end
---@param L System.IntPtr
---@param idx number
---@return number
function XLua.LuaDLL.Lua.xlua_pgettable(L, idx) end
---@param L System.IntPtr
---@param idx number
---@return number
function XLua.LuaDLL.Lua.xlua_psettable(L, idx) end
---@param L System.IntPtr
---@param meta string
function XLua.LuaDLL.Lua.luaL_getmetatable(L, meta) end
---@param L System.IntPtr
---@param buff number[]
---@param size number
---@param name string
---@return number
function XLua.LuaDLL.Lua.xluaL_loadbuffer(L, buff, size, name) end
---@param L System.IntPtr
---@param buff string
---@param name string
---@return number
function XLua.LuaDLL.Lua.luaL_loadbuffer(L, buff, name) end
---@param L System.IntPtr
---@param obj number
---@return number
function XLua.LuaDLL.Lua.xlua_tocsobj_safe(L, obj) end
---@param L System.IntPtr
---@param obj number
---@return number
function XLua.LuaDLL.Lua.xlua_tocsobj_fast(L, obj) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.lua_error(L) end
---@param L System.IntPtr
---@param extra number
---@return boolean
function XLua.LuaDLL.Lua.lua_checkstack(L, extra) end
---@param L System.IntPtr
---@param index number
---@return number
function XLua.LuaDLL.Lua.lua_next(L, index) end
---@param L System.IntPtr
---@param udata System.IntPtr
function XLua.LuaDLL.Lua.lua_pushlightuserdata(L, udata) end
---@return System.IntPtr
function XLua.LuaDLL.Lua.xlua_tag() end
---@param L System.IntPtr
---@param level number
function XLua.LuaDLL.Lua.luaL_where(L, level) end
---@param L System.IntPtr
---@param key number
---@param cache_ref number
---@return number
function XLua.LuaDLL.Lua.xlua_tryget_cachedud(L, key, cache_ref) end
---@param L System.IntPtr
---@param key number
---@param meta_ref number
---@param need_cache boolean
---@param cache_ref number
function XLua.LuaDLL.Lua.xlua_pushcsobj(L, key, meta_ref, need_cache, cache_ref) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.gen_obj_indexer(L) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.gen_obj_newindexer(L) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.gen_cls_indexer(L) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.gen_cls_newindexer(L) end
---@param L System.IntPtr
---@param Ref number
---@return number
function XLua.LuaDLL.Lua.load_error_func(L, Ref) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.luaopen_i64lib(L) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.luaopen_socket_core(L) end
---@param L System.IntPtr
---@param n number
function XLua.LuaDLL.Lua.lua_pushint64(L, n) end
---@param L System.IntPtr
---@param n number
function XLua.LuaDLL.Lua.lua_pushuint64(L, n) end
---@param L System.IntPtr
---@param idx number
---@return boolean
function XLua.LuaDLL.Lua.lua_isint64(L, idx) end
---@param L System.IntPtr
---@param idx number
---@return boolean
function XLua.LuaDLL.Lua.lua_isuint64(L, idx) end
---@param L System.IntPtr
---@param idx number
---@return number
function XLua.LuaDLL.Lua.lua_toint64(L, idx) end
---@param L System.IntPtr
---@param idx number
---@return number
function XLua.LuaDLL.Lua.lua_touint64(L, idx) end
---@param L System.IntPtr
---@param fn System.IntPtr
---@param n number
function XLua.LuaDLL.Lua.xlua_push_csharp_function(L, fn, n) end
---@param L System.IntPtr
---@param message string
---@return number
function XLua.LuaDLL.Lua.xlua_csharp_str_error(L, message) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.Lua.xlua_csharp_error(L) end
---@param buff System.IntPtr
---@param offset number
---@param field number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_int8_t(buff, offset, field) end
---@param buff System.IntPtr
---@param offset number
---@param out_field number
---@return boolean, number
function XLua.LuaDLL.Lua.xlua_unpack_int8_t(buff, offset, out_field) end
---@param buff System.IntPtr
---@param offset number
---@param field number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_int16_t(buff, offset, field) end
---@param buff System.IntPtr
---@param offset number
---@param out_field number
---@return boolean, number
function XLua.LuaDLL.Lua.xlua_unpack_int16_t(buff, offset, out_field) end
---@param buff System.IntPtr
---@param offset number
---@param field number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_int32_t(buff, offset, field) end
---@param buff System.IntPtr
---@param offset number
---@param out_field number
---@return boolean, number
function XLua.LuaDLL.Lua.xlua_unpack_int32_t(buff, offset, out_field) end
---@param buff System.IntPtr
---@param offset number
---@param field number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_int64_t(buff, offset, field) end
---@param buff System.IntPtr
---@param offset number
---@param out_field number
---@return boolean, number
function XLua.LuaDLL.Lua.xlua_unpack_int64_t(buff, offset, out_field) end
---@param buff System.IntPtr
---@param offset number
---@param field number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_float(buff, offset, field) end
---@param buff System.IntPtr
---@param offset number
---@param out_field number
---@return boolean, number
function XLua.LuaDLL.Lua.xlua_unpack_float(buff, offset, out_field) end
---@param buff System.IntPtr
---@param offset number
---@param field number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_double(buff, offset, field) end
---@param buff System.IntPtr
---@param offset number
---@param out_field number
---@return boolean, number
function XLua.LuaDLL.Lua.xlua_unpack_double(buff, offset, out_field) end
---@param L System.IntPtr
---@param size number
---@param meta_ref number
---@return System.IntPtr
function XLua.LuaDLL.Lua.xlua_pushstruct(L, size, meta_ref) end
---@param L System.IntPtr
---@param field_count number
---@param meta_ref number
function XLua.LuaDLL.Lua.xlua_pushcstable(L, field_count, meta_ref) end
---@param L System.IntPtr
---@param idx number
---@return System.IntPtr
function XLua.LuaDLL.Lua.lua_touserdata(L, idx) end
---@param L System.IntPtr
---@param idx number
---@return number
function XLua.LuaDLL.Lua.xlua_gettypeid(L, idx) end
---@return number
function XLua.LuaDLL.Lua.xlua_get_registry_index() end
---@param L System.IntPtr
---@param idx number
---@param path string
---@return number
function XLua.LuaDLL.Lua.xlua_pgettable_bypath(L, idx, path) end
---@param L System.IntPtr
---@param idx number
---@param path string
---@return number
function XLua.LuaDLL.Lua.xlua_psettable_bypath(L, idx, path) end
---@param buff System.IntPtr
---@param offset number
---@param f1 number
---@param f2 number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_float2(buff, offset, f1, f2) end
---@param buff System.IntPtr
---@param offset number
---@param out_f1 number
---@param out_f2 number
---@return boolean, number, number
function XLua.LuaDLL.Lua.xlua_unpack_float2(buff, offset, out_f1, out_f2) end
---@param buff System.IntPtr
---@param offset number
---@param f1 number
---@param f2 number
---@param f3 number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_float3(buff, offset, f1, f2, f3) end
---@param buff System.IntPtr
---@param offset number
---@param out_f1 number
---@param out_f2 number
---@param out_f3 number
---@return boolean, number, number, number
function XLua.LuaDLL.Lua.xlua_unpack_float3(buff, offset, out_f1, out_f2, out_f3) end
---@param buff System.IntPtr
---@param offset number
---@param f1 number
---@param f2 number
---@param f3 number
---@param f4 number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_float4(buff, offset, f1, f2, f3, f4) end
---@param buff System.IntPtr
---@param offset number
---@param out_f1 number
---@param out_f2 number
---@param out_f3 number
---@param out_f4 number
---@return boolean, number, number, number, number
function XLua.LuaDLL.Lua.xlua_unpack_float4(buff, offset, out_f1, out_f2, out_f3, out_f4) end
---@param buff System.IntPtr
---@param offset number
---@param f1 number
---@param f2 number
---@param f3 number
---@param f4 number
---@param f5 number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_float5(buff, offset, f1, f2, f3, f4, f5) end
---@param buff System.IntPtr
---@param offset number
---@param out_f1 number
---@param out_f2 number
---@param out_f3 number
---@param out_f4 number
---@param out_f5 number
---@return boolean, number, number, number, number, number
function XLua.LuaDLL.Lua.xlua_unpack_float5(buff, offset, out_f1, out_f2, out_f3, out_f4, out_f5) end
---@param buff System.IntPtr
---@param offset number
---@param f1 number
---@param f2 number
---@param f3 number
---@param f4 number
---@param f5 number
---@param f6 number
---@return boolean
function XLua.LuaDLL.Lua.xlua_pack_float6(buff, offset, f1, f2, f3, f4, f5, f6) end
---@param buff System.IntPtr
---@param offset number
---@param out_f1 number
---@param out_f2 number
---@param out_f3 number
---@param out_f4 number
---@param out_f5 number
---@param out_f6 number
---@return boolean, number, number, number, number, number, number
function XLua.LuaDLL.Lua.xlua_unpack_float6(buff, offset, out_f1, out_f2, out_f3, out_f4, out_f5, out_f6) end
---@param buff System.IntPtr
---@param offset number
---@param ref_dec System.Decimal
---@return boolean, System.Decimal
function XLua.LuaDLL.Lua.xlua_pack_decimal(buff, offset, ref_dec) end
---@param buff System.IntPtr
---@param offset number
---@param out_scale number
---@param out_sign number
---@param out_hi32 number
---@param out_lo64 number
---@return boolean, number, number, number, number
function XLua.LuaDLL.Lua.xlua_unpack_decimal(buff, offset, out_scale, out_sign, out_hi32, out_lo64) end
---@overload fun(L: System.IntPtr, index: number, str: string) : boolean
---@param L System.IntPtr
---@param index number
---@param str string
---@param str_len number
---@return boolean
function XLua.LuaDLL.Lua.xlua_is_eq_str(L, index, str, str_len) end
---@param L System.IntPtr
---@return System.IntPtr
function XLua.LuaDLL.Lua.xlua_gl(L) end

---@class XLua.LuaDLL.lua_CSFunction : System.MulticastDelegate
XLua.LuaDLL.lua_CSFunction = {}
---@alias CS.XLua.LuaDLL.lua_CSFunction XLua.LuaDLL.lua_CSFunction
CS.XLua.LuaDLL.lua_CSFunction = XLua.LuaDLL.lua_CSFunction

---@param object System.Object
---@param method System.IntPtr
---@return XLua.LuaDLL.lua_CSFunction
function XLua.LuaDLL.lua_CSFunction.New(object, method) end
---@param L System.IntPtr
---@return number
function XLua.LuaDLL.lua_CSFunction:Invoke(L) end
---@param L System.IntPtr
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.LuaDLL.lua_CSFunction:BeginInvoke(L, callback, object) end
---@param result System.IAsyncResult
---@return number
function XLua.LuaDLL.lua_CSFunction:EndInvoke(result) end

---@class XLua.LuaEnv : System.Object
---@field CSHARP_NAMESPACE string
---@field MAIN_SHREAD string
---@field Global XLua.LuaTable
---@field GcPause number
---@field GcStepmul number
---@field Memroy number
XLua.LuaEnv = {}
---@alias CS.XLua.LuaEnv XLua.LuaEnv
CS.XLua.LuaEnv = XLua.LuaEnv

---@return XLua.LuaEnv
function XLua.LuaEnv.New() end
---@param initer System.Action | function
function XLua.LuaEnv.AddIniter(initer) end
---@param chunk string
---@param chunkName string
---@param env XLua.LuaTable
---@return XLua.LuaFunction
function XLua.LuaEnv:LoadString(chunk, chunkName, env) end
---@overload fun(self: XLua.LuaEnv, chunk: number[], chunkName: string, env: XLua.LuaTable) : System.Object[]
---@param chunk string
---@param chunkName string
---@param env XLua.LuaTable
---@return System.Object[]
function XLua.LuaEnv:DoString(chunk, chunkName, env) end
---@param type System.Type
---@param alias string
function XLua.LuaEnv:Alias(type, alias) end
function XLua.LuaEnv:Tick() end
function XLua.LuaEnv:GC() end
---@return XLua.LuaTable
function XLua.LuaEnv:NewTable() end
---@overload fun(self: XLua.LuaEnv)
---@param dispose boolean
function XLua.LuaEnv:Dispose(dispose) end
---@param oldTop number
function XLua.LuaEnv:ThrowExceptionFromError(oldTop) end
---@param loader XLua.LuaEnv.CustomLoader
function XLua.LuaEnv:AddLoader(loader) end
---@param name string
---@param initer XLua.LuaDLL.lua_CSFunction
function XLua.LuaEnv:AddBuildin(name, initer) end
function XLua.LuaEnv:FullGc() end
function XLua.LuaEnv:StopGc() end
function XLua.LuaEnv:RestartGc() end
---@param data number
---@return boolean
function XLua.LuaEnv:GcStep(data) end

---@class XLua.LuaEnv.CustomLoader : System.MulticastDelegate
XLua.LuaEnv.CustomLoader = {}
---@alias CS.XLua.LuaEnv.CustomLoader XLua.LuaEnv.CustomLoader
CS.XLua.LuaEnv.CustomLoader = XLua.LuaEnv.CustomLoader

---@param object System.Object
---@param method System.IntPtr
---@return XLua.LuaEnv.CustomLoader
function XLua.LuaEnv.CustomLoader.New(object, method) end
---@param ref_filepath string
---@return number[], string
function XLua.LuaEnv.CustomLoader:Invoke(ref_filepath) end
---@param ref_filepath string
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult, string
function XLua.LuaEnv.CustomLoader:BeginInvoke(ref_filepath, callback, object) end
---@param ref_filepath string
---@param result System.IAsyncResult
---@return number[], string
function XLua.LuaEnv.CustomLoader:EndInvoke(ref_filepath, result) end

---@class XLua.LuaEnv.GCAction : System.ValueType
---@field Reference number
---@field IsDelegate boolean
XLua.LuaEnv.GCAction = {}
---@alias CS.XLua.LuaEnv.GCAction XLua.LuaEnv.GCAction
CS.XLua.LuaEnv.GCAction = XLua.LuaEnv.GCAction


---@class XLua.LuaException : System.Exception
XLua.LuaException = {}
---@alias CS.XLua.LuaException XLua.LuaException
CS.XLua.LuaException = XLua.LuaException

---@param message string
---@return XLua.LuaException
function XLua.LuaException.New(message) end

---@class XLua.LuaFunction : XLua.LuaBase
XLua.LuaFunction = {}
---@alias CS.XLua.LuaFunction XLua.LuaFunction
CS.XLua.LuaFunction = XLua.LuaFunction

---@param reference number
---@param luaenv XLua.LuaEnv
---@return XLua.LuaFunction
function XLua.LuaFunction.New(reference, luaenv) end
---@overload fun(self: XLua.LuaFunction, args: System.Object[], returnTypes: System.Type[]) : System.Object[]
---@param args System.Object[]
---@return System.Object[]
function XLua.LuaFunction:Call(args) end
---@param env XLua.LuaTable
function XLua.LuaFunction:SetEnv(env) end
---@return string
function XLua.LuaFunction:ToString() end

---@class XLua.LuaGCOptions
---@field LUA_GCSTOP XLua.LuaGCOptions
---@field LUA_GCRESTART XLua.LuaGCOptions
---@field LUA_GCCOLLECT XLua.LuaGCOptions
---@field LUA_GCCOUNT XLua.LuaGCOptions
---@field LUA_GCCOUNTB XLua.LuaGCOptions
---@field LUA_GCSTEP XLua.LuaGCOptions
---@field LUA_GCSETPAUSE XLua.LuaGCOptions
---@field LUA_GCSETSTEPMUL XLua.LuaGCOptions
XLua.LuaGCOptions = {}
---@alias CS.XLua.LuaGCOptions XLua.LuaGCOptions
CS.XLua.LuaGCOptions = XLua.LuaGCOptions


---@class XLua.LuaIndexes : System.Object
---@field LUA_REGISTRYINDEX number
XLua.LuaIndexes = {}
---@alias CS.XLua.LuaIndexes XLua.LuaIndexes
CS.XLua.LuaIndexes = XLua.LuaIndexes

---@return XLua.LuaIndexes
function XLua.LuaIndexes.New() end

---@class XLua.LuaTable : XLua.LuaBase
---@field Length number
XLua.LuaTable = {}
---@alias CS.XLua.LuaTable XLua.LuaTable
CS.XLua.LuaTable = XLua.LuaTable

---@param reference number
---@param luaenv XLua.LuaEnv
---@return XLua.LuaTable
function XLua.LuaTable.New(reference, luaenv) end
---@param metaTable XLua.LuaTable
function XLua.LuaTable:SetMetaTable(metaTable) end
---@return string
function XLua.LuaTable:ToString() end

---@class XLua.LuaThreadStatus
---@field LUA_RESUME_ERROR XLua.LuaThreadStatus
---@field LUA_OK XLua.LuaThreadStatus
---@field LUA_YIELD XLua.LuaThreadStatus
---@field LUA_ERRRUN XLua.LuaThreadStatus
---@field LUA_ERRSYNTAX XLua.LuaThreadStatus
---@field LUA_ERRMEM XLua.LuaThreadStatus
---@field LUA_ERRERR XLua.LuaThreadStatus
XLua.LuaThreadStatus = {}
---@alias CS.XLua.LuaThreadStatus XLua.LuaThreadStatus
CS.XLua.LuaThreadStatus = XLua.LuaThreadStatus


---@class XLua.LuaTypes
---@field LUA_TNONE XLua.LuaTypes
---@field LUA_TNIL XLua.LuaTypes
---@field LUA_TNUMBER XLua.LuaTypes
---@field LUA_TSTRING XLua.LuaTypes
---@field LUA_TBOOLEAN XLua.LuaTypes
---@field LUA_TTABLE XLua.LuaTypes
---@field LUA_TFUNCTION XLua.LuaTypes
---@field LUA_TUSERDATA XLua.LuaTypes
---@field LUA_TTHREAD XLua.LuaTypes
---@field LUA_TLIGHTUSERDATA XLua.LuaTypes
XLua.LuaTypes = {}
---@alias CS.XLua.LuaTypes XLua.LuaTypes
CS.XLua.LuaTypes = XLua.LuaTypes


---@class XLua.MethodWrap : System.Object
XLua.MethodWrap = {}
---@alias CS.XLua.MethodWrap XLua.MethodWrap
CS.XLua.MethodWrap = XLua.MethodWrap

---@param methodName string
---@param overloads System.Collections.Generic.List
---@param forceCheck boolean
---@return XLua.MethodWrap
function XLua.MethodWrap.New(methodName, overloads, forceCheck) end
---@param L System.IntPtr
---@return number
function XLua.MethodWrap:Call(L) end

---@class XLua.MethodWrapsCache : System.Object
XLua.MethodWrapsCache = {}
---@alias CS.XLua.MethodWrapsCache XLua.MethodWrapsCache
CS.XLua.MethodWrapsCache = XLua.MethodWrapsCache

---@param translator XLua.ObjectTranslator
---@param objCheckers XLua.ObjectCheckers
---@param objCasters XLua.ObjectCasters
---@return XLua.MethodWrapsCache
function XLua.MethodWrapsCache.New(translator, objCheckers, objCasters) end
---@param type System.Type
---@return XLua.LuaDLL.lua_CSFunction
function XLua.MethodWrapsCache:GetConstructorWrap(type) end
---@param type System.Type
---@param methodName string
---@return XLua.LuaDLL.lua_CSFunction
function XLua.MethodWrapsCache:GetMethodWrap(type, methodName) end
---@param type System.Type
---@param methodName string
---@return XLua.LuaDLL.lua_CSFunction
function XLua.MethodWrapsCache:GetMethodWrapInCache(type, methodName) end
---@param type System.Type
---@return XLua.LuaDLL.lua_CSFunction
function XLua.MethodWrapsCache:GetDelegateWrap(type) end
---@param type System.Type
---@param eventName string
---@return XLua.LuaDLL.lua_CSFunction
function XLua.MethodWrapsCache:GetEventWrap(type, eventName) end
---@param type System.Type
---@param methodName string
---@param methodBases System.Collections.Generic.IEnumerable
---@param forceCheck boolean
---@return XLua.MethodWrap
function XLua.MethodWrapsCache:_GenMethodWrap(type, methodName, methodBases, forceCheck) end

---@class XLua.MonoPInvokeCallbackAttribute : System.Attribute
XLua.MonoPInvokeCallbackAttribute = {}
---@alias CS.XLua.MonoPInvokeCallbackAttribute XLua.MonoPInvokeCallbackAttribute
CS.XLua.MonoPInvokeCallbackAttribute = XLua.MonoPInvokeCallbackAttribute

---@param t System.Type
---@return XLua.MonoPInvokeCallbackAttribute
function XLua.MonoPInvokeCallbackAttribute.New(t) end

---@class XLua.MyCustomBuildProcessor : System.Object
---@field callbackOrder number
XLua.MyCustomBuildProcessor = {}
---@alias CS.XLua.MyCustomBuildProcessor XLua.MyCustomBuildProcessor
CS.XLua.MyCustomBuildProcessor = XLua.MyCustomBuildProcessor

---@return XLua.MyCustomBuildProcessor
function XLua.MyCustomBuildProcessor.New() end
---@param report UnityEditor.Build.Reporting.BuildReport
function XLua.MyCustomBuildProcessor:OnPostBuildPlayerScriptDLLs(report) end

---@class XLua.ObjectCast : System.MulticastDelegate
XLua.ObjectCast = {}
---@alias CS.XLua.ObjectCast XLua.ObjectCast
CS.XLua.ObjectCast = XLua.ObjectCast

---@param object System.Object
---@param method System.IntPtr
---@return XLua.ObjectCast
function XLua.ObjectCast.New(object, method) end
---@param L System.IntPtr
---@param idx number
---@param target System.Object
---@return System.Object
function XLua.ObjectCast:Invoke(L, idx, target) end
---@param L System.IntPtr
---@param idx number
---@param target System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.ObjectCast:BeginInvoke(L, idx, target, callback, object) end
---@param result System.IAsyncResult
---@return System.Object
function XLua.ObjectCast:EndInvoke(result) end

---@class XLua.ObjectCasters : System.Object
XLua.ObjectCasters = {}
---@alias CS.XLua.ObjectCasters XLua.ObjectCasters
CS.XLua.ObjectCasters = XLua.ObjectCasters

---@param translator XLua.ObjectTranslator
---@return XLua.ObjectCasters
function XLua.ObjectCasters.New(translator) end
---@param type System.Type
---@param oc XLua.ObjectCast
function XLua.ObjectCasters:AddCaster(type, oc) end
---@param type System.Type
---@return XLua.ObjectCast
function XLua.ObjectCasters:GetCaster(type) end

---@class XLua.ObjectCheck : System.MulticastDelegate
XLua.ObjectCheck = {}
---@alias CS.XLua.ObjectCheck XLua.ObjectCheck
CS.XLua.ObjectCheck = XLua.ObjectCheck

---@param object System.Object
---@param method System.IntPtr
---@return XLua.ObjectCheck
function XLua.ObjectCheck.New(object, method) end
---@param L System.IntPtr
---@param idx number
---@return boolean
function XLua.ObjectCheck:Invoke(L, idx) end
---@param L System.IntPtr
---@param idx number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.ObjectCheck:BeginInvoke(L, idx, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.ObjectCheck:EndInvoke(result) end

---@class XLua.ObjectCheckers : System.Object
XLua.ObjectCheckers = {}
---@alias CS.XLua.ObjectCheckers XLua.ObjectCheckers
CS.XLua.ObjectCheckers = XLua.ObjectCheckers

---@param translator XLua.ObjectTranslator
---@return XLua.ObjectCheckers
function XLua.ObjectCheckers.New(translator) end
---@param oc XLua.ObjectCheck
---@return XLua.ObjectCheck
function XLua.ObjectCheckers:genNullableChecker(oc) end
---@param type System.Type
---@param oc XLua.ObjectCheck
function XLua.ObjectCheckers:AddChecker(type, oc) end
---@param type System.Type
---@return XLua.ObjectCheck
function XLua.ObjectCheckers:GetChecker(type) end

---@class XLua.ObjectPool : System.Object
---@field Item System.Object
XLua.ObjectPool = {}
---@alias CS.XLua.ObjectPool XLua.ObjectPool
CS.XLua.ObjectPool = XLua.ObjectPool

---@return XLua.ObjectPool
function XLua.ObjectPool.New() end
function XLua.ObjectPool:Clear() end
---@param obj System.Object
---@return number
function XLua.ObjectPool:Add(obj) end
---@param index number
---@param out_obj System.Object
---@return boolean, System.Object
function XLua.ObjectPool:TryGetValue(index, out_obj) end
---@param index number
---@return System.Object
function XLua.ObjectPool:Get(index) end
---@param index number
---@return System.Object
function XLua.ObjectPool:Remove(index) end
---@param index number
---@param o System.Object
---@return System.Object
function XLua.ObjectPool:Replace(index, o) end
---@param check_pos number
---@param max_check number
---@param checker System.Func
---@param reverse_map System.Collections.Generic.Dictionary
---@return number
function XLua.ObjectPool:Check(check_pos, max_check, checker, reverse_map) end

---@class XLua.ObjectPool.Slot : System.ValueType
---@field next number
---@field obj System.Object
XLua.ObjectPool.Slot = {}
---@alias CS.XLua.ObjectPool.Slot XLua.ObjectPool.Slot
CS.XLua.ObjectPool.Slot = XLua.ObjectPool.Slot

---@param next number
---@param obj System.Object
---@return XLua.ObjectPool.Slot
function XLua.ObjectPool.Slot.New(next, obj) end

---@class XLua.ObjectTranslator : System.Object
---@field cacheRef number
XLua.ObjectTranslator = {}
---@alias CS.XLua.ObjectTranslator XLua.ObjectTranslator
CS.XLua.ObjectTranslator = XLua.ObjectTranslator

---@param luaenv XLua.LuaEnv
---@param L System.IntPtr
---@return XLua.ObjectTranslator
function XLua.ObjectTranslator.New(luaenv, L) end
---@param L System.IntPtr
---@param val UnityEngine.Vector2
function XLua.ObjectTranslator:PushUnityEngineVector2(L, val) end
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, index: number, out_val: UnityEngine.Vector2) : UnityEngine.Vector2
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, index: number, out_val: UnityEngine.Vector3) : UnityEngine.Vector3
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, index: number, out_val: UnityEngine.Vector4) : UnityEngine.Vector4
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, index: number, out_val: UnityEngine.Color) : UnityEngine.Color
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, index: number, out_val: UnityEngine.Quaternion) : UnityEngine.Quaternion
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, index: number, out_val: UnityEngine.Ray) : UnityEngine.Ray
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, index: number, out_val: UnityEngine.Bounds) : UnityEngine.Bounds
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, index: number, out_val: UnityEngine.Ray2D) : UnityEngine.Ray2D
---@param L System.IntPtr
---@param index number
---@param out_val System.Decimal
---@return System.Decimal
function XLua.ObjectTranslator:Get(L, index, out_val) end
---@param L System.IntPtr
---@param index number
---@param val UnityEngine.Vector2
function XLua.ObjectTranslator:UpdateUnityEngineVector2(L, index, val) end
---@param L System.IntPtr
---@param val UnityEngine.Vector3
function XLua.ObjectTranslator:PushUnityEngineVector3(L, val) end
---@param L System.IntPtr
---@param index number
---@param val UnityEngine.Vector3
function XLua.ObjectTranslator:UpdateUnityEngineVector3(L, index, val) end
---@param L System.IntPtr
---@param val UnityEngine.Vector4
function XLua.ObjectTranslator:PushUnityEngineVector4(L, val) end
---@param L System.IntPtr
---@param index number
---@param val UnityEngine.Vector4
function XLua.ObjectTranslator:UpdateUnityEngineVector4(L, index, val) end
---@param L System.IntPtr
---@param val UnityEngine.Color
function XLua.ObjectTranslator:PushUnityEngineColor(L, val) end
---@param L System.IntPtr
---@param index number
---@param val UnityEngine.Color
function XLua.ObjectTranslator:UpdateUnityEngineColor(L, index, val) end
---@param L System.IntPtr
---@param val UnityEngine.Quaternion
function XLua.ObjectTranslator:PushUnityEngineQuaternion(L, val) end
---@param L System.IntPtr
---@param index number
---@param val UnityEngine.Quaternion
function XLua.ObjectTranslator:UpdateUnityEngineQuaternion(L, index, val) end
---@param L System.IntPtr
---@param val UnityEngine.Ray
function XLua.ObjectTranslator:PushUnityEngineRay(L, val) end
---@param L System.IntPtr
---@param index number
---@param val UnityEngine.Ray
function XLua.ObjectTranslator:UpdateUnityEngineRay(L, index, val) end
---@param L System.IntPtr
---@param val UnityEngine.Bounds
function XLua.ObjectTranslator:PushUnityEngineBounds(L, val) end
---@param L System.IntPtr
---@param index number
---@param val UnityEngine.Bounds
function XLua.ObjectTranslator:UpdateUnityEngineBounds(L, index, val) end
---@param L System.IntPtr
---@param val UnityEngine.Ray2D
function XLua.ObjectTranslator:PushUnityEngineRay2D(L, val) end
---@param L System.IntPtr
---@param index number
---@param val UnityEngine.Ray2D
function XLua.ObjectTranslator:UpdateUnityEngineRay2D(L, index, val) end
---@param type System.Type
---@param loader System.Action | function
function XLua.ObjectTranslator:DelayWrapLoader(type, loader) end
---@param type System.Type
---@param creator System.Func
function XLua.ObjectTranslator:AddInterfaceBridgeCreator(type, creator) end
---@param L System.IntPtr
---@param type System.Type
---@return boolean
function XLua.ObjectTranslator:TryDelayWrapLoader(L, type) end
---@param type System.Type
---@param alias string
function XLua.ObjectTranslator:Alias(type, alias) end
---@param L System.IntPtr
---@param delegateType System.Type
---@param idx number
---@return System.Object
function XLua.ObjectTranslator:CreateDelegateBridge(L, delegateType, idx) end
---@return boolean
function XLua.ObjectTranslator:AllDelegateBridgeReleased() end
---@param L System.IntPtr
---@param reference number
---@param is_delegate boolean
function XLua.ObjectTranslator:ReleaseLuaBase(L, reference, is_delegate) end
---@param L System.IntPtr
---@param interfaceType System.Type
---@param idx number
---@return System.Object
function XLua.ObjectTranslator:CreateInterfaceBridge(L, interfaceType, idx) end
---@param L System.IntPtr
function XLua.ObjectTranslator:CreateArrayMetatable(L) end
---@param L System.IntPtr
function XLua.ObjectTranslator:CreateDelegateMetatable(L) end
---@param L System.IntPtr
function XLua.ObjectTranslator:OpenLib(L) end
---@param L System.IntPtr
---@param idx number
---@return System.Type
function XLua.ObjectTranslator:GetTypeOf(L, idx) end
---@param L System.IntPtr
---@param index number
---@param type System.Type
---@return boolean
function XLua.ObjectTranslator:Assignable(L, index, type) end
---@param L System.IntPtr
---@param index number
---@param type System.Type
---@return System.Object
function XLua.ObjectTranslator:GetObject(L, index, type) end
---@param L System.IntPtr
---@param index number
---@param type System.Type
---@return System.Array
function XLua.ObjectTranslator:GetParams(L, index, type) end
---@param L System.IntPtr
---@param ary System.Array
function XLua.ObjectTranslator:PushParams(L, ary) end
---@param L System.IntPtr
---@param type System.Type
---@return number
function XLua.ObjectTranslator:GetTypeId(L, type) end
---@param L System.IntPtr
---@param type System.Type
function XLua.ObjectTranslator:PrivateAccessible(L, type) end
---@param L System.IntPtr
---@param o System.Object
function XLua.ObjectTranslator:PushAny(L, o) end
---@param L System.IntPtr
---@param type System.Type
---@param idx number
---@return number
function XLua.ObjectTranslator:TranslateToEnumToTop(L, type, idx) end
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, o: XLua.LuaDLL.lua_CSFunction)
---@overload fun(self: XLua.ObjectTranslator, L: System.IntPtr, o: XLua.LuaBase)
---@param L System.IntPtr
---@param o System.Object
function XLua.ObjectTranslator:Push(L, o) end
---@param L System.IntPtr
---@param o System.Object
---@param type_id number
function XLua.ObjectTranslator:PushObject(L, o, type_id) end
---@param L System.IntPtr
---@param index number
---@param obj System.Object
function XLua.ObjectTranslator:Update(L, index, obj) end
---@param type System.Type
---@return boolean
function XLua.ObjectTranslator:HasCustomOp(type) end
---@param L System.IntPtr
---@param val System.Decimal
function XLua.ObjectTranslator:PushDecimal(L, val) end
---@param L System.IntPtr
---@param index number
---@return boolean
function XLua.ObjectTranslator:IsDecimal(L, index) end
---@param L System.IntPtr
---@param index number
---@return System.Decimal
function XLua.ObjectTranslator:GetDecimal(L, index) end

---@class XLua.ObjectTranslator.CheckFunc : System.MulticastDelegate
XLua.ObjectTranslator.CheckFunc = {}
---@alias CS.XLua.ObjectTranslator.CheckFunc XLua.ObjectTranslator.CheckFunc
CS.XLua.ObjectTranslator.CheckFunc = XLua.ObjectTranslator.CheckFunc

---@param object System.Object
---@param method System.IntPtr
---@return XLua.ObjectTranslator.CheckFunc
function XLua.ObjectTranslator.CheckFunc.New(object, method) end
---@param L System.IntPtr
---@param idx number
---@return boolean
function XLua.ObjectTranslator.CheckFunc:Invoke(L, idx) end
---@param L System.IntPtr
---@param idx number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.ObjectTranslator.CheckFunc:BeginInvoke(L, idx, callback, object) end
---@param result System.IAsyncResult
---@return boolean
function XLua.ObjectTranslator.CheckFunc:EndInvoke(result) end

---@class XLua.ObjectTranslator.GetCSObject : System.MulticastDelegate
XLua.ObjectTranslator.GetCSObject = {}
---@alias CS.XLua.ObjectTranslator.GetCSObject XLua.ObjectTranslator.GetCSObject
CS.XLua.ObjectTranslator.GetCSObject = XLua.ObjectTranslator.GetCSObject

---@param object System.Object
---@param method System.IntPtr
---@return XLua.ObjectTranslator.GetCSObject
function XLua.ObjectTranslator.GetCSObject.New(object, method) end
---@param L System.IntPtr
---@param idx number
---@return System.Object
function XLua.ObjectTranslator.GetCSObject:Invoke(L, idx) end
---@param L System.IntPtr
---@param idx number
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.ObjectTranslator.GetCSObject:BeginInvoke(L, idx, callback, object) end
---@param result System.IAsyncResult
---@return System.Object
function XLua.ObjectTranslator.GetCSObject:EndInvoke(result) end

---@class XLua.ObjectTranslator.GetFunc : System.MulticastDelegate
XLua.ObjectTranslator.GetFunc = {}
---@alias CS.XLua.ObjectTranslator.GetFunc XLua.ObjectTranslator.GetFunc
CS.XLua.ObjectTranslator.GetFunc = XLua.ObjectTranslator.GetFunc

---@param object System.Object
---@param method System.IntPtr
---@return XLua.ObjectTranslator.GetFunc
function XLua.ObjectTranslator.GetFunc.New(object, method) end
---@param L System.IntPtr
---@param idx number
---@param out_val T
---@return T
function XLua.ObjectTranslator.GetFunc:Invoke(L, idx, out_val) end
---@param L System.IntPtr
---@param idx number
---@param out_val T
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult, T
function XLua.ObjectTranslator.GetFunc:BeginInvoke(L, idx, out_val, callback, object) end
---@param out_val T
---@param result System.IAsyncResult
---@return T
function XLua.ObjectTranslator.GetFunc:EndInvoke(out_val, result) end

---@class XLua.ObjectTranslator.IniterAdderUnityEngineVector2 : System.Object
XLua.ObjectTranslator.IniterAdderUnityEngineVector2 = {}
---@alias CS.XLua.ObjectTranslator.IniterAdderUnityEngineVector2 XLua.ObjectTranslator.IniterAdderUnityEngineVector2
CS.XLua.ObjectTranslator.IniterAdderUnityEngineVector2 = XLua.ObjectTranslator.IniterAdderUnityEngineVector2

---@return XLua.ObjectTranslator.IniterAdderUnityEngineVector2
function XLua.ObjectTranslator.IniterAdderUnityEngineVector2.New() end

---@class XLua.ObjectTranslator.LOGLEVEL
---@field NO XLua.ObjectTranslator.LOGLEVEL
---@field INFO XLua.ObjectTranslator.LOGLEVEL
---@field WARN XLua.ObjectTranslator.LOGLEVEL
---@field ERROR XLua.ObjectTranslator.LOGLEVEL
XLua.ObjectTranslator.LOGLEVEL = {}
---@alias CS.XLua.ObjectTranslator.LOGLEVEL XLua.ObjectTranslator.LOGLEVEL
CS.XLua.ObjectTranslator.LOGLEVEL = XLua.ObjectTranslator.LOGLEVEL


---@class XLua.ObjectTranslator.PushCSObject : System.MulticastDelegate
XLua.ObjectTranslator.PushCSObject = {}
---@alias CS.XLua.ObjectTranslator.PushCSObject XLua.ObjectTranslator.PushCSObject
CS.XLua.ObjectTranslator.PushCSObject = XLua.ObjectTranslator.PushCSObject

---@param object System.Object
---@param method System.IntPtr
---@return XLua.ObjectTranslator.PushCSObject
function XLua.ObjectTranslator.PushCSObject.New(object, method) end
---@param L System.IntPtr
---@param obj System.Object
function XLua.ObjectTranslator.PushCSObject:Invoke(L, obj) end
---@param L System.IntPtr
---@param obj System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.ObjectTranslator.PushCSObject:BeginInvoke(L, obj, callback, object) end
---@param result System.IAsyncResult
function XLua.ObjectTranslator.PushCSObject:EndInvoke(result) end

---@class XLua.ObjectTranslator.UpdateCSObject : System.MulticastDelegate
XLua.ObjectTranslator.UpdateCSObject = {}
---@alias CS.XLua.ObjectTranslator.UpdateCSObject XLua.ObjectTranslator.UpdateCSObject
CS.XLua.ObjectTranslator.UpdateCSObject = XLua.ObjectTranslator.UpdateCSObject

---@param object System.Object
---@param method System.IntPtr
---@return XLua.ObjectTranslator.UpdateCSObject
function XLua.ObjectTranslator.UpdateCSObject.New(object, method) end
---@param L System.IntPtr
---@param idx number
---@param obj System.Object
function XLua.ObjectTranslator.UpdateCSObject:Invoke(L, idx, obj) end
---@param L System.IntPtr
---@param idx number
---@param obj System.Object
---@param callback System.AsyncCallback
---@param object System.Object
---@return System.IAsyncResult
function XLua.ObjectTranslator.UpdateCSObject:BeginInvoke(L, idx, obj, callback, object) end
---@param result System.IAsyncResult
function XLua.ObjectTranslator.UpdateCSObject:EndInvoke(result) end

---@class XLua.ObjectTranslatorPool : System.Object
---@field Instance XLua.ObjectTranslatorPool
XLua.ObjectTranslatorPool = {}
---@alias CS.XLua.ObjectTranslatorPool XLua.ObjectTranslatorPool
CS.XLua.ObjectTranslatorPool = XLua.ObjectTranslatorPool

---@return XLua.ObjectTranslatorPool
function XLua.ObjectTranslatorPool.New() end
---@param L System.IntPtr
---@return XLua.ObjectTranslator
function XLua.ObjectTranslatorPool.FindTranslator(L) end
---@param L System.IntPtr
---@param translator XLua.ObjectTranslator
function XLua.ObjectTranslatorPool:Add(L, translator) end
---@param L System.IntPtr
---@return XLua.ObjectTranslator
function XLua.ObjectTranslatorPool:Find(L) end
---@param L System.IntPtr
function XLua.ObjectTranslatorPool:Remove(L) end

---@class XLua.OptimizeFlag
---@field Default XLua.OptimizeFlag
---@field PackAsTable XLua.OptimizeFlag
XLua.OptimizeFlag = {}
---@alias CS.XLua.OptimizeFlag XLua.OptimizeFlag
CS.XLua.OptimizeFlag = XLua.OptimizeFlag


---@class XLua.OverloadMethodWrap : System.Object
---@field HasDefalutValue boolean
XLua.OverloadMethodWrap = {}
---@alias CS.XLua.OverloadMethodWrap XLua.OverloadMethodWrap
CS.XLua.OverloadMethodWrap = XLua.OverloadMethodWrap

---@param translator XLua.ObjectTranslator
---@param targetType System.Type
---@param method System.Reflection.MethodBase
---@return XLua.OverloadMethodWrap
function XLua.OverloadMethodWrap.New(translator, targetType, method) end
---@param objCheckers XLua.ObjectCheckers
---@param objCasters XLua.ObjectCasters
function XLua.OverloadMethodWrap:Init(objCheckers, objCasters) end
---@param L System.IntPtr
---@return boolean
function XLua.OverloadMethodWrap:Check(L) end
---@param L System.IntPtr
---@return number
function XLua.OverloadMethodWrap:Call(L) end

---@class XLua.RawObject
---@field Target System.Object
XLua.RawObject = {}
---@alias CS.XLua.RawObject XLua.RawObject
CS.XLua.RawObject = XLua.RawObject


---@class XLua.ReferenceEqualsComparer : System.Object
XLua.ReferenceEqualsComparer = {}
---@alias CS.XLua.ReferenceEqualsComparer XLua.ReferenceEqualsComparer
CS.XLua.ReferenceEqualsComparer = XLua.ReferenceEqualsComparer

---@return XLua.ReferenceEqualsComparer
function XLua.ReferenceEqualsComparer.New() end
---@param o1 System.Object
---@param o2 System.Object
---@return boolean
function XLua.ReferenceEqualsComparer:Equals(o1, o2) end
---@param obj System.Object
---@return number
function XLua.ReferenceEqualsComparer:GetHashCode(obj) end

---@class XLua.ReflectionUseAttribute : System.Attribute
XLua.ReflectionUseAttribute = {}
---@alias CS.XLua.ReflectionUseAttribute XLua.ReflectionUseAttribute
CS.XLua.ReflectionUseAttribute = XLua.ReflectionUseAttribute

---@return XLua.ReflectionUseAttribute
function XLua.ReflectionUseAttribute.New() end

---@class XLua.Report : System.Object
XLua.Report = {}
---@alias CS.XLua.Report XLua.Report
CS.XLua.Report = XLua.Report

---@return XLua.Report
function XLua.Report.New() end

---@class XLua.SignatureLoader : System.Object
XLua.SignatureLoader = {}
---@alias CS.XLua.SignatureLoader XLua.SignatureLoader
CS.XLua.SignatureLoader = XLua.SignatureLoader

---@param publicKey string
---@param loader XLua.LuaEnv.CustomLoader
---@return XLua.SignatureLoader
function XLua.SignatureLoader.New(publicKey, loader) end

---@class XLua.StaticLuaCallbacks : System.Object
XLua.StaticLuaCallbacks = {}
---@alias CS.XLua.StaticLuaCallbacks XLua.StaticLuaCallbacks
CS.XLua.StaticLuaCallbacks = XLua.StaticLuaCallbacks

---@return XLua.StaticLuaCallbacks
function XLua.StaticLuaCallbacks.New() end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.EnumAnd(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.EnumOr(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.DelegateCall(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.LuaGC(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.ToString(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.DelegateCombine(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.DelegateRemove(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.ArrayIndexer(L) end
---@param type System.Type
---@param L System.IntPtr
---@param obj System.Object
---@param array_idx number
---@param obj_idx number
---@return boolean
function XLua.StaticLuaCallbacks.TryPrimitiveArraySet(type, L, obj, array_idx, obj_idx) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.ArrayNewIndexer(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.ArrayLength(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.MetaFuncIndex(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.LoadAssembly(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.ImportType(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.ImportGenericType(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.Cast(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.XLuaAccess(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.XLuaPrivateAccessible(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.XLuaMetatableOperation(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.DelegateConstructor(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.ToFunction(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.GenericMethodWraper(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.GetGenericMethod(L) end
---@param L System.IntPtr
---@return number
function XLua.StaticLuaCallbacks.ReleaseCsObject(L) end

---@class XLua.SysGenConfig : System.Object
XLua.SysGenConfig = {}
---@alias CS.XLua.SysGenConfig XLua.SysGenConfig
CS.XLua.SysGenConfig = XLua.SysGenConfig


---@class XLua.TemplateEngine.Chunk : System.Object
---@field Type XLua.TemplateEngine.TokenType
---@field Text string
XLua.TemplateEngine.Chunk = {}
---@alias CS.XLua.TemplateEngine.Chunk XLua.TemplateEngine.Chunk
CS.XLua.TemplateEngine.Chunk = XLua.TemplateEngine.Chunk

---@param type XLua.TemplateEngine.TokenType
---@param text string
---@return XLua.TemplateEngine.Chunk
function XLua.TemplateEngine.Chunk.New(type, text) end

---@class XLua.TemplateEngine.LuaTemplate : System.Object
XLua.TemplateEngine.LuaTemplate = {}
---@alias CS.XLua.TemplateEngine.LuaTemplate XLua.TemplateEngine.LuaTemplate
CS.XLua.TemplateEngine.LuaTemplate = XLua.TemplateEngine.LuaTemplate

---@return XLua.TemplateEngine.LuaTemplate
function XLua.TemplateEngine.LuaTemplate.New() end
---@param chunks System.Collections.Generic.List
---@return string
function XLua.TemplateEngine.LuaTemplate.ComposeCode(chunks) end
---@overload fun(luaenv: XLua.LuaEnv, snippet: string) : XLua.LuaFunction
---@param L System.IntPtr
---@return number
function XLua.TemplateEngine.LuaTemplate.Compile(L) end
---@overload fun(compiledTemplate: XLua.LuaFunction, parameters: XLua.LuaTable) : string
---@overload fun(compiledTemplate: XLua.LuaFunction) : string
---@param L System.IntPtr
---@return number
function XLua.TemplateEngine.LuaTemplate.Execute(L) end
---@param L System.IntPtr
function XLua.TemplateEngine.LuaTemplate.OpenLib(L) end

---@class XLua.TemplateEngine.Parser : System.Object
---@field RegexString string
XLua.TemplateEngine.Parser = {}
---@alias CS.XLua.TemplateEngine.Parser XLua.TemplateEngine.Parser
CS.XLua.TemplateEngine.Parser = XLua.TemplateEngine.Parser

---@return XLua.TemplateEngine.Parser
function XLua.TemplateEngine.Parser.New() end
---@param snippet string
---@return System.Collections.Generic.List
function XLua.TemplateEngine.Parser.Parse(snippet) end

---@class XLua.TemplateEngine.TemplateFormatException : System.Exception
XLua.TemplateEngine.TemplateFormatException = {}
---@alias CS.XLua.TemplateEngine.TemplateFormatException XLua.TemplateEngine.TemplateFormatException
CS.XLua.TemplateEngine.TemplateFormatException = XLua.TemplateEngine.TemplateFormatException

---@param message string
---@return XLua.TemplateEngine.TemplateFormatException
function XLua.TemplateEngine.TemplateFormatException.New(message) end

---@class XLua.TemplateEngine.TokenType
---@field Code XLua.TemplateEngine.TokenType
---@field Eval XLua.TemplateEngine.TokenType
---@field Text XLua.TemplateEngine.TokenType
XLua.TemplateEngine.TokenType = {}
---@alias CS.XLua.TemplateEngine.TokenType XLua.TemplateEngine.TokenType
CS.XLua.TemplateEngine.TokenType = XLua.TemplateEngine.TokenType


---@class XLua.TemplateRef : UnityEngine.ScriptableObject
---@field LuaClassWrap UnityEngine.TextAsset
---@field LuaClassWrapGCM UnityEngine.TextAsset
---@field LuaDelegateBridge UnityEngine.TextAsset
---@field LuaDelegateWrap UnityEngine.TextAsset
---@field LuaEnumWrap UnityEngine.TextAsset
---@field LuaEnumWrapGCM UnityEngine.TextAsset
---@field LuaInterfaceBridge UnityEngine.TextAsset
---@field LuaRegister UnityEngine.TextAsset
---@field LuaRegisterGCM UnityEngine.TextAsset
---@field LuaWrapPusher UnityEngine.TextAsset
---@field PackUnpack UnityEngine.TextAsset
---@field TemplateCommon UnityEngine.TextAsset
XLua.TemplateRef = {}
---@alias CS.XLua.TemplateRef XLua.TemplateRef
CS.XLua.TemplateRef = XLua.TemplateRef

---@return XLua.TemplateRef
function XLua.TemplateRef.New() end

---@class XLua.TypeExtensions : System.Object
XLua.TypeExtensions = {}
---@alias CS.XLua.TypeExtensions XLua.TypeExtensions
CS.XLua.TypeExtensions = XLua.TypeExtensions

---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsValueType(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsEnum(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsPrimitive(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsAbstract(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsSealed(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsInterface(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsClass(type) end
---@param type System.Type
---@return System.Type
function XLua.TypeExtensions.BaseType(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsGenericType(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsGenericTypeDefinition(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsNestedPublic(type) end
---@param type System.Type
---@return boolean
function XLua.TypeExtensions.IsPublic(type) end
---@param type System.Type
---@return string
function XLua.TypeExtensions.GetFriendlyName(type) end

---@class XLua.Utils : System.Object
---@field OBJ_META_IDX number
---@field METHOD_IDX number
---@field GETTER_IDX number
---@field SETTER_IDX number
---@field CLS_IDX number
---@field CLS_META_IDX number
---@field CLS_GETTER_IDX number
---@field CLS_SETTER_IDX number
---@field LuaIndexsFieldName string
---@field LuaNewIndexsFieldName string
---@field LuaClassIndexsFieldName string
---@field LuaClassNewIndexsFieldName string
XLua.Utils = {}
---@alias CS.XLua.Utils XLua.Utils
CS.XLua.Utils = XLua.Utils

---@param L System.IntPtr
---@param idx number
---@param field_name string
---@return boolean
function XLua.Utils.LoadField(L, idx, field_name) end
---@param L System.IntPtr
---@return System.IntPtr
function XLua.Utils.GetMainState(L) end
---@param exclude_generic_definition boolean
---@return System.Collections.Generic.List
function XLua.Utils.GetAllTypes(exclude_generic_definition) end
---@param L System.IntPtr
---@param type System.Type
---@param metafunc string
---@param index number
function XLua.Utils.loadUpvalue(L, type, metafunc, index) end
---@param L System.IntPtr
---@param type System.Type
function XLua.Utils.RegisterEnumType(L, type) end
---@param L System.IntPtr
---@param type System.Type
function XLua.Utils.MakePrivateAccessible(L, type) end
---@param L System.IntPtr
---@param type System.Type
---@param privateAccessible boolean
function XLua.Utils.ReflectionWrap(L, type, privateAccessible) end
---@param type System.Type
---@param L System.IntPtr
---@param translator XLua.ObjectTranslator
---@param meta_count number
---@param method_count number
---@param getter_count number
---@param setter_count number
---@param type_id number
function XLua.Utils.BeginObjectRegister(type, L, translator, meta_count, method_count, getter_count, setter_count, type_id) end
---@param type System.Type
---@param L System.IntPtr
---@param translator XLua.ObjectTranslator
---@param csIndexer XLua.LuaDLL.lua_CSFunction
---@param csNewIndexer XLua.LuaDLL.lua_CSFunction
---@param base_type System.Type
---@param arrayIndexer XLua.LuaDLL.lua_CSFunction
---@param arrayNewIndexer XLua.LuaDLL.lua_CSFunction
function XLua.Utils.EndObjectRegister(type, L, translator, csIndexer, csNewIndexer, base_type, arrayIndexer, arrayNewIndexer) end
---@param L System.IntPtr
---@param idx number
---@param name string
---@param func XLua.LuaDLL.lua_CSFunction
function XLua.Utils.RegisterFunc(L, idx, name, func) end
---@param L System.IntPtr
---@param idx number
---@param name string
---@param type System.Type
---@param memberType XLua.LazyMemberTypes
---@param isStatic boolean
function XLua.Utils.RegisterLazyFunc(L, idx, name, type, memberType, isStatic) end
---@param L System.IntPtr
---@param translator XLua.ObjectTranslator
---@param idx number
---@param name string
---@param obj System.Object
function XLua.Utils.RegisterObject(L, translator, idx, name, obj) end
---@param type System.Type
---@param L System.IntPtr
---@param creator XLua.LuaDLL.lua_CSFunction
---@param class_field_count number
---@param static_getter_count number
---@param static_setter_count number
function XLua.Utils.BeginClassRegister(type, L, creator, class_field_count, static_getter_count, static_setter_count) end
---@param type System.Type
---@param L System.IntPtr
---@param translator XLua.ObjectTranslator
function XLua.Utils.EndClassRegister(type, L, translator) end
---@param L System.IntPtr
---@param type System.Type
function XLua.Utils.LoadCSTable(L, type) end
---@param L System.IntPtr
---@param type System.Type
---@param cls_table number
function XLua.Utils.SetCSTable(L, type, cls_table) end
---@param delegateMethod System.Reflection.MethodInfo
---@param bridgeMethod System.Reflection.MethodInfo
---@return boolean
function XLua.Utils.IsParamsMatch(delegateMethod, bridgeMethod) end
---@param method System.Reflection.MethodInfo
---@return boolean
function XLua.Utils.IsSupportedMethod(method) end
---@param method System.Reflection.MethodInfo
---@return System.Reflection.MethodInfo
function XLua.Utils.MakeGenericMethodWithConstraints(method) end
---@param csFunction XLua.LuaDLL.lua_CSFunction
---@return boolean
function XLua.Utils.IsStaticPInvokeCSFunction(csFunction) end
---@param type System.Type
---@return boolean
function XLua.Utils.IsPublic(type) end

---@class XLua.Utils.MethodKey : System.ValueType
---@field Name string
---@field IsStatic boolean
XLua.Utils.MethodKey = {}
---@alias CS.XLua.Utils.MethodKey XLua.Utils.MethodKey
CS.XLua.Utils.MethodKey = XLua.Utils.MethodKey



