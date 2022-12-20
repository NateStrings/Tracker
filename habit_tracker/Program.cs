using System;
using Microsoft.Data.Sqlite;

namespace habit_tracker
{
    class Program
    {
        public static Action<string> cw = Console.WriteLine; // Good Stuff! I'll start using this myself
        //public static Action<string> cr = Console.ReadLine;
        static void Main(string[] args)
        {
            sqlConnector(@"CREATE TABLE IF NOT EXISTS daily_items (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER,
                        Item TEXT)");

            
            GetUserInput();
        }
        
        // Inconsistent method name
        static void sqlConnector(string SQLText)
        {
            using (var conn = new SqliteConnection(@"Data Source=habit-tracking.db")) 
            {
                            conn.Open();
                            using (var tableCmd = conn.CreateCommand())
                            {
                                tableCmd.CommandText = SQLText;

                                tableCmd.ExecuteNonQuery();
                            }
            }
        }
        static void GetUserInput()
        {
            Console.Clear();
            // Separate These 
            bool closeApp = false;
            while (closeApp == false)
            {
                cw("\n\nMain Menu");
                cw("\nWhat would you like to do?");
                cw("\nType 0 to Close Application");
                cw("Type 1 to View All Records." );
                cw("Type 2 to Insert Record");
                cw("Type 3 to Delete Record");
                cw("Type 4 to Update Record.");
                cw("-----------------------------------------\n");

                string command = Console.ReadLine().Trim();

                switch(command)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        break;
                    // case "1":
                    //    GetAllRecord();
                    //    break;
                    case "2":
                        Insert();
                        break;
                    //case "3":
                      //  Delete();
                        //break;
                    //case "4":
                      //  Update();
                        //break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type an option from 0-4.\n");
                        break;
                }
            }
            
        }
        // private static void GetAllRecord()
        // {
        //     Console.Clear();
        //     using (var connection = new SqliteConnection(@"Data Source=habit-tracking.db"))
        //     {
        //         connection.Open();
        //         var tableCmd = connection.CreateCommand();
        //         tableCmd.CommandText = 
        //             "SELECT * FROM daily_items";

        //             List<DailyItems> tableData = new();
                    
        //             SqliteDataReader reader = tableCmd.ExecuteReader();

        //             if (reader.HasRows)
        //             {
        //                 while (reader.Read())
        //                 {
        //                     tableData.Add(
        //                         new DailyItems
        //                         {
        //                             Id = reader.GetInt32(0),
        //                             Date = DateTime
        //                         }
        //                     )
        //                 }
        //             } else
        //             {
        //                 Console.WriteLine("No Rows");
        //             }
                    
        //     }
        // }
        private static void Insert()
        {
                string date = GetDateInput();

                int quantity = GetNumberInput("\n\nPlease insert number of the item you are trying to record (no decimals)");

                string item = GetItemInput();

                sqlConnector($"INSERT INTO daily_items(date, quantity, item) VALUES('{date}', {quantity}, '{item}')");
        }
        internal static string GetItemInput()
        {
            Console.WriteLine("\n\nPlease insert the name of the item in plural form");

            string getItemInput = Console.ReadLine().ToLower().Trim();

            return getItemInput;
        }
        internal static string GetDateInput()
        {
            // returning directly without declaring a variable
            return DateTime.Now.ToString("MM-dd-yy");
        }
        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);
            
            int numberInput = Convert.ToInt32(Console.ReadLine());

            if(numberInput == 0) GetUserInput();

             return numberInput;
        }
        // private static void Delete()
        // {

        // }
        // private static void Update()
        // {

        // }
        
    }
}

// public class DailyItems
// {
//     public int Id {get; set;}

//     public DateTime Date { get; set;}

//     public int Quantity {get; set;}

//     public string Item {get; set;}
// }
