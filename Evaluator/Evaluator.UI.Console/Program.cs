﻿using Evaluator.core;
var infix1 = "4*5/(4+6)";
var result1 = MyEvaluator.Evaluate(infix1);
Console.WriteLine($"{infix1} = {result1}");
var infix2 = "4*(5+6-(8/2^3)-7)-1";
var result2 = MyEvaluator.Evaluate(infix2);
Console.WriteLine($"{infix2} = {result2}");