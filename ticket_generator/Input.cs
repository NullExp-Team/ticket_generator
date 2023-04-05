﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Words.NET;

namespace ticket_generator
{
    public static class Import
    {

        public static void ImportStudents(string path)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.doc; *.docx)|*.doc; *.docx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
            }

            List<string> students = new List<string>();
            List<string> newstudents = new List<string>();
            var document = DocX.Load(path);

            StringBuilder fullString = new StringBuilder();
            for (int i = 0; i < document.Paragraphs.Count; i++)
            {
                fullString.Append(document.Paragraphs[i].Text + '\n');
            }
            StringBuilder clearedString = new StringBuilder();
            char[] chars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ')', '(', '*', '-', '{', '}', '[', ']', '?', '=', '+', '-', '_', ',', '.' };
            for (int i = 0; i < fullString.Length; i++)
            {
                if (!chars.Contains(fullString[i]))
                    clearedString.Append(fullString[i]);
            }
        }

    //    public static List<int> GetTaskTypesFromString(string str)
    //    {
    //        List<int> result = new List<int>();

    //        char[] chars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '-', ' ' };

    //        for (int i = 0; i < str.Length; i++)
    //        {
    //            if (!chars.Contains(str[i]))
    //                throw new FormatException("GetTaskTypesFromString " + str);
    //        }

    //        string clearedString = str.Replace(" ", "");
    //        string[] splittedString = clearedString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

    //        for (int i = 0; i < splittedString.Length; i++)
    //        {
    //            if (splittedString[i].Contains('-'))
    //            {
    //                string[] splittedString2 = splittedString[i].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
    //                if (splittedString2.Length == 2)
    //                {
    //                    int from = Convert.ToInt32(splittedString2[0]), to = Convert.ToInt32(splittedString2[1]);
    //                    for (int j = from; j <= to; j++)
    //                    {
    //                        result.Add(j);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                int type = Convert.ToInt32(splittedString[i]);
    //                result.Add(type);
    //            }
    //        }

    //        result = result.Distinct().ToList();
    //        result.Sort();
    //        return result;
    //    }
    //}
}
