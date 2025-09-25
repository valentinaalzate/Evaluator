using System.Data;
using System.IO.Pipes;

namespace Evaluator.core
{
    public class ExpressionEvaluator
    {
        public static double Evaluate(String infix)//va hacer el metodo que evalua
        {
            var postfix = InfixToPostfix(infix);
            Console.WriteLine(postfix);
            return Calculate(postfix);
        }


        //transformar el infijo en posfijo
        private static string InfixToPostfix(string infix)
        {
            var stack = new Stack<char>(); //pila
            var postfix = string.Empty;
            foreach (char item in infix) {

                if (IsOperator(item))
                {
                    if (item == ')')
                    {
                        do// mientras sea diferente ( vamos desapilando y metiendo a la posfija
                        {

                            postfix += stack.Pop(); //pop saco
                        } while (stack.Peek() != '(');
                        stack.Pop();
                    }
                    else {
                        if (stack.Count > 0)
                        {
                            if (PriorityInfix(item) > PriorityStack(stack.Peek()))
                            {
                                stack.Push(item);
                            }
                            else
                            {
                                postfix += stack.Pop(); //concatenamos operador
                                stack.Push(item);

                            }

                        }
                        else
                        {
                            stack.Push(item); // apila
                        }
                    }

                }
                else {
                    postfix += item; //concatenamos el operando 
                }
            }
            while (stack.Count>0) {
                postfix += stack.Pop();
            }

            return postfix;
        }
        //USE PATTERN MATCHING
        private static bool IsOperator(char item) => item is '^' or '*' or '/' or '%' or '+' or '-' or '(' or ')';
        /*feo
          if (item == '^' || item == '*' || item == '/' || item == '%' || item == '+' || item == '-' || item == '(' || item ==')') {

            return true;
        }
        return false;*/
        //simplicf


        private static int PriorityInfix(char op) => op switch
            /*feo
             switch (op) {
                case '^': return 4;
                case '*': return 2;
                case '/': return 2;
                case '%': return 2;
                case '+': return 1;
                case '-': return 1;
                case '(': return 5;
                default: throw new Exception("Invalid expression");
                

            }*/
            //bonito
           
            {
                '^' => 4,
                '*' or '/' or '%' => 2,
                '+' or '-' => 1,
                '(' => 5,
                _ => throw new Exception("Invalid expression"),
            };

        private  static int PriorityStack(char op) => op switch
       

        {
            '^' => 3,
            '*' or '/' or '%' => 2,
            '+' or '-' => 1,
            '(' => 0,
            _ => throw new Exception("Invalid expression"),
        };

        private static double Calculate(string postfix)
        {
            //evaluar los posfijos
            var stack = new Stack<double>();

            foreach (char item in postfix)
            {
                if (IsOperator(item)) {

                    var op2 = stack.Pop();
                    var op1 = stack.Pop();
                    //generate method
                    stack.Push(Calculate(op1, item, op2)); // sobrecargar
                }
                else
                {

                    stack.Push(Convert.ToDouble (item.ToString()));
                 
                }
            }
            return stack.Peek();
        }

        private static double Calculate(double op1, char item, double op2) => item switch
        {
            '*' => op1 * op2,
            '/' => op1 / op2,
            '^' => Math.Pow(op1, op2),
            '+' => op1 + op2,
            '-' => op1 - op2,
            _ => throw new Exception("Invalid Expression"),
        };
    }
}
