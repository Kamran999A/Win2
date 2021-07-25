using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using T2.Calculator;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void CalculateEquation()
        {
            this.guna2ResultLabel1.Text = ParseOperation();

            FocusInputText();
        }
        private string ParseOperation()
        {
            try
            {
                var input = this.gunaOperationtxt.Text;

                input = input.Replace(" ", "");

                var operation = new Operation();
                var leftSide = true;

                for (int i = 0; i < input.Length; i++)
                {
                    if ("0123456789.".Any(c => input[i] == c))
                    {
                        if (leftSide)
                            operation.LeftSide = AddNumberPart(operation.LeftSide, input[i]);
                        else
                            operation.RightSide = AddNumberPart(operation.RightSide, input[i]);
                    }
                    else if ("+−X÷".Any(c => input[i] == c))
                    {
                        if (!leftSide)
                        {
                            var operatorType = GetOperationType(input[i]);

                            if (operation.RightSide.Length == 0)
                            {
                                if (operatorType != OperationType.Sub)
                                    throw new InvalidOperationException($"Operator (+ X ÷ or more than one −) specified without an right side number");

                                operation.RightSide += input[i];
                            }
                            else
                            {
                                operation.LeftSide = CalculateOperation(operation);

                                operation.OperationType = operatorType;

                                operation.RightSide = string.Empty;
                            }
                        }
                        else
                        {
                            var operatorType = GetOperationType(input[i]);

                            if (operation.LeftSide.Length == 0)
                            {
                                if (operatorType != OperationType.Sub)
                                    throw new InvalidOperationException($"Operator (+ X ÷ or more than one −) specified without an left side number");

                                operation.LeftSide += input[i];
                            }
                            else
                            {
                                operation.OperationType = operatorType;

                                leftSide = false;
                            }
                        }
                    }
                }

                return CalculateOperation(operation);
            }
            catch (Exception ex)
            {
                return $"Invalid equation. {ex.Message}";
            }
        }
        private string CalculateOperation(Operation operation)
        {
            decimal left = 0;
            decimal right = 0;

            if (string.IsNullOrEmpty(operation.LeftSide) || !decimal.TryParse(operation.LeftSide, out left))
                throw new InvalidOperationException($"Left side of the operation was not a number. {operation.LeftSide}");

            if (string.IsNullOrEmpty(operation.RightSide) || !decimal.TryParse(operation.RightSide, out right))
                throw new InvalidOperationException($"Right side of the operation was not a number. {operation.RightSide}");

            try
            {
                switch (operation.OperationType)
                {
                    case OperationType.Add:
                        return (left + right).ToString();
                    case OperationType.Sub:
                        return (left - right).ToString();
                    case OperationType.Div:
                        return (left / right).ToString();
                    case OperationType.Mul:
                        return (left * right).ToString();
                    default:
                        throw new InvalidOperationException($"Unknown operator type when calculating operation. { operation.OperationType }");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to calculate operation {operation.LeftSide} {operation.OperationType} {operation.RightSide}. {ex.Message}");
            }
        }
        private OperationType GetOperationType(char character)
        {
            switch (character)
            {
                case '+':
                    return OperationType.Add;
                case '−':
                    return OperationType.Sub;
                case '÷':
                    return OperationType.Div;
                case 'X':
                    return OperationType.Mul;
                default:
                    throw new InvalidOperationException($"Unknown operator type { character }");
            }
        }
        private string AddNumberPart(string currentNumber, char newCharacter)
        {
            if (newCharacter == '.' && currentNumber.Contains('.'))
                throw new InvalidOperationException($"Number {currentNumber} already contains a . and another cannot be added");

            return currentNumber + newCharacter;
        }
        private void InsertTextValue(string value)
        {
            var selectionStart = this.gunaOperationtxt.SelectionStart;

            this.gunaOperationtxt.Text = this.gunaOperationtxt.Text.Insert(this.gunaOperationtxt.SelectionStart, value);

            this.gunaOperationtxt.SelectionStart = selectionStart + value.Length;

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button btn )
            {
                InsertTextValue(btn.Text);

                FocusInputText();
            }
            if (sender is Control btn2)
            {
                InsertTextValue(btn2.Text);

                FocusInputText();
            }
        }

        private void FocusInputText()
        {
            this.gunaOperationtxt.Focus();
        }

        private void DeleteTextValue()
        {
            if (this.gunaOperationtxt.SelectionStart == 0)
                return;


            var selectionStart = this.gunaOperationtxt.SelectionStart;

            this.gunaOperationtxt.Text = this.gunaOperationtxt.Text.Remove(this.gunaOperationtxt.SelectionStart - 1, 1);

            this.gunaOperationtxt.SelectionStart = selectionStart - 1;
        }
        private void ClearUserInput()
        {
            this.gunaOperationtxt.Text = string.Empty;
        }

        private void ClearResult()
        {
            this.guna2ResultLabel1.Text = string.Empty;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void gunaACbtn_Click(object sender, EventArgs e)
        {
            ClearUserInput();
            ClearResult();
        }

        private void guna2GradientCircleButton19_Click(object sender, EventArgs e)
        {
            CalculateEquation();
        }

        private void gunaClearbtn_Click(object sender, EventArgs e)
        {
            this.gunaOperationtxt.Text = string.Empty;
        }

        private void gunaDelbtn_Click(object sender, EventArgs e)
        {
            DeleteTextValue();
            FocusInputText();
        }

        private void gunaRefreshbtn_Click(object sender, EventArgs e)
        {
            ClearUserInput();
            ClearResult();
        }
    }
}
