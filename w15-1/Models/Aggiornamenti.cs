using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace w15_1.Models
{
    public class Aggiornamenti
    {
        public int IdAggiornamento { get; set; }
        [Required(ErrorMessage = "Il campo stato spedizione non è valido")]
      //  [Display(Name = "Inserisci lo stato della spedizione")]
        public int StatoSpedizione { get; set; }
        [Required(ErrorMessage = "Il campo luogo non è valido")]
      //  [Display(Name = "Inserisci il luogo")]
        public string Luogo { get; set; }
        [Required(ErrorMessage = "Il campo descrizione non è valido")]
       // [Display(Name = "Inserisci descrizione")]
        public string Descrizione { get; set; }
        public DateTime DataOraAggiornamento { get; set; }
        [Required(ErrorMessage = "Il campo  non è valido")]
       // [Display(Name = "Inserisci codice identificativo ")]
        public int IdSpedizioni { get; set; }

        //StatoSpedizione
        public int Idstato { get; set; }
        public string Nomestato { get; set; }

        public static List<SelectListItem> DropdownAggiornamenti = new List<SelectListItem>();
        public static List<Aggiornamenti> ListAggiornamenti = new List<Aggiornamenti>();

        //select
        public static List<Aggiornamenti> Select()
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT * From StatoSpedizione", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Aggiornamenti p = new Aggiornamenti();
                p.Nomestato = sqlreader1["NomeStato"].ToString();
                p.Idstato = Convert.ToInt16(sqlreader1["IdStato"]);
                ListAggiornamenti.Add(p);

            }
            conn.Close();
            return ListAggiornamenti;

        }
        public static List<Aggiornamenti> SelectId()
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT * From StatoSpedizione ", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Aggiornamenti p = new Aggiornamenti();
                p.Nomestato = sqlreader1["NomeStato"].ToString();
                p.Idstato = Convert.ToInt16(sqlreader1["IdStato"]);
                ListAggiornamenti.Add(p);

            }
            conn.Close();
            return ListAggiornamenti;

        }
        public static void SelectAggiornamenti()
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT *,NomeStato,IdStato From Aggiornamenti INNER JOIN StatoSpedizione on Aggiornamenti.StatoSpedizione=StatoSpedizione.IdStato", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Aggiornamenti p = new Aggiornamenti();
                p.Nomestato = sqlreader1["NomeStato"].ToString();
                p.Idstato = Convert.ToInt16(sqlreader1["IdStato"]);
                p.Luogo = sqlreader1["Luogo"].ToString();
                p.Descrizione = sqlreader1["Descrizione"].ToString();
                p.IdAggiornamento = Convert.ToInt16(sqlreader1["IdAggiornamento"]);
                p.DataOraAggiornamento =Convert.ToDateTime( sqlreader1["DataOraAggiornamento"]);
                p.IdSpedizioni = Convert.ToInt16(sqlreader1["IdSpedizioni"]);
                ListAggiornamenti.Add(p);

            }
            conn.Close();
           

        }
        public static void SelectAggiornamentiWhereId()
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT *,NomeStato,IdStato From Aggiornamenti INNER JOIN StatoSpedizione on Aggiornamenti.StatoSpedizione=StatoSpedizione.IdStato Where IdSpedizioni=@id", conn);
            SqlDataReader sqlreader1;

            cmd1.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["Id"]);
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();
            Aggiornamenti p = new Aggiornamenti();
            while (sqlreader1.Read())
            {
               
                p.Nomestato = sqlreader1["NomeStato"].ToString();
                p.Idstato = Convert.ToInt16(sqlreader1["IdStato"]);
                p.StatoSpedizione = Convert.ToInt16(sqlreader1["StatoSpedizione"]);
                p.IdAggiornamento = Convert.ToInt16(sqlreader1["IdAggiornamento"]);
                p.Luogo = sqlreader1["Luogo"].ToString();
                p.Descrizione = sqlreader1["Descrizione"].ToString();
                p.DataOraAggiornamento = Convert.ToDateTime(sqlreader1["DataOraAggiornamento"]);
                p.IdSpedizioni = Convert.ToInt16(sqlreader1["IdSpedizioni"]);
                

            }
     ListAggiornamenti.Add(p);
            conn.Close();


        }
        public static void Dropdown()
        {
            List<Aggiornamenti> sped = new List<Aggiornamenti>();
            sped = Aggiornamenti.Select();
            foreach (Aggiornamenti item in sped)
            {
                SelectListItem l = new SelectListItem { Text = item.Nomestato, Value = item.Idstato.ToString() };
                DropdownAggiornamenti.Add(l);
            }


        }

         //Modifica insert e elimina
        public static void Insert(Aggiornamenti a, string messaggio, int Stato)
        {
            DateTime now = DateTime.Now;
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
          .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Aggiornamenti VALUES(@StatoSpedizione,@Luogo,@Descrizione,@DataOraAggiornamento,@IdSpedizioni)";
                cmd.Parameters.AddWithValue("StatoSpedizione", Stato);
                cmd.Parameters.AddWithValue("Luogo", a.Luogo);
                cmd.Parameters.AddWithValue("Descrizione", a.Descrizione);
                cmd.Parameters.AddWithValue("DataOraAggiornamento", now );
                cmd.Parameters.AddWithValue("IdSpedizioni", HttpContext.Current.Request.QueryString["Id"]);

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
        public static void Elimina()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
            .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Aggiornamenti where IdAggiornamento =@id";
            cmd.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["Id"]);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public static void Modifica(Aggiornamenti s, int Stato)
        {
            DateTime now = DateTime.Now;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn2;
            cmd.CommandText = "UPDATE Aggiornamenti SET StatoSpedizione=@StatoSpedizione,Luogo=@Luogo,Descrizione=@Descrizione,DataOraAggiornamento=@DataOraAggiornamento,IdSpedizioni=@IdSpedizioni where IdAggiornamento=@id";
            Aggiornamenti a = new Aggiornamenti();
            cmd.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["Id"]);
            cmd.Parameters.AddWithValue("StatoSpedizione", Stato);
            cmd.Parameters.AddWithValue("Luogo", s.Luogo);
            cmd.Parameters.AddWithValue("Descrizione", s.Descrizione);
            cmd.Parameters.AddWithValue("DataOraAggiornamento", now);
            cmd.Parameters.AddWithValue("IdSpedizioni", HttpContext.Current.Request.QueryString["IdS"]);
           
            conn2.Open();

            cmd.ExecuteNonQuery();

            conn2.Close();
        }
    }


}