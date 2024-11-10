using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static InfixToPostfixConvertersh.Form1;
namespace InfixToPostfixConvertersh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

public class InfixToPostfixConverter
        {

            private int GetPrecedence(char op)
            {
                switch (op)
                {
                    case '+':
                    case '-':
                        return 1;
                    case '*':
                    case '/':
                        return 2;
                    case '^':
                        return 3;
                    default:
                        return 0;
                }
            }

            private bool IsOperator(char c)
            {
                return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
            }

            public string Convert(string infix)
            {
                StringBuilder postfix = new StringBuilder();
                Stack<char> stack = new Stack<char>();

                foreach (char token in infix)
                {
                    if (char.IsWhiteSpace(token))
                        continue;

                    if (char.IsLetterOrDigit(token))
                    {
                        postfix.Append(token);
                    }
                    else if (IsOperator(token))
                    {
                        while (stack.Count > 0 && GetPrecedence(stack.Peek()) >= GetPrecedence(token))
                        {
                            postfix.Append(stack.Pop());
                        }
                        stack.Push(token);
                    }
                    else if (token == '(')
                    {
                        stack.Push(token);
                    }
                    else if (token == ')')
                    {
                        while (stack.Count > 0 && stack.Peek() != '(')
                        {
                            postfix.Append(stack.Pop());
                        }
                        stack.Pop(); // Remove '(' from stack
                    }
                }

                while (stack.Count > 0)
                {
                    postfix.Append(stack.Pop());
                }

                return postfix.ToString();
            }
        }

      

        private void btnConvert_Click_1(object sender, EventArgs e)
        {
                string infixExpression = textBox1.Text;
                InfixToPostfixConverter converter = new InfixToPostfixConverter();
                string postfixExpression = converter.Convert(infixExpression);
                textBox2.Text = postfixExpression;

                PostfixEvaluator evaluator = new PostfixEvaluator();
                int result = evaluator.Evaluate(postfixExpression); // در صورتی که بخواهید محاسبه کنید
                label1.Text= ("نتیجه: " + result);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
        }
    }
}
public class PostfixEvaluator
{
    public int Evaluate(string postfix)
    {
        Stack<int> stack = new Stack<int>();

        foreach (char token in postfix)
        {
            if (char.IsDigit(token))
            {
                stack.Push(token - '0'); // تبدیل به عدد
            }
            else
            {
                int operand2 = stack.Pop();
                int operand1 = stack.Pop();
                switch (token)
                {
                    case '+':
                        stack.Push(operand1 + operand2);
                        break;
                    case '-':
                        stack.Push(operand1 - operand2);
                        break;
                    case '*':
                        stack.Push(operand1 * operand2);
                        break;
                    case '/':
                        stack.Push(operand1 / operand2);
                        break;
                }
            }
        }

        return stack.Pop();
    }
}
