using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;

namespace ticket_generator
{

    public enum TaskType
    {
        Theory,
        Practice,
    }

    public class GeneratorsTask : IComparable<GeneratorsTask>
    {
        public int id;
        public int difficulty;
        public TaskType type;
        public List<TaskText> taskTexts;

        public GeneratorsTask(int id, int difficulty, TaskType type, List<TaskText> text)
        {
            this.id = id;
            this.difficulty = difficulty;
            this.type = type;
            this.taskTexts = text;
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

    public class TaskText
    {
        public List<Picture> pictures;
        public Paragraph paragraph;

        public TaskText(List<Picture> images, Paragraph paragraph)
        {
            this.pictures = images;
            this.paragraph = paragraph;
        }
    }

    public class Ticket
    {
        public int number;
        public List<GeneratorsTask> tasks;

        public Ticket(int number, List<GeneratorsTask> tasks)
        {
            this.number = number;
            this.tasks = tasks;
        }
    }

    public class ExamTest
    {
        public string title;
        public List<Ticket> tickets;

        public ExamTest(string title, List<Ticket> tickets)
        {
            this.title = title;
            this.tickets = tickets;
        }
    }
}
