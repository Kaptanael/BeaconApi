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
    public class BeaconRepository : IBeaconRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connString;

        public BeaconRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connString = _configuration.GetConnectionString("BeaconDbContext");
        }
        public List<Beacon> GetAll()
        {
            List<Beacon> beaconList = new List<Beacon>();
            using (SqlConnection con = new SqlConnection(_connString))
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

        public Beacon GetBeaconByUUID(string uuid)
        {
            Beacon beacon = null;
            using (SqlConnection con = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Beacon WHERE UUID=@UUID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("UUID", uuid);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    beacon = new Beacon();
                    beacon.GUID = new Guid(reader["GUID"].ToString());
                    beacon.UUID = reader["UUID"].ToString();
                    beacon.ShortDescription = reader["ShortDescription"].ToString();
                    beacon.LongDescription = reader["LongDescription"].ToString();
                    beacon.SVGHeight = reader["SVGHeight"].ToString();
                    beacon.SVGWidth = reader["SVGWidth"].ToString();
                    beacon.ThumbnailFilePath = reader["ThumbnailFilePath"].ToString();
                    beacon.ImageFilePath = reader["ImageFilePath"].ToString();
                    beacon.VideoFilePath = reader["VideoFilePath"].ToString();
                }
            }

            return beacon;
        }

        public Beacon GetBeaconById(Guid guid)
        {
            Beacon beacon = null;
            using (SqlConnection con = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Beacon WHERE GUID=@GUID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("GUID", guid);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    beacon = new Beacon();
                    beacon.GUID = new Guid(reader["GUID"].ToString());
                    beacon.UUID = reader["UUID"].ToString();
                    beacon.ShortDescription = reader["ShortDescription"].ToString();
                    beacon.LongDescription = reader["LongDescription"].ToString();
                    beacon.SVGHeight = reader["SVGHeight"].ToString();
                    beacon.SVGWidth = reader["SVGWidth"].ToString();
                    beacon.ThumbnailFilePath = reader["ThumbnailFilePath"].ToString();
                    beacon.ImageFilePath = reader["ImageFilePath"].ToString();
                    beacon.VideoFilePath = reader["VideoFilePath"].ToString();
                }
            }

            return beacon;
        }

        public bool Insert(Beacon beacon)
        {
            int result = 0;            
            using (SqlConnection con = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Beacon(UUID,ShortDescription,LongDescription,SVGHeight,SVGWidth,ThumbnailFilePath,ImageFilePath,VideoFilePath) " +
                                                " Values(@UUID,@ShortDescription,@LongDescription,@SVGHeight,@SVGWidth,@ThumbnailFilePath,@ImageFilePath,@VideoFilePath)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("UUID", beacon.UUID);
                cmd.Parameters.AddWithValue("ShortDescription", beacon.ShortDescription);
                cmd.Parameters.AddWithValue("LongDescription", beacon.LongDescription);
                cmd.Parameters.AddWithValue("SVGHeight", beacon.SVGHeight);
                cmd.Parameters.AddWithValue("SVGWidth", beacon.SVGWidth);
                cmd.Parameters.AddWithValue("ThumbnailFilePath", beacon.ThumbnailFilePath);
                cmd.Parameters.AddWithValue("ImageFilePath", beacon.ImageFilePath);
                cmd.Parameters.AddWithValue("VideoFilePath", beacon.VideoFilePath);
                con.Open();
                result = cmd.ExecuteNonQuery();
            }

            return result > 0 ? true : false;
        }

        public bool Delete(Guid guid)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(_connString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Beacon WHERE GUID=@GUID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("GUID", guid);                
                con.Open();
                result = cmd.ExecuteNonQuery();
            }

            return result > 0 ? true : false;
        }
    }
}
