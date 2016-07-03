using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data;
using System.Data.Linq.Mapping;
using HMSOM;

namespace HMSDL
{
    public class PackagesDL
    {
        #region Private Members
        private string connectionString;
        #endregion

        #region Constructor
        public PackagesDL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        public Packages Load(int PackageID)
        {
            SqlConnection SqlCon = new SqlConnection(connectionString);
            Packages objPackages = new Packages();
            var dc = new DataContext(SqlCon);
            try
            {
                //Get Packages
                var resultPackages = dc.ExecuteQuery<Packages>("exec Get_Packages {0}", PackageID).ToList();
                if (resultPackages.Count > 0)
                {
                    objPackages = resultPackages[0];
                }
                //Get PackageChannels
                var resultPackageChannels = dc.ExecuteQuery<PackageChannels>("exec Get_PackageChannels {0}", PackageID).ToList();
                objPackages.PackageChannelsList = resultPackageChannels;
                objPackages.Types = new System.Collections.Hashtable();
                objPackages.Types["PackageChannelsList"] = new PackageChannels();
                dc.Dispose();
                return objPackages;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
                SqlCon.Dispose();
            }
        }
        public DataTable LoadAllPackages()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("exec Get_Packages", con);

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;
        }
        public bool Update(Packages objPackages)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //update Packages
                UpdatePackages(objPackages, trn);
                if (objPackages.PackageID > 0)
                {

                    ////Update PackageChannels
                    //DeletePackageChannels(Convert.ToInt32(objPackages.PackageID), trn);
                    //foreach (PackageChannels objPackageChannels in objPackages.PackageChannelsList)
                    //{
                    //    objPackageChannels.PackageID = objPackages.PackageID;
                    //    InsertIntoPackageChannels(objPackageChannels, trn);
                    //}
                    trn.Commit();
                }
                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                con.Dispose();
            }
        }
        public bool Delete(int PackageID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //Delete PackageChannels
                DeletePackageChannels(PackageID, trn);
                //Delete Packages
                DeletePackages(PackageID, trn);
                trn.Commit();
                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                con.Dispose();
            }
        }

        public bool UpdatePackages(Packages objPackages, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Update_Packages", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Transaction = trn;
                cmd.Parameters.Add("@AddOnPrice", SqlDbType.Decimal).Value = objPackages.AddOnPrice;
                cmd.Parameters.Add("@BasicPrice", SqlDbType.Decimal).Value = objPackages.BasicPrice;
                cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = objPackages.Discount;
                cmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = objPackages.PackageID;
                cmd.Parameters["@PackageID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@PackageName", SqlDbType.VarChar, 50).Value = objPackages.PackageName;
                cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = objPackages.OperatorID;
                cmd.Parameters.Add("@Channels", SqlDbType.VarChar, 5000).Value = objPackages.Channels;
                cmd.Parameters.Add("@ServiceTaxPerc", SqlDbType.Decimal).Value = objPackages.ServiceTaxPerc;
                cmd.Parameters.Add("@EntTax", SqlDbType.Decimal).Value = objPackages.EntTax;

                cmd.ExecuteNonQuery();

                //after updating the Packages, update PackageID
                objPackages.PackageID = Convert.ToInt32(cmd.Parameters["@PackageID"].Value);

                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        public bool DeletePackages(int PackageID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from Packages where PackageID=@PackageID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = PackageID;

                cmd.ExecuteNonQuery();


                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
        }
        public bool DeletePackageChannels(int PackageID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from PackageChannels where PackageID=@PackageID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = PackageID;

                cmd.ExecuteNonQuery();


                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
        }
        public bool InsertIntoPackageChannels(PackageChannels objPackageChannels, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Into_PackageChannels", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trn;
                cmd.Parameters.Add("@ChannelID", SqlDbType.Int).Value = objPackageChannels.ChannelID;
                cmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = objPackageChannels.PackageID;

                cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        #endregion
    }
}
