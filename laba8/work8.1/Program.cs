using System;
using System.IO;
using System.Diagnostics;

class Program
{
	static void Main(string[] args)
	{
		string[] paths = {
			"sorted_SelectionSort.dat",
			"sorted_BubbleSort.dat",
			"sorted_CocktailSort.dat",
			"sorted_InsertionSort.dat",
			"sorted_ShellSort.dat"
		};

		for (int i = 0; i < paths.Length; i++) {
			int[] arr = ReadArrayFromFile(paths[i]);
			int target = 1; // Целевой элемент для поиска

			Console.WriteLine($"    {paths[i]}");

			LinearSearch(arr, target);
			BinarySearch(arr, target);
			InterpolationSearch(arr, target);
		}
	}

	static void LinearSearch(int[] arr, int x)
	{
		int comparisons = 0;
		Stopwatch sw = new Stopwatch();
		sw.Start();
		for (int i = 0; i < arr.Length; i++)
		{
			comparisons++;
			if (arr[i] == x)
			{
				sw.Stop();
				Console.WriteLine($"Линейный поиск: Элемент найден на позиции {i}. Время работы: {sw.Elapsed.Seconds} секунд : {sw.Elapsed.Milliseconds} миллисекунд ({sw.Elapsed}). Количество сравнений: {comparisons}");
				return;
			}
		}
		sw.Stop();
		Console.WriteLine($"Линейный поиск: Элемент не найден. Время работы: {sw.Elapsed.Seconds} секунд : {sw.Elapsed.Milliseconds} миллисекунд ({sw.Elapsed}). Количество сравнений: {comparisons}");
	}

	static void BinarySearch(int[] arr, int x)
	{
		int comparisons = 0;
		Stopwatch sw = new Stopwatch();
		sw.Start();
		int left = 0;
		int right = arr.Length - 1;
		while (left <= right)
		{
			comparisons++;
			int mid = (left + right) / 2;
			if (arr[mid] == x)
			{
				sw.Stop();
				Console.WriteLine($"Бинарный поиск: Элемент найден на позиции {mid}. Время работы: {sw.Elapsed.Seconds} секунд : {sw.Elapsed.Milliseconds} миллисекунд ({sw.Elapsed}). Количество сравнений: {comparisons}");
				return;
			}
			else if (arr[mid] < x)
			{
				left = mid + 1;
			}
			else
			{
				right = mid - 1;
			}
		}
		sw.Stop();
		Console.WriteLine($"Бинарный поиск: Элемент не найден. Время работы: {sw.Elapsed.Seconds} секунд : {sw.Elapsed.Milliseconds} миллисекунд ({sw.Elapsed}). Количество сравнений: {comparisons}");
	}

	static void InterpolationSearch(int[] arr, int x)
	{
		int comparisons = 0;
		Stopwatch sw = new Stopwatch();
		sw.Start();
		int left = 0;
		int right = arr.Length - 1;
		while (left <= right && arr[left] <= x && arr[right] >= x)
		{
			comparisons++;
			int pos = left + ((x - arr[left]) * (right - left)) / (arr[right] - arr[left]);
			if (arr[pos] == x)
			{
				sw.Stop();
				Console.WriteLine($"Интерполяционный: Элемент найден на позиции {pos}. Время работы: {sw.Elapsed.Seconds} секунд : {sw.Elapsed.Milliseconds} миллисекунд ({sw.Elapsed}). Количество сравнений: {comparisons}");
				return;
			}
			else if (arr[pos] < x)
			{
				left = pos + 1;
			}
			else
			{
				right = pos - 1;
			}
		}
		sw.Stop();
		Console.WriteLine($"Интерполяционный: Элемент не найден. Время работы: {sw.Elapsed.Seconds} секунд : {sw.Elapsed.Milliseconds} миллисекунд ({sw.Elapsed}). Количество сравнений: {comparisons}");
	}

	public static int[] ReadArrayFromFile(string path)
	{
		var lines = File.ReadAllLines(path);
		var nums = new int[lines.Length];
		for (int i = 0; i < lines.Length; i++)
		{
			nums[i] = int.Parse(lines[i]);
		}
		return nums;
	}
}