using UnityEngine;

#if !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
#endif

namespace Battlehub.UIControls
{
#if !ENABLE_LEGACY_INPUT_MANAGER


    public class VirtualizingItemsControlInputProvider : InputProvider
    {
        public KeyCode MultiselectKey = KeyCode.LeftControl;
        public KeyCode RangeselectKey = KeyCode.LeftShift;
        public KeyCode SelectAllKey = KeyCode.A;
        public KeyCode DeleteKey = KeyCode.Delete;

        public bool IncludeRightCtrl = true;
        public bool IncludeRightShift = true;

        [System.NonSerialized] private Key _multiselectKey;
        [System.NonSerialized] private Key _multiselectKeyAlt;

        [System.NonSerialized] private Key _rangeselectKey;
        [System.NonSerialized] private Key _rangeselectKeyAlt;

        [System.NonSerialized] private Key _selectAllKey;
        [System.NonSerialized] private Key _deleteKey;

        private void Awake()
        {
            _multiselectKey = Convert(MultiselectKey);
            _rangeselectKey = Convert(RangeselectKey);
            _selectAllKey = Convert(SelectAllKey);
            _deleteKey = Convert(DeleteKey);

            _multiselectKeyAlt = GetAltCtrl(MultiselectKey);
            _rangeselectKeyAlt = GetAltShift(RangeselectKey);
        }

        private Key Convert(KeyCode keyCode)
        {
            return keyCode switch
            {
                KeyCode.LeftControl => Key.LeftCtrl,
                KeyCode.RightControl => Key.RightCtrl,
                KeyCode.LeftShift => Key.LeftShift,
                KeyCode.RightShift => Key.RightShift,
                KeyCode.A => Key.A,
                KeyCode.Delete => Key.Delete,
                KeyCode.Backspace => Key.Backspace,
                _ => Key.None
            };
        }

        private Key GetAltCtrl(KeyCode keyCode)
        {
            if (keyCode == KeyCode.LeftControl) return Key.RightCtrl;
            if (keyCode == KeyCode.RightControl) return Key.LeftCtrl;
            return Key.None;
        }

        private Key GetAltShift(KeyCode keyCode)
        {
            if (keyCode == KeyCode.LeftShift) return Key.RightShift;
            if (keyCode == KeyCode.RightShift) return Key.LeftShift;
            return Key.None;
        }

        private bool IsCtrlPressed()
        {
            var kb = Keyboard.current;
            if (kb == null)
                return false;

            bool primary = _multiselectKey != Key.None && kb[_multiselectKey].isPressed;
            bool secondary = IncludeRightCtrl &&
                             _multiselectKeyAlt != Key.None &&
                             kb[_multiselectKeyAlt].isPressed;

            return primary || secondary;
        }

        private bool IsShiftPressed()
        {
            var kb = Keyboard.current;
            if (kb == null)
                return false;

            bool primary = _rangeselectKey != Key.None && kb[_rangeselectKey].isPressed;
            bool secondary = IncludeRightShift &&
                             _rangeselectKeyAlt != Key.None &&
                             kb[_rangeselectKeyAlt].isPressed;

            return primary || secondary;
        }

        private bool IsDeleteDown()
        {
            var kb = Keyboard.current;
            if (kb == null)
                return false;

            if (DeleteKey == KeyCode.Delete)
                return kb.deleteKey.wasPressedThisFrame;

            if (DeleteKey == KeyCode.Backspace)
                return kb.backspaceKey.wasPressedThisFrame;

            return _deleteKey != Key.None &&
                   kb[_deleteKey].wasPressedThisFrame;
        }

        private bool IsSelectAllDown()
        {
            var kb = Keyboard.current;
            if (kb == null)
                return false;

            return _selectAllKey != Key.None &&
                   kb[_selectAllKey].wasPressedThisFrame;
        }

        public override bool IsFunctionalButtonPressed
        {
            get => IsCtrlPressed();
        }

        public override bool IsFunctional2ButtonPressed
        {
            get => IsShiftPressed();
        }

        public override bool IsDeleteButtonDown
        {
            get => IsDeleteDown();
        }

        public override bool IsSelectAllButtonDown
        {
            get => IsSelectAllDown();
        }

        public override int TouchCount => Touch.activeTouches.Count;

        public override Touch GetTouch(int i) => Touch.activeTouches[i];

        public override bool IsTouchSupported => Touchscreen.current != null;
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