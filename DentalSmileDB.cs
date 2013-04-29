using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

//Add MySql Library
using MySql.Data.MySqlClient;
using smileUp.DataModel;
using System.Data;

namespace smileUp
{
   public class DentalSmileDB
    {
        private MySqlConnection connection;
        private MySqlConnection patientConnection;
        private MySqlConnection dentistConnection;
        private MySqlConnection phaseConnection;
        private MySqlConnection fileConnection;

        private string server;
        private string database;
        private string uid;
        private string password;

        private string user = "USER";
        public string User { get { return user; } set { this.user = value; } }

        //Constructor
        public DentalSmileDB()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "dentalsmile";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connectionString += "Convert Zero Datetime=True;";

            connection = new MySqlConnection(connectionString);
            patientConnection = new MySqlConnection(connectionString);
            dentistConnection = new MySqlConnection(connectionString);
            phaseConnection = new MySqlConnection(connectionString);
            fileConnection = new MySqlConnection(connectionString);
        }


        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool OpenPatientConnection()
        {
            try
            {
                patientConnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool ClosePatientConnection()
        {
            try
            {
                patientConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool OpenDentistConnection()
        {
            try
            {
                dentistConnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseDentistConnection()
        {
            try
            {
                dentistConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool OpenPhaseConnection()
        {
            try
            {
                phaseConnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool ClosePhaseConnection()
        {
            try
            {
                phaseConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool OpenFileConnection()
        {
            try
            {
                fileConnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseFileConnection()
        {
            try
            {
                fileConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        
        public void InsertPatient(Patient p)
        {
            string tableName = "PATIENT";
            string columns = "(id, fname, lname, birthdate, birthplace, gender,address1,address2,city,phone, created,createdBy)";
            string values = "('" + p.Id + "','" + p.FirstName + "','" + p.LastName + "'," + p.BirthDate.ToString(Smile.DATE_FORMAT) + ",'" + p.BirthPlace + "','" + p.Gender + "','" + p.Address1 + "','" + p.Address2 + "','" + p.City + "','" + p.Phone + "," + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + ",'"+User+"')";
            string query = "INSERT INTO "+tableName + " "+ columns +" values "+ values +" ;";

            if (this.OpenPatientConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, patientConnection);
                cmd.ExecuteNonQuery();
                this.ClosePatientConnection();
            }
        }
        public void UpdatePatient(Patient p)
        {
            string tableName = "PATIENT";
            string setColumns = "fname = '" + p.FirstName + "', lname= '" + p.LastName + "', birthdate = '" + p.BirthDate.ToString(Smile.DATE_FORMAT) + "', birthplace= '" + p.BirthPlace + "', gender= '" + p.Gender + "',address1= '" + p.Address1 + "',address2= '" + p.Address2 + "',city= '" + p.City + "',phone= '" + p.Phone + "', modified = '" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy= '"+User+"' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = '"+p.Id+"'";

            if (this.OpenPatientConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, patientConnection);
                cmd.ExecuteNonQuery();
                this.ClosePatientConnection();
            }
        }

        public bool InsertDentist (Dentist p)
        {
            string tableName = "DENTIST";
            string columns = "(userid, fname, lname, birthdate, birthplace, gender,address1,address2,city,phone, created,createdBy)";
            string values = "('" + p.UserId + "','" + p.FirstName + "','" + p.LastName + "','" + p.BirthDate.ToString(Smile.DATE_FORMAT) + "','" + p.BirthPlace + "','" + p.Gender + "','" + p.Address1 + "','" + p.Address2 + "','" + p.City + "','" + p.Phone + "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','"+User+"')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenDentistConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dentistConnection);
                cmd.ExecuteNonQuery();
                this.CloseDentistConnection();
                return true;
            }
            
            return false;
        }

        public void UpdateDentist (Dentist p)
        {
            string tableName = "DENTIST";
            string setColumns = "fname = '" + p.FirstName + "', lname= '" + p.LastName + "', birthdate = '" + p.BirthDate.ToString(Smile.DATE_FORMAT) + "', birthplace= '" + p.BirthPlace + "', gender= '" + p.Gender + "',address1= '" + p.Address1 + "',address2= '" + p.Address2 + "',city= '" + p.City + "',phone= '" + p.Phone + "', modified = '" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy= '"+User+"' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE userid = " + p.UserId;

            if (this.OpenDentistConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dentistConnection);
                cmd.ExecuteNonQuery();
                this.CloseDentistConnection();
            }
        }

        public bool InsertUser(SmileUser p)
        {
            string tableName = "SmileUser";
            string columns = "(userid, password, created,createdBy)";
            string values = "('" + p.UserId + "','" + p.Password+ "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','"+User+"')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }

            return false;
        }

        public void SetPassword(string md5generated, string userid)
        {
            string tableName = "SmileUser";
            string setColumns = "password = '" + md5generated + "', modified = '" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy= '"+User+"' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE userid = " + userid;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        
        public void SetAdmin(bool p, string userid)
        {
            string tableName = "SmileUser";
            string setColumns = "admin= '" + p + "', modified = '" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy= '"+User+"' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE userid = " + userid;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }


        public bool InsertTreatment(Treatment t)
        {
            string tableName = "TREATMENT";
            string columns = "(id, phase, dentist, patient, tdate, ttime, room, tref, created,createdBy)";
            string values = "('" + t.Id + "'," + t.Phase.Id + ",'" + t.Dentist.UserId + "','" + t.Patient.Id + "','" + t.TreatmentDate.ToString(Smile.DATE_FORMAT) + "','" + t.TreatmentTime + "','" + t.Room + "','" + "','" + t.TRefId+ "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','" + User + "')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }
            return false;
        }

        public void UpdateTreatment(Treatment t)
        {
            string tableName = "TREATMENT";
            string setColumns = "phase =" + t.Phase.Id + ", dentist='" + t.Dentist.UserId + "', patient='" + t.Patient.Id + "', tdate='" + t.TreatmentDate.ToString(Smile.DATE_FORMAT) + "', ttime='" + t.TreatmentTime + "',room='" + t.Room + "',tref='" + t.TRefId+ "', modified='" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy='" + User + "' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = '" + t.Id + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public bool InsertFileInfo(SmileFile t)
        {
            string tableName = "PFILE";
            string columns = "(id, filename, description, patient, screenshot, type, created,createdBy)";
            string values = "('" + t.Id + "','" + t.FileName + "','" + t.Description + "','" + t.Patient.Id + "','" + t.Screenshot + "'," + t.Type + ",'" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','"+User+"')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                
                return true;
            }
            return false;
        }

        public void UpdateFileInfo(SmileFile t)
        {
            string tableName = "PFILE";
            string setColumns = "filename='" + t.FileName + "', description='" + t.Description + "', patient='" + t.Patient.Id + "', screenshot='" + t.Screenshot + "', type=" + t.Type + ", modified='" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy='"+User+"' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = '" + t.Id + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public List<Dentist> findDentistsByOr(string userid, string firstname, string lastname)
        {
            string query = "SELECT * FROM DENTIST ";
            bool any = false;
            string where = "";
            if (userid != null)
            {
                where += " userid like '%" + userid + "%' ";
                any = true;
            }
            if (firstname != null)
            {
                if (any) where += " or ";
                where += " fname like '%" + firstname + "%' ";
                any = true;
            }
            if (lastname != null)
            {
                if (any) where += " or ";
                where += " lname like '%" + lastname + "%' ";
                any = true;
            }
            if (any) query += " WHERE " + where;

            List<Dentist> list = null;
            if (this.OpenDentistConnection() == true)
            {
                list = new List<Dentist>();
                MySqlCommand cmd = new MySqlCommand(query, dentistConnection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Dentist p = toDentist(dataReader);
                    list.Add(p);
                }
                dataReader.Close();

                this.CloseDentistConnection();

            }
            return list;
        }

        public List<Dentist> findDentistsByAnd(string userid, string firstname, string lastname)
        {
            string query = "SELECT * FROM DENTIST ";
            bool any = false;
            string where = "";
            if (userid != null)
            {
                where += " userid like '%" + userid + "%' ";
                any = true;
            }
            if (firstname != null)
            {
                if (any) where += " and ";
                where += " fname like '%" + firstname + "%' ";
                any = true;
            }
            if (lastname != null)
            {
                if (any) where += " and ";
                where += " lname like '%" + lastname + "%' ";
                any = true;
            }
            if (any) query += " WHERE " + where;

            List<Dentist> list = null;
            if (this.OpenDentistConnection() == true)
            {
                list = new List<Dentist>();
                MySqlCommand cmd = new MySqlCommand(query, dentistConnection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Dentist p = toDentist(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseDentistConnection();

            }
            return list;
        }

        public List<Dentist> SelectAllDentists()
        {
            string query = "SELECT * FROM DENTIST";
            List<Dentist> list = null;
            if (this.OpenDentistConnection() == true)
            {
                list = new List<Dentist>();
                MySqlCommand cmd = new MySqlCommand(query, dentistConnection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Dentist p = toDentist(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseDentistConnection();
            }
            return list;
        }

        private Dentist toDentist(MySqlDataReader dataReader)
        {
            Dentist p = new Dentist();

            p.UserId = GetStringSafe(dataReader, "userid");
            p.FirstName = GetStringSafe(dataReader, "fname");
            p.LastName = GetStringSafe(dataReader, "lname");
            p.BirthDate = dataReader.GetDateTime("birthdate");
            p.BirthPlace = GetStringSafe(dataReader, "birthplace");
            p.Gender = GetStringSafe(dataReader, "gender");
            p.Address1 = GetStringSafe(dataReader, "address1");
            p.Address2 = GetStringSafe(dataReader, "address2");
            p.City = GetStringSafe(dataReader, "city");
            p.Phone = GetStringSafe(dataReader, "phone");

            return p;
        }

        private Patient toPatient(MySqlDataReader dataReader)
        {
            Patient p = new Patient();

            p.Id = GetStringSafe(dataReader, "id");
            p.FirstName = GetStringSafe(dataReader, "fname");
            p.LastName = GetStringSafe(dataReader, "lname");
            p.BirthDate = dataReader.GetDateTime("birthdate");
            p.BirthPlace = GetStringSafe(dataReader, "birthplace");
            p.Gender = GetStringSafe(dataReader, "gender");
            p.Address1 = GetStringSafe(dataReader, "address1");
            p.Address2 = GetStringSafe(dataReader, "address2");
            p.City = GetStringSafe(dataReader, "city");
            p.Phone = GetStringSafe(dataReader, "phone");
            
            return p;
        }

        private SmileFile toSmileFile(MySqlDataReader dataReader, bool nested)
        {
            SmileFile p = new SmileFile();
            
            p.Id = GetStringSafe(dataReader, "id");
            p.FileName = GetStringSafe(dataReader, "filename");
            p.Description = GetStringSafe(dataReader, "description");
            p.Screenshot = GetStringSafe(dataReader, "screenshot");
            p.Type = dataReader.GetInt32("type");
            
            if(nested) p.Patient = findPatientById(GetStringSafe(dataReader, "patient"));

            return p;
        }

        private Phase toPhase(MySqlDataReader dataReader)
        {
            Phase p = new Phase();
            
            p.Id = dataReader.GetInt32("id");
            p.Name= dataReader.GetString("name");

            return p;
        }

        private Treatment toTreatment(MySqlDataReader dataReader)
        {
            Treatment p = new Treatment();

            p.Id = GetStringSafe(dataReader, "id");
            p.Room = GetStringSafe(dataReader, "room");
            p.TreatmentDate = dataReader.GetDateTime("tdate");
            p.TreatmentTime = GetStringSafe(dataReader, "ttime");
            p.TRefId = GetStringSafe(dataReader, "tref");

            p.Patient = findPatientById(GetStringSafe(dataReader, "patient"));
            //p.Phase = findPhaseById(dataReader.GetInt32("phase"));
            p.Phase = Smile.GetPhase(dataReader.GetInt32("phase"));
            p.Dentist = findDentistByUserId(GetStringSafe(dataReader, "dentist"));

            p.Files = findSmileFilesByTreatmentId(p.Id);
            return p;
        }


        private SmileUser toSmileUser(MySqlDataReader dataReader)
        {
            SmileUser p = new SmileUser();

            p.UserId = dataReader.GetString("userid");
            p.Person= findDentistByUserId(dataReader.GetString("userid"));

            return p;
        }

        private Phase findPhaseById(int id)
        {
            string query = "SELECT * FROM Phase WHERE id= @id";
            Phase p = null;
            if (this.OpenPhaseConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, phaseConnection);
                cmd.Parameters.Add(new MySqlParameter("id", id));

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toPhase(dataReader);
                }
                dataReader.Close();
                this.ClosePhaseConnection();

            }
            return p;
        }

        private Dentist findDentistByUserId(string id)
        {
            string query = "SELECT * FROM DENTIST WHERE userid = @userid";
            Dentist p = null;
            if (this.OpenDentistConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dentistConnection);
                cmd.Parameters.Add(new MySqlParameter("userid", id));

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toDentist(dataReader);
                }
                dataReader.Close();
                this.CloseDentistConnection();

            }
            return p;
        }

        private SmileUser login(string id, string password)
        {
            string query = "SELECT * FROM SmileUser WHERE userid = @userid";
            SmileUser p = null;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("userid", id));

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = new SmileUser();

                    p.UserId = GetStringSafe(dataReader, "userid");
                    p.Password = GetStringSafe(dataReader, "password");
                    //p.Person = findDentistByUserId(dataReader.GetString("userid"));
                }
                dataReader.Close();
                this.CloseConnection();
            }

            if (p != null)
            {
                //crosscheck the password using MD5
            }

            return p;
        }

        private SmileUser findUserByUserId(string id)
        {
            string query = "SELECT * FROM SmileUser WHERE userid = @userid";
            SmileUser p = null;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("userid", id));

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toSmileUser(dataReader);
                }
                dataReader.Close();
                this.CloseConnection();

            }
            return p;
        }
        
        private List<SmileFile> findSmileFilesByTreatmentId(string treatmentId)
        {
            string query = "SELECT f.* FROM TREATMENT_PFILE t, PFILE f WHERE t.Treatment = @id AND t.Pfile = f.Id";
            List<SmileFile> list = null;
            if (this.OpenFileConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, fileConnection);
                cmd.Parameters.Add(new MySqlParameter("id", treatmentId));
                list = new List<SmileFile>();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    SmileFile p = toSmileFile(dataReader, false);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseFileConnection();

            }
            return list;
        }

        private Patient findPatientById(string id)
        {
            string query = "SELECT * FROM PATIENT WHERE id = @id";
            Patient p = null;
            if (this.OpenPatientConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, patientConnection);
                cmd.Parameters.Add(new MySqlParameter("id", id));

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toPatient(dataReader);
                }
                dataReader.Close();
                this.ClosePatientConnection();

            }
            return p;
        }

        public List<Patient> SelectAllPatient()
        {
            string query = "SELECT * FROM PATIENT";
            List<Patient> list = null;
            if (this.OpenPatientConnection() == true)
            {
                list = new List<Patient>();

                MySqlCommand cmd = new MySqlCommand(query, patientConnection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Patient p = toPatient(dataReader);
                    list.Add(p);
                }
                dataReader.Close();

                this.ClosePatientConnection();

            }
            return list;
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar()+"");
                
                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);

                
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                
                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to Restore!");
            }
        }

        //internal void InsertMeasurement(string measurement, string patient, string fileid, string measurementstatus, string measurementlastupdate, char measurementby)
        //{
        //    throw new NotImplementedException();
        //}

        //Insert  teethFile
        private void InsertTeethFiles(int patient_id, string file_name, string file_desc)
        {
            var A = DateTime.Today;
            var created_date = DateTime.Today;
            var modified_date = DateTime.Today;
            string created_by = "Asri";
            string modified_by = "Asri";

            string query = "INSERT INTO teeth_files (patient_id, file_name, file_desc, created_date, created_by, modified_date,modified_by) VALUES('" + patient_id + "', '" + file_name + "', '" + file_desc + "', '" + created_date + "', '" + created_by + "', '" + modified_date + "', '" + modified_by + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        private void InsertPatientTreatment(int patient_id, int doctor_id, int phase)
        {
           
            string query = "INSERT INTO patient_treatment (patient_id, doctor_id, phase) VALUES('" + patient_id + "', '" + doctor_id + "', '" + phase + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }


        internal List<Phase> SelectAllPhases()
        {
            string query = "SELECT * FROM PHASE";
            List<Phase> list = null;
            if (this.OpenPhaseConnection() == true)
            {
                list = new List<Phase>();
                MySqlCommand cmd = new MySqlCommand(query, phaseConnection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Phase p = toPhase(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.ClosePhaseConnection();
            }
            return list;
        }

        public bool insertTreatmentFiles(Treatment treatment, SmileFile file)
        {
            string tableName = "TREATMENT_PFILE";
            string columns = "(TREATMENT, PFILE)";
            string values = "('" + treatment.Id + "','" + file.Id+ "')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();

                return true;
            }
            return false;
        }

        public string getSmileFileNewId(string patientid)
        {
            string query = "SELECT MAX(SUBSTR(id, 14)) id FROM PFILE WHERE patient = @patient";
            int p = 0;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("patient", patientid));

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = dataReader.GetInt32("id"); ;
                }
                dataReader.Close();
                this.CloseConnection();
            }
            
            p = p + 1; //newid
            string r = patientid +""+p.ToString("000");
            return r;
        }

        public string getTreatmentNewId(string patientid)
        {
            string query = "SELECT MAX(SUBSTR(id, 14)) id FROM TREATMENT WHERE patient = @patient";
            int p = 0;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("patient", patientid));

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = dataReader.GetInt32("id"); ;
                }
                dataReader.Close();
                this.CloseConnection();
            }

