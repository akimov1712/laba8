using System.Diagnostics;

internal class Program{
    
    public static void Main(string[] args)
    {

        Test("ABABABAB", "ABAB");
        Test("abcabcabcabc", "abc");
        Test("ABCDEABCD", "ABCD");
        Test("ABCDEABCD", "ABCE");
        Test("ABCDEFG", "BCDEFG");
        Test("HFGHSDFSDFRBGFBNFGHFGNTHFGGHLFGJHLHKFGJHLKFGJHLKFGJHLFGJHLKGFJHLKFG", "JHLKFG");

    }
    
    static int SimpleSearch(string text, string pattern, out int comparisons)
    {
        int n = text.Length;
        int m = pattern.Length;
        comparisons = 0;

        for (int i = 0; i <= n - m; i++)
        {
            int j;
            for (j = 0; j < m; j++)
            {
                comparisons++;
                if (text[i + j] != pattern[j])
                {
                    break;
                }
            }
            if (j == m)
            {
                return i;
            }
        }
        return -1;
    }
    
    static int KMP(string text, string pattern, out int comparisons)
    {
        int n = text.Length;
        int m = pattern.Length;
        int[] pi = ComputePrefixFunction(pattern);
        int q = 0;
        comparisons = 0;

        for (int i = 0; i < n; i++)
        {
            while (q > 0 && pattern[q] != text[i])
            {
                q = pi[q - 1];
                comparisons++;
            }
            if (pattern[q] == text[i])
            {
                q++;
                comparisons++;
            }
            if (q == m)
            {
                return i - m + 1;
            }
        }
        return -1;
    }

    static int[] ComputePrefixFunction(string pattern)
    {
        int m = pattern.Length;
        int[] pi = new int[m];
        int k = 0;
        pi[0] = 0;

        for (int q = 1; q < m; q++)
        {
            while (k > 0 && pattern[k] != pattern[q])
            {
                k = pi[k - 1];
            }
            if (pattern[k] == pattern[q])
            {
                k++;
            }
            pi[q] = k;
        }
        return pi;
    }
    
    static int BM(string text, string pattern, out int comparisons)
    {
        int n = text.Length;
        int m = pattern.Length;
        int[] last = BuildLast(pattern);
        int i = m - 1;
        int j = m - 1;
        comparisons = 0;

        while (i < n && j >= 0)
        {
            if (text[i] == pattern[j])
            {
                i--;
                j--;
                comparisons++;
            }
            else
            {
                int l = last[text[i]];
                i = i + m - Math.Min(j, 1 + l);
                j = m - 1;
                comparisons++;
            }
        }
        if (j < 0)
        {
            return i + 1;
        }
        else
        {
            return -1;
        }
    }

    static int[] BuildLast(string pattern)
    {
        int[] last = new int[256];
        for (int i = 0; i < last.Length; i++)
        {
            last[i] = -1;
        }
        for (int i = 0; i < pattern.Length; i++)
        {
            last[pattern[i]] = i;
        }
        return last;
    }
    
    static void Test(string text, string pattern)
    {
        int comparisons, position;
        Stopwatch stopwatch = new Stopwatch();

        Console.WriteLine($"Text: \"{text}\"");
        Console.WriteLine($"Pattern: \"{pattern}\"");

        stopwatch.Start();
        position = KMP(text, pattern, out comparisons);
        stopwatch.Stop();
        if (position == -1)
        {
            Console.WriteLine("KMP: не найдено");
        }
        else
        {
            Console.WriteLine($"KMP: Найдено на позиции {position}, {stopwatch.Elapsed.TotalSeconds:0.000}:" +
                              $"{stopwatch.Elapsed.TotalMilliseconds % 1000:0.000} сек, {comparisons} сравнений");
        }

        stopwatch.Restart();
        position = BM(text, pattern, out comparisons);
        stopwatch.Stop();
        if (position == -1)
        {
            Console.WriteLine("BM: не найдено");
        }
        else
        {
            Console.WriteLine($"BM: Найдено на позиции {position}, {stopwatch.Elapsed.TotalSeconds:0.000}:" +
                              $"{stopwatch.Elapsed.TotalMilliseconds % 1000:0.000} сек, {comparisons} сравнений");
        }

        stopwatch.Restart();
        position = SimpleSearch(text, pattern, out comparisons);
        stopwatch.Stop();
        
        if (position == -1)
        {
            Console.WriteLine("Обычный поиск: не найдено");
        }
        else
        {
            Console.WriteLine($"Обычный поиск: Найдено на позиции {position}, {stopwatch.Elapsed.TotalSeconds:0.000}:" +
                              $"{stopwatch.Elapsed.TotalMilliseconds % 1000:0.000} сек, {comparisons} сравнений \n");
        }
    }
    
}