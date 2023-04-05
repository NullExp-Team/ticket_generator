using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

abstract class GeneratorsTask : IComparable<GeneratorsTask>
{
    public string text;
    public int difficulty;

    public GeneratorsTask(string text, int dif)
    {
        this.text = text;
        this.difficulty = dif;
    }

    public int CompareTo(GeneratorsTask other)
    {
        return this.difficulty > other.difficulty ||
                this.difficulty == other.difficulty && this.difficulty > other.difficulty
            ? 1
            : this.difficulty == other.difficulty && this.difficulty == other.difficulty
                ? 0
                : -1;
    }
}

class Teority : GeneratorsTask
{
    public Teority(string text, int dif) : base(text, dif) { }
}

class Practis : GeneratorsTask
{
    public Practis(string text, int dif) : base(text, dif) { }
}

class TypeVariablesNode : IComparable<TypeVariablesNode>
{
    public double difficulty;
    public int index;

    public TypeVariablesNode(int index, double dif)
    {
        this.difficulty = dif;
        this.index = index;
    }

    public int CompareTo(TypeVariablesNode other)
    {
        return this.difficulty > other.difficulty ||
                this.difficulty == other.difficulty && this.index > other.index
            ? 1
            : this.difficulty == other.difficulty && this.index == other.index
                ? 0
                : -1;
    }
}

class TypeVariables
{
    public int count;
    public List<GeneratorsTask> list;
    public List<TypeVariablesNode> tree;
    public int shift;

    public TypeVariables(int count, List<GeneratorsTask> list)
    {
        this.count = count;
        this.list = list;
        this.shift = -1;
        rebuildTree();

    }

    public void rebuildTree()
    {
        this.tree = new List<TypeVariablesNode>();
        for (int i = 0; i < this.list.Count; i++)
        {
            this.tree.Add(new TypeVariablesNode(i, this.list[i].difficulty));
        }
        this.tree.Sort();
        shift++;
    }
}

class Algorith
{
    public void Main()
    {
        List<GeneratorsTask> teorityList = new List<GeneratorsTask>{
          new Teority("1", 1),
          new Teority("3", 3),
          new Teority("4", 4),
          new Teority("5", 5),
          new Teority("2", 2)
        };
        List<GeneratorsTask> practisList = new List<GeneratorsTask>{
          new Practis("44", 4),
          new Practis("11", 1),
          new Practis("22", 2),
          new Practis("55", 5),
          new Practis("33", 3),
          new Practis("66", 6),
          new Practis("77", 7)
        };

        int teorityCount = 3;
        int practisCount = 2;
        int needDifficulty = 15;
        int variantsCount = 300;
        // конец ввода

        List<TypeVariables> types = new List<TypeVariables> {
          new TypeVariables(
            count: teorityCount,
            list: teorityList
          ),
          new TypeVariables(
            count: practisCount,
            list: practisList
          )
        };
        List<List<GeneratorsTask>> variants = new List<List<GeneratorsTask>>(variantsCount);
        for (int i = 0; i < variantsCount; i++)
        {
            variants.Add(new List<GeneratorsTask>());
        }

            int savePoint = 1;
        for (int i = 0; i < variantsCount; i++)
        {
            int lastTaskCount = 0;
            foreach (TypeVariables x in types)
            {
                lastTaskCount += x.count;
            }
            int nowDifficulty = 0;
            for (int k = 0; k < types.Count; k++)
            {
                for (int j = 0; j < types[k].count; j++)
                {
                    int indexOfTask;
                    if (j == 0 && types[k].shift > 0)
                    {
                        indexOfTask = new Random(DateTime.Now.Millisecond % 100 + i).Next(types[k].tree.Count);
                    }
                    else
                    {
                        double median = Convert.ToDouble(needDifficulty - nowDifficulty) / lastTaskCount;


                        indexOfTask = types[k].tree.BinarySearch(new TypeVariablesNode(-1, median));
                        if (indexOfTask < 0)
                        {
                            indexOfTask = ~indexOfTask;
                        }
                        bool substract = indexOfTask == types[k].tree.Count ||
                            indexOfTask - 1 > 0 &&
                                Math.Abs(types[k].tree[indexOfTask].difficulty - median) >
                                    Math.Abs(types[k].tree[indexOfTask - 1].difficulty - median);

                        indexOfTask = substract ? indexOfTask - 1 : indexOfTask;
                    }
                    variants[i].Add(types[k].list[types[k].tree[indexOfTask].index]);
                    nowDifficulty += variants[i].Last().difficulty;
                    lastTaskCount--;
                    types[k].tree.RemoveAt(indexOfTask);

                    if (types[k].tree.Count == 0)
                    {
                        types[k].rebuildTree();
                        types[k].shift++;
                    }
                }
            }

            if (Math.Abs(nowDifficulty - needDifficulty) >= 2 && i > savePoint)
            {
                for (int k = 0; k < types.Count; k++)
                {
                    types[k].rebuildTree();
                }
                variants[i].Clear();
                savePoint++;
                i--;
            }
        }

        for (int i = 0; i < variants.Count; i++)
        {
            int tmp = 0;
            foreach (var x in variants[i])
            {
                tmp += x.difficulty;
            }
            Console.WriteLine(tmp- needDifficulty);
        }

        HashSet<string> setic = new HashSet<string>();
        for (int i = 0; i < variants.Count; i++)
        {
            variants[i].Sort();

            string tmp = "";
            foreach (var x in variants[i])
            {
                tmp += x.text+ "|";
            }

            setic.Add(tmp);
        }
        Console.WriteLine("");
        Console.WriteLine(setic.Count);

    }

}