            p = p + 1; //newid
            string r = patientid + "" + p.ToString("000");
            return r;
        }



        public List<Treatment> findTreatmentsByPatientId(string patientid)
        {
            string query = "SELECT * FROM TREATMENT WHERE patient = @id";
            List<Treatment> list = null;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("id", patientid));

                MySqlDataReader dataReader = cmd.ExecuteReader();
                list = new List<Treatment>(); 
                while (dataReader.Read())
                {
                    Treatment p = toTreatment(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return list;
        }

        public List<Treatment> findTreatments()
        {
            string query = "SELECT * FROM TREATMENT";
            List<Treatment> list = null;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();
                list = new List<Treatment>();
                while (dataReader.Read())
                {
                    Treatment p = toTreatment(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return list;
        }

        internal bool insertMeasurementTeeth(Measurement measurement, List<MeasurementTeeth> results)
        {
            if (this.OpenConnection() == true)
            {
                string tableName = "MeasurementTeeth";
                string columns = "(measurementid, teethId, length, spoint, epoint,type)";
                string values = "";
                foreach (MeasurementTeeth m in results)
                {
                    values = "(" + measurement.Id + ",'" + m.Identity + "'," + m.Length + ",'" + m.SPoint + "','" + m.EPoint + "','" + m.Type+ "')";
                    string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    m.Id = (int)cmd.LastInsertedId;
                }
                
                this.CloseConnection();
                return true;
            }
            return false;
        }

        internal bool insertMeasurement(Measurement measurement)
        {
            if (this.OpenConnection() == true)
            {
                string tableName = "Measurement";
                string columns = "(patient, treatment, pfile, type, created, createdBy)";
                string values = "";
                values = "('" + measurement.Patient + "','" + measurement.Treatment + "','" + measurement.Pfile + "'," + measurement.Type + ",'" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','"+User+"')";
                string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                measurement.Id = (int) cmd.LastInsertedId;

                this.CloseConnection();

                return true;
            }
            return false;
        }


        internal List<Treatment> findTreatmentsByOr(string id, string tdate, string patient, string dentist)
        {
            string query = "SELECT * FROM TREATMENT ";
            bool any = false;
            string where = "";
            if (id != null)
            {
                where += " id like '%" + id + "%' ";
                any = true;
            }
            if (tdate != null)
            {
                if (any) where += " or";
                where += " tdate = '" + tdate+ "' ";
                any = true;
            }
            if (patient!= null)
            {
                if (any) where += " or ";
                where += " patient like '%" + patient+ "%' ";
                any = true;
            }
            
            if (dentist!= null)
            {
                if (any) where += " or ";
                where += " dentist like '%" + dentist + "%' ";
                any = true;
            }

            if (any) query += " WHERE " + where;

            List<Treatment> list = null;
            if (this.OpenConnection() == true)
            {
                list = new List<Treatment>();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Treatment p = toTreatment(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();

            }
            return list;
        }

        internal bool insertTreatmentNotes(Treatment treatment, string resume, SmileFile file, string description)
        {
            if (this.OpenConnection() == true)
            {
                string tableName = "Treatment_Notes";
                string columns = "(treatment, notes, pfile, description, created, createdBy)";
                string values = "";
                values = "('" + treatment.Id + "','" + resume + "','" + file.Id + "','" + description+ "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','" + User + "')";
                string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                this.CloseConnection();

                return true;
            }
            return false;
        }

        public static string GetStringSafe(IDataReader reader, int colIndex)
        {
            return GetStringSafe(reader, colIndex, string.Empty);
        }

        public static string GetStringSafe(IDataReader reader, int colIndex, string defaultValue)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return defaultValue;
        }

        public static string GetStringSafe(IDataReader reader, string indexName)
        {
            return GetStringSafe(reader, reader.GetOrdinal(indexName));
        }

        public static string GetStringSafe(IDataReader reader, string indexName, string defaultValue)
        {
            return GetStringSafe(reader, reader.GetOrdinal(indexName), defaultValue);
        }
    }
}