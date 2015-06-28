using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Input
{
    public class CharacterEventArgs : EventArgs
    {
        private readonly char _character;

        public CharacterEventArgs(char character)
        {
            this._character = character;
        }

        public char Character   { get { return _character; } }
    }

    public class KeyEventArgs : EventArgs
    {
        private Keys _keyCode;
        private Modifiers _keyModifiers;
        public KeyEventArgs(Keys keyCode, Modifiers keyModifiers)
        {
            this._keyCode = keyCode;
            this._keyModifiers = keyModifiers;
        }

        public Keys KeyCode
        {
            get { return _keyCode; }
        }

        public Modifiers KeyModifiers
        {
            get { return _keyModifiers; }
        }
    }

    public class MouseEventArgs : EventArgs
    {
        private MouseButton button;
        private int clicks;
        private int x;
        private int y;
        private int delta;

        public MouseButton Button { get { return button;         } }
        public int Clicks         { get { return clicks;         } }
        public int X              { get { return x;              } }
        public int Y              { get { return y;              } }
        public Point Location     { get { return new Point(x,y); } }
        public int Delta          { get { return delta;          } }

        public MouseEventArgs(MouseButton button, int clicks, int x, int y, int delta)
        {
            this.button = button;
            this.clicks = clicks;
            this.x = x;
            this.y = y;
            this.delta = delta;
        }
    }

    public delegate void CharEnteredHandler(object sender, CharacterEventArgs e);
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);

    public delegate void MouseEventHandler(object sender, MouseEventArgs e);

    public enum MouseButton
    {
        None, Left, Right, Middle, X1, X2
    }

    public static class InputSystem
    {
        #region Events
        /// <summary>
        /// Event raised when a character has been entered.
        /// </summary>
        public static event CharEnteredHandler CharEntered;

        /// <summary>
        /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
        /// </summary>
        public static event KeyEventHandler KeyDown;

        /// <summary>
        /// Event raised when a key has been released.
        /// </summary>
        public static event KeyEventHandler KeyUp;

        /// <summary>
        /// Event raised when a mouse button is pressed.
        /// </summary>
        public static event MouseEventHandler MouseDown;

        /// <summary>
        /// Event raised when a mouse button is released.
        /// </summary>
        public static event MouseEventHandler MouseUp;

        /// <summary>
        /// Event raised when the mouse changes location.
        /// </summary>
        public static event MouseEventHandler MouseMove;

        /// <summary>
        /// Event raised when the mouse has hovered in the same location for a short period of time.
        /// </summary>
        public static event MouseEventHandler MouseHover;

        /// <summary>
        /// Event raised when the mouse wheel has been moved.
        /// </summary>
        public static event MouseEventHandler MouseWheel;

        /// <summary>
        /// Event raised when a mouse button has been double clicked.
        /// </summary>
        public static event MouseEventHandler MouseDoubleClick;
        #endregion

        static bool initialized;

        public static Point MouseLocation
        {
            get
            {
                MouseState state = Mouse.GetState();
                return new Point(state.X, state.Y);
            }
        }

        public static bool ShiftDown
        {
            get
            {
                KeyboardState state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift);
            }
        }

        public static bool CtrlDown
        {
            get
            {
                KeyboardState state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftControl) || state.IsKeyDown(Keys.RightControl);
            }
        }

        public static bool AltDown
        {
            get
            {
                KeyboardState state = Keyboard.GetState();
                return state.IsKeyDown(Keys.LeftAlt) || state.IsKeyDown(Keys.RightAlt);
            }
        }

        private static KeyboardState _previousKeyboardState;
        private static MouseState _previousMouseState;
        public static void Update(GameTime time)
        {
            UpdateMouseEvents(time);
            UpdateKeyboardEvents(time);
        }

        private static void UpdateMouseEvents(GameTime gameTime)
        {
            MouseState current = Mouse.GetState();

            // Check button press.
            if (current.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released) 
                RaiseMouseDownEvent(MouseButton.Left, current.X, current.Y);

            if (current.MiddleButton == ButtonState.Pressed && _previousMouseState.MiddleButton == ButtonState.Released) 
                RaiseMouseDownEvent(MouseButton.Middle, current.X, current.Y);

            if (current.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released) 
                RaiseMouseDownEvent(MouseButton.Right, current.X, current.Y);
            
            if (current.XButton1 == ButtonState.Pressed && _previousMouseState.XButton1 == ButtonState.Released) 
                RaiseMouseDownEvent(MouseButton.X1, current.X, current.Y);
            
            if (current.XButton2 == ButtonState.Pressed && _previousMouseState.XButton2 == ButtonState.Released) 
                RaiseMouseDownEvent(MouseButton.X2, current.X, current.Y);
            
            // Check button releases.
            if (current.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed) 
                RaiseMouseUpEvent(MouseButton.Left, current.X, current.Y);
            
            if (current.MiddleButton == ButtonState.Released && _previousMouseState.MiddleButton == ButtonState.Pressed) 
                RaiseMouseUpEvent(MouseButton.Middle, current.X, current.Y);
            
            if (current.RightButton == ButtonState.Released && _previousMouseState.RightButton == ButtonState.Pressed) 
                RaiseMouseUpEvent(MouseButton.Right, current.X, current.Y);

            if (current.XButton1 == ButtonState.Released && _previousMouseState.XButton1 == ButtonState.Pressed) 
                RaiseMouseUpEvent(MouseButton.X1, current.X, current.Y);
            
            if (current.XButton2 == ButtonState.Released && _previousMouseState.XButton2 == ButtonState.Pressed) 
                RaiseMouseUpEvent(MouseButton.X2, current.X, current.Y);

            // Whether ANY button is pressed.
            bool buttonDown = current.LeftButton == ButtonState.Pressed ||
                current.MiddleButton == ButtonState.Pressed ||
                current.RightButton == ButtonState.Pressed ||
                current.XButton1 == ButtonState.Pressed ||
                current.XButton2 == ButtonState.Pressed;

            // Check for any sort of mouse movement. If a button is down, it's a drag,
            // otherwise it's a move.
            if (_previousMouseState.X != current.X || _previousMouseState.Y != current.Y)
                RaiseMouseMoveEvent(current.X, current.Y);

            // Handle mouse wheel events.
            if (_previousMouseState.ScrollWheelValue != current.ScrollWheelValue)
                RaiseMouseScrollEvent(current.X, current.Y, current.ScrollWheelValue);

            _previousMouseState = current;
        }

        // TODO Delegate keyboard and mouse handling in
        //      their own respective classes
        static TimeSpan _lastPress = TimeSpan.Zero;
        static int _initialDelay = 800;
        static int _repeatDelay = 50;
        static Keys _lastKey = Keys.None;
        static bool _isInitial = false;

        private static void UpdateKeyboardEvents(GameTime gameTime)
        {
            KeyboardState current = Keyboard.GetState();

            // Build the modifiers that currently apply to the current situation.
            var modifiers = Modifiers.None;
            if (current.IsKeyDown(Keys.LeftControl) || current.IsKeyDown(Keys.RightControl))
                modifiers |= Modifiers.Control;
            
            if (current.IsKeyDown(Keys.LeftShift) || current.IsKeyDown(Keys.RightShift))
                modifiers |= Modifiers.Shift;
            
            if (current.IsKeyDown(Keys.LeftAlt) || current.IsKeyDown(Keys.RightAlt))
                modifiers |= Modifiers.Alt;
            
            // Key pressed and initial key typed events for all keys.
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (current.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key))
                {
                    RaiseKeyDownEvent(key, modifiers);
                    char? keyChar = KeyboardUtil.ToChar(key, modifiers);
                    if (keyChar.HasValue)
                        RaiseCharacterEvent(keyChar.Value);

                    // Maintain the state of last key pressed.
                    _lastKey = key;
                    _lastPress = gameTime.TotalGameTime;
                    _isInitial = true;
                }

                if (current.IsKeyUp(key) && _previousKeyboardState.IsKeyDown(key))
                    RaiseKeyUpEvent(key, modifiers);
            }

            // Handle keys being held down and getting multiple KeyTyped events in sequence.
            var elapsedTime = (gameTime.TotalGameTime - _lastPress).TotalMilliseconds;

            if (current.IsKeyDown(_lastKey) && 
                ((_isInitial && elapsedTime > _initialDelay) || (!_isInitial && elapsedTime > _repeatDelay)))
            {
                RaiseKeyDownEvent(_lastKey, modifiers);
                char? lastKeyChar = KeyboardUtil.ToChar(_lastKey, modifiers);
                if (lastKeyChar.HasValue) 
                {
                    RaiseCharacterEvent(lastKeyChar.Value);
                    _lastPress = gameTime.TotalGameTime;
                    _isInitial = false;
                }
            }

            _previousKeyboardState = current;
        }

        #region Keyboard Message Helpers
        static void RaiseCharacterEvent(char character)
        {
            if (CharEntered != null)
                CharEntered(null, new CharacterEventArgs(character));
        }
            
        static void RaiseKeyUpEvent(Keys key, Modifiers modifiers)
        {
            if (KeyUp != null)
                KeyUp(null, new KeyEventArgs(key, modifiers));
        }

        static void RaiseKeyDownEvent(Keys key, Modifiers modifiers)
        {
            if (KeyDown != null)
                KeyDown(null, new KeyEventArgs(key, modifiers));
        }
        #endregion

        #region Mouse Message Helpers
        static void RaiseMouseScrollEvent(int x, int y, int delta)
        {
            if (MouseWheel != null)
                MouseWheel(null, new MouseEventArgs(MouseButton.None, 0, x, y, delta));
        }

        static void RaiseMouseMoveEvent(int x, int y)
        {
            if (MouseMove != null)
                MouseMove(null, new MouseEventArgs(MouseButton.None, 0, x, y, 0));
        }

        static void RaiseMouseDownEvent(MouseButton button, int x, int y)
        {
            if (MouseDown != null)
                MouseDown(null, new MouseEventArgs(button, 1, x, y, 0));
        }

        static void RaiseMouseUpEvent(MouseButton button, int x, int y)
        {
            if (MouseUp != null)
                MouseUp(null, new MouseEventArgs(button, 1, x, y, 0));
        }

        static void RaiseMouseDblClickEvent(MouseButton button, int x, int y)
        {
            if (MouseDoubleClick != null)
                MouseDoubleClick(null, new MouseEventArgs(button, 1, x, y, 0));
        }
        #endregion
    }
}