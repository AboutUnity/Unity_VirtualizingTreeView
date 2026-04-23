using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Battlehub.UIControls
{
#if ENABLE_INPUT_SYSTEM

    public class VirtualizingItemsControlInputProvider : InputProvider
    {
        public Key MultiselectKey = Key.LeftCtrl;
        public Key RangeselectKey = Key.LeftShift;
        public Key SelectAllKey = Key.A;
        public Key DeleteKey = Key.Delete;

        public bool IncludeRightCtrl = true;
        public bool IncludeRightShift = true;

        private static Keyboard Kb => Keyboard.current;
        private static Mouse Ms => Mouse.current;
        private static Touchscreen Ts => Touchscreen.current;

        private bool IsCtrlPressed()
        {
            var kb = Kb;
            if (kb == null) return false;

            if (MultiselectKey == Key.RightCtrl) return kb.rightCtrlKey.isPressed;
            if (MultiselectKey == Key.LeftCtrl) return kb.leftCtrlKey.isPressed;

            return kb.leftCtrlKey.isPressed || (IncludeRightCtrl && kb.rightCtrlKey.isPressed);
        }

        private bool IsShiftPressed()
        {
            var kb = Kb;
            if (kb == null) return false;

            if (RangeselectKey == Key.RightShift) return kb.rightShiftKey.isPressed;
            if (RangeselectKey == Key.LeftShift) return kb.leftShiftKey.isPressed;

            return kb.leftShiftKey.isPressed || (IncludeRightShift && kb.rightShiftKey.isPressed);
        }

        private bool IsDeleteDown()
        {
            var kb = Kb;
            if (kb == null) return false;

            if (DeleteKey == Key.Backspace) return kb.backspaceKey.wasPressedThisFrame;
            if (DeleteKey == Key.Delete) return kb.deleteKey.wasPressedThisFrame;

            return kb.backspaceKey.wasPressedThisFrame || kb.deleteKey.wasPressedThisFrame;
        }

        private bool IsSelectAllDown()
        {
            var kb = Kb;
            if (kb == null) return false;

            return kb.aKey.wasPressedThisFrame;
        }

        public override float HorizontalAxis
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

        public override float VerticalAxis
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

        public override float HorizontalAxis2
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

        public override float VerticalAxis2
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

        public override bool IsHorizontalButtonDown =>
            Kb != null && (Kb.leftArrowKey.wasPressedThisFrame || Kb.rightArrowKey.wasPressedThisFrame);

        public override bool IsVerticalButtonDown =>
            Kb != null && (Kb.upArrowKey.wasPressedThisFrame || Kb.downArrowKey.wasPressedThisFrame);

        public override bool IsHorizontal2ButtonDown =>
            Kb != null && (Kb.numpad4Key.wasPressedThisFrame || Kb.numpad6Key.wasPressedThisFrame);

        public override bool IsVertical2ButtonDown =>
            Kb != null && (Kb.numpad8Key.wasPressedThisFrame || Kb.numpad2Key.wasPressedThisFrame);

        public override bool IsFunctionalButtonPressed => IsCtrlPressed();
        public override bool IsFunctional2ButtonPressed => IsShiftPressed();

        public override bool IsSubmitButtonDown =>
            Kb != null && (Kb.enterKey.wasPressedThisFrame || Kb.numpadEnterKey.wasPressedThisFrame);

        public override bool IsSubmitButtonUp =>
            Kb != null && (Kb.enterKey.wasReleasedThisFrame || Kb.numpadEnterKey.wasReleasedThisFrame);

        public override bool IsCancelButtonDown =>
            Kb != null && Kb.escapeKey.wasPressedThisFrame;

        public override bool IsDeleteButtonDown => IsDeleteDown();
        public override bool IsSelectAllButtonDown => IsSelectAllDown();

        public override bool IsAnyKeyDown =>
            Kb != null && Kb.anyKey.wasPressedThisFrame;

        public override Vector3 MousePosition
        {
            get
            {
                var ms = Ms;
                if (ms == null) return Vector3.zero;
                var p = ms.position.ReadValue();
                return new Vector3(p.x, p.y, 0);
            }
        }

        public override bool IsMouseButtonDown(int button)
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

        public override bool IsMousePresent => Ms != null;
        public override bool IsKeyboardPresent => Kb != null;

        public override int TouchCount
        {
            get
            {
                var ts = Ts;
                if (ts == null) return 0;

                int count = 0;
                foreach (var t in ts.touches)
                {
                    if (t.press != null && t.press.isPressed)
                        count++;
                }
                return count;
            }
        }

        public override Touch GetTouch(int i)
        {
            return default;
        }

        public override bool IsTouchSupported => Ts != null;
    }

#else

    public class VirtualizingItemsControlInputProvider : InputProvider
    {
        public KeyCode MultiselectKey = KeyCode.LeftControl;
        public KeyCode RangeselectKey = KeyCode.LeftShift;
        public KeyCode SelectAllKey = KeyCode.A;
        public KeyCode DeleteKey = KeyCode.Delete;

        public override bool IsFunctionalButtonPressed => Input.GetKey(MultiselectKey);
        public override bool IsFunctional2ButtonPressed => Input.GetKey(RangeselectKey);
        public override bool IsDeleteButtonDown => Input.GetKeyDown(DeleteKey);
        public override bool IsSelectAllButtonDown => Input.GetKeyDown(SelectAllKey);
    }

#endif
}
