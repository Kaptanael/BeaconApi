using BeaconApi.Models;
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
    public class BeaconImageRepository : IBeaconImageRepository
    {
        private readonly IConfiguration _configuration;

        public BeaconImageRepository(IConfiguration configuration)
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

        public bool Insert(BeaconImage beacon)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BeaconImage](BeaconGUID,BeaconImageBinary) " +
                                                " Values(@BeaconGUID,@BeaconImageBinary)", con);
                cmd.CommandType = CommandType.Text;                
                cmd.Parameters.AddWithValue("BeaconGUID", beacon.BeaconGUID);
                cmd.Parameters.AddWithValue("BeaconImageBinary", beacon.BeaconImageBinary);
                con.Open();

                result = cmd.ExecuteNonQuery();
            }

            return result > 0 ? true : false;
        }

        public List<BeaconImage> GetBeaconImageByBeaconGuid(Guid guid)
        {
            List<BeaconImage> beaconImages = null;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[BeaconImage] WHERE BeaconGUID=@BeaconGUID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("BeaconGUID", guid);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    beaconImages = GetBeaconImages(reader);
                }
            }

            return beaconImages;
        }

        public BeaconImage GetBeaconImageByGuid(Guid guid)
        {
            BeaconImage beaconImage = null;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[BeaconImage] WHERE BeaconImageID=@BeaconImageID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("BeaconImageID", guid);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    beaconImage = GetBeaconImage(reader);
                }
            }

            return beaconImage;
        }

        private List<BeaconImage> GetBeaconImages(SqlDataReader reader)
        {
            var beaconImages = new List<BeaconImage>();

            while (reader.Read())
            {
                var beaconImage = new BeaconImage();
                beaconImage.BeaconImageID = new Guid(reader["BeaconImageID"].ToString());
                beaconImage.BeaconGUID = new Guid(reader["BeaconGUID"].ToString());
                beaconImage.BeaconImageBinary = reader.IsDBNull("BeaconImageBinary") ? null : (byte[])reader["BeaconImageBinary"]; 
                beaconImages.Add(beaconImage);
            }

            return beaconImages;
        }

        private BeaconImage GetBeaconImage(SqlDataReader reader)
        {
            var beaconImage = new BeaconImage();

            while (reader.Read())
            {
                beaconImage.BeaconImageID = new Guid(reader["BeaconImageID"].ToString());
                beaconImage.BeaconGUID = new Guid(reader["BeaconGUID"].ToString());
                beaconImage.BeaconImageBinary = reader.IsDBNull("BeaconImageBinary") ? null : (byte[])reader["BeaconImageBinary"];
            }

            return beaconImage;
        }
    }
}
