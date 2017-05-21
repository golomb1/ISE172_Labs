using System;
using System.Data.SqlClient;
using System.IO;

namespace Lab8_SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fileStream = File.Open("Answers.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(fileStream);
            string connectionString = @"Data Source=ise172.ise.bgu.ac.il;Initial Catalog=MoviesDB;User ID=labuser;Password=wonsawheightfly";
            SqlConnection myConnection = new SqlConnection(connectionString);
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            // warming up
            SqlCommand myCommand = new SqlCommand(
                    @"SELECT [director_name]
                        From [dbo].[Movies]
                        Order by duration", myConnection);
            SqlDataReader reader = myCommand.ExecuteReader();
            reader.Read();
            Console.WriteLine("Wariming up: the director is " + reader["director_name"]);
            reader.Close();
            Console.Read();
            // Question 1
            writer.WriteLine("-- Answer 1 --");
            myCommand = new SqlCommand(
                    @"SELECT [director_name]
                        From [dbo].[Movies]
                        Where movie_title = 'Suicide Squad'", myConnection);
            reader = myCommand.ExecuteReader();
            reader.Read();
            writer.WriteLine("The director is: " + reader["director_name"]);
            reader.Close();
            // Question 2
            writer.WriteLine("-- Answer 2 --");
            myCommand = new SqlCommand(
                    @"SELECT [movie_title],[num_user_for_reviews]
                        From [dbo].[Movies]
                        Order by imdb_score DESC", myConnection);
            reader = myCommand.ExecuteReader();
            reader.Read();
            writer.WriteLine("The Movie name is: " + reader["movie_title"] + " and the number of reviewer is " + reader["num_user_for_reviews"]);
            reader.Close();

            // Question 3
            writer.WriteLine("-- Answer 3 --");
            myCommand = new SqlCommand(
                    @"SELECT [movie_title],[director_name]
                        From [dbo].[Movies]
                        Where title_year = 2016
                        Order by budget DESC", myConnection);
            reader = myCommand.ExecuteReader();
            reader.Read();
            writer.WriteLine("The Movie name is: " + reader["movie_title"] + " And it was directed by " + reader["director_name"]);
            reader.Close();

            // Question 4
            writer.WriteLine("-- Answer 4 --");
            myCommand = new SqlCommand(
                    @"SELECT [color],Count(*) as count
                        From [dbo].[Movies]
                        group by [color]", myConnection);
            reader = myCommand.ExecuteReader();
            reader.Read();
            if (reader["color"] == null)
            {
                reader.Read();
            }
            writer.Write("There are : " + reader["count"] + " Movies in " + reader["color"]);
            reader.Read();
            writer.WriteLine("and: " + reader["count"] + " Movies in " + reader["color"]);
            reader.Close();

            // Question 5
            writer.WriteLine("-- Answer 5 --");
            myCommand = new SqlCommand(
                    @"SELECT movie_title, num_voted_users, gross - budget as money
                        From [dbo].[Movies]
                        order by gross - budget DESC", myConnection);
            reader = myCommand.ExecuteReader();
            reader.Read();
            writer.WriteLine("The Movie name is: " + reader["movie_title"] + " the number of voted users are " + reader["num_voted_users"] + " and the diffrent between gross and budget is " + reader["money"]);
            reader.Close();
            writer.Flush();
            writer.Close();


            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
