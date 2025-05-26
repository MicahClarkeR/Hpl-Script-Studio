using hpl_editor_application.Managers.Models;
using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace hpl_editor_application.Managers
{
    internal static class EditorManager
    {
        public static ActiveEditorState Active { get; private set; } = new ActiveEditorState();

        private static Dictionary<TextEditor, EditorState> States = new Dictionary<TextEditor, EditorState>();

        public static void InitialiseEditor(params TextEditor[] editors)
        {
            foreach (TextEditor editor in editors)
            {
                editor.TextArea.Caret.PositionChanged += Caret_PositionChanged;
                editor.GotFocus += Editor_GotFocus;
                editor.QueryCursor += Editor_QueryCursor;

                States.Add(editor, new EditorState());
            }
        }

        private static void Caret_PositionChanged(object? sender, EventArgs e)
        {
            if (Active.State != null)
            {
                var position = Active.Editor.TextArea.Caret.Position;
                Active.State.CaretPosition = position;
            }
        }

        private static void Editor_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GetEditorState(sender, out TextEditor? editor, out EditorState? state) && editor != null && state != null)
            {
                SetActiveEditor(editor, state);
            }
        }

        private static void Editor_QueryCursor(object sender, System.Windows.Input.QueryCursorEventArgs e)
        {
            if (GetEditorState(sender, out TextEditor? editor, out EditorState? state) && editor != null && state != null)
            {

            }
        }

        private static bool GetEditorState(object sender, out TextEditor? editor, out EditorState? state)
        {
            state = null;
            editor = (TextEditor) sender;

            var found = States.TryGetValue(editor, out EditorState? foundState);

            if (found && foundState != null)
            {
                state = foundState;
                return true;
            }

            return false;
        }

        private static void SetActiveEditor(TextEditor editor, EditorState state)
        {
            Active = new ActiveEditorState()
            {
                Editor = editor,
                State = state
            };

            MainWindow.Instance.SetStatus("Focused changed.");
        }

        public static void AppendAtCaret(string text)
        {
            if (Active.Editor == null)
                return;

            text = text.Trim();
            int caretPosition = Active.Editor.CaretOffset;
            string code = Active.Editor.Text;
            
            code = code.Insert(caretPosition, text);
            Active.Editor.Text = code;

            Active.Editor.Focus();
            Active.Editor.CaretOffset = caretPosition + text.Length;
        }

        public class ActiveEditorState
        {
            public EditorState State { get; set; }
            public TextEditor Editor { get; set; }
        }
    }
}
