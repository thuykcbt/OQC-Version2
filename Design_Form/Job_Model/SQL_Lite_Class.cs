using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Form.Job_Model
{
    public class SQL_Lite_Class
    {
        private readonly string databasePath;
        public SQL_Lite_Class(string databasePath)
        {
            this.databasePath = databasePath;
            InitializeDatabase();
        }
        private SQLiteConnection GetConnection()
        {
                return new SQLiteConnection($"Data Source={databasePath};Version=3;");
        }
        public void InitializeDatabase()
        {
			using (SQLiteConnection conn = GetConnection())
			{
				conn.Open();
				string sql = @"
        CREATE TABLE IF NOT EXISTS Board (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Barcode TEXT UNIQUE,
            TimeStamp TEXT,
            Result TEXT
        );

        CREATE TABLE IF NOT EXISTS Component (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            BoardId INTEGER,
            ComponentName TEXT,
            Result TEXT,
            NgCode TEXT,
            ImagePath TEXT,
			UNIQUE(BoardId, ComponentName),
            FOREIGN KEY (BoardId) REFERENCES Board(Id)
        );
        ";

				using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}
       
        public int InsertProduct(string barcode,string result)
        {
			using (SQLiteConnection conn = GetConnection())
			{
				conn.Open();
				string sql = @"
        INSERT OR IGNORE INTO Board (Barcode, TimeStamp, Result)
        VALUES (@Barcode, @Time, @Result);";

				using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Barcode", barcode);
					cmd.Parameters.AddWithValue("@Time",
						DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
					cmd.Parameters.AddWithValue("@Result", result);
					cmd.ExecuteNonQuery();
				}
				string sqlSelect = "SELECT Id FROM Board WHERE Barcode = @Barcode";
				using (SQLiteCommand cmd = new SQLiteCommand(sqlSelect, conn))
				{
					cmd.Parameters.AddWithValue("@Barcode", barcode);
					return Convert.ToInt32(cmd.ExecuteScalar());
				}
			}
		}
		public void InsertComponent(
	                                int boardId,
	                                string componentName,
	                                bool isOK,
	                                string ngCode,
	                                string imagePath)
		{
			using (SQLiteConnection conn = GetConnection())
			{
				conn.Open();
				string sql = @"
							INSERT INTO Component
							(BoardId, ComponentName, Result, NgCode, ImagePath)
							VALUES
							(@BoardId, @Name, @Result, @NgCode, @ImagePath)
							ON CONFLICT(BoardId, ComponentName)
							DO UPDATE SET
								Result = excluded.Result,
								NgCode = excluded.NgCode,
								ImagePath = excluded.ImagePath;
							";

				using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@BoardId", boardId);
					cmd.Parameters.AddWithValue("@Name", componentName);
					cmd.Parameters.AddWithValue("@Result", isOK ? "OK" : "NG");
					cmd.Parameters.AddWithValue("@NgCode", isOK ? null  : ngCode);
					cmd.Parameters.AddWithValue("@ImagePath", isOK ? null : imagePath);
					cmd.ExecuteNonQuery();
				}
			}
		}
		
		public void UpdateBoardResult(int boardId)
		{
			using (SQLiteConnection conn = GetConnection())
			{
				conn.Open();
				string sql = @"
                                UPDATE Board
                                SET Result = 
                                    CASE 
                                        WHEN EXISTS (
                                            SELECT 1 FROM Component 
                                            WHERE BoardId = @Id AND Result = 'NG'
                                        )
                                        THEN 'NG'
                                        ELSE 'OK'
                                    END
                                WHERE Id = @Id;
                                ";

				using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Id", boardId);
					cmd.ExecuteNonQuery();
				}
			}
		}
		public List<ComponentResult> GetComponents(int boardId)
		{
			List<ComponentResult> list = new List<ComponentResult>();

			using (SQLiteConnection conn = GetConnection())
			{
				conn.Open();
				string sql = "SELECT * FROM Component WHERE BoardId = @Id";

				using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Id", boardId);

					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							list.Add(new ComponentResult
							{
								BoardId = boardId,
								ComponentName = reader["ComponentName"].ToString(),
								Result =(bool)reader["Result"],
								NgCode = reader["NgCode"]?.ToString(),
								ImagePath = reader["ImagePath"]?.ToString()
							});
						}
					}
				}
			}

			return list;
		}

	}
	
}
 

