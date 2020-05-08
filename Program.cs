using System;

namespace Equations_Solver
{
	class Program
	{
		// Methods /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		static void Main(string[] args)
		{
			while (true)
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.Write("Enter equation: ");
				Console.ForegroundColor = ConsoleColor.Gray;
				string equationString = Console.ReadLine();
				try
				{
					Equation equation = new Equation(equationString);
					string variableString = "0";
					bool isThereAVariableInTheEquation = false;
					for (int i = 0; i < equationString.Length; i++)
					{
						if (equationString[i] == 'x')
						{
							isThereAVariableInTheEquation = true;
							break;
						}
					}
					if (isThereAVariableInTheEquation)
					{
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.Write("Enter variable: ");
						Console.ForegroundColor = ConsoleColor.Gray;
						variableString = Console.ReadLine();
					}
					if (variableString != null)
						if (!IsVariableValid(variableString))
							throw new Exception("Variable is not valid!");
					string trigonometricMethodOption = "0";
					bool areThereTrigonometricFunctions = false;
					for (int i = 0; i < equationString.Length - 2; i++)
					{
						if ((equationString[i] == 's' && equationString[i + 1] == 'i' && equationString[i + 2] == 'n') ||
							(equationString[i] == 'c' && equationString[i + 1] == 'o' && equationString[i + 2] == 's') ||
							(equationString[i] == 't' && equationString[i + 1] == 'a' && equationString[i + 2] == 'n'))
						{
							areThereTrigonometricFunctions = true;
							break;
						}
					}
					if (areThereTrigonometricFunctions)
					{
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.Write("Enter trigonometric method option ('0' = radians, '1' = degrees): ");
						Console.ForegroundColor = ConsoleColor.Gray;
						trigonometricMethodOption = Console.ReadLine();
					}
					if (trigonometricMethodOption != null)
						if (!trigonometricMethodOption.Equals("0") && !trigonometricMethodOption.Equals("1"))
							throw new Exception("The specified trigonometric method option is not valid!");
					Console.WriteLine("------------------------------------------------------------------------------");
					try
					{
						double variable;
						if (variableString.Equals("∞"))
							variable = double.PositiveInfinity;
						else if (variableString.Equals("-∞"))
							variable = double.NegativeInfinity;
						else if (variableString.Equals("π"))
							variable = Math.PI;
						else if (variableString.Equals("-π"))
							variable = -Math.PI;
						else if (variableString.Equals("e"))
							variable = Math.E;
						else if (variableString.Equals("-e"))
							variable = -Math.E;
						else
							variable = Convert.ToDouble(variableString);
						double equationResult = equation.Solve(variable, trigonometricMethodOption.Equals("1"), true);
						Console.WriteLine("------------------------------------------------------------------------------");
						Console.ForegroundColor = ConsoleColor.Black;
						Console.BackgroundColor = ConsoleColor.Green;
						Console.Write("Result:");
						Console.ResetColor();
						Console.ForegroundColor = ConsoleColor.Gray;
						if (double.IsPositiveInfinity(equationResult))
							Console.WriteLine(" Infinity");
						else if (double.IsNegativeInfinity(equationResult))
							Console.WriteLine(" -Infinity");
						else
							Console.WriteLine(" " + equationResult);
					}
					catch (Exception e)
					{
						Console.WriteLine("------------------------------------------------------------------------------");
						Console.ForegroundColor = ConsoleColor.Black;
						Console.BackgroundColor = ConsoleColor.Red;
						Console.Write("Result:");
						Console.ResetColor();
						Console.ForegroundColor = ConsoleColor.Gray;
						Console.WriteLine(" NaN");
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Error: " + e.Message);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("------------------------------------------------------------------------------");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: " + e.Message);
					Console.ResetColor();
				}
				Console.ResetColor();
				Console.WriteLine();
				Console.WriteLine();
			}
		}
		private static bool IsVariableValid(string variableString)
		{
			if (variableString.Length == 0)
				return false;
			else if (variableString.Length == 1)
			{
				if (!(((variableString[0] >= '0' && variableString[0] <= '9') || variableString[0] == '.')))
					if (!(variableString[0] == 'π' || variableString[0] == 'e' || variableString[0] == '∞'))
						return false;
			}
			else if (variableString.Length == 2)
			{
				if (!(((variableString[0] >= '0' && variableString[0] <= '9') || variableString[0] == '.') && ((variableString[1] >= '0' && variableString[1] <= '9') || variableString[1] == '.')))
					if (!((variableString[1] >= '0' && variableString[1] <= '9') && variableString[0] == '-'))
						if (!((variableString[1] == 'π' || variableString[1] == 'e' || variableString[1] == '∞') && variableString[0] == '-'))
							return false;
			}
			else
				for (int i = 0; i < variableString.Length; i++)
					if (!(((variableString[i] >= '0' && variableString[i] <= '9') || variableString[i] == '.') || (variableString[i] == '-' && i == 0)))
						return false;
			int decimalPointsCount = 0;
			for (int i = 0; i < variableString.Length; i++)
				if (variableString[i] == '.')
					decimalPointsCount++;
			if (decimalPointsCount > 1)
				return false;
			return true;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	}
}
