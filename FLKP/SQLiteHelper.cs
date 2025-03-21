using Microsoft.Data.Sqlite;
namespace FLKP
{
    class SQLiteHelper
    {
        string conn_string = $"Data Source={System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase}FLKP.db";

        public SQLiteHelper()
        {
            CreateTable();
        }


        void CreateTable()
        {
            using (SqliteConnection m_dbConnection = new SqliteConnection(conn_string))
            {
                m_dbConnection.Open();
                using (var transaction = m_dbConnection.BeginTransaction())
                {
                    var cmd = m_dbConnection.CreateCommand();
                    cmd.CommandText = "CREATE TABLE FLKP (date INTEGER PRIMARY KEY, checks INTEGER);";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }

                }
            }
        }

        public void InsertNewCheck(int date)
        {
            using (SqliteConnection m_dbConnection = new SqliteConnection(conn_string))
            {
                m_dbConnection.Open();
                using (var transaction = m_dbConnection.BeginTransaction())
                {
                    var cmd = m_dbConnection.CreateCommand();
                    cmd.CommandText =
    @"
        INSERT INTO FLKP ( date, checks) VALUES ( $date, 0 );
    ";
                    cmd.Parameters.AddWithValue("$date", date);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }

                }
            }
        }

        public void UpdateCheck(int date, long checks)
        {
            using (SqliteConnection m_dbConnection = new SqliteConnection(conn_string))
            {
                m_dbConnection.Open();
                using (var transaction = m_dbConnection.BeginTransaction())
                {
                    var cmd = m_dbConnection.CreateCommand();
                    cmd.CommandText =
    @"
UPDATE FLKP SET checks = $checks WHERE date = $date; 
    ";
                    cmd.Parameters.AddWithValue("$checks", checks);
                    cmd.Parameters.AddWithValue("$date", date);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }

                }
            }
        }

        public long ReadChecks(int date)
        {
            using (SqliteConnection m_dbConnection = new SqliteConnection(conn_string))
            {
                m_dbConnection.Open();
                using (var transaction = m_dbConnection.BeginTransaction())
                {
                    var cmd = m_dbConnection.CreateCommand();
                    cmd.CommandText =
        @"
        SELECT checks FROM FLKP WHERE date = $date
    ";
                    cmd.Parameters.AddWithValue("$date", date);

                    using var reader = cmd.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            var checks = reader.GetInt64(0);
                            return checks;
                        }
                    }
                    m_dbConnection.Close();
                    InsertNewCheck(date);
                    return 0;
                }
            }
        }
    }
}
