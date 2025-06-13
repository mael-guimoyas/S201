using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGrease.Activities;

namespace S201
{
    public class DataAccess
    {

        private static readonly DataAccess instance = new DataAccess();
        private readonly string connectionString = "Host=srv-peda-new;Port=5433;Username=negrima;Password=76Z4Yh;Database=DB_SAE_2.01;Options='-c search_path=sibilla'";
        private NpgsqlConnection connection;

        public static DataAccess Instance
        {
            get
            {
                return instance;
            }
        }

       
        private DataAccess()
        {

            try
            {
                connection = new NpgsqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        
        public NpgsqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }


            return connection;
        }

        public DataTable ExecuteSelect(NpgsqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection = GetConnection();
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return dataTable;
        }

        

        public int ExecuteInsert(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw;
            }
            return nb;
        }




        
        public int ExecuteSet(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            return nb;

        }

        

        //  Fermer la connexion 
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

}
