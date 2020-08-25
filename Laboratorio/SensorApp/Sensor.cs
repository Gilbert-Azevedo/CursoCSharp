using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Threading;
using Modelo;

namespace SensorApp
{
    class Sensor
    {
        private int labId;
        private DispatcherTimer timer;
        private Random random = new Random();
        public Sensor(int labId, TimeSpan intervalo)
        {
            this.labId = labId;
            timer = new DispatcherTimer();
            timer.Interval = intervalo;
            //timer.Tick += timerTick;
            timer.Tick += timerTickWeb;
        }
        public override string ToString()
        {
            return $"Sensor={labId}   Ligado={timer.IsEnabled}";
        }
        private void timerTick(object sender, EventArgs e)
        {
            Medicao m = new Medicao
            {
                LabId = labId,
                Horario = DateTime.Now,
                Temperatura = random.Next(200, 300) / 10.0
            };
            MedicaoDAL.Salvar(m);
        }
        private async void timerTickWeb(object sender, EventArgs e)
        {
            Medicao m = new Medicao
            {
                LabId = labId,
                Horario = DateTime.Now,
                Temperatura = random.Next(200, 300) / 10.0
            };
            //string ip = "http://localhost:51273";
            string ip = "http://localhost";
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            string s = "=" + JsonSerializer.Serialize<Medicao>(m);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            //await httpClient.PostAsync("/api/medicao", content);
            await httpClient.PostAsync("/lab/api/medicao", content);
        }
        public void Ligar()
        {
            timer.Start();
        }
        public void Desligar()
        {
            timer.Stop();
        }
    }
}
