﻿using BeaconApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconApi.Data
{
    public class BeaconRepository : IBeaconRepository
    {
        private readonly IConfiguration _configuration;        

        public BeaconRepository(IConfiguration configuration)
        {
            _configuration = configuration;            
        }

        private string ConnString
        {
            get
            {
                return _configuration.GetConnectionString("BeaconDbContext");
            }
        }
        public List<Beacon> GetAll()
        {
            List<Beacon> beacons = null;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Beacon ORDER BY GUID DESC", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    beacons = GetBeaconList(reader);
                }
            }

            return beacons;
        }

        public Beacon GetBeacon(string uuid, int major, int minor)
        {
            Beacon beacon = null;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Beacon WHERE UUID=@UUID AND Major=@Major AND Minor=@Minor", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("UUID", uuid);
                cmd.Parameters.AddWithValue("Major", major);
                cmd.Parameters.AddWithValue("Minor", minor);                
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    beacon = GetBeacon(reader);
                }
            }

            return beacon;
        }

        public Beacon GetBeaconByUUID(string uuid)
        {
            Beacon beacon = null;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Beacon WHERE UUID=@UUID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("UUID", uuid);                
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    beacon = GetBeacon(reader);
                }
            }

            return beacon;
        }

        public Beacon GetBeaconByGuid(Guid guid)
        {
            Beacon beacon = null;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Beacon WHERE GUID=@GUID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("GUID", guid);                
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    beacon = GetBeacon(reader);
                }
            }

            return beacon;
        }

        public bool Insert(Beacon beacon)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Beacon(UUID,ShortDescription,LongDescription,SVGHeight,SVGWidth,Major,Minor,ThumbnailImageBinary) " +
                                                " Values(@UUID,@ShortDescription,@LongDescription,@SVGHeight,@SVGWidth,@Major,@Minor,@ThumbnailImageBinary)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("UUID", beacon.UUID);
                cmd.Parameters.AddWithValue("ShortDescription", beacon.ShortDescription);
                cmd.Parameters.AddWithValue("LongDescription", string.IsNullOrEmpty(beacon.LongDescription) ? (object)DBNull.Value : beacon.LongDescription);
                cmd.Parameters.AddWithValue("SVGHeight", string.IsNullOrEmpty(beacon.SVGHeight) ? (object)DBNull.Value : beacon.SVGHeight);
                cmd.Parameters.AddWithValue("SVGWidth", string.IsNullOrEmpty(beacon.SVGWidth) ? (object)DBNull.Value : beacon.SVGWidth);
                cmd.Parameters.AddWithValue("Major", beacon.Major);
                cmd.Parameters.AddWithValue("Minor", beacon.Minor);
                if (beacon.ThumbnailImageBinary != null)
                {
                    cmd.Parameters.AddWithValue("ThumbnailImageBinary", beacon.ThumbnailImageBinary);
                }
                else 
                {
                    cmd.Parameters.Add("ThumbnailImageBinary", SqlDbType.VarBinary, -1).Value = DBNull.Value;                   
                }
                
                con.Open();

                result = cmd.ExecuteNonQuery();
            }

            return result > 0 ? true : false;
        }

        public bool Delete(Guid guid)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Beacon WHERE GUID=@GUID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("GUID", guid);
                con.Open();

                result = cmd.ExecuteNonQuery();
            }

            return result > 0 ? true : false;
        }

        public bool BeaconExists(string uuid,int major, int minor)
        {
            int count = 0;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Beacon WHERE UUID=@UUID AND Major=@Major AND Minor=@Minor", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("UUID", uuid);
                cmd.Parameters.AddWithValue("Major", major);
                cmd.Parameters.AddWithValue("Minor", minor);
                con.Open();

                count = Convert.ToInt32(cmd.ExecuteScalar());                
            }

            return count > 0 ? true : false;
        }

        private Beacon GetBeacon(SqlDataReader reader)
        {
            var beacon = new Beacon();

            while (reader.Read())
            {
                beacon.GUID = new Guid(reader["GUID"].ToString());
                beacon.UUID = reader.GetString(reader.GetOrdinal("UUID"));
                beacon.ShortDescription = reader.GetString(reader.GetOrdinal("ShortDescription"));
                beacon.LongDescription = reader.IsDBNull("LongDescription") ? null : reader.GetString(reader.GetOrdinal("LongDescription"));
                beacon.Major = reader.GetInt32(reader.GetOrdinal("Major"));
                beacon.Minor = reader.GetInt32(reader.GetOrdinal("Minor"));
                beacon.SVGHeight = reader.IsDBNull("SVGHeight") ? null : reader.GetString(reader.GetOrdinal("SVGHeight"));
                beacon.SVGWidth = reader.IsDBNull("SVGWidth") ? null : reader.GetString(reader.GetOrdinal("SVGWidth"));
                beacon.ThumbnailImageBinary = reader.IsDBNull("ThumbnailImageBinary") ? null : (byte[])reader["ThumbnailImageBinary"];
            }

            return beacon;
        }

        private List<Beacon> GetBeaconList(SqlDataReader reader)
        {
            var beacons = new List<Beacon>();

            while (reader.Read())
            {
                var beacon = new Beacon();
                beacon.GUID = new Guid(reader["GUID"].ToString());
                beacon.UUID = reader.GetString(reader.GetOrdinal("UUID"));
                beacon.ShortDescription = reader.GetString(reader.GetOrdinal("ShortDescription"));
                beacon.LongDescription = reader.IsDBNull("LongDescription") ? null : reader.GetString(reader.GetOrdinal("LongDescription"));
                beacon.Major = reader.GetInt32(reader.GetOrdinal("Major"));
                beacon.Minor = reader.GetInt32(reader.GetOrdinal("Minor"));
                beacon.SVGHeight = reader.IsDBNull("SVGHeight") ? null : reader.GetString(reader.GetOrdinal("SVGHeight"));
                beacon.SVGWidth = reader.IsDBNull("SVGWidth") ? null : reader.GetString(reader.GetOrdinal("SVGWidth"));                
                beacon.ThumbnailImageBinary = reader.IsDBNull("ThumbnailImageBinary") ? null : (byte[])reader["ThumbnailImageBinary"];
                beacons.Add(beacon);
            }

            return beacons;
        }
    }
}
