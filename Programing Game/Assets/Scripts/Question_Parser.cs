using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

public class Question_Parser : MonoBehaviour
{
    public Text qtext;
    public Text a1text;
    public Text a2text;
    public Text a3text;
    public Text a4text;
    public GameObject qUI;
    private string[] q;
    public Database data;
    public GameObject message;
    [HideInInspector]public bool hit;
    [HideInInspector] public bool attack;
    public int level;

    //Question bank created on Parser.Run
    List<string> qType = new List<string>();
    List<string> question = new List<string>();
    List<string> cAnswer = new List<string>();
    List<string> iAnswer1 = new List<string>();
    List<string> iAnswer2 = new List<string>();
    List<string> iAnswer3 = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("level");
    }

    //Creates the question bank in lists. Each Index in each list go together.
    //Input must be a string in the form   string ex = @"fullpath"
    public void Qpop()
    {
        string path = Application.streamingAssetsPath + "/Questions.csv";
        using (var reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(':');

                qType.Add(values[0]);
                question.Add(values[1]);
                cAnswer.Add(values[2]);
                iAnswer1.Add(values[3]);
                iAnswer2.Add(values[4]);
                iAnswer3.Add(values[5]);
            }
        }       

        if(level == 1)
        {
            q = this.GetQuestion(UnityEngine.Random.Range(0, 6));
        }
        else
        {
            q = this.GetQuestion(UnityEngine.Random.Range(0, 3));
        }
        
        qtext.text = q[0].Replace("@", Environment.NewLine);

        Boolean flag = true;
        int count = 0;
        int[] check = new int[]{ 0, 0, 0, 0 };
        while (flag) {
            System.Random ran = new System.Random();
            int k = ran.Next(4);
            if (check[k] == 0)
            {
                check[k] = 1;
                if (count == 0) { a1text.text = q[k + 1]; }
                else if(count == 1) { a2text.text = q[k + 1]; }
                else if(count == 2) { a3text.text = q[k + 1]; }
                else { a4text.text = q[k + 1]; }
                count++;
                if(check[0] == 1 && check[1] == 1 && check[2] == 1 && check[3] == 1)
                {
                    flag = false;
                }
            }           
        }
        
        qUI.SetActive(true);

        return;
    }

    public void Acheck(Text t)
    {
        if (t.text.Equals(q[1]))
        {
            message.GetComponent<Text>().text =  t.text + " is correct";
            message.SetActive(true);
            StartCoroutine(MessageTimer(message, .5f, qUI));
            if (level == 1) 
            { 
                data.currUser.qstCrctBeginner ++;
                data.currUser.progressBeginner += 2.5f;
                if (data.currUser.progressBeginner >= 100)
                {
                    data.currUser.progressBeginner = 100;
                }
            }
            else if (level == 2)
            {
                data.currUser.qstCrctIntermediate++;
                data.currUser.progressIntermediate += 2.5f;
                if (data.currUser.progressIntermediate >= 100)
                {
                    data.currUser.progressIntermediate = 100;
                }
            }
            else if (level == 3)
            {
                data.currUser.qstCrctAdvanced++;
                data.currUser.progressAdvanced += 2.5f;
                if (data.currUser.progressAdvanced >= 100)
                {
                    data.currUser.progressAdvanced = 100;
                }
            }
            
            hit = true;
            attack = true;
            data.users[data.name] = data.currUser;
            return;
        }
        else
        {
            message.GetComponent<Text>().text = t.text + " is incorrect " +q[1]+ " is correct";
            message.SetActive(true);
            StartCoroutine(MessageTimer(message, 1f, qUI));
            if (level == 1)
            {
                data.currUser.qstWrgBeginner++;
                data.currUser.progressBeginner -= 1f;
                if(data.currUser.progressBeginner <= 0) 
                {
                    data.currUser.progressBeginner = 0;
                }
            }
            else if (level == 2)
            {
                data.currUser.qstWrgIntermediate++;
                data.currUser.progressIntermediate -= 1f;
                if (data.currUser.progressIntermediate <= 0)
                {
                    data.currUser.progressIntermediate = 0;
                }
            }
            else if (level == 3)
            {
                data.currUser.qstWrgAdvanced++;
                data.currUser.progressAdvanced -= 1f;
                if (data.currUser.progressAdvanced <= 0)
                {
                    data.currUser.progressAdvanced = 0;
                }
            }
            hit = false;
            attack = true;
            data.users[data.name] = data.currUser;
            return;
        }
    }

    

    //Input is an integer. Returns question at that index in the form of a string array.
    //Index 0 is the question. Index 1 is the correct answer. Index 2,3, and 4 are the incorrect answers.
    public string[] GetQuestion(int n)
    {
        //the array to be filled with question and answers.
        string[] q = new string[5];

        switch (qType[n])
        {
            //case 'a' is a question that has varables. Max 4 varables. Answers are all generated with math equations.
            case "a":
                //generate random variables to be used with the question
                System.Random rnd = new System.Random();
                int a = rnd.Next(1, 5 * level); 
                int b = rnd.Next(1, 5 * level);
                int x = rnd.Next(1, 2 + level); 
                int y = rnd.Next(1, 5 * level);

                string ques = question[n];
                string ans = cAnswer[n];
                string i1 = iAnswer1[n];
                string i2 = iAnswer2[n];
                string i3 = iAnswer3[n];

                //replaces varables in questions and answers with the random values.
                ques = ques.Replace("{a}", a.ToString());
                ques = ques.Replace("{b}", b.ToString());
                ques = ques.Replace("{x}", x.ToString());
                ques = ques.Replace("{y}", y.ToString());

                ans = ans.Replace("{a}", a.ToString());
                ans = ans.Replace("{b}", b.ToString());
                ans = ans.Replace("{x}", x.ToString());
                ans = ans.Replace("{y}", y.ToString());

                i1 = i1.Replace("{a}", a.ToString());
                i1 = i1.Replace("{b}", b.ToString());
                i1 = i1.Replace("{x}", x.ToString());
                i1 = i1.Replace("{y}", y.ToString());

                i2 = i2.Replace("{a}", a.ToString());
                i2 = i2.Replace("{b}", b.ToString());
                i2 = i2.Replace("{x}", x.ToString());
                i2 = i2.Replace("{y}", y.ToString());

                i3 = i3.Replace("{a}", a.ToString());
                i3 = i3.Replace("{b}", b.ToString());
                i3 = i3.Replace("{x}", x.ToString());
                i3 = i3.Replace("{y}", y.ToString());

                //Method to compute string formulas
                StringToFormula stf = new StringToFormula();

                q[0] = ques;
                q[1] = stf.Eval(ans).ToString();
                q[2] = stf.Eval(i1).ToString();
                q[3] = stf.Eval(i2).ToString();
                q[4] = stf.Eval(i3).ToString();

                break;
            //case b is questions without varables. Questions are presented as the are placed into txt file
            case "b":
                q[0] = question[n];
                q[1] = cAnswer[n];
                q[2] = iAnswer1[n];
                q[3] = iAnswer2[n];
                q[4] = iAnswer3[n];
                break;
            //when question type has not be coded.
            default:
                Console.WriteLine($"Incorrect Question Type: {qType[n]}");
                break;
        }

        return q;
    }

    public IEnumerator MessageTimer(GameObject message, float t, GameObject qui)
    {
        yield return new WaitForSeconds(t);
        message.gameObject.SetActive(false);
        qui.gameObject.SetActive(false);
    }

    //publicly avalible code found to evaluate math expressions from strings.
    public class StringToFormula
    {
        private string[] _operators = { "-", "+", "/", "*", "^" };
        private Func<double, double, double>[] _operations = {
        (a1, a2) => a1 - a2,
        (a1, a2) => a1 + a2,
        (a1, a2) => a1 / a2,
        (a1, a2) => a1 * a2,
        (a1, a2) => Math.Pow(a1, a2)
    };

        public double Eval(string expression)
        {
            List<string> tokens = getTokens(expression);
            Stack<double> operandStack = new Stack<double>();
            Stack<string> operatorStack = new Stack<string>();
            int tokenIndex = 0;

            while (tokenIndex < tokens.Count)
            {
                string token = tokens[tokenIndex];
                if (token == "(")
                {
                    string subExpr = getSubExpression(tokens, ref tokenIndex);
                    operandStack.Push(Eval(subExpr));
                    continue;
                }
                if (token == ")")
                {
                    throw new ArgumentException("Mis-matched parentheses in expression");
                }
                //If this is an operator  
                if (Array.IndexOf(_operators, token) >= 0)
                {
                    while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
                    {
                        string op = operatorStack.Pop();
                        double arg2 = operandStack.Pop();
                        double arg1 = operandStack.Pop();
                        operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                    }
                    operatorStack.Push(token);
                }
                else
                {
                    operandStack.Push(double.Parse(token));
                }
                tokenIndex += 1;
            }

            while (operatorStack.Count > 0)
            {
                string op = operatorStack.Pop();
                double arg2 = operandStack.Pop();
                double arg1 = operandStack.Pop();
                operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
            }
            return operandStack.Pop();
        }

        private string getSubExpression(List<string> tokens, ref int index)
        {
            StringBuilder subExpr = new StringBuilder();
            int parenlevels = 1;
            index += 1;
            while (index < tokens.Count && parenlevels > 0)
            {
                string token = tokens[index];
                if (tokens[index] == "(")
                {
                    parenlevels += 1;
                }

                if (tokens[index] == ")")
                {
                    parenlevels -= 1;
                }

                if (parenlevels > 0)
                {
                    subExpr.Append(token);
                }

                index += 1;
            }

            if ((parenlevels > 0))
            {
                throw new ArgumentException("Mis-matched parentheses in expression");
            }
            return subExpr.ToString();
        }

        private List<string> getTokens(string expression)
        {
            string operators = "()^*/+-";
            List<string> tokens = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (char c in expression.Replace(" ", string.Empty))
            {
                if (operators.IndexOf(c) >= 0)
                {
                    if ((sb.Length > 0))
                    {
                        tokens.Add(sb.ToString());
                        sb.Length = 0;
                    }
                    tokens.Add(c.ToString());
                }
                else
                {
                    sb.Append(c);
                }
            }

            if ((sb.Length > 0))
            {
                tokens.Add(sb.ToString());
            }
            return tokens;
        }
    }
}

