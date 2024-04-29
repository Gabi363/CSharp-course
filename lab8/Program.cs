using System.Data;
using Microsoft.Data.Sqlite;
using SQLitePCL;

class SQLite {

    public List<List<string>> read_csv(string file, char delimiter){
        List<List<string>> result = new List<List<string>>();
        StreamReader reader = new StreamReader(file);
        while(!reader.EndOfStream){
            var line = reader.ReadLine();
            string[] values = line.Split(delimiter);
            result.Add(values.ToList());
        }
        // result[0].ForEach(Console.WriteLine);
        return result;
    }

    public List<List<string>> get_types(List<List<string>> data){
        List<List<string>> columns = new List<List<string>>();
        for(int i=0; i<data[0].Count; i++){
            columns.Add(new List<string>());
        }
        foreach(List<string> line in data){
            for(int i=0; i<data[0].Count; i++){
                columns[i].Add(line[i]);
            }
        }

        List<List<string>> dict = new List<List<string>>();
        foreach(var line in columns){

            bool null_allowed = false;
            string type = "INTEGER";

            foreach(var element in line.Skip(1)){
                int x = 0;
                if(element == ""){
                    null_allowed = true;
                }
                else if(!Int32.TryParse(element, out x)){
                    type = "";
                    break;
                }
            }
            if(type == ""){
                type = "REAL";
                null_allowed = false;
                foreach(var element in line.Skip(1)){
                    double y = 0.0;
                    if(element == ""){
                        null_allowed = true;
                    }
                    else if(!Double.TryParse(element.Replace('.', ','), out y)){
                        type = "";
                        break;
                    }
                }
                if(type == ""){
                    type = "TEXT";
                    null_allowed = false;
                    foreach(var element in line.Skip(1)){
                        if(element == ""){
                            null_allowed = true;
                        }
                    }
                }
            }
            dict.Add(new List<string>{line[0], type, null_allowed.ToString()});
        }
        
        return dict;
    }


    public bool create_table(List<List<string>> types, string name, SqliteConnection connection){
        try {
                SqliteCommand delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS " + name;
                delTableCmd.ExecuteNonQuery();

                string command = "CREATE TABLE \"" + name + "\" (";
                foreach(var line in types){
                    command += "\"" + line[0] + "\"	" + line[1];
                    if(line[2] == "False"){
                        command += " NOT NULL,";
                    }
                    else{
                        command += ",";
                    }
                }
                command = command.Remove(command.Length-1);
                command += ");";

                SqliteCommand createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = command;             
                createTableCmd.ExecuteNonQuery();

            } catch (Exception e){ 
                Console.WriteLine(e.Message);
                return false;
            }
         return true;
    }


    public bool seed_data(List<List<string>> data, string name, SqliteConnection connection)
        {
            try {
                using (var transaction = connection.BeginTransaction())
                    {
                        string command = "INSERT INTO " + name + "(";
                        foreach(var x in data[0]){
                            command += x + ", ";
                        }
                        command = command.Remove(command.Length-1);
                        command = command.Remove(command.Length-1);
                        command += ")VALUES";
                        foreach(var line in data.Skip(1)){
                            command += "(";
                            foreach(var x in line){
                                double y;
                                if(x == "") command += "NULL, ";
                                else if (!Double.TryParse(x, out y)) command += "\"" + x + "\", ";
                                else command += x+", ";
                            }
                            command = command.Remove(command.Length-1);
                            command = command.Remove(command.Length-1);
                            command += "),";
                        }
                        command = command.Remove(command.Length-1);
                        // Console.WriteLine(command);

                        SqliteCommand insertCmd = connection.CreateCommand();
                        insertCmd.CommandText = command;
                        insertCmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
            } catch (Exception e){ 
                Console.WriteLine(e.Message);
                return false;}
            return true;
        }


        public void show_table(string name, SqliteConnection connection)
        {
            SqliteCommand selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM " + name;

            using (SqliteDataReader reader = selectCmd.ExecuteReader())
            {
                bool firstRow = true;
                while (reader.Read())
                {
                    if (firstRow)
                    {
                        for (int a = 0; a < reader.FieldCount; a++)
                        {
                            Console.Write(reader.GetName(a));
                            Console.Write(",");
                        }
                        firstRow = false;
                        Console.WriteLine("");
                    }
                    for (int a = 0; a < reader.FieldCount; a++)
                    {
                        String?val = null;
                        try {
                            val = reader.GetString(a);
                        } catch {}
                        Console.Write(val != null ? val : "NULL");
                        Console.Write(",");
                    }
                    Console.WriteLine("");
                }
                reader.Close();
            }
        }



    public static void Main(){
        SQLite program = new SQLite();
        // zad1
        var data = program.read_csv("file1.csv", ',');
        // data.ForEach(x => x.ForEach(Console.WriteLine));

        // zad2
        var types = program.get_types(data);
        types.ForEach(x => x.ForEach(Console.WriteLine));
        // for(int i=0; i<types.Count; i++){
            // Console.WriteLine(types[i][0]);
        // }

        // zad3-5
        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "./data_base.db";

        using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
        {
            connection.Open();
            // if(program.create_table(types, "name", connection))
            // if (program.seed_data(data, "name", connection))
            //     program.show_table("name", connection);
        }


    }
}