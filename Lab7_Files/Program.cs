

using System;
using System.IO;
using System.Text;

namespace Lab7_Files
{
    public class Program
    {
        private static void Part1()
        {
            FileStream stream = new FileStream("../../Pokemon.csv", FileMode.Open);
            byte[] fileData = new byte[1024];
            stream.Read(fileData, 0, fileData.Length);
            string fileDataChars = Encoding.UTF8.GetString(fileData);
            Console.WriteLine(fileDataChars);
            Console.Read();
            int index = fileDataChars.IndexOf("Squirtle");
            stream.Seek(index, SeekOrigin.Begin);
            byte[] dataToWrite = Encoding.UTF8.GetBytes("SQUIRTLE");
            stream.Write(dataToWrite, 0, dataToWrite.Length);
            Console.Read();
            stream.Close();
        }

        private static void Part2()
        {
            using (var stream = new FileStream("../../Pokemon.csv",FileMode.Open))
            using (var reader = new StreamReader("../../Pokemon.csv"))
            using (var writer = new StreamWriter(stream))
            {
                string data = reader.ReadToEnd();
                int index = data.IndexOf("SQUIRTLE");
                stream.Seek(index, SeekOrigin.Begin);
                writer.Write("squirtle");
                writer.Flush();
            }
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Part1();
            }
            else
            {
                Part2();
            }
        }
    }
}
