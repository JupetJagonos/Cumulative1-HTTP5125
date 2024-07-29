using MySql.Data.MySqlClient;

namespace SchoolManagementSystem.Models
{
    public class SchoolManagementSystemDbContext
    {
        public string ConnectionString { get; set; }

        public SchoolManagementSystemDbContext()
        {
            ConnectionString = "server=localhost;database=schooldb;user=root;password=Test#1234";
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        // Methods for Teachers
        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> list = new List<Teacher>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM teachers", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Teacher()
                        {
                            TeacherId = Convert.ToInt32(reader["teacherid"]),
                            Name = reader["name"].ToString(),
                            HireDate = Convert.ToDateTime(reader["hiredate"]),
                            Salary = Convert.ToDecimal(reader["salary"])
                        });
                    }
                }
            }
            return list;
        }

        public Teacher GetTeacherById(int id)
        {
            Teacher teacher = null;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM teachers WHERE teacherid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        teacher = new Teacher()
                        {
                            TeacherId = Convert.ToInt32(reader["teacherid"]),
                            Name = reader["name"].ToString(),
                            HireDate = Convert.ToDateTime(reader["hiredate"]),
                            Salary = Convert.ToDecimal(reader["salary"])
                        };
                    }
                }
            }
            return teacher;
        }

        public void AddTeacher(Teacher teacher)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO teachers (name, hiredate, salary) VALUES (@name, @hiredate, @salary)", conn);
                cmd.Parameters.AddWithValue("@name", teacher.Name);
                cmd.Parameters.AddWithValue("@hiredate", teacher.HireDate);
                cmd.Parameters.AddWithValue("@salary", teacher.Salary);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTeacher(Teacher teacher)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE teachers SET name = @name, hiredate = @hiredate, salary = @salary WHERE teacherid = @id", conn);
                cmd.Parameters.AddWithValue("@id", teacher.TeacherId);
                cmd.Parameters.AddWithValue("@name", teacher.Name);
                cmd.Parameters.AddWithValue("@hiredate", teacher.HireDate);
                cmd.Parameters.AddWithValue("@salary", teacher.Salary);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTeacher(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open(); // Open the connection explicitly

                // Set teacherid to NULL in classes table
                MySqlCommand updateClassesCmd = new MySqlCommand("UPDATE classes SET teacherid = NULL WHERE teacherid = @id", conn);
                updateClassesCmd.Parameters.AddWithValue("@id", id);
                updateClassesCmd.ExecuteNonQuery();

                // Delete the teacher record
                MySqlCommand deleteTeacherCmd = new MySqlCommand("DELETE FROM teachers WHERE teacherid = @id", conn);
                deleteTeacherCmd.Parameters.AddWithValue("@id", id);
                deleteTeacherCmd.ExecuteNonQuery();
            }
        }

        // Additional methods for search functionality
        public List<Teacher> SearchTeachers(string name, DateTime? hireDate, decimal? salary)
        {
            List<Teacher> list = new List<Teacher>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM teachers WHERE (name LIKE @name OR @name IS NULL) AND (hiredate = @hireDate OR @hireDate IS NULL) AND (salary = @salary OR @salary IS NULL)", conn);
                cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                cmd.Parameters.AddWithValue("@hireDate", hireDate.HasValue ? (object)hireDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@salary", salary.HasValue ? (object)salary.Value : DBNull.Value);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Teacher()
                        {
                            TeacherId = Convert.ToInt32(reader["teacherid"]),
                            Name = reader["name"].ToString(),
                            HireDate = Convert.ToDateTime(reader["hiredate"]),
                            Salary = Convert.ToDecimal(reader["salary"])
                        });
                    }
                }
            }

            return list;
        }

        // Methods for Students
        public List<Student> GetAllStudents()
        {
            List<Student> list = new List<Student>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM students", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Student()
                        {
                            StudentId = Convert.ToInt32(reader["studentid"]),
                            Name = reader["name"].ToString(),
                            EnrollmentDate = Convert.ToDateTime(reader["enrollmentdate"])
                        });
                    }
                }
            }
            return list;
        }

        public Student GetStudentById(int id)
        {
            Student student = null;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM students WHERE studentid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        student = new Student()
                        {
                            StudentId = Convert.ToInt32(reader["studentid"]),
                            Name = reader["name"].ToString(),
                            EnrollmentDate = Convert.ToDateTime(reader["enrollmentdate"])
                        };
                    }
                }
            }
            return student;
        }

        public void AddStudent(Student student)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO students (name, enrollmentdate) VALUES (@name, @enrollmentdate)", conn);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@enrollmentdate", student.EnrollmentDate);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE students SET name = @name, enrollmentdate = @enrollmentdate WHERE studentid = @id", conn);
                cmd.Parameters.AddWithValue("@id", student.StudentId);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@enrollmentdate", student.EnrollmentDate);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM students WHERE studentid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // Methods for Classes
        public List<Class> GetAllClasses()
        {
            List<Class> list = new List<Class>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM classes", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int classId = Convert.ToInt32(reader["classid"]);
                        string className = reader["classname"].ToString();
                        int? teacherId = null;

                        // Check for DBNull before conversion
                        if (!Convert.IsDBNull(reader["teacherid"]))
                        {
                            teacherId = Convert.ToInt32(reader["teacherid"]);
                        }

                        list.Add(new Class()
                        {
                            ClassId = classId,
                            ClassName = className,
                            TeacherId = teacherId
                        });
                    }
                }
            }
            return list;
        }

        public Class GetClassById(int id)
        {
            Class classData = null;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM classes WHERE classid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        classData = new Class()
                        {
                            ClassId = Convert.ToInt32(reader["classid"]),
                            ClassName = reader["classname"].ToString(),
                            TeacherId = reader["teacherid"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["teacherid"])
                        };
                    }
                }
            }
            return classData;
        }


        public void AddClass(Class classData)
        {
            // Check if the provided teacherid exists in the teachers table
            if (!TeacherExists(classData.TeacherId))
            {
                throw new Exception("Teacher with the provided teacherid does not exist.");
            }

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO classes (classname, teacherid) VALUES (@classname, @teacherid)", conn);
                cmd.Parameters.AddWithValue("@classname", classData.ClassName);
                cmd.Parameters.AddWithValue("@teacherid", classData.TeacherId);
                cmd.ExecuteNonQuery();
            }
        }

        private bool TeacherExists(int? teacherId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM teachers WHERE teacherid = @teacherid", conn);
                cmd.Parameters.AddWithValue("@teacherid", teacherId);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public void UpdateClass(Class classData)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE classes SET classname = @classname, teacherid = @teacherid WHERE classid = @id", conn);
                cmd.Parameters.AddWithValue("@id", classData.ClassId);
                cmd.Parameters.AddWithValue("@classname", classData.ClassName);
                cmd.Parameters.AddWithValue("@teacherid", classData.TeacherId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteClass(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM classes WHERE classid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
