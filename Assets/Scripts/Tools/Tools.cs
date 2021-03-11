using UnityEngine;
using System.IO;
namespace Tools
{

    public class Tool
    {
        
        //输入当前分数，并返回排行榜
        public int[] Rank(int score)
        {
            string path = Application.persistentDataPath + "/Rank.txt";
            int[] rank = new int[10];
            int i = 0;
            FileStream F = new FileStream(path, FileMode.OpenOrCreate);
            F.Close();
            StreamReader streamReader = new StreamReader(path);
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                int.TryParse(line, out rank[i]);
                i++;
            }
            streamReader.Close();
            for(i = 0; i < 10; i++)
            {
                if(rank[i] < score)
                {
                    for(int j = 9; j > i; j--)
                    {
                        rank[j] = rank[j - 1];
                    }
                    rank[i] = score;
                    break;
                }
            }
            StreamWriter streamWriter = new StreamWriter(path);
            for (i = 0; i < 10; i++)
            {
                streamWriter.WriteLine(rank[i]);
            }
            streamWriter.Close();
            return rank;
        }

        //直接返回排行榜
        public int[] Rank()
        {
            string path = Application.persistentDataPath + "/Rank.txt";
            int[] rank = new int[10];
            int i = 0;
            FileStream F = new FileStream(path, FileMode.OpenOrCreate);
            F.Close();
            StreamReader streamReader = new StreamReader(path);
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                int.TryParse(line, out rank[i]);
                i++;
            }
            streamReader.Close();
            return rank;
        }
        public int IntervalTime(int start, int end)
        {
            int interval = 0;
            if (end >= start)
            {
                interval = end - start;
            }
            else
            {
                interval = end + 1000 - start;
            }
            return interval;
        }

        public int RandomType(int[] types)
        {
            int max = 1;
            for(int i = 0; i < types.Length; i++)
            {
                if(types[i] != 0)
                {
                    if(i > max && i < 6)
                    {
                        max = i;
                    }
                }
            }
            return Random.Range(0,max);
        }
    }
}