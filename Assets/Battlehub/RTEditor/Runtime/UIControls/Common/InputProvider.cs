using UnityEngine;
using UnityEngine.EventSystems;

#if !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
#endif

namespace Battlehub.UIControls
{
    public interface IUpdateFocusedHandler : IEventSystemHandler
    {
        void OnUpdateFocused(BaseEventData eventData);
    }

#if !ENABLE_LEGACY_INPUT_MANAGER

    public class InputProvider : MonoBehaviour
    {
        protected static Keyboard Kb => Keyboard.current;
        protected static Mouse Ms => Mouse.current;

        public virtual float HorizontalAxis
        {
            get
            {
                var kb = Kb;
                if (kb == null) return 0;
                if (kb.leftArrowKey.isPressed) return -1;
                if (kb.rightArrowKey.isPressed) return 1;
                return 0;
            }
        }

        public virtual float VerticalAxis
        {
            get
            {
                var kb = Kb;
                if (kb == null) return 0;
                if (kb.upArrowKey.isPressed) return 1;
                if (kb.downArrowKey.isPressed) return -1;
                return 0;
            }
        }

        public virtual float HorizontalAxis2
        {
            get
            {
                var kb = Kb;
                if (kb == null) return 0;
                if (kb.numpad4Key.isPressed) return -1;
                if (kb.numpad6Key.isPressed) return 1;
                return 0;
            }
        }

        public virtual float VerticalAxis2
        {
            get
            {
                var kb = Kb;
                if (kb == null) return 0;
                if (kb.numpad8Key.isPressed) return 1;
                if (kb.numpad2Key.isPressed) return -1;
                return 0;
            }
        }

        public virtual bool IsHorizontalButtonDown
        {
            get
            {
                var kb = Kb;
                return kb != null && (kb.leftArrowKey.wasPressedThisFrame || kb.rightArrowKey.wasPressedThisFrame);
            }
        }

        public virtual bool IsVerticalButtonDown
        {
            get
            {
                var kb = Kb;
                return kb != null && (kb.upArrowKey.wasPressedThisFrame || kb.downArrowKey.wasPressedThisFrame);
            }
        }

        public virtual bool IsHorizontal2ButtonDown
        {
            get
            {
                var kb = Kb;
                return kb != null && (kb.numpad4Key.wasPressedThisFrame || kb.numpad6Key.wasPressedThisFrame);
            }
        }

        public virtual bool IsVertical2ButtonDown
        {
            get
            {
                var kb = Kb;
                return kb != null && (kb.numpad8Key.wasPressedThisFrame || kb.numpad2Key.wasPressedThisFrame);
            }
        }

        public virtual bool IsFunctionalButtonPressed
        {
            get
            {
                var kb = Kb;
                return kb != null && kb.leftCtrlKey.isPressed;
            }
        }

        public virtual bool IsFunctional2ButtonPressed
        {
            get
            {
                var kb = Kb;
                return kb != null && kb.leftShiftKey.isPressed;
            }
        }

        public virtual bool IsSubmitButtonDown
        {
            get
            {
                var kb = Kb;
                return kb != null && (kb.enterKey.wasPressedThisFrame || kb.numpadEnterKey.wasPressedThisFrame);
            }
        }

        public virtual bool IsSubmitButtonUp
        {
            get
            {
                var kb = Kb;
                return kb != null && (kb.enterKey.wasReleasedThisFrame || kb.numpadEnterKey.wasReleasedThisFrame);
            }
        }

        public virtual bool IsCancelButtonDown
        {
            get
            {
                var kb = Kb;
                return kb != null && kb.escapeKey.wasPressedThisFrame;
            }
        }

        public virtual bool IsDeleteButtonDown
        {
            get
            {
                var kb = Kb;
                return kb != null && kb.deleteKey.wasPressedThisFrame;
            }
        }

        public virtual bool IsSelectAllButtonDown
        {
            get
            {
                var kb = Kb;
                return kb != null && kb.aKey.wasPressedThisFrame;
            }
        }

