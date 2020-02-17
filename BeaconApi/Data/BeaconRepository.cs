using BeaconApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconApi.Data
{
    public class BeaconRepository
    {
        private readonly IConfiguration _configuration;
        public BeaconRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Beacon> GetAll() 
        {
            List<Beacon> beaconList = new List<Beacon>();
            string connectionString = _configuration.GetConnectionString("BeaconDbContext");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Beacon", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var beacon = new Beacon();

                    beacon.GUID = new Guid(reader["GUID"].ToString());
                    beacon.UUID = reader["UUID"].ToString();
                    beacon.ShortDescription = reader["ShortDescription"].ToString();
                    beacon.LongDescription = reader["LongDescription"].ToString();
                    beacon.SVGHeight = reader["SVGHeight"].ToString();
                    beacon.SVGWidth = reader["SVGWidth"].ToString();      
                    beacon.ThumbnailFilePath = reader["ThumbnailFilePath"].ToString();
                    beacon.ImageFilePath = reader["ImageFilePath"].ToString();
                    beacon.VideoFilePath = reader["VideoFilePath"].ToString();
                    beaconList.Add(beacon);
                }
            }

            return beaconList;
        }
    }
}
