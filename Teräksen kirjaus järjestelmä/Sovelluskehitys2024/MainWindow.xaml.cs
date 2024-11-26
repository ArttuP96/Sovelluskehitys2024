using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using Microsoft.Data.SqlClient;
namespace Sovelluskehitys2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string polku = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Arttu\\Documents\\testitietokanta.mdf;Integrated Security=True;Connect Timeout=30";
        public MainWindow()
        {
            InitializeComponent();

            ThemeManager.Current.ChangeTheme(this, "Light.Taupe");

            try
            {
                PaivitaDataGrid("SELECT materiaali, muoto, mitat FROM tuotteet ORDER BY materiaali, muoto", "tuotteet", tuotelista);
                PaivitaDataGrid("SELECT * FROM asiakkaat", "asiakkaat", asiakaslista);
                PaivitaDataGrid("SELECT * FROM tuotteet ORDER BY materiaali, muoto", "tuotteet", varastolista);
                PaivitaAsiakasComboBox();
                PaivitaMateriaaliComboBox();
                PaivitaMuotoComboBox();


            }
            catch
            {
                tilaviesti.Text = "Ei tietokantayhteyttä";
            }
        }

        private void PaivitaDataGrid(string kysely, string taulu, DataGrid grid)
        {
            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            SqlCommand komento = yhteys.CreateCommand();
            komento.CommandText = kysely;

            SqlDataAdapter adapteri = new SqlDataAdapter(komento);
            DataTable dt = new DataTable(taulu);
            adapteri.Fill(dt);

            grid.ItemsSource = dt.DefaultView;

            yhteys.Close();
        }

        private void PaivitaComboBox(ComboBox kombo1, ComboBox kombo2)
        {
            //tuotelista_cb.Items.Clear();

            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            SqlCommand komento = new SqlCommand("SELECT * FROM tuotteet", yhteys);
            SqlDataReader lukija = komento.ExecuteReader();

            DataTable taulu = new DataTable();
            taulu.Columns.Add("ID",  typeof(string));
            taulu.Columns.Add("NIMI", typeof(string));

            /* tehdään sidokset että comboboxissa näytetää datataulua*/
            kombo1.ItemsSource = taulu.DefaultView;
            kombo1.DisplayMemberPath = "NIMI";
            kombo1.SelectedValuePath = "ID";

            kombo2.ItemsSource = taulu.DefaultView;
            kombo2.DisplayMemberPath = "NIMI";
            kombo2.SelectedValuePath = "ID";

            while (lukija.Read()) // käsitellään kyselytulos rivi riviltä
            {
                int id = lukija.GetInt32(0); 
                string nimi = lukija.GetString(1);
                taulu.Rows.Add(id, nimi); // lisätään datatauluun rivi tietoineen
                //tuotelista_cb.Items.Add(lukija.GetString(1));
            }
            lukija.Close();

            yhteys.Close();
        }


        private void PaivitaAsiakasComboBox()
        {
            //tuotelista_cb.Items.Clear();
            
            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            SqlCommand komento = new SqlCommand("SELECT * FROM asiakkaat", yhteys);
            SqlDataReader lukija = komento.ExecuteReader();

            DataTable taulu = new DataTable();
            taulu.Columns.Add("ID", typeof(string));
            taulu.Columns.Add("NIMI", typeof(string));

            while (lukija.Read()) // käsitellään kyselytulos rivi riviltä
            {
                int id = lukija.GetInt32(0);
                string nimi = lukija.GetString(1);
                taulu.Rows.Add(id, nimi); // lisätään datatauluun rivi tietoineen
                //tuotelista_cb.Items.Add(lukija.GetString(1));
            }
            lukija.Close();
            yhteys.Close();
        }

        private void PaivitaMateriaaliComboBox() //kopioitu asiakas cb:stä
        {
            //tuotelista_cb.Items.Clear();

            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            SqlCommand komento = new SqlCommand("SELECT distinct materiaali FROM tuotteet", yhteys);
            SqlDataReader lukija = komento.ExecuteReader();

            DataTable taulu = new DataTable();
            //taulu.Columns.Add("ID", typeof(string));
            taulu.Columns.Add("NIMI", typeof(string));

            while (lukija.Read()) // käsitellään kyselytulos rivi riviltä
            {
                //int id = lukija.GetInt32(0);
                string nimi = lukija.GetString(0);
                taulu.Rows.Add(nimi); // lisätään datatauluun rivi tietoineen
                materiaali_cb.Items.Add(lukija.GetString(0));
            }
            lukija.Close();
            yhteys.Close();
        }

        private void PaivitaMuotoComboBox() //kopioitu asiakas cb:stä
        {
            //tuotelista_cb.Items.Clear();

            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            SqlCommand komento = new SqlCommand("SELECT distinct muoto FROM tuotteet", yhteys);
            SqlDataReader lukija = komento.ExecuteReader();

            DataTable taulu = new DataTable();
            //taulu.Columns.Add("ID", typeof(string));
            taulu.Columns.Add("NIMI", typeof(string));

            while (lukija.Read()) // käsitellään kyselytulos rivi riviltä
            {
                //int id = lukija.GetInt32(0);
                string nimi = lukija.GetString(0);
                taulu.Rows.Add(nimi); // lisätään datatauluun rivi tietoineen
                Muoto_cb.Items.Add(lukija.GetString(0));
            }
            lukija.Close();
            yhteys.Close();
        }

        private void Lisää_tuote_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            string kysely = "INSERT INTO tuotteet (materiaali, muoto, mitat, määrä) VALUES ('" + materiaali.Text + "','" + muoto.Text + "','" + mitat.Text + "','" + 0 + "');";
            SqlCommand komento = new SqlCommand(kysely, yhteys);
            komento.ExecuteNonQuery();

            yhteys.Close();

            PaivitaDataGrid("SELECT materiaali, muoto, mitat FROM tuotteet ORDER BY materiaali, muoto", "tuotteet", tuotelista);
            PaivitaDataGrid("SELECT * FROM tuotteet ORDER BY materiaali, muoto", "tuotteet", varastolista);
        }



        private void Lisää_Asiakas_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            string kysely = "INSERT INTO asiakkaat (yrityksen_nimi, yhteys_henkilö, puhelin_numero) VALUES ('" + yrityksen_nimi.Text + "','" + yhteys_henkilö.Text + "','" + puhelin_numero.Text + "');";
            SqlCommand komento = new SqlCommand(kysely, yhteys);
            komento.ExecuteNonQuery();

            yhteys.Close();

            PaivitaDataGrid("SELECT * FROM asiakkaat", "asiakkaat", asiakaslista);
            PaivitaAsiakasComboBox();
        }

        private void Lisää_varastoon(object sender, RoutedEventArgs e)
        {
            SqlConnection yhteys = new SqlConnection(polku);
            yhteys.Open();

            string materiaali = materiaali_cb.SelectedValue.ToString();
            string muoto = Muoto_cb.SelectedValue.ToString();

            //UPDATE tuotteet SET määrä=15 WHERE materiaali='ALU' AND muoto = 'neliöputki' AND mitat = '30x30x2';
            string kysely = "UPDATE tuotteet SET määrä=" + määrä.Text + " WHERE materiaali='" + materiaali + "' AND muoto = '"+ muoto +"' AND mitat = '" + mitat.Text + "';";
            SqlCommand komento = new SqlCommand(kysely, yhteys);
            komento.ExecuteNonQuery();

            yhteys.Close();

            poista.Content=kysely;

            PaivitaDataGrid("SELECT materiaali, muoto, mitat FROM tuotteet ORDER BY materiaali, muoto", "tuotteet", tuotelista);
            PaivitaDataGrid("SELECT * FROM tuotteet ORDER BY materiaali, muoto", "tuotteet", varastolista);
        }
    }
}