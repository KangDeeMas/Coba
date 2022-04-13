using Hari4.Helpers;
using Hari4.Models;
using Hari4.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace Hari4.Controllers
{
    public class MahasiswaController : Controller
    {
        public IActionResult IndexMahasiswa()
        {
            List<MahasiswaModel> objList = new List<MahasiswaModel>();
            //MahasiswaViewModels mahasiswaView = new MahasiswaViewModels();
            SqlConnection connection = new SqlConnection(Constants.CONNECTION_STRINGS);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Mahasiswa";

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    MahasiswaModel model = new MahasiswaModel();
                    model.ID = Convert.ToInt32(reader["ID"]);
                    model.Npm = reader["Npm"].ToString();
                    model.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                    model.Email = reader["Email"].ToString();
                    model.Alamat = reader["Alamat"].ToString();
                    model.JenisKelamin = reader["JenisKelamin"].ToString();
                    model.IsActive = Convert.ToBoolean(reader["IsActive"]);

                    objList.Add(model);
                    //mahasiswaView.ListMahasiswa.Add(model);
                }
            }

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

            return View(objList);
        }

        public IActionResult Details(int id)
        {
            MahasiswaModel model = new MahasiswaModel();
            SqlConnection connection = new SqlConnection(Constants.CONNECTION_STRINGS);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Mahasiswa WHERE ID = " + id;

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MahasiswaModel();
                model.ID = Convert.ToInt32(reader["ID"]);
                model.Npm = reader["Npm"].ToString();
                model.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                model.Email = reader["Email"].ToString();
                model.Alamat = reader["Alamat"].ToString();
                model.JenisKelamin = reader["JenisKelamin"].ToString();
                model.IsActive = Convert.ToBoolean(reader["IsActive"]);
            }
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MahasiswaViewModels objmahasiswa)
        {
            try
            {
                SqlConnection connection = new(Constants.CONNECTION_STRINGS);
                SqlCommand cmd = new();
                cmd.Connection = connection;
                cmd.CommandText = @$"INSERT INTO [dbo].[Mahasiswa]
                                       ([NPM], [NamaMahasiswa], [Email], [Alamat], [JenisKelamin], [IsActive])
                                 VALUES
                                       ('{objmahasiswa.Mahasiswa.Npm}', '{objmahasiswa.Mahasiswa.NamaMahasiswa}', '{objmahasiswa.Mahasiswa.Email}', '{objmahasiswa.Mahasiswa.Alamat}', '{objmahasiswa.Mahasiswa.JenisKelamin}', {(objmahasiswa.Mahasiswa.IsActive == true ? 1 : 0)})";

                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();

                    return RedirectToAction(nameof(IndexMahasiswa));
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            MahasiswaModel model = new MahasiswaModel();
            SqlConnection connection = new SqlConnection(Constants.CONNECTION_STRINGS);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Mahasiswa WHERE ID = " + id;

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MahasiswaModel();
                model.ID = Convert.ToInt32(reader["ID"]);
                model.Npm = reader["Npm"].ToString();
                model.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                model.Email = reader["Email"].ToString();
                model.Alamat = reader["Alamat"].ToString();
                model.JenisKelamin = reader["JenisKelamin"].ToString();
                model.IsActive = Convert.ToBoolean(reader["IsActive"]);
            }

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ID, Npm, NamaMahasiswa, Email, Alamat, JenisKelamin, IsActive")] MahasiswaModel objmahasiswa)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Constants.CONNECTION_STRINGS);
                SqlCommand cmd = new();
                cmd.Connection = connection;
                cmd.CommandText = @$"UPDATE [dbo].[Mahasiswa]
                                       SET [Npm] = '{objmahasiswa.Npm}'
                                          ,[NamaMahasiswa] = '{objmahasiswa.NamaMahasiswa}'
                                          ,[Email] = '{objmahasiswa.Email}'
                                          ,[Alamat] = '{objmahasiswa.Alamat}'
                                          ,[JenisKelamin] = '{objmahasiswa.JenisKelamin}'
                                          ,[IsActive] = {(objmahasiswa.IsActive == true ? 1 : 0)}
                                     WHERE ID = {objmahasiswa.ID}";

                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();

                    return RedirectToAction(nameof(IndexMahasiswa));
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            MahasiswaModel model = new MahasiswaModel();
            SqlConnection connection = new SqlConnection(Constants.CONNECTION_STRINGS);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Mahasiswa WHERE ID = " + id;

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MahasiswaModel();
                model.ID = Convert.ToInt32(reader["ID"]);
                model.Npm = reader["Npm"].ToString();
                model.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                model.Email = reader["Email"].ToString();
                model.Alamat = reader["Alamat"].ToString();
                model.JenisKelamin = reader["JenisKelamin"].ToString();
                model.IsActive = Convert.ToBoolean(reader["IsActive"]);
            }

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind("ID, NPM, NamaMahasiswa, Email, Alamat, JenisKelamin, IsActive")] MahasiswaModel objmahasiswa)
        {
            try
            {
                SqlConnection connection = new(Constants.CONNECTION_STRINGS);
                SqlCommand cmd = new();
                cmd.Connection = connection;
                cmd.CommandText = $@"DELETE FROM [dbo].[Mahasiswa] WHERE ID = {objmahasiswa.ID}";

                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();

                    return RedirectToAction(nameof(IndexMahasiswa));
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
