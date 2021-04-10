using System.Collections.Generic;
using System.Data.SqlClient;

namespace Pop_Data
{
    internal class DB
    {
        const string CONNECTION_STRING = @"Server=.\SQLEXPRESS01;Database=Population;Trusted_Connection=True";

        const string SELECT_CMD = "SELECT cityName, population FROM dbo.City";

        const string INSERT_CMD = "INSERT INTO City(cityName, population) VALUES (@CITYNAME, @POPULATION)";

        const string UPDATE_CMD = "UPDATE City SET population = @POPULATION WHERE cityName = @CITYNAME ";

        const string DELETE_CMD = "DELETE FROM CITY WHERE cityName = @CITYNAME";

        private static DB db;

        public static DB Instance
        {
            get
            {
                db ??= new DB();
                return db;
            }
        }

        private readonly SqlConnection conn;

        private DB()
        {
            conn = new SqlConnection(CONNECTION_STRING);
            conn.Open();
        }

        public bool Insert(City city)
        {
            using SqlCommand cmd = new SqlCommand(INSERT_CMD, conn);
            cmd.Parameters.AddWithValue("@CITYNAME", city.CityName);
            cmd.Parameters.AddWithValue("@POPULATION", city.Population);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(City city)
        {
            using SqlCommand cmd = new SqlCommand(UPDATE_CMD, conn);
            cmd.Parameters.AddWithValue("@CITYNAME", city.CityName);
            cmd.Parameters.AddWithValue("@POPULATION", city.Population);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(City city)
        {
            using SqlCommand cmd = new SqlCommand(DELETE_CMD, conn);
            cmd.Parameters.AddWithValue("@CITYNAME", city.CityName);

            return cmd.ExecuteNonQuery() > 0;
        }

        public List<City> Get()
        {
            List<City> cities = new List<City>();

            using SqlCommand cmd = new SqlCommand(SELECT_CMD, conn);
            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                City city = new City()
                {
                    CityName = dr.GetString(dr.GetOrdinal("cityName")),
                    Population = dr.GetDouble(dr.GetOrdinal("population")),
                    IsNew = false
                };

                cities.Add(city);
            }

            dr.Close();
            return cities;
        }
    }
}
