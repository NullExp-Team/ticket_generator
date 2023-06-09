﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ticket_generator
{
    class Algorithm
    {
        class TypeVariablesNode : IComparable<TypeVariablesNode>
        {
            public double difficulty;
            public int index;

            public TypeVariablesNode(int index, double dif)
            {
                this.index = index;
                this.difficulty = dif;
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

        public ExamTest Compute(List<GeneratorsTask> tasks, int teorityCount, int practisCount, double needDifficulty, int variantsCount)
        {
            needDifficulty *= teorityCount + practisCount;
            List<GeneratorsTask> teorityList = tasks.Where((task) => task.type == TaskType.Theory).ToList();

            List<GeneratorsTask> practisList = tasks.Where((task) => task.type == TaskType.Practice).ToList(); ;

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

            int savePoint = 0;
            HashSet<string> unicVars = new HashSet<string>();
            bool tryToUnic = true;
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
                        if (j < types[k].shift)
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
                        }
                    }
                }

                if (Convert.ToDouble(Math.Abs(nowDifficulty - needDifficulty)/ (teorityCount + practisCount)) >= 1 && i > savePoint)
                {
                    for (int k = 0; k < types.Count; k++)
                    {
                        types[k].rebuildTree();
                    }
                    variants[i].Clear();
                    savePoint++;
                    i--;
                } else if (tryToUnic)
                {
                    variants[i].Sort();
                    string tmp = "";
                    foreach (var x in variants[i])
                    {
                        tmp += x.ToString() + "|";
                    }
                    if (unicVars.Contains(tmp))
                    {
                        tryToUnic = false;
                        variants[i].Clear();
                        i--;
                    } else
                    {
                        unicVars.Add(tmp);
                    }
                } else
                {
                    tryToUnic = true;
                }
            }

            for (int i = 0; i < variants.Count; i++)
            {
                int tmp = 0;
                foreach (var x in variants[i])
                {
                    tmp += x.difficulty;
                }
                Console.WriteLine(tmp - needDifficulty);
            }

            Console.WriteLine("");
            Console.WriteLine(unicVars.Count);

            ExamTest examTest = new ExamTest("❤️lovemaker❤️", new List<Ticket>());

            for (int i = 0; i < variants.Count; i++)
            {
                var theory = variants[i].Where((task) => task.type == TaskType.Theory).ToList();
                var practice = variants[i].Where((task) => task.type == TaskType.Practice).ToList();
                variants[i] = new List<GeneratorsTask>();
                variants[i].AddRange(theory);
                variants[i].AddRange(practice);
                examTest.tickets.Add(new Ticket(i + 1, variants[i]));
            }

            return examTest;
        }
    }
}