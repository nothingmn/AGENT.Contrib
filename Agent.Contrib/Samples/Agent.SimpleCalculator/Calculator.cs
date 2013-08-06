using System;
using System.Collections;
using AGENT.Contrib.Drawing;
using AGENT.Contrib.Hardware;
using AGENT.Contrib.Util;
using AGENT.SimpleCalculator;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace SimpleCalculator
{
    public class Calculator
    {
        public ArrayList Buttons { get; set; }
        private int ButtonHeight = 15;
        private int ButtonWidth = 15;
        private int ButtonBorder = 3;
        private Font font = null;
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
        public CalculatorButton SelectedButton { get; set; }
        private string input = "";
        private string op = "";
        private string secondInput = "";

        public Calculator()
        {
            font = Resources.GetFont(Resources.FontResources.NinaB);
            ButtonHelper.Current.OnButtonPress += Current_OnButtonPress;
            BackgroundColor = Color.White;
            ForegroundColor = Color.Black;

            ButtonHeight = font.Height;
            ButtonWidth = font.CharWidth('8')*2;

            //each button has a height of 15
            //we want to align from the bottom up
            int leftPadding = 7;
            Buttons = new ArrayList();
            int bottom = Program.AgentSize - ButtonHeight - ButtonBorder*2;
            int left = leftPadding;
            int width = ButtonWidth + ButtonBorder;
            char[] map = new char[]
                {
                    '0', '.', 'x', '/', '=',
                    '1', '2', '3', '-', '=',
                    '4', '5', '6', '+', '=',
                    '7', '8', '9', 'C', '='
                };
            for (int i = 0; i <= map.Length - 1; i++)
            {
                var newButton = new CalculatorButton()
                    {
                        Point = new Point(left, bottom),
                        Text = map[i].ToString()
                    };

                Buttons.Add(newButton);

                if (SelectedButton == null)
                {
                    SelectedButton = newButton;
                    newButton.Selected = true;
                }
                if (i == 4 || i == 9 || i == 14 || i == 19)
                {
                    bottom = bottom - ButtonHeight - ButtonBorder*2;
                    left = leftPadding;
                }
                else
                {
                    left = left + (ButtonWidth + ButtonBorder + 4);
                }
            }
        }

        private void Current_OnButtonPress(Buttons button, Microsoft.SPOT.Hardware.InterruptPort port,
                                           ButtonDirection direction, DateTime time)
        {
            if (direction == ButtonDirection.Up)
            {
                if (button == AGENT.Contrib.Hardware.Buttons.TopRight)
                {
                    Debug.Print("Button press up");
                    for (int i = 0; i < Buttons.Count - 1; i++)
                    {
                        CalculatorButton current = Buttons[i] as CalculatorButton;
                        if (current.Selected && i < Buttons.Count)
                        {
                            SelectedButton = Buttons[i + 1] as CalculatorButton;
                            current.Selected = false;
                            SelectedButton.Selected = true;
                            Debug.Print("was:" + current.Text + " is:" + SelectedButton.Text);
                            break;
                        }
                    }
                    Render(Screen);
                }
                if (button == AGENT.Contrib.Hardware.Buttons.BottomRight)
                {
                    Debug.Print("Button press down");
                    for (int i = Buttons.Count - 1; i >= 0; i--)
                    {
                        CalculatorButton current = Buttons[i] as CalculatorButton;
                        if (current.Selected && i > 0)
                        {
                            SelectedButton = Buttons[i - 1] as CalculatorButton;
                            current.Selected = false;
                            SelectedButton.Selected = true;
                            Debug.Print("was:" + current.Text + " is:" + SelectedButton.Text);
                            break;
                        }
                    }
                    Render(Screen);
                }
                if (button == AGENT.Contrib.Hardware.Buttons.MiddleRight)
                {
                    Debug.Print("Button press select:" + SelectedButton.Text);
                    var text = SelectedButton.Text;


                    if (input !="" && op == "" && (text == "x" || text == "/" || text == "+" || text == "-"))
                    {
                        Debug.Print("OP GO");
                        secondInput = input;
                        input = "";
                        op = text;
                    }
                    else if (text == "C")
                    {
                        Debug.Print("Clear");
                        secondInput = "";
                        op = "";
                        input = "";
                    }
                    else if (op !="" && input != "" && secondInput !="" && text == "=")
                    {
                        Debug.Print("CALC");
                        double left = 0;
                        double right = 0;
                        if (Parse.TryParseDouble(secondInput, out left) && Parse.TryParseDouble(input, out right))
                        {
                            try
                            {
                                switch (op)
                                {
                                    case "x":
                                        secondInput = (left*right).ToString("f2");
                                        break;
                                    case "/":
                                        secondInput = (left/right).ToString("f2");
                                        break;
                                    case "+":
                                        secondInput = (left + right).ToString("f2");
                                        break;
                                    case "-":
                                        secondInput = (left - right).ToString("f2");
                                        break;
                                    default:
                                        break;
                                }
                                op = "";
                                input = "";
                            }
                            catch (Exception)
                            {
                                secondInput = "";
                                op = "";
                                input = "Err.";
                            }
                        }

                    }
                    else
                    {
                        Debug.Print("Dumped to other");
                        int value = 0;
                        if (Parse.TryParseInt(text, out value) || text == ".")
                        {
                            Debug.Print("numeric");
                            input = input + SelectedButton.Text;
                        }
                    }

                    Render(Screen);
                }
            }
        }

        //private Operation op = new Operation();
        private void Clear()
        {
        }

        public Bitmap Screen { get; set; }

        public void Render(Bitmap screen)
        {
            Screen = screen;
            if (Buttons != null)
            {
                screen.Clear();
                foreach (CalculatorButton button in Buttons)
                {
                    button.Render(screen, font, ButtonHeight, ButtonWidth, ButtonBorder, ForegroundColor,
                                  BackgroundColor);
                }


                //calculation result
                screen.DrawText(input, font, BackgroundColor, 2, 2);
                screen.DrawText(secondInput + op, font, BackgroundColor, 2, 2 + font.Height + 2);

                //draw a simple border for the digit display
                screen.DrawRectangle(ForegroundColor, 1, 1, 1, Program.AgentSize, 28, 0, 0,
                                     BackgroundColor, 0, 0, BackgroundColor, 0, 0, 0);

                //draw a simple border
                screen.DrawRectangle(Color.White, 1, 1, 1, Program.AgentSize, Program.AgentSize, 0, 0,
                                     BackgroundColor, 0, 0, BackgroundColor, 0, 0, 0);

                screen.Flush();
            }
        }

    }
}