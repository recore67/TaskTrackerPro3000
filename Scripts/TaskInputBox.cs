using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerPro3000.Scripts
{
    internal class TaskInputBox : Control
    {
        private TextBox textBox;

        public TaskListBox taskListBox;

        public string taskNameText
        {
            get { return textBox.Text; }
            //set { textBox.Text = value; }
        }

        public TaskInputBox(Control Parent)
        {
            textBox = CreateTextBox(Parent);
        }

        TextBox CreateTextBox(Control controlToParent)
        {
            textBox = new TextBox();
            textBox.Dock = DockStyle.Bottom;
            textBox.Leave += TextBox_Leave;
            textBox.KeyPress += TextBox_KeyPress;

            Button button = new Button();
            button.AutoSize = true;
            button.Text = "Add Task";
            button.Dock = DockStyle.Right;
            button.MouseClick += Button_MouseClick;

            textBox.Controls.Add(button);
            controlToParent.Controls.Add(textBox);

            return textBox;
        }

        private void Button_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!string.IsNullOrEmpty(taskNameText))
                {
                    taskListBox.CreateNewTask(taskNameText);
                    textBox.Clear();
                }
            }
        }

        private void TextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    taskListBox.CreateNewTask(textBox.Text);
                    textBox.Clear();
                }
            }
        }

        private void TextBox_Leave(object? sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Clear();
        }
    }
}
