using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace w15_1.Models
{
    public class Spedizioni
    {
        public int IdSpedizione { get; set; }
        [Required(ErrorMessage = "Il campo non è valido")]
       
        public int Cliente { get; set; }
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "Il campo città non è valido")]

        public string Città { get; set; }
        [Required(ErrorMessage = "Il campo non è valido")]
        public int Peso { get; set; }
        [Required(ErrorMessage = "Il campo non è valido")]
        [Display(Name = "Data spedizione")]
        public DateTime DataSpedizione { get; set; }
        [Required(ErrorMessage = "Il campo non è valido")]
        public string Indirizzo { get; set; }
        [Required(ErrorMessage = "Il campo non è valido")]
        [Display(Name = "Destinatario")]
        public string NominativoDestinatario { get; set; }
        [Required(ErrorMessage = "Il campo non è valido")]
       [Display(Name = "Costo della spedizione")]
        public double CostoSpedizione { get; set; }
        [Required(ErrorMessage = "Il campo non è valido")]
         [Display(Name = "Data consegna")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime DataConsegna  { get; set; }

        [Display(Name = "Codice fiscale")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il campo richiede 16 caratteri")]

         //Cliente
        public string CodiceFiscale { get; set; }
        public int IdCliente { get; set; }

        //StatoSpedizione
        public int Idstato { get; set; }
        public string Nomestato { get; set; }
        public int Totale { get; set; }

        //Date Json
        public string DataJson { get; set; }
        public string DataJson2 { get; set; }

        public string DataC { get; set; }
        public string DataS { get; set; }

        public int IdAggiornamento { get; set; }

        public int StatoSpedizione { get; set; }
  
        public string Luogo { get; set; }

        public string Descrizione { get; set; }
        public DateTime DataOraAggiornamento { get; set; }
        public int IdSpedizioni { get; set; }


        public  static   List<Spedizioni> spedizioni = new List<Spedizioni>();

        public static List<Spedizioni> ListClienti = new List<Spedizioni>();

        //Dropdown Clienti
        public static List<Spedizioni> Select()
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT * , Nome, IdCliente FROM Spedizione INNER JOIN Clienti ON Spedizione.Cliente=Clienti.IdCliente ", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Spedizioni p = new Spedizioni();
                p.NomeCliente = sqlreader1["Nome"].ToString();
                p.Città = sqlreader1["Città"].ToString();
                p.Indirizzo = sqlreader1["Indirizzo"].ToString();
                p.NominativoDestinatario = sqlreader1["NominativoDestinatario"].ToString();
                p.CostoSpedizione = Convert.ToDouble(sqlreader1["CostoSpedizione"]);
                p.DataSpedizione = Convert.ToDateTime(sqlreader1["DataSpedizione"]);
                p.DataConsegna = Convert.ToDateTime(sqlreader1["DataConsegna"]);
                p.DataC = Convert.ToDateTime(sqlreader1["DataConsegna"]).ToShortDateString();
                p.DataS = Convert.ToDateTime(sqlreader1["DataSpedizione"]).ToShortDateString();
                p.IdCliente = Convert.ToInt16(sqlreader1["IdCliente"]);
                p.Peso = Convert.ToInt16(sqlreader1["Peso"]);
                p.IdSpedizione = Convert.ToInt16(sqlreader1["IdSpedizione"]);
                spedizioni.Add(p);

            } 
            conn.Close();
            return spedizioni;
        
        }
         public static List<SelectListItem> DropdownClienti = new List<SelectListItem>();
        public static void Dropdown()
        {
            List<Spedizioni> sped = new List<Spedizioni>();
            sped = Spedizioni.Select();
            foreach (Spedizioni item in sped )
            {
                SelectListItem l = new SelectListItem { Text = item.NomeCliente, Value = item.IdCliente.ToString() };
                DropdownClienti.Add(l);
            }


        }

        public static void Insert(Spedizioni s,string messaggio, int Cliente)
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
          .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Spedizione VALUES(@Cliente,@DataSpedizione,@Peso,@Città,@Indirizzo,@NominativoDestinatario,@CostoSpedizione,@DataConsegna)";
                cmd.Parameters.AddWithValue("Cliente", Cliente);
                cmd.Parameters.AddWithValue("Indirizzo", s.Indirizzo);
                cmd.Parameters.AddWithValue("Città", s.Città);
                cmd.Parameters.AddWithValue("DataSpedizione", s.DataSpedizione);
                cmd.Parameters.AddWithValue("Peso", s.Peso);
                cmd.Parameters.AddWithValue("NominativoDestinatario", s.NominativoDestinatario);
                cmd.Parameters.AddWithValue("CostoSpedizione", s.CostoSpedizione);
                cmd.Parameters.AddWithValue("DataConsegna", s.DataConsegna);
            


                int inserimentoEffettuato = cmd.ExecuteNonQuery();

                if (inserimentoEffettuato > 0)
                {
                    messaggio = "Inserimento effetuato con successo";
                }

            }
            catch (Exception ex)
            {
                 messaggio = $"{ex}";
            }
            finally { conn.Close(); }
        }
        public static void SelectSpedizione( Spedizioni S)
    {   
        
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT * , Nome FROM Spedizione INNER JOIN Clienti ON Spedizione.Cliente=Clienti.IdCliente inner join Aggiornamenti on Spedizione.IdSpedizione=Aggiornamenti.IdSpedizioni inner join StatoSpedizione on Aggiornamenti.StatoSpedizione=StatoSpedizione.IdStato WHERE Spedizione.IdSpedizione=@id and CodiceFiscale=@CodiceFiscale", conn);
            SqlDataReader sqlreader1;
            conn.Open();

            cmd1.Parameters.AddWithValue("id",S.IdSpedizione);
            cmd1.Parameters.AddWithValue("CodiceFiscale", S.CodiceFiscale);
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Spedizioni p = new Spedizioni();
                p.NomeCliente =  sqlreader1["Nome"].ToString();
                p.Città = sqlreader1["Città"].ToString();
                p.Indirizzo = sqlreader1["Indirizzo"].ToString();
                p.NominativoDestinatario = sqlreader1["NominativoDestinatario"].ToString();
                p.CostoSpedizione =Convert.ToDouble( sqlreader1["CostoSpedizione"]);
                p.DataSpedizione =Convert.ToDateTime( sqlreader1["DataSpedizione"]);
                p.DataC= Convert.ToDateTime(sqlreader1["DataConsegna"]).ToShortDateString();
                p.DataS = Convert.ToDateTime(sqlreader1["DataSpedizione"]).ToShortDateString();
                p.DataConsegna =Convert.ToDateTime( sqlreader1["DataConsegna"]);
                p.Peso = Convert.ToInt16(sqlreader1["Peso"]);
                p.IdSpedizione = Convert.ToInt16(sqlreader1["IdSpedizione"]);
                p.Nomestato = sqlreader1["NomeStato"].ToString();
                p.Idstato = Convert.ToInt16(sqlreader1["IdStato"]);
                p.StatoSpedizione = Convert.ToInt16(sqlreader1["StatoSpedizione"]);
                p.IdAggiornamento = Convert.ToInt16(sqlreader1["IdAggiornamento"]);
                p.Luogo = sqlreader1["Luogo"].ToString();
                p.Descrizione = sqlreader1["Descrizione"].ToString();
                p.DataOraAggiornamento = Convert.ToDateTime(sqlreader1["DataOraAggiornamento"]);
                p.IdSpedizioni = Convert.ToInt16(sqlreader1["IdSpedizioni"]);
                spedizioni.Add(p);
              
            }
            conn.Close();
        }

        public static List<Spedizioni> SelectNome()
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT Nome,IdCliente From Clienti", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Spedizioni c = new Spedizioni();
                c.NomeCliente = sqlreader1["Nome"].ToString();
                c.IdCliente=Convert.ToInt16( sqlreader1["IdCliente"]);
                ListClienti.Add(c);

            }
            conn.Close();
            return ListClienti;

        }
        public static void Dropdown1()
        {
            List<Spedizioni> sped = new List<Spedizioni>();
            sped = SelectNome();
            foreach (Spedizioni item in sped)
            {
                SelectListItem l = new SelectListItem { Text = item.NomeCliente, Value = item.IdCliente.ToString() };
                DropdownClienti.Add(l);
            }


        }

        //Delete
        public static void Elimina()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
            .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Spedizione where IdSpedizione =@id";
            cmd.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["Id"]);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public static void SelectWhereId(int Id)
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT *  FROM Spedizione WHERE IdSpedizione=@id", conn);
            SqlDataReader sqlreader1;
            conn.Open();

            cmd1.Parameters.AddWithValue("id", Id);
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Spedizioni p = new Spedizioni();
                p.Cliente = Convert.ToInt16(sqlreader1["Cliente"]);
                p.Città = sqlreader1["Città"].ToString();
                p.Indirizzo = sqlreader1["Indirizzo"].ToString();
                p.NominativoDestinatario = sqlreader1["NominativoDestinatario"].ToString();
                p.CostoSpedizione = Convert.ToDouble(sqlreader1["CostoSpedizione"]);
                p.DataSpedizione = Convert.ToDateTime(sqlreader1["DataSpedizione"]);
                p.DataConsegna = Convert.ToDateTime(sqlreader1["DataConsegna"]);
                p.Peso = Convert.ToInt16(sqlreader1["Peso"]);
                p.IdSpedizione = Convert.ToInt16(sqlreader1["IdSpedizione"]);
                spedizioni.Add(p);

            }
            conn.Close();
        }

        //Modifica
        public static void Dropdown2(int id)
        {
            List<Spedizioni> sped = new List<Spedizioni>();
            sped = Spedizioni.SelectId(id);
            foreach (Spedizioni item in sped)
            {
                SelectListItem l = new SelectListItem { Text = item.NomeCliente, Value = item.IdCliente.ToString() };
                DropdownClienti.Add(l);
            }


        }
        public static List<Spedizioni> SelectId(int Id)
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT * , Nome, IdCliente FROM Spedizione INNER JOIN Clienti ON Spedizione.Cliente = Clienti.IdCliente  WHERE IdSpedizione=@id", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            cmd1.Parameters.AddWithValue("id", Id);
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Spedizioni p = new Spedizioni();
                p.NomeCliente= sqlreader1["Nome"].ToString();
                p.Città = sqlreader1["Città"].ToString();
                p.Indirizzo = sqlreader1["Indirizzo"].ToString();
                p.NominativoDestinatario = sqlreader1["NominativoDestinatario"].ToString();
                p.CostoSpedizione = Convert.ToDouble(sqlreader1["CostoSpedizione"]);
                p.DataSpedizione = Convert.ToDateTime(sqlreader1["DataSpedizione"]);
                p.DataConsegna = Convert.ToDateTime(sqlreader1["DataConsegna"]);
                p.DataC = Convert.ToDateTime(sqlreader1["DataConsegna"]).ToShortDateString();
                p.DataS = Convert.ToDateTime(sqlreader1["DataSpedizione"]).ToShortDateString();
                p.Peso = Convert.ToInt16(sqlreader1["Peso"]);
                p.IdCliente = Convert.ToInt16(sqlreader1["IdCliente"]);
                p.IdSpedizione = Convert.ToInt16(sqlreader1["IdSpedizione"]);
                spedizioni.Add(p);

            }
            conn.Close();
            return spedizioni;

        }
        public static void Modifica(Spedizioni s)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn2;
            cmd.CommandText = "UPDATE Spedizione SET Cliente=@Cliente,DataSpedizione=@DataSpedizione,Peso=@Peso,Città=@Città,Indirizzo=@Indirizzo,NominativoDestinatario=@NominativoDestinatario,CostoSpedizione=@CostoSpedizione,DataConsegna=@DataConsegna  where IdSpedizione=@id";
           
            cmd.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["Id"]);
            cmd.Parameters.AddWithValue("Cliente", s.Cliente);
            cmd.Parameters.AddWithValue("Indirizzo", s.Indirizzo);
            cmd.Parameters.AddWithValue("Città", s.Città);
            cmd.Parameters.AddWithValue("DataSpedizione", s.DataSpedizione);
            cmd.Parameters.AddWithValue("Peso", s.Peso);
            cmd.Parameters.AddWithValue("NominativoDestinatario", s.NominativoDestinatario);
            cmd.Parameters.AddWithValue("CostoSpedizione", s.CostoSpedizione);
            cmd.Parameters.AddWithValue("DataConsegna", s.DataConsegna);


            conn2.Open();

            cmd.ExecuteNonQuery();

            conn2.Close();
        }

        //Query
        public static void SelectData()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
           .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT *  FROM Aggiornamenti inner join Spedizione on Aggiornamenti.IdSpedizioni=Spedizione.IdSpedizione  inner join StatoSpedizione on Aggiornamenti.StatoSpedizione=StatoSpedizione.IdStato  where NomeStato='In Consegna' and DataConsegna=@Data", conn);
            SqlDataReader sqlreader1;
            conn.Open();

            DateTime now = DateTime.Today;
            cmd1.Parameters.AddWithValue("Data", now);
            sqlreader1 = cmd1.ExecuteReader();

            
            while (sqlreader1.Read())
            {
                Spedizioni p = new Spedizioni();
                p.Nomestato = sqlreader1["NomeStato"].ToString();
                p.Città = sqlreader1["Città"].ToString();
                p.Indirizzo = sqlreader1["Indirizzo"].ToString();
                p.NominativoDestinatario = sqlreader1["NominativoDestinatario"].ToString();
                p.CostoSpedizione = Convert.ToDouble(sqlreader1["CostoSpedizione"]);
                p.DataJson = Convert.ToDateTime(sqlreader1["DataSpedizione"]).ToShortDateString();
                p.DataJson2 = Convert.ToDateTime(sqlreader1["DataConsegna"]).ToShortDateString();
                p.Peso = Convert.ToInt16(sqlreader1["Peso"]);
                p.IdSpedizione = Convert.ToInt16(sqlreader1["IdSpedizione"]);
                spedizioni.Add(p);

            }
            conn.Close();
        }
        public static void SelectTotConsegna()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
           .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT count ( * ) as Totale FROM Aggiornamenti inner join Spedizione on Aggiornamenti.IdSpedizioni=Spedizione.IdSpedizione  inner join StatoSpedizione on Aggiornamenti.StatoSpedizione=StatoSpedizione.IdStato  where NomeStato='In Consegna' ", conn);
            SqlDataReader sqlreader1;
            conn.Open();

     

            sqlreader1 = cmd1.ExecuteReader();


            while (sqlreader1.Read())
            {
                Spedizioni p = new Spedizioni();
                p.Totale= Convert.ToInt16(sqlreader1["Totale"]);


                spedizioni.Add(p);

            }
            conn.Close();
        }
        public static void SelectTotConsegnaCittà()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
           .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT count ( * ) as Totale,Città FROM Aggiornamenti inner join Spedizione on Aggiornamenti.IdSpedizioni=Spedizione.IdSpedizione\r\n inner join StatoSpedizione on Aggiornamenti.StatoSpedizione=StatoSpedizione.IdStato  GROUP BY Città  ", conn);
            SqlDataReader sqlreader1;
            conn.Open();

            sqlreader1 = cmd1.ExecuteReader();


            while (sqlreader1.Read())
            {
                Spedizioni p = new Spedizioni();
                p.Totale = Convert.ToInt16(sqlreader1["Totale"]);
                p.Città = sqlreader1["Città"].ToString();


                spedizioni.Add(p);

            }
            conn.Close();
        }

    }




}