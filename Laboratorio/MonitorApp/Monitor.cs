using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Threading;
using Modelo;

namespace MonitorApp
{
    class Monitor
    {
        private Laboratorio lab;
        private DispatcherTimer timer;
        private MonitorWindow mw;
        public Monitor(Laboratorio lab, TimeSpan intervalo)
        {
            this.lab = lab;
            timer = new DispatcherTimer();
            timer.Interval = intervalo;
            //timer.Tick += timerTick;
            timer.Tick += timerTickWeb;
            mw = new MonitorWindow();
            mw.Title = lab.Descricao;
        }
        public override string ToString()
        {
            return $"Monitor={lab.Descricao}   Ligado={timer.IsEnabled}";
        }
        private void timerTick(object sender, EventArgs e)
        {
            mw.list.ItemsSource = null;
            mw.list.ItemsSource = MedicaoDAL.Abrir(lab.Id);
        }
        private async void timerTickWeb(object sender, EventArgs e)
        {
            //string ip = "http://localhost:51273";
            string ip = "http://localhost";
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            //var response = await httpClient.GetAsync($"/api/medicao/{lab.Id}");
            var response = await httpClient.GetAsync($"/lab/api/medicao/{lab.Id}");
            var s = response.Content.ReadAsStringAsync().Result;
            List<Medicao> m = JsonSerializer.Deserialize<List<Medicao>>(s);
            mw.list.ItemsSource = null;
            mw.list.ItemsSource = m;
        }
        public void Ligar()
        {
            timer.Start();
            mw.Show();
        }
        public void Desligar()
        {
            timer.Stop();
            mw.Hide();
        }
    }
}