        public virtual bool IsAnyKeyDown
        {
            get
            {
                var kb = Kb;
                return kb != null && kb.anyKey.wasPressedThisFrame;
            }
        }

        public virtual Vector3 MousePosition
        {
            get
            {
                var ms = Ms;
                if (ms == null) return Vector3.zero;
                Vector2 p = ms.position.ReadValue();
                return new Vector3(p.x, p.y, 0);
            }
        }

        public virtual bool IsMouseButtonDown(int button)
        {
            var ms = Ms;
            if (ms == null) return false;

            return button switch
            {
                0 => ms.leftButton.wasPressedThisFrame,
                1 => ms.rightButton.wasPressedThisFrame,
                2 => ms.middleButton.wasPressedThisFrame,
                _ => false,
            };
        }

        public virtual bool IsMousePresent
        {
            get { return Ms != null; }
        }

        public virtual bool IsKeyboardPresent
        {
            get { return Kb != null; }
        }

        public virtual int TouchCount
        {
            get { return Touch.activeTouches.Count; }
        }

        public virtual Touch GetTouch(int i)
        {
            return Touch.activeTouches[i];
        }

        public virtual bool IsTouchSupported
        {
            get { return Touchscreen.current != null; }
        }
    }

#else

    public class InputProvider : MonoBehaviour
    {
        public virtual float HorizontalAxis
        {
            get
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    return -1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    return 1;
                }
                return 0;
            }
        }

        public virtual float VerticalAxis
        {
            get
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    return 1;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    return -1;
                }
                return 0;
            }
        }

        public virtual float HorizontalAxis2
        {
            get
            {
                if (Input.GetKey(KeyCode.Keypad4))
                {
                    return -1;
                }
                else if (Input.GetKey(KeyCode.Keypad6))
                {
                    return 1;
                }
                return 0;
            }
        }

        public virtual float VerticalAxis2
        {
            get
            {
                if (Input.GetKey(KeyCode.Keypad8))
                {
                    return 1;
                }
                else if (Input.GetKey(KeyCode.Keypad2))
                {
                    return -1;
                }
                return 0;
            }
        }

        public virtual bool IsHorizontalButtonDown
        {
            get { return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow); }
        }

        public virtual bool IsVerticalButtonDown
        {
            get { return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow); }
        }

        public virtual bool IsHorizontal2ButtonDown
        {
            get { return Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Keypad6); }
        }

        public virtual bool IsVertical2ButtonDown
        {
            get { return Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Keypad2); }
        }

        public virtual bool IsFunctionalButtonPressed
        {
            get { return Input.GetKey(KeyCode.LeftControl); }
        }

        public virtual bool IsFunctional2ButtonPressed
        {
            get { return Input.GetKey(KeyCode.LeftShift); }
        }

        public virtual bool IsSubmitButtonDown
        {
            get { return Input.GetKeyDown(KeyCode.Return); }
        }

        public virtual bool IsSubmitButtonUp
        {
            get { return Input.GetKeyUp(KeyCode.Return); }
        }

        public virtual bool IsCancelButtonDown
        {
            get { return Input.GetKeyDown(KeyCode.Escape); }
        }

        public virtual bool IsDeleteButtonDown
        {
            get { return Input.GetKeyDown(KeyCode.Delete); }
        }

        public virtual bool IsSelectAllButtonDown
        {
            get { return Input.GetKeyDown(KeyCode.A); }
        }

        public virtual bool IsAnyKeyDown
        {
            get { return Input.anyKeyDown; }
        }

        public virtual Vector3 MousePosition
        {
            get { return Input.mousePosition; }
        }

        public virtual bool IsMouseButtonDown(int button)
        {
            return Input.GetMouseButtonDown(button);
        }

        public virtual bool IsMousePresent
        {
            get { return Input.mousePresent; }
        }

        public virtual bool IsKeyboardPresent
        {
            get { return true; }
        }

        public virtual int TouchCount
        {
            get { return Input.touchCount; }
        }

        public virtual Touch GetTouch(int i)
        {
            return Input.GetTouch(i);
        }

        public virtual bool IsTouchSupported
        {
            get { return Input.touchSupported; }
        }
    }

#endif
}