using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDeDatos
{
    public class BeerDB : DB
    {
        public BeerDB(string server, string db, bool windowsAuth) :
            base(server, db, windowsAuth)
        { }

        public List<Beer> GetAll()
        {
            float miVariable = 10.5f;
            Connect();
            List<Beer> beers = new List<Beer>();
            string query = "SELECT BeerID, Name, BrandID FROM Beer";
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int brandId = reader.GetInt32(2);
                beers.Add(new Beer(id, name, brandId));
            }

            Close();

            return beers;
        }

        public Beer Get(int id)
        {
            Connect();
            Beer beer = null;

            string query = "SELECT BeerID, Name, BrandID FROM Beer " +
                "WHERE BeerID = @id";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string name = reader.GetString(1);
                int brandId = reader.GetInt32(2);
                beer = new Beer(id, name, brandId);
            }

            Close();

            return beer;
        }

        public void Add(Beer beer)
        {
            Connect();

            string query = "INSERT INTO Beer(Name, BrandID) " +
                "VALUES(@name, @brandId)";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@name", beer.Name);
            command.Parameters.AddWithValue("@brandId", beer.BrandID);
            command.ExecuteNonQuery();

            Close();
        }

        public void Edit(Beer beer)
        {
            Connect();

            string query = "UPDATE beer SET Name=@name, BrandID=@brandId " +
                "WHERE BeerID=@id";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@name", beer.Name);
            command.Parameters.AddWithValue("@brandId", beer.BrandID);
            command.Parameters.AddWithValue("@id", beer.BeerID);
            command.ExecuteNonQuery();

            Close();
        }

        public void Delete(int id)
        {
            Connect();

            string query = "DELETE FROM Beer WHERE BeerID = @id";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            Close();
        }
    }
}
